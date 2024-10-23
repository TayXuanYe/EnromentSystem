using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

public class PdfTableGenerator
{
    public PdfTableGenerator() { }

    // Generate PDF method that accepts table data and output path
    public void generatePDF(string[][] tableData, string outputPath)
    {
        // Create PDF writer and document
        using (PdfWriter writer = new PdfWriter(outputPath))
        {
            using (PdfDocument pdf = new PdfDocument(writer))
            {
                Document document = new Document(pdf);

                // Create a table with the number of columns based on the input data
                Table table = new Table(tableData[0].Length);

                // Fill the table with data
                foreach (var row in tableData)
                {
                    foreach (var cellData in row)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(cellData)));
                    }
                }

                // Add the table to the document
                document.Add(table);
                document.Close();
            }
        }
    }
}

