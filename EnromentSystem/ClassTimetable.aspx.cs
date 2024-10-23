using System;
using System.IO;
using System.Web.UI;

public partial class ClassTimetable : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GenerateAndDisplayPDF();
        }
    }

    private void GenerateAndDisplayPDF()
    {
        // Sample data to generate PDF table
        string[][] tableData = new string[][] {
            new string[] { "Day", "Time", "Subject" },
            new string[] { "Monday", "10:00 AM - 11:00 AM", "Math" },
            new string[] { "Tuesday", "11:00 AM - 12:00 PM", "Science" },

        };

        // Set the PDF file path
        string pdfFileName = "ClassTimetable.pdf";
        string pdfFilePath = Server.MapPath("~/PDFs/" + pdfFileName);

        // Ensure the PDFs folder exists
        string pdfDirectory = Server.MapPath("~/PDFs/");
        if (!Directory.Exists(pdfDirectory))
        {
            Directory.CreateDirectory(pdfDirectory);
        }

        // Instantiate the PdfTableGenerator class and generate the PDF
        PdfTableGenerator pdfGenerator = new PdfTableGenerator();
        try
        {
            pdfGenerator.generatePDF(tableData, pdfFilePath); // Make sure this matches your method
        }
        catch (Exception ex)
        {
            // Handle any errors
            Response.Write("Error generating PDF: " + ex.Message);
        }

        // Embed the PDF into the web page for preview
        DisplayPDF(pdfFileName);
    }

    private void DisplayPDF(string fileName)
    {
        // Embed the PDF file in the Literal control for displaying
        LiteralPdfPreview.Text = $"<iframe src='PDFs/{fileName}' width='100%' height='600px'></iframe>";
    }
}


