using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminMaintainCourseAndSectionPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["aid"] != null)
        {
            if (!IsPostBack)
            {
                SetCourseInfoGridView();
                PopulateSemester();
            }
        }
        else
        {
            Response.Redirect("AdminLoginPage.aspx");
        }
    }
    //SEARCH BAR
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SetCourseInfoGridView();
    }

    //course part
    private void SetCourseInfoGridView()
    {
        string condition = "FULL JOIN course_prerequisite AS cp ON course.cid = cp.cid FULL JOIN course_major AS cm ON course.cid = cm.cid ";
        string value = txtSearchInput.Text;
        DataSet dataSet = null;
        if (value != "")
        {
            switch (ddlSearch.SelectedValue)
            {
                case "all":
                    condition += $@"WHERE course.cid like '%{value}%'";
                    condition += $@"WHERE course.name like '%{value}%'";
                    condition += $@"WHERE credit_hours like '%{value}%'";
                    condition += $@"WHERE available like '%{value}%'";
                    condition += $@"WHERE price like '%{value}%'";
                    condition += $@"WHERE prerequisite like '%{value}%'";
                    condition += $@"WHERE program like '%{value}%'";
                    condition += $@"WHERE major like '%{value}%'";
                    break;

                case "cid":
                    condition += $@"WHERE course.cid like '%{value}%'";
                    break;

                case "name":
                    condition += $@"WHERE course.name like '%{value}%'";
                    break;

                case "credit_hours":
                    condition += $@"WHERE credit_hours like '%{value}%'";
                    break;

                case "available":
                    condition += $@"WHERE available like '%{value}%'";
                    break;

                case "price":
                    condition += $@"WHERE price like '%{value}%'";
                    break;

                case "prerequisite":
                    condition += $@"WHERE prerequisite like '%{value}%'";
                    break;

                case "program":
                    condition += $@"WHERE program like '%{value}%'";
                    break;

                case "major":
                    condition += $@"WHERE major like '%{value}%'";
                    break;
            }

            dataSet = DatabaseManager.GetRecord(
                "course",
                new List<string> { "course.cid", "course.name", "credit_hours", "available", "price", "program", "major", "prerequisite" },
                condition
            );
        }
        else
        {
            dataSet = DatabaseManager.GetRecord(
                "course",
                new List<string> { "course.cid", "course.name", "credit_hours", "available", "price", "program", "major", "prerequisite" },
                "FULL JOIN course_prerequisite AS cp ON course.cid = cp.cid FULL JOIN course_major AS cm ON course.cid = cm.cid "
            );
        }
        if (dataSet != null)
        {
            DataTable table = dataSet.Tables[0];
            gvCourseInfo.DataSource = table;
            gvCourseInfo.DataBind();
        }
    }

    protected void gvCourseInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Control commandSource = (Control)e.CommandSource;
        GridViewRow row = (GridViewRow)commandSource.NamingContainer;

        string courseId = row.Cells[0].Text; 
        string program = row.Cells[6].Text; 
        int index = row.RowIndex;

        if (e.CommandName == "Edit")
        {
            //Response.Redirect($"AdminModifyProgramAndMajorPage.aspx?course={course}&index={index}");
        }
        else if (e.CommandName == "Delete")
        {
            Response.Redirect($"AdminDeleteCoursePage.aspx?course={courseId}&program={program}");
        }
    }

    protected void btnAddCourse_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminAddCourseAndSectionPage.aspx");
    }

    protected void gvCourseInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string course = gvCourseInfo.SelectedRow.Cells[0].Text;
        string program = gvCourseInfo.SelectedRow.Cells[6].Text;
        SetSectionInfoGridView(course, ddlSectionSemester.SelectedValue, program);
    }

    //section part
    private void PopulateSemester()
    {
        DataSet dataSet = null;
        dataSet = DatabaseManager.GetDistinctRecord(
            "section",
            new List<string> { "semester" }
            );
        if(dataSet != null)
        {
            List<string> value = new List<string>();
            foreach(DataRow row in dataSet.Tables[0].Rows)
            {
                value.Add(row["semester"].ToString());
            }
            UIComponentGenerator.PopulateDropDownList(ddlSectionSemester, value, value);

        }
    }
    private void SetSectionInfoGridView(string course, string semester, string program)
    {
        DataSet dataSet = null;
        dataSet = DatabaseManager.GetRecord(   
            "section",
            new List<string> { "sid", "section.name",  "max_enroll" },
            $@"WHERE cid = '{course}' AND semester = '{semester}' AND program = '{program}'"
        );
        if (dataSet != null)
        {
            DataTable table = dataSet.Tables[0];
            gvSectionInfo.DataSource = table;
            gvSectionInfo.DataBind();
        }
    }
}