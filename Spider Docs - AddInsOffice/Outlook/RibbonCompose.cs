using System;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools.Ribbon;
using SpiderDocsForms;
using NLog;
namespace AddInOutlook2013
{
	public partial class RibbonCompose : SpiderDocsOutlookRibbon
	{

		public RibbonCompose()
		{
            try{
                InitializeComponent();
                initialize(menuSaveSpiderCompose, buttonSaveWorkspaceCompose);
            }
            catch (System.Exception error)
            {
                logger.Error(error);
            }
		}

//---------------------------------------------------------------------------------
		private void btnSaveToDatabse_Click(object sender, RibbonControlEventArgs e)
		{
            try
            {
                SaveToSpiderDocs(e.Control.Context, true);
            }
            catch (System.Exception error)
            {
                logger.Error(error);
            }
		}

//---------------------------------------------------------------------------------
		private void btnSaveWorkSpace_Click(object sender, RibbonControlEventArgs e)
		{
            try
            {
                SaveToWorkSpace(e.Control.Context);
            }
            catch (System.Exception error)
            {
                logger.Error(error);
            }

		}
	}
}
