using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
        Random random = new Random();
        string orderReference = "M"+random.Next(1000000000);

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
            orderReference = orderreference.Text;

          
            string studentId = Session["sid"] as string; 
            string paymentDescription = paymentdescription.Text;  
            decimal totalAmount = Convert.ToDecimal(Session["NetAmount"]);  

            SavePaymentTransaction(orderReference, studentId, paymentDescription, totalAmount);
        
    }
    private void SavePaymentTransaction(string orderReference, string studentId, string paymentDescription, decimal totalAmount)
    {
        string insertQuery = @"
        INSERT INTO payment (sid, process, particulars, documentNo, session, amount) 
        VALUES (@sid, @process, @particulars, @documentNo, @session, @amount)";
        try
        {
            using (SqlConnection conn = DatabaseManager.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    // Directly assign the value "AUG2024" to session
                    string session = "AUG2024";

                    // Add parameters for the query
                    cmd.Parameters.AddWithValue("@sid", studentId);
                    cmd.Parameters.AddWithValue("@process", "Online");
                    cmd.Parameters.AddWithValue("@particulars", paymentDescription);
                    cmd.Parameters.AddWithValue("@documentNo", orderReference);
                    cmd.Parameters.AddWithValue("@session", session);  // Insert "AUG2024" as session value
                    cmd.Parameters.AddWithValue("@amount", totalAmount);

                    // Open connection and execute the command
                    conn.Open();
                    
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        // Success message with redirect
                        string successScript = "alert('Transaction Successfull!');";
                        ClientScript.RegisterStartupScript(this.GetType(), "SaveSuccess", successScript, true);
                        Response.Redirect("PaymentPage.aspx");
                    }
                    else
                    {
                        // Error message with redirect
                        string errorScript = "alert('Error updating bank details.');";
                        ClientScript.RegisterStartupScript(this.GetType(), "SaveError", errorScript, true);
                    }
                }

                
            }
        }
        catch (Exception ex)
        {
            studentname.Text = "Error saving transaction: " + ex.Message;
        }
    }


    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentPage.aspx");
    }
}

