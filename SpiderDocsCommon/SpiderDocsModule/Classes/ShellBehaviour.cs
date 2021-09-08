using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Win32;
using NLog;
//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{

	public class ShellBehaviour
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        readonly Models.ShellBehaviour _work;

        public ShellBehaviour(Models.ShellBehaviour extensionBehaviour)
        {
            _work = extensionBehaviour;
        }

        string GetShellTxt(Models.ShellBehaviour.en_Shell shell)
        {
            switch(shell)
            {
                case Models.ShellBehaviour.en_Shell.Edit:
                    return "Edit";
                case Models.ShellBehaviour.en_Shell.New:
                    return "New";
                case Models.ShellBehaviour.en_Shell.Open:
                    return "Open";
                case Models.ShellBehaviour.en_Shell.Print:
                    return "Print";
                case Models.ShellBehaviour.en_Shell.Printto:
                    return "Printto";
                default:
                    return string.Empty;              
            }
        }

        string Seek()
        {            
            string command = string.Empty;

            if (string.IsNullOrWhiteSpace(_work.extension) || _work.override_behaviour == Models.ShellBehaviour.en_Shell.None) return command;

            using (RegistryKey keyExt = Registry.ClassesRoot.OpenSubKey(_work.extension))
            {
                logger.Debug("Extension:{0}", _work.extension);

                if (keyExt == null) return command;

                var keyRealExt = keyExt.GetValue("");
                
                if (string.IsNullOrEmpty(keyRealExt.ToString())) return command;

                logger.Debug("Real extension:{0}", keyRealExt);
                using (RegistryKey keyReal = Registry.ClassesRoot.OpenSubKey(keyRealExt.ToString()))
                {
                    
                    if (keyReal == null) return command;

                    string keycommandPath = string.Format(@"shell\{0}\command", GetShellTxt(_work.override_behaviour));

                    logger.Debug("Command:{0}", keycommandPath);
                    using (RegistryKey keyCommand = keyReal.OpenSubKey(keycommandPath))
                    {
                        command = keyCommand.GetValue("").ToString();
                    }
                }
            }
           
            return command;
        }

        string ParameterSetup(string command,string path)
        {
            
            string newBehaviour = command.Replace("%1", path);

            return newBehaviour;
        }

        public int Run(string path)
        {
            int processId = 0;
            string command = Seek();

            if (string.IsNullOrWhiteSpace(command))
            {
                //logger.Warn("File behaviour is not found:{0}", Newtonsoft.Json.JsonConvert.SerializeObject(_work));

                processId = RunDefault(path);

                return processId;
            }

            string splitter = @"\.exe""?\s";
            string finalized = string.IsNullOrWhiteSpace(command) ? path : ParameterSetup(command, path);

            try
            {
                string[] exeOrArgs = System.Text.RegularExpressions.Regex.Split(finalized, splitter, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                string
                        exe = exeOrArgs[0] + (exeOrArgs[0].Substring(0, 1) == "\"" ? ".exe\" " : ".exe "), // must keep white space

                        args = exeOrArgs[1];


                using (System.Diagnostics.Process ps = new System.Diagnostics.Process())
                {
                    ps.StartInfo.UseShellExecute = false;
                    ps.StartInfo.FileName = exe;
                    ps.StartInfo.Arguments = args;
                    ps.StartInfo.CreateNoWindow = true;
                    ps.Start();

                    processId = ps.Id;
                }

            }catch(Exception ex)
            {
                logger.Error(ex);
                RunDefault(finalized);
            }
            return processId;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>process id</returns>
        int RunDefault(string path)
        {
            //System.Threading.Tasks.Task.Factory.StartNew(() => System.Diagnostics.Process.Start(path));

            //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //startInfo.FileName = "cmd.exe";
            try
            { 
                using (System.Diagnostics.Process ps = new System.Diagnostics.Process())
                {
                    //ps.StartInfo.Arguments = path;
                    //ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    //ps.StartInfo.FileName = "cmd.exe";

                    //ps.StartInfo.UseShellExecute = false;
                    ps.StartInfo.FileName = path;
                    //ps.StartInfo.Arguments = args;
                    //ps.StartInfo.CreateNoWindow = true;
                    ps.Start();
                
                    return ps.Id;
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex);

                // zero means the process has been expired or not started.some case happens.
                return 0;
            }
        }
    }
}
