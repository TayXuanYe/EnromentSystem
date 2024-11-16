using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminAddProgramAndMajorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateSchool();
            DataTable majorInfo = new DataTable();
            majorInfo.Columns.Add("major", typeof(string));
            gvMajorInfo.DataSource = majorInfo;
            gvMajorInfo.DataBind();
            Session["majorInfo"] = majorInfo;
        }
    }

    //program details part
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

    private bool CheckProgramNameIsExist(string name)
    {
        DataSet dataSet = DatabaseManager.GetRecord(
            "program",
            new List<string> { "program" },
            $@"WHERE program = '{name}'"
            );
        if (dataSet != null)
        {
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    protected void CheckProgramNameIsExist_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = !CheckProgramNameIsExist(txtProgramName.Text);
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
            }
        }
        dataTable.AcceptChanges();
        gvMajorInfo.DataSource = dataTable;
        gvMajorInfo.DataBind();
    }

    protected void btnAddMajor_Click(object sender, EventArgs e)
    {
        DataTable majorInfo = Session["majorInfo"] as DataTable;
        DataRow row = majorInfo.NewRow();
        if(!CheckMajorExist(txtMajorName.Text))
        {
            row["major"] = txtMajorName.Text.ToUpper();
            majorInfo.Rows.Add(row);
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
    //button contain part
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMaintainProgramAndMajorPage.aspx");
    }

    protected void btnAddProgram_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            bool successInsertProgram = DatabaseManager.InsertData(
                "program",
                new List<string> { "program", "school", "level" },
                new List<object> { txtProgramName.Text.ToUpper(), ddlSchool.SelectedValue, ddlLevel.SelectedValue}
                );
            bool successInsertMajor = true;
            DataTable majorInfo = Session["majorInfo"] as DataTable;
            if (successInsertProgram)
            {
                foreach (DataRow row in majorInfo.Rows)
                {
                    successInsertMajor = DatabaseManager.InsertData(
                        "major",
                        new List<string> { "major", "program" },
                        new List<object> { row["major"].ToString(), txtProgramName.Text.ToUpper() }
                        );
                    if (successInsertMajor == false)
                    {
                        break;
                    }
                }
            }
            else
            {
                successInsertMajor = false;
            }
            if (successInsertProgram&& successInsertMajor)
            {
                successfulWindow.Style["display"] = "flex";
            }
        }
    }
}