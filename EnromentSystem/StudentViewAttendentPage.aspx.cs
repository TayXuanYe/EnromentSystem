using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentViewAttendentPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sid"] != null)
        {
            if (!IsPostBack)
            {
                string studentId = Session["sid"].ToString();
                DataSet courseData = DatabaseManager.GetRecord(
                    "student_taken_course AS stc",
                    new List<string> { "stc.cid", "name", "section_id" },
                    $@"JOIN course ON course.cid = stc.cid WHERE status = 'TAKEN' AND stc.sid = 'I23024312' "
                    );
                DataTable displayTable = new DataTable();
                displayTable.Columns.Add("cid", typeof(string));
                displayTable.Columns.Add("name", typeof(string));
                displayTable.Columns.Add("attendance", typeof(string));
                if (courseData != null)
                {
                    foreach (DataRow row in courseData.Tables[0].Rows)
                    {
                        double allAttendance = DatabaseManager.GetRecordCount(
                            "lecturer_create_attendance_record",
                            $@"WHERE sectionId = '{row["section_id"].ToString()}'"
                            );
                        double takenAttendance = DatabaseManager.GetRecordCount(
                            "student_take_attendance",
                            $@"WHERE rid IN (select rid FROM lecturer_create_attendance_record WHERE sectionId = '{row["section_id"].ToString()}');"
                            );
                        double attendance = takenAttendance / allAttendance * 100;

                        displayTable.Rows.Add(row["cid"].ToString(), row["name"].ToString(), attendance);
                    }
                }
                gvCourse.DataSource = displayTable;
                gvCourse.DataBind();
                DataSet lectureData = DatabaseManager.GetRecord(
                    "student",
                    new List<string> { "name" },
                    $@"WHERE sid = '{studentId}'"
                    );
                if (lectureData != null)
                {
                    DataRow row = lectureData.Tables[0].Rows[0];
                    lblName.Text = row["name"].ToString();
                }

                DataSet semesterData = DatabaseManager.GetRecord(
                    "current_semester",
                    new List<string> { "semester" }
                    );
                if (semesterData != null)
                {
                    DataRow row = semesterData.Tables[0].Rows[0];
                    lblSemester.Text = row["semester"].ToString();
                }
            }
        }
        else
        {
            Response.Redirect("StudentHomePage.aspx");
        }
    }

    protected void gvCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        string courseId = gvCourse.SelectedRow.Cells[0].Text;
        string studentId = lblName.Text;
        string semester = lblSemester.Text;
        Response.Redirect($"StudentAttendanceDetailsPage.aspx?courseId={courseId}&lectureName={studentId}&semester={semester}");
    }
}