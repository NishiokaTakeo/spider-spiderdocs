using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.ServiceProcess;
using Microsoft.Win32;
using IWshRuntimeLibrary;
using NLog;
using SpiderDocsModule;
using System.Security.Principal;
using System.Security.AccessControl;

namespace SpiderDocsModule
{
    public class Setup
    {
        /*
        static bool Office2010 = false;
        static bool Office2013 = false;
        static bool Office2016 = false;
        */
        static Logger logger = LogManager.GetLogger("Updater");

        //static string program_files_folder;
        public static string program_files_folder = ProgramFilesx86() + @"\Spider Docs\";
        public static string add_ins_folder { get { return new DirectoryInfo(program_files_folder).Parent.FullName + @"\Spider Docs AddIns"; } }

        //static readonly string DesktopShortCut = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory) + "\\Spider Docs.lnk";

        //static string SendTo_Database = @"\AppData\Roaming\Microsoft\Windows\SendTo\Spider Docs - Database.lnk";
        //static string SendTo_WorkSpace = @"\AppData\Roaming\Microsoft\Windows\SendTo\Spider Docs - Workspace.lnk";
        public static string SendTo_Database = @"\Spider Docs - Database.lnk";
        public static string SendTo_WorkSpace = @"\Spider Docs - Workspace.lnk";
        public static string[] UserProfileDirs;

        public static readonly string windows_folder = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\Spider Docs\";
        public static readonly string AddInsPath = @"file:///" + ProgramFilesx86() + @"\Spider Docs AddIns\[ADD_IN_NAME].vsto|vstolocal";
        public static readonly string AddInsKey = @"Software\Microsoft\Office\";
        public static readonly string GroupPolicyKey = @"Software\Policies\Microsoft\Office\";
        public static readonly string GroupPolicyRootKey = @"Software\Policies";


        static readonly string[] AddinNames = new string[]
		{
			"AddInExcel2013",
			"AddInOutlookAttachment2013",
			"AddInOutlook2013",
			"AddInPowerPoint2013",
			"AddInWord2013"
		};

        static readonly string[] OfficeNames = new string[]
		{
			"Excel",
			"Outlook",
			"Outlook",
			"PowerPoint",
			"Word"
		};

        static readonly string[] Descriptions = new string[]
		{
			"Spider Docs Excel Addin",
			"Spider Docs Outlook Attachment Addin",
			"Spider Docs Outlook Addin",
			"Spider Docs PowerPoint Addin",
			"Spider Docs Word Addin"
		};

        static readonly int LoadBehavior = 3;

        public static void createSendTo(string root)
        {
            if (!System.IO.File.Exists(root + SendTo_Database))
                CreateShortcut(root + SendTo_Database, program_files_folder + "SpiderDocs.exe", "savefile", "Import to Database");

            if (!System.IO.File.Exists(root + SendTo_WorkSpace))
                CreateShortcut(root + SendTo_WorkSpace, program_files_folder + "SpiderDocs.exe", "workspace", "Import to Work Space");			
        }

