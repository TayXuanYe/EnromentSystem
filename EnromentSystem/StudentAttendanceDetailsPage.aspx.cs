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

                }
            }
        }
        else
        {
            Response.Redirect("StudentHomePage.aspx");
        }
    }


    private void SetHistoryGridView(string courseId, string)
    {

    }

}