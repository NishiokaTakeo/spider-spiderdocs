using System;
using System.IO;
using System.Collections.Generic;
using SpiderDocsOCR.Pdf;
using SpiderDocsOCR.Enums;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
    public class OCRManager
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        public bool CancelReq = false;

        public delegate void EventFunc(object arg);

        //public event EventFunc OnStartExtractImage;
        //public event EventFunc OnUpdatedExtractImage;

        //public event EventFunc OnUpdatedCreatePDF;

        public event EventFunc OnCancelled;

        string _path = string.Empty;
        bool _searchable = true;
        public string DestinationPath { get; set; }

        List<string> distillerOptions = new List<string>
            {
                "-dSubsetFonts=true",
                "-dCompressFonts=true",
                "-sProcessColorModel=DeviceRGB",
                "-sColorConversionStrategy=sRGB",
                "-sColorConversionStrategyForImages=sRGB",
                "-dConvertCMYKImagesToRGB=true",
                "-dDetectDuplicateImages=true",
                "-dDownsampleColorImages=false",
                "-dDownsampleGrayImages=false",
                "-dDownsampleMonoImages=false",
                "-dColorImageResolution=265",
                "-dGrayImageResolution=265",
                "-dMonoImageResolution=265",
                "-dDoThumbnails=false",
                "-dCreateJobTicket=false",
                "-dPreserveEPSInfo=false",
                "-dPreserveOPIComments=false",
                "-dPreserveOverprintSettings=false",
                "-dUCRandBGInfo=/Remove"
            };


        public OCRManager(string path/*, string dst,string title*/)
        {
            _path = path;
        }

        public OCRManager(string path, bool searchable = true)
        {
            _path = path;
            _searchable = searchable;
        }

        //MemoryStream StremFrom()
        //{
        //    Syncfusion.Pdf.PdfDocument document = new Syncfusion.Pdf.PdfDocument();
        //    MemoryStream stream = new MemoryStream();

        //    if (FileFolder.extensionsForImage.Exists(f => _path.Contains(f)))
        //    {
        //        ////Create a new PDF document
        //        //PdfDocument document = new PdfDocument();

        //        //Add a page to the document
        //        Syncfusion.Pdf.PdfPage page = document.Pages.Add();

        //        //Create PDF graphics for the page
        //        Syncfusion.Pdf.Graphics.PdfGraphics graphics = page.Graphics;

        //        //Load the image from the disk
        //        Syncfusion.Pdf.Graphics.PdfBitmap image = new Syncfusion.Pdf.Graphics.PdfBitmap(_path);

        //        //Draw the image
        //        graphics.DrawImage(image, 0, 0, page.GetClientSize().Width, page.GetClientSize().Height);

        //        //Save the document into stream
        //        document.Save(stream);

        //    }
        //    else if(FileFolder.extensionsForPDF.Exists( f => _path.Contains(f)))
        //    {
        //        Syncfusion.Pdf.Parsing.PdfLoadedDocument loadDoc = new Syncfusion.Pdf.Parsing.PdfLoadedDocument(_path);
        //        loadDoc.Save(stream);
        //    }
        //    else
        //    {
        //        throw new FileLoadException("File is not OCRable");
        //    }

        //    return stream;
        //}
        MemoryStream StremFrom(string path)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document();

            MemoryStream stream = new MemoryStream();

            if (FileFolder.Has4Image(path))
            {
                string cnved = Fn.Img2Pdf(File.ReadAllBytes(path));

                using (FileStream fileStream = File.OpenRead(cnved))
                {
                    //create new MemoryStream object

                    stream.SetLength(fileStream.Length);
                    //read file to MemoryStream
                    fileStream.Read(stream.GetBuffer(), 0, (int)fileStream.Length);
                }

                //MemoryStream StremFrom()
                //{
                //    iTextSharp.text.Document document = new iTextSharp.text.Document();

                //    MemoryStream stream = new MemoryStream();

                //    if (FileFolder.extensionsForImage.Exists(f => _path.Contains(f)))
                //    {
                //        PdfWriter.GetInstance(document, stream);
                //        document.Open();
                //        using (var imageStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                //        {
                //            var image = iTextSharp.text.Image.GetInstance(imageStream);
                //            document.Add(image);
                //        }
                //        document.Close();
                //    }
                //}

            }
            else if (FileFolder.Has4PDF(path))
            {
                stream = new MemoryStream(System.IO.File.ReadAllBytes(path));

                //Syncfusion.Pdf.Parsing.PdfLoadedDocument loadDoc = new Syncfusion.Pdf.Parsing.PdfLoadedDocument(path);
                //loadDoc.Save(stream);
            }
            else
            {
                logger.Debug("path is {0}",path);
                throw new FileLoadException("File is not OCRable");
            }

            return stream;
        }

        //---------------------------------------------------------------------------------
        /// <summary>
        /// Convert PDF
        /// </summary>
        /// <param name="dst">Path where PDF is output. Will be environmental temp folder if no specified.</param>
        //public string GetPDF(string dst = "")
        //{
        //    //Available output PDF path
        //    DestinationPath = OutputFileNameBy(dst ?? _path);

        //    MemoryStream stream = StremFrom();

        //    //Initialize the OCR processor by providing the path of tesseract binaries(SyncfusionTesseract.dll and liblept168.dll)
        //    using (Syncfusion.OCRProcessor.OCRProcessor processor = new Syncfusion.OCRProcessor.OCRProcessor(FileFolder.OCR))
        //    {
        //        //Load a PDF document
        //        Syncfusion.Pdf.Parsing.PdfLoadedDocument lDoc = new Syncfusion.Pdf.Parsing.PdfLoadedDocument(stream);

        //        //Set OCR language to process
        //        processor.Settings.Language = Syncfusion.OCRProcessor.Languages.English;

        //        //Process OCR by providing the PDF document and Tesseract data
        //        if(_searchable) processor.PerformOCR(lDoc, FileFolder.OCR_DATA);

        //        //Save the OCR processed PDF document in the disk
        //        lDoc.Save(DestinationPath);

        //        //Close the document
        //        lDoc.Close(true);
        //    }

        //    return DestinationPath;
        //}

        public string GetPDF(string dst = "")
        {
            string intermediatepath = string.Empty;

            DestinationPath = intermediatepath = OutputFileNameBy(dst ?? _path);

            PdfCompressorSettings pdfSettings = new PdfCompressorSettings
            {
                PdfCompatibilityLevel = PdfCompatibilityLevel.Acrobat_5_1_4,
                WriteTextMode = WriteTextMode.Word,
                Dpi = 300,
                ImageType = PdfImageType.Tif,
                ImageQuality = 100,
                CompressFinalPdf = false,//CompressFinalPdf = true,
                DistillerMode = dPdfSettings.prepress,
                DistillerOptions = string.Join(" ", distillerOptions.ToArray())
            };

            var _comp = new PdfCompressor(@"C:\gs\bin\gswin64c.exe", pdfSettings);
            _comp.OnExceptionOccurred += Compressor_OnExceptionOccurred;
            _comp.OnCompressorEvent += _comp_OnCompressorEvent;



            //byte[] data = File.ReadAllBytes(_path);
            byte[] data = StremFrom(_path).ToArray();

            if (_searchable)
            {

                data = _comp.CreateSearchablePdf(data, new PdfMeta
                {
                    Author = "",
                    KeyWords = string.Empty,
                    Subject = string.Empty,
                    Title = string.Empty
                }).Item1;

            }

            File.WriteAllBytes(DestinationPath, data);

            return DestinationPath;
        }

        private static void _comp_OnCompressorEvent(string msg) { logger.Debug(msg); }

        private static void Compressor_OnExceptionOccurred(PdfCompressor c, Exception x)
        {
            logger.Error(x);
            //Console.WriteLine("Exception Occured! ");
            //Console.WriteLine(x.Message);
            //Console.WriteLine(x.StackTrace);
        }

        static public string GetPDFWithMerge(string[] paths,bool searchable = true)
        {
            List<string> pdfs = new List<string>();
            foreach (string tmp in paths)
                pdfs.Add(new OCRManager(tmp, searchable).GetPDF());

            string path = FileFolder.GetTempFolder() + "pdfTemp.pdf";
            path = FileFolder.YeildNewFileName(path);

            PdfMerger.MergeFiles(pdfs, path);
            FileFolder.DeleteFiles(pdfs);

            return path;
        }

        /// <summary>
        /// Change file extension to PDF
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string ConvPDFName(string path)
        {
            // change extension to .pdf
            if (!string.IsNullOrWhiteSpace(path) && Path.GetExtension(path) != ".pdf")
                path = System.Text.RegularExpressions.Regex.Replace(path, Path.GetExtension(path) + "$", ".pdf");

            return path;

        }
        /// <summary>
        /// Rerutns available output PDF path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string OutputFileNameBy(string path)
        {
            path = ConvPDFName(path);

            // place a file in temporary folder if the path wasn't specified.
            if (string.IsNullOrWhiteSpace(path))
            {
                path = Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetFileName(ConvPDFName(_path));
            }

            // Don't override existing file
            if (File.Exists(path))
            {
                //Don't override if a file with same name is there.
                path = FileFolder.YeildNewFileName(path);
            }

            return path;
        }

        public void Cancel()
        {

        }

    }
}


