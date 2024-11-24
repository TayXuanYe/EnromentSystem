using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentStatementPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sid"] != null)
        {

        }
        else
        {
            Response.Redirect("StudentLoginPage.aspx");
        }

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        string studentID = Session["sid"].ToString();
        string htmlContent = GenerateStudentPaymentHtml(studentID);

        PdfGenerator pdfGenerator = new PdfGenerator();
        pdfGenerator.GenerateLandscapePdf(htmlContent, "StudentStatement.pdf", "StudentStatement");

        DisplayPdf("StudentStatement.pdf");
    }
    private string GenerateStudentPaymentHtml(string studentID)
    {
        string cdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string date = "";
        string studentName = "";
        string program = "";
        string ic = "";
        string sid = "";
        string pid = "";
        string process = "";
        string particulars = "";
        string documentNo = "";
        string session = "";
        double amount = 0.0;
        string endDate = txtEndDate.Text;
        string startDate = txtStartDate.Text;
        DateTime date1 = DateTime.Parse(startDate);
        DateTime date2 = DateTime.Parse(endDate);
        string statementdate = txtStartDate.Text + " to " + txtEndDate.Text;

        DataSet studentDetails = DatabaseManager.GetRecord(
            "student",
            new List<string> { "name", "sid", "ic_or_passport", "program" },
            $@"WHERE sid = '{studentID}'"
        );
        if (studentDetails != null)
        {
            foreach (DataRow row in studentDetails.Tables[0].Rows)
            {
                studentName = row["name"].ToString();
                sid = row["sid"].ToString();
                ic = row["ic_or_passport"].ToString();
                program = row["program"].ToString();
            }
        }

        DataSet previousPayments = DatabaseManager.GetRecord(
     "payment",
     new List<string> { "pid", "sid", "date", "process", "particulars", "documentNo", "session", "amount" },
     $@"WHERE sid = '{studentID}' AND date <= '{startDate}'"
 );

        double cuAmount = 0.0;
        string table = "<tr>";

        if (previousPayments != null)
        {
            foreach (DataRow row in previousPayments.Tables[0].Rows)
            {
                amount = double.Parse(row["amount"].ToString());
                cuAmount += amount;
            }
            table += $"<td>0</td><td></td><td>{startDate}</td><td></td><td>B/F</td><td></td><td></td><td>{cuAmount}</td><td>{cuAmount}</td>";
            table += "</tr><tr>";
        }

        DataSet paymentDetails = DatabaseManager.GetRecord(
            "payment",
            new List<string> { "pid", "sid", "date", "process", "particulars", "documentNo", "session", "amount" },
            $@"WHERE sid = '{studentID}' AND date >= '{startDate}' AND date <= '{endDate}'"
        );

        if (paymentDetails != null)
        {
            foreach (DataRow row in paymentDetails.Tables[0].Rows)
            {
                DateTime paymentDate = DateTime.Parse(row["date"].ToString());
                pid = Convert.ToString(row["pid"]);
                sid = Convert.ToString(row["sid"]);
                date = Convert.ToString(row["date"]);
                process = row["process"].ToString();
                particulars = row["particulars"].ToString();
                documentNo = row["documentNo"].ToString();
                session = row["session"].ToString();
                amount = double.Parse(row["amount"].ToString());
                cuAmount += amount;

                table += $"<td>{pid}</td><td>{sid}</td><td>{date}</td><td>{process}</td><td>{particulars}</td><td>{documentNo}</td><td>{session}</td><td>{amount}</td><td>{cuAmount}</td>";
                table += "</tr><tr>";
            }
            table += "</tr>";
        }

        string htmlContent = $@"
            <html>
            <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>Student Statement</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    margin: 0;
                    padding: 0;
                    text-align: center;
                }}

                header {{
                    margin-bottom: 20px;
                    line-height: 1.6;
                }}

                h1 {{
                    font-weight: bold;
                }}

                .date-time {{
                    margin-top: 10px;
                    font-size: 14px;
                }}

                .content {{
                    margin: 20px 0;
                }}

                .table-container {{
                    margin: 20px auto;
                    width: 80%;
                    text-align: left;
                }}

                table {{
                    width: 100%;
                    border-collapse: collapse;
                }}

                table, th, td {{
                    border: 1px solid black;
                }}

                table th, table td {{
                    padding: 8px;
                    text-align: center;
                }}

                .no-border {{
                    border: none;
                    width: 80%;
                    margin: 0 auto;
                }}

                .no-border td {{
                    border: none;
                    padding: 8px;
                }}

                .summary-table {{
                    width: 50%;
                    margin: 20px auto;
                    border: 1px solid black;
                }}

                .summary-table td {{
                    padding: 8px;
                    text-align: center;
                }}

                .summary-table td:first-child {{
                    font-weight: bold;
                }}
            </style>
            </head>
            <body>

                <header>
                    <p>INTI INTERNATIONAL EDUCATION SDN BHD</p>
                    <p>Registration No: 199401043150 (328838A)</p>
                    <p>PERSIARAN PERDANA BBN, PUTRA NILAI, 71800 NILAI</p>
                </header>

                <h1>STUDENT STATEMENT - UNIVERSITY FEE</h1>

                <div class=""date-time"">
                    <p>{cdate}</p>
                </div>

                <div class=""content"">
                    <h2>STUDENT DETAILS</h2>

                    <table class=""no-border"">
                        <tr>
                            <td>Statement Date</td>
                            <td>{statementdate}</td>
                        </tr>
                        <tr>
                            <td>Student Name</td>
                            <td>{studentName}</td>
                        </tr>
                        <tr>
                            <td>Matriculation No</td>
                            <td>{sid}</td>
                        </tr>
                        <tr>
                            <td>IC No. / Passport No.</td>
                            <td>{ic}</td>
                        </tr>
                        <tr>
                            <td>Program</td>
                            <td>{program}</td>
                        </tr>
                    </table>
                </div>

                <div class=""table-container"">
                    <h2>Payment Details</h2>
                    <table>
                        <thead>
                            <tr>
                                <th>paymentid</th>
                                <th>studentid</th>
                                <th>date</th>
                                <th>process</th>
                                <th>particulars</th>
                                <th>documentNo</th>
                                <th>session</th>
                                <th>amount</th>
                                <th>totalamount</th>
                            </tr>
                        </thead>
                        <tbody id=""paymentData"">
                          {table}
                        </tbody>
                    </table>
                </div>
            </body>
            </html>";

        return htmlContent;
    }

    private void DisplayPdf(string fileName)
    {
        string pdfUrl = ResolveUrl($"~/StudentStatement/{fileName}");
        pdfFrame.Attributes["src"] = pdfUrl;
    }


}