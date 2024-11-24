using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class OnlinePaymentHistoryandReceiptpage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sid"] == null)
        {
            Response.Redirect("StudentLoginPage.aspx");
        }
        DateTime fromDate, toDate;
        if (DateTime.TryParse(TextBox1.Text, out fromDate) && DateTime.TryParse(TextBox2.Text, out toDate))
        {
            fromDate = fromDate.Date;
            toDate = toDate.Date;    
        }
    }


    protected void Button1_Click1(object sender, EventArgs e)
    {
        string studentId = Session["sid"].ToString();

        if (DateTime.TryParse(TextBox1.Text, out DateTime fromDate) && DateTime.TryParse(TextBox2.Text, out DateTime toDate))
        {
            fromDate = fromDate.Date;
            toDate = toDate.Date;

            DataTable paymentHistory = GetPaymentHistory(studentId, fromDate, toDate);

            if (paymentHistory.Rows.Count > 0)
            {
                courseTable.Rows.Clear(); // Clear existing rows, if any

                // Add the header row to the table
                TableHeaderRow headerRow = new TableHeaderRow();
                headerRow.Cells.Add(new TableHeaderCell { Text = "No" });
                headerRow.Cells.Add(new TableHeaderCell { Text = "Payment Type" });
                headerRow.Cells.Add(new TableHeaderCell { Text = "Transaction Date" });
                headerRow.Cells.Add(new TableHeaderCell { Text = "Amount" });
                headerRow.Cells.Add(new TableHeaderCell { Text = "Status" });
                headerRow.Cells.Add(new TableHeaderCell { Text = "Reprint Receipt" });
                courseTable.Rows.Add(headerRow);

                // Add data rows
                int counter = 1;
                foreach (DataRow row in paymentHistory.Rows)
                {
                    TableRow tableRow = new TableRow();
                    tableRow.Cells.Add(new TableCell { Text = counter.ToString() });
                    tableRow.Cells.Add(new TableCell { Text = row["PaymentType"].ToString() });
                    tableRow.Cells.Add(new TableCell { Text = Convert.ToDateTime(row["transactiondate"]).ToString("yyyy-MM-dd") });
                    tableRow.Cells.Add(new TableCell { Text = row["amount"].ToString() });
                    tableRow.Cells.Add(new TableCell { Text = row["paymentstatus"].ToString() });

                    TableCell receiptCell = new TableCell();
                    if (row["paymentstatus"].ToString().Equals("Approved", StringComparison.OrdinalIgnoreCase))
                    {
                        HyperLink printLink = new HyperLink
                        {
                            Text = "Reprint Receipt",
                            NavigateUrl = "#" // Update this URL when ready
                        };
                        receiptCell.Controls.Add(printLink);
                    }
                    else
                    {
                        receiptCell.Text = "N/A";
                    }
                    tableRow.Cells.Add(receiptCell);

                    courseTable.Rows.Add(tableRow);
                    counter++;
                }
            }
            else
            {
                Label1.Text = "No payment history found for the selected date range.";
            }
        }
        else
        {
            string errorScript = "alert('Please enter valid date ranges.');";
            ClientScript.RegisterStartupScript(this.GetType(), "SaveError", errorScript, true);
        }
    }

    private DataTable GetPaymentHistory(string studentId, DateTime fromDate, DateTime toDate)
    {
        DataTable dt = new DataTable();

        using (SqlConnection conn = DatabaseManager.GetConnection())
        {
            string query = @"
                SELECT 
                    'Online' AS PaymentType,
                    date AS transactiondate,
                    amount AS amount,
                    'Approved' AS paymentstatus,
                    pid
                FROM payment
                WHERE sid = @sid
                AND date BETWEEN @fromDate AND @toDate
                ORDER BY transactiondate ASC";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@sid", studentId);
            cmd.Parameters.AddWithValue("@fromDate", fromDate);
            cmd.Parameters.AddWithValue("@toDate", toDate);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
        }

        return dt;
    }
}
