using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentResetPasswordPage : System.Web.UI.Page
{
    private int verificationCode;
    protected void cvdVerificationCodeMatch_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }

    protected void btnSendVerificationCode_Click(object sender, EventArgs e)
    {
        if (rfvUserId.IsValid == true)
        {
            Random random = new Random();
            verificationCode = random.Next(100000, 999999);

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
                emailManager.SetEmailBody("Dear " + name + ":");
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
            successfulPopUpWindows.Style["display"] = "flex";
        }
    }
}