//using System;
//using System.IO;
//using System.Collections.Generic;

////---------------------------------------------------------------------------------
//namespace SpiderDocsModule
//{
//	public class OCRManager
//	{
////---------------------------------------------------------------------------------
//		static int CfgObj = 0;
//		static int OcrObj = 0;
//		static int ScanObj = 0;
//		static int SvrObj = 0;
//		static int ImgObj = 0;

//		public static bool CancelReq = false;

//		public delegate void EventFunc(object arg);

//		public static event EventFunc OnStartExtractImage;
//		public static event EventFunc OnUpdatedExtractImage;

//		public static event EventFunc OnUpdatedCreatePDF;

//		public static event EventFunc OnCancelled;

////---------------------------------------------------------------------------------
//		static void Init()
//		{
//			int res;
//			int format = TNSOCR32.SVR_FORMAT_PDF;

//			TNSOCR.Engine_Initialize();
//			TNSOCR.Cfg_Create(out CfgObj);

//			//load options, if path not specified, current folder and folder with NSOCR.dll will be checked
//			TNSOCR.Cfg_LoadOptions(CfgObj);
//			TNSOCR.SetGsPath(CfgObj);

//			// tuning up options
//			//TNSOCR.Cfg_SetOption(CfgObj, TNSOCR32.BT_DEFAULT, "ImgAlizer/Inversion", "0");
//			TNSOCR.Cfg_SetOption(CfgObj, TNSOCR32.BT_DEFAULT, "Zoning/FindBarcodes", "0");
//			//TNSOCR.Cfg_SetOption(CfgObj, TNSOCR32.BT_DEFAULT, "Zoning/DetectInversion", "0");
//			//TNSOCR.Cfg_SetOption(CfgObj, TNSOCR32.BT_DEFAULT, "Zoning/DetectRotation", "0");
//			//TNSOCR.Cfg_SetOption(CfgObj, TNSOCR32.BT_DEFAULT, "Main/FastMode", "2");

