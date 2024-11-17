using iText.Html2pdf;
using System;
using System.IO;

public partial class StudentClassTimetablePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sid"] != null)
        {
            GenerateTimeTable("", "test");
            DisplayPdf("test");
        }
        else
        {
            Response.Redirect("StudentLoginPage.aspx");
        }
    }

    private void GenerateTimeTable(string studentId, string fileName)
    {
        string htmlContent = @"
            <html>
                <head>
                    <style>
                        body { font-family: Arial, sans-serif; }
                        h1 { color: blue; }
                        p { font-size: 14px; }
                    </style>
                </head>
                <body>
                    <h1>Hello, PDF!</h1>
                    <p>This is a sample PDF generated from HTML using iText7.</p>
                </body>
            </html>";

        // 定义 PDF 输出路径
        string pdfPath = Server.MapPath($"~/StudentTimeTable/{fileName}.pdf");

        // 使用 iText7 将 HTML 转换为 PDF
        using (FileStream pdfStream = new FileStream(pdfPath, FileMode.Create))
        {
            HtmlConverter.ConvertToPdf(htmlContent, pdfStream);
        }

        Console.WriteLine("PDF has been generated at: " + pdfPath);
    }

    private void DisplayPdf(string fileName)
    {
        string pdfUrl = ResolveUrl($"~/StudentTimeTable/{fileName}.pdf");
        pdfFrame.Attributes["src"] = pdfUrl;
    }
}