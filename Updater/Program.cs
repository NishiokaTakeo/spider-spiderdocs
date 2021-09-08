using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using NLog;

namespace Updater
{
	static class Program
	{
        //static Logger logger = LogManager.GetLogger("Updater");

        static Logger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// The main entry point for the application.
        /// This exe will be called by AutoUpdateService
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
            logger.Info("Main Start");

            if(args.Length != 2)
            {
                logger.Error("Argments aren't correct");
                return ;
            }

            logger.Info("args[1] -> {0}: args[2] -> {1}", args[0], args[1]);
            
            string installer_path = args[0] //user temp/setup.exe path
                    ,log_path = args[1];  // log path

            TimeSpan timeout = TimeSpan.FromMilliseconds(50000);
            ServiceController service = new ServiceController("Spider Docs Auto Update");

            try
            {
                if( service.CanStop )
                {
                    logger.Info("Attemping Stop");

                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);                    
                }

                logger.Info("Spider Docs Auto Update is stopped");
                
            }catch {}

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = installer_path;
            //startInfo.Arguments = "/s /v/qn /v/log /v" + mInstalationLogPath;
            startInfo.Arguments = "/S /s /v/qn /v/log /v" + log_path;
        
            // To get administrator right
            startInfo.Verb = "runas";

            //start installation process
            process.StartInfo = startInfo;
            process.Start();

            logger.Info("setup.exe is run {0} , {1}", startInfo.FileName, startInfo.Arguments);
            
            int cnt = 0;
            while(!process.HasExited)
            {
                System.Threading.Thread.Sleep(1000);
                cnt++;

                if( cnt % 10 == 0 )
                {
                    logger.Info("Waiting {0}",cnt);
                }
            }
            
            if( process.HasExited )
            {
                logger.Info("Install is finished {0}",cnt);
                process.Close();
            }

            //int cnt = 0;
            //while((cnt++ < 180) && !process.HasExited)
            //    System.Threading.Thread.Sleep(1000);
            //
            //if(!process.HasExited)
            //    process.Close();        
		}
	}

    /*
    class Logger
    {

        readonly string logs_path = ProgramFilesx86() + "\\Spider Docs\\";
        StringBuilder _builtText;

        public Logger(string title)
        {
            this._builtText = new StringBuilder(string.Format("{0}: {1}", DateTime.Now, title + Environment.NewLine));
        }

        string GetLogsFolder()
        {
            return logs_path;
        }
        public void Append(string str, int indent = 1)
        {
            string tab = "";
            for (int i = 1; i <= indent; i++)
                tab += "\t";

            _builtText.Append(tab + str + Environment.NewLine);
            Put();
        }
        //---------------------------------------------------------------------------------
        // error logger --------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        public bool Put()
        {
            bool ans = true;

            try
            {
                string pathLogFile = GetLogsFolder() + "SpiderDocsLog.txt";
                StreamWriter swLog;

                if (!Directory.Exists(GetLogsFolder()))
                    Directory.CreateDirectory(GetLogsFolder());

                if (!System.IO.File.Exists(pathLogFile))
                    swLog = new StreamWriter(pathLogFile);
                else
                    swLog = System.IO.File.AppendText(pathLogFile);

                swLog.WriteLine(_builtText.ToString());
                swLog.WriteLine();
                swLog.Close();
            }
            catch
            {
                ans = false;
            }

            return ans;
        }

        static string ProgramFilesx86()
        {
            if (Is64bit())
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            else
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        }

        //---------------------------------------------------------------------------------
        static string Systemx86()
        {
            if (Is64bit())
                return Environment.GetFolderPath(Environment.SpecialFolder.SystemX86);
            else
                return Environment.GetFolderPath(Environment.SpecialFolder.System);
        }

        //---------------------------------------------------------------------------------
        static bool Is64bit()
        {
            if ((8 == IntPtr.Size)
            || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return true;
            }

            return false;
        }
    }
    */
    

}
