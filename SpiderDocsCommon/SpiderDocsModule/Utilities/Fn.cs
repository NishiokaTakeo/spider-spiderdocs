using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spider.IO;
using NLog;
using System.IO;
using System.Text.RegularExpressions;

namespace SpiderDocsModule
{
    public class Fn
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        
        #region Path manipulation

        public static string MapPath(string afterRootPath)
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;

            if( afterRootPath.StartsWith("~/"))
            {
                return root+ "\\" + afterRootPath.Substring(2);
            }
            else
            {
                return ToFixPath(root+ "\\" + afterRootPath);
            }            
        }

        /// <summary>
        /// c:dev/aaa/bbb/ccc.jpg to ccc.jpg
        /// </summary>
        /// <param name="fullpath"></param>
        /// <returns>filename</returns>
        public static string Path2Filename(string fullpath)
        {            
            string filename = ToFixPath(fullpath).Split(new char[] { '/', '\\' }).LastOrDefault();

            return filename;
        }
        public static string Extension(string path)
        {

            string extention = string.Empty;

            try
            {
                extention = Path2Filename(path).Split('.').LastOrDefault();
            }
            catch(Exception ex)
            {
                logger.Warn(ex, "{0}", path);
                extention = Path2Filename(path);
            }

            return extention;
        }

        public static string ToFixPath(string fullpath)
        {
            logger.Debug("parameters: fullpath={0}", fullpath);
            
            Regex r = new Regex(@"(:\/|\:\\)");                        
            bool mached = r.IsMatch(fullpath);
            string prefix = string.Empty, separator = string.Empty ,dir = string.Empty, ans = string.Empty ;
            try
            {
                if (mached)
                {
                    separator = r.Match(fullpath).Groups[0].ToString();
                    string[]  protocolAndDir = r.Split(fullpath);
                
                    prefix = protocolAndDir.First();
                    dir = protocolAndDir.Last();
                }
                else
                {
                    dir = fullpath;
                }
                            
                Regex division = new Regex(@"(\/|\\)+");
            
                string fixedpath = division.Replace(dir, "/");


                if (mached)
                    ans = prefix + separator + fixedpath;
                else
                    ans = fixedpath;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }

            return ans;
        }

        #endregion
        public static string TypeByExtnt(string filename)
        {
            Dictionary<string, string> DB = new Dictionary<string, string>();

            DB.Add("doc", "application/msword");
            DB.Add("dot", "application/msword");
            DB.Add("docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            DB.Add("dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template");
            DB.Add("docm", "application/vnd.ms-word.document.macroEnabled.12");
            DB.Add("dotm", "application/vnd.ms-word.template.macroEnabled.12");
            DB.Add("xls", "application/vnd.ms-excel");
            DB.Add("xlt", "application/vnd.ms-excel");
            DB.Add("xla", "application/vnd.ms-excel");
            DB.Add("xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            DB.Add("xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template");
            DB.Add("xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12");
            DB.Add("xltm", "application/vnd.ms-excel.template.macroEnabled.12");
            DB.Add("xlam", "application/vnd.ms-excel.addin.macroEnabled.12");
            DB.Add("xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12");
            DB.Add("ppt", "application/vnd.ms-powerpoint");
            DB.Add("pot", "application/vnd.ms-powerpoint");
            DB.Add("pps", "application/vnd.ms-powerpoint");
            DB.Add("ppa", "application/vnd.ms-powerpoint");
            DB.Add("pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation");
            DB.Add("potx", "application/vnd.openxmlformats-officedocument.presentationml.template");
            DB.Add("ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow");
            DB.Add("ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12");
            DB.Add("pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12");
            DB.Add("potm", "application/vnd.ms-powerpoint.template.macroEnabled.12");
            DB.Add("ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12");
            DB.Add("mdb", "application/vnd.ms-access");
            DB.Add("au", "audio/basic");
            DB.Add("avi", "video/msvideo, video/avi, video/x-msvideo");
            DB.Add("bmp", "image/bmp");
            DB.Add("bz2", "application/x-bzip2");
            DB.Add("css", "text/css");
            DB.Add("dtd", "application/xml-dtd");
            DB.Add("es", "application/ecmascript");
            DB.Add("exe", "application/octet-stream");
            DB.Add("gif", "image/gif");
            DB.Add("gz", "application/x-gzip");
            DB.Add("hqx", "application/mac-binhex40");
            DB.Add("html", "text/html");
            DB.Add("jar", "application/java-archive");
            DB.Add("jpg", "image/jpeg");
            DB.Add("js", "application/x-javascript");
            DB.Add("midi", "audio/x-midi");
            DB.Add("mp3", "audio/mpeg");
            DB.Add("mpeg", "video/mpeg");
            DB.Add("ogg", "audio/vorbis, application/ogg");
            DB.Add("pdf", "application/pdf");
            DB.Add("pl", "application/x-perl");
            DB.Add("png", "image/png");
            DB.Add("ps", "application/postscript");
            DB.Add("qt", "video/quicktime");
            DB.Add("ra", "audio/x-pn-realaudio, audio/vnd.rn-realaudio");
            DB.Add("ram", "audio/x-pn-realaudio, audio/vnd.rn-realaudio");
            DB.Add("rdf", "application/rdf, application/rdf+xml");
            DB.Add("rtf", "application/rtf");
            DB.Add("sgml", "text/sgml");
            DB.Add("sit", "application/x-stuffit");
            DB.Add("sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide");
            DB.Add("svg", "image/svg+xml");
            DB.Add("swf", "application/x-shockwave-flash");
            DB.Add("tar.gz", "application/x-tar");
            DB.Add("tgz", "application/x-tar");
            DB.Add("tiff", "image/tiff");
            DB.Add("tsv", "text/tab-separated-values");
            DB.Add("txt", "text/plain");
            DB.Add("wav", "audio/wav, audio/x-wav");
            DB.Add("xml", "application/xml");
            DB.Add("zip", "application/zip, application/x-compressed-zip");
            DB.Add("msg", "application/vnd.ms-outlook");

            string extntn = Extension(filename);

            if (!DB.ContainsKey(extntn))
                logger.Error("No Extention Found. : {0}", extntn);

            return DB.ContainsKey(extntn) ? DB[extntn] : DB["txt"];
        }

        public static bool ForceDLExtension(string path)
        {
            string[] target = new string[] { ".msg", ".eml" };
            return target.Contains(System.IO.Path.GetExtension(path));
        }

        public static string Guid()
        {
            return System.Guid.NewGuid().ToString().Substring(0, 7);
        }

        public static string EmptyPDF()
        {
            string  tmpfolder = FileFolder.GetTempFolder() + "template" ,
                    tmpfile = tmpfolder + "\\empty.pdf";

            Directory.CreateDirectory(tmpfolder);

            iTextSharp.text.Document doc = null;
            iTextSharp.text.pdf.PdfWriter writer = null;
            try
            {
                if (File.Exists(tmpfile)) File.Delete(tmpfile);

                // Initialize the PDF document
                doc = new iTextSharp.text.Document();
                writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc,
                    new System.IO.FileStream(tmpfile,
                        System.IO.FileMode.Create));


                // Set margins and page size for the document
                doc.SetMargins(0, 0, 0, 0);

                doc.SetPageSize(iTextSharp.text.PageSize.A4);
                // Open the document for writing content
                doc.Open();
                //doc.NewPage();
                doc.Add(new iTextSharp.text.Paragraph(" "));
                

            }
            catch (iTextSharp.text.DocumentException dex)
            {
                logger.Error(dex);
            }
            finally
            {
                // Clean up
                doc.Close();
                writer.Close();

            }


            return tmpfile;

        }

        public static string Img2Pdf(byte[] byte_image, string output_pdf_path = null)
        {
            Directory.CreateDirectory(FileFolder.GetTempFolder() + "template");

            output_pdf_path = output_pdf_path ?? FileFolder.GetTempFolder() + "exported.pdf";

            int padding = 10;
            iTextSharp.text.Rectangle defaultPageSize = iTextSharp.text.PageSize.A4;

            string  empty_pdf_path = EmptyPDF();

                    output_pdf_path = FileFolder.GetAvailableFileName(output_pdf_path);
            
            using (Stream inputPdfStream = new FileStream(empty_pdf_path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream inputImageStream = new MemoryStream(byte_image, false))
            using (Stream outputPdfStream = new FileStream(output_pdf_path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var reader = new iTextSharp.text.pdf.PdfReader(inputPdfStream);
                var stamper = new iTextSharp.text.pdf.PdfStamper(reader, outputPdfStream);

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);

                double ratio = RatioBy(image, defaultPageSize);
                image.ScaleAbsolute((float)(image.Width * ratio) - (padding * 2), (float)(image.Height * ratio) - (padding * 2));
                // Put image at center
                image.SetAbsolutePosition((iTextSharp.text.PageSize.A4.Width - image.ScaledWidth) / 2, (iTextSharp.text.PageSize.A4.Height - image.ScaledHeight) / 2);

                // Output
                stamper.GetOverContent(1).AddImage(image);
                stamper.Close();
            }

            return output_pdf_path;

        }

        public static string Txt2Pdf(string text, string output_pdf_path = null)
        {
            output_pdf_path = output_pdf_path ?? FileFolder.GetTempFolder() + "exported.pdf";

            string

                empty_pdf_path = EmptyPDF();

            output_pdf_path = FileFolder.GetAvailableFileName(output_pdf_path);

            //Create a New instance on Document Class
            var doc = new iTextSharp.text.Document();

            //Create a New instance of PDFWriter Class for Output File
            iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(output_pdf_path, FileMode.Create));

            //Open the Document
            doc.Open();

            //Add the content of Text File to PDF File
            doc.Add(new iTextSharp.text.Paragraph(text));

            //Close the Document
            doc.Close();

            return output_pdf_path;
        }

        public static iTextSharp.text.pdf.PdfCopy AppendPDF(iTextSharp.text.pdf.PdfCopy copyman ,string from)
        {

            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(System.IO.File.ReadAllBytes(from));

            for (int i = 0; i < reader.NumberOfPages; i++)
            {
                iTextSharp.text.pdf.PdfImportedPage page = copyman.GetImportedPage(reader, i + 1);

                copyman.AddPage(page);
            }

            return copyman;
        }



        public static double RatioBy(iTextSharp.text.Image image, iTextSharp.text.Rectangle page)//(float width,float size)
        {
            double adjustment = 0.3;
            double higher;// = image.Width > image.Height ? image.Width : image.Height;// width;
            float size;//= image.Width > image.Height ?

            if (page.Width < image.Width && page.Height < image.Height)
            {
                if ((image.Width - page.Width) > (image.Height - page.Height))
                {
                    higher = image.Width;
                    size = page.Width;
                }
                else
                {
                    higher = image.Height;
                    size = page.Height;
                }
            }
            else if (page.Width < image.Width)
            {
                higher = image.Width;
                size = page.Width;
            }
            else if (page.Height < image.Height)
            {
                higher = image.Height;
                size = page.Height;
            }
            else
            {
                return 1;
            }

            for (double ratio = 1000000; ratio > 0; ratio--)   // accurate is 0.0001
            {
                double trial = ratio / 1000000;

                if ((higher * trial) <= size)
                {
                    adjustment = trial;
                    break;
                }
            }

            return adjustment;
        }

        public static string ConvertToLetter(int version)
        {

            int alpha, remainder;
            string str = "";

            if (version > 0)
            {
                alpha = (int)(version / 27);

                remainder = version - (alpha * 26);

                if (alpha > 0)
                {
                    str = ((char)(alpha + 64)).ToString();
                }

                if (remainder > 0)
                {
                    str = (str + (char)(remainder + 64));
                }
            }

            return str;

        }

    }
}