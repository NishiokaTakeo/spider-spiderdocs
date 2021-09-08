using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace SpiderDocsModule
{
    public class PDFWriter: System.IDisposable
    {
        iTextSharp.text.Document doc;
        iTextSharp.text.pdf.PdfWriter writer;
        MemoryStream stream;
        
        const string FONT = "Arial";
        static iTextSharp.text.Font FONT_TITLE = iTextSharp.text.FontFactory.GetFont(FONT, 24);
        static iTextSharp.text.Font FONT_SECTION_TITLE = iTextSharp.text.FontFactory.GetFont(FONT, 16);
        static iTextSharp.text.Font FONT_NORMAL = iTextSharp.text.FontFactory.GetFont(FONT, 12);
        string path = ""; 
        
        public enum en_BLOCK
        {
            Title = 1,            
            SectionTitle = 2,
            Normal = 3
        }

        public PDFWriter()
        {
            doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 25f, 20f, 20f, 25f);
            path = FileFolder.GetAvailableFileName(FileFolder.GetTempFolder() + "tmp.pdf");
        }

        public PDFWriter Open()
        {
            writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, stream = new MemoryStream());
            
            doc.Open();

            return this;
        }
        public PDFWriter MakeNextPage()
        {
            doc.NewPage();

            return this;
        }
        public PDFWriter Title(string title)
        {
            var para = new Paragraph(title, FONT_TITLE);
            para.SpacingAfter = 10f;
            doc.Add(para);
            

            return this;
        }

        public PDFWriter SectionTitle(string paragraph)
        {
            var para = new Paragraph(paragraph, FONT_SECTION_TITLE);
            para.SpacingAfter = 5f;
            doc.Add(para);
            
            return this;
        }

        public PDFWriter Paragraph(string paragraph)
        {
            
            doc.Add(new Paragraph(paragraph, FONT_NORMAL));

            return this;
        }

        public PDFWriter Table(System.Data.DataTable dt)
        {            
            PdfPTable table = new PdfPTable(dt.Columns.Count);
            
            foreach(var cl in dt.Columns)
            {                
                PdfPCell cell = new PdfPCell(new Phrase(cl.ToString()));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = new BaseColor(255, 102, 0);
                table.AddCell(cell);
            }

            foreach (System.Data.DataRow row in dt.Rows)
            {
                foreach (var cell in row.ItemArray)
                    table.AddCell(cell.ToString());
            }

            doc.Add(table);

            return this;
        }

        public PDFWriter Save(string path2)
        {
            doc.Close();
            writer.Close();
            stream.Close();

            byte[] bytes = stream.ToArray();

            using (var fs = new FileStream(path2, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }

            
            return this;

        }
        
        public void Dispose()
        {
            //writer.Close();
            doc.Close();
            //stream.Close();
        }
    }
}
