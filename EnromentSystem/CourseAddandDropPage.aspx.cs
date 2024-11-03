using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CourseAddandDropPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sid"] != null)
        {
            SetCourseEnrolmentDetails();
            SetCurrentEnrolledCourseTable();
            SetRequestChangesTable();
            lblErrorMessage.Text = "";
            if (!IsPostBack)
            {
                PopulateCourseCodeListing();
                SetPreRequisiteTable(ddlCourseCodeListing.SelectedValue);
                PopulateCourseSection(ddlCourseCodeListing.SelectedValue);
            }
        }
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
              "current_semester",
              new List<string> {
                    "semester"
              }
          );
        dataTable = dataSet.Tables[0];
        string semester = null;
        foreach (DataRow row in dataTable.Rows)
        {
            semester = row["semester"].ToString();
        }

        lblMatriculationNo.Text = Session["sid"].ToString();
        lblStudentName.Text = name;
        lblIcPassportNo.Text = icPassport;
        lblStudyMode.Text = studyMode;
        lblSession.Text = semester;
        lblSchool.Text = school;
        lblLevel.Text = level;
        lblProgram.Text = program;
        lblMajor.Text = major;
    }

    private void SetCurrentEnrolledCourseTable()
    {
        //Add header
        tblCurrentEnrolledCourse.Rows.Clear();
        if (tblCurrentEnrolledCourse.Rows.Count == 0)
        {
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell cellNoHeader = new TableHeaderCell { Text = "No" };
            TableHeaderCell cellCourseCodeHeader = new TableHeaderCell { Text = "Course Code" };
            TableHeaderCell cellCourseNameHeader = new TableHeaderCell { Text = "Course Name" };
            TableHeaderCell cellCourseCreditsHeader = new TableHeaderCell { Text = "Credits" };
            TableHeaderCell cellActionHeader = new TableHeaderCell { Text = "Operation" };
            headerRow.Cells.Add(cellNoHeader);
            headerRow.Cells.Add(cellCourseCodeHeader);
            headerRow.Cells.Add(cellCourseNameHeader);
            headerRow.Cells.Add(cellCourseCreditsHeader);
            headerRow.Cells.Add(cellActionHeader);
            tblCurrentEnrolledCourse.Rows.Add(headerRow);
        }
        //insert data from database
        DataSet courseDataSet = DatabaseManager.GetRecord(
                "student_taken_course",
                new List<string> {
                    "student_taken_course.cid",
                    "name",
                    "credit_hours"
                },
                "INNER JOIN course AS c " +
                "ON student_taken_course.cid = c.cid " +
                "WHERE student_taken_course.sid = \'" + Session["sid"] + "\' " +
                "AND student_taken_course.status = 'TAKEN' "
            );
        if (courseDataSet != null)
        {
            DataTable dt = courseDataSet.Tables[0];
            int count = 0;
            foreach (DataRow row in dt.Rows)
            {
                count++;
                TableRow tbRow = new TableRow();
                TableCell cellNo = new TableCell { Text = count.ToString() };
                TableCell cellCourseCode = new TableCell { Text = row["cid"].ToString() };
                TableCell cellCourseName = new TableCell { Text = row["name"].ToString() };
                TableCell cellCourseCredits = new TableCell { Text = row["credit_hours"].ToString() };
                
                TableCell cellAction = new TableCell();

                bool edited = false;
                DataSet addCourseRecord = DatabaseManager.GetRecord(
                    "request_add_course",
                    new List<string> { "*" },
                    "WHERE cid = \'" + row["cid"].ToString() +
                    "\' AND sid = \'" + Session["sid"] + "\'"
                    );
                if (addCourseRecord.Tables[0].Rows.Count != 0)
                {
                    edited = true;
                }
                DataSet dropCourseRecord = DatabaseManager.GetRecord(
                    "request_drop_course",
                    new List<string> { "*" },
                    "WHERE cid = \'" + row["cid"].ToString() +
                    "\' AND sid = \'" + Session["sid"] + "\'"
                    );
                if (dropCourseRecord.Tables[0].Rows.Count != 0)
                {
                    edited = true;
                }
                DataSet changeSectionRecord = DatabaseManager.GetRecord(
                    "request_change_section",
                    new List<string> { "*" },
                    "WHERE cid = \'" + row["cid"].ToString() +
                    "\' AND sid = \'" + Session["sid"] + "\'"
                    );
                if (changeSectionRecord.Tables[0].Rows.Count != 0)
                {
                    edited = true;
                }
                if(edited == false)
                {
                    Button btn = new Button
                    {
                        CssClass = "action-button",
                        CommandArgument = (tblCurrentEnrolledCourse.Rows.Count).ToString(),
                        Text = "Operation"
                    };
                    btn.Click += new EventHandler(btnOperateCourse_Click);
                    cellAction.Controls.Add(btn);
                }
                else
                {
                    cellAction.Text = "<br>";
                }

                tbRow.Cells.Add(cellNo);

                tbRow.Cells.Add(cellCourseCode);
                tbRow.Cells.Add(cellCourseName);
                tbRow.Cells.Add(cellCourseCredits);
                tbRow.Cells.Add(cellAction);
                tblCurrentEnrolledCourse.Rows.Add(tbRow);
            }
        }


        if (tblCurrentEnrolledCourse.Rows.Count == 1)
        {
            TableRow tbRow = new TableRow();
            TableCell cellNo = new TableCell { Text = "1" };
            TableCell cellCourseCode = new TableCell { Text = "<br>" };
            TableCell cellCourseName = new TableCell { Text = "<br>" };
            TableCell cellCourseCredits = new TableCell { Text = "<br>" };
            TableCell cellAction = new TableCell { Text = "<br>" };

            tbRow.Cells.Add(cellNo);
            tbRow.Cells.Add(cellCourseCode);
            tbRow.Cells.Add(cellCourseName);
            tbRow.Cells.Add(cellCourseCredits);
            tbRow.Cells.Add(cellAction);
            tblCurrentEnrolledCourse.Rows.Add(tbRow);
        }
    }

    private void SetRequestChangesTable()
    {
        //Add header
        tblRequestChanges.Rows.Clear();
        if (tblRequestChanges.Rows.Count == 0)
        {
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell cellNoHeader = new TableHeaderCell { Text = "No" };
            TableHeaderCell cellCourseCodeHeader = new TableHeaderCell { Text = "Course Code" };
            TableHeaderCell cellCourseInfoHeader = new TableHeaderCell { Text = "Course Info" };
            TableHeaderCell cellCourseCreditsHeader = new TableHeaderCell { Text = "Credits" };
            TableHeaderCell cellActionHeader = new TableHeaderCell { Text = "Delete" };
            headerRow.Cells.Add(cellNoHeader);
            headerRow.Cells.Add(cellCourseCodeHeader);
            headerRow.Cells.Add(cellCourseInfoHeader);
            headerRow.Cells.Add(cellCourseCreditsHeader);
            headerRow.Cells.Add(cellActionHeader);
            tblRequestChanges.Rows.Add(headerRow);
        }
        // DROP COURSE
        {
            //insert data from database (DROP course)
            DataSet courseDropDataSet = DatabaseManager.GetRecord(
                    "request_drop_course",
                    new List<string> {
                        "request_drop_course.cid",
                        "credit_hours",
                        "status",
                        "reason"
                    },
                    "INNER JOIN course AS c " +
                    "ON request_drop_course.cid = c.cid " +
                    "WHERE request_drop_course.sid = \'" + Session["sid"] + "\' "
                );
            if (courseDropDataSet != null)
            {
                DataTable dt = courseDropDataSet.Tables[0];
                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    count++;
                    TableRow tbRow = new TableRow();
                    TableCell cellNo = new TableCell { Text = count.ToString() };
                    TableCell cellCourseCode = new TableCell { Text = row["cid"].ToString() };
                    TableCell cellCourseInfo = new TableCell();
                    if (row["status"].ToString() == "HOLD")
                    {
                        cellCourseInfo.Text =
                            "You have selected to <b>DROP</b> Course <b>" +
                            row["cid"].ToString() +
                            "</b>. The request has not been sent yet.<br>" +
                            "<span class=\"request-reason\"><b>Reason:</b> " +
                            row["reason"].ToString() +
                            "</span>";
                    }
                    else
                    {
                        cellCourseInfo.Text =
                            "You have selected to <b>DROP</b> Course <b>" +
                            row["cid"].ToString() +
                            "</b> and is's <span class=\"approve-status\">" +
                            row["status"].ToString() +
                            "</span> for your HOP Approve.<br>" +
                            "<span class=\"request-reason\"><b>Reason:</b> " +
                            row["reason"].ToString() +
                            "</span>";
                    }
                    
                    TableCell cellCourseCredits = new TableCell { Text = row["credit_hours"].ToString() };

                    TableCell cellAction = new TableCell();
                    if (row["status"].ToString() == "PENDING" || row["status"].ToString() == "HOLD")
                    {
                        Button btn = new Button
                        {
                            CssClass = "action-button",
                            CommandArgument = (tblRequestChanges.Rows.Count).ToString(),
                            Text = "Delete"
                        };
                        btn.Click += new EventHandler(btnDeleteRequestDropCourse_Click);
                        cellAction.Controls.Add(btn);
                    }
                    else
                    {
                        cellAction.Text = "<br>";
                    }

                    tbRow.Cells.Add(cellNo);

                    tbRow.Cells.Add(cellCourseCode);
                    tbRow.Cells.Add(cellCourseInfo);
                    tbRow.Cells.Add(cellCourseCredits);
                    tbRow.Cells.Add(cellAction);
                    tblRequestChanges.Rows.Add(tbRow);
                }
            }
        }

        //ADD COURSE
        {
            //insert data from database (ADD course)
            DataSet courseDropDataSet = DatabaseManager.GetRecord(
                    "request_add_course",
                    new List<string> {
                        "request_add_course.cid",
                        "credit_hours",
                        "status",
                        "s.name",
                        "reason"
                    },
                    "INNER JOIN course AS c " +
                    "ON request_add_course.cid = c.cid " +
                    "INNER JOIN section AS s " +
                    "ON request_add_course.section_id = s.sid " +
                    "WHERE request_add_course.sid = \'" + Session["sid"] + "\' "
                );
            if (courseDropDataSet != null)
            {
                DataTable dt = courseDropDataSet.Tables[0];
                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    count++;
                    TableRow tbRow = new TableRow();
                    TableCell cellNo = new TableCell { Text = count.ToString() };
                    TableCell cellCourseCode = new TableCell { Text = row["cid"].ToString() };
                    TableCell cellCourseInfo = new TableCell();
                    if (row["status"].ToString() == "HOLD")
                    {
                        cellCourseInfo.Text =
                            "You have selected to <b>DROP</b> Course <b>" +
                            row["cid"].ToString() +
                            "</b>. The request has not been sent yet.<br>" +
                            "<span class=\"request-reason\"><b>Reason:</b> " +
                            row["reason"].ToString() +
                            "</span>";
                    }
                    else
                    {
                        cellCourseInfo.Text =
                            "You have selected to <b>DROP</b> Course <b>" +
                            row["cid"].ToString() +
                            "</b> and is's <span class=\"approve-status\">" +
                            row["status"].ToString() +
                            "</span> for your HOP Approve.<br>" +
                            "<span class=\"request-reason\"><b>Reason:</b> " +
                            row["reason"].ToString() +
                            "</span>";
                    }
                    TableCell cellCourseCredits = new TableCell { Text = row["credit_hours"].ToString() };

                    TableCell cellAction = new TableCell();
                    string status = row["status"].ToString();
                    if (status == "PENDING" || row["status"].ToString() == "HOLD")
                    {
                        Button btn = new Button
                        {
                            CssClass = "action-button",
                            CommandArgument = (tblRequestChanges.Rows.Count).ToString(),
                            Text = "Delete"
                        };
                        btn.Click += new EventHandler(btnDeleteRequestAddCourse_Click);
                        cellAction.Controls.Add(btn);
                    }
                    else
                    {
                        cellAction.Text = "<br>";
                    }
                    tbRow.Cells.Add(cellNo);

                    tbRow.Cells.Add(cellCourseCode);
                    tbRow.Cells.Add(cellCourseInfo);
                    tbRow.Cells.Add(cellCourseCredits);
                    tbRow.Cells.Add(cellAction);
                    tblRequestChanges.Rows.Add(tbRow);
                }
            }
        }

        //CHANGE SECTION 
        {
            //insert data from database (CHANGE section)
            DataSet courseDropDataSet = DatabaseManager.GetRecord(
                    "request_change_section",
                    new List<string> {
                        "request_change_section.cid",
                        "credit_hours",
                        "status",
                        "s1.name",
                        "s2.name",
                        "reason"
                    },
                    "INNER JOIN course AS c " +
                    "ON request_change_section.cid = c.cid " +
                    "INNER JOIN section AS s1 " +
                    "ON request_change_section.current_section_id = s1.sid " +
                    "INNER JOIN section AS s2 " +
                    "ON request_change_section.target_section_id = s2.sid " +
                    "WHERE request_change_section.sid = \'" + Session["sid"] + "\' "
                );
            if (courseDropDataSet != null)
            {
                DataTable dt = courseDropDataSet.Tables[0];
                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    count++;
                    TableRow tbRow = new TableRow();
                    TableCell cellNo = new TableCell { Text = count.ToString() };
                    TableCell cellCourseCode = new TableCell { Text = row["cid"].ToString() };
                    TableCell cellCourseInfo = new TableCell();
                    if (row["status"].ToString() == "HOLD")
                    {
                        cellCourseInfo.Text =
                            "You have selected to <b>DROP</b> Course <b>" +
                            row["cid"].ToString() +
                            "</b>. The request has not been sent yet.<br>" +
                            "<span class=\"request-reason\"><b>Reason:</b> " +
                            row["reason"].ToString() +
                            "</span>";
                    }
                    else
                    {
                        cellCourseInfo.Text =
                            "You have selected to <b>DROP</b> Course <b>" +
                            row["cid"].ToString() +
                            "</b> and is's <span class=\"approve-status\">" +
                            row["status"].ToString() +
                            "</span> for your HOP Approve.<br>" +
                            "<span class=\"request-reason\"><b>Reason:</b> " +
                            row["reason"].ToString() +
                            "</span>";
                    }
                    TableCell cellCourseCredits = new TableCell { Text = row["credit_hours"].ToString() };

                    TableCell cellAction = new TableCell();
                    string status = row["status"].ToString();
                    if (status == "PENDING" || row["status"].ToString() == "HOLD")
                    {
                        Button btn = new Button
                        {
                            CssClass = "action-button",
                            CommandArgument = (tblRequestChanges.Rows.Count).ToString(),
                            Text = "Delete"
                        };
                        btn.Click += new EventHandler(btnDeleteRequestChangeSection_Click);
                        cellAction.Controls.Add(btn);
                    }
                    else
                    {
                        cellAction.Text = "<br>";
                    }
                    tbRow.Cells.Add(cellNo);

                    tbRow.Cells.Add(cellCourseCode);
                    tbRow.Cells.Add(cellCourseInfo);
                    tbRow.Cells.Add(cellCourseCredits);
                    tbRow.Cells.Add(cellAction);
                    tblRequestChanges.Rows.Add(tbRow);
                }
            }
        }


        if (tblRequestChanges.Rows.Count == 1)
        {
            TableRow tbRow = new TableRow();
            TableCell cellNo = new TableCell { Text = "1" };
            TableCell cellCourseCode = new TableCell { Text = "<br>" };
            TableCell cellCourseInfo = new TableCell { Text = "<br>" };
            TableCell cellCourseCredits = new TableCell { Text = "<br>" };
            TableCell cellAction = new TableCell { Text = "<br>" };

            tbRow.Cells.Add(cellNo);
            tbRow.Cells.Add(cellCourseCode);
            tbRow.Cells.Add(cellCourseInfo);
            tbRow.Cells.Add(cellCourseCredits);
            tbRow.Cells.Add(cellAction);
            tblRequestChanges.Rows.Add(tbRow);
        }
    }

    protected void btnAddCourse_Click(object sender, EventArgs e)
    {
        SetCurrentEnrolledCourseTable();
        PopulateCourseCodeListing();
        SetPreRequisiteTable(ddlCourseCodeListing.SelectedValue);
        txtAddCourseReason.Text = "";
        addCoursePopUpWindow.Style["display"] = "flex";
    }

    protected void btnOperateCourse_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = int.Parse(btn.CommandArgument);
        string courseId = null;
        if (rowIndex < tblCurrentEnrolledCourse.Rows.Count)
        {
            TableCell cell = tblCurrentEnrolledCourse.Rows[rowIndex].Cells[1];
            courseId = cell.Text;
        }
        if (courseId != null)
        {
            lblCourseId.Text = courseId;
            selectOperationpopUpWindow.Style["display"] = "flex";
        }
    }

    protected void btnDeleteRequestAddCourse_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = int.Parse(btn.CommandArgument);
        string courseId = null;
        if (rowIndex < tblRequestChanges.Rows.Count)
        {
            TableCell cell = tblRequestChanges.Rows[rowIndex].Cells[1];
            courseId = cell.Text;
        }
        if (courseId != null)
        {
            DatabaseManager.DeleteData(
                "request_add_course",
                "WHERE sid = " +
                "\'" + Session["sid"] + "\' " +
                "AND cid = \'" + courseId + "\'"
                );
        }

        SetRequestChangesTable();
    }    
    
    protected void btnDeleteRequestDropCourse_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = int.Parse(btn.CommandArgument);
        string courseId = null;
        if (rowIndex < tblRequestChanges.Rows.Count)
        {
            TableCell cell = tblRequestChanges.Rows[rowIndex].Cells[1];
            courseId = cell.Text;
        }
        if (courseId != null)
        {
            DatabaseManager.DeleteData(
                "request_drop_course",
                "WHERE sid = " +
                "\'" + Session["sid"] + "\' " +
                "AND cid = \'" + courseId + "\'"
                );
        }

        SetRequestChangesTable();
    }

    protected void btnDeleteRequestChangeSection_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        int rowIndex = int.Parse(btn.CommandArgument);
        string courseId = null;
        if (rowIndex < tblRequestChanges.Rows.Count)
        {
            TableCell cell = tblRequestChanges.Rows[rowIndex].Cells[1];
            courseId = cell.Text;
        }
        if (courseId != null)
        {
            DatabaseManager.DeleteData(
                "request_change_section",
                "WHERE sid = " +
                "\'" + Session["sid"] + "\' " +
                "AND cid = \'" + courseId + "\'"
                );
        }
        PopulateCourseCodeListing();
        SetRequestChangesTable();
    }


    /*Add course pop up windows */
    private void PopulateCourseCodeListing()
    {
        //get student details
        DataSet studentSet = DatabaseManager.GetRecord(
               "student",
               new List<string> { "program", "major" },
               "WHERE sid = \'" + Session["sid"] + "\'"
           );

        DataTable dt = studentSet.Tables[0];
        string program = null;
        string major = null;
        foreach (DataRow row in dt.Rows)
        {
            program = row["program"].ToString();
            major = row["major"].ToString();
        }

        //get course data
        DataSet courseSet = DatabaseManager.GetRecord(
               "course",
               new List<string> {
                    "course.cid",
                    "name",
                    "available",
                    "credit_hours",
                    "price"
               },
               "FULL JOIN course_major AS cm ON course.cid = cm.cid  " +
               "WHERE program = \'" + program + "\' " +
               "AND (major = \'" + major + "\'" + " OR major = \'NONE\') " +
               "AND available = '1' " +
               "AND course.cid NOT IN (SELECT cid FROM student_taken_course WHERE sid = \'" + Session["sid"] + "\'" + " AND status != 'FAIL') " +
               "AND course.cid NOT IN (SELECT cid FROM request_add_course WHERE status != 'NOT APPROVE' AND sid = \'" + Session["sid"] + "\') "
           );
        dt = courseSet.Tables[0];
        List<string> course = new List<string>();
        List<string> courseCode = new List<string>();
        foreach (DataRow row in dt.Rows)
        {
            course.Add(row["cid"].ToString() + ":" + row["name"].ToString());
            courseCode.Add(row["cid"].ToString());
        }
        UIComponentGenerator.PopulateDropDownList(ddlCourseCodeListing, course, courseCode);
        PopulateCourseSection(courseCode[0]);
    }

    private void PopulateCourseSection(string courseCode)
    {
        //get student details
        DataSet studentSet = DatabaseManager.GetRecord(
               "student",
               new List<string> { "program" },
               "WHERE sid = \'" + Session["sid"] + "\'"
           );

        DataTable dt = studentSet.Tables[0];
        string program = null;
        foreach (DataRow row in dt.Rows)
        {
            program = row["program"].ToString();
        }


        DataSet courseSet = DatabaseManager.GetDistinctRecord(
               "section",
               new List<string> {
                    "name",
                    "sid"
               },
               "WHERE cid = \'" + courseCode + "\' " +
               "AND current_enroll < max_enroll " +
               "AND program = \'" + program + "\' " +
               "AND semester IN (SELECT semester FROM current_semester);"
           );
        dt = courseSet.Tables[0];
        List<string> sectionName = new List<string>();
        List<string> sectionId = new List<string>();
        foreach (DataRow row in dt.Rows)
        {
            sectionName.Add(row["name"].ToString());
            sectionId.Add(row["sid"].ToString());
        }
        UIComponentGenerator.PopulateDropDownList(ddlCourseSection, sectionName, sectionId);
    }

    private void SetPreRequisiteTable(string courseCode)
    {
        //Add header
        if (tblPreRequisite.Rows.Count == 0)
        {
            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell cellCourseCodeHeader = new TableHeaderCell { Text = "Course Code" };
            TableHeaderCell cellCourseNameHeader = new TableHeaderCell { Text = "Course Name" };
            TableHeaderCell cellMeetsPrerequisitesHeader = new TableHeaderCell { Text = "Meets Pre-requisites" };
            headerRow.Cells.Add(cellCourseCodeHeader);
            headerRow.Cells.Add(cellCourseNameHeader);
            headerRow.Cells.Add(cellMeetsPrerequisitesHeader);
            tblPreRequisite.Rows.Add(headerRow);
        }
        //get pre requisit course
        DataSet courseDataSet = DatabaseManager.GetRecord(
                "course",
                new List<string> {
                    "cid",
                    "name"
                },
                "WHERE cid IN (" +
                "SELECT prerequisite FROM course FULL JOIN course_prerequisite AS cp " +
                "ON course.cid = cp.cid " +
                "WHERE course.cid = \'" + courseCode + "\')"
            );
        if (courseDataSet != null)
        {
            DataTable courseDataTable = courseDataSet.Tables[0];
            foreach (DataRow row in courseDataTable.Rows)
            {
                TableRow tbRow = new TableRow();
                TableCell cellCourseCode = new TableCell { Text = row["cid"].ToString() };
                TableCell cellCourseName = new TableCell { Text = row["name"].ToString() };

                TableCell cellMeetsPrerequisites = null;

                //Get student taken cours
                DataSet takenCourseDataSet = DatabaseManager.GetRecord(
                    "student_taken_course",
                    new List<string> {
                    "cid"
                    },
                    "WHERE sid = \'" + Session["sid"] + "\' " +
                    "AND cid IN ( " +
                    "SELECT prerequisite FROM course " +
                    "FULL JOIN course_prerequisite AS cp ON course.cid = cp.cid " +
                    "WHERE course.cid = \'" + courseCode + "\' " +
                    "AND status = 'COMPLETE')"
                );

                if (takenCourseDataSet != null)
                {
                    DataTable dt = takenCourseDataSet.Tables[0];
                    foreach (DataRow programRow in dt.Rows)
                    {
                        if (row["cid"] == programRow["cid"])
                        {
                            cellMeetsPrerequisites = new TableCell { Text = "Satisfied", ForeColor = System.Drawing.Color.Green };
                            break;
                        }
                        else
                        {
                            cellMeetsPrerequisites = new TableCell { Text = "Unsatisfied", ForeColor = System.Drawing.Color.Red };
                        }
                    }
                    if (dt.Rows.Count == 0)
                    {
                        cellMeetsPrerequisites = new TableCell { Text = "Unsatisfied", ForeColor = System.Drawing.Color.Red };
                    }
                }
                else
                {
                    cellMeetsPrerequisites = new TableCell { Text = "Unsatisfied", ForeColor = System.Drawing.Color.Red };
                }
                tbRow.Cells.Add(cellCourseCode);
                tbRow.Cells.Add(cellCourseName);
                tbRow.Cells.Add(cellMeetsPrerequisites);
                tblPreRequisite.Rows.Add(tbRow);
            }
        }
        if (tblPreRequisite.Rows.Count == 1)
        {
            TableRow tbRow = new TableRow();
            TableCell cellCourseCode = new TableCell { Text = "None" };
            TableCell cellCourseName = new TableCell { Text = "None" };
            TableCell cellMeetsPrerequisites = new TableCell { Text = "Satisfied", ForeColor = System.Drawing.Color.Green };
            tbRow.Cells.Add(cellCourseCode);
            tbRow.Cells.Add(cellCourseName);
            tbRow.Cells.Add(cellMeetsPrerequisites);
            tblPreRequisite.Rows.Add(tbRow);

        }
    }

    protected void ddlCourseCodeListing_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateCourseSection(ddlCourseCodeListing.SelectedValue);
        SetPreRequisiteTable(ddlCourseCodeListing.SelectedValue);
    }

    protected void btnExitAddCourseWindow_Click(object sender, EventArgs e)
    {
        addCoursePopUpWindow.Style["display"] = "none";
    }

    protected void btnAddCourse_Click1(object sender, EventArgs e)
    {
        SetPreRequisiteTable(ddlCourseCodeListing.SelectedValue);
        lblErrorMessage.Text = "";
        bool preRequisiteMeet = PreRequisiteMeetValidate();
        bool courseAvailable = CourseAvailableValidate();
        bool creidtHoursMeet = AchieveMaxCreidtHoursValidate();
        bool timeTabletNoCrash = TimeTableCrashCheckingValidate();

        if (preRequisiteMeet && courseAvailable && creidtHoursMeet && timeTabletNoCrash)
        {
            DatabaseManager.InsertData(
                "request_add_course",
                new List<string> { "sid", "cid" , "section_id", "reason", "status"},
                new List<object> { Session["sid"], ddlCourseCodeListing.SelectedValue, ddlCourseSection.SelectedValue, txtAddCourseReason.Text, "HOLD" }
                );
            addCoursePopUpWindow.Style["display"] = "none";
            SetRequestChangesTable();
        }
    }

    private bool PreRequisiteMeetValidate()
    {
        bool preRequisiteMeet = true;
        foreach (TableRow row in tblPreRequisite.Rows)
        {
            if (row.Cells[2].Text == "Unsatisfied")
            {
                preRequisiteMeet = false;
            }
        }
        if (preRequisiteMeet == true)
        {
            return true;
        }
        else
        {
            lblErrorMessage.Text += "Pre Requisite Not Meet<br>";
            return false;
        }
    }

    private bool CourseAvailableValidate()
    {
        if (ddlCourseSection.Items.Count == 0)
        {
            lblErrorMessage.Text += "No course section are available<br>";
            return false;
        }
        else
        {
            ; return true;
        }
    }

    private bool TimeTableCrashCheckingValidate()
    {
        //get course credit hour
        DataSet dataSet = DatabaseManager.GetRecord(
               "course",
               new List<string> { "time" },
               "INNER JOIN section AS s ON course.cid = s.cid " +
               "INNER JOIN class AS c ON s.sid = c.sid " +
               "WHERE course.cid IN (" +
               "SELECT cid FROM student_taken_course WHERE sid = \'" + Session["sid"] + "\' " +
               "AND status = 'ADD') " +
               "AND s.sid IN (" +
               "SELECT section_id FROM student_taken_course WHERE sid = \'" + Session["sid"] + "\' " +
               "AND status = 'ADD')"
           );
        DataTable dt = null;
        List<int> timeSelected = new List<int> { };
        if (dataSet != null)
        {
            dt = dataSet.Tables[0];
        }
        else
        {
            Debug.WriteLine("TimeTableCrashCheckingValidate: get course credit hour dataset is null");
            return false;
        }
        if (dt != null)
        {
            foreach (DataRow row in dt.Rows)
            {
                timeSelected.Add(int.Parse(row["time"].ToString()));
            }
        }
        else
        {
            Debug.WriteLine("TimeTableCrashCheckingValidate: get course credit hour datatable is null");
            return false;
        }
        //get current course time
        dataSet = DatabaseManager.GetRecord(
               "course",
               new List<string> { "time" },
               "INNER JOIN section AS s ON course.cid = s.cid " +
               "INNER JOIN class AS c ON s.sid = c.sid " +
               "WHERE course.cid = \'" + ddlCourseCodeListing.SelectedValue + "\' " +
               "AND s.sid = \'" + ddlCourseSection.SelectedValue + "\'"
           );
        dt = null;
        if (dataSet != null)
        {
            dt = dataSet.Tables[0];
        }
        else
        {
            Debug.WriteLine("TimeTableCrashCheckingValidate: get current course time dataset is null");
            return false;
        }
        if (dt != null)
        {
            bool crash = false;
            foreach (DataRow row in dt.Rows)
            {
                foreach (int time in timeSelected)
                {
                    if (int.Parse(row["time"].ToString()) == time)
                    {
                        crash = true;
                        break;
                    }
                }
                if (crash)
                {
                    lblErrorMessage.Text += "Time table crash<br>";
                    return false;
                }
            }

        }
        return true;
    }

    private bool AchieveMaxCreidtHoursValidate()
    {
        int currentCreditHours = 0;
        foreach (TableRow row in tblCurrentEnrolledCourse.Rows)
        {
            try
            {
                currentCreditHours += int.Parse(row.Cells[3].Text);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
            }
        }
        //get course credit hour
        DataSet dataSet = DatabaseManager.GetRecord(
               "course",
               new List<string> { "credit_hours" },
               "WHERE cid = \'" + ddlCourseCodeListing.SelectedValue + "\'"
           );
        DataTable dt = null;
        if (dataSet != null)
        {
            dt = dataSet.Tables[0];
        }
        if (dt != null)
        {
            string currentSellectCreditHour = null;
            foreach (DataRow row in dt.Rows)
            {
                currentSellectCreditHour = row["credit_hours"].ToString();
            }
            try
            {
                currentCreditHours += int.Parse(currentSellectCreditHour);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        //get this sem available mex credit hour
        dataSet = DatabaseManager.GetRecord(
               "current_semester",
               new List<string> { "credit_hour" }
           );
        dt = null;
        if (dataSet != null)
        {
            dt = dataSet.Tables[0];
        }
        int maxCreditHour = 0;
        if (dt != null)
        {
            string maxCreditHourInString = null;
            foreach (DataRow row in dt.Rows)
            {
                maxCreditHourInString = row["credit_hour"].ToString();
            }
            try
            {
                maxCreditHour = int.Parse(maxCreditHourInString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        //verification
        if (currentCreditHours > maxCreditHour)
        {
            lblErrorMessage.Text += "You have met the maximum available credit hours<br>";
            return false;
        }
        return true;
    }

    protected void OperationReasonLengthValidate(object source, ServerValidateEventArgs args)
    {
        string userInput = txtAddCourseReason.Text;
        int maxLength = 1000;
        if(userInput.Length > maxLength)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }

    /*Operation course pop up window*/
    protected void btnExitOperatingWindoiw_Click(object sender, EventArgs e)
    {
        selectOperationpopUpWindow.Style["display"] = "none";
    }

    protected void btnChangeSection_Click(object sender, EventArgs e)
    {

    }

    protected void btnDropCourse_Click(object sender, EventArgs e)
    {
        PopulateDropCourseCodeListing();
        txtDropCourseReason.Text = "";
        selectOperationpopUpWindow.Style["display"] = "none";
        dropCoursePopUpWindow.Style["display"] = "flex";
    }

    /*Drop course windows*/
    private void PopulateDropCourseCodeListing()
    {
        //get student details
        DataSet studentSet = DatabaseManager.GetRecord(
               "student",
               new List<string> { "program", "major" },
               "WHERE sid = \'" + Session["sid"] + "\'"
           );

        DataTable dt = studentSet.Tables[0];
        string program = null;
        string major = null;
        foreach (DataRow row in dt.Rows)
        {
            program = row["program"].ToString();
            major = row["major"].ToString();
        }

        //get course data
        DataSet courseSet = DatabaseManager.GetRecord(
               "student_taken_course",
               new List<string> {
                    "student_taken_course.cid",
                    "name"
               },
               "INNER JOIN course AS c ON student_taken_course.cid = c.cid " +
               "WHERE sid = \'" + Session["sid"] + "\' " +
               "AND status = 'TAKEN' " +
               "AND student_taken_course.cid NOT IN (SELECT cid FROM request_add_course WHERE status != 'NOT APPROVE' AND sid = \'" + Session["sid"]+ "\') " +
               "AND student_taken_course.cid NOT IN (SELECT cid FROM request_drop_course WHERE status != 'NOT APPROVE' AND sid = \'" + Session["sid"]+ "\') " +
               "AND student_taken_course.cid NOT IN (SELECT cid FROM request_change_section WHERE status != 'NOT APPROVE' AND sid = \'" + Session["sid"] + "\') "
           );
        dt = courseSet.Tables[0];
        List<string> course = new List<string>();
        List<string> courseCode = new List<string>();
        foreach (DataRow row in dt.Rows)
        {
            course.Add(row["cid"].ToString() + ":" + row["name"].ToString());
            courseCode.Add(row["cid"].ToString());
        }
        UIComponentGenerator.PopulateDropDownList(ddlDropCourseListing, course, courseCode);
    }

    protected void btnDropCourseApply_Click(object sender, EventArgs e)
    {
        lblErrorMessage.Text = "";

        DatabaseManager.InsertData(
            "request_drop_course",
            new List<string> { "sid", "cid", "reason", "status" },
            new List<object> { Session["sid"], ddlDropCourseListing.SelectedValue, txtDropCourseReason.Text, "HOLD" }
            );
        dropCoursePopUpWindow.Style["display"] = "none";
        SetCurrentEnrolledCourseTable();
        SetRequestChangesTable();
    }

    protected void btnExitDropCourseWindow_Click(object sender, EventArgs e)
    {
        dropCoursePopUpWindow.Style["display"] = "none";
    }

    /*Change section windows*/

    /*bottom btn bar*/
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("StudentHomePage.aspx");
    }

    protected void btnRequestToApprove_Click(object sender, EventArgs e)
    {
       /* DatabaseManager.UpdateData(
          "student_taken_course",
          new List<string> { "status" },
          new List<object> { "TAKEN" },
          "WHERE status = 'ADD' " +
          "AND sid = \'" + Session["sid"] + "\'"
          );*/
        Response.Redirect("StudentHomePage.aspx");
    }





}