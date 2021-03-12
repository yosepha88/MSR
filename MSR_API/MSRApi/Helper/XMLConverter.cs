using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MSRApi.Helper
{
    public class XMLConverter
    {
        public static void ConvertWordtoXML()
        {
            //using (WordprocessingDocument doc = WordprocessingDocument.Create("E:\\test111.docx", DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            //{
            //    // Add a main document part.
            //    MainDocumentPart mainPart = doc.AddMainDocumentPart();

            //    // Create the document structure and add some text.
            //    mainPart.Document = new Document();
            //    Body body = mainPart.Document.AppendChild(new Body());
            //    Paragraph para = body.AppendChild(new Paragraph());
            //    Run run = para.AppendChild(new Run());

            //    // String msg contains the text, "Hello, Word!"
            //    run.AppendChild(new Text("New text in document"));
            //}



            // Open word document for read  
            using (var doc = WordprocessingDocument.Open("E:\\test111NEW.docx", true))
            {
                // To create a temporary table   
                DataTable dt = new DataTable();
                int rowCount = 0;

                //var docbody = doc.MainDocumentPart.Document.Body;

                foreach (var tableItem in doc.MainDocumentPart.Document.Body.Elements<Table>().ToList())
                {

                    // Find the first table in the document.   
                    Table table = tableItem;
                    if (table != null)
                    {
                        // To get all rows from table  
                        IEnumerable<TableRow> rows = table.Elements<TableRow>();

                        // To read data from rows and to add records to the temporary table  
                        foreach (TableRow row in rows)
                        {
                            if (rowCount == 0)
                            {
                                foreach (TableCell cell in row.Descendants<TableCell>())
                                {
                                    dt.Columns.Add(cell.InnerText);
                                }
                                rowCount += 1;
                            }
                            else
                            {
                                dt.Rows.Add();
                                int i = 0;
                                foreach (TableCell cell in row.Descendants<TableCell>())
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = cell.InnerText;
                                    i++;
                                }
                            }
                        }
                    }
                }

            }
        }


    }
}
