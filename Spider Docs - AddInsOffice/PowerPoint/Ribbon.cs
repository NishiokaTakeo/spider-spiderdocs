using System;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Core;
using SpiderDocsForms;
using Document = SpiderDocsForms.Document;
using AddInModules;

namespace AddInPowerPoint2013
{
	public partial class Ribbon : SpiderDocsRibbon
	{
		Microsoft.Office.Interop.PowerPoint.Presentation activeDocument
		{
			get
			{
				if(0 < Globals.ThisAddIn.Application.Presentations.Count)
					return Globals.ThisAddIn.Application.ActivePresentation;
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
			string strDocPath = AddInModule.AddExtension(activeDocument.FullName, ".pptx");
			AddInModule.SaveDocument(strDocPath, activeDocument, PowerPointSaveMethods);
		}

		private void buttonSaveWorkspace_Click(object sender, RibbonControlEventArgs e)
		{
			string strDocPath = AddInModule.AddExtension(activeDocument.FullName, ".pptx");
			AddInModule.SaveWorkspace(strDocPath, activeDocument, PowerPointSaveMethods);
		}

		bool PowerPointSaveMethods(string path, Document doc)
		{
			if(activeDocument.ReadOnly == MsoTriState.msoFalse)
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
