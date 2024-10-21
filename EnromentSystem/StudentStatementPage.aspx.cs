using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

public partial class StudentStatementPage : System.Web.UI.Page
{
    string connectionString = "Data Source=云烟\\SQLEXPRESS;Initial Catalog=StudentEnrollmentSystem;Integrated Security=True";


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindData();
        }
    }

    private DataTable GetStatementRecords()
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM statement_record";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }
        return dt;
    }

    private void BindData()
    {
        GridView1.DataSource = GetStatementRecords();
        GridView1.DataBind();
    }

    public void GeneratePdfFromDatabase()
    {
        DataTable dt = GetStatementRecords();
        string pdfPath = Server.MapPath("~/statement_record.pdf");

        using (PdfWriter writer = new PdfWriter(pdfPath))
        {
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            int columnCount = dt.Columns.Count;

            iText.Layout.Element.Table table = new iText.Layout.Element.Table(columnCount);

            foreach (DataColumn column in dt.Columns)
            {
                table.AddHeaderCell(new Cell().Add(new Paragraph(column.ColumnName)));
            }

            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    table.AddCell(new Cell().Add(new Paragraph(item.ToString())));
                }
            }

            document.Add(table);

            document.Close();
        }
    }

    protected void btnGeneratePdf_Click(object sender, EventArgs e)
    {
        GeneratePdfFromDatabase();

        Response.Redirect("~/statement_record.pdf");
    }
}




