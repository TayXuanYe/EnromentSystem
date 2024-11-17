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
            DisplayPdf("timetable");
        }
        else
        {
            Response.Redirect("StudentLoginPage.aspx");
        }
    }

    private void GenerateTimeTable(string studentId, string fileName)
    {
        
    }

    private void DisplayPdf(string fileName)
    {
        string pdfUrl = ResolveUrl($"~/StudentTimeTable/{fileName}.pdf");
        pdfFrame.Attributes["src"] = pdfUrl;
    }
}