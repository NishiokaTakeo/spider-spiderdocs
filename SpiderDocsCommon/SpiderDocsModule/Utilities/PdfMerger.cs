using System;
using System.Collections.Generic;
using System.IO;
//using iTextSharp.text;
//using iTextSharp.text.pdf;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public static class PdfMerger
	{
		const int HEADER_MERGIN = 30;
		const int FOOTER_MERGIN = 30;

        //---------------------------------------------------------------------------------
        ///// <param name="CompressionLevel">0: No compression to 9: The highest compression</param>
        //public static void MergeFiles(List<string> sourceFilePath, string dst_path, int CompressionLevel = -1)
        //{
        //	MergeFiles(sourceFilePath, dst_path, false, 0, 0, false, 0, 0, null, CompressionLevel);
        //}

        ///// <param name="CompressionLevel">0: No compression to 9: The highest compression</param>
        //public static void MergeFiles(
        //					List<string> sourceFilePath, string dst_path,
        //					bool print_header, float header_x, float header_y,
        //					bool print_footer, float footer_x, float footer_y,
        //                          iTextSharp.text.Font font,
        //					int CompressionLevel = -1)
        //{
        //	iTextSharp.text.Document document = new iTextSharp.text.Document();
        //	using (MemoryStream ms = new MemoryStream())
        //	{
        //              iTextSharp.text.pdf.PdfReader reader;
        //              iTextSharp.text.pdf.PdfCopy copy = new iTextSharp.text.pdf.PdfCopy(document, ms);
        //		document.Open();
        //		int documentPageCounter = 0;
        //		int numberOfPages;

        //		if(font == null)
        //			font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 9);

        //		// Iterate through all pdf documents
        //		for (int fileCounter = 0; fileCounter < sourceFilePath.Count; fileCounter++)
        //		{
        //			// Create pdf reader
        //			FileStream fs = new FileStream(sourceFilePath[fileCounter], FileMode.Open);
        //			byte[] buf = new byte[fs.Length];
        //			fs.Read(buf, 0, buf.Length);

        //			reader = new iTextSharp.text.pdf.PdfReader(buf);
        //			numberOfPages = reader.NumberOfPages;

        //			// Iterate through all pages
        //			for (int currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
        //			{
        //				documentPageCounter++;
        //                      iTextSharp.text.pdf.PdfImportedPage importedPage = copy.GetImportedPage(reader, currentPageIndex);
        //                      iTextSharp.text.pdf.PdfCopy.PageStamp pageStamp = copy.CreatePageStamp(importedPage);

        //				// Write header
        //				if(print_header)
        //				{
        //					if(header_x <= 0)
        //						header_x = importedPage.Width / 2;

        //					if(header_y <= 0)
        //						header_y = importedPage.Height - HEADER_MERGIN;

        //                          iTextSharp.text.pdf.ColumnText.ShowTextAligned(pageStamp.GetOverContent(), iTextSharp.text.Element.ALIGN_CENTER,
        //						new iTextSharp.text.Phrase(/*"Page " + */documentPageCounter.ToString(), font), header_x, (importedPage.Height - header_y),
        //						importedPage.Width < importedPage.Height ? 0 : 1);
        //				}

        //				// Write footer
        //				if(print_footer)
        //				{
        //					if(footer_x <= 0)
        //						footer_x = importedPage.Width / 2;

        //					if(footer_y <= 0)
        //						footer_y = FOOTER_MERGIN;

        //                          iTextSharp.text.pdf.ColumnText.ShowTextAligned(pageStamp.GetOverContent(), iTextSharp.text.Element.ALIGN_CENTER,
        //						new iTextSharp.text.Phrase(/*"Page " + */documentPageCounter.ToString(), font), footer_x, footer_y,
        //						importedPage.Width < importedPage.Height ? 0 : 1);
        //				}

        //				pageStamp.AlterContents();

        //				copy.AddPage(importedPage);
        //			}

        //			copy.FreeReader(reader);
        //			reader.Close();

        //			fs.Close();
        //			fs.Dispose();
        //		}

        //		document.Close();

        //		reader = new iTextSharp.text.pdf.PdfReader(ms.ToArray());
        //              iTextSharp.text.pdf.PdfStamper stamper = new iTextSharp.text.pdf.PdfStamper(reader, new FileStream(dst_path, FileMode.Create));

        //		numberOfPages = reader.NumberOfPages;
        //		for(int i = 1; i <= numberOfPages; i++)
        //			reader.SetPageContent(i, reader.GetPageContent(i), CompressionLevel);

        //		stamper.SetFullCompression(); 
        //		stamper.Close();
        //		reader.Close();

        //		copy.Close();

        //		ms.Close();
        //		ms.Dispose();
        //	}
        //}

        public static void MergeFiles(List<string> filesPath, string outPutFilePath)
        {
            List<iTextSharp.text.pdf.PdfReader> readerList = new List<iTextSharp.text.pdf.PdfReader>();
            foreach (string filePath in filesPath)
            {
                iTextSharp.text.pdf.PdfReader pdfReader = new iTextSharp.text.pdf.PdfReader(filePath);
                readerList.Add(pdfReader);
            }

            //Define a new output document and its size, type
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 0, 0, HEADER_MERGIN, FOOTER_MERGIN);
            //Create blank output pdf file and get the stream to write on it.
            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(outPutFilePath, FileMode.Create));
            document.Open();

            foreach (iTextSharp.text.pdf.PdfReader reader in readerList)
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    iTextSharp.text.pdf.PdfImportedPage page = writer.GetImportedPage(reader, i);
                    document.Add(iTextSharp.text.Image.GetInstance(page));
                }
            }
            document.Close();
            writer.Close();
        }


        //---------------------------------------------------------------------------------
    }
}
