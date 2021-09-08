using System;
using Microsoft.Office.Tools.Ribbon;
using SpiderDocsForms;
using Document = SpiderDocsForms.Document;
using AddInModules;
using NLog;
namespace AddInExcel2013
{
	public partial class Ribbon : SpiderDocsRibbon
	{
		Microsoft.Office.Interop.Excel.Workbook activeDocument
		{
			get
			{
				if(0 < Globals.ThisAddIn.Application.Workbooks.Count)
					return Globals.ThisAddIn.Application.ActiveWorkbook;
				else
					return null;
			}
		}

		private void Ribbon_Load(object sender, RibbonUIEventArgs e)
		{
            logger.Trace("Begin");

            initialize(menuSaveSpider, buttonSaveWorkspace);
            logger.Trace("End");
        }

		override public void SetMenuEnabled()
		{
            logger.Trace("Begin");
            SetMenuEnabled(activeDocument != null ? activeDocument.FullName : "");
            logger.Trace("End");
        }

		private void btnSave_Click(object sender, RibbonControlEventArgs e)
		{
			string strDocPath = AddInModule.AddExtension(activeDocument.FullName, ".xlsx");
			AddInModule.SaveDocument(strDocPath, activeDocument, ExcelSaveMethods);
		}

		private void buttonSaveWorkspace_Click(object sender, RibbonControlEventArgs e)
		{
			string strDocPath = AddInModule.AddExtension(activeDocument.FullName, ".xlsx");
			AddInModule.SaveWorkspace(strDocPath, activeDocument, ExcelSaveMethods);
		}

		bool ExcelSaveMethods(string path, Document doc)
		{
			if(!activeDocument.ReadOnly)
			{
				if(String.IsNullOrEmpty(path))
					activeDocument.Save();
				else
					activeDocument.SaveAs(path);
			}

			return true;
		}
	}
}
