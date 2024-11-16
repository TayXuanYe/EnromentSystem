using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminDeleteStudentPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["sid"] != null)
        {
            string studentId = Request.QueryString["sid"];
            SetStudentInfo(studentId);
        }
        else
        {
            Response.Redirect("AdminMaintainStudentMainPage.aspx");
        }
    }

    private void SetStudentInfo(string id)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "student",
            new List<string> { "name", "ic_or_passport", "date_of_birth", "hp_no",
                    "permanent_address", "permanent_postcode", "permanent_city", "permanent_state", "permanent_country",
                    "mode_of_study", "school", "level", "program", "major", "student_email", "scholarship",
                    "password", "admission_date" },
            $@"WHERE sid = '{id}'" 
            );

        if(dataSet != null)
        {
            foreach(DataRow row in dataSet.Tables[0].Rows)
            {
                lblName.Text = row["name"].ToString();
                lblPassportIc.Text = row["ic_or_passport"].ToString();
                lblDateOfBirth.Text = row["date_of_birth"].ToString();
                lblPhoneNumber.Text = row["hp_no"].ToString();
                lblAddress.Text = row["permanent_address"].ToString();
                lblPostcode.Text = row["permanent_postcode"].ToString();
                lblCity.Text = row["permanent_city"].ToString();
                lblState.Text = row["permanent_state"].ToString();
                lblCountry.Text = row["permanent_country"].ToString();
                lblModeOfStudy.Text = row["mode_of_study"].ToString();
                lblSchool.Text = row["school"].ToString();
                lblLevel.Text = row["level"].ToString();
                lblProgram.Text = row["program"].ToString();
                lblMajor.Text = row["major"].ToString();
                lblEmail.Text = row["student_email"].ToString();
                lblScholarship.Text = row["scholarship"].ToString();
                lblAdmissionDate.Text = row["admission_date"].ToString();
            }
            lblStudentId.Text = id;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        bool succesful = DatabaseManager.DeleteData(
            "student",
            $@"WHERE sid = '{lblStudentId.Text}'"
            );
        if (succesful)
        {
            successfulWindow.Style["display"] = "flex";
        }
        else
        {
            failWindow.Style["display"] = "flex";
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMaintainStudentMainPage.aspx");
    }
}