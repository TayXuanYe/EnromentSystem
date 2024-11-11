using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminHomePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblUserName.Text = "TAY XUAN YE";
        if (Session["aid"] != null)
        {
            lblUserName.Text = "TAY XUAN YE";
        }
    }
}