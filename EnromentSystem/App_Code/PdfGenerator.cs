using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;


/// <summary>
/// Summary description for PdfGenerator
/// </summary>
public class PdfGenerator : System.Web.UI.Page
{
    public PdfGenerator()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void GenerateLandscapePdf(string htmlContent, string fileName, string folderPath)
    {
        string tempHtmlFile = Path.Combine(Server.MapPath($"~/{folderPath}"), "temp.html");
        File.WriteAllText(tempHtmlFile, htmlContent);

        string wkHtmlToPdfPath = @"C:\Program Files\wkhtmltopdf\bin\wkhtmltopdf.exe";  // 修改为实际路径

        string pdfOutputPath = Path.Combine(Server.MapPath($"~/{folderPath}"), fileName);

        Process process = new Process();
        process.StartInfo.FileName = wkHtmlToPdfPath;
        process.StartInfo.Arguments = $"--orientation Landscape \"{tempHtmlFile}\" \"{pdfOutputPath}\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        process.WaitForExit();

        File.Delete(tempHtmlFile);
    }
    
    public void GenerateProtraitPdf(string htmlContent, string fileName, string folderPath)
    {
        string tempHtmlFile = Path.Combine(Server.MapPath($"~/{folderPath}"), "temp.html");
        File.WriteAllText(tempHtmlFile, htmlContent);

        string wkHtmlToPdfPath = @"C:\Program Files\wkhtmltopdf\bin\wkhtmltopdf.exe";  // 修改为实际路径

        string pdfOutputPath = Path.Combine(Server.MapPath($"~/{folderPath}"), fileName);

        Process process = new Process();
        process.StartInfo.FileName = wkHtmlToPdfPath;
        process.StartInfo.Arguments = $"\"{tempHtmlFile}\" \"{pdfOutputPath}\"";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        process.WaitForExit();

        File.Delete(tempHtmlFile);
    }
}