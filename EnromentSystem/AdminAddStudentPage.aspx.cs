using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

public partial class AdminAddStudentPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateSchool();
            SetSuggestStudentId();
        }
    }
    //info contain
    private void PopulateSchool()
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "school",
            new List<string> { "school" }
            );
        if(dataSet != null)
        {
            List<string> value = new List<string>();
            foreach(DataRow row in dataSet.Tables[0].Rows)
            {
                value.Add(row["school"].ToString());
            }
            UIComponentGenerator.PopulateDropDownList(ddlSchool,value,value);
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

    private void SetSuggestStudentId()
    {
        for (int i = 0; i <= 9999; i++)
        {
            string id = "I" + DateTime.Now.Year + i.ToString("D4");
            id.ToUpper();
            if (!CheckStudentIdIsExist(id))
            {
                txtStudentId.Text = id;
                SetSuggestStudentEmail(id);
                break;
            }
        }
        
    }

    private void SetSuggestStudentEmail(string id)
    {
        string email = id.ToLower() + "@student.newinti.edu.my";
        if (!CheckStudentEmailIsExist(email))
        {
            txtStudentEmail.Text = email;
        }
        
    }

    private bool CheckStudentIdIsExist(string id)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "student",
            new List<string> { "sid" },
            "WHERE sid = \'" + id + "\'"
            );
        if(dataSet != null)
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
    
    private bool CheckStudentEmailIsExist(string email)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
        "student",
            new List<string> { "student_email" },
            "WHERE student_email = \'" + email + "\'"
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

    protected void CheckStudentIdIsExist_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !CheckStudentIdIsExist(txtStudentId.Text);
    }

    protected void CheckStudentEmailIsExist_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !CheckStudentEmailIsExist(txtStudentEmail.Text);
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMaintainStudentMainPage.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            successfulWindow.Style["display"] = "flex";
            DatabaseManager.InsertData(
                "student",
                new List<string> { "name", "ic_or_passport", "date_of_birth", "hp_no",
                    "permanent_address", "premenant_postcode", "permenant_city", "permenant_state", "permenant_country",
                    "sid", "mode_of_study", "school", "level", "program", "major", "student_email", "scholarship",
                    "password", "admission_date"
                },
                new List<object> {
                    txtStudentName.Text, txtPassportOrIC.Text, txtDate.Text, txtPhoneNumber.Text,
                    txtPermanentAddress.Text, txtPermanentPostcode.Text, txtPermanentCity.Text, txtState.Text, txtCountry.Text,
                    txtStudentId.Text, ddlModeOfStudy.SelectedValue, ddlSchool.SelectedValue, ddlLevel.SelectedValue, ddlProgram.SelectedValue,
                    ddlMajor.SelectedValue, txtStudentEmail.Text, txtScholarship.Text,
                    "iu"+txtPassportOrIC.Text, DateTime.Now.Date
                }
                );
        }
    }
}