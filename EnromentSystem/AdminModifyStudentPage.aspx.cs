using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

public partial class AdminModifyStudentPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateSchool();
            if (Request.QueryString["sid"] != null)
            {
                string studentId = Request.QueryString["sid"];
                SetStudentInfo(studentId);
            }
            else
            {
                Response.Redirect("AdminLoginPage.aspx");
            }
        }
    }
    //info contain
    private void SetStudentInfo(string id)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "student",
            new List<string> { "name", "scholarship", "mode_of_study", "school", "level", "program", "major" },
            $@"WHERE sid = '{id}'" 
            );

        if(dataSet != null)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            { 
                lblName.Text = row["name"].ToString();
                txtScholarship.Text = row["scholarship"].ToString();
                ddlModeOfStudy.SelectedValue = row["mode_of_study"].ToString();
                PopulateSchool();
                ddlSchool.SelectedValue = row["school"].ToString();
                ddlLevel.SelectedValue = row["level"].ToString();
                PopulateProgram(row["school"].ToString());
                ddlProgram.SelectedValue = row["program"].ToString();
                PopulateMajor(row["school"].ToString());
                ddlMajor.SelectedValue = row["major"].ToString();
            }
            lblId.Text = id;
        }
    }
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
        PopulateProgram(ddlSchool.SelectedValue);
    }

    private void PopulateProgram(string school)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "program",
            new List<string> { "program" },
            "WHERE school = \'" + school + "\' AND level = \'" + ddlLevel.SelectedValue + "\'"
            );
        if (dataSet != null)
        {
            List<string> value = new List<string>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                value.Add(row["program"].ToString());
            }
            UIComponentGenerator.PopulateDropDownList(ddlProgram, value, value);
        }
        PopulateMajor(ddlProgram.SelectedValue);
    }

    private void PopulateMajor(string program)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "major",
            new List<string> { "major" },
            "WHERE program = \'" + program + "\'"
            );
        if (dataSet != null)
        {
            List<string> value = new List<string>();
            value.Add("None");
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                value.Add(row["major"].ToString());
            }
            UIComponentGenerator.PopulateDropDownList(ddlMajor, value, value);
        }
    }

    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateProgram(ddlSchool.SelectedValue);
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateProgram(ddlSchool.SelectedValue);
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateMajor(ddlProgram.SelectedValue);
    }

    protected void ProgramIsEmpty_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = (ddlProgram.SelectedIndex != -1);
    }

    //button contain
    private void SetComfirmTable(string id)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "student",
            new List<string> { "name", "scholarship", "mode_of_study", "school", "level", "program", "major" },
            $@"WHERE sid = '{id}'"
            );

        if (dataSet != null)
        {
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                lblCurrenScholarship.Text = row["scholarship"].ToString();
                lblCurrenStudyMode.Text = row["mode_of_study"].ToString();
                lblCurrenSchool.Text = row["school"].ToString();
                lblCurrenLevel.Text = row["level"].ToString();
                lblCurrenProgram.Text = row["program"].ToString();
                lblCurrenMajor.Text = row["major"].ToString();
            }
            lblUpdateLevel.Text = ddlLevel.SelectedValue;
            lblUpdateMajor.Text = ddlMajor.SelectedValue;
            lblUpdateProgram.Text = ddlProgram.SelectedValue;
            lblUpdateScholarship.Text = txtScholarship.Text;
            lblUpdateSchool.Text = ddlSchool.SelectedValue;
            lblUpdateStudyMode.Text = ddlModeOfStudy.SelectedValue;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMaintainStudentMainPage.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        confirmationWindow.Style["display"] = "flex";
        SetComfirmTable(lblId.Text);
    }

    //confirm windows button
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        confirmationWindow.Style["display"] = "none";
        if (Page.IsValid)
        {
            string studentId = Request.QueryString["sid"];
            successfulWindow.Style["display"] = "flex";
            bool success = DatabaseManager.UpdateData(
                "student",
                new List<string> { "scholarship", "mode_of_study", "school", "level", "program", "major" },
                new List<object> { txtScholarship.Text, ddlModeOfStudy.SelectedValue, ddlSchool.SelectedValue, 
                    ddlLevel.SelectedValue, ddlProgram.SelectedValue, ddlMajor.SelectedValue},
                $@"WHERE sid = '{studentId}'"
                );
            if(success)
            {
                successfulWindow.Style["display"] = "flex";
            }
        }
    }
    
    protected void btnCancelUpdate_Click(object sender, EventArgs e)
    {
        confirmationWindow.Style["display"] = "none";
    }
}