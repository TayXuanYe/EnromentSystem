using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HOPReviewPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["hid"] == null)
        {
            Response.Redirect("HopLoginPage.aspx");
        }

        string hid = Session["hid"].ToString();
        string fullname = GetFullname(hid);

        if (!string.IsNullOrEmpty(fullname))
        {
            welcome.Text = $"Welcome {fullname}";
        }
        else
        {
            welcome.Text = "Welcome Guest";
        }
    }

    private string GetFullname(string hid)
    {
        string fullname = string.Empty;
        List<string> selectColumns = new List<string> { "fullname" };
        string condition = $"WHERE hid = '{hid}'";

        DataSet result = DatabaseManager.GetRecord("hop", selectColumns, condition);

        if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
        {
            fullname = result.Tables[0].Rows[0]["fullname"].ToString();
        }

        return fullname;
    }
}