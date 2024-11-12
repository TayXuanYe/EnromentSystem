using System;
using System.Collections.Generic;
using System.Data;

public partial class AdminHomePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["aid"] != null)
        {
            DataSet dataSet = DatabaseManager.GetRecord(
                "admin",
                new List<string> { "name" },
                $@"WHERE aid = '{Session["aid"]}'"
                );
            if(dataSet != null)
            {
                foreach(DataRow row in dataSet.Tables[0].Rows)
                {
                    lblUserName.Text = row["name"].ToString();
                }
            }
        }
    }
}