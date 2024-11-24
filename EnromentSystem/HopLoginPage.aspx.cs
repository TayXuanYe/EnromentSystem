using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HopLoginPage : System.Web.UI.Page
{
    protected void LoginFall(object source, ServerValidateEventArgs args)
    {
        args.IsValid = checkPasswordMatchId();
    }

    private bool checkPasswordMatchId()
    {
        string condition = "WHERE hid = \'" + HOPID.Text.ToUpper() + "\'";
        DataSet dataSet = DatabaseManager.GetRecord("hop", new List<string> { "password" }, condition);
        DataTable dt = dataSet.Tables[0];
        string password = null;
        foreach (DataRow row in dt.Rows)
        {
            password = row["password"].ToString();
        }

        if (password == HOPPassword.Text)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Session["hid"] = HOPID.Text.ToUpper();
            Session.Timeout = 30;
            Response.Redirect("HOPReviewHomePage.aspx");
        }
    }
}