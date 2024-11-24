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
                Response.Redirect("StudentLoginPage.aspx");
                return;
            }

            if (Session["NetAmount"] != null)
            {
                decimal netAmount = Convert.ToDecimal(Session["NetAmount"]);

                HiddenNetAmount.Value = netAmount.ToString("0.00"); 
                TextBoxAmount.Text = netAmount.ToString("0.00");    
            }
            else
            {
                TextBoxAmount.Text = "Net Amount not found."; 
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!CheckBoxAgreeTerms.Checked)
        {
            TextBoxAmount.Text = "You must agree to the terms and conditions before proceeding.";
            return; 
        }

    
        string userAmountText = TextBoxAmount.Text.Trim();
        userAmountText = userAmountText.Replace("$", "").Replace(",", ""); 
        decimal userAmount;
        bool isDecimal = Decimal.TryParse(userAmountText, out userAmount);

       
        decimal passingAmount = Convert.ToDecimal(HiddenNetAmount.Value);

        
        string phoneNumber = TextBoxPhone.Text.Trim();

        if (isDecimal)
        {
            if (userAmount <= passingAmount)
            {
                
                Session["NetAmount"] = userAmount;

                
                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    Session["PhoneNumber"] = phoneNumber;
                }
                else
                {
                    Session["PhoneNumber"] = "Phone number not provided.";
                }

                
                Response.Redirect("Maybank Portal.aspx");
            }
            else
            {
                TextBoxAmount.Text = "Amount cannot exceed the passing value.";
            }
        }
        else
        {
            TextBoxAmount.Text = "Please enter a valid amount.";
        }
    }

    protected void Cancelbutton_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentPage.aspx");
    }
}