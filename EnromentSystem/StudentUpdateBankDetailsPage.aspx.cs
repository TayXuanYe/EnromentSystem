using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Web.UI.WebControls;

public partial class StudentUpdateBankDetailsPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["sid"] == null)
            {
                Response.Redirect("StudentLoginPage.aspx");
                return;
            }
            string studentId = Session["sid"].ToString();
            LoadStudentBankDetails(studentId);
            PopulateBankName();
        }
    }

    private void PopulateBankName()
    {
        //get bank details
        DataSet studentSet = DatabaseManager.GetRecord(
               "bank",
               new List<string> { "bank_name" }
           );

        DataTable dt = studentSet.Tables[0];
        List<string> value = new List<string>();
        foreach (DataRow row in dt.Rows)
        {
            value.Add(row["bank_name"].ToString());
        }
        UIComponentGenerator.PopulateDropDownList(ddlBankName, value, value);
    }

    private void LoadStudentBankDetails(string studentId)
    {
        string query = "SELECT bank_name, bank_account, bank_holder_name, bank_verification_document FROM student WHERE sid = @StudentID";

        using (SqlConnection conn = DatabaseManager.GetConnection())
        {
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@StudentID", studentId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                ddlBankName.SelectedValue = reader["bank_name"].ToString();
                txtAcountNo.Text = reader["bank_account"].ToString();
                txtHolderName.Text = reader["bank_holder_name"].ToString();

                if (!DBNull.Value.Equals(reader["bank_verification_document"]))
                {
                    Literal1.Text = $"<a href='/uploads/{reader["bank_verification_document"].ToString()}' target='_blank'>Download</a>";
                }
            }
            conn.Close();

        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string studentId = Session["SID"].ToString();
        if (string.IsNullOrEmpty(studentId))
        {
            Response.Write("Student ID not found.");
            return;
        }

        string bankName = ddlBankName.SelectedValue;
        string bankAccount = txtAcountNo.Text;
        string bankHolderName = txtHolderName.Text;
        string verificationDocument = null;

        // If a file is uploaded, save it and get the file name
        if (fileUploadVerificationDocument.HasFile)
        {
            string originalExtension = Path.GetExtension(fileUploadVerificationDocument.FileName);
            string filePath = Server.MapPath("~/uploads/") + Session["sid"] + "_Bank_Verification_Document" + originalExtension;
            fileUploadVerificationDocument.SaveAs(filePath);
            verificationDocument = Session["sid"] + "_Bank_Verification_Document" + originalExtension;
        }

        UpdateBankDetails(studentId, bankName, bankAccount, bankHolderName, verificationDocument);
    }

    private void UpdateBankDetails(string studentId, string bankName, string bankAccount, string bankHolderName, string verificationDocument)
    {
        using (SqlConnection conn = DatabaseManager.GetConnection())
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

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("StudentHomePage.aspx");
    }
}