//			TNSOCR.Ocr_Create(CfgObj, out OcrObj);
//			TNSOCR.Scan_Create(CfgObj, out ScanObj);
//			TNSOCR.Cfg_SetOption(CfgObj, TNSOCR32.BT_DEFAULT, "Saver/PDF/ImageLayer", "1");
//			res = TNSOCR.Svr_Create(CfgObj, format, out SvrObj);
//			TNSOCR.Svr_NewDocument(SvrObj);
//		}

////---------------------------------------------------------------------------------
//		static void UnInit()
//		{
//			if(ImgObj != 0) TNSOCR.Img_Destroy(ImgObj);
//			if(OcrObj != 0) TNSOCR.Ocr_Destroy(OcrObj);
//			if(CfgObj != 0) TNSOCR.Cfg_Destroy(CfgObj);
//			if(ScanObj != 0) TNSOCR.Scan_Destroy(ScanObj);
//			if(SvrObj != 0) TNSOCR.Svr_Destroy(SvrObj);

//			TNSOCR.Engine_Uninitialize();

//			OnUpdatedCreatePDF = null;
//			OnStartExtractImage = null;
//			OnUpdatedExtractImage = null;

//			if((CancelReq == true) && (OnCancelled != null))
//				OnCancelled(null);
//		}

