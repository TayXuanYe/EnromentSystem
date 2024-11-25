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

       
        
        string phoneNumber = TextBoxPhone.Text.Trim();
        Session["NetAmount"] = userAmountText;
        Response.Redirect("Maybank Portal.aspx");
    }

    protected void Cancelbutton_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentPage.aspx");
    }
}