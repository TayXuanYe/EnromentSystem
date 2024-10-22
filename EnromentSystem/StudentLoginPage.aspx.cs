using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentLoginPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void cvdLoginFall_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = checkPasswordMatchId();
    }

    private bool checkPasswordMatchId()
    {
        string condition = "WHERE sid = \'" + txtUserId.Text + "\'";
        DataSet dataSet = DatabaseManager.getRecord("student", new List<string> { "password" }, condition);
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
}