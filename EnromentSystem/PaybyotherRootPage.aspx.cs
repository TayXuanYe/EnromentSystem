using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaybyotherRootPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!CheckBoxAgreeTerms.Checked)
        {
            Response.Write("<script>alert('You must agree to the terms and conditions before proceeding.');</script>");
            return; 
        }

        // If the checkbox is checked, proceed with the logic (e.g., redirecting to the next page)
        Response.Redirect("Paybyother.aspx"); // Replace "NextPage.aspx" with your actual next page
    }
}
