using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI;

public partial class AdminDeleteProgramPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["program"] != null)
            {
                lblProgramName.Text = Request.QueryString["program"];
                SetProgramDetails(lblProgramName.Text);
                DisplayMajorInfo(lblProgramName.Text);
            }
            else
            {
                Response.Redirect("AdminMaintainProgramAndMajorPage.aspx");
            }
        }
    }

    //program details part
    private void SetProgramDetails(string program)
    {
        DataSet ds = DatabaseManager.GetRecord(
            "program",
            new List<string> { "school", "level" },
            $@"WHERE program = '{program}'"
            );
        if (ds != null)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                lblLevel.Text = row["level"].ToString();
                lblSchool.Text = row["school"].ToString();
            }
        }
    }

    //major part
    private void DisplayMajorInfo(string program)
    {
        DataSet ds = DatabaseManager.GetRecord(
            "major",
            new List<string> { "major" },
            $@"WHERE program = '{program}'"
            );
        if (ds != null)
        {
            DataTable majorInfo = ds.Tables[0];
            gvMajorInfo.DataSource = majorInfo;
            gvMajorInfo.DataBind();
            Session["majorInfo"] = majorInfo;
        }
    }
    //button contain part
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMaintainProgramAndMajorPage.aspx");
    }

    protected void btnDeleteProgram_Click(object sender, EventArgs e)
    {
        lblConformText.Text = lblProgramName.Text;
        conformWindow.Style["display"] = "flex";
    }

    //conform windows
    protected void btnConformDeleteProgram_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            conformWindow.Style["display"] = "none";
            bool successDeleteMajor = true;
            DataSet ds = DatabaseManager.GetRecord(
            "major",
            new List<string> { "major" },
            $@"WHERE program = '{lblProgramName.Text}'"
            );
            successDeleteMajor = DatabaseManager.DeleteData(
            "major",
            $@"WHERE program = '{lblProgramName.Text}'"
            );
            if (ds != null)
            {
                DataTable majorInfo = ds.Tables[0];
                if (majorInfo.Rows.Count == 0)
                {
                    successDeleteMajor = true;
                }
            }
            bool successDeleteProgram = true;
            if (successDeleteMajor)
            {
                successDeleteProgram = DatabaseManager.DeleteData(
                "program",
                $@"WHERE program = '{lblProgramName.Text}'"
                );
            }
            if (successDeleteProgram && successDeleteMajor)
            {
                successfulWindow.Style["display"] = "flex";
            }
            else
            {
                failWindow.Style["display"] = "flex";
            }
        }
    }

    protected void ConformTextCustomValidator_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
    {
       if(lblConformText.Text == txtConformText.Text)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = false;
        }
    }

    protected void btnCancelDeleteConform_Click(object sender, EventArgs e)
    {
        conformWindow.Style["display"] = "none";
    }
}