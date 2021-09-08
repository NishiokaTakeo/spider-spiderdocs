using System;
using System.Windows.Forms;
using NLog;
using SpiderDocsServerModule;

namespace SpiderDocsServer
{
	static class Program
	{
        static Logger logger = NLog.LogManager.GetCurrentClassLogger();

		[STAThread]
		static void Main(string[] args)
		{
            try { 
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			SpiderDocsApplication.LoadAllSettings2();

			bool Isminimized = false;

			foreach(string arg in args)
			{
				switch(arg)
				{
				case "minimized":
					Isminimized = true;
					break;
				}
			}

			//if(!SpiderDocsModule.Utilities.IsAnotherInstance((IntPtr)MMF.ReadData<int>(MMF_Items.WindowHandle)))			
			if(!SpiderDocsModule.Utilities.IsAnotherInstance((IntPtr)MMF.ReadData<int>(MMF_Items.WindowHandle)))
			{
				frmMain frmMain = new frmMain(Isminimized);
				Application.Run(frmMain);
			}
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }

        }
	}
}
