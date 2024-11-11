using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

public partial class AdminModifyLecturerPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["lid"] != null)
            {
                string id = Request.QueryString["lid"];
                SetLecturerInfo(id);
            }
            else
            {
                Response.Redirect("AdminMaintainLecturerMainPage.aspx");
            }
        }
    }

    private void SetLecturerInfo(string id)
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
                txtName.Text = row["name"].ToString();
            }
            lblId.Text = id;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        confirmationWindow.Style["display"] = "none";
        if (Page.IsValid)
        {
            string id = Request.QueryString["lid"];
            successfulWindow.Style["display"] = "flex";
            bool success = DatabaseManager.UpdateData(
                "lecture",
                new List<string> { "name" },
                new List<object> { txtName.Text },
                $@"WHERE lid = '{id}'"
                );
            if (success)
            {
                successfulWindow.Style["display"] = "flex";
            }
        }
    }

    protected void btnCancelUpdate_Click(object sender, EventArgs e)
    {
        confirmationWindow.Style["display"] = "none";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMaintainLecturerMainPage.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        confirmationWindow.Style["display"] = "flex";
        SetComfirmTable(lblId.Text);
    }

    private void SetComfirmTable(string id)
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
                lblCurrenName.Text = row["name"].ToString();
            }
            lblUpdateName.Text = txtName.Text;
        }
    }
}