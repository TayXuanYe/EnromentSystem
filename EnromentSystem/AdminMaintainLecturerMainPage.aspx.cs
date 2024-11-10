using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminMaintainLecturerMainPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["aid"] != null)
        {
            if (!IsPostBack)
            {
                SetLecturerInfoGridView();
            }
        }
        else
        {
            Response.Redirect("AdminLoginPage.aspx");
        }
    }

    private void SetLecturerInfoGridView()
    {
        string condition = null;
        string value = txtSearchInput.Text;
        DataSet dataSet = null;
        if (value != "")
        {
            switch (ddlSearch.SelectedValue)
            {
                case "all":
                    condition = "WHERE name like \'%" + value + "%\' "
                        + "lid like \'%" + value + "%\' "
                        + "program like \'%" + value + "%\' "
                        + "major like \'%" + value + "%\' ";
                    break;

                case "name":
                    condition = "WHERE name like \'%" + value + "%\'";
                    break;

                case "lid":
                    condition = "WHERE lid like \'%" + value + "%\'";
                    break;
            }

            dataSet = DatabaseManager.GetRecord(
                "lecture",
                new List<string> { "name", "lid"},
                condition
            );
        }
        else
        {
            dataSet = DatabaseManager.GetRecord(
                "lecture",
                new List<string> { "name", "lid"}
            );
        }
        if (dataSet != null)
        {
            DataTable table = dataSet.Tables[0];
            gvLecturerInfo.DataSource = table;
            gvLecturerInfo.DataBind();
        }
    }

    protected void btnAddLecturer_Click(object sender, EventArgs e)
    {
        Response.Redirect($"AdminAddLecturerPage.aspx");
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SetLecturerInfoGridView();
    }

    protected void gvLecturerInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string studentId = e.CommandArgument.ToString();
            Response.Redirect($"AdminModifyLecturerPage.aspx?lid={studentId}");
        }
        else if (e.CommandName == "Delete")
        {
            string studentId = e.CommandArgument.ToString();
            Response.Redirect($"AdminDeleteLecturerPage.aspx?lid={studentId}");
        }
    }
}