        public static void addService(string program_files_folder)
        {
            ServiceController[] services = ServiceController.GetServices();
            ServiceController service = services.FirstOrDefault(s => s.ServiceName == "Spider Docs Auto Update");

            try
            {
                if (service == null)
                {
                    //logger.Append(string.Format("Serive is not installed : {0}", "Spider Docs Auto Update"));
                    logger.Debug("Serive is not installed : {0}", "Spider Docs Auto Update");

                    string AddServiceArg = "/C sc create \"Spider Docs Auto Update\" start= delayed-auto binPath= \"" + program_files_folder + "AutoUpdateService.exe\"";

                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = AddServiceArg;
                    process.StartInfo = startInfo;

                    // To get administrator right
                    startInfo.Verb = "runas";

                    process.Start();
                    process.WaitForExit();

                    service = new ServiceController("Spider Docs Auto Update");
                    //logger.Append(string.Format("Create Serive: {0}", AddServiceArg));
                    logger.Debug("Create Serive: {0}", AddServiceArg);
                }
                else
                {
                    //logger.Append(string.Format("Serive exists: {0}", "Spider Docs Auto Update"));
                    logger.Debug("Serive exists: {0}", "Spider Docs Auto Update");
                }

                if (service.Status != ServiceControllerStatus.Running)
                {
                    TimeSpan timeout = TimeSpan.FromMilliseconds(120000);
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                    //logger.Append(string.Format("Serive is started : {0}", "Spider Docs Auto Update"));
                    logger.Debug("Serive is started : {0}", "Spider Docs Auto Update");
                }
            }
            catch (SystemException ex)
            {
                //logger.Append(ex.ToString());
                //logger.Put();
                logger.Error(ex);
            }
            finally
            {
            }
        }
        //---------------------------------------------------------------------------------
        public static void AddInstallationInfo(string version_no)
        {
            RegistryKey regkey;

            regkey = get32BitRegKey();
            AddInstallationInfo(regkey, version_no);
            regkey.Flush();
            //logger.Append(string.Format("Add instllation info : {0} -> {1}", regkey, version_no));
            logger.Debug("Add instllation1 info : {0} -> {1}", regkey, version_no);

            regkey = get64BitRegKey();
            AddInstallationInfo(regkey, version_no);
            regkey.Flush();
            //logger.Append(string.Format("Add instllation info : {0} -> {1}", regkey, version_no));
            logger.Debug("Add instllation2 info : {0} -> {1}", regkey, version_no);



        }

