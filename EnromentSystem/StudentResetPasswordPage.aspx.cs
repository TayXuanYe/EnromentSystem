using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentResetPasswordPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void cvdVerificationCodeMatch_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = false;
        if (Session["verification"] != null)
        {
            if (txtVerifcationCode.Text == Session["verification"].ToString())
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }

    protected void btnSendVerificationCode_Click(object sender, EventArgs e)
    {
        if (rfvUserId.IsValid == true && !(String.IsNullOrEmpty(txtUserId.Text)))
        {
            Random random = new Random();
            Session["verification"] = random.Next(100000, 999999);

            DataSet dataSet = DatabaseManager.getRecord(
            "student",
            new List<string> { "student_email", "name" },
            "WHERE sid = \'" + txtUserId.Text+ "\'"
            );

            DataTable dt = dataSet.Tables[0];
            string name = null;
            string student_email = null;
            foreach (DataRow row in dt.Rows)
            {
                name = row["name"].ToString();
                student_email = row["student_email"].ToString();
            }

            if (name != null)
            {
                EmailManager emailManager = new EmailManager();
                emailManager.SetEmailReceiver(name, student_email);
                emailManager.SetEmailSubject("Verification Code");
                //send verification
                string verificationCode = Session["verification"].ToString();
                string body = $@" 
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Verification Code</title>
                        <style>
                            body {{font - family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }}
                            .container {{max - width: 600px; margin: 0 auto; background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }}
                            h2 {{color: #333333; }}
                            p {{color: #555555; line-height: 1.6; }}
                            .code {{font - size: 24px; font-weight: bold; color: #4CAF50; padding: 10px 20px; background-color: #f4f4f4; border: 1px dashed #4CAF50; display: inline-block; margin: 10px 0; }}
                            .footer {{font - size: 12px; color: #999999; text-align: center; margin-top: 20px; }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h2>Verification Code</h2>
                            <p>Dear {name},</p>
                            <p>Forget your password? <br> The following is your verification code. Please use this verification code to complete the verification to reset password.</p>
                            <div class='code'>{verificationCode}</div>
                            <p>If you did not request this verification code, please ignore this email.</p>
                            <div class='footer'>
                                <p>This is a computer generated email. Please do not respond to this email.</p>
                            </div>
                        </div>
                    </body>
                    </html>";
                emailManager.SetEmailBody(body);
                emailManager.SendEmail();
            }
            else
            {
                failPopUpWindows.Style["display"] = "flex"; ;
            }

        }
    }

    protected void ExitPage(object sender, EventArgs e)
    {
        Response.Redirect("StudentLoginPage.aspx");
    }


    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        if(Page.IsValid == true)
        {
            DatabaseManager.UpdateData(
                "student",
                new List<string> { "password" },
                new List<object> { txtNewPassword.Text },
                "WHERE sid = \'" + txtUserId.Text + "\'"
            );
            successfulPopUpWindows.Style["display"] = "flex";
        }
    }
}