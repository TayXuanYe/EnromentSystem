using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                LoadPaymentHistory(studentId);
                CalculateNetAmount(studentId);

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
            Console.WriteLine($"SQL Error: {ex.Message}");
        }
    }
    private void LoadPaymentHistory(string studentId)
    {
        using (SqlConnection conn = DatabaseManager.GetConnection())
        {
           
            string query = @"SELECT date, amount, documentNo 
                         FROM payment 
                         WHERE sid = @sid
                         ORDER BY date";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@sid", studentId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

          
            decimal totalSettled = 0;

            while (reader.Read())
            {
                // Retrieve values from the database
                DateTime paymentDate = Convert.ToDateTime(reader["date"]);
                decimal amountPaid = Convert.ToDecimal(reader["amount"]);
                string documentNo = reader["documentNo"]?.ToString() ?? "N/A";

              
                if (totalSettled == 0)
                {
                    totalSettled = amountPaid; 
                }
                else
                {
                   
                    totalSettled += amountPaid;
                }

               
                TableRow row = new TableRow();

               
                row.Cells.Add(new TableCell { Text = documentNo });

              
                row.Cells.Add(new TableCell { Text = "Payment" });

                
                row.Cells.Add(new TableCell { Text = paymentDate.ToString("yyyy-MM-dd") });

                
                row.Cells.Add(new TableCell { Text = amountPaid.ToString("F2") });

              
                row.Cells.Add(new TableCell { Text = totalSettled.ToString("F2") });

                
                courseTable.Rows.Add(row);
            }

            conn.Close();
        }
    }

    private void CalculateNetAmount(string studentId)
    {

        using (SqlConnection conn = DatabaseManager.GetConnection())
        {

            conn.Open();

           
            string feeQuery = @"SELECT SUM(c.price) AS TotalFees
                            FROM student_taken_course stc
                            JOIN course c ON stc.cid = c.cid
                            WHERE stc.sid = @studentID";

            SqlCommand feeCmd = new SqlCommand(feeQuery, conn);
            feeCmd.Parameters.AddWithValue("@studentID", studentId);

            
            object totalFeesResult = feeCmd.ExecuteScalar();
            decimal totalFees = 0;

            if (totalFeesResult != DBNull.Value)
            {
                totalFees = Convert.ToDecimal(totalFeesResult);
            }

            Session["NetAmount"] = totalFees;
           
            Label9.Text = totalFees.ToString("F2");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
     {
       Response.Redirect("PaymentPaybyflywire.aspx");
     }

       
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Paybyother.aspx");
        }


}