        //---------------------------------------------------------------------------------
        static void AddInstallationInfo(RegistryKey regkey, string version_no)
        {
            try
            {
                regkey = regkey.CreateSubKey(@"Software\SpiderDocs\");

                regkey.SetValue("SpiderDocsPath", program_files_folder + "SpiderDocs.exe", RegistryValueKind.String);
                regkey.SetValue("Version", version_no, RegistryValueKind.String);

                regkey.Close();
                regkey.Flush();
            }
            catch { }
        }

        //---------------------------------------------------------------------------------
        public static void DeleteInstallationInfo()
        {
            RegistryKey regkey;

            try
            {
                regkey = get32BitRegKey();
                regkey = regkey.OpenSubKey(@"Software\SpiderDocs\", true);
                regkey.DeleteValue("SpiderDocsPath", false);
                regkey.DeleteValue("Version", false);
                regkey.Close();
                regkey.Flush();
            }
            catch { }

            try
            {
                regkey = get64BitRegKey();
                regkey = regkey.OpenSubKey(@"Software\SpiderDocs\", true);
                regkey.DeleteValue(@"SpiderDocsPath", false);
                regkey.DeleteValue(@"Version", false);
                regkey.Close();
                regkey.Flush();
            }
            catch { }
        }

        //---------------------------------------------------------------------------------
        static bool GetVSTOInfo(RegistryKey regkey, ref int OFFICERUNTIME, ref int VSTORUNTIMEREDIST)
        {
            bool ans = false;

            try
            {
                regkey = regkey.OpenSubKey(@"SOFTWARE\Microsoft\VSTO Runtime Setup\v4");
                if (regkey != null)
                {
                    OFFICERUNTIME = int.Parse(regkey.GetValue("Version").ToString().Replace(".", ""));
                    ans = true;
                    regkey.Close();
                    regkey.Flush();
                }
            }
            catch { }

            try
            {
                regkey = regkey.OpenSubKey(@"SOFTWARE\Microsoft\VSTO Runtime Setup\v4R");
                if (regkey != null)
                {
                    VSTORUNTIMEREDIST = int.Parse(regkey.GetValue("Version").ToString().Replace(".", ""));
                    ans = true;
                    regkey.Close();
                    regkey.Flush();
                }
            }
            catch { }

            return ans;
        }
        /*
        //---------------------------------------------------------------------------------
        static void DetectOffice()
        {
            if (DetectOffice(get32BitRegKey(), @"SOFTWARE\Microsoft\Office\16.0")
            || DetectOffice(get64BitRegKey(), @"SOFTWARE\Microsoft\Office\16.0"))
                Office2016 = true;

            if (DetectOffice(get32BitRegKey(), @"SOFTWARE\Microsoft\Office\15.0")
            || DetectOffice(get64BitRegKey(), @"SOFTWARE\Microsoft\Office\15.0"))
                Office2013 = true;

            if (DetectOffice(get32BitRegKey(), @"SOFTWARE\Microsoft\Office\14.0")
            || DetectOffice(get64BitRegKey(), @"SOFTWARE\Microsoft\Office\14.0"))
                Office2010 = true;
        }
        */
        public static bool DetectOffice(string version)
        {
            if (version == "2016") {
                if (DetectOffice(get32BitRegKey(), @"SOFTWARE\Microsoft\Office\16.0")
                || DetectOffice(get64BitRegKey(), @"SOFTWARE\Microsoft\Office\16.0"))
                    return true;
                else
                    return false;
            }

            if (version == "2013")
            {
                if (DetectOffice(get32BitRegKey(), @"SOFTWARE\Microsoft\Office\15.0")
                || DetectOffice(get64BitRegKey(), @"SOFTWARE\Microsoft\Office\15.0"))
                    return true;
                else
                    return false;
            }
            if (version == "2010") {
                if (DetectOffice(get32BitRegKey(), @"SOFTWARE\Microsoft\Office\14.0")
                || DetectOffice(get64BitRegKey(), @"SOFTWARE\Microsoft\Office\14.0"))
                    return true;
                else
                    return false;
            }
            return false;
        }


        //---------------------------------------------------------------------------------
        static bool DetectOffice(RegistryKey regkey, string baseKey)
        {
            bool ans = false;

            try
            {
                using (regkey = regkey.OpenSubKey(baseKey)) { 
                
                    if (regkey != null)
                        ans = true;

                    //regkey.Close();
                    //regkey.Flush();
                }
            }
            catch { }

            return ans;
        }

        //---------------------------------------------------------------------------------
        public static void InstallAddin()
        {
            RegistryKey regkey;

            int i = 0;
            foreach (string AddinName in AddinNames)
            {
                regkey = get32BitRegKey();
                InstallAddin(regkey, AddInsKey + OfficeNames[i] + @"\Addins\SpiderDocs." + AddinName, i);

                regkey = get64BitRegKey();
                InstallAddin(regkey, AddInsKey + OfficeNames[i] + @"\Addins\SpiderDocs." + AddinName, i);

                i++;
            }
        }

        //---------------------------------------------------------------------------------
        static void InstallAddin(RegistryKey regkey, string Key, int idx)
        {
            try
            {
                regkey = regkey.CreateSubKey(Key);

                regkey.SetValue("Description", Descriptions[idx], RegistryValueKind.String);
                regkey.SetValue("FriendlyName", Descriptions[idx], RegistryValueKind.String);
                regkey.SetValue("LoadBehavior", LoadBehavior, RegistryValueKind.DWord);
                regkey.SetValue("Manifest", AddInsPath.Replace("[ADD_IN_NAME]", AddinNames[idx]), RegistryValueKind.String);

                regkey.Close();
            }
            catch { }
        }

        //---------------------------------------------------------------------------------
        public static void ResetResiliency(string Version)
        {
            RegistryKey regkey;
            string[] names = OfficeNames.Distinct().ToArray();

            foreach (string name in names)
            {
                string Key = AddInsKey + Version + "\\" + name;

                regkey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Default);
                regkey.DeleteSubKeyTree(Key + "\\Resiliency", false);

                regkey.Close();
            }
        }

        //---------------------------------------------------------------------------------

        /// <summary>
        /// Add force enable addin
        /// </summary>
        /// <param name="Version"></param>
        /// <param name="delete"></param>
        public static void AddOrDeleteDoNotDisableAddinList(string Version, bool delete)
        {
            logger.Debug("64 Bit: {0}", Environment.Is64BitOperatingSystem);

            // Add registry key for the reason why 
            RegistryKey regkey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Default);

            RegistryKey OfficeRoot = regkey.OpenSubKey(AddInsKey + Version, true);
            if (OfficeRoot == null)
                OfficeRoot = regkey.CreateSubKey(AddInsKey + Version, RegistryKeyPermissionCheck.ReadWriteSubTree);


            if (OfficeRoot != null)
            {
                for (int i = 0; i < AddinNames.Length; i++)
                {
                    RegistryKey OfficeName = OfficeRoot.OpenSubKey(OfficeNames[i], true);

                    if (OfficeName == null)
                        OfficeName = OfficeRoot.CreateSubKey(OfficeNames[i], RegistryKeyPermissionCheck.ReadWriteSubTree);

                    RegistryKey Resiliency = OfficeName.OpenSubKey("Resiliency", true);

                    if (Resiliency == null)
                        Resiliency = OfficeName.CreateSubKey("Resiliency", RegistryKeyPermissionCheck.ReadWriteSubTree);

                    RegistryKey DoNotDisableAddinList = Resiliency.OpenSubKey("DoNotDisableAddinList", true);

                    if (!delete)
                    {
                        if (DoNotDisableAddinList == null)
                            DoNotDisableAddinList = Resiliency.CreateSubKey("DoNotDisableAddinList", RegistryKeyPermissionCheck.ReadWriteSubTree);

                        DoNotDisableAddinList.SetValue("SpiderDocs." + AddinNames[i], 1, RegistryValueKind.DWord);

                    }
                    else
                    {
                        if (DoNotDisableAddinList != null)
                            DoNotDisableAddinList.DeleteValue("SpiderDocs." + AddinNames[i], false);
                    }

                    if (DoNotDisableAddinList != null)
                        DoNotDisableAddinList.Close();

                    Resiliency.Close();
                    OfficeName.Close();
                }

                OfficeRoot.Close();
            }
            
            //
            // Add registory key to always enable addin
            //
            RegistryKey PolicyRoot = regkey.OpenSubKey(GroupPolicyKey + Version, true);
            if (PolicyRoot == null)
                PolicyRoot = regkey.CreateSubKey(GroupPolicyKey + Version, RegistryKeyPermissionCheck.ReadWriteSubTree);

            for (int i = 0; i < AddinNames.Length; i++)
            {
                RegistryKey OfficeName = PolicyRoot.OpenSubKey(OfficeNames[i], true);

                if (OfficeName == null)
                    OfficeName = PolicyRoot.CreateSubKey(OfficeNames[i], RegistryKeyPermissionCheck.ReadWriteSubTree);

                RegistryKey Resiliency = OfficeName.OpenSubKey("Resiliency", true);

                if (Resiliency == null)
                    Resiliency = OfficeName.CreateSubKey("Resiliency", RegistryKeyPermissionCheck.ReadWriteSubTree);


                RegistryKey AddinList = Resiliency.OpenSubKey("AddinList", true);

                if (!delete)
                {
                    if (AddinList == null)
                        AddinList = Resiliency.CreateSubKey("AddinList", RegistryKeyPermissionCheck.ReadWriteSubTree);

                    AddinList.SetValue("SpiderDocs." + AddinNames[i], '1', RegistryValueKind.String);

                }
                else
                {
                    if (AddinList != null)
                        AddinList.DeleteValue("SpiderDocs." + AddinNames[i], false);
                }

                if (AddinList != null)
                    AddinList.Close();

                Resiliency.Close();
                OfficeName.Close();
            }

            PolicyRoot.Close();


            regkey.Close();
        }

        //---------------------------------------------------------------------------------
        public static void DeleteAddin()
        {
            RegistryKey regkey;

            try
            {
                int i = 0;
                foreach (string AddinName in AddinNames)
                {
                    regkey = get32BitRegKey();
                    regkey.DeleteSubKey(AddInsKey + OfficeNames[i] + @"\Addins\SpiderDocs." + AddinName, false);
                    regkey.Close();
                    regkey.Flush();
                    i++;
                }
            }
            catch { }

            try
            {
                int i = 0;
                foreach (string AddinName in AddinNames)
                {
                    regkey = get64BitRegKey();
                    regkey.DeleteSubKey(AddInsKey + OfficeNames[i] + @"\Addins\SpiderDocs." + AddinName, false);
                    regkey.Close();
                    regkey.Flush();
                    i++;
                }
            }
            catch { }

            if (Directory.Exists(add_ins_folder))
            {
                ClearAttributes(add_ins_folder);
                Directory.Delete(add_ins_folder, true);
            }

            mkdir(add_ins_folder);
        }


        public static void GrantFull2Registory(RegistryKey regkey, string key)
        {

            // CHANGED to open the key as writable: using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(key))
            using (RegistryKey rk = regkey.OpenSubKey(key, RegistryKeyPermissionCheck.ReadWriteSubTree))
            {

                // CHANGED to add to existing security: RegistrySecurity rs = new RegistrySecurity();
                RegistrySecurity rs = rk.GetAccessControl();

                // Creating registry access rule for 'Everyone' NT account
                RegistryAccessRule rar = new RegistryAccessRule(
                    "Everyone",
                    RegistryRights.FullControl,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow);

                rs.AddAccessRule(rar);
                rk.SetAccessControl(rs);
            }
        }


        public static string[] GetAllUsers()
        {
            
            using(RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList"))
            {
                return key.GetSubKeyNames();
            }
            
        }
        static void mkdir(string dir)
        {
            //logger.Append(string.Format("{0} is exist: {1}", dir,Directory.Exists(dir)));
            logger.Debug("{0} is exist: {1}", dir, Directory.Exists(dir));
            if (!Directory.Exists(dir))
            {
                string DeleteServiceArg = "/C mkdir \"" + dir + "\"";

                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = DeleteServiceArg;

                // To get administrator right
                startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            }
        }
        //---------------------------------------------------------------------------------
        static RegistryKey get32BitRegKey()
        {
            return RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
        }

        //---------------------------------------------------------------------------------
        static RegistryKey get64BitRegKey()
        {
            return RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
        }

        //---------------------------------------------------------------------------------
        static void ClearAttributes(string currentDir)
        {
            string[] subDirs = Directory.GetDirectories(currentDir);
            foreach (string dir in subDirs)
                ClearAttributes(dir);
            string[] files = files = Directory.GetFiles(currentDir);
            foreach (string file in files)
                System.IO.File.SetAttributes(file, FileAttributes.Normal);
        }

        //---------------------------------------------------------------------------------
        public static string ProgramFilesx86()
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

        //---------------------------------------------------------------------------------
        public static void CreateShortcut(string save_path, string target, string args = "", string description = "", bool isadmin = false)
        {
            WshShell shell = new WshShell();

            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(save_path);
            shortcut.TargetPath = target;
            shortcut.WorkingDirectory = target.Replace(Path.GetFileName(target), "");
            shortcut.Arguments = args;
            shortcut.Description = description;
            shortcut.Save();

            if (isadmin)
            {
                /*
                using (FileStream fs = new FileStream(save_path, FileMode.Open, FileAccess.ReadWrite))
                {
                    fs.Seek(21, SeekOrigin.Begin);
                    fs.WriteByte(0x22);
                }
                */
            }
        }

        //---------------------------------------------------------------------------------
        static bool OfficeCheck()
        {
            bool ans = true;
            bool loop = true;

            while (loop)
            {
                ans = true;
                loop = false;

                foreach (var process in Process.GetProcesses())
                {
                    if ((process.ProcessName.ToLower() == "winword")
                    || (process.ProcessName.ToLower() == "excel")
                    || (process.ProcessName.ToLower() == "powerpnt")
                    || (process.ProcessName.ToLower() == "outlook"))
                    {
                        ans = false;

                        // if this is running from a service (SessionId == 0), it is impossible to show dialog.
                        if (0 < System.Diagnostics.Process.GetCurrentProcess().SessionId)
                        {
                            DialogResult ret = MessageBox.Show(
                                "Please close all Office application before to continue.",
                                "Spider Docs Update",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);

                            loop = true;
                        }

                        break;
                    }
                }
            }

            return ans;
        }
    }
}
