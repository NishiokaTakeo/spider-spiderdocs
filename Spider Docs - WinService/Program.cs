using System;
using System.ServiceProcess;
using System.Windows.Forms;
using SpiderDocsServerModule;
using NLog;

namespace SpiderDocsWinService
{
	static class Program
	{

        static Logger logger = LogManager.GetCurrentClassLogger();
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
            logger.Info("The application has been started.");

            SpiderDocsApplication.LoadAllSettings2();

			if(Environment.UserInteractive)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Form1());

			}else
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[] { new Service() };
				ServiceBase.Run(ServicesToRun);
			}
		}
	}
}
