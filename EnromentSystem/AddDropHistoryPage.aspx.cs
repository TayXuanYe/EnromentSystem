using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddDropHistoryPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sid"] != null)
        {
            SetAddDropHistoryTable();
        }
        else
        {
            Response.Redirect("StudentLoginPage.aspx");
        }
    }

    private void SetAddDropHistoryTable()
    {
        DataTable displayTable = new DataTable();
        displayTable.Columns.Add("no", typeof(int));
        displayTable.Columns.Add("courseInfo",typeof(string));
        displayTable.Columns.Add("date", typeof(string));
        int count = 0;
        //Drop course
        DataSet courseDropDataSet = DatabaseManager.GetRecord(
            "request_drop_course",
            new List<string> {
            "cid",
            "status",
            "reason",
            "FORMAT(create_time, 'yyyy-MM-dd') AS DateOnly"
            },
            "WHERE sid = \'" + Session["sid"] + "\' " +
            "AND status != \'HOLD\'"
        );
        if(courseDropDataSet != null)
        {
            DataTable dt = courseDropDataSet.Tables[0];
            foreach(DataRow row in dt.Rows )
            {
                count++;
                DataRow displayRow = displayTable.NewRow();
                displayRow["no"] = count;
                displayRow["date"] = row["DateOnly"];
                displayRow["courseInfo"] = 
                    "You have selected to <b>DROP</b> Course <b>" +
                    row["cid"].ToString() +
                    "</b> and is's <span class=\"approve-status\">" +
                    row["status"].ToString() +
                    "</span> for your HOP Approve.<br>" +
                    "<span class=\"request-reason\"><b>Reason:</b> " +
                    row["reason"].ToString() +
                    "</span>";
                displayTable.Rows.Add(displayRow);
            }
        }
        
        //Add course
        DataSet courseAddDataSet = DatabaseManager.GetRecord(
            "request_add_course",
            new List<string> {
            "request_add_course.cid",
            "status",
            "reason",
            "s.name",
            "FORMAT(create_time, 'yyyy-MM-dd') AS DateOnly"
            },
            "INNER JOIN course AS c " +
            "ON request_add_course.cid = c.cid " +
            "INNER JOIN section AS s " +
            "ON request_add_course.section_id = s.sid " +
            "WHERE request_add_course.sid = \'" + Session["sid"] + "\' " +
            "AND status != \'HOLD\'"
        );
        if(courseAddDataSet != null)
        {
            DataTable dt = courseAddDataSet.Tables[0];
            foreach(DataRow row in dt.Rows )
            {
                count++;
                DataRow displayRow = displayTable.NewRow();
                displayRow["no"] = count;
                displayRow["date"] = row["DateOnly"];
                displayRow["courseInfo"] =
                    "You have selected to <b>ADD</b> Course <b>" +
                    row["cid"].ToString() +
                    "</b> " +
                    "under <b>" +
                    row["name"] +
                    "</b> section " +
                    "and is's <span class=\"approve-status\">" +
                    row["status"].ToString() +
                    "</span> for your HOP Approve.<br>" +
                    "<span class=\"request-reason\"><b>Reason:</b> " +
                    row["reason"].ToString() +
                    "</span>";
                displayTable.Rows.Add(displayRow);
            }
        }

        //change section
        DataSet sectionChangeDataSet = DatabaseManager.GetRecord(
            "request_change_section",
            new List<string> {
            "request_change_section.cid",
            "status",
            "reason",
            "s1.name AS currestSection",
            "s2.name AS targetSection",
            "FORMAT(create_time, 'yyyy-MM-dd') AS DateOnly"
            },
            "INNER JOIN course AS c " +
            "ON request_change_section.cid = c.cid " +
            "INNER JOIN section AS s1 " +
            "ON request_change_section.current_section_id = s1.sid " +
            "INNER JOIN section AS s2 " +
            "ON request_change_section.target_section_id = s2.sid " +
            "WHERE request_change_section.sid = \'" + Session["sid"] + "\' " +
            "AND status != \'HOLD\'"
        );
        if (sectionChangeDataSet != null)
        {
            DataTable dt = sectionChangeDataSet.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                count++;
                DataRow displayRow = displayTable.NewRow();
                displayRow["no"] = count;
                displayRow["date"] = row["DateOnly"];
                displayRow["courseInfo"] =
                    "You have selected to <b>CHANGE</b> Section from <b>" +
                    row["currestSection"] +
                    "</b> to <b>" +
                    row["targetSection"] +
                    "</b> " +
                    "of Course <b>" +
                    row["cid"].ToString() +
                    "</b> and  is's <span class=\"approve-status\">" +
                    row["status"].ToString() +
                    "</span> for your HOP Approve.<br>" +
                    "<span class=\"request-reason\"><b>Reason:</b> " +
                    row["reason"].ToString() +
                    "</span>";
                displayTable.Rows.Add(displayRow);
            }
        }

        if(displayTable.Rows.Count == 0)
        {
            DataRow displayRow = displayTable.NewRow();
            displayRow["no"] = 1;
            displayRow["date"] = "";
            displayRow["courseInfo"] = "";
            displayTable.Rows.Add(displayRow);
        }

        GridViewAddDropHistory.DataSource = displayTable;
        GridViewAddDropHistory.DataBind();

    }
}