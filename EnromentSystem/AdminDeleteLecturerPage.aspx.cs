using System;
using System.Collections.Generic;
using System.Data;

public partial class AdminDeleteLecturerPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["lid"] != null)
        {
            string id = Request.QueryString["lid"];
            SetLectureDetails(id);
        }
        else
        {
            Response.Redirect("AdminMaintainLecturerMainPage.aspx");
        }
    }

    private void SetLectureDetails(string id)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "lecture",
            new List<string> { "name" },
            $@"WHERE lid = '{id}'"
            );

        if (dataSet != null)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                lblName.Text = row["name"].ToString();
            }
            lblId.Text = id;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        bool succesful = DatabaseManager.DeleteData(
            "lecture",
            $@"WHERE lid = '{lblId.Text}'"
            );
        if (succesful)
        {
            successfulWindow.Style["display"] = "flex";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMaintainStudentMainPage.aspx");
    }
}