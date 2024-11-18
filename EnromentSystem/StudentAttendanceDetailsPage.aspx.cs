using Microsoft.Ajax.Utilities;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentAttendanceDetailsPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sid"] != null)
        {
            if (!IsPostBack)
            {
                string courseId = Request.QueryString["courseId"];
                string lectureName = Request.QueryString["lectureName"];
                string semester = Request.QueryString["semester"];
                
                if (courseId.IsNullOrWhiteSpace() || lectureName.IsNullOrWhiteSpace() || semester.IsNullOrWhiteSpace())
                {
                    Response.Redirect("StudentViewAttendentPage.aspx");
                }
                else
                {
                    lblCourse.Text = courseId;
                    lblLecturer.Text = lectureName;
                    lblSemester.Text = semester;
                    SetHistoryGridView(courseId, Session["sid"].ToString());
                }
            }
        }
        else
        {
            Response.Redirect("StudentHomePage.aspx");
        }
    }


    private void SetHistoryGridView(string courseId, string studentid)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "lecturer_create_attendance_record AS lca",
            new List<string> { "cid", "name", "date", "sta.rid" },
            $@"WHERE sid IN ('{studentid}',null) AND courseId = '{courseId}' AND 
            sectionId IN (SELECT section_id FROM student_taken_course WHERE cid = '{courseId}' AND sid = '{studentid}')  ORDER by date"
            );
        DataTable displayTable = new DataTable();
        displayTable.Columns.Add("cid",typeof(string));
        displayTable.Columns.Add("name",typeof(string));
        displayTable.Columns.Add("date",typeof(string));
        displayTable.Columns.Add("attendance", typeof(string));
        foreach(DataRow row in dataSet.Tables[0].Rows)
        {
            string attend = "";
            if (row["attendance"].ToString().IsNullOrWhiteSpace())
            {
                attend = "Not Attend";
            }
            else
            {
                attend = "Attend";
            }
            displayTable.Rows.Add(row["cid"].ToString(), row["name"].ToString(), row["date"].ToString(), attend);
        }

        gvHistory.DataSource = displayTable;
        gvHistory.DataBind();
    }

}