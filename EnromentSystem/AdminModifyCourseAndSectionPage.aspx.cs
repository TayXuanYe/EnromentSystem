using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

public partial class AdminModifyCourseAndSectionPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string courseId = Request.QueryString["course"];
            string program = Request.QueryString["program"];
            //set course details
            SetCourseDetials(courseId, program);
            //set gvPrerequisite 
            DataTable preCourse = new DataTable();
            preCourse.Columns.Add("cid", typeof(string));
            preCourse.Columns.Add("name", typeof(string));
            preCourse = ReadPrerequisite(courseId);
            gvPrerequisite.DataSource = preCourse;
            gvPrerequisite.DataBind();
            Session["preCourseAdded"] = preCourse;
            PopulatePrerequisite(program, lblMajor.Text, preCourse);

            //set gvSectionInfo
            List<Section> sections = ReadCurrenSection(courseId, program);
            Session["sectionAdded"] = sections;
            SetSectionInfoTable(sections);
        }
    }
    //course part
    private void SetCourseDetials(string courseId, string program)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "course",
            new List<string> { "major", "name", "credit_hours", "price", "available" },
            $@"FULL JOIN course_major AS cm ON course.cid = cm.cid WHERE cm.cid = '{courseId}' AND program = '{program}'"
            );

        if (dataSet != null)
        {
            lblProgram.Text = program;
            lblCourseId.Text = courseId;
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                lblMajor.Text = row["major"].ToString();
                lblCourseName.Text = row["name"].ToString();
                ddlCreditHours.SelectedValue = row["credit_hours"].ToString();
                txtPrice.Text = row["price"].ToString();
                ddlAvailable.SelectedValue = row["available"].ToString();
            }
        }
    }

    private DataTable ReadPrerequisite(string courseId)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "course",
            new List<string> { "cid", "name" },
            $@"WHERE cid IN (SELECT prerequisite FROM course_prerequisite WHERE cid = '{courseId}')"
            );
        DataTable dataTable = dataSet.Tables[0];
        return dataTable;
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
        if (dataSet != null)
        {
            List<string> value = new List<string>();
            List<string> text = new List<string>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
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
        Session["preCourseAdded"] = dataTable;
        PopulatePrerequisite(lblProgram.Text, lblMajor.Text, dataTable);
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
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                courseName = row["name"].ToString();
            }
        }
        displayValue.Rows.Add(ddlPrerequisite.SelectedValue, courseName);
        gvPrerequisite.DataSource = displayValue;
        gvPrerequisite.DataBind();
        Session["preCourseAdded"] = displayValue;
        PopulatePrerequisite(lblProgram.Text, lblMajor.Text, displayValue);
    }
    //Section
    private List<Section> ReadCurrenSection(string courseId, string program)
    {
        DataSet sectionData = DatabaseManager.GetRecord(
            "section AS s",
            new List<string> { "s.name", "sid", "semester", "max_enroll", "available" },
            $@"JOIN course ON s.cid = course.cid WHERE s.cid = '{courseId}' AND program = '{program}'"
            );
        List<Section> sections = new List<Section>();
        foreach (DataRow row in sectionData.Tables[0].Rows)
        {
            Section section = new Section(row["name"].ToString(), row["sid"].ToString(), row["semester"].ToString(), courseId,
                int.Parse(row["max_enroll"].ToString()));
            DataSet classData = DatabaseManager.GetRecord(
                "class",
                new List<string> { "id", "time", "class_room", "lid", "type" },
                $@"WHERE sid = '{row["sid"].ToString()}'"
                );
            foreach (DataRow classRow in classData.Tables[0].Rows)
            {
                if (classRow["type"].ToString() == "LECTURE")
                {
                    Class newClass = new Class(classRow["id"].ToString(), classRow["type"].ToString(), classRow["class_room"].ToString(),
                        classRow["lid"].ToString(), int.Parse(classRow["time"].ToString()));
                    section.lectureClass.Add(newClass);
                }
                else if (classRow["type"].ToString() == "PRACTICAL")
                {
                    Class newClass = new Class(classRow["id"].ToString(), classRow["type"].ToString(), classRow["class_room"].ToString(),
                        classRow["lid"].ToString(), int.Parse(classRow["time"].ToString()));
                    section.practicalClass.Add(newClass);
                }
            }
            sections.Add(section);
        }
        return sections;
    }

    private void SetSectionInfoTable(List<Section> sectionAdded)
    {
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("sid", typeof(string));
        dataTable.Columns.Add("name", typeof(string));
        dataTable.Columns.Add("semester", typeof(string));
        for (int i = 0; i < sectionAdded.Count; i++)
        {
            dataTable.Rows.Add(sectionAdded[i].sectionId, sectionAdded[i].name, sectionAdded[i].semester);
        }
        gvSectionInfo.DataSource = dataTable;
        gvSectionInfo.DataBind();
    }

    protected void btnAddSection_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate("section");
        Page.Validate("");
        if (Page.IsValid)
        {
            classWindows.Style["display"] = "flex";
            lblWarningLectureClassLecturer.Style["display"] = "none";
            lblWarningPracticalClassLecturer.Style["display"] = "none";
            lblWarningNoClassAdded.Style["display"] = "none";
            //get current semester
            DataSet semesterData = DatabaseManager.GetRecord(
                "current_semester",
                new List<string> { "semester" }
                );
            string currentSemester = "";
            if (semesterData != null)
            {
                DataRow row = semesterData.Tables[0].Rows[0];
                currentSemester = row["semester"].ToString();
            }

            string name = txtSectionName.Text.ToUpper();
            string sectionId = $"{lblCourseId.Text}-{currentSemester}-{name}";

            lblClassWindowsSectionId.Text = sectionId;
            PopulateLecturer();
            string courseId = lblCourseId.Text.ToUpper();
            //set new section session
            Session["newSection"] = null;
            Section newSection = new Section(name, currentSemester, courseId);
            Session["newSection"] = newSection;
            SetClassTimetable(newSection.GetLectureClassTable(), newSection.GetPracticalClassTable());
        }
    }

    protected void CheckSectionIsExist_ServerValidate(object source, ServerValidateEventArgs args)
    {
        //get current semester
        DataSet semesterData = DatabaseManager.GetRecord(
            "current_semester",
            new List<string> { "semester" }
            );
        string currentSemester = "";
        if (semesterData != null)
        {
            DataRow row = semesterData.Tables[0].Rows[0];
            currentSemester = row["semester"].ToString();
        }

        string name = args.Value;
        string sectionId = $"{lblCourseId.Text}-{currentSemester}-{name}";
        List<Section> sections = Session["sectionAdded"] as List<Section>;
        for (int i = 0; i < sections.Count; i++)
        {
            Section section = sections[i];
            if (section.sectionId == sectionId)
            {
                args.IsValid = false;
                return;
            }
        }
        args.IsValid = true;
    }

    protected void gvSectionInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sectionId = gvSectionInfo.SelectedRow.Cells[0].Text;
        List<Section> sectionAdded = Session["sectionAdded"] as List<Section>;
        for (int i = 0; i < sectionAdded.Count; i++)
        {
            if (sectionAdded[i].sectionId == sectionId)
            {
                sectionAdded.Remove(sectionAdded[i]);
            }
        }
        Session["sectionAdded"] = sectionAdded;
        SetSectionInfoTable(sectionAdded);
    }

    protected void gvSectionInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            string sectionId = e.CommandArgument.ToString();
            List<Section> sectionAdded = Session["sectionAdded"] as List<Section>;
            for (int i = 0; i < sectionAdded.Count; i++)
            {
                if (sectionAdded[i].sectionId == sectionId)
                {
                    classWindows.Style["display"] = "flex";
                    lblClassWindowsSectionId.Text = sectionAdded[i].sectionId;
                    PopulateLecturer();
                    if (sectionAdded[i].lectureClass.Count > 0)
                    {
                        ddlLectureClassLecturer.SelectedValue = sectionAdded[i].lectureClass[0].lecturerId;
                    }
                    if (sectionAdded[i].practicalClass.Count > 0)
                    {
                        ddlPracticalClassLecturer.SelectedValue = sectionAdded[i].practicalClass[0].lecturerId;
                    }
                    txtMaxEnroll.Text = sectionAdded[i].maxEnrollAllow.ToString();
                    Session["newSection"] = sectionAdded[i];
                    SetClassTimetable(sectionAdded[i].GetLectureClassTable(), sectionAdded[i].GetPracticalClassTable());
                    break;
                }
            }
        }
    }
    // class select pop up windows
    private void SetClassTimetable(DataTable lectureClass, DataTable practicalClass)
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

        DataRow monDay = dt.NewRow();
        monDay["day"] = "Mon";

        DataRow tueDay = dt.NewRow();
        tueDay["day"] = "Tue";

        DataRow wedDay = dt.NewRow();
        wedDay["day"] = "Wed";

        DataRow thuDay = dt.NewRow();
        thuDay["day"] = "Thu";

        DataRow friDay = dt.NewRow();
        friDay["day"] = "Fri";

        DataRow satDay = dt.NewRow();
        satDay["day"] = "Sat";

        DataRow sunDay = dt.NewRow();
        sunDay["day"] = "Sun";
        foreach (DataRow row in lectureClass.Rows)
        {
            string input = row["timeIndex"].ToString();
            int index = int.Parse(input) / 15;
            if (int.Parse(input) % 15 == 0)
            {
                index--;
            }
            switch (index)
            {
                case 0:
                    monDay = SetDailyClass(monDay, row, "LECTURE", int.Parse(input) % 15);
                    break;

                case 1:
                    tueDay = SetDailyClass(tueDay, row, "LECTURE", int.Parse(input) % 15);
                    break;

                case 2:
                    wedDay = SetDailyClass(wedDay, row, "LECTURE", int.Parse(input) % 15);
                    break;

                case 3:
                    thuDay = SetDailyClass(thuDay, row, "LECTURE", int.Parse(input) % 15);
                    break;

                case 4:
                    friDay = SetDailyClass(friDay, row, "LECTURE", int.Parse(input) % 15);
                    break;

                case 5:
                    satDay = SetDailyClass(satDay, row, "LECTURE", int.Parse(input) % 15);
                    break;

                case 6:
                    sunDay = SetDailyClass(sunDay, row, "LECTURE", int.Parse(input) % 15);
                    break;

            }
        }

        foreach (DataRow row in practicalClass.Rows)
        {
            string input = row["timeIndex"].ToString();
            switch (int.Parse(input) / 16)
            {
                case 0:
                    monDay = SetDailyClass(monDay, row, "PRACTICAL", int.Parse(input) % 15);
                    break;

                case 1:
                    tueDay = SetDailyClass(tueDay, row, "PRACTICAL", int.Parse(input) % 15);
                    break;

                case 2:
                    wedDay = SetDailyClass(wedDay, row, "PRACTICAL", int.Parse(input) % 15);
                    break;

                case 3:
                    thuDay = SetDailyClass(thuDay, row, "PRACTICAL", int.Parse(input) % 15);
                    break;

                case 4:
                    friDay = SetDailyClass(friDay, row, "PRACTICAL", int.Parse(input) % 15);
                    break;

                case 5:
                    satDay = SetDailyClass(satDay, row, "PRACTICAL", int.Parse(input) % 15);
                    break;

                case 6:
                    sunDay = SetDailyClass(sunDay, row, "PRACTICAL", int.Parse(input) % 15);
                    break;

            }
        }
        dt.Rows.Add(monDay);
        dt.Rows.Add(tueDay);
        dt.Rows.Add(wedDay);
        dt.Rows.Add(thuDay);
        dt.Rows.Add(friDay);
        dt.Rows.Add(satDay);
        dt.Rows.Add(sunDay);
        gvClassTimetable.DataSource = dt;
        gvClassTimetable.DataBind();
    }

    private DataRow SetDailyClass(DataRow targetRow, DataRow info, string classType, int timeIndex)
    {
        switch (timeIndex)
        {
            case 1:
                targetRow["0800"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
            case 2:
                targetRow["0900"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
            case 3:
                targetRow["1000"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
            case 4:
                targetRow["1100"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
            case 5:
                targetRow["1200"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
            case 6:
                targetRow["1300"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
            case 7:
                targetRow["1400"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
            case 8:
                targetRow["1500"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
            case 9:
                targetRow["1600"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
            case 10:
                targetRow["1700"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
            case 11:
                targetRow["1800"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
            case 12:
                targetRow["1900"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
            case 13:
                targetRow["2000"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
            case 14:
                targetRow["2100"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
            case 0:
                targetRow["2200"] = $"{info["classRoom"].ToString()}    {classType}";
                return targetRow;
        }
        return null;
    }

    private void PopulateLecturer()
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "lecture",
            new List<string> { "name", "lid" }
            );
        if (dataSet != null)
        {
            List<string> value = new List<string>();
            value.Add("None");
            List<string> text = new List<string>();
            text.Add("None");
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                value.Add(row["lid"].ToString());
                text.Add(row["name"].ToString());
            }
            UIComponentGenerator.PopulateDropDownList(ddlLectureClassLecturer, text, value);
            UIComponentGenerator.PopulateDropDownList(ddlPracticalClassLecturer, text, value);
        }
    }

    protected void btnAddLectureClass_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlLectureClassLecturer.SelectedValue == "None")
        {
            lblWarningLectureClassLecturer.Style["display"] = "inline";
        }
        else
        {
            lblWarningLectureClassLecturer.Style["display"] = "none";
            lblSelectingClass.Text = "Lecture Class";
            selectClassWindow.Style["display"] = "flex";
            Section newSection = Session["newSection"] as Section;
            SetSelectTable(newSection.GetLectureClassTable(), newSection.GetPracticalClassTable());
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
            lblSelectingClass.Text = "Practical Class";
            selectClassWindow.Style["display"] = "flex";
            Section newSection = Session["newSection"] as Section;
            SetSelectTable(newSection.GetLectureClassTable(), newSection.GetPracticalClassTable());
        }
    }

    protected void btnAddClass_Click(object sender, EventArgs e)
    {
        Section newSection = Session["newSection"] as Section;
        if (newSection.lectureClass.Count == 0 && newSection.practicalClass.Count == 0)
        {
            lblWarningNoClassAdded.Style["display"] = "inline";
        }
        else
        {
            newSection.maxEnrollAllow = int.Parse(txtMaxEnroll.Text);
            lblWarningNoClassAdded.Style["display"] = "none";
            List<Section> sectionAdded = Session["sectionAdded"] as List<Section>;
            for (int i = 0; i < sectionAdded.Count; i++)
            {
                if (sectionAdded[i].sectionId == lblClassWindowsSectionId.Text)
                {
                    sectionAdded[i] = newSection;
                    Session["sectionAdded"] = sectionAdded;
                    SetSectionInfoTable(sectionAdded);
                    classWindows.Style["display"] = "none";
                    return;
                }
            }
            sectionAdded.Add(newSection);
            Session["sectionAdded"] = sectionAdded;
            SetSectionInfoTable(sectionAdded);
            classWindows.Style["display"] = "none";
        }
    }

    protected void btnCancelAddClass_Click(object sender, EventArgs e)
    {
        classWindows.Style["display"] = "none";
    }

    protected void ddlPracticalClassLecturer_SelectedIndexChanged(object sender, EventArgs e)
    {
        Section newSection = Session["newSection"] as Section;
        newSection.practicalClass.Clear();
        Session["newSection"] = newSection;
        SetClassTimetable(newSection.GetLectureClassTable(), newSection.GetPracticalClassTable());
    }

    protected void ddlLectureClassLecturer_SelectedIndexChanged(object sender, EventArgs e)
    {
        Section newSection = Session["newSection"] as Section;
        newSection.lectureClass.Clear();
        Session["newSection"] = newSection;
        SetClassTimetable(newSection.GetLectureClassTable(), newSection.GetPracticalClassTable());
    }
    //selectClassWindow
    private void SetSelectTable(DataTable lectureClass, DataTable practicalClass)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("day", typeof(string));
        dt.Rows.Add("Mon");
        dt.Rows.Add("Tue");
        dt.Rows.Add("Wed");
        dt.Rows.Add("Thu");
        dt.Rows.Add("Fri");
        dt.Rows.Add("Sat");
        dt.Rows.Add("Sun");
        gvSelectClass.DataSource = dt;
        gvSelectClass.DataBind();
        ResetSelectTable();

        //get lecture all time
        string lecturerId = "";
        if (lblSelectingClass.Text == "Lecture Class")
        {
            lecturerId = ddlLectureClassLecturer.SelectedValue;
        }
        else
        {
            lecturerId = ddlPracticalClassLecturer.SelectedValue;
        }
        DataSet lectureTime = DatabaseManager.GetRecord(
            "class",
            new List<string> { "time" },
            $@"WHERE lid = '{lecturerId}' AND sid NOT IN (SELECT sid FROM section WHERE cid = '{lblCourseId.Text}')"
            );
        HashSet<int> lecturerUseTime = new HashSet<int>();
        if (lectureTime != null)
        {
            foreach (DataRow row in lectureTime.Tables[0].Rows)
            {
                lecturerUseTime.Add(int.Parse(row["time"].ToString()));
            }
        }
        //set lecture not available time
        for (int i = 0; i < gvSelectClass.Rows.Count; i++)
        {
            GridViewRow row = gvSelectClass.Rows[i];
            CheckBox chkSelect08 = (CheckBox)row.FindControl("chkSelect08");
            CheckBox chkSelect09 = (CheckBox)row.FindControl("chkSelect09");
            CheckBox chkSelect10 = (CheckBox)row.FindControl("chkSelect10");
            CheckBox chkSelect11 = (CheckBox)row.FindControl("chkSelect11");
            CheckBox chkSelect12 = (CheckBox)row.FindControl("chkSelect12");
            CheckBox chkSelect13 = (CheckBox)row.FindControl("chkSelect13");
            CheckBox chkSelect14 = (CheckBox)row.FindControl("chkSelect14");
            CheckBox chkSelect15 = (CheckBox)row.FindControl("chkSelect15");
            CheckBox chkSelect16 = (CheckBox)row.FindControl("chkSelect16");
            CheckBox chkSelect17 = (CheckBox)row.FindControl("chkSelect17");
            CheckBox chkSelect18 = (CheckBox)row.FindControl("chkSelect18");
            CheckBox chkSelect19 = (CheckBox)row.FindControl("chkSelect19");
            CheckBox chkSelect20 = (CheckBox)row.FindControl("chkSelect20");
            CheckBox chkSelect21 = (CheckBox)row.FindControl("chkSelect21");
            CheckBox chkSelect22 = (CheckBox)row.FindControl("chkSelect22");

            if (lecturerUseTime.Contains(i * 15 + 1))
            {
                DisableUnavailableCheckBox(chkSelect08);
            }
            if (lecturerUseTime.Contains(i * 15 + 2))
            {
                DisableUnavailableCheckBox(chkSelect09);
            }
            if (lecturerUseTime.Contains(i * 15 + 3))
            {
                DisableUnavailableCheckBox(chkSelect10);
            }
            if (lecturerUseTime.Contains(i * 15 + 4))
            {
                DisableUnavailableCheckBox(chkSelect11);
            }
            if (lecturerUseTime.Contains(i * 15 + 5))
            {
                DisableUnavailableCheckBox(chkSelect12);
            }
            if (lecturerUseTime.Contains(i * 15 + 6))
            {
                DisableUnavailableCheckBox(chkSelect13);
            }
            if (lecturerUseTime.Contains(i * 15 + 7))
            {
                DisableUnavailableCheckBox(chkSelect14);
            }
            if (lecturerUseTime.Contains(i * 15 + 8))
            {
                DisableUnavailableCheckBox(chkSelect15);
            }
            if (lecturerUseTime.Contains(i * 15 + 9))
            {
                DisableUnavailableCheckBox(chkSelect16);
            }
            if (lecturerUseTime.Contains(i * 15 + 10))
            {
                DisableUnavailableCheckBox(chkSelect17);
            }
            if (lecturerUseTime.Contains(i * 15 + 11))
            {
                DisableUnavailableCheckBox(chkSelect18);
            }
            if (lecturerUseTime.Contains(i * 15 + 12))
            {
                DisableUnavailableCheckBox(chkSelect19);
            }
            if (lecturerUseTime.Contains(i * 15 + 13))
            {
                DisableUnavailableCheckBox(chkSelect20);
            }
            if (lecturerUseTime.Contains(i * 15 + 14))
            {
                DisableUnavailableCheckBox(chkSelect21);
            }
            if (lecturerUseTime.Contains(i * 15 + 15))
            {
                DisableUnavailableCheckBox(chkSelect22);
            }
        }

        //get class time
        HashSet<int> lectureClassTime = new HashSet<int>();
        foreach (DataRow row in lectureClass.Rows)
        {
            lectureClassTime.Add(int.Parse(row["timeIndex"].ToString()));
        }

        HashSet<int> practicalClassTime = new HashSet<int>();
        foreach (DataRow row in practicalClass.Rows)
        {
            practicalClassTime.Add(int.Parse(row["timeIndex"].ToString()));
        }

        //set check box curren class and disable the check box for another class
        if (lblSelectingClass.Text == "Lecture Class")
        {
            for (int i = 0; i < gvSelectClass.Rows.Count; i++)
            {
                GridViewRow row = gvSelectClass.Rows[i];
                CheckBox chkSelect08 = (CheckBox)row.FindControl("chkSelect08");
                CheckBox chkSelect09 = (CheckBox)row.FindControl("chkSelect09");
                CheckBox chkSelect10 = (CheckBox)row.FindControl("chkSelect10");
                CheckBox chkSelect11 = (CheckBox)row.FindControl("chkSelect11");
                CheckBox chkSelect12 = (CheckBox)row.FindControl("chkSelect12");
                CheckBox chkSelect13 = (CheckBox)row.FindControl("chkSelect13");
                CheckBox chkSelect14 = (CheckBox)row.FindControl("chkSelect14");
                CheckBox chkSelect15 = (CheckBox)row.FindControl("chkSelect15");
                CheckBox chkSelect16 = (CheckBox)row.FindControl("chkSelect16");
                CheckBox chkSelect17 = (CheckBox)row.FindControl("chkSelect17");
                CheckBox chkSelect18 = (CheckBox)row.FindControl("chkSelect18");
                CheckBox chkSelect19 = (CheckBox)row.FindControl("chkSelect19");
                CheckBox chkSelect20 = (CheckBox)row.FindControl("chkSelect20");
                CheckBox chkSelect21 = (CheckBox)row.FindControl("chkSelect21");
                CheckBox chkSelect22 = (CheckBox)row.FindControl("chkSelect22");

                //set practical selected class disable
                if (practicalClassTime.Contains(i * 15 + 1))
                {
                    DisableUsedCheckBox(chkSelect08);
                }
                if (practicalClassTime.Contains(i * 15 + 2))
                {
                    DisableUsedCheckBox(chkSelect09);
                }
                if (practicalClassTime.Contains(i * 15 + 3))
                {
                    DisableUsedCheckBox(chkSelect10);
                }
                if (practicalClassTime.Contains(i * 15 + 4))
                {
                    DisableUsedCheckBox(chkSelect11);
                }
                if (practicalClassTime.Contains(i * 15 + 5))
                {
                    DisableUsedCheckBox(chkSelect12);
                }
                if (practicalClassTime.Contains(i * 15 + 6))
                {
                    DisableUsedCheckBox(chkSelect13);
                }
                if (practicalClassTime.Contains(i * 15 + 7))
                {
                    DisableUsedCheckBox(chkSelect14);
                }
                if (practicalClassTime.Contains(i * 15 + 8))
                {
                    DisableUsedCheckBox(chkSelect15);
                }
                if (practicalClassTime.Contains(i * 15 + 9))
                {
                    DisableUsedCheckBox(chkSelect16);
                }
                if (practicalClassTime.Contains(i * 15 + 10))
                {
                    DisableUsedCheckBox(chkSelect17);
                }
                if (practicalClassTime.Contains(i * 15 + 11))
                {
                    DisableUsedCheckBox(chkSelect18);
                }
                if (practicalClassTime.Contains(i * 15 + 12))
                {
                    DisableUsedCheckBox(chkSelect19);
                }
                if (practicalClassTime.Contains(i * 15 + 13))
                {
                    DisableUsedCheckBox(chkSelect20);
                }
                if (practicalClassTime.Contains(i * 15 + 14))
                {
                    DisableUsedCheckBox(chkSelect21);
                }
                if (practicalClassTime.Contains(i * 15 + 15))
                {
                    DisableUsedCheckBox(chkSelect22);
                }
                //set lecturer selected check box checked
                if (lectureClassTime.Contains(i * 15 + 1))
                {
                    CheckCheckBox(chkSelect08);
                }
                if (lectureClassTime.Contains(i * 15 + 2))
                {
                    CheckCheckBox(chkSelect09);
                }
                if (lectureClassTime.Contains(i * 15 + 3))
                {
                    CheckCheckBox(chkSelect10);
                }
                if (lectureClassTime.Contains(i * 15 + 4))
                {
                    CheckCheckBox(chkSelect11);
                }
                if (lectureClassTime.Contains(i * 15 + 5))
                {
                    CheckCheckBox(chkSelect12);
                }
                if (lectureClassTime.Contains(i * 15 + 6))
                {
                    CheckCheckBox(chkSelect13);
                }
                if (lectureClassTime.Contains(i * 15 + 7))
                {
                    CheckCheckBox(chkSelect14);
                }
                if (lectureClassTime.Contains(i * 15 + 8))
                {
                    CheckCheckBox(chkSelect15);
                }
                if (lectureClassTime.Contains(i * 15 + 9))
                {
                    CheckCheckBox(chkSelect16);
                }
                if (lectureClassTime.Contains(i * 15 + 10))
                {
                    CheckCheckBox(chkSelect17);
                }
                if (lectureClassTime.Contains(i * 15 + 11))
                {
                    CheckCheckBox(chkSelect18);
                }
                if (lectureClassTime.Contains(i * 15 + 12))
                {
                    CheckCheckBox(chkSelect19);
                }
                if (lectureClassTime.Contains(i * 15 + 13))
                {
                    CheckCheckBox(chkSelect20);
                }
                if (lectureClassTime.Contains(i * 15 + 14))
                {
                    CheckCheckBox(chkSelect21);
                }
                if (lectureClassTime.Contains(i * 15 + 15))
                {
                    CheckCheckBox(chkSelect22);
                }
            }

        }
        else
        {
            for (int i = 0; i < gvSelectClass.Rows.Count; i++)
            {
                GridViewRow row = gvSelectClass.Rows[i];
                CheckBox chkSelect08 = (CheckBox)row.FindControl("chkSelect08");
                CheckBox chkSelect09 = (CheckBox)row.FindControl("chkSelect09");
                CheckBox chkSelect10 = (CheckBox)row.FindControl("chkSelect10");
                CheckBox chkSelect11 = (CheckBox)row.FindControl("chkSelect11");
                CheckBox chkSelect12 = (CheckBox)row.FindControl("chkSelect12");
                CheckBox chkSelect13 = (CheckBox)row.FindControl("chkSelect13");
                CheckBox chkSelect14 = (CheckBox)row.FindControl("chkSelect14");
                CheckBox chkSelect15 = (CheckBox)row.FindControl("chkSelect15");
                CheckBox chkSelect16 = (CheckBox)row.FindControl("chkSelect16");
                CheckBox chkSelect17 = (CheckBox)row.FindControl("chkSelect17");
                CheckBox chkSelect18 = (CheckBox)row.FindControl("chkSelect18");
                CheckBox chkSelect19 = (CheckBox)row.FindControl("chkSelect19");
                CheckBox chkSelect20 = (CheckBox)row.FindControl("chkSelect20");
                CheckBox chkSelect21 = (CheckBox)row.FindControl("chkSelect21");
                CheckBox chkSelect22 = (CheckBox)row.FindControl("chkSelect22");

                //set lecturer selected class disable
                if (lectureClassTime.Contains(i * 15 + 1))
                {
                    DisableUsedCheckBox(chkSelect08);
                }
                if (lectureClassTime.Contains(i * 15 + 2))
                {
                    DisableUsedCheckBox(chkSelect09);
                }
                if (lectureClassTime.Contains(i * 15 + 3))
                {
                    DisableUsedCheckBox(chkSelect10);
                }
                if (lectureClassTime.Contains(i * 15 + 4))
                {
                    DisableUsedCheckBox(chkSelect11);
                }
                if (lectureClassTime.Contains(i * 15 + 5))
                {
                    DisableUsedCheckBox(chkSelect12);
                }
                if (lectureClassTime.Contains(i * 15 + 6))
                {
                    DisableUsedCheckBox(chkSelect13);
                }
                if (lectureClassTime.Contains(i * 15 + 7))
                {
                    DisableUsedCheckBox(chkSelect14);
                }
                if (lectureClassTime.Contains(i * 15 + 8))
                {
                    DisableUsedCheckBox(chkSelect15);
                }
                if (lectureClassTime.Contains(i * 15 + 9))
                {
                    DisableUsedCheckBox(chkSelect16);
                }
                if (lectureClassTime.Contains(i * 15 + 10))
                {
                    DisableUsedCheckBox(chkSelect17);
                }
                if (lectureClassTime.Contains(i * 15 + 11))
                {
                    DisableUsedCheckBox(chkSelect18);
                }
                if (lectureClassTime.Contains(i * 15 + 12))
                {
                    DisableUsedCheckBox(chkSelect19);
                }
                if (lectureClassTime.Contains(i * 15 + 13))
                {
                    DisableUsedCheckBox(chkSelect20);
                }
                if (lectureClassTime.Contains(i * 15 + 14))
                {
                    DisableUsedCheckBox(chkSelect21);
                }
                if (lectureClassTime.Contains(i * 15 + 15))
                {
                    DisableUsedCheckBox(chkSelect22);
                }
                //set practical selected check box checked
                if (practicalClassTime.Contains(i * 15 + 1))
                {
                    CheckCheckBox(chkSelect08);
                }
                if (practicalClassTime.Contains(i * 15 + 2))
                {
                    CheckCheckBox(chkSelect09);
                }
                if (practicalClassTime.Contains(i * 15 + 3))
                {
                    CheckCheckBox(chkSelect10);
                }
                if (practicalClassTime.Contains(i * 15 + 4))
                {
                    CheckCheckBox(chkSelect11);
                }
                if (practicalClassTime.Contains(i * 15 + 5))
                {
                    CheckCheckBox(chkSelect12);
                }
                if (practicalClassTime.Contains(i * 15 + 6))
                {
                    CheckCheckBox(chkSelect13);
                }
                if (practicalClassTime.Contains(i * 15 + 7))
                {
                    CheckCheckBox(chkSelect14);
                }
                if (practicalClassTime.Contains(i * 15 + 8))
                {
                    CheckCheckBox(chkSelect15);
                }
                if (practicalClassTime.Contains(i * 15 + 9))
                {
                    CheckCheckBox(chkSelect16);
                }
                if (practicalClassTime.Contains(i * 15 + 10))
                {
                    CheckCheckBox(chkSelect17);
                }
                if (practicalClassTime.Contains(i * 15 + 11))
                {
                    CheckCheckBox(chkSelect18);
                }
                if (practicalClassTime.Contains(i * 15 + 12))
                {
                    CheckCheckBox(chkSelect19);
                }
                if (practicalClassTime.Contains(i * 15 + 13))
                {
                    CheckCheckBox(chkSelect20);
                }
                if (practicalClassTime.Contains(i * 15 + 14))
                {
                    CheckCheckBox(chkSelect21);
                }
                if (practicalClassTime.Contains(i * 15 + 15))
                {
                    CheckCheckBox(chkSelect22);
                }
            }
        }
    }

    private void ResetSelectTable()
    {
        for (int i = 0; i < gvSelectClass.Rows.Count; i++)
        {
            GridViewRow row = gvSelectClass.Rows[i];
            CheckBox chkSelect08 = (CheckBox)row.FindControl("chkSelect08");
            CheckBox chkSelect09 = (CheckBox)row.FindControl("chkSelect09");
            CheckBox chkSelect10 = (CheckBox)row.FindControl("chkSelect10");
            CheckBox chkSelect11 = (CheckBox)row.FindControl("chkSelect11");
            CheckBox chkSelect12 = (CheckBox)row.FindControl("chkSelect12");
            CheckBox chkSelect13 = (CheckBox)row.FindControl("chkSelect13");
            CheckBox chkSelect14 = (CheckBox)row.FindControl("chkSelect14");
            CheckBox chkSelect15 = (CheckBox)row.FindControl("chkSelect15");
            CheckBox chkSelect16 = (CheckBox)row.FindControl("chkSelect16");
            CheckBox chkSelect17 = (CheckBox)row.FindControl("chkSelect17");
            CheckBox chkSelect18 = (CheckBox)row.FindControl("chkSelect18");
            CheckBox chkSelect19 = (CheckBox)row.FindControl("chkSelect19");
            CheckBox chkSelect20 = (CheckBox)row.FindControl("chkSelect20");
            CheckBox chkSelect21 = (CheckBox)row.FindControl("chkSelect21");
            CheckBox chkSelect22 = (CheckBox)row.FindControl("chkSelect22");

            chkSelect08.Enabled = true;
            chkSelect08.CssClass = "can-select-checkbox";
            chkSelect08.Checked = false;

            chkSelect09.Enabled = true;
            chkSelect09.CssClass = "can-select-checkbox";
            chkSelect09.Checked = false;

            chkSelect10.Enabled = true;
            chkSelect10.CssClass = "can-select-checkbox";
            chkSelect10.Checked = false;

            chkSelect11.Enabled = true;
            chkSelect11.CssClass = "can-select-checkbox";
            chkSelect11.Checked = false;

            chkSelect12.Enabled = true;
            chkSelect12.CssClass = "can-select-checkbox";
            chkSelect12.Checked = false;

            chkSelect13.Enabled = true;
            chkSelect13.CssClass = "can-select-checkbox";
            chkSelect13.Checked = false;

            chkSelect14.Enabled = true;
            chkSelect14.CssClass = "can-select-checkbox";
            chkSelect14.Checked = false;

            chkSelect15.Enabled = true;
            chkSelect15.CssClass = "can-select-checkbox";
            chkSelect15.Checked = false;

            chkSelect16.Enabled = true;
            chkSelect16.CssClass = "can-select-checkbox";
            chkSelect16.Checked = false;

            chkSelect17.Enabled = true;
            chkSelect17.CssClass = "can-select-checkbox";
            chkSelect17.Checked = false;


            chkSelect18.Enabled = true;
            chkSelect18.CssClass = "can-select-checkbox";
            chkSelect18.Checked = false;

            chkSelect19.Enabled = true;
            chkSelect19.CssClass = "can-select-checkbox";
            chkSelect19.Checked = false;

            chkSelect20.Enabled = true;
            chkSelect20.CssClass = "can-select-checkbox";
            chkSelect20.Checked = false;

            chkSelect21.Enabled = true;
            chkSelect21.CssClass = "can-select-checkbox";
            chkSelect21.Checked = false;

            chkSelect22.Enabled = true;
            chkSelect22.CssClass = "can-select-checkbox";
            chkSelect22.Checked = false;
        }
    }

    private CheckBox DisableUnavailableCheckBox(CheckBox checkBox)
    {
        checkBox.Enabled = false;
        checkBox.CssClass = "unable-select-checkbox";
        return checkBox;
    }

    private CheckBox DisableUsedCheckBox(CheckBox checkBox)
    {
        checkBox.Enabled = false;
        checkBox.CssClass = "have-select-checkbox";
        return checkBox;
    }

    private CheckBox CheckCheckBox(CheckBox checkBox)
    {
        checkBox.Checked = true;
        return checkBox;
    }

    protected void btnContinueSelectClass_Click(object sender, EventArgs e)
    {
        List<int> selectedTime = new List<int>();
        for (int i = 0; i < gvSelectClass.Rows.Count; i++)
        {
            GridViewRow row = gvSelectClass.Rows[i];
            CheckBox chkSelect08 = (CheckBox)row.FindControl("chkSelect08");
            CheckBox chkSelect09 = (CheckBox)row.FindControl("chkSelect09");
            CheckBox chkSelect10 = (CheckBox)row.FindControl("chkSelect10");
            CheckBox chkSelect11 = (CheckBox)row.FindControl("chkSelect11");
            CheckBox chkSelect12 = (CheckBox)row.FindControl("chkSelect12");
            CheckBox chkSelect13 = (CheckBox)row.FindControl("chkSelect13");
            CheckBox chkSelect14 = (CheckBox)row.FindControl("chkSelect14");
            CheckBox chkSelect15 = (CheckBox)row.FindControl("chkSelect15");
            CheckBox chkSelect16 = (CheckBox)row.FindControl("chkSelect16");
            CheckBox chkSelect17 = (CheckBox)row.FindControl("chkSelect17");
            CheckBox chkSelect18 = (CheckBox)row.FindControl("chkSelect18");
            CheckBox chkSelect19 = (CheckBox)row.FindControl("chkSelect19");
            CheckBox chkSelect20 = (CheckBox)row.FindControl("chkSelect20");
            CheckBox chkSelect21 = (CheckBox)row.FindControl("chkSelect21");
            CheckBox chkSelect22 = (CheckBox)row.FindControl("chkSelect22");

            if (chkSelect08.Checked)
            {
                selectedTime.Add(i * 15 + 1);
            }
            if (chkSelect09.Checked)
            {
                selectedTime.Add(i * 15 + 2);
            }
            if (chkSelect10.Checked)
            {
                selectedTime.Add(i * 15 + 3);
            }
            if (chkSelect11.Checked)
            {
                selectedTime.Add(i * 15 + 4);
            }
            if (chkSelect12.Checked)
            {
                selectedTime.Add(i * 15 + 5);
            }
            if (chkSelect13.Checked)
            {
                selectedTime.Add(i * 15 + 6);
            }
            if (chkSelect14.Checked)
            {
                selectedTime.Add(i * 15 + 7);
            }
            if (chkSelect15.Checked)
            {
                selectedTime.Add(i * 15 + 8);
            }
            if (chkSelect16.Checked)
            {
                selectedTime.Add(i * 15 + 9);
            }
            if (chkSelect17.Checked)
            {
                selectedTime.Add(i * 15 + 10);
            }
            if (chkSelect18.Checked)
            {
                selectedTime.Add(i * 15 + 11);
            }
            if (chkSelect19.Checked)
            {
                selectedTime.Add(i * 15 + 12);
            }
            if (chkSelect20.Checked)
            {
                selectedTime.Add(i * 15 + 13);
            }
            if (chkSelect21.Checked)
            {
                selectedTime.Add(i * 15 + 14);
            }
            if (chkSelect22.Checked)
            {
                selectedTime.Add(i * 15 + 15);
            }
        }
        if (selectedTime.Count > 0)
        {
            lblNoTimeSelected.Style["display"] = "none";
            SetAssignClassroom(selectedTime);
            lblAssigningClassType.Text = lblSelectingClass.Text;
            assignClassroomWindow.Style["display"] = "flex";
            selectClassWindow.Style["display"] = "none";
        }
        else
        {
            lblNoTimeSelected.Style["display"] = "inline";
        }
    }

    protected void btnCancelSelectClass_Click(object sender, EventArgs e)
    {
        classWindows.Style["display"] = "flex";
        lblNoTimeSelected.Style["display"] = "none";
        selectClassWindow.Style["display"] = "none";
    }
    //assign classrom window
    private void SetAssignClassroom(List<int> timeIndex)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("timeDay", typeof(string));
        dt.Columns.Add("timeIndex", typeof(int));
        timeIndex.Sort();
        for (int i = 0; i < timeIndex.Count; i++)
        {
            DataRow row = dt.NewRow();
            row["timeIndex"] = timeIndex[i];
            string timeDay = "";
            switch (timeIndex[i] / 16)
            {
                case 0:
                    timeDay += "Mon - ";
                    break;
                case 1:
                    timeDay += "Tue - ";
                    break;
                case 2:
                    timeDay += "Wen - ";
                    break;
                case 3:
                    timeDay += "Thu - ";
                    break;
                case 4:
                    timeDay += "Fri - ";
                    break;
                case 5:
                    timeDay += "Sat - ";
                    break;
                case 6:
                    timeDay += "Sun - ";
                    break;
            }
            switch (timeIndex[i] % 15)
            {
                case 1:
                    timeDay += "0800";
                    break;
                case 2:
                    timeDay += "0900";
                    break;
                case 3:
                    timeDay += "1000";
                    break;
                case 4:
                    timeDay += "1100";
                    break;
                case 5:
                    timeDay += "1200";
                    break;
                case 6:
                    timeDay += "1300";
                    break;
                case 7:
                    timeDay += "1400";
                    break;
                case 8:
                    timeDay += "1500";
                    break;
                case 9:
                    timeDay += "1600";
                    break;
                case 10:
                    timeDay += "1700";
                    break;
                case 11:
                    timeDay += "1800";
                    break;
                case 12:
                    timeDay += "1900";
                    break;
                case 13:
                    timeDay += "2000";
                    break;
                case 14:
                    timeDay += "2100";
                    break;
                case 0:
                    timeDay += "2200";
                    break;
            }
            row["timeDay"] = timeDay;
            dt.Rows.Add(row);
        }
        gvAssignClassroom.DataSource = dt;
        gvAssignClassroom.DataBind();
    }

    protected void CheckClassIsUsed_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator validator = (CustomValidator)source;
        GridViewRow row = (GridViewRow)validator.NamingContainer;
        int rowIndex = row.RowIndex;
        string inputValue = args.Value;
        int timeIndex = int.Parse(row.Cells[2].Text);
        DataSet dataSet = DatabaseManager.GetRecord(
            "class",
            new List<string> { "time" },
            $@"WHERE time = '{timeIndex}' AND class_room = '{inputValue}'"
            );
        if (dataSet != null)
        {
            if (dataSet.Tables[0].Rows.Count == 0)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }

    protected void btnSaveAssignClassroom_Click(object sender, EventArgs e)
    {
        Section newSection = Session["newSection"] as Section;
        if (lblAssigningClassType.Text == "Lecture Class")
        {
            newSection.lectureClass.Clear();
        }
        else
        {
            newSection.practicalClass.Clear();
        }

        for (int i = 0; i < gvAssignClassroom.Rows.Count; i++)
        {
            GridViewRow row = gvAssignClassroom.Rows[i];
            TextBox txtClassRoom = (TextBox)row.FindControl("txtClassRoomName");
            string classRoom = txtClassRoom.Text.ToUpper();
            int timeIndex = int.Parse(row.Cells[2].Text);
            string classType = lblAssigningClassType.Text;
            string lecturerId = null;
            if (classType == "Lecture Class")
            {
                lecturerId = ddlLectureClassLecturer.SelectedValue;
                newSection.AddLectureClass(classRoom, lecturerId, timeIndex);
            }
            else
            {
                lecturerId = ddlPracticalClassLecturer.SelectedValue;
                newSection.AddPracticalClass(classRoom, lecturerId, timeIndex);
            }
        }

        Session["newSection"] = newSection;
        assignClassroomWindow.Style["display"] = "none";
        classWindows.Style["display"] = "flex";
        SetClassTimetable(newSection.GetLectureClassTable(), newSection.GetPracticalClassTable());
    }

    protected void btnCancelAssignClassroom_Click(object sender, EventArgs e)
    {
        selectClassWindow.Style["display"] = "flex";
        assignClassroomWindow.Style["display"] = "none";
    }

    //btn bottom
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMaintainCourseAndSectionPage.aspx");
    }

    protected void btnUpdateCourse_Click(object sender, EventArgs e)
    {
        Page.Validate("");
        if (Page.IsValid)
        {
            string courseId = Request.QueryString["course"];
            string program = Request.QueryString["program"];
            //get curren preCourseAdded
            DataTable currenPreCourse = new DataTable();
            currenPreCourse.Columns.Add("cid", typeof(string));
            currenPreCourse.Columns.Add("name", typeof(string));
            currenPreCourse = ReadPrerequisite(courseId);

            //get new preCourseAdd
            DataTable newPreCourse = new DataTable();
            newPreCourse.Columns.Add("cid", typeof(string));
            newPreCourse.Columns.Add("name", typeof(string));
            newPreCourse = Session["preCourseAdded"] as DataTable;
            //get deleted preCourse and added course
            DataTable deletedPreCourse = new DataTable();
            deletedPreCourse.Columns.Add("cid", typeof(string));
            deletedPreCourse.Columns.Add("name", typeof(string));

            foreach (DataRow currenRow in currenPreCourse.Rows)
            {
                bool exist = false;
                for (int i = 0; i < newPreCourse.Rows.Count; i++)
                {
                    DataRow newRow = newPreCourse.Rows[i];
                    if (currenRow["cid"].ToString() == newRow["cid"].ToString())
                    {
                        //remove found row
                        exist = true;
                        newPreCourse.Rows.Remove(newRow);
                        break;
                    }
                }
                if (exist != true)
                {
                    deletedPreCourse.Rows.Add(currenRow["cid"].ToString(), currenRow["name"].ToString());
                }
            }

            foreach (DataRow row in deletedPreCourse.Rows)
            {
                DatabaseManager.DeleteData(
                    "course_prerequisite",
                    $@"WHERE cid = '{courseId}' AND prerequisite = '{row["cid"].ToString()}'"
                    );
            }

            foreach (DataRow row in newPreCourse.Rows)
            {
                DatabaseManager.InsertData(
                    "course_prerequisite",
                    new List<string> { "cid", "prerequisite" },
                    new List<object> { courseId, row["cid"].ToString() }
                    );
            }

            //get curren section
            List<Section> currenSections = ReadCurrenSection(courseId, program);

            //get new section
            List<Section> newSections = Session["sectionAdded"] as List<Section>;

            List<Section> deleteSections = new List<Section>();
            List<Section> updatedSections = new List<Section>();

            foreach (Section currenSection in currenSections)
            {
                bool exist = false;
                for (int i = 0; i < newSections.Count; i++)
                {
                    Section newSection = newSections[i];
                    if (currenSection.sectionId == newSection.sectionId)
                    {
                        //remove found section
                        updatedSections.Add(newSection);
                        exist = true;
                        newSections.Remove(newSection);
                        break;
                    }
                }
                if (exist != true)
                {
                    deleteSections.Add(currenSection);
                }
            }

            //delete section
            foreach (Section section in deleteSections)
            {
                string name = section.name;
                string sectionId = section.sectionId;
                string semester = section.semester;
                string cid = section.courseId;
                int maxEnrollAllow = section.maxEnrollAllow;
                List<Class> lectureClass = section.lectureClass;
                List<Class> practicalClass = section.practicalClass;

                //delete class
                foreach(Class deleteClass in lectureClass)
                {
                    DatabaseManager.DeleteData(
                        "class",
                        $@"WHERE sid = '{sectionId}'"
                        );
                }
                foreach(Class deleteClass in practicalClass)
                {
                    DatabaseManager.DeleteData(
                        "class",
                        $@"WHERE sid = '{sectionId}'"
                        );
                }
                //delete section
                DatabaseManager.DeleteData(
                    "section",
                    $@"WHERE sid = '{sectionId}'"
                    );
            }

            //add section
            foreach (Section section in newSections)
            {
                string name = section.name;
                string sectionId = section.sectionId;
                string semester = section.semester;
                string cid = section.courseId;
                int maxEnrollAllow = section.maxEnrollAllow;
                List<Class> lectureClass = section.lectureClass;
                List<Class> practicalClass = section.practicalClass;
                
                //insert section
                DatabaseManager.InsertData(
                    "section",
                    new List<string> { "sid", "name", "cid", "semester", "program", "max_enroll", "current_enroll" },
                    new List<object> { sectionId, name, cid, semester, program, maxEnrollAllow, 0}
                    );

                //insert class
                foreach (Class addedClass in lectureClass)
                {
                    string classId = addedClass.classId;
                    string classType = addedClass.classType;
                    string classRoom = addedClass.classRoom;
                    string lecturerId = addedClass.lecturerId;
                    int timeIndex = addedClass.timeIndex;
                    DatabaseManager.InsertData(
                        "class",
                        new List<string> { "id", "sid", "time", "class_room", "lid", "type" },
                        new List<object> { classId, sectionId, timeIndex, classRoom, lecturerId, "LECTURE" }
                        );
                }
                foreach (Class addedClass in practicalClass)
                {
                    string classId = addedClass.classId;
                    string classType = addedClass.classType;
                    string classRoom = addedClass.classRoom;
                    string lecturerId = addedClass.lecturerId;
                    int timeIndex = addedClass.timeIndex;
                    DatabaseManager.InsertData(
                        "class",
                        new List<string> { "id", "sid", "time", "class_room", "lid", "type" },
                        new List<object> { classId, sectionId, timeIndex, classRoom, lecturerId, "PRACTICAL" }
                        );
                }
            }

            //update section
            foreach (Section section in updatedSections)
            {
                string name = section.name;
                string sectionId = section.sectionId;
                string semester = section.semester;
                string cid = section.courseId;
                int maxEnrollAllow = section.maxEnrollAllow;
                List<Class> lectureClass = section.lectureClass;
                List<Class> practicalClass = section.practicalClass;

                //update section
                DatabaseManager.UpdateData(
                    "section",
                    new List<string> { "max_enroll" },
                    new List<object> { maxEnrollAllow },
                    $@"WHERE sid = '{sectionId}'"
                    );

                //update class
                foreach (Class deleteClass in lectureClass)
                {
                    DatabaseManager.DeleteData(
                        "class",
                        $@"WHERE sid = '{sectionId}'"
                        );
                }
                foreach (Class deleteClass in practicalClass)
                {
                    DatabaseManager.DeleteData(
                        "class",
                        $@"WHERE sid = '{sectionId}'"
                        );
                }
                foreach (Class addedClass in lectureClass)
                {
                    string classId = addedClass.classId;
                    string classType = addedClass.classType;
                    string classRoom = addedClass.classRoom;
                    string lecturerId = addedClass.lecturerId;
                    int timeIndex = addedClass.timeIndex;
                    DatabaseManager.InsertData(
                        "class",
                        new List<string> { "id", "sid", "time", "class_room", "lid", "type" },
                        new List<object> { classId, sectionId, timeIndex, classRoom, lecturerId, "LECTURE" }
                        );
                }
                foreach (Class addedClass in practicalClass)
                {
                    string classId = addedClass.classId;
                    string classType = addedClass.classType;
                    string classRoom = addedClass.classRoom;
                    string lecturerId = addedClass.lecturerId;
                    int timeIndex = addedClass.timeIndex;
                    DatabaseManager.InsertData(
                        "class",
                        new List<string> { "id", "sid", "time", "class_room", "lid", "type" },
                        new List<object> { classId, sectionId, timeIndex, classRoom, lecturerId, "PRACTICAL" }
                        );
                }
            }
            successfulWindow.Style["display"] = "flex";
        }
    }
}