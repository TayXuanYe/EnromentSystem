using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class request_add_course : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
        string sectionId = row.Cells[3].Text; 

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

            string updateQuery = "UPDATE request_add_course SET status = @status WHERE rid = @rid";
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

            // INSERT student_taken_course
            string insertQuery = @"
            INSERT INTO student_taken_course (sid, cid, section_id, status) 
            VALUES (@sid, @cid, @sectionId, @status)";
            using (SqlCommand insertCommand = new SqlCommand(insertQuery, DatabaseManager.connection))
            {
                insertCommand.Parameters.AddWithValue("@sid", sid);
                insertCommand.Parameters.AddWithValue("@cid", cid);
                insertCommand.Parameters.AddWithValue("@sectionId", sectionId);
                insertCommand.Parameters.AddWithValue("@status", "APPROVED");

                int rowsInserted = insertCommand.ExecuteNonQuery();
                if (rowsInserted == 0)
                {
                    ShowError("Failed to insert into student_taken_course.");
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

            string updateQuery = "UPDATE request_add_course SET status = @status WHERE rid = @rid";
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
        List<string> selectColumns = new List<string> { "rid", "sid", "cid", "section_id", "reason", "status", "create_time" };

        string condition = "WHERE status = 'PENDING'";

        DataSet pendingRequestsDataSet = DatabaseManager.GetRecord("request_add_course", selectColumns, condition);

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
}
