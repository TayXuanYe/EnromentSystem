using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminMaintainProgramAndMajorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["aid"] != null)
        {
            if (!IsPostBack)
            {
                SetProgramInfoGridView();
            }
        }
        else
        {
            Response.Redirect("AdminLoginPage.aspx");
        }
    }

    //serach bar
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SetProgramInfoGridView();
    }

    //gridview -- program
    protected void gvProgramInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string program = e.CommandArgument.ToString();
            Response.Redirect($"AdminModifyProgramAndMajorPage.aspx?program={program}");
        }
        else if (e.CommandName == "Delete")
        {
            string program = e.CommandArgument.ToString();
            Response.Redirect($"AdminDeleteProgramPage.aspx?program={program}");
        }
    }

    private void SetProgramInfoGridView()
    {
        string condition = null;
        string value = txtSearchInput.Text;
        DataSet dataSet = null;
        if (value != "")
        {
            switch (ddlSearch.SelectedValue)
            {
                case "all":
                    condition = "WHERE program like \'%" + value + "%\' "
                        + "school like \'%" + value + "%\' "
                        + "level like \'%" + value + "%\' ";
                    break;

                case "program":
                    condition = "WHERE program like \'%" + value + "%\'";
                    break;

                case "school":
                    condition = "WHERE school like \'%" + value + "%\'";
                    break;

                case "level":
                    condition = "WHERE level like \'%" + value + "%\'";
                    break;
            }

            dataSet = DatabaseManager.GetRecord(
                "program",
                new List<string> { "program", "school", "level" },
                condition
            );
        }
        else
        {
            dataSet = DatabaseManager.GetRecord(
                "program",
                new List<string> { "program", "school", "level" }
            );
        }
        if (dataSet != null)
        {
            DataTable table = dataSet.Tables[0];
            gvProgramInfo.DataSource = table;
            gvProgramInfo.DataBind();
        }
    }

    protected void btnAddProgram_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminAddProgramAndMajorPage.aspx");
    }

    protected void gvProgramInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string program = gvProgramInfo.SelectedRow.Cells[0].Text;
        SetMajorInfoGridView(program);
    }

    //gridview -- major
    private void SetMajorInfoGridView(string program)
    {
        DataSet dataSet = null;
        dataSet = DatabaseManager.GetRecord(
            "major",
            new List<string> { "major" },
            $@"WHERE program = '{program}'"
        );
        if (dataSet != null)
        {
            DataTable table = dataSet.Tables[0];
            gvMajorInfo.DataSource = table;
            gvMajorInfo.DataBind();
        }
    }
}