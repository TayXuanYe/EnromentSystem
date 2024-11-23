using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HOPReviewChangeSection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["hid"] == null)
        {
            Response.Redirect("HopLoginPage.aspx");
        }

        if (!IsPostBack)
        {
            LoadPendingRequests();
        }
    }

    protected void gvRequests_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        int rowIndex = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gvRequests.Rows[rowIndex];

        string rid = row.Cells[0].Text;
        string sid = row.Cells[1].Text;
        string cid = row.Cells[2].Text;
        string sectionId = row.Cells[4].Text;

        if (e.CommandName == "Accept")
        {
            HandleAcceptRequest(rid, sid, cid, sectionId);
        }
        else if (e.CommandName == "NotAccept")
        {
            HandleNotAcceptRequest(rid);
        }

        LoadPendingRequests();
    }

    protected void gvRequests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // 获取状态列的值
            string status = DataBinder.Eval(e.Row.DataItem, "status").ToString();

            // 从 TemplateField 中找到按钮
            Button acceptButton = e.Row.FindControl("btnAccept") as Button;
            Button notAcceptButton = e.Row.FindControl("btnNotAccept") as Button;

            // 检查按钮是否存在并设置可见性
            if (acceptButton != null && notAcceptButton != null)
            {
                if (status == "PENDING")
                {
                    acceptButton.Visible = true;
                    notAcceptButton.Visible = true;
                }
                else
                {
                    acceptButton.Visible = false;
                    notAcceptButton.Visible = false;
                }
            }
        }
    }

    private void HandleAcceptRequest(string rid, string sid, string cid, string sectionId)
    {
        try
        {
            DatabaseManager.ConnectDatabase();

            if (DatabaseManager.connection == null || DatabaseManager.connection.State != System.Data.ConnectionState.Open)
            {
                ShowError("Failed to connect to the database.");
                return;
            }

            string updateQuery = "UPDATE request_change_section SET status = @status WHERE rid = @rid";
            using (SqlCommand updateCommand = new SqlCommand(updateQuery, DatabaseManager.connection))
            {
                updateCommand.Parameters.AddWithValue("@status", "APPROVE");
                updateCommand.Parameters.AddWithValue("@rid", rid);

                int rowsAffected = updateCommand.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    ShowError("Failed to update request status to APPROVE.");
                    return;
                }
            }

            // update student_taken_course
            string updateQuery2 = "UPDATE student_taken_course SET section_id = @sectionId WHERE cid = @cid AND sid=@sid ";
            using (SqlCommand updateCommand = new SqlCommand(updateQuery2, DatabaseManager.connection))
            {
                updateCommand.Parameters.AddWithValue("@sectionId", sectionId);
                updateCommand.Parameters.AddWithValue("@cid", cid);
                updateCommand.Parameters.AddWithValue("@sid", sid);

                int rowsAffected = updateCommand.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    ShowError("Failed to update student_taken_course.");
                    return;
                }
            }
        }
        catch (Exception ex)
        {

            ShowError("An error occurred: " + ex.Message);
        }
        finally
        {

            if (DatabaseManager.connection != null && DatabaseManager.connection.State == System.Data.ConnectionState.Open)
            {
                DatabaseManager.connection.Close();
            }
        }
    }



    private void HandleNotAcceptRequest(string rid)
    {
        try
        {
            DatabaseManager.ConnectDatabase();

            if (DatabaseManager.connection == null || DatabaseManager.connection.State != System.Data.ConnectionState.Open)
            {
                ShowError("Failed to connect to the database.");
                return;
            }

            string updateQuery = "UPDATE request_change_section SET status = @status WHERE rid = @rid";
            using (SqlCommand updateCommand = new SqlCommand(updateQuery, DatabaseManager.connection))
            {
                updateCommand.Parameters.AddWithValue("@status", "NOT APPROVE");
                updateCommand.Parameters.AddWithValue("@rid", rid);

                int rowsAffected = updateCommand.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    ShowError("Failed to update request status to NOT APPROVE.");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ShowError("An error occurred: " + ex.Message);
        }
        finally
        {
            if (DatabaseManager.connection != null && DatabaseManager.connection.State == System.Data.ConnectionState.Open)
            {
                DatabaseManager.connection.Close();
            }
        }
    }

    private void LoadPendingRequests()
    {
        List<string> selectColumns = new List<string> { "rid", "sid", "cid", "current_section_id", "target_section_id", "reason", "status", "create_time" };

        string condition = "WHERE status = 'PENDING'";

        DataSet pendingRequestsDataSet = DatabaseManager.GetRecord("request_change_section", selectColumns, condition);

        if (pendingRequestsDataSet != null && pendingRequestsDataSet.Tables.Count > 0)
        {
            gvRequests.DataSource = pendingRequestsDataSet.Tables[0];
            gvRequests.DataBind();
        }
        else
        {
            gvRequests.DataSource = null;
            gvRequests.DataBind();
        }
    }

    private void ShowError(string message)
    {
        Response.Write("<script>alert('" + message + "');</script>");
    }

    protected void Return_Click(object sender, EventArgs e)
    {
        Response.Redirect("HOPReviewHomePage.aspx");
    }
}