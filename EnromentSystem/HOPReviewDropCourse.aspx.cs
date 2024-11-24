using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HOPReviewDropCourse : System.Web.UI.Page
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
            HandleAcceptRequest(rid, sid, cid);
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

            string status = DataBinder.Eval(e.Row.DataItem, "status").ToString();

            Button acceptButton = e.Row.FindControl("btnAccept") as Button;
            Button notAcceptButton = e.Row.FindControl("btnNotAccept") as Button;

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

    private void HandleAcceptRequest(string rid, string sid, string cid)
    {
        try
        {
            DatabaseManager.ConnectDatabase();

            if (DatabaseManager.connection == null || DatabaseManager.connection.State != System.Data.ConnectionState.Open)
            {
                ShowError("Failed to connect to the database.");
                return;
            }

            string updateQuery = "UPDATE request_drop_course SET status = @status WHERE rid = @rid";
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

            string deleteQuery = "DELETE FROM student_taken_course WHERE cid = @cid";
            using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, DatabaseManager.connection))
            {
                deleteCommand.Parameters.AddWithValue("@cid", cid);

                int rowsAffected = deleteCommand.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    ShowError("Failed to update student_taken_course.");
                    return;
                }
            }


            //get current semester
            string getSemesterQuery = "SELECT semester FROM current_semester";
            string semester = "";
            using (SqlCommand command = new SqlCommand(getSemesterQuery, DatabaseManager.connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        semester = reader["semester"].ToString();
                    }
                }
            }

            //get price for this course
            string getPriceQuery = "SELECT price FROM course WHERE cid = @cid";
            double price = 0.0;
            using (SqlCommand command = new SqlCommand(getPriceQuery, DatabaseManager.connection))
            {
                command.Parameters.AddWithValue("@cid", cid);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            price = double.Parse(reader["price"].ToString());
                        }
                    }
                    else
                    {
                        ShowError("Get Price Fail");
                    }
                }
            }
            //get scolar ship
            double scholarShipAmount = 0.0;
            string getScholarShip = "SELECT scholarship FROM student WHERE sid = @sid";
            using (SqlCommand command = new SqlCommand(getScholarShip, DatabaseManager.connection))
            {
                command.Parameters.AddWithValue("@sid", sid);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            scholarShipAmount = price * double.Parse(reader["scholarship"].ToString()) / 100.0;
                        }
                    }
                    else
                    {
                        ShowError("Get Scholarship Fail");
                    }
                }
            }

            //Add payment record
            DatabaseManager.InsertData(
                "payment",
                new List<string> { "sid", "process", "particulars", "documentNo", "session", "amount" },
                new List<object> { sid, "DROP", "TUIT", "IM24BCS100004", semester, price }
                );
            //Add scohloar ship
            DatabaseManager.InsertData(
                "payment",
                new List<string> { "sid", "process", "particulars", "documentNo", "session", "amount" },
                new List<object> { sid, "DROP", "SYSSCHO", "IM24BCS100004", semester, scholarShipAmount * -1 }
                );
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

            string updateQuery = "UPDATE request_drop_course SET status = @status WHERE rid = @rid";
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
        List<string> selectColumns = new List<string> { "rid", "sid", "cid", "reason", "status", "create_time" };

        string condition = "WHERE status = 'PENDING'";

        DataSet pendingRequestsDataSet = DatabaseManager.GetRecord("request_drop_course", selectColumns, condition);

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