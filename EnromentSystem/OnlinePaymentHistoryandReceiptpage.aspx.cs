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
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        // Toggle the visibility of the calendar below TextBox1
        Calendar1.Visible = !Calendar1.Visible;
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        // Toggle the visibility of the calendar below TextBox2
        Calendar2.Visible = !Calendar2.Visible;
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        // Set the selected date from Calendar1 to TextBox1
        TextBox1.Text = Calendar1.SelectedDate.ToShortDateString();
        // Hide the calendar after selecting the date
        Calendar1.Visible = false;
    }

    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        // Set the selected date from Calendar2 to TextBox2
        TextBox2.Text = Calendar2.SelectedDate.ToShortDateString();
        // Hide the calendar after selecting the date
        Calendar2.Visible = false;
    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        string studentId = Session["sid"].ToString();

        DateTime fromDate, toDate;
        if (DateTime.TryParse(TextBox1.Text, out fromDate) && DateTime.TryParse(TextBox2.Text, out toDate))
        {
            // Remove the time portion to keep only the date
            fromDate = fromDate.Date;
            toDate = toDate.Date;

            // Fetch the payment history for the student within the selected date range
            DataTable paymentHistory = GetPaymentHistory(studentId, fromDate, toDate);

            if (paymentHistory.Rows.Count > 0)
            {
                courseTable.Rows.Clear();

                // Add the header row to the table
                if (courseTable.Rows.Count == 0)
                {
                    TableHeaderRow headerRow = new TableHeaderRow();
                    headerRow.Cells.Add(new TableHeaderCell { Text = "No" });
                    headerRow.Cells.Add(new TableHeaderCell { Text = "Payment Type" });
                    headerRow.Cells.Add(new TableHeaderCell { Text = "Transaction Date" });
                    headerRow.Cells.Add(new TableHeaderCell { Text = "Amount" });
                    headerRow.Cells.Add(new TableHeaderCell { Text = "Status" });
                    headerRow.Cells.Add(new TableHeaderCell { Text = "Reprint Receipt" }); // Still adding the column header
                    courseTable.Rows.Add(headerRow);
                }

                // Add data rows
                int counter = 1;
                foreach (DataRow row in paymentHistory.Rows)
                {
                    TableRow tableRow = new TableRow();

                    tableRow.Cells.Add(new TableCell { Text = counter.ToString() });
                    tableRow.Cells.Add(new TableCell { Text = row["PaymentType"].ToString() });
                    tableRow.Cells.Add(new TableCell { Text = Convert.ToDateTime(row["TransactionDate"]).ToString("yyyy-MM-dd") });
                    tableRow.Cells.Add(new TableCell { Text = row["Amount"].ToString() });
                    tableRow.Cells.Add(new TableCell { Text = row["PaymentStatus"].ToString() });

                    TableCell receiptCell = new TableCell();
                    if (row["PaymentStatus"].ToString().Equals("Approved", StringComparison.OrdinalIgnoreCase))
                    {
                        HyperLink printLink = new HyperLink
                        {
                            Text = "Print",
                            NavigateUrl = "~/Generatepdf.aspx?sid=" + HttpUtility.UrlEncode(Session["sid"].ToString()) +
                                          "&paymentId=" + HttpUtility.UrlEncode(row["PaymentID"].ToString())  // Ensure PaymentID is passed here
                        };
                        receiptCell.Controls.Add(printLink);
                    }
                    else
                    {
                        receiptCell.Text = "N/A"; // Or leave it blank
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
            Label1.Text = "Please enter valid date ranges.";
        }

    }

    private DataTable GetPaymentHistory(string studentId, DateTime fromDate, DateTime toDate)
    {
        DataTable dt = new DataTable();

        using (SqlConnection conn = DatabaseManager.GetConnection())
        {
            string query = @"
        SELECT 
            PaymentUploadID AS PaymentID,  -- Adding PaymentID
            'Online' AS PaymentType,
            TransactionDate AS transactiondate,
            AmountPaid AS amount,
            PaymentStatus AS paymentstatus
        FROM PaymentUpload
        WHERE sid = @sid
        AND TransactionDate BETWEEN @fromDate AND @toDate

        UNION ALL

        SELECT
            PaymentID,  -- Adding PaymentID
            'Administrative Collection' AS PaymentType,
            TransactionDate AS transactiondate,
            TotalAmount AS amount,
            Status AS paymentstatus
        FROM MaybankPay
        WHERE sid = @sid
        AND TransactionDate BETWEEN @fromDate AND @toDate

        ORDER BY TransactionDate ASC";

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