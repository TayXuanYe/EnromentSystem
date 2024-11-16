using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI;

public partial class LecturerSiteMaster : MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Timeout = 30;
        if (Session["lid"] != null)
        {
            DataSet dataSet = DatabaseManager.GetRecord(
                "lecture",
                new List<string> { "name" },
                $@"WHERE lid = '{Session["lid"].ToString()}'"
            );

            DataTable dt = dataSet.Tables[0];
            string name = null;
            foreach (DataRow row in dt.Rows)
            {
                name = row["name"].ToString();
            }

            lblLecturerDetails.Text = $@"{name}<br>{Session["lid"].ToString()}";
        }else
        {
            Response.Redirect("LecturerLoginPage.aspx");
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["sid"] = null;
        Response.Redirect("LecturerLoginPage.aspx");
    }
}