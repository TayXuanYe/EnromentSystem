using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentTakeAttendentPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sid"] != null)
        {
            string studentId = Session["sid"].ToString();
            if (Request.QueryString["qrData"] != null)
            {
                string qrData = Request.QueryString["qrData"];

                // 在后端显示二维码内容
                Debug.WriteLine("扫描到的内容: " + qrData);
            }
        }
        else
        {
            Response.Redirect("StudentLoginPage.aspx");
        }
    }


    private bool CheckQrNotExpired()
    {
        return true;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
}