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

            ddlCurrency.Items.Clear();
            ddlCurrency.Items.Add(new ListItem("Select a currency", ""));
            ddlCurrency.Items.AddRange(currencies.ToArray());
        }

    
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string studentId = Session["sid"] as string;
            DateTime transactionDate = DateTime.Now.Date;
            string currency = ddlCurrency.SelectedValue;
            decimal amountPaid = Convert.ToDecimal(txtAmountPaid.Text);
            string documentNo = txtReferenceNo.Text;
            string particulars = txtRemarks.Text;

            // Set session and process as per the new requirements
            string session = "AUG2024";
            string process = "Payment";

            // Save transaction to the database
            SavePaymentTransaction(studentId, transactionDate, process, particulars, documentNo, session, amountPaid);
        }

        private void SavePaymentTransaction(string studentId, DateTime transactionDate, string process, string particulars,
                                            string documentNo, string session, decimal amountPaid)
        {
            string query = @"
            INSERT INTO payment (sid, date, process, particulars, documentNo, session, amount)
            VALUES (@sid, @date, @process, @particulars, @documentNo, @session, @amount)";

            try
            {
                using (SqlConnection conn = DatabaseManager.GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@sid", studentId);
                    cmd.Parameters.AddWithValue("@date", transactionDate);
                    cmd.Parameters.AddWithValue("@process", process);
                    cmd.Parameters.AddWithValue("@particulars", particulars);
                    cmd.Parameters.AddWithValue("@documentNo", documentNo);
                    cmd.Parameters.AddWithValue("@session", session);
                    cmd.Parameters.AddWithValue("@amount", amountPaid);

                    conn.Open();
                    cmd.ExecuteNonQuery();  // Execute the query
                }

                // Show success message after saving to the database
                Label1.Text = "Payment successfully processed!";
            }
            catch (Exception ex)
            {
                // Handle any errors
                Label1.Text = "Error: " + ex.Message;
            }
        }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentPage.aspx");
    }
}

