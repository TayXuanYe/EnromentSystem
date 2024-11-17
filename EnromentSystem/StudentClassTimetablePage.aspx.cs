using iText.Html2pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

public partial class StudentClassTimetablePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string htmlContent = GenerateTimetable("I23024312");
        PdfGenerator pdfGenerator = new PdfGenerator();
        pdfGenerator.GenerateLandscapePdf(htmlContent, "output.pdf", "StudentTimeTable");
        DisplayPdf("output.pdf");
    }

    private string GenerateTimetable(string studentID)
    {
        string date = System.DateTime.Now.Date.ToString("yyyy-MM-dd");
        string time = System.DateTime.Now.ToString("HH:mm");
        //student details
        string school = "";
        string studentName = "";
        string level = "";
        string session = "";
        string program = "";
        string modeOfStudy = "";
        string major = "";
        int year = 0;
        int semester = 0;
        string admission_date = "";
        DataSet studentDetails = DatabaseManager.GetRecord(
            "student",
            new List<string> { "school", "name", "level", "program", "mode_of_study", "major", "admission_date" },
            $@"WHERE sid = '{studentID}'"
            );
        if (studentDetails != null)
        {
            foreach (DataRow row in studentDetails.Tables[0].Rows)
            {
                school = row["school"].ToString();
                studentName = row["name"].ToString();
                level = row["level"].ToString();
                program = row["program"].ToString();
                modeOfStudy = row["mode_of_study"].ToString();
                major = row["major"].ToString();
                admission_date = Convert.ToDateTime(row["admission_date"]).ToString("yyyy-MM-dd");
            }
        }
        DataSet currenSemester = DatabaseManager.GetRecord(
            "current_semester",
            new List<string> { "semester" }
            );
        if (currenSemester != null)
        {
            foreach (DataRow row in currenSemester.Tables[0].Rows)
            {
                session = row["semester"].ToString();
            }
        }
        {
            DateTime endDate = DateTime.Now.Date;
            DateTime startDate = DateTime.Parse(admission_date);

            double years = (endDate - startDate).TotalDays / 365.25;

            year = (int)Math.Ceiling(years);

            if ((years % 1) * 12 < 5)
            {
                int i = (int)Math.Floor(years);
                semester = i * 3 + 1;
            }
            else if ((years % 1) * 12 < 7)
            {
                int i = (int)Math.Floor(years);
                semester = i * 3 + 2;
            }
            else
            {
                int i = (int)Math.Floor(years);
                semester = i * 3 + 3;
            }
        }

        //course data table
        string studentCourseTable = "";
        int totalCreaditHours = 0;
        DataSet courseData = DatabaseManager.GetRecord(
            "student_taken_course AS stc",
            new List<string> { "stc.cid", "course.name AS courseName", "section.name AS sectionName", "course.credit_hours", "section_id" },
            $@"JOIN course ON course.cid = stc.cid JOIN section ON section.sid = stc.section_id 
            WHERE status = 'TAKEN' AND stc.sid = '{studentID}'"
            );
        if (courseData != null)
        {
            int i = 1;
            foreach (DataRow row in courseData.Tables[0].Rows)
            {
                DataSet lecturerData = DatabaseManager.GetDistinctRecord(
                    "lecture",
                    new List<string> { "name" },
                    $@"WHERE lid IN (SELECT lid FROM class WHERE sid='{row["section_id"].ToString()}')"
                    );
                string lecture = "";
                if (lecturerData != null)
                {
                    for (int j = 0; j < lecturerData.Tables[0].Rows.Count; j++)
                    {
                        DataRow lectureRow = lecturerData.Tables[0].Rows[j];
                        if (j != 0)
                        {
                            lecture += ", ";
                        }
                        lecture += lectureRow["name"].ToString();
                    }
                }

                studentCourseTable += $@"
                    <tr>
                        <td>{i}</td>
                        <td>{row["cid"].ToString()}</td>
                        <td>{row["courseName"].ToString()}</td>
                        <td>{lecture}</td>
                        <td>{row["sectionName"].ToString()}</td>
                        <td>{row["credit_hours"].ToString()}</td>
                    </tr>";
                totalCreaditHours += int.Parse(row["credit_hours"].ToString());
                i++;
            }
        }
        //student time table
        string studentTimeTable = "";
        DataSet classData = DatabaseManager.GetRecord(
            "class",
            new List<string> { "cid", "name", "class_room", "type", "time" },
            $@"JOIN section ON class.sid = section.sid WHERE class.sid IN (SELECT section_id FROM student_taken_course WHERE status = 'TAKEN' AND sid = '{studentID}') ORDER BY 'time'"
            );
        if (classData != null)
        {
            int rowRead = 0;
            studentTimeTable += "<tr>";
            studentTimeTable += "<td>MON</td>";
            for (int i = 1; i < 106; i++)
            {
                string courseId = "";
                string sectionName = "";
                string classRoom = "";
                string classType = "";
                if (rowRead < classData.Tables[0].Rows.Count)
                {
                    DataRow classRow = classData.Tables[0].Rows[rowRead];
                    if (i == int.Parse(classRow["time"].ToString()))
                    {
                        courseId = classRow["cid"].ToString();
                        sectionName = classRow["name"].ToString();
                        classRoom = classRow["class_room"].ToString();
                        classType = classRow["type"].ToString();
                        rowRead++;
                    }
                }
                studentTimeTable += $@"
                    <td>
                        {courseId} {sectionName}</br>
                        {classRoom}</br>
                        {classType}
                    </td>";
                if (i % 15 == 0 && i != 105)
                {
                    studentTimeTable += "</tr><tr>";
                    switch (i)
                    {
                        case 15:
                            studentTimeTable += "<td>TUE</td>";
                            break;

                        case 30:
                            studentTimeTable += "<td>WED</td>";
                            break;

                        case 45:
                            studentTimeTable += "<td>THU</td>";
                            break;

                        case 60:
                            studentTimeTable += "<td>FRI</td>";
                            break;

                        case 75:
                            studentTimeTable += "<td>SAT</td>";
                            break;

                        case 90:
                            studentTimeTable += "<td>SUN</td>";
                            break;
                    }
                }
            }
            studentTimeTable += "</tr>";
        }

        string htmlContent =
            $@"<html>
                <head>
                    <style>
                        .header{{
                            width: 100%;
                            padding:0px 20px;
                            font-size: 12pt;
                        }}
                        .header td:nth-child(5n+1)::before{{
                            text-align: right;
                            float: right;
                            content: "":"";
                            font-weight: 600;
                            font-size: 12pt;
                            color: #000;
                        }}
                        .header td:nth-child(3n+1){{
                            width: 5%;
                            padding-right: 5px;
                        }}
                        .header td:nth-child(3n+2){{
                            width: 20%;
                        }}
                        .header td:nth-child(5n+3){{
                            text-align: center;
                        }}
                        h1{{
                            font-size: 18pt;
                            margin-bottom: 0%;
                            vertical-align: bottom;
                        }}
                        h2{{
                            vertical-align: bottom;
                            font-size: 14pt;
                            margin-bottom: 0%;
                        }}
                        .header{{
                            border-bottom: solid 1px #000;
                        }}
                        .student-details{{
                            width: 100%;
                            padding: 5px 20px;
                            font-size: 12pt;
                        }}
                        .student-details td:nth-child(2n+1){{
                            width: 15%;
                            min-width: 150px; 
                            font-weight: 600;
                        }}
                        .student-details td:nth-child(2n){{
                            padding-left: 2px;
                        }}
                        .student-details td:nth-child(2n+1)::before{{
                            content: "":"";
                            font-weight: 600;
                            font-size: 12pt;
                            text-align: right;
                            float: right;
                            color: #000;
                        }}
                        .course-details{{
                            width: 100%;
                            padding: 5px 20px;
                            font-size: 12pt;
                        }}
                        .course-details td{{
                            /* border: solid 1px #000; */
                            padding: 0px 3px;
                        }}
                        .course-details td:nth-child(6n+1){{
                            text-align: center;
                            width: 5%;
                        }}
                        .course-details td:nth-child(6n+2){{
                            width: 10%;
                        }}
                        .course-details td:nth-child(6n+3){{
                            width: 30%;
                        }}
                        .course-details td:nth-child(6n+4){{
                            width: 30%;
                        }}
                        .course-details td:nth-child(6n+5){{
                            text-align: center;
                            width: 10%;
                        }}
                        .course-details td:nth-child(6n){{
                            text-align: center;
                            width: 10%;
                        }}
                        .student-time-table{{
                            width: 100%;
                            margin: 25px 0px;
                            font-size: 10pt;
                            border-collapse: collapse;
                        }}
                        .student-time-table th{{font-size: 16pt;}}
                        .student-time-table td:first-child{{font-size: 16pt; text-align: center}}
                        .student-time-table th,.student-time-table td{{
                            min-width: 90px;
                            width: 90px; height: 60px;
                            border:solid 1px #000;
                            /* vertical-align: middle;
                            text-align: center; */
                            padding: 5px;
                        }}
                        .student-time-table th::after, .student-time-table th::before  {{
                            content: '';
                            display: block;
                            padding-top: 30%; /* 使高度与宽度一致 */
                        }}
                    </style>
                </head>
                <body>
                    <table class=""header"">
                        <tr>
                            <td>Date</td>
                            <td>{date}</td>
                            <td><h1>INTI International University</h1></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Time</td>
                            <td>{time}</td>
                            <td><h2>REGISTRATION SUMMARY</h2></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                    <table class=""student-details"">
                        <tr>
                            <td>Matricdation No</td>
                            <td>{studentID}</td>
                            <td>School</td>
                            <td>{school}</td>
                        </tr>
                        <tr>
                            <td>Student Name</td>
                            <td>{studentName}</td>
                            <td>Level</td>
                            <td>{level}</td>
                        </tr>
                        <tr>
                            <td>Session</td>
                            <td>{session}</td>
                            <td>Program</td>
                            <td>{program}</td>
                        </tr>
                        <tr>
                            <td>Mode of Study</td>
                            <td>{modeOfStudy}</td>
                            <td>Major</td>
                            <td>{major}</td>
                        </tr>
                        <tr>
                            <td>Year</td>
                            <td>{year}</td>
                            <td>Semester</td>
                            <td>{semester}</td>
                        </tr>
                    </table>
                    <table class=""course-details"">
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td><b>Lecturer</b></td>
                            <td><b>Section</b></td>
                            <td><b>Creadit Hours</b></td>
                        </tr>
                        {studentCourseTable}
                        <tr>
                            <td colspan=""5"" style=""text-align: rigth; padding-right: 25px;""><b style=""display:block;width:100%;text-align:right;"">Total Credit Hours</b></td>
                            <td style=""border-top:solid 1px #000;border-bottom:solid 1px #000;text-align:center;"">{totalCreaditHours}</td>
                        </tr>          
                    </table>
                    <table class=""student-time-table"" >
                        <tr>
                            <th>DAY</th>
                            <th>0800</th>
                            <th>0900</th>
                            <th>1000</th>
                            <th>1100</th>
                            <th>1200</th>
                            <th>1300</th>
                            <th>1400</th>
                            <th>1500</th>
                            <th>1600</th>
                            <th>1700</th>
                            <th>1800</th>
                            <th>1900</th>
                            <th>2000</th>
                            <th>2100</th>
                            <th>2200</th>
                        </tr>
                        {studentTimeTable}
                    </table>
                </body>
             </html>";

        return htmlContent;
    }

    private void DisplayPdf(string fileName)
    {
        string pdfUrl = ResolveUrl($"~/StudentTimeTable/{fileName}");
        pdfFrame.Attributes["src"] = pdfUrl;
    }
}