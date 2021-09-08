using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.PowerPoint;
using System.IO;
using Marshal = System.Runtime.InteropServices.Marshal;
using Spider.IO;
using SpiderDocsModule;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
	public static class PDFConverter
	{
//---------------------------------------------------------------------------------
		enum en_FileType
		{
			Word,
			Excel,
			PowerPoint,
			NotSuport,

			Max
		}

//---------------------------------------------------------------------------------
		public static string pdfconversion(string inputfile, string outpath)	//to export office files to pdf
		{
			return pdfconversion(inputfile, outpath, true);
		}

		public static string pdfconversion(string inputfile, string outpath, bool del)	//to export office files to pdf
		{
			bool ans = false;

			string ext = Path.GetExtension(inputfile).ToLower();
			string name = Path.GetFileName(outpath);
			string outputfile;

			if(name == "")
			{
				name = Path.GetFileNameWithoutExtension(inputfile);
				outputfile = outpath + name + ".pdf";

			}else
			{
				outputfile = outpath;
			}

			switch(FileFolder.OfficeCheck(ext))
			{
				case en_OfficeType.Word:
				ans = ExportWordToPdf(inputfile, outputfile);
				break;

				case en_OfficeType.Excel:
				ans = ExportWorkbookToPdf(inputfile, outputfile);
				break;

				case en_OfficeType.PowerPoint:
				ans = ExportpptToPdf(inputfile, outputfile);
				break;
			}

			if(ans)
			{
				if(del)
					File.Delete(inputfile);
			}else
			{
				outputfile = "";
			}

			return outputfile;
		}

//---------------------------------------------------------------------------------
		static bool ExportWordToPdf(string workbookPath, string outputPath)
		{
			// Create COM Objects
			Microsoft.Office.Interop.Word.Application wordApp;
			Microsoft.Office.Interop.Word.Document wordDoc;       

			// Create new instance of Word
			wordApp = new Microsoft.Office.Interop.Word.Application();

			// Make the process invisible to the user
			wordApp.ScreenUpdating = false;

			// Make the process silent
			wordApp.DisplayAlerts = 0;

			// Open the word document that you wish to export to PDF
			wordDoc = wordApp.Documents.Open(workbookPath);

			bool exportSuccessful = true;
			try
			{
				// Call Word's native export function (valid in Office 2007 and Office 2010, AFAIK)
				wordDoc.ExportAsFixedFormat(outputPath, WdExportFormat.wdExportFormatPDF, Item: WdExportItem.wdExportDocumentWithMarkup);
			}
			catch
			{
				// Mark the export as failed for the return value...
				exportSuccessful = false;
			}
			finally
			{
				// Close the workbook, quit the Word, and clean up regardless of the results...
				((Microsoft.Office.Interop.Word._Document)wordDoc).Close();
				((Microsoft.Office.Interop.Word._Application)wordApp).Quit();

				wordApp = null;
				wordDoc = null;
			}

			return exportSuccessful;
		}

//---------------------------------------------------------------------------------
		static bool ExportWorkbookToPdf(string workbookPath, string outputPath)
		{
			// Create COM Objects
			Microsoft.Office.Interop.Excel.Application excelApplication;
			Microsoft.Office.Interop.Excel.Workbooks excelWorkbooks;
			Microsoft.Office.Interop.Excel.Workbook excelWorkbook;

			// Create new instance of Excel
			excelApplication = new Microsoft.Office.Interop.Excel.Application();

			// Make the process invisible to the user
			excelApplication.ScreenUpdating = false;

			// Make the process silent
			excelApplication.DisplayAlerts = false;

			// Open the workbook that you wish to export to PDF
			excelWorkbooks = excelApplication.Workbooks;
			excelWorkbook = excelWorkbooks.Open(workbookPath);

			bool exportSuccessful = true;
			try
			{
				// Call Excel's native export function (valid in Office 2007 and Office 2010, AFAIK)
				excelWorkbook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, outputPath);
			}
			catch
			{
				// Mark the export as failed for the return value...
				exportSuccessful = false;
			}
			finally
			{
				// Close the workbook, quit the Excel, and clean up regardless of the results...
				excelWorkbook.Close();
				excelWorkbooks.Close();
				excelApplication.Quit();

				Marshal.ReleaseComObject(excelWorkbook);
				Marshal.ReleaseComObject(excelWorkbooks);
				Marshal.ReleaseComObject(excelApplication);
					
				excelWorkbook = null;
				excelWorkbooks = null;
				excelApplication = null;

				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}

			return exportSuccessful;
		}

//---------------------------------------------------------------------------------
		static bool ExportpptToPdf(string workbookPath, string outputPath)
		{
			// Create COM Objects
			Microsoft.Office.Interop.PowerPoint._Application PPApplication;
			Presentations PPDocs;
			Presentation PPDoc;

			// Create new instance of powerpoint
			PPApplication = new Microsoft.Office.Interop.PowerPoint.Application();
			PPDocs = PPApplication.Presentations;

			// Make the process silent
			PPApplication.DisplayAlerts = 0;

			// Open the powerpoint that you wish to export to PDF
			PPDoc = PPDocs.Open(FileName: workbookPath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);

			bool exportSuccessful = true;
			try
			{
				// Call powerPOINT's native export function (valid in Office 2007 and Office 2010, AFAIK)
				PPDoc.ExportAsFixedFormat(outputPath, PpFixedFormatType.ppFixedFormatTypePDF);
			}
			catch
			{
				// Mark the export as failed for the return value...
				exportSuccessful = false;
			}
			finally
			{
				// Close the workbook, quit the Excel, and clean up regardless of the results...
				PPDoc.Close();
				PPApplication.Quit();

				Marshal.ReleaseComObject(PPDoc);
				Marshal.ReleaseComObject(PPDocs);
				Marshal.ReleaseComObject(PPApplication);
					
				PPDoc = null;
				PPDocs = null;
				PPApplication = null;

				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}

			return exportSuccessful;
		}

//---------------------------------------------------------------------------------
	}
}
