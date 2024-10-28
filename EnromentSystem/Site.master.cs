using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Timeout = 30;
        if (Session["sid"] != null)
        {
            DataSet dataSet = DatabaseManager.GetRecord(
                "student",
                new List<string> { "name", "program" },
                "WHERE sid = \'" + Session["sid"] + "\'"
            );

            DataTable dt = dataSet.Tables[0];
            string name = null;
            string program = null;
            foreach (DataRow row in dt.Rows)
            {
                name = row["name"].ToString();
                program = row["program"].ToString();
            }

            lblStudentDetails.Text =
                name + "<br>" +
                Session["sid"].ToString() + "<br>" +
                program;
        }else
        {
            //Response.Redirect("StudentLoginPage.aspx");
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["sid"] = null;
        Response.Redirect("StudentLoginPage.aspx");
    }
}