using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentTakeAttendentResultPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sid"] != null)
        {
            string studentId = Session["sid"].ToString();
            if (Request.QueryString["qrData"] != null)
            {
                string qrData = Request.QueryString["qrData"];
                string[] parts = qrData.Split('~');
                if (parts.Count() >= 4)
                {
                    string recordId = parts[0];
                    string courseId = parts[1];
                    string sectionId = parts[2];
                    string dateTimeGenerate = parts[3];
                    if (CheckQrNotExpired(dateTimeGenerate))
                    {
                        if (CheckTakenThisCourse(studentId, courseId))
                        {
                            int count = DatabaseManager.GetRecordCount(
                                "student_take_attendance",
                                $@"WHERE rid = '{recordId}'"
                             );
                            if (count < 1)
                            {
                                bool success = DatabaseManager.InsertData(
                                    "student_take_attendance",
                                    new List<string> { "rid", "sid" },
                                    new List<object> { recordId, studentId }
                                    );
                                if (success)
                                {
                                    
                                    successfulWindow.Style["display"] = "flex";
                                }
                                else
                                {
                                    lblErrorMessage.Text = "Attendent Taken Fail";
                                    failWindow.Style["display"] = "flex";
                                }
                            }
                            else
                            {
                                lblErrorMessage.Text = "You Have Taken Attendend For This Class Already";
                                failWindow.Style["display"] = "flex";
                            }
                        }
                        else
                        {
                            lblErrorMessage.Text = "You have not taken this course this semester";
                            failWindow.Style["display"] = "flex";
                        }
                    }
                    else
                    {
                        lblErrorMessage.Text = "This QR Code have expired";
                        failWindow.Style["display"] = "flex";
                    }
                }
                else
                {
                    lblErrorMessage.Text = "This QR Code is invalid";
                    failWindow.Style["display"] = "flex";
                }
            }
            
        }
        else
        {
            Response.Redirect("StudentLoginPage.aspx");
        }
    }

    private bool CheckTakenThisCourse(string studentId, string courseId)
    {
        int count = DatabaseManager.GetRecordCount(
                "student_taken_course",
                $@"WHERE status = 'TAKEN' AND sid = '{studentId}' AND cid = '{courseId}' "
            );
        return count > 0;
    }

    private bool CheckQrNotExpired(string dateTimeGenerate)
    {
        DateTime date = DateTime.Parse(dateTimeGenerate);
        TimeSpan difference = DateTime.Now - date;
        if (difference.TotalMinutes < 30)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
            Response.Redirect("StudentHomePage.aspx");
    }
}