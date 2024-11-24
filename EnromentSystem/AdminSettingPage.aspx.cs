using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminSettingPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet dataSet = DatabaseManager.GetRecord(
            "current_semester",
            new List<string> { "semester", "credit_hour" }
            );
            if (dataSet != null)
            {
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    txtCreditHours.Text = row["credit_hour"].ToString();
                    txtSemesterName.Text = row["semester"].ToString();
                    hfCurrenSemester.Value = row["semester"].ToString();
                }
            }
            SetButtonDetails();
        }

        
    }

    private void SetButtonDetails()
    {
        DataSet data = DatabaseManager.GetRecord(
            "system_function_available",
            new List<string> { "system_function", "available" }
            );
        if (data != null)
        {
            foreach (DataRow row in data.Tables[0].Rows)
            {
                switch (row["system_function"].ToString())
                {
                    case "ENROL":
                        if ((bool)row["available"])
                        {
                            btnEnroll.Text = "Allow";
                            btnEnroll.Style["background-color"] = "#4caf50";
                            btnEnroll.Style["color"] = "#fff";
                        }
                        else
                        {
                            btnEnroll.Text = "Not Allow";
                            btnEnroll.Style["background-color"] = "#ccc";
                            btnEnroll.Style["color"] = "#000";
                        }
                        break;

                    case "ADDDROP":
                        if ((bool)row["available"])
                        {
                            btnAddDrop.Text = "Allow";
                            btnAddDrop.Style["background-color"] = "#4caf50";
                            btnAddDrop.Style["color"] = "#fff";
                        }
                        else
                        {
                            btnAddDrop.Text = "Not Allow";
                            btnAddDrop.Style["background-color"] = "#ccc";
                            btnAddDrop.Style["color"] = "#000";
                        }
                        break;
                }
            }
        }
    }

    protected void btnUpdateSemester_Click(object sender, EventArgs e)
    {
        bool success = DatabaseManager.UpdateData(
           "current_semester",
           new List<string> { "semester", "credit_hour" },
           new List<object> { txtSemesterName.Text, txtCreditHours.Text },
           $@"WHERE semester = '{hfCurrenSemester.Value}'"
           );
        SetButtonDetails();
        if (success)
        {
            successfulWindow.Style["display"] = "flex";
        }
    }

    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        int available = 0;
        if (btnEnroll.Text == "Allow")
        {
            available = 0;
        }
        else
        {
            available = 1;
        }

        DatabaseManager.UpdateData(
            "system_function_available",
            new List<string> { "available" },
            new List<object> { available },
            $@"WHERE system_function = 'ENROL'"
            );
        SetButtonDetails();
    }

    protected void btnAddDrop_Click(object sender, EventArgs e)
    {
        int available = 0;
        if (btnAddDrop.Text == "Allow")
        {
            available = 0;
        }
        else
        {
            available = 1;
        }

        DatabaseManager.UpdateData(
            "system_function_available",
            new List<string> { "available" },
            new List<object> { available },
            $@"WHERE system_function = 'ADDDROP'"
            );
        SetButtonDetails();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        successfulWindow.Style["display"] = "none";
    }
}