using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string studentID = "I23024312";
        string htmlContent = GenerateStudentPaymentHtml(studentID);

        PdfGenerator pdfGenerator = new PdfGenerator();
        pdfGenerator.GenerateLandscapePdf(htmlContent, "Default2.pdf", "Default2");

        DisplayPdf("Default2.pdf");
    }

    private string GenerateStudentPaymentHtml(string studentID)
    {
        string date = DateTime.Now.ToString("yyyy-MM-dd");
        string studentName = "";
        string program = "";
        string level = "";
        string paymentStatus = "";
        decimal totalFees = 0, paidAmount = 0, outstandingBalance = 0;

        DataSet studentDetails = DatabaseManager.GetRecord(
            "student",
            new List<string> { "name", "program", "level" },
            $"WHERE sid = '{studentID}'"
        );
        if (studentDetails != null)
        {
            foreach (DataRow row in studentDetails.Tables[0].Rows)
            {
                studentName = row["name"].ToString();
                program = row["program"].ToString();
                level = row["level"].ToString();
            }
        }

        DataSet paymentDetails = DatabaseManager.GetRecord(
            "payment",
            new List<string> { "total_fees", "paid_amount", "outstanding_balance", "payment_status" },
            $"WHERE sid = '{studentID}'"
        );
        if (paymentDetails != null)
        {
            foreach (DataRow row in paymentDetails.Tables[0].Rows)
            {
                totalFees = Convert.ToDecimal(row["total_fees"]);
                paidAmount = Convert.ToDecimal(row["paid_amount"]);
                outstandingBalance = Convert.ToDecimal(row["outstanding_balance"]);
                paymentStatus = row["payment_status"].ToString();
            }
        }

        string htmlContent = $@"
            <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                        }}
                        .header {{
                            width: 100%;
                            margin-bottom: 20px;
                        }}
                        .header td {{
                            padding: 5px;
                            font-size: 14px;
                        }}
                        .details, .payment {{
                            width: 100%;
                            border-collapse: collapse;
                            margin: 20px 0;
                        }}
                        .details th, .payment th {{
                            background-color: #f2f2f2;
                            text-align: left;
                        }}
                        .details td, .payment td {{
                            border: 1px solid #ddd;
                            padding: 8px;
                        }}
                        .details th, .payment th {{
                            padding-top: 12px;
                            padding-bottom: 12px;
                            font-size: 14px;
                        }}
                    </style>
                </head>
                <body>
                    <h2>Student Payment Summary</h2>
                    <table class='header'>
                        <tr>
                            <td>Date:</td>
                            <td>{date}</td>
                        </tr>
                        <tr>
                            <td>Student Name:</td>
                            <td>{studentName}</td>
                        </tr>
                        <tr>
                            <td>Program:</td>
                            <td>{program}</td>
                        </tr>
                        <tr>
                            <td>Level:</td>
                            <td>{level}</td>
                        </tr>
                    </table>
                    <h3>Payment Details</h3>
                    <table class='payment'>
                        <tr>
                            <th>Total Fees</th>
                            <th>Paid Amount</th>
                            <th>Outstanding Balance</th>
                            <th>Payment Status</th>
                        </tr>
                        <tr>
                            <td>${totalFees}</td>
                            <td>${paidAmount}</td>
                            <td>${outstandingBalance}</td>
                            <td>{paymentStatus}</td>
                        </tr>
                    </table>
                </body>
            </html>";

        return htmlContent;
    }

    private void DisplayPdf(string fileName)
    {
        string pdfUrl = ResolveUrl($"~/Default2/{fileName}");
        pdfFrame.Attributes["src"] = pdfUrl;
    }

}
