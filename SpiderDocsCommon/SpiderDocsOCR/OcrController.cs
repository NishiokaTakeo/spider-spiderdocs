using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using SpiderDocsOCR.HocrElements;
using SpiderDocsOCR.ImageProcessors;
using Tesseract;
using ImageFormat = System.Drawing.Imaging.ImageFormat;
using NLog;

namespace SpiderDocsOCR
{
    internal class OcrController
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        static string program_files_folder = ProgramFilesx86() + @"\Spider Docs AddIns\";

        internal void AddToDocument(string language, Image image, ref HDocument doc, string sessionName)
        {
            Bitmap b = ImageProcessor.GetAsBitmap(image, (int) Math.Ceiling(image.HorizontalResolution));
            string imageFile = TempData.Instance.CreateTempFile(sessionName, ".tif");
            b.Save(imageFile, ImageFormat.Tiff);
            string result = CreateHocr(language, imageFile, sessionName);
            doc.AddFile(result);
            b.Dispose();
        }

        public string CreateHocr(string language, string imagePath, string sessionName)
        {
            string dataFolder = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            string dataPath = TempData.Instance.CreateDirectory(sessionName, dataFolder);
            string outputFile = Path.Combine(dataPath, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));

            string enginePath = string.Empty;
            try
            {
                //enginePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException(), "tessdata");
                enginePath = Path.Combine(program_files_folder ?? throw new InvalidOperationException(), "tessdata");
            }
            catch (Exception e)
            {
                logger.Error(e);
                enginePath = Path.Combine(Environment.CurrentDirectory, "tessdata");
            }




            logger.Debug("Tesseract data path:{0}",enginePath);

            using (TesseractEngine engine = new TesseractEngine(enginePath, "eng"))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {                        
                        string hocrtext = page.GetHOCRText(0);
                        logger.Debug("hocrtext:{0}", hocrtext);

                        File.WriteAllText(outputFile + ".hocr", hocrtext);
                    }
                }
            }
            return outputFile + ".hocr";
        }

        static string ProgramFilesx86()
        {
            if (Is64bit())
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            else
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        }

        static bool Is64bit()
        {
            if ((8 == IntPtr.Size)
            || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return true;
            }

            return false;
        }

    }
}