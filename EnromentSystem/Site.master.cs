using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI;

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
            Debug.WriteLine("return");
            Response.Redirect("StudentLoginPage.aspx");
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["sid"] = null;
        Response.Redirect("StudentLoginPage.aspx");
    }
}