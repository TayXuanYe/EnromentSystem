using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class InvoiceAndAdjustmentNote : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindInvoiceData();
            if (Request.QueryString["pdf"] != null)
            {
                string particulars = Request.QueryString["pdf"];
                DisplayPdf(particulars);
            }
        }
    }

    protected void gvInvoices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string particulars = DataBinder.Eval(e.Row.DataItem, "Particular").ToString();
            HyperLink link = new HyperLink
            {
                Text = particulars,
                NavigateUrl = $"javascript:loadPdf('{particulars}');"
            };
            e.Row.Cells[0].Controls.Add(link);
        }
    }

    private void BindInvoiceData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Particular", typeof(string));
        dt.Columns.Add("Type", typeof(string));
        dt.Columns.Add("DocumentDate", typeof(DateTime));
        dt.Columns.Add("Amount", typeof(decimal));
        dt.Columns.Add("AmountSettled", typeof(decimal));

        dt.Rows.Add("IM24BIT100236", "INVOICE", new DateTime(2024, 8, 21), 6675.00m, 6675.00m);
        dt.Rows.Add("DM24BIT100091", "DEBIT NOTE", new DateTime(2024, 8, 22), 2225.00m, 2225.00m);
        dt.Rows.Add("CM24BIT100130", "CREDIT NOTE", new DateTime(2024, 8, 22), 0.00m, 0.00m);

        gvInvoices.DataSource = dt;
        gvInvoices.DataBind();
    }

    private void DisplayPdf(string particulars)
    {
        byte[] pdfBytes = GenerateInvoicePdf(particulars);
        if (pdfBytes != null)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", $"inline; filename={particulars}.pdf");
            Response.BinaryWrite(pdfBytes);
            Response.End();
        }
    }

    private byte[] GenerateInvoicePdf(string particulars)
    {
        // 获取学生和发票数据
        var studentData = GetStudentData();
        var invoiceData = GetInvoiceData(particulars);

        // 创建PDF文件
        string folderPath = Server.MapPath("~/PDFs");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, $"{particulars}.pdf");

        using (MemoryStream ms = new MemoryStream())
        {
            using (var writer = new iText.Kernel.Pdf.PdfWriter(ms))
            {
                var pdf = new iText.Kernel.Pdf.PdfDocument(writer);
                var document = new iText.Layout.Document(pdf);

                document.Add(new iText.Layout.Element.Paragraph("INTI INTERNATIONAL EDUCATION SDN BHD")
                .SetFontSize(12)
                .SetBold()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                document.Add(new iText.Layout.Element.Paragraph("Registration No: 19940103150 (328883A)")
                .SetFontSize(10)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                document.Add(new iText.Layout.Element.Paragraph("PERSIARAN PERDANA BBN, PUTRA NILAI, 71800 NILAI,\nNEGERI SEMBILAN, MALAYSIA.")
                .SetFontSize(10)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                document.Add(new iText.Layout.Element.Paragraph("TEL: 06-7982000     FAX: 06-7997532")
                .SetFontSize(10)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                document.Add(new iText.Layout.Element.Paragraph("INVOICE")
                .SetFontSize(12)
                .SetBold()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                document.Add(new iText.Layout.Element.Paragraph($"DATE: {invoiceData.DocumentDate:dd MMM yyyy}")
                .SetFontSize(10)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                document.Add(new iText.Layout.Element.Paragraph($"NO: {invoiceData.Particular}")
                .SetFontSize(10)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                document.Add(new iText.Layout.Element.Paragraph("\n"));

                iText.Layout.Element.Table studentTable = new iText.Layout.Element.Table(new float[] { 1, 2 }).UseAllAvailableWidth();
                studentTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph("STUDENT NAME :")).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
                studentTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph(studentData.FullName)).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
                studentTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph("MATRICULATION NO :")).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
                studentTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph(studentData.Sid)).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
                studentTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph("IC / PASSPORT NO :")).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
                studentTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph(studentData.IcoRpassport)).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
                studentTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph("PROGRAM :")).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
                studentTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph(studentData.Program)).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
                studentTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph("SEMESTER :")).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
                studentTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph(studentData.Semester)).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
                document.Add(studentTable);

                // Space between sections
                document.Add(new iText.Layout.Element.Paragraph("\n"));

                // Fees Table
                iText.Layout.Element.Table feesTable = new iText.Layout.Element.Table(new float[] { 3, 1 }).UseAllAvailableWidth();
                feesTable.AddHeaderCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph("PARTICULARS").SetBold()));
                feesTable.AddHeaderCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph("AMOUNT\nRM").SetBold()));
                feesTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph(invoiceData.Type?.ToString() ?? "N/A")));
                feesTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph(invoiceData.Amount.ToString("N2"))));
                // Add total row
                feesTable.AddCell(new iText.Layout.Element.Cell(1, 2).Add(new iText.Layout.Element.Paragraph(" ")));  // Empty row for alignment
                feesTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph("TOTAL").SetBold()));
                feesTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph(invoiceData.Amount.ToString("N2")).SetBold()));
                document.Add(feesTable);

                // Space between sections
                document.Add(new iText.Layout.Element.Paragraph("\n"));

                // Footer Section
                iText.Layout.Element.Table footerTable = new iText.Layout.Element.Table(new float[] { 2 }).UseAllAvailableWidth();
                footerTable.AddCell(new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph("1. Applicability of Student Charges and Fee Policy (“Policy”)\r\ni. This Policy applies to all students who have successfully enrolled in INTI and completed the registration.\r\nii. Students are advised to review this Policy in detail and to take note of the consequences highlighted. For queries or clarification on the Policy, students are to contact Finance Office.\n\n" +
                    "2. Payment of fees – obligation, penalty and consequences\ni. It is the responsibility of INTI student to ensure timely payment of fees and other related charges associated with the respective programme of\r\nstudy. Details of fees are set out in the Fee Schedule, forwarded with the Offer Letter.\r\nii. All fees paid (except deposit) are neither refundable nor transferable once the semester has commenced.\r\niii. All payment should be in the form of cheque or bank draft or telegraphic transfer made payable to INTI INTERNATIONAL EDUCATION SDN BHD. Please indicate your name, NRIC/passport number and contact number on the reverse side of the cheque or bank draft.\n\n" +
                    "3. The following would be applicable to new and returning students \ni. All fees are payable in advance except for students who apply Monthly Payment Plan. Please see additional terms and conditions of Monthly\r\nPayment Plan.\nii. Full settlement of semester fees is required upon registration or by the start date of semester and according to the due dates for subsequent\r\nsemesters.\niii. For returning INTI students,a late payment charge of Ringgit Malaysia Three Hundred (RM300) will be imposed commencing from Day 4 Week 2 of the semester.\n\r" +
                    "Note:\r\nIf at the end of Day 4 Week 2, the fees continue to be outstanding with no justifiable explanation received for the delay, INTI reserves the right to\r\nreview the status of the student and to take such necessary action as it deems fit, including but not limited to the cancellation of enrollment (auto\r\ndrop), barring the student from classes and facilities, suspension, withholding of all examination results, certificates and records of the student. Students who have not made full payment of their outstanding fees by the end of Day 3 Week 3 of the semester calendar for their respective programmes, student enrollment shall be cancelled (auto drop from the respective programmes). Between Week 4 and Week 5, students can\r\nre-enroll into their respective programmes subject to full payment of semester fees and a late payment charge of Ringgit Malaysia Three\r\nHundred (RM300). By the end of Day 5 Week 5, students shall not be re-enrolled into their respective programmes.\n" +
                    "This is a computer generated document. No signature is required. ")));
                document.Add(footerTable);
                document.Close();
            }

            return ms.ToArray();
        }
    }

    // 方法：从数据库获取学生数据
    private (string Sid, string FullName, string IcoRpassport, string Program, string Semester) GetStudentData()
    {
        string sid = "I18016442";  // 假设我们已经有sid
        string connectionString = "Server=云烟\\SQLEXPRESS;Database=StudentEnrollmentSystem;Integrated Security=True;";
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT sid, fullname, icorpassport, program, semester FROM student WHERE sid = @sid";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@sid", sid);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (reader["sid"].ToString(),
                                reader["fullname"].ToString(),
                                reader["icorpassport"].ToString(),
                                reader["program"].ToString(),
                                reader["semester"].ToString());
                    }
                }
            }
        }
        return (null, null, null, null, null);  // 如果没有找到数据，返回默认值
    }


    // 方法：从数据库获取发票数据
    private (string Particular, string Sid, string Type, DateTime DocumentDate, decimal Amount) GetInvoiceData(string particulars)
    {
        string connectionString = "Server=云烟\\SQLEXPRESS;Database=StudentEnrollmentSystem;Integrated Security=True;";
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT particular, sid, feetype, documentdate, amount FROM invoice WHERE particular = @particulars";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@particulars", particulars);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (reader["particular"].ToString(),
                                reader["sid"].ToString(),
                                reader["feetype"].ToString(),
                                Convert.ToDateTime(reader["documentdate"]),
                                Convert.ToDecimal(reader["amount"]));
                    }
                }
            }
        }
        return (null, null, null, DateTime.MinValue, 0);  // 如果没有找到数据，返回默认值
    }


}