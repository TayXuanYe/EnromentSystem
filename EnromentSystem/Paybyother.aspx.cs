using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Paybyother : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
            if (!IsPostBack)
            {
                // Set the default date to today's date
                txtTransactionDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                PopulateCurrencyDropdown();
            }
    }
    private void PopulateCurrencyDropdown()
    {
        // Define a list of currencies with their codes and names
        var currencies = new List<ListItem>
    {
        new ListItem("United States Dollar (USD)", "USD"),
        new ListItem("Euro (EUR)", "EUR"),
        new ListItem("Malaysian Ringgit (MYR)", "MYR"),
        new ListItem("British Pound Sterling (GBP)", "GBP"),
        new ListItem("Japanese Yen (JPY)", "JPY"),
        new ListItem("Australian Dollar (AUD)", "AUD"),
        new ListItem("Canadian Dollar (CAD)", "CAD"),
        new ListItem("Swiss Franc (CHF)", "CHF"),
        new ListItem("Chinese Yuan Renminbi (CNY)", "CNY"),
        new ListItem("Indian Rupee (INR)", "INR")
    };

        // Bind the list to the dropdown control
        ddlCurrency.Items.Clear(); // Clear any existing items
        ddlCurrency.Items.Add(new ListItem("Select a currency", "")); // Add a default placeholder
        ddlCurrency.Items.AddRange(currencies.ToArray());
    }
    protected void imgCalendar_Click(object sender, EventArgs e)
        {
            // Toggle calendar visibility
            Calendar1.Visible = !Calendar1.Visible;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            // Update the textbox with the selected date
            txtTransactionDate.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd");

            // Hide the calendar after selecting a date
            Calendar1.Visible = false;
        }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        string studentId = Session["sid"] as string;
        DateTime transactionDate = DateTime.Now.Date;
        string currency = ddlCurrency.SelectedValue; 
        decimal amountPaid = Convert.ToDecimal(txtAmountPaid.Text); 
        string referenceNo = txtReferenceNo.Text; 
        string remarks = txtRemarks.Text; 
        string uploadSlipPath = UploadPaymentSlip(); 
        string contactNo = txtContactNo.Text; 
        string paymentStatus = "Pending"; 
        string paymentMethod = "Administrative Collection";
       
        SavePaymentUpload(studentId, transactionDate, currency, amountPaid, referenceNo, remarks, uploadSlipPath, contactNo, paymentStatus, paymentMethod);
    }

    private string UploadPaymentSlip()
    {
        
        if (FileUpload1.HasFile)
        {
            string filePath = "~/Uploads/" + FileUpload1.FileName;
            FileUpload1.SaveAs(Server.MapPath(filePath));
            return filePath;
        }
        return string.Empty; 
    }

    private void SavePaymentUpload(string studentId, DateTime transactionDate, string currency, decimal amountPaid,
                                    string referenceNo, string remarks, string uploadSlipPath, string contactNo, string paymentStatus, string paymentMethod)
    {
        string query = @"
                     INSERT INTO PaymentUpload (sid, TransactionDate, Currency, AmountPaid, ReferenceNo, Remarks, UploadSlip, ContactNo, PaymentStatus, PaymentMethod) 
                     VALUES (@sid, @TransactionDate, @Currency, @AmountPaid, @ReferenceNo, @Remarks, @UploadSlip, @ContactNo, @PaymentStatus, @PaymentMethod)";

        try
        {
            using (SqlConnection conn = DatabaseManager.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@sid", studentId);
                cmd.Parameters.AddWithValue("@TransactionDate", transactionDate);
                cmd.Parameters.AddWithValue("@Currency", currency);
                cmd.Parameters.AddWithValue("@AmountPaid", amountPaid);
                cmd.Parameters.AddWithValue("@ReferenceNo", referenceNo);
                cmd.Parameters.AddWithValue("@Remarks", remarks);
                cmd.Parameters.AddWithValue("@UploadSlip", uploadSlipPath);
                cmd.Parameters.AddWithValue("@ContactNo", contactNo);
                cmd.Parameters.AddWithValue("@PaymentStatus", paymentStatus);
                cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                conn.Open();
                cmd.ExecuteNonQuery();  // Execute the query
            }


            // Show success message after saving to the database
            Label1.Text = "Payment successfully uploaded!";
        }
        catch (Exception ex)
        {
            // Handle any errors
            Label1.Text = "Error: " + ex.Message;
        }
    }
}

    

    



