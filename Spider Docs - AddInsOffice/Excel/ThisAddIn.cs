using System;
using Microsoft.Office.Interop.Excel;

namespace AddInExcel2013
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
			Application.WorkbookActivate += new AppEvents_WorkbookActivateEventHandler(Application_WorkbookActivate);
			Application.WorkbookBeforeClose += new AppEvents_WorkbookBeforeCloseEventHandler(Application_WorkbookBeforeClose);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

		void Application_WorkbookActivate(Workbook wb)
		{
			Globals.Ribbons.Ribbon1.SetMenuEnabled();
		}

		void Application_WorkbookBeforeClose(Workbook Wb, ref bool Cancel)
		{
		}

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
