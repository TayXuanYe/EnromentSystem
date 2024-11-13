using iText.Layout.Properties.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminModifyProgramAndMajorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(Request.QueryString["program"] != null)
            {
                PopulateSchool();
                lblProgramName.Text = Request.QueryString["program"];
                SetProgramDetails(lblProgramName.Text);
                DisplayMajorInfo(lblProgramName.Text);

                DataTable addedMajor = new DataTable();
                addedMajor.Columns.Add("major", typeof(string));
                Session["addedMajor"] = addedMajor;

                DataTable deletedMajor = new DataTable();
                deletedMajor.Columns.Add("major", typeof(string));
                Session["deletedMajor"] = deletedMajor;
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
        if(ds != null)
        {
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                ddlLevel.SelectedValue = row["level"].ToString();
                ddlSchool.SelectedValue = row["school"].ToString();
            }
        }
    }

    private void PopulateSchool()
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "school",
            new List<string> { "school" }
            );
        if (dataSet != null)
        {
            List<string> value = new List<string>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                value.Add(row["school"].ToString());
            }
            UIComponentGenerator.PopulateDropDownList(ddlSchool, value, value);
        }
    }

    //major part
    protected void gvMajorInfo_RowCommand(object sender, EventArgs e)
    {
        string major = gvMajorInfo.SelectedRow.Cells[0].Text;
        DataTable dataTable = Session["majorInfo"] as DataTable;
        if (dataTable == null) return;
        for (int i = dataTable.Rows.Count - 1; i >= 0; i--)
        {
            if (dataTable.Rows[i]["major"].ToString() == major)
            {
                dataTable.Rows[i].Delete();
                break;
            }
        }
        dataTable.AcceptChanges();
        gvMajorInfo.DataSource = dataTable;
        gvMajorInfo.DataBind();

        bool foundInAddedTable = false;
        DataTable addedMajor = Session["addedMajor"] as DataTable;
        for (int i = 0; i < addedMajor.Rows.Count; i++)
        {
            if (addedMajor.Rows[i]["major"].ToString() == major)
            {
                addedMajor.Rows[i].Delete();
                foundInAddedTable = true;
                break;
            }
        }
        if (foundInAddedTable)
        {
            Session["addedMajor"] = addedMajor;
        }
        else
        {
            DataTable deletedMajor = Session["deletedMajor"] as DataTable;
            DataRow row = deletedMajor.NewRow();
            row["major"] = major;
            deletedMajor.Rows.Add(row);
            Session["deletedMajor"] = deletedMajor;
        }
    }

    protected void btnAddMajor_Click(object sender, EventArgs e)
    {
        DataTable majorInfo = Session["majorInfo"] as DataTable;
        DataRow row = majorInfo.NewRow();
        if (!CheckMajorExist(txtMajorName.Text))
        {
            row["major"] = txtMajorName.Text.ToUpper();
            majorInfo.Rows.Add(row);

            DataTable addedMajor = Session["addedMajor"] as DataTable;
            DataRow rowAdd = addedMajor.NewRow();
            addedMajor.Rows.Add(rowAdd);
            rowAdd["major"] = txtMajorName.Text.ToUpper();
            Session["addedMajor"] = addedMajor;
        }
        gvMajorInfo.DataSource = majorInfo;
        gvMajorInfo.DataBind();
        Session["majorInfo"] = majorInfo;
    }

    private bool CheckMajorExist(string major)
    {
        DataTable majorInfo = Session["majorInfo"] as DataTable;
        if (majorInfo.Rows.Count > 0)
        {
            foreach (DataRow row in majorInfo.Rows)
            {
                if (row["major"].ToString() == major)
                {
                    return true;
                }
            }
        }
        return false;
    }

    protected void CheckMajorExist_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !CheckMajorExist(txtMajorName.Text);
    }

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

    protected void btnUpdateProgram_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            bool successUpdateProgram = DatabaseManager.UpdateData(
                "program",
                new List<string> { "school", "level" },
                new List<object> { ddlSchool.SelectedValue, ddlLevel.SelectedValue },
                $@"WHERE program = '{lblProgramName.Text}'"
                );
            bool successUpdateMajor = true;
            DataTable majorInfo = Session["majorInfo"] as DataTable;
            if (successUpdateProgram)
            {
                DataTable addedMajor = Session["addedMajor"] as DataTable;
                DataTable deletedMajor = Session["deletedMajor"] as DataTable;
                foreach (DataRow row in deletedMajor.Rows)
                {
                    successUpdateMajor = DatabaseManager.DeleteData(
                        "major",
                        $@"WHERE program = '{lblProgramName.Text}' AND major = '{row["major"].ToString()}'"
                        );
                    if(successUpdateMajor == false)
                    {
                        break;
                    }
                }
                if(successUpdateMajor == true)
                {
                    foreach(DataRow row in addedMajor.Rows )
                    {
                        successUpdateMajor = DatabaseManager.InsertData(
                            "major",
                            new List<string> { "major", "program" },
                            new List<object> { row["major"].ToString(), lblProgramName.Text }
                            );
                        if (successUpdateMajor == false)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                successUpdateMajor = false;
            }
            if (successUpdateProgram && successUpdateMajor)
            {
                successfulWindow.Style["display"] = "flex";
            }
            else
            {
                failWindow.Style["display"] = "flex";
            }
        }
    }
}