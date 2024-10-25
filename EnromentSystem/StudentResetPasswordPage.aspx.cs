using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentResetPasswordPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void cvdVerificationCodeMatch_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }

    protected void btnSendVerificationCode_Click(object sender, EventArgs e)
    {

    }

    protected void ExitPage(object sender, EventArgs e)
    {
        Response.Redirect("StudentLoginPage.aspx");
    }

}