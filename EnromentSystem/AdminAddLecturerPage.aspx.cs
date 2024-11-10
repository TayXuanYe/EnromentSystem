using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminAddLecturerPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetSuggestLecturerId();
        }
    }

    private void SetSuggestLecturerId()
    {
        for (int i = 0; i <= 9999; i++)
        {
            string id = "L" + DateTime.Now.Year + i.ToString("D4");
            id.ToUpper();
            if (!CheckLecturerIdIsExist(id))
            {
                txtId.Text = id;
                CheckLecturerIdIsExist(id);
                break;
            }
        }
    }

    private bool CheckLecturerIdIsExist(string id)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "lecture",
            new List<string> { "lid" },
            "WHERE lid = \'" + id + "\'"
            );
        if (dataSet != null)
        {
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    protected void CheckIdIsExist_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !CheckLecturerIdIsExist(txtId.Text);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            bool success = DatabaseManager.InsertData(
                "lecture",
                new List<string> { "name", "lid", "password" },
                new List<object> { txtName.Text, txtId.Text, "iu" + txtId.Text }
                );

            if (success)
            {
                successfulWindow.Style["display"] = "flex";
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMaintainLecturerMainPage.aspx");
    }
}