using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaymentPaybyflywire : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string studentId = Session["sid"] as string;

            if (string.IsNullOrEmpty(studentId))
            {
                // Redirect if student ID is not found in the session
                Response.Redirect("StudentLoginPage.aspx");
                return;
            }

            // Check if the NetAmount is present in the session
            if (Session["NetAmount"] != null)
            {
                decimal netAmount = Convert.ToDecimal(Session["NetAmount"]);

                // Store the NetAmount in a hidden field and display in the TextBox
                HiddenNetAmount.Value = netAmount.ToString("0.00"); // Store net amount in hidden field
                TextBoxAmount.Text = netAmount.ToString("0.00");    // Display the net amount in the TextBox
            }
            else
            {
                TextBoxAmount.Text = "Net Amount not found."; // Handle case when net amount is not in session
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!CheckBoxAgreeTerms.Checked)
        {
            // If not checked, display an error message
            TextBoxAmount.Text = "You must agree to the terms and conditions before proceeding.";
            return; // Exit the function
        }

        // Get the amount entered by the user
        string userAmountText = TextBoxAmount.Text.Trim();
        userAmountText = userAmountText.Replace("$", "").Replace(",", ""); // Remove unwanted characters

        decimal userAmount;
        bool isDecimal = Decimal.TryParse(userAmountText, out userAmount);

        // Get the value of NetAmount from HiddenNetAmount
        decimal passingAmount = Convert.ToDecimal(HiddenNetAmount.Value);

        // Get the phone number entered by the user
        string phoneNumber = TextBoxPhone.Text.Trim();

        if (isDecimal)
        {
            if (userAmount <= passingAmount)
            {
                // Store the NetAmount in session
                Session["NetAmount"] = userAmount;

                // Store the Phone Number in session
                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    Session["PhoneNumber"] = phoneNumber;
                }
                else
                {
                    Session["PhoneNumber"] = "Phone number not provided.";
                }

                // Redirect to the Maybank Portal page
                Response.Redirect("Maybank Portal.aspx");
            }
            else
            {
                // Invalid: Display error message
                TextBoxAmount.Text = "Amount cannot exceed the passing value.";
            }
        }
        else
        {
            // Invalid decimal format
            TextBoxAmount.Text = "Please enter a valid amount.";
        }
    }
}