using System;
using Microsoft.Office.Tools.Ribbon;
using SpiderDocsForms;
using Document = SpiderDocsForms.Document;
using AddInModules;

namespace AddInWord2013
{
	public partial class Ribbon : SpiderDocsRibbon
	{
		Microsoft.Office.Interop.Word.Document activeDocument
		{
			get
			{
				if(0 < Globals.ThisAddIn.Application.Documents.Count)
					return Globals.ThisAddIn.Application.ActiveDocument;
				else
					return null;
			}
		}

		private void Ribbon_Load(object sender, RibbonUIEventArgs e)
		{
			initialize(menuSaveSpider, buttonSaveWorkspace);
		}

		override public void SetMenuEnabled()
		{
			SetMenuEnabled(activeDocument != null ? activeDocument.FullName : "");
		}

		private void btnSave_Click(object sender, RibbonControlEventArgs e)
		{
			string strDocPath = AddInModule.AddExtension(activeDocument.FullName, ".docx");
			AddInModule.SaveDocument(strDocPath, activeDocument, WordSaveMethods);
		}

		private void buttonSaveWorkspace_Click(object sender, RibbonControlEventArgs e)
		{
			string strDocPath = AddInModule.AddExtension(activeDocument.FullName, ".docx");
			AddInModule.SaveWorkspace(strDocPath, activeDocument, WordSaveMethods);
		}

		bool WordSaveMethods(string path, Document doc)
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
