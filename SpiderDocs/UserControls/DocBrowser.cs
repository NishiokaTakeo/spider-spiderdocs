using System;
using System.Linq;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using Marshal = System.Runtime.InteropServices.Marshal;
using SpiderDocsModule;
using SpiderDocsForms;
using Spider;

namespace SpiderDocs
{
	public partial class DocBrowser : Spider.Forms.UserControlBase
	{
		private readonly string PdfStr = "#toolbar=0&navpanes=0";
		private string _tempFileName;
		private WebBrowser _wb;
		private delegate void ConvertDocumentDelegate(string fileName);
		private delegate void Del_SetCursor(Cursor val, bool visible);
		public string FilePath = "";
		private bool delete = false;
		public bool busy = false;

		public DocBrowser()
		{
			InitializeComponent();
			this.SetWebVrowserControl();
		}

		private void SetWebVrowserControl()
		{
			this._wb = new WebBrowser();
			this._wb.Dock = DockStyle.Fill;
			this._wb.Name = "webBrowser1";

			this.Controls.Add(this._wb);

			// set up an event handler to delete our temp file when we're done with it. 
			this._wb.DocumentCompleted += this.webBrowser1_DocumentCompleted;
		}

		public bool LoadDocument()
		{
			bool ans = false;

			if(!busy && File.Exists(FilePath))
			{
				busy = true;
				SetCursor(System.Windows.Forms.Cursors.WaitCursor, false);

				// Call ConvertDocument asynchronously. 
				ConvertDocumentDelegate del = new ConvertDocumentDelegate(ConvertDocument);

				// Call DocumentConversionComplete when the method has completed. 
				del.BeginInvoke(FilePath, DocumentConversionComplete, null);

				ans = true;
			}

			return ans;
		}

		void ConvertDocument(string fileName)
		{
			string ext = Path.GetExtension(fileName).ToLower();
			bool ans = false;

			switch(ext)
			{
			case ".pdf":
				_tempFileName = fileName + PdfStr;
				ans = true;
				delete = false;
				break;

			case ".jpg":
			case ".gif":
			case ".png":
			case ".bmp":
				_tempFileName = fileName;
				ans = true;
				delete = false;
				break;

			case ".xls":
			case ".xlsx":
				if(SpiderDocsApplication.IsExcel)
				{
					_tempFileName = LoadExcel(fileName);

					if(_tempFileName != "")
						ans = true;
				}
				break;

			case ".doc":
			case ".docx":
			case ".txt":
				if(SpiderDocsApplication.IsWord)
				{
					_tempFileName = GetTempFile("html");
					ans = LoadWord(fileName, _tempFileName);
					delete = true;
				}
				break;
			}

			if(!ans)
			{
				_tempFileName = GetTempFile("html");
				
				StreamWriter  fs = new StreamWriter(_tempFileName);
				string msg = "<font size=\"5\" color=\"red\">Preview is not available.</font>";
				fs.Write(msg);
				fs.Close();
				
				delete = true;
			}
		}

		private bool LoadWord(string oldFileName, string newFileName)
		{
			bool ans = false;
			Word.Application app = null;

			try
			{
				app = new Word.Application();
				app.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;

				Word.Document doc = app.Documents.Open(oldFileName, ReadOnly: true);

				// We will be saving this file as HTML format. 
				object fileType = (object)Word.WdSaveFormat.wdFormatHTML;

				doc.SaveAs(FileName: newFileName, FileFormat: fileType);

				// Resolving ambiguity between method Close() and non-method Close.
				((Word._Document)doc).Close();

				ans = true;
			}
			finally
			{
				if(app != null)
					// Resolving ambiguity between method Quit() and non-method Quit.
					((Word._Application)app).Quit();
			}

			return ans;
		}

		private string LoadExcel(string fileName)
		{
			string newFileName = "";

			Excel.Application app = null;
			Excel.Workbooks workbooks = null;
			Excel.Workbook workbook = null;
			Excel.Sheets worksheets = null;
			Excel.Worksheet worksheet = null;

			try
			{
				app = new Excel.Application();
				app.DisplayAlerts = false;

				workbooks =  app.Workbooks;
				workbook = workbooks.Open(fileName, ReadOnly: true, UpdateLinks: false);
				worksheets = workbook.Worksheets;
				worksheet = worksheets.get_Item(1) as Excel.Worksheet;

				// Resolving ambiguity between method Activate()' and non-method Activate.
				((Excel._Worksheet)worksheet).Activate();

				if(!worksheet.ProtectContents &&
				   !worksheet.ProtectDrawingObjects &&
				   !worksheet.ProtectScenarios)
				{
					// We will be saving this file as HTML format. 
					newFileName = GetTempFile("html");

					var fileType = Excel.XlFileFormat.xlHtml;
					workbook.SaveAs(Filename: newFileName, FileFormat: fileType, AccessMode: Excel.XlSaveAsAccessMode.xlNoChange);

					delete = true;

				}else
				{
					newFileName = GetTempFile("pdf");

					workbook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, newFileName);
					newFileName += PdfStr;

					delete = false;
				}
			}
			finally
			{
				if(app != null)
				{
					workbook.Close();
					workbooks.Close();
					app.Quit();

					Marshal.ReleaseComObject(worksheet);
					Marshal.ReleaseComObject(worksheets);
					Marshal.ReleaseComObject(workbook);
					Marshal.ReleaseComObject(workbooks);
					Marshal.ReleaseComObject(app);
					
					worksheet = null;
					worksheets = null;
					workbook = null;
					workbooks= null;
					app = null;

					GC.Collect();
					GC.WaitForPendingFinalizers();
					GC.Collect();
					GC.WaitForPendingFinalizers();
				}
			}

			return newFileName;
		}

		void DocumentConversionComplete(IAsyncResult result)
		{
			_wb.Navigate(_tempFileName);

			Del_SetCursor del = new Del_SetCursor(SetCursor);
			this.BeginInvoke(del, System.Windows.Forms.Cursors.Default, true);
		}

		void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if(_tempFileName != string.Empty)
			{
				// delete the temp file created. 
				if(delete)
					FileFolder.DeleteTempFiles("*" + Path.GetFileNameWithoutExtension(_tempFileName) + "*");

				_tempFileName = string.Empty;
			}
			
			busy = false;
		}

		string GetTempFile(string extension)
		{
			// Uses the Combine, GetTempPath, ChangeExtension, 
			// and GetRandomFile methods of Path to 
			// create a temp file
			return Path.Combine(FileFolder.TempPath,
				Path.ChangeExtension("Preview_" + Path.GetRandomFileName(), extension));
		}

		void SetCursor(Cursor val, bool visible)
		{
			this.Cursor = val;
			_wb.Visible = visible;
		}
	}
}
