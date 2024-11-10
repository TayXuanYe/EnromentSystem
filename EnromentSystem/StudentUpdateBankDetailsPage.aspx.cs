using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentUpdateBankDetailsPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["SID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            string studentId = Session["SID"].ToString();
            LoadStudentBankDetails(studentId);
        }
    }

    private void LoadStudentBankDetails(string studentId)
    {
        string connectionString = "Data Source=LAPTOP-25QCMRDF\\SQLEXPRESS; Initial Catalog=StudentDB; Integrated Security=True;";

        string query = "SELECT bank_name, bank_account, bank_holder_name, bank_verification_document FROM student WHERE sid = @StudentID";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@StudentID", studentId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                DropDownList1.SelectedValue = reader["bank_name"].ToString();
                TextBox1.Text = reader["bank_account"].ToString();
                TextBox2.Text = reader["bank_holder_name"].ToString();

                if (!DBNull.Value.Equals(reader["bank_verification_document"]))
                {
                    Literal1.Text = $"<a href='uploads/{reader["bank_verification_document"].ToString()}' target='_blank'>Download</a>";
                }
            }
            conn.Close();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string studentId = Session["SID"].ToString();
        if (string.IsNullOrEmpty(studentId))
        {
            Response.Write("Student ID not found.");
            return;
        }

        string bankName = DropDownList1.SelectedValue;
        string bankAccount = TextBox1.Text;
        string bankHolderName = TextBox2.Text;
        string verificationDocument = null;

        // If a file is uploaded, save it and get the file name
        if (FileUpload1.HasFile)
        {
            string filePath = Server.MapPath("~/uploads/") + FileUpload1.FileName;
            FileUpload1.SaveAs(filePath);
            verificationDocument = FileUpload1.FileName;
        }

        UpdateBankDetails(studentId, bankName, bankAccount, bankHolderName, verificationDocument);
    }

    private void UpdateBankDetails(string studentId, string bankName, string bankAccount, string bankHolderName, string verificationDocument)
    {
        string connectionString = "Data Source=LAPTOP-25QCMRDF\\SQLEXPRESS; Initial Catalog=StudentDB; Integrated Security=True;";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "UPDATE student SET bank_name = @BankName, bank_account = @BankAccount, bank_holder_name = @BankHolderName";

            if (verificationDocument != null)
            {
                query += ", bank_verification_document = @VerificationDocument";
            }

            query += " WHERE sid = @StudentID";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@BankName", bankName);
                cmd.Parameters.AddWithValue("@BankAccount", bankAccount);
                cmd.Parameters.AddWithValue("@BankHolderName", bankHolderName);
                cmd.Parameters.AddWithValue("@StudentID", studentId);

                if (verificationDocument != null)
                {
                    cmd.Parameters.AddWithValue("@VerificationDocument", verificationDocument);
                }

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                if (rowsAffected > 0)
                {
                    // Success message with redirect
                    string successScript = "alert('Bank details updated successfully!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SaveSuccess", successScript, true);
                }
                else
                {
                    // Error message with redirect
                    string errorScript = "alert('Error updating bank details.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SaveError", errorScript, true);
                }
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("StudentHomePage.aspx");
    }

}

