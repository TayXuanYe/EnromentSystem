//using iText.Forms.Form.Element;
using System;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminAddCourseAndSectionPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetClassTimetable();
        }
    }
    //Section
    protected void btnAddSection_Click(object sender, ImageClickEventArgs e)
    {
        classWindows.Style["display"] = "flex";
        lblClassWindowsSectionName.Text = txtSectionName.Text;
    }


    protected void gvSectionInfo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void gvSectionInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {

        }
    }
    // class select pop up windows
    private void SetClassTimetable()
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
        dt.Rows.Add("Mon");
        dt.Rows.Add("Tue");
        dt.Rows.Add("Wed");
        dt.Rows.Add("Thu");
        dt.Rows.Add("Fri");
        dt.Rows.Add("Sat");
        dt.Rows.Add("Sun");

        gvClassTimetable.DataSource = dt;
        gvClassTimetable.DataBind();
    }

    protected void btnAddLectureClass_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnAddPracticalClass_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnAddClass_Click(object sender, EventArgs e)
    {
        SetClassTimetable();
    }

    protected void btnCancelAddClass_Click(object sender, EventArgs e)
    {
        classWindows.Style["display"] = "none";
    }


    //course part
    protected void CheckCourseIdIsExist_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }

    
    //btn bottom
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMaintainCourseAndSectionPage.aspx");
    }

    protected void btnAddCourse_Click(object sender, EventArgs e)
    {

    }
}