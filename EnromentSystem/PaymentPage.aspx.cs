using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class PaymentPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string studentId = Session["sid"] as string;

            if (!string.IsNullOrEmpty(studentId))
            {
                LoadStudentDetails(studentId);
                CalculateNetAmount(studentId);
                PopulatePaymentTable(studentId);
            }
            else
            {
                Response.Redirect("StudentLoginPage.aspx");
            }
        }
    }

    private void LoadStudentDetails(string studentId)
    {
        string query = @"SELECT name AS StudentName, 
                             sid AS StudentID, 
                             ic_or_passport AS IDOrPassport, 
                             program AS Program
                     FROM student 
                     WHERE sid = @studentID";

        string semquery = @"SELECT TOP 1 semester AS Semester FROM current_semester";

        try
        {
            using (SqlConnection conn = DatabaseManager.GetConnection())
            {
                conn.Open();

                // Load student details
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@studentID", studentId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                           
                            Label1.Text = reader["StudentName"]?.ToString() ?? "N/A";
                            Label2.Text = reader["StudentID"]?.ToString() ?? "N/A";
                            Label3.Text = reader["IDOrPassport"]?.ToString() ?? "N/A";
                            Label4.Text = reader["Program"]?.ToString() ?? "N/A";
                        }
                        else
                        {
                            Label1.Text = "Student not found";
                            Label2.Text = "";
                            Label3.Text = "";
                            Label4.Text = "";
                        }
                    }
                }

                // Load current semester
                using (SqlCommand semCommand = new SqlCommand(semquery, conn))
                {
                    using (SqlDataReader semReader = semCommand.ExecuteReader())
                    {
                        if (semReader.Read())
                        {
                            Label5.Text = semReader["Semester"]?.ToString() ?? "N/A";
                            Label6.Text = semReader["Semester"]?.ToString() ?? "N/A";
                        }
                        else
                        {
                            Label5.Text = "Semester not found";
                        }
                    }
                }
            }
        }
        catch (SqlException ex)
        {
          
            Label1.Text = "Error loading student details";
            Debug.WriteLine($"SQL Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
        }
    }

    private void CalculateNetAmount(string studentId)
    {
        decimal totalTakenAmount = GetTotalTakenAmount(studentId);
        decimal addedCoursesAmount = GetAddedCoursesAmount(studentId);
        decimal droppedCoursesAmount = GetDroppedCoursesAmount(studentId);
        decimal totalPaidMaybank = GetTotalPaidMaybank(studentId);
        decimal totalPaidUpload = GetTotalPaidUpload(studentId);
        decimal scholarshipPercentage = GetScholarshipPercentage(studentId);

      
        // Calculate the net amount
        decimal grossAmount = totalTakenAmount + addedCoursesAmount - droppedCoursesAmount;
        decimal totalPaid = totalPaidMaybank + totalPaidUpload;
        decimal scholarshipAmount = (grossAmount * scholarshipPercentage) / 100;
        decimal netAmount = grossAmount - totalPaid - scholarshipAmount;
       

        // Update the labels with the calculated amounts
        Label7.Text = grossAmount.ToString("C2"); 
        Label8.Text = scholarshipAmount.ToString("C2"); 
        Label13.Text = totalPaid.ToString("C2");
        Label9.Text = netAmount.ToString("C2"); 
        
    }

    private decimal GetTotalTakenAmount(string studentId)
    {
        string query = @"
        SELECT ISNULL(SUM(CAST(c.price AS float)), 0) AS total_amount
        FROM student_taken_course stc
        JOIN course c ON stc.cid = c.cid
        WHERE stc.sid = @sid AND stc.status IN ('TAKEN', 'FAIL')";

        

        return ExecuteQueryAndGetDecimal(query, studentId);

    }

    private decimal GetAddedCoursesAmount(string studentId)
    {
        string query = @"
            SELECT ISNULL(SUM(CAST(c.price AS float)), 0) AS added_courses_amount
            FROM request_add_course rac
            JOIN course c ON rac.cid = c.cid
            WHERE rac.sid = @sid AND rac.status = 'Approved'";

        return ExecuteQueryAndGetDecimal(query, studentId);
    }

    private decimal GetDroppedCoursesAmount(string studentId)
    {
        string query = @"
            SELECT ISNULL(SUM(CAST(c.price AS float)), 0) AS dropped_courses_amount
            FROM request_drop_course rdc
            JOIN course c ON rdc.cid = c.cid
            WHERE rdc.sid = @sid AND rdc.status = 'Approved'";

        return ExecuteQueryAndGetDecimal(query, studentId);
    }

    private decimal GetTotalPaidMaybank(string studentId)
    {
        string query = @"
            SELECT ISNULL(SUM(p.TotalAmount), 0) AS total_paid_maybank
            FROM MaybankPay p
            WHERE p.sid = @sid AND p.Status = 'Approved'";

        return ExecuteQueryAndGetDecimal(query, studentId);
    }

    private decimal GetTotalPaidUpload(string studentId)
    {
        string query = @"
            SELECT ISNULL(SUM(p.AmountPaid), 0) AS total_paid_upload
            FROM PaymentUpload p
            WHERE p.sid = @sid AND p.PaymentStatus = 'Approved'";

        return ExecuteQueryAndGetDecimal(query, studentId);
    }

    private decimal GetScholarshipPercentage(string studentId)
    {
        string query = @"
            SELECT scholarship
            FROM student
            WHERE sid = @sid";

        return ExecuteQueryAndGetDecimal(query, studentId);
    }

    private decimal ExecuteQueryAndGetDecimal(string query, string studentId)
    {
        try
        {
            using (SqlConnection conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@sid", studentId);
                object result = cmd.ExecuteScalar();
                return ConvertToDecimal(result);
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Error: {ex.Message}");
            Label1.Text = "Error loading data. Please try again later.";
            return 0;  
        }
    }

    private decimal ConvertToDecimal(object value)
{
    if (value == null || value == DBNull.Value) return 0;
    if (value is decimal decimalValue) return decimalValue;
    if (value is float floatValue) return Convert.ToDecimal(floatValue);
    return 0;
}


    private void PopulatePaymentTable(string studentId)
    {
        string paymentQuery = @"
        SELECT 
            OrderReference AS ReferenceNo,
            'Invoice' AS PaymentType,
            TransactionDate,
            TotalAmount AS Amount,
            0 AS AmountSettled
        FROM MaybankPay
        WHERE sid = @sid
        AND Status = 'Approved'

        UNION ALL

        SELECT 
            ReferenceNo,
            'Invoice' AS PaymentType,
            TransactionDate,
            AmountPaid AS Amount,
            0 AS AmountSettled
        FROM PaymentUpload
        WHERE sid = @sid
        AND PaymentStatus = 'Approved'

        ORDER BY TransactionDate ASC";

        try
        {
            using (SqlConnection conn = DatabaseManager.GetConnection())
            {
                conn.Open();

                // Fetch payment records
                SqlCommand paymentCmd = new SqlCommand(paymentQuery, conn);
                paymentCmd.Parameters.AddWithValue("@sid", studentId);
                SqlDataReader paymentReader = paymentCmd.ExecuteReader();

                List<PaymentRecord> paymentRecords = new List<PaymentRecord>();

                while (paymentReader.Read())
                {
                    paymentRecords.Add(new PaymentRecord
                    {
                        ReferenceNo = paymentReader["ReferenceNo"].ToString(),
                        PaymentType = paymentReader["PaymentType"].ToString(),
                        TransactionDate = Convert.ToDateTime(paymentReader["TransactionDate"]),
                        Amount = Convert.ToDecimal(paymentReader["Amount"]),
                        AmountSettled = 0
                    });
                }

                paymentReader.Close();

                
                decimal cumulativeSettled = 0;
                foreach (var record in paymentRecords.OrderBy(r => r.TransactionDate))
                {
                    record.AmountSettled = cumulativeSettled;
                    cumulativeSettled += record.Amount;
                }

               
                Label13.Text = cumulativeSettled.ToString("C2");

                
                BindPaymentTable(paymentRecords);
            }
        }
        catch (SqlException ex)
        {
            
            Label1.Text = "Error loading payment records.";
            Console.WriteLine($"SQL Error: {ex.Message}");
        }
    }

    private void BindPaymentTable(List<PaymentRecord> paymentRecords)
    {
        
        if (courseTable.Rows.Count == 0)
        {
            TableHeaderRow header = new TableHeaderRow();
            header.Cells.AddRange(new TableHeaderCell[]
            {
            new TableHeaderCell { Text = "Particulars" },
            new TableHeaderCell { Text = "Type" },
            new TableHeaderCell { Text = "Document Date/Instalment Due Date" },
            new TableHeaderCell { Text = "Amount (RM)" },
            new TableHeaderCell { Text = "Amount Settled (RM)" }
            });
            courseTable.Rows.Add(header);
        }

        // Add each payment record as a new row
        foreach (var record in paymentRecords)
        {
            TableRow row = new TableRow();

           
            TableCell particularsCell = new TableCell();
            HyperLink referenceLink = new HyperLink
            {
                Text = record.ReferenceNo,
                NavigateUrl = $"#",
                CssClass = "hyperlink-style"
            };
            particularsCell.Controls.Add(referenceLink);
            row.Cells.Add(particularsCell);

            
            TableCell typeCell = new TableCell { Text = record.PaymentType };
            row.Cells.Add(typeCell);

           
            TableCell dateCell = new TableCell
            {
                Text = record.TransactionDate.ToString("yyyy-MM-dd")
            };
            row.Cells.Add(dateCell);

            
            TableCell amountCell = new TableCell
            {
                Text = record.Amount.ToString("C2")
            };
            row.Cells.Add(amountCell);

            
            TableCell settledCell = new TableCell
            {
                Text = record.AmountSettled.ToString("C2")
            };
            row.Cells.Add(settledCell);

            courseTable.Rows.Add(row);
        }
    }


    public class PaymentRecord
    {
        public string ReferenceNo { get; set; }
        public string PaymentType { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountSettled { get; set; }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaymentPaybyflywire.aspx");
    }

       
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaybyotherRootPage.aspx");
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("StudentHomePage.aspx");
    }
}



