using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentChangePasswordPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sid"] == null)
        {
            Response.Redirect("StudentLoginPage.aspx");
        }

        // Check if the verification pop-up should be displayed
        if (Request.QueryString["showVerification"] == "true")
        {
            verificationPopUp.Style["display"] = "block";
        }
    }

    protected void PasswordFormat_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string password = args.Value;
        args.IsValid = password.Length >= 8 &&
                       password.Length <= 20 &&
                       System.Text.RegularExpressions.Regex.IsMatch(password, @"[a-zA-Z]") &&
                       System.Text.RegularExpressions.Regex.IsMatch(password, @"\d") &&
                       !System.Text.RegularExpressions.Regex.IsMatch(password, @"[^a-zA-Z0-9]");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string studentId = Session["sid"].ToString();
        string existingPassword = txtExistingPassword.Text.Trim();
        string newPassword = txtNewPassword.Text.Trim();

        if (!IsExistingPasswordCorrect(studentId, existingPassword))
        {
            ShowMessage("The existing password is incorrect.");
            return;
        }

        if (SendVerificationCode(studentId))
        {
            verificationPopUp.Style["display"] = "block"; 
        }
    }

    protected void btnVerifyCode_Click(object sender, EventArgs e)
    {
        string enteredVerificationCode = txtVerificationCode.Text.Trim();

        if (IsVerificationCodeValid(enteredVerificationCode))
        {
            string studentId = Session["sid"].ToString();
            string newPassword = txtNewPassword.Text.Trim();

          
            if (UpdatePassword(studentId, newPassword))
            {
                ShowMessage("Password updated successfully!");
            }
            else
            {
                ShowMessage("Failed to update the password. Please try again.");
            }
        }
        else
        {
            lblVerificationMessage.Text = "Invalid verification code.";
            lblVerificationMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("StudentHomePage.aspx");
    }

    private bool IsExistingPasswordCorrect(string studentId, string existingPassword)
    {
        string query = "SELECT COUNT(*) FROM student WHERE sid = @StudentID AND password = @Password";

        using (SqlConnection conn = DatabaseManager.GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                cmd.Parameters.AddWithValue("@Password", existingPassword);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }

    private bool UpdatePassword(string studentId, string newPassword)
    {
        string query = "UPDATE student SET password = @NewPassword WHERE sid = @StudentID";

        using (SqlConnection conn = DatabaseManager.GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                cmd.Parameters.AddWithValue("@StudentID", studentId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }

    private bool SendVerificationCode(string studentId)
    {
        Random random = new Random();
        Session["verification"] = random.Next(100000, 999999); // Generate a random verification code

        string query = "SELECT student_email, name FROM student WHERE sid = @StudentID";
        DataTable dt = GetStudentDetails(studentId, query);

        if (dt.Rows.Count > 0)
        {
            string name = dt.Rows[0]["name"].ToString();
            string studentEmail = dt.Rows[0]["student_email"].ToString();

            EmailManager emailManager = new EmailManager();
            emailManager.SetEmailReceiver(name, studentEmail);
            emailManager.SetEmailSubject("Verification Code");

            string verificationCode = Session["verification"].ToString();
            string body = $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Verification Code</title>
                    <style>
                        body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }}
                        .container {{ max-width: 600px; margin: 0 auto; background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }}
                        h2 {{ color: #333333; }}
                        .code {{ font-size: 24px; font-weight: bold; color: #4CAF50; padding: 10px 20px; background-color: #f4f4f4; border: 1px dashed #4CAF50; display: inline-block; margin: 10px 0; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h2>Verification Code</h2>
                        <p>Dear {name},</p>
                        <p>The following is your verification code. Please use this to complete the password reset process.</p>
                        <div class='code'>{verificationCode}</div>
                    </div>
                </body>
                </html>";

            emailManager.SetEmailBody(body);
            emailManager.SendEmail();
            return true;
        }
        else
        {
            ShowMessage("Failed to retrieve student details.");
            return false;
        }
    }

    private DataTable GetStudentDetails(string studentId, string query)
    {
        DataTable dt = new DataTable();
        using (SqlConnection conn = DatabaseManager.GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
        }
        return dt;
    }

    private bool IsVerificationCodeValid(string enteredCode)
    {
        return enteredCode == Session["verification"].ToString();
    }

    private void ShowMessage(string message)
    {
        lblMessage.Text = message;
        lblMessage.ForeColor = System.Drawing.Color.Red;
    }
}