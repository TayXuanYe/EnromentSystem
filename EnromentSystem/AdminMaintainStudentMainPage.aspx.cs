using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminMaintainStudentMainPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["aid"] != null)
        {
            if (!IsPostBack)
            {
                SetStudentInfoGridView();
            }
        }
        else
        {
            Response.Redirect("AdminLoginPage.aspx");
        }
    }

    private void SetStudentInfoGridView()
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
                        + "sid like \'%" + value + "%\' "
                        + "program like \'%" + value + "%\' "
                        + "major like \'%" + value + "%\' ";
                    break;

                case "name":
                    condition = "WHERE name like \'%" + value + "%\'";
                    break;

                case "sid":
                    condition = "WHERE sid like \'%" + value + "%\'";
                    break;

                case "program":
                    condition = "WHERE program like \'%" + value + "%\'";
                    break;

                case "major":
                    condition = "WHERE major like \'%" + value + "%\'";
                    break;
            }

            dataSet = DatabaseManager.GetRecord(
                "student",
                new List<string> { "name", "sid", "program", "major" },
                condition
            );
        }
        else
        {
            dataSet = DatabaseManager.GetRecord(
                "student",
                new List<string> { "name", "sid", "program", "major" }
            );
        }

        if(dataSet != null)
        {
            DataTable table = dataSet.Tables[0];
            gvStudentInfo.DataSource = table;
            gvStudentInfo.DataBind();
        }
    }

    protected void gvStudentInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string studentId = e.CommandArgument.ToString();
            Response.Redirect($"AdminModifyStudentPage.aspx?sid={studentId}");
        }
        else if (e.CommandName == "Delete")
        {
            string studentId = e.CommandArgument.ToString();
            Response.Redirect($"AdminDeleteStudentPage.aspx?sid={studentId}");
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SetStudentInfoGridView();
    }

    protected void btnAddStudent_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminAddStudentPage.aspx");
    }
}