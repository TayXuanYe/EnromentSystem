using System;
using System.IO;
using System.Web.UI;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.Kernel.Colors;

public partial class ClassTimetable : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sid = Request.QueryString["sid"]; // Get student SID from query string
            byte[] pdfBytes = GenerateClassTimeTablePdf(sid); // Generate PDF

            // Save PDF to the server for embedding
            string pdfFilePath = Server.MapPath($"~/PDFs/{sid}classtimetable.pdf");
            File.WriteAllBytes(pdfFilePath, pdfBytes);

            // Display PDF in an iframe
            LiteralPdfPreview.Text = $"<iframe src='PDFs/{sid}classtimetable.pdf' width='100%' height='600px'></iframe>";
        }
    }

    private byte[] GenerateClassTimeTablePdf(string sid)
    {
        // Simulating student data retrieval
        var studentData = GetStudentData();

        // Simulating class timetable data (you may replace this with actual data)
        // Define a 2D array with 8 rows (MON-SUN + header) and 16 columns (time slots + header)

        string[,] timetableData = new string[8, 16]
        {
    { "Class Timetable", "0800", "0900", "1000", "1100", "1200", "1300", "1400", "1500", "1600", "1700", "1800", "1900", "2000", "2100", "2200" },
    { "MON", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
    { "TUE", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
    { "WED", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
    { "THU", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
    { "FRI", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
    { "SAT", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
    { "SUN", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" }
        };


        // Ensure the PDFs folder exists
        string folderPath = Server.MapPath("~/PDFs");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = System.IO.Path.Combine(folderPath, $"{sid}classtimetable.pdf");

        // Create PDF in memory using iTextSharp
        using (MemoryStream ms = new MemoryStream())
        {
            // Create PdfWriter and PdfDocument
            var writer = new iText.Kernel.Pdf.PdfWriter(ms);
            var pdf = new iText.Kernel.Pdf.PdfDocument(writer);
            var document = new iText.Layout.Document(pdf, iText.Kernel.Geom.PageSize.A4.Rotate());
            DateTime currentDate = DateTime.Now;

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
            document.Add(new iText.Layout.Element.Paragraph($"DATE: {currentDate:dd MMM yyyy}")
            .SetFontSize(10)
            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
            // Add Student Information (no border table)
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

            // Add a page break after student data
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            // Create Class Timetable Table (with black borders)
            iText.Layout.Element.Table timetableTable = new iText.Layout.Element.Table(16).SetWidth(100);
            timetableTable.SetBorder(iText.Layout.Borders.Border.NO_BORDER);

            // Add Table Headers (First row)
            for (int i = 0; i < 16; i++)
            {
                timetableTable.AddCell(new Cell().Add(new Paragraph(timetableData[0, i])).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBold());
            }

            // Add Days of the Week (MON, TUE, etc.)
            for (int row = 1; row < 8; row++)
            {
                for (int col = 0; col < 16; col++)
                {
                    timetableTable.AddCell(new Cell().Add(new Paragraph(timetableData[row, col])).SetTextAlignment(TextAlignment.CENTER));
                }
            }

            document.Add(timetableTable);

            // Add footer text
            var footerText = "Please check your student email account I18016442@student.newinti.edu.my for all communication from the university. To access your webmail, please visit http://mail.student.newinti.edu.my. If you encounter problems signing into your email account, kindly send an email to google.helpdesk@newinti.edu.my.";
            document.Add(new Paragraph(footerText).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10).SetMarginTop(20));

            // Close the document (this writes the PDF content to memory)
            document.Close();

            // Save the generated PDF to the file system
            File.WriteAllBytes(filePath, ms.ToArray());
        }

        // Return the byte array of the PDF content
        return File.ReadAllBytes(filePath); // Return the byte array to embed in the frontend
    }

    // Simulate method to get student data (replace with your actual DB code)
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
        return (null, null, null, null, null);
    }
}










