using iText.Kernel.Pdf;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iText.Layout;


public partial class Generatepdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sid = Request.QueryString["sid"];
        string paymentId = Request.QueryString["paymentId"];

        if (string.IsNullOrEmpty(sid) || string.IsNullOrEmpty(paymentId))
        {
            Response.Write("Missing parameters: SID or PaymentID not provided.");
            return;
        }

        // Fetch payment details using the paymentId
        DataTable paymentDetails = GetPaymentDetails(paymentId);

        if (paymentDetails.Rows.Count > 0)
        {
            GeneratePDF(paymentDetails);
        }
        else
        {
            Response.Write("No payment details found for the provided PaymentID.");
        }
    }

    private void GeneratePDF(DataTable paymentDetails)
    {
        var row = paymentDetails.Rows[0];

        // Define the file path without using ReferenceNumber
        string pdfPath = Server.MapPath("~/Receipts/") + row["PaymentId"] + ".pdf";  // Using PaymentId for filename

        // Create PDF
        using (FileStream stream = new FileStream(pdfPath, FileMode.Create))
        {
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Add content without Reference Number
            document.Add(new Paragraph("Payment Receipt"));
            document.Add(new Paragraph($"Payment Type: {row["PaymentType"]}"));
            document.Add(new Paragraph($"Transaction Date: {row["TransactionDate"]}"));
            document.Add(new Paragraph($"Amount: {row["Amount"]}"));
            document.Add(new Paragraph($"Status: {row["PaymentStatus"]}"));

            document.Close();
        }

        // Serve the file for download
        Response.ContentType = "application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + row["PaymentId"] + ".pdf");
        Response.TransmitFile(pdfPath);
        Response.End();
    }
    private DataTable GetPaymentDetails(string paymentId)
    {
        DataTable dt = new DataTable();

        using (SqlConnection conn = DatabaseManager.GetConnection())
        {
            string query = @"
            SELECT 
                PaymentMethod,
                TransactionDate,
                AmountPaid AS Amount,
                PaymentStatus,
                sid
            FROM PaymentUpload
            WHERE PaymentUploadID = @paymentId

            UNION ALL

            SELECT 
                PaymentType,
                TransactionDate,
                TotalAmount AS Amount,
                Status AS PaymentStatus,
                sid
            FROM MaybankPay
            WHERE PaymentID = @paymentId";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@paymentId", paymentId);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
        }

        return dt;
    }

}