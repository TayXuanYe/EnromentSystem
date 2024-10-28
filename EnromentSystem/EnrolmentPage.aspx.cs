using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class EnrolmentPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["sid"] = "I23024312";
        SetCourseEnrolmentDetails();
        SetPrevoiousFailedCourseTable();
        SetPreviousCompulsoryCourseTable();
        SetCourseEnrolledTable();
    }

    private void SetCourseEnrolmentDetails()
    {
        DataSet dataSet = DatabaseManager.GetRecord(
                "student",
                new List<string> {
                    "name",
                    "ic_or_passport",
                    "mode_of_study",
                    "school",
                    "level",
                    "program",
                    "major"
                },
                "WHERE sid = \'" + Session["sid"] + "\'"
            );
        DataTable dataTable = dataSet.Tables[0];
        string name = null;
        string icPassport = null;
        string studyMode = null;
        string school = null;
        string level = null;
        string program = null;
        string major = null;
        foreach (DataRow row in dataTable.Rows)
        {
            name = row["name"].ToString();
            icPassport = row["ic_or_passport"].ToString();
            studyMode = row["mode_of_study"].ToString();
            school = row["school"].ToString();
            level = row["level"].ToString();
            program = row["program"].ToString();
            major = row["major"].ToString();
        }
        dataSet = DatabaseManager.GetRecord(
              "current_session",
              new List<string> {
                    "session",
                    "credit_hour"
              }
          );
        dataTable = dataSet.Tables[0];
        string session = null;
        foreach (DataRow row in dataTable.Rows)
        {
            session = row["session"].ToString();
            Session["maxCreditHourAvailable"] = row["credit_hour"].ToString();

        }

        lblMatriculationNo.Text = Session["sid"].ToString();
        lblStudentName.Text = name;
        lblIcPassportNo.Text = icPassport;
        lblStudyMode.Text = studyMode;
        lblSession.Text = session;
        lblSchool.Text = school;
        lblLevel.Text = level;
        lblProgram.Text = program;
        lblMajor.Text = major;
    }

    private void SetPrevoiousFailedCourseTable()
    {
        //Add header
        TableHeaderRow headerRow = new TableHeaderRow();
        TableHeaderCell cellCourseCodeHeader = new TableHeaderCell { Text = "Course Code" };
        TableHeaderCell cellCourseNameHeader = new TableHeaderCell { Text = "Course Name" };
        headerRow.Cells.Add(cellCourseCodeHeader);
        headerRow.Cells.Add(cellCourseNameHeader);
        tblPreviousFailedCourse.Rows.Add(headerRow);
        //Get record
        DataSet dataSet = DatabaseManager.GetRecord(
                "student_taken_course",
                new List<string> {
                    "student_taken_course.cid",
                    "name"
                },
                "INNER JOIN course ON student_taken_course.cid = course.cid " +
                "WHERE student_id = \'" + Session["sid"] + "\' AND " + "status = 'fail'"
            );
        DataTable dataTable = dataSet.Tables[0];
        foreach (DataRow row in dataTable.Rows)
        {
            TableRow tbRow = new TableRow();
            TableCell cellCourseCode = new TableCell { Text = row["cid"].ToString() };
            TableCell cellCourseName = new TableCell { Text = row["name"].ToString() };
            tbRow.Cells.Add(cellCourseCode);
            tbRow.Cells.Add(cellCourseName);
            tblPreviousFailedCourse.Rows.Add(tbRow);
        }
        if (tblPreviousFailedCourse.Rows.Count == 1)
        {
            TableRow tbRow = new TableRow();
            TableCell cellCourseCode = new TableCell { Text = "<br>" };
            TableCell cellCourseName = new TableCell { Text = "<br>" };
            tbRow.Cells.Add(cellCourseCode);
            tbRow.Cells.Add(cellCourseName);
            tblPreviousFailedCourse.Rows.Add(tbRow);
        }
    }

    private void SetPreviousCompulsoryCourseTable()
    {
        //Add header
        TableHeaderRow headerRow = new TableHeaderRow();
        TableHeaderCell cellCourseCodeHeader = new TableHeaderCell { Text = "Course Code" };
        TableHeaderCell cellCourseNameHeader = new TableHeaderCell { Text = "Course Name" };
        headerRow.Cells.Add(cellCourseCodeHeader);
        headerRow.Cells.Add(cellCourseNameHeader);
        tblPreviousCompulsoryCourse.Rows.Add(headerRow);
        //Get record
        DataSet dataSet = DatabaseManager.GetRecord(
                "previous_compulsory_course",
                new List<string> {
                    "previous_compulsory_course.cid",
                    "name"
                },
                "INNER JOIN course ON previous_compulsory_course.cid = course.cid " +
                "WHERE sid = \'" + Session["sid"] + "\'"
            );
        DataTable dataTable = dataSet.Tables[0];
        foreach (DataRow row in dataTable.Rows)
        {
            TableRow tbRow = new TableRow();
            TableCell cellCourseCode = new TableCell { Text = row["cid"].ToString() };
            TableCell cellCourseName = new TableCell { Text = row["name"].ToString() };
            tbRow.Cells.Add(cellCourseCode);
            tbRow.Cells.Add(cellCourseName);
            tblPreviousCompulsoryCourse.Rows.Add(tbRow);
        }
        if(tblPreviousCompulsoryCourse.Rows.Count == 1)
        {
            TableRow tbRow = new TableRow();
            TableCell cellCourseCode = new TableCell { Text = "<br>" };
            TableCell cellCourseName = new TableCell { Text = "<br>" };
            tbRow.Cells.Add(cellCourseCode);
            tbRow.Cells.Add(cellCourseName);
            tblPreviousCompulsoryCourse.Rows.Add(tbRow);
        }
    }

    private void SetCourseEnrolledTable()
    {
        //Add header
        TableHeaderRow headerRow = new TableHeaderRow();
        TableHeaderCell cellNoHeader= new TableHeaderCell { Text = "No" };
        TableHeaderCell cellCourseCodeHeader = new TableHeaderCell { Text = "Course Code" };
        TableHeaderCell cellCourseNameHeader = new TableHeaderCell { Text = "Course Name" };
        TableHeaderCell cellCourseCreditsHeader = new TableHeaderCell { Text = "Credits" };
        TableHeaderCell cellActionHeader = new TableHeaderCell { Text = "Action" };
        headerRow.Cells.Add(cellNoHeader);
        headerRow.Cells.Add(cellCourseCodeHeader);
        headerRow.Cells.Add(cellCourseNameHeader);
        headerRow.Cells.Add(cellCourseCreditsHeader);
        headerRow.Cells.Add(cellActionHeader);
        tblCourseEnrolled.Rows.Add(headerRow);

        if (tblCourseEnrolled.Rows.Count == 1)
        {
            TableRow tbRow = new TableRow();
            TableCell cellNo = new TableCell { Text = "<br>" };
            TableCell cellCourseCode = new TableCell { Text = "<br>" };
            TableCell cellCourseName = new TableCell { Text = "<br>" };
            TableCell cellCourseCredits = new TableCell { Text = "<br>" };
            TableCell cellAction = new TableCell { Text = "<br>" };

            tbRow.Cells.Add(cellNo);
            tbRow.Cells.Add(cellCourseCode);
            tbRow.Cells.Add(cellCourseName);
            tbRow.Cells.Add(cellCourseCredits);
            tbRow.Cells.Add(cellAction);
            tblCourseEnrolled.Rows.Add(tbRow);
        }
    }
}