////---------------------------------------------------------------------------------
//		public static void GetPDF(string path, string dst, string title)
//		{
//			Init();
//			List<string> paths = null;

//			if(Path.GetExtension(path).ToLower() == ".pdf")
//			{
//				paths = ExtractImage(path);

//			}else
//			{
//				paths = new List<string>();
//				paths.Add(path);
//			}

//			if(paths != null)
//				GetPDF(paths, dst, title);

//			UnInit();
//		}

////---------------------------------------------------------------------------------
//		public static void GetPDF(List<string> paths, string dst, string title)
//		{
//			Init();

//			int flags = TNSOCR32.FMT_EDITCOPY;
//			int res;

//			foreach(string path in paths)
//			{
//				if(CancelReq)
//					break;

//				res = TNSOCR.Img_Create(OcrObj, out ImgObj);
//				res = TNSOCR.Img_LoadFile(ImgObj, path);
//				res = TNSOCR.Img_OCR(ImgObj, TNSOCR32.OCRSTEP_FIRST, TNSOCR32.OCRSTEP_ZONING - 1, TNSOCR32.OCRFLAG_NONE);
//				res = TNSOCR.Img_OCR(ImgObj, TNSOCR32.OCRSTEP_ZONING, TNSOCR32.OCRSTEP_LAST, TNSOCR32.OCRFLAG_NONE);
//				res = TNSOCR.Svr_AddPage(SvrObj, ImgObj, flags);
//				res = TNSOCR.Img_Destroy(ImgObj);

//				if(OnUpdatedCreatePDF != null)
//					OnUpdatedCreatePDF(null);
//			}

//			if(!CancelReq)
//			{
//				res = TNSOCR.Svr_SetDocumentInfo(SvrObj, TNSOCR32.INFO_PDF_TITLE, title);
//				res = TNSOCR.Svr_SaveToFile(SvrObj, dst);

//			}else
//			{
//				CancelReq = false;
//			}

//			OnUpdatedCreatePDF = null;

//			UnInit();
//		}

////---------------------------------------------------------------------------------
//		public static List<string> ExtractPDFToImage(string[] paths)
//		{
//			Init();

//			List<string> ans = new List<string>();

//			foreach(string path in paths)
//			{
//				if(CancelReq)
//				{
//					CancelReq = false;
//					break;
//				}

//				if(Path.GetExtension(path).ToLower() == ".pdf")
//				{
//					List<string> images = ExtractImage(path);
//					ans.AddRange(images);

//				}else
//				{
//					ans.Add(path);
//				}
//			}

//			UnInit();
//			return ans;
//		}

////---------------------------------------------------------------------------------
//        /// <summary>
//        /// Convert PDF to BITMAP
//        /// </summary>
//        /// <param name="pdfFile"></param>
//        /// <returns></returns>
//		static List<string> ExtractImage(string pdfFile)
//		{
//			int res;
//			List<string> ans = new List<string>();

//			res = TNSOCR.Img_Create(OcrObj, out ImgObj);
//			res = TNSOCR.Img_LoadFile(ImgObj, pdfFile);

//			int cnt = TNSOCR.Img_GetPageCount(ImgObj);

//			if((0 < cnt) && (OnStartExtractImage != null))
//				OnStartExtractImage(cnt);

//			for(int i = 0; i < cnt; i++)
//			{
//				if(CancelReq)
//				{
//					CancelReq = false;
//					break;
//				}

//				string path = FileFolder.GetTempFolder() + Path.GetRandomFileName() + ".bmp";

//				res = TNSOCR.Img_SetPage(ImgObj, i);
//				res = TNSOCR.Img_SaveToFile(ImgObj, path, TNSOCR32.IMG_FORMAT_BMP, 0);
//				ans.Add(path);

//				if(OnUpdatedExtractImage != null)
//					OnUpdatedExtractImage(null);
//			}

//			res = TNSOCR.Img_Destroy(ImgObj);

//			return ans;
//		}

////---------------------------------------------------------------------------------
//	}
//}
