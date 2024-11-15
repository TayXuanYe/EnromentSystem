using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminDeleteCoursePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["course"] != null && Request.QueryString["program"] != null)
        {
            string courseId = Request.QueryString["course"];
            string program = Request.QueryString["program"];
            SetCourseDetails(courseId, program);
            SetSectionInfoGridView(courseId, program);
        }
        else
        {
            Response.Redirect("AdminMaintainCourseAndSectionPage.aspx");
        }
    }
    //course part
    private void SetCourseDetails(string courseId, string program)
    {
        DataSet courseData = DatabaseManager.GetRecord(
            "course",
            new List<string> { "major", "name", "credit_hours", "price" },
            $@"JOIN course_major AS cm ON course.cid = cm.cid WHERE program = '{program}' AND course.cid = '{courseId}'"
            );
        if(courseId != null)
        {
            lblProgram.Text = program;
            foreach(DataRow dataRow in courseData.Tables[0].Rows)
            {
                lblMajor.Text = dataRow["major"].ToString();
                lblCourseId.Text = courseId;
                lblCourseName.Text = dataRow["name"].ToString();
                lblCreditHours.Text = dataRow["credit_hours"].ToString();
                lblPrice.Text = dataRow["price"].ToString();
            }
        }

        DataSet preCourse = DatabaseManager.GetRecord(
            "course",
            new List<string> { "cid", "name" },
            $@"WHERE cid IN (SELECT prerequisite FROM course_prerequisite WHERE cid = '{courseId}')"
            );
        if(preCourse != null)
        {
            gvPrerequisite.DataSource = preCourse;
            gvPrerequisite.DataBind();
        }
    }

    //section part
    private void SetSectionInfoGridView(string courseId, string program)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "section",
            new List<string> { "sid", "name", "semester" },
            $@"WHERE cid = '{courseId}' AND program = '{program}'"
            );
        if(dataSet != null)
        {
            gvSectionInfo.DataSource = dataSet.Tables[0];
            gvSectionInfo.DataBind();
        }
    }

    //footer button
    protected void btnDeleteCourse_Click(object sender, EventArgs e)
    {
        lblConformText.Text = lblCourseId.Text + "-" + lblCourseName.Text;
        conformWindow.Style["display"] = "flex";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMaintainCourseAndSectionPage.aspx");
    }

    //conform window
    protected void ConformTextCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (lblConformText.Text == txtConformText.Text)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }

    protected void btnCancelDeleteConform_Click(object sender, EventArgs e)
    {
        conformWindow.Style["display"] = "none";
    }

    protected void btnConformDeleteCourse_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            conformWindow.Style["display"] = "none";
            string cid = lblCourseId.Text;
            bool successDeleteClass = DatabaseManager.DeleteData(
                "class",
                $@"WHERE sid IN (SELECT sid FROM section WHERE cid = '{cid}')"
                );

            bool successDeleteSection = DatabaseManager.DeleteData(
                "section",
                $@"WHERE cid = '{cid}'"
            );

            bool successDeletePreCourse = DatabaseManager.DeleteData(
                "course_prerequisite",
                $@"WHERE cid = '{cid}' OR prerequisite = '{cid}'"
            );

            bool successDeleteCourseMajor = DatabaseManager.DeleteData(
                "course_major",
                $@"WHERE cid = '{cid}'"
            );
            
            bool successDeleteCourse = DatabaseManager.DeleteData(
                "course",
                $@"WHERE cid = '{cid}'"
            );

            if (successDeleteClass && successDeleteSection && successDeletePreCourse && successDeleteCourseMajor && successDeleteCourse)
            {
                successfulWindow.Style["display"] = "flex";
            }
            else
            {
                failWindow.Style["display"] = "flex";
            }
        }
    }
}