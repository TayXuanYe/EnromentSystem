using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminAddProgramAndMajorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateSchool();
        }
        gvMajorInfo.DataSource = majorInfo;
        gvMajorInfo.DataBind();
    }

    //program details part
    private void PopulateSchool()
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "school",
            new List<string> { "school" }
            );
        if (dataSet != null)
        {
            List<string> value = new List<string>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                value.Add(row["school"].ToString());
            }
            UIComponentGenerator.PopulateDropDownList(ddlSchool, value, value);
        }
    }

    private bool CheckProgramNameIsExist(string name)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "program",
            new List<string> { "program" },
            $@"WHERE program = '{name}'"
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

    protected void CheckProgramNameIsExist_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !CheckProgramNameIsExist(txtProgramName.Text);
    }

    //major part
    protected void gvMajorInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string program = e.CommandArgument.ToString();
            Response.Redirect($"AdminModifyProgramPage.aspx?program={program}");
        }
        else if (e.CommandName == "Delete")
        {
            string program = e.CommandArgument.ToString();
            Response.Redirect($"AdminDeleteProgramPage.aspx?program={program}");
        }
    }

    DataTable majorInfo = null;
    private void AddMajorInfo()
    {
        
    }
    protected void btnAddMajor_Click(object sender, EventArgs e)
    {

    }
    //button contain part
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMaintainProgramAndMajorMainPage.aspx");
    }

    protected void btnAddProgram_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            bool success = DatabaseManager.InsertData(
                "program",
                new List<string> { "program", "school", "level" },
                new List<object> { txtProgramName.Text, ddlSchool.SelectedValue, }
                );

            if (success)
            {
                successfulWindow.Style["display"] = "flex";
            }
        }
    }
}