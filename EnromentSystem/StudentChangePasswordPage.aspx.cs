using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentChangePasswordPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Ensure the student is logged in
        if (Session["sid"] == null)
        {
            Response.Redirect("StudentLoginPage.aspx");
            return;
        }
    }



    protected void PasswordFormat_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string password = args.Value;
        bool isValid = true;

        // Check if password is between 8 to 20 characters, alphanumeric, and no symbols
        if (password.Length < 8 || password.Length > 20)
        {
            isValid = false;
        }
        else if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"^[a-zA-Z0-9]*$"))
        {
            isValid = false;
        }

        args.IsValid = isValid;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string studentId = Session["sid"].ToString();

        // Get the current password from the user
        string currentPassword = txtExistingPassword.Text.Trim();
        string newPassword = txtNewPassword.Text.Trim();
        string confirmNewPassword = txtConfirmNewPassword.Text.Trim();

        // Check if the new password and confirmation match
        if (newPassword != confirmNewPassword)
        {
            lblMessage.Text = "New password and confirmation do not match.";
            return;
        }

        // Validate the current password and new password formats (using validators already defined)
        if (Page.IsValid)
        {
            if (ValidateCurrentPassword(studentId, currentPassword))
            {
                // Update the password in the database
                UpdatePassword(studentId, newPassword);
            }
            else
            {
                lblMessage.Text = "Current password is incorrect.";
            }
        }
    }

    // Function to validate the current password in the database
    private bool ValidateCurrentPassword(string studentId, string currentPassword)
    {
        bool isValid = false;
        string query = "SELECT password FROM student WHERE sid = @StudentID";

        using (SqlConnection conn = DatabaseManager.GetConnection())
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StudentID", studentId);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string storedPassword = reader["password"].ToString();
                if (storedPassword == currentPassword) // Compare with the stored password
                {
                    isValid = true;
                }
            }
            conn.Close();
        }

        return isValid;
    }

    // Function to update the password in the database
    private void UpdatePassword(string studentId, string newPassword)
    {
        string query = "UPDATE student SET password = @NewPassword WHERE sid = @StudentID";

        using (SqlConnection conn = DatabaseManager.GetConnection())
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@NewPassword", newPassword);
            cmd.Parameters.AddWithValue("@StudentID", studentId);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();

            if (rowsAffected > 0)
            {
                string script = "alert('Password updated successfully!');";
                ClientScript.RegisterStartupScript(this.GetType(), "Success", script, true);
                txtExistingPassword.Text = "";
                txtNewPassword.Text = "";
                txtConfirmNewPassword.Text = "";

            }
            else
            {
                string script = "alert('Error updating password. Please try again.');";
                ClientScript.RegisterStartupScript(this.GetType(), "Error", script, true);
                
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("StudentHomePage.aspx");
    }
}
