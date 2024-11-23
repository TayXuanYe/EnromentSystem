using Microsoft.Ajax.Utilities;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LecturerTakeAttendentPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lid"] != null)
        {
            if (!IsPostBack)
            {
                string courseId = Request.QueryString["courseId"];
                string lectureName = Request.QueryString["lectureName"];
                string semester = Request.QueryString["semester"];
                if (courseId.IsNullOrWhiteSpace() || lectureName.IsNullOrWhiteSpace() || semester.IsNullOrWhiteSpace())
                {
                    Response.Redirect("LecturerHomePage.aspx");
                }
                else
                {
                    lblCourse.Text = courseId;
                    lblLecturer.Text = lectureName;
                    lblSemester.Text = semester;
                    PopulateSection(Session["lid"].ToString());
                    PopulateClassType();
                    PopulateClassTime(Session["lid"].ToString(), ddlSection.SelectedValue, ddlClassType.SelectedValue);
                    qrPart.Style["display"] = "none";
                }
            }
        }
        else
        {
            Response.Redirect("LecturerLoginPage.aspx");
        }
    }

    private void PopulateSection(string lecturerId)
    {
        DataSet dataSet = DatabaseManager.GetDistinctRecord(
            "class",
            new List<string> { "name", "class.sid" },
            $@"JOIN section ON class.sid = section.sid WHERE lid = '{lecturerId}' AND semester IN (SELECT semester FROM current_semester)"
            );
        if(dataSet != null)
        {
            List<string> text = new List<string>();
            List<string> value = new List<string>();
            foreach(DataRow row in dataSet.Tables[0].Rows)
            {
                text.Add(row["name"].ToString());
                value.Add(row["sid"].ToString());
            }
            UIComponentGenerator.PopulateDropDownList(ddlSection,text,value);
        }
    }

    private void PopulateClassType()
    {
        DataSet dataSet = DatabaseManager.GetDistinctRecord(
            "class",
            new List<string> { "type" }
            );
        if (dataSet != null)
        {
            List<string> text = new List<string>();
            List<string> value = new List<string>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                text.Add(row["type"].ToString());
                value.Add(row["type"].ToString());
            }
            UIComponentGenerator.PopulateDropDownList(ddlClassType, text, value);
        }
    }

    private void PopulateClassTime(string lecturerId, string sectionId, string classType)
    {
        DataSet dataSet = DatabaseManager.GetDistinctRecord(
            "class",
            new List<string> { "time", "id" },
            $@"JOIN section ON class.sid = section.sid WHERE lid = '{lecturerId}' AND 
                semester IN (SELECT semester FROM current_semester) AND 
                class.sid = '{sectionId}' AND type = '{classType}'"
            );
        if (dataSet != null)
        {
            List<string> text = new List<string>();
            List<string> value = new List<string>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                string day = "";
                string time = "";
                int index = int.Parse(row["time"].ToString()) / 15;
                if (int.Parse(row["time"].ToString()) % 15 == 0)
                {
                    index--;
                }
                switch (index)
                {
                    case 0:
                        day = "Monday";
                        break;

                    case 1:
                        day = "Tuesday";
                        break;

                    case 2:
                        day = "Wednesday";
                        break;

                    case 3:
                        day = "Thursday";
                        break;

                    case 4:
                        day = "Friday";
                        break;

                    case 5:
                        day = "Saturday";
                        break;

                    case 6:
                        day = "Sunday";
                        break;
                }
                switch (int.Parse(row["time"].ToString()) % 15)
                {
                    case 1:
                        time = "0800";
                        break;

                    case 2:
                        time = "0900";
                        break;

                    case 3:
                        time = "1000";
                        break;

                    case 4:
                        time = "1100";
                        break;

                    case 5:
                        time = "1200";
                        break;

                    case 6:
                        time = "1300";
                        break;

                    case 7:
                        time = "1400";
                        break;

                    case 8:
                        time = "1500";
                        break;

                    case 9:
                        time = "1600";
                        break;

                    case 10:
                        time = "1700";
                        break;

                    case 11:
                        time = "1800";
                        break;

                    case 12:
                        time = "1900";
                        break;

                    case 13:
                        time = "2000";
                        break;

                    case 14:
                        time = "2100";
                        break;

                    case 0:
                        time = "2200";
                        break;
                }

                text.Add($"{day} - {time}");
                value.Add(row["id"].ToString());
            }
            UIComponentGenerator.PopulateDropDownList(ddlClassTime, text, value);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("LecturerHomePage.aspx");
    }

    protected void btnGenerateQr_Click(object sender, EventArgs e)
    {
        string courseId = lblCourse.Text;
        string sectionId = ddlSection.SelectedValue;
        string classId = ddlClassTime.SelectedValue;
        string dateTimeGenerate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string date = txtDate.Text;
        // rid/courseId/sectionId/date Generate/Time Generate
        string qrMessage = $"{classId}-{date}~{courseId}~{sectionId}~{dateTimeGenerate}";
        Debug.WriteLine(classId);
        DatabaseManager.InsertData(
            "lecturer_create_attendance_record",
            new List<string> { "rid", "classId", "courseId", "sectionId", "date" },
            new List<object> { $"{classId}-{date}", classId, courseId, sectionId, date }
            );
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        {
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrMessage, QRCodeGenerator.ECCLevel.Q);
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                using (Bitmap qrCodeImage = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        string base64Image = Convert.ToBase64String(byteImage);

                        imgQrCode.ImageUrl = "data:image/png;base64," + base64Image;
                    }
                }
            }
        }
        qrPart.Style["display"] = "block";
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateClassTime(Session["lid"].ToString(), ddlSection.SelectedValue, ddlClassType.SelectedValue);
    }

    protected void ddlClassType_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateClassTime(Session["lid"].ToString(), ddlSection.SelectedValue, ddlClassType.SelectedValue);
    }
}