using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Maybank_Portal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string studentId = Session["sid"] as string;

            if (!string.IsNullOrEmpty(studentId))
            {
                PopulatePanel(studentId);
            }
            else
            {
                Response.Redirect("StudentLoginPage.aspx");
            }


            string orderReference = GenerateOrderReference();
            orderreference.Text = orderReference;


            if (Session["NetAmount"] != null)
            {
                decimal passedAmount = Convert.ToDecimal(Session["NetAmount"]);
                amount.Text = passedAmount.ToString("0.00");
            }
            else
            {
                amount.Text = "Amount not found.";
            }

            if (Session["PhoneNumber"] != null)
            {
                string phoneNumber = Session["PhoneNumber"].ToString();
                paymentdescription.Text = phoneNumber;
            }
            else
            {
                paymentdescription.Text = "Phone number not found.";
            }

            PopulateExpirationDateDropdowns();
        }
    }
    private void PopulateExpirationDateDropdowns()
    {

        for (int month = 1; month <= 12; month++)
        {
            ddlExpirationMonth.Items.Add(new ListItem(month.ToString("D2"), month.ToString()));
        }


        int currentYear = DateTime.Now.Year;
        for (int year = currentYear; year <= currentYear + 10; year++)
        {
            ddlExpirationYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
        }
    }

    private string GenerateOrderReference()
    {
        string orderReference = string.Empty;
        string query = "SELECT TOP 1 OrderReference FROM MaybankPay ORDER BY OrderReference DESC";

        try
        {
            using (SqlConnection conn = DatabaseManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    int lastOrderReference = 0;

                    if (result != DBNull.Value && result != null)
                    {
                        lastOrderReference = Convert.ToInt32(result.ToString().Substring(3)); 
                    }

                    if (lastOrderReference == 0) lastOrderReference = 1000;
                    lastOrderReference++;
                    orderReference = "000" + lastOrderReference.ToString("D4");
                }
            }
        }
        catch (Exception ex)
        {
            orderReference = "Error generating order reference: " + ex.Message;
        }

        return orderReference;
    }

    private void PopulatePanel(string studentId)
    {
        string query = "SELECT name as Name FROM student WHERE sid = @studentID";

        try
        {
            using (SqlConnection conn = DatabaseManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@studentID", studentId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            studentname.Text = reader["Name"]?.ToString() ?? "N/A";
                        }
                        else
                        {
                            studentname.Text = "Student not found";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            studentname.Text = "Error: " + ex.Message;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string orderReference = string.Empty;


        if (orderreference.Text.Length >= 15)
        {
            orderReference = orderreference.Text.Substring(15);
        }
        else
        {

            orderReference = orderreference.Text;


            string studentId = Session["sid"] as string;
            string customerName = studentname.Text;
            string paymentDescription = paymentdescription.Text; 
            decimal totalAmount = Convert.ToDecimal(Session["NetAmount"]);
            string cardNumber = cardnumber.Text;
            string cardHolderName = nameoncard.Text;
            int expirationMonth = Convert.ToInt32(ddlExpirationMonth.SelectedValue);
            int expirationYear = Convert.ToInt32(ddlExpirationYear.SelectedValue);
            string cvv = cvc.Text;
            DateTime transactionDate = DateTime.Now.Date;
            string paymentMethod = "Online"; 
            string status = "Approved"; 

            // Save transaction details to the database
            SavePaymentTransaction(orderReference, studentId, customerName, paymentDescription, totalAmount, cardNumber,
                                   cardHolderName, expirationMonth, expirationYear, cvv, transactionDate, paymentMethod, status);
        }
    }
        private void SavePaymentTransaction(string orderReference, string studentId, string customerName, string paymentDescription,
                                             decimal totalAmount, string cardNumber, string cardHolderName, int expirationMonth,
                                             int expirationYear, string cvv, DateTime transactionDate, string paymentMethod, string status)
        {
            string insertQuery = @"
        INSERT INTO MaybankPay (OrderReference, sid, CustomerName, PaymentDescription, TotalAmount, CardNumber, 
                                CardHolderName, ExpirationMonth, ExpirationYear, CVV, TransactionDate, PaymentMethod, Status) 
        VALUES (@OrderReference, @sid, @CustomerName, @PaymentDescription, @TotalAmount, @CardNumber, 
                @CardHolderName, @ExpirationMonth, @ExpirationYear, @CVV, @TransactionDate, @PaymentMethod, @Status)";

            try
            {
                using (SqlConnection conn = DatabaseManager.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@OrderReference", orderReference);
                        cmd.Parameters.AddWithValue("@sid", studentId);
                        cmd.Parameters.AddWithValue("@CustomerName", customerName);
                        cmd.Parameters.AddWithValue("@PaymentDescription", paymentDescription);
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        cmd.Parameters.AddWithValue("@CardNumber", cardNumber);
                        cmd.Parameters.AddWithValue("@CardHolderName", cardHolderName);
                        cmd.Parameters.AddWithValue("@ExpirationMonth", expirationMonth);
                        cmd.Parameters.AddWithValue("@ExpirationYear", expirationYear);
                        cmd.Parameters.AddWithValue("@CVV", cvv);
                        cmd.Parameters.AddWithValue("@TransactionDate", transactionDate);
                        cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                        cmd.Parameters.AddWithValue("@Status", status);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                studentname.Text = "Error saving transaction: " + ex.Message;
            }
        }
}

