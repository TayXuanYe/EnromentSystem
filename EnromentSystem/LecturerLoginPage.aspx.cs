using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LecturerLoginPage : System.Web.UI.Page
{
    protected void cvdLoginFall_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = checkPasswordMatchId();
    }

    private bool checkPasswordMatchId()
    {
        string condition = "WHERE lid = \'" + txtUserId.Text.ToUpper() + "\'";
        DataSet dataSet = DatabaseManager.GetRecord("lecture", new List<string> { "password" }, condition);
        DataTable dt = dataSet.Tables[0];
        string password = null;
        foreach (DataRow row in dt.Rows)
        {
            password = row["password"].ToString();
        }
        if (password == txtPassword.Text)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Session["lid"] = txtUserId.Text.ToUpper();
            Session.Timeout = 30;
            Response.Redirect("LecturerHomePage.aspx");
        }
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("MainLoginPage.aspx");
    }
}