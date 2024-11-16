using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LecturerHomePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lid"] != null)
        {
            if (!IsPostBack)
            {
                string lecturerId = Session["lid"].ToString();
                DataSet dataSet = DatabaseManager.GetRecord(
                    "course",
                    new List<string> { "cid", "name" },
                    $@"WHERE cid IN (
                        SELECT cid FROM class JOIN section ON class.sid = section.sid WHERE lid = '{lecturerId}' 
                        AND semester IN (SELECT semester FROM current_semester))"
                    );
                if(dataSet != null)
                {
                    gvCourse.DataSource = dataSet;
                    gvCourse.DataBind();
                }

                DataSet lectureData = DatabaseManager.GetRecord(
                    "lecture",
                    new List<string> { "name" },
                    $@"WHERE lid = '{lecturerId}'"
                    );
                if(lectureData != null)
                {
                    DataRow row = lectureData.Tables[0].Rows[0];
                    lblName.Text = row["name"].ToString();
                }
                
                DataSet semesterData = DatabaseManager.GetRecord(
                    "current_semester",
                    new List<string> { "semester" }
                    );
                if(semesterData != null)
                {
                    DataRow row = semesterData.Tables[0].Rows[0];
                    lblSemester.Text = row["semester"].ToString();
                }
            }
        }
        else
        {
            Response.Redirect("LecturerLoginPage.aspx");
        }
    }

    protected void gvCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        string courseId = gvCourse.SelectedRow.Cells[0].Text;
        
    }
}