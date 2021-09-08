using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using ClientSpiderDocsWebService.EXIF;
using System.Drawing.Imaging;
using iTextSharp.text.pdf;
using System.Collections;

namespace SpiderDocs
{
	//public class Utilities : UtilitiesBase
	//{
	//}
}

namespace ClientSpiderDocsWebService
{
    public class Utilities
    {

        static string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["spiderDocs"].ConnectionString;
       


        public static SqlConnection getSqlConnBeginTran()
        {

            SqlConnection sqlConn = new SqlConnection(strConn);
            sqlConn.Open();

            SqlCommand cmd = new SqlCommand(" begin tran", sqlConn);
            cmd.ExecuteNonQuery();

            return sqlConn;
        }

        public static SqlConnection sqlConnCommit(SqlConnection sqlConn)
        {

            SqlCommand cmd = new SqlCommand(" commit tran", sqlConn);
            cmd.ExecuteNonQuery();

            return sqlConn;
        }

        public static SqlConnection sqlConnRollBack(SqlConnection sqlConn)
        {
            SqlCommand cmd = new SqlCommand(" rollback tran", sqlConn);
            cmd.ExecuteNonQuery();

            return sqlConn;
        }

        public static bool saveHistoric(SqlConnection sqlConn,int id_version, int id_event, int id_user)
        {
            Document objDoc = new Document();
            objDoc.id_version = id_version;
            objDoc.id_event = id_event;
            objDoc.id_user = id_user;
            objDoc.date = DateTime.Now;
            SpiderDocsModule.HistoryController.SaveDocumentHistoric(null,objDoc);

            return true;
        }

        public static Image fixImageOrientation(Image imageOriginal){


            // Rotate the image according to EXIF data
            var img = imageOriginal;
            var exif = new EXIFextractor(ref img, "\n"); // get source from http://www.codeproject.com/KB/graphics/exifextractor.aspx?fid=207371

            if(exif["Orientation"] != null)
            {
                RotateFlipType flip = OrientationToFlipType(exif["Orientation"].ToString());

                if(flip != RotateFlipType.RotateNoneFlipNone) // don't flip of orientation is correct
                {
                    img.RotateFlip(flip);
                   // exif.setTag(0x112, "1"); // Optional: reset orientation tag
                }

            }

            return imageOriginal;
        }
 
        private static RotateFlipType OrientationToFlipType(string orientation)
            {
            switch(int.Parse(orientation))
            {
            case 1:
            return RotateFlipType.RotateNoneFlipNone;
            case 2:
            return RotateFlipType.RotateNoneFlipX;
            case 3:
            return RotateFlipType.Rotate180FlipNone;
            case 4:
            return RotateFlipType.Rotate180FlipX;
            case 5:
            return RotateFlipType.Rotate90FlipX;
            case 6:
            return RotateFlipType.Rotate90FlipNone;
            case 7:
            return RotateFlipType.Rotate270FlipX;
            case 8:
            return RotateFlipType.Rotate270FlipNone;
            default:
            return RotateFlipType.RotateNoneFlipNone;
            }
            }

        public static string OCR(System.Drawing.Image img)
        {


            Bitmap bitmap = new Bitmap(img);
            string text = " ";
            string TessractData = AppDomain.CurrentDomain.BaseDirectory + "ocr\\";

            //TesseractProcessor processor = new TesseractProcessor();
            //var success = processor.Init(TessractData, "eng", (int)eOcrEngineMode.OEM_TESSERACT_ONLY);
            //if(!success)
            //{

            //}
            //else
            //{
            //    text += processor.Recognize(bitmap).Replace("\n", " \n ");

            //}

            return text;


        }

        public static byte[] makeSearcheablePdf(PdfSharp.Pdf.PdfDocument doc, ArrayList OcrText)
        {


            //memory stream
            MemoryStream stream = new MemoryStream();


            String imagePdfPath = AppDomain.CurrentDomain.BaseDirectory +"imagePdf" + DateTime.Now.Ticks + ".pdf";
            String imagePdfPath2 = AppDomain.CurrentDomain.BaseDirectory +"imagePdf2" + DateTime.Now.Ticks + ".pdf";
            doc.Save(imagePdfPath);

            doc.Save(stream, false);
            byte[] bytes = stream.ToArray();
            doc.Close();



            string oldFile = imagePdfPath;
            string newFile = imagePdfPath2;

            // open the reader
            PdfReader reader = new PdfReader(oldFile);
            iTextSharp.text.Rectangle size = reader.GetPageSizeWithRotation(1);
            iTextSharp.text.Document document = new iTextSharp.text.Document(size);



            FileStream fs = new FileStream(newFile, FileMode.Create, FileAccess.Write);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();



            // the pdf content
            PdfContentByte cb = writer.DirectContent;

            for(int i = 1; i <= OcrText.Count; i++)
            {
                document.NewPage();
                document.SetPageSize(reader.GetPageSizeWithRotation(i));
                String text = (String)OcrText[i - 1];

                ////// select the font properties
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                // put the alignment and coordinates here
                cb.BeginText();
                cb.SetFontAndSize(bf, 1);
                cb.ShowText(text);
                cb.EndText();


                // create the new page and add it to the pdf
                PdfImportedPage page = writer.GetImportedPage(reader, i);
                cb.AddTemplate(page, 0, 0);

            }

            // close the streams and voilá the file should be changed :)
            document.Close();
            fs.Close();
            writer.Close();
            reader.Close();


            FileStream fileStream = new FileStream(newFile, FileMode.Open, FileAccess.Read);

            byte[] docBytes = new byte[fileStream.Length];
            fileStream.Read(docBytes, 0, docBytes.Length);


            fileStream.Close();



            File.Delete(imagePdfPath);
            File.Delete(imagePdfPath2);

            return docBytes;
        }
        
    }
}