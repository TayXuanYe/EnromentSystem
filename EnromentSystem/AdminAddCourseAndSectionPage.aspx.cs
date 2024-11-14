using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminAddCourseAndSectionPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetClassTimetable();
            PopulateProgram();
            DataTable dt = new DataTable();
            gvPrerequisite.DataSource = dt;
            gvPrerequisite.DataBind();
            gvSectionInfo.DataSource = dt;
            gvSectionInfo.DataBind();
            DataTable preCourse = new DataTable();
            preCourse.Columns.Add("cid",typeof(string));
            preCourse.Columns.Add("name",typeof(string));
            Session["preCourseAdded"] = preCourse;
            PopulatePrerequisite(ddlProgram.SelectedValue,ddlMajor.SelectedValue, preCourse);
        }
    }
    //course part
    private void PopulateProgram()
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "program",
            new List<string> { "program" }
            );
        if(dataSet != null)
        {
            List<string> value = new List<string>();
            foreach(DataRow row in dataSet.Tables[0].Rows)
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
            $@"WHERE program = '{program}'"
            );
        if (dataSet != null)
        {
            List<string> value = new List<string>();
            value.Add("NONE");
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                value.Add(row["major"].ToString());
            }
            UIComponentGenerator.PopulateDropDownList(ddlMajor, value, value);
        }
    }

    protected void CheckCourseIdIsExist_ServerValidate(object source, ServerValidateEventArgs args)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "course",
            new List<string> { "cid" },
            $@"WHERE cid = '{txtCourseId.Text}'"
            );
        if(dataSet != null)
        {
            if(dataSet.Tables[0].Rows.Count == 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateMajor(ddlProgram.SelectedValue);
        //reset prerequisit ddl and session
        DataTable preCourse = new DataTable();
        preCourse.Columns.Add("cid", typeof(string));
        preCourse.Columns.Add("name", typeof(string));
        Session["preCourseAdded"] = preCourse;
        PopulatePrerequisite(ddlProgram.SelectedValue, ddlMajor.SelectedValue, preCourse);
        //empty gvPrerequisite
        DataTable empty = new DataTable();
        empty.Columns.Add("cid", typeof(string));
        empty.Columns.Add("name", typeof(string));
        gvPrerequisite.DataSource = empty;
        gvPrerequisite.DataBind();
    }

    protected void ddlMajor_SelectedIndexChanged(object sender, EventArgs e)
    {
        //reset prerequisit ddl and session
        DataTable preCourse = new DataTable();
        preCourse.Columns.Add("cid", typeof(string));
        preCourse.Columns.Add("name", typeof(string));
        Session["preCourseAdded"] = preCourse;
        PopulatePrerequisite(ddlProgram.SelectedValue, ddlMajor.SelectedValue, preCourse);
        //empty gvPrerequisite
        DataTable empty = new DataTable();
        empty.Columns.Add("cid", typeof(string));
        empty.Columns.Add("name", typeof(string));
        gvPrerequisite.DataSource = empty;
        gvPrerequisite.DataBind();
    }

    private void PopulatePrerequisite(string program, string major, DataTable addedCourse)
    {
        List<string> addedCourseList = new List<string>();
        addedCourseList.Add("empty");
        foreach (DataRow row in addedCourse.Rows)
        {
            addedCourseList.Add(row["cid"].ToString());
        }
        string removeCourses = string.Join(",", addedCourseList.Select(item => $"'{item}'"));
        DataSet dataSet = DatabaseManager.GetRecord(
            "course",
            new List<string> { "course.cid", "name" },
            $@"JOIN course_major as cm ON course.cid = cm.cid WHERE program = '{program}' AND major in ('{major}', 'NONE') AND course.cid NOT IN ({removeCourses})"
        );
        if(dataSet != null)
        {
            List<string> value = new List<string>();
            List<string> text = new List<string>();
            foreach(DataRow row in dataSet.Tables[0].Rows)
            {
                value.Add(row["cid"].ToString());
                text.Add($"{row["cid"].ToString()} - {row["name"].ToString()}");
            }
            UIComponentGenerator.PopulateDropDownList(ddlPrerequisite, text, value);
        }
    }

    protected void gvPrerequisite_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cid = gvPrerequisite.SelectedRow.Cells[0].Text;
        DataTable dataTable = Session["preCourseAdded"] as DataTable;
        if (dataTable == null) return;
        for (int i = dataTable.Rows.Count - 1; i >= 0; i--)
        {
            if (dataTable.Rows[i]["cid"].ToString() == cid)
            {
                dataTable.Rows[i].Delete();
            }
        }
        dataTable.AcceptChanges();
        gvPrerequisite.DataSource = dataTable;
        gvPrerequisite.DataBind();
    }

    protected void btnAddPrerequisite_Click(object sender, ImageClickEventArgs e)
    {
        DataTable displayValue = Session["preCourseAdded"] as DataTable;
        DataSet dataSet = DatabaseManager.GetRecord(
            "course",
            new List<string> { "name" },
            $@"WHERE cid = '{ddlPrerequisite.SelectedValue}'"
            );
        string courseName = "";
        if (dataSet != null)
        {
            foreach(DataRow row in dataSet.Tables[0].Rows)
            {
                courseName = row["name"].ToString();
            }
        }
        displayValue.Rows.Add(ddlPrerequisite.SelectedValue, courseName);
        gvPrerequisite.DataSource = displayValue;
        gvPrerequisite.DataBind();
        Session["preCourseAdded"] = displayValue;
        PopulatePrerequisite(ddlProgram.SelectedValue, ddlMajor.SelectedValue, displayValue);
    }
    //Section
    protected void btnAddSection_Click(object sender, ImageClickEventArgs e)
    {
        classWindows.Style["display"] = "flex";
        lblClassWindowsSectionName.Text = txtSectionName.Text;
        PopulateLecturer();
    }


    protected void gvSectionInfo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void gvSectionInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {

        }
    }
    // class select pop up windows
    private void SetClassTimetable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("day", typeof(string));
        dt.Columns.Add("0800", typeof(string));
        dt.Columns.Add("0900", typeof(string));
        dt.Columns.Add("1000", typeof(string));
        dt.Columns.Add("1100", typeof(string));
        dt.Columns.Add("1200", typeof(string));
        dt.Columns.Add("1300", typeof(string));
        dt.Columns.Add("1400", typeof(string));
        dt.Columns.Add("1500", typeof(string));
        dt.Columns.Add("1600", typeof(string));
        dt.Columns.Add("1700", typeof(string));
        dt.Columns.Add("1800", typeof(string));
        dt.Columns.Add("1900", typeof(string));
        dt.Columns.Add("2000", typeof(string));
        dt.Columns.Add("2100", typeof(string));
        dt.Columns.Add("2200", typeof(string));
        dt.Rows.Add("Mon");
        dt.Rows.Add("Tue");
        dt.Rows.Add("Wed");
        dt.Rows.Add("Thu");
        dt.Rows.Add("Fri");
        dt.Rows.Add("Sat");
        dt.Rows.Add("Sun");

        gvClassTimetable.DataSource = dt;
        gvClassTimetable.DataBind();
    }

    private void PopulateLecturer()
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "lecture",
            new List<string> { "name" }
            );
        if(dataSet != null)
        {
            List<string> value = new List<string>();
            value.Add("None");
            foreach(DataRow row in dataSet.Tables[0].Rows)
            {
                value.Add(row["name"].ToString());
            }
            UIComponentGenerator.PopulateDropDownList(ddlLectureClassLecturer,value,value);
            UIComponentGenerator.PopulateDropDownList(ddlPracticalClassLecturer,value,value);
        }
    }

    protected void btnAddLectureClass_Click(object sender, ImageClickEventArgs e)
    {
        if(ddlLectureClassLecturer.SelectedValue == "None")
        {
            lblWarningLectureClassLecturer.Style["display"] = "inline";
        }
        else
        {
            lblWarningLectureClassLecturer.Style["display"] = "none";

        }
    }

    protected void btnAddPracticalClass_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlPracticalClassLecturer.SelectedValue == "None")
        {
            lblWarningPracticalClassLecturer.Style["display"] = "inline";
        }
        else
        {
            lblWarningPracticalClassLecturer.Style["display"] = "none";

        }
    }

    protected void btnAddClass_Click(object sender, EventArgs e)
    {
        SetClassTimetable();
    }

    protected void btnCancelAddClass_Click(object sender, EventArgs e)
    {
        classWindows.Style["display"] = "none";
    }
    
    //btn bottom
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMaintainCourseAndSectionPage.aspx");
    }

    protected void btnAddCourse_Click(object sender, EventArgs e)
    {

    }
}