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
            PopulateProgram();
            //let gvPrerequisite and gvSectionInfo display
            DataTable dt = new DataTable();
            gvPrerequisite.DataSource = dt;
            gvPrerequisite.DataBind();
            gvSectionInfo.DataSource = dt;
            gvSectionInfo.DataBind();
            //set pre requisite session and populate prerequisite details
            DataTable preCourse = new DataTable();
            preCourse.Columns.Add("cid",typeof(string));
            preCourse.Columns.Add("name",typeof(string));
            Session["preCourseAdded"] = preCourse;
            PopulatePrerequisite(ddlProgram.SelectedValue,ddlMajor.SelectedValue, preCourse);
            //set section session
            DataTable sectionAdded = new DataTable();
            sectionAdded.Columns.Add("name", typeof(string));
            Session["sectionAdded"] = sectionAdded;
            //set class info session
            DataSet classInfo = new DataSet();
            DataTable lectureClass = new DataTable("lectureClass");
            lectureClass.Columns.Add("classRoom",typeof(string));
            lectureClass.Columns.Add("timeIndex",typeof(int));
            DataTable practicalClass = new DataTable("practicalClass");
            practicalClass.Columns.Add("classRoom", typeof(string));
            practicalClass.Columns.Add("timeIndex", typeof(int));
            classInfo.Tables.Add(lectureClass);
            classInfo.Tables.Add(practicalClass);
            Session["classInfo"] = classInfo;
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
        Session["preCourseAdded"] = dataTable;
        PopulatePrerequisite(ddlProgram.SelectedValue, ddlMajor.SelectedValue, dataTable);
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
        Page.Validate("section");
        if (Page.IsValid)
        {
            classWindows.Style["display"] = "flex";
            lblClassWindowsSectionName.Text = txtSectionName.Text;
            PopulateLecturer();
            //set class info session
            DataSet classInfo = new DataSet();
            DataTable lectureClass = new DataTable("lectureClass");
            lectureClass.Columns.Add("classRoom", typeof(string));
            lectureClass.Columns.Add("timeIndex", typeof(int));
            DataTable practicalClass = new DataTable("practicalClass");
            practicalClass.Columns.Add("classRoom", typeof(string));
            practicalClass.Columns.Add("timeIndex", typeof(int));
            classInfo.Tables.Add(lectureClass);
            classInfo.Tables.Add(practicalClass);
            Session["classInfo"] = null;
            Session["classInfo"] = classInfo;
            SetClassTimetable(classInfo);
        }
    }

    protected void CheckSectionIsExist_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string name = args.Value;
        if (Session[name + "_class_info"] == null)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }

    protected void gvSectionInfo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name = gvSectionInfo.SelectedRow.Cells[0].Text;
        DataTable dataTable = Session["sectionAdded"] as DataTable;
        for (int i = dataTable.Rows.Count - 1; i >= 0; i--)
        {
            if (dataTable.Rows[i]["name"].ToString() == name)
            {
                dataTable.Rows[i].Delete();
            }
        }
        dataTable.AcceptChanges();
        gvSectionInfo.DataSource = dataTable;
        gvSectionInfo.DataBind();
        Session[name + "_class_info"] = null;
        Session[name + "_practical_lecturer"] = null;
        Session[name + "_lecture_lecturer"] = null;
    }

    protected void gvSectionInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            string name = e.CommandArgument.ToString();
            Session["classInfo"] = Session[name + "_class_info"];
            classWindows.Style["display"] = "flex";
            lblClassWindowsSectionName.Text = name;
            PopulateLecturer();
            SetClassTimetable(Session["classInfo"] as DataSet);
        }
    }
    // class select pop up windows
    private void SetClassTimetable(DataSet classInfo)
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

        DataTable lectureClass = classInfo.Tables["lectureClass"];
        DataTable practicalClass = classInfo.Tables["practicalClass"];
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
            switch (int.Parse(input) / 16)
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
            switch (int.Parse(input) /16)
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
        if(dataSet != null)
        {
            List<string> value = new List<string>();
            value.Add("None");
            List<string> text = new List<string>();
            text.Add("None");
            foreach(DataRow row in dataSet.Tables[0].Rows)
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
        if(ddlLectureClassLecturer.SelectedValue == "None")
        {
            lblWarningLectureClassLecturer.Style["display"] = "inline";
        }
        else
        {
            lblWarningLectureClassLecturer.Style["display"] = "none";
            lblSelectingClass.Text = "Lecture Class";
            selectClassWindow.Style["display"] = "flex";
            classWindows.Style["display"] = "none";
            SetSelectTable(Session["classInfo"] as DataSet);
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
            classWindows.Style["display"] = "none";
            SetSelectTable(Session["classInfo"] as DataSet);
        }
    }

    protected void btnAddClass_Click(object sender, EventArgs e)
    {
        DataTable displayValue = Session["sectionAdded"] as DataTable;
        DataSet classInfo = Session["classInfo"] as DataSet;
        DataTable lectureClass = classInfo.Tables["lectureClass"];
        DataTable practicalClass = classInfo.Tables["practicalClass"];
        if (lectureClass.Rows.Count == 0 && practicalClass.Rows.Count == 0)
        {
            lblWarningNoClassAdded.Style["display"] = "inline";
        }
        else
        {
            lblWarningNoClassAdded.Style["display"] = "none";
            string name = lblClassWindowsSectionName.Text;
            Session[name+"_class_info"] = Session["classInfo"] as DataSet;
            displayValue.Rows.Add(name);
            gvSectionInfo.DataSource = displayValue;
            gvSectionInfo.DataBind();
            Session["sectionAdded"] = displayValue;
            if (lectureClass.Rows.Count != 0)
            {
                Session[name + "_lecture_lecturer"] = ddlLectureClassLecturer.SelectedValue;
            }
            if(practicalClass.Rows.Count != 0)
            {
                Session[name + "_practical_lecturer"] = ddlPracticalClassLecturer.SelectedValue;
            }
            classWindows.Style["display"] = "none";
        }
    }

    protected void btnCancelAddClass_Click(object sender, EventArgs e)
    {
        classWindows.Style["display"] = "none";
    }

    protected void ddlPracticalClassLecturer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet classInfo = Session["classInfo"] as DataSet;
        DataTable practicalClass = classInfo.Tables["practicalClass"];
        practicalClass.Rows.Clear();
        Session["classInfo"] = classInfo;
        SetClassTimetable(classInfo); 
    }

    protected void ddlLectureClassLecturer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet classInfo = Session["classInfo"] as DataSet;
        DataTable lectureClass = classInfo.Tables["lectureClass"];
        lectureClass.Rows.Clear();
        Session["classInfo"] = classInfo;
        SetClassTimetable(classInfo);
    }
    //selectClassWindow
    private void SetSelectTable(DataSet classInfo)
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
        string lecturer = "";
        if(lblSelectingClass.Text == "Lecture Class")
        {
            lecturer = ddlLectureClassLecturer.SelectedValue;
        }
        else
        {
            lecturer = ddlPracticalClassLecturer.SelectedValue;
        }
        DataSet lectureTime = DatabaseManager.GetRecord(
            "class",
            new List<string> { "time" },
            $@"JOIN section as s on s.sid = class.sid WHERE lid = '{lecturer}' "
            );
        HashSet<int> lecturerUseTime = new HashSet<int>();
        if(lectureTime != null)
        {
            foreach (DataRow row in lectureTime.Tables[0].Rows)
            {
                lecturerUseTime.Add(int.Parse(row["time"].ToString()));
            }
        }
        //set lecture not available time
        for(int i = 0; i < gvSelectClass.Rows.Count; i++)
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
        DataTable lectureClass = classInfo.Tables["lectureClass"];
        HashSet<int> lectureClassTime = new HashSet<int>();
        foreach(DataRow row in lectureClass.Rows)
        {
            lectureClassTime.Add(int.Parse(row["timeIndex"].ToString()));
        }

        DataTable practicalClass = classInfo.Tables["practicalClass"];
        HashSet<int> practicalClassTime = new HashSet<int>();
        foreach (DataRow row in practicalClass.Rows)
        {
            practicalClassTime.Add(int.Parse(row["timeIndex"].ToString()));
        }

        //set check box curren class and disable the check box for another class
        if(lblSelectingClass.Text == "Lecture Class")
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
        if(selectedTime.Count > 0)
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
        dt.Columns.Add("timeDay",typeof(string));
        dt.Columns.Add("timeIndex",typeof(int));
        timeIndex.Sort();
        for(int i = 0; i < timeIndex.Count; i++)
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
        if(dataSet != null)
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
        DataSet courseInfo = Session["classInfo"] as DataSet;
        if(lblAssigningClassType.Text == "Lecture Class")
        {
            DataTable lectureClass = courseInfo.Tables["lectureClass"];
            for (int i = 0; i < gvAssignClassroom.Rows.Count; i++)
            {
                GridViewRow row = gvAssignClassroom.Rows[i];
                TextBox txtClassRoom = (TextBox)row.FindControl("txtClassRoomName");
                DataRow dataRow = lectureClass.NewRow();
                dataRow["classRoom"] = txtClassRoom.Text;
                dataRow["timeIndex"] = int.Parse(row.Cells[2].Text);
                lectureClass.Rows.Add(dataRow);
            }
        }
        else
        {
            DataTable practicalClass = courseInfo.Tables["practicalClass"];
            for (int i = 0; i < gvAssignClassroom.Rows.Count; i++)
            {
                GridViewRow row = gvAssignClassroom.Rows[i];
                TextBox txtClassRoom = (TextBox)row.FindControl("txtClassRoomName");
                DataRow dataRow = practicalClass.NewRow();
                dataRow["classRoom"] = txtClassRoom.Text;
                dataRow["timeIndex"] = int.Parse(row.Cells[2].Text);
                practicalClass.Rows.Add(dataRow);
            }
        }
        Session["classInfo"] = courseInfo;
        assignClassroomWindow.Style["display"] = "none";
        classWindows.Style["display"] = "flex";
        SetClassTimetable(courseInfo);
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

    protected void btnAddCourse_Click(object sender, EventArgs e)
    {
        DatabaseManager.InsertData(
            "course",
            new List<string> { "cid", "name", "credit_hours", "available", "price" },
            new List<object> { txtCourseId.Text, txtCourseName.Text, ddlCreditHours.SelectedValue, "1", txtPrice.Text }
            );
        DatabaseManager.InsertData(
            "course_major",
            new List<string> { "cid", "major", "program" },
            new List<object> { txtCourseId.Text, ddlMajor.SelectedValue, ddlProgram.SelectedValue }
            );
        DataTable preCourseAdded = Session["preCourseAdded"] as DataTable;
        if(preCourseAdded != null)
        {
            foreach(DataRow row in preCourseAdded.Rows)
            {
                DatabaseManager.InsertData(
                    "course_prerequisite",
                    new List<string> { "cid", "prerequisite" },
                    new List<object> { txtCourseId.Text, row["cid"].ToString() }
                    );
            }
        }
        //DataTable sectionAdded = Session["sectionAdded"] as DataTable;
        //if(sectionAdded != null)
        //{
        //    foreach(DataRow row in sectionAdded.Rows)
        //    {
        //        string name = row["name"].ToString();
        //    }
        //}
    }

}