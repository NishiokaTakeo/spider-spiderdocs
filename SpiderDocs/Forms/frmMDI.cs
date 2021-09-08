using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SpiderDocsForms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocs
{
	public partial class frmMDI : Spider.Forms.FormBase
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		public bool minimized = false;

//---------------------------------------------------------------------------------
		public frmMDI()
		{
			InitializeComponent();

			tsStatus.Text = "";

			if(!SpiderDocsApplication.CurrentServerSettings.localDb)
				Menu_Sync.Visible = false;

			TextExtraction.ProgressUpdate = SyncStatusUpdate;
			TextExtraction.PrograssFinished = SyncStatusFinished;
		}

//---------------------------------------------------------------------------------
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			IntPtr mainWindowHandle = this.Handle;

			try
			{
				lock(this)
				{
					//Write the handle to the Shared Memory
					MMF.WriteData<int>((int)mainWindowHandle, MMF_Items.WindowHandle);
				}
			}
			catch(Exception error)
			{
				logger.Error(error);
			}
		}

//---------------------------------------------------------------------------------
		private void frmMDI_Load(object sender, EventArgs e)
		{
			frmWorkSpace frmWrkSpc = null;

			try
			{
				//system version
				tsVersion.Text += Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 5);

				//user information
				tsUser.Text = "User:  " + SpiderDocsApplication.CurrentUserName + "  ";

				if(SpiderDocsApplication.CurrentUserSettings.offline)
				{
					frmWrkSpc = new frmWorkSpace();
					frmWrkSpc.workOffline = true;

					//user information
					tsGroup.Text = "Group: --- ";
				}
                else
				{
					frmWrkSpc = new frmWorkSpace();

					if(minimized)
						WindowState = FormWindowState.Minimized;

					User user = UserController.GetUser(true, SpiderDocsApplication.CurrentUserId);
					string GroupName = PermissionController.GetMenuPermissionGroupName(user.id_permission);
					tsGroup.Text = "Group:  " + GroupName + "  ";

                    tsServer.Text = "Server:  " +  SpiderDocsApplication.CurrentServerSettings.server;

                    //load menus by user logged
                    enablingMenus();
				}
			}
			catch(Exception error)
			{
				MessageBox.Show("There was an unexpected error loading the form.\n\n" + error.Message, "Closing Application...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				logger.Error(error);
				Close();
			}

			//open work space form
			OpenForm(frmWrkSpc);
		}

		private void enablingMenus()
		{
            // TODO: Improve performance
			try
			{
				int IdPermission = UserController.GetUser(false, SpiderDocsApplication.CurrentUserId).id_permission;
				List<int> MainIds = PermissionController.GetMenuPermission(en_MenuPermissionMode.Main, IdPermission);
				List<int> SubIds = PermissionController.GetMenuPermission(en_MenuPermissionMode.Sub, IdPermission);


                List<string> arrayMenus = PermissionController.GetPermittedMenuTitles(en_MenuPermissionMode.Main, MainIds.ToArray());
				List<string> arraySubMenus = SubIds.Count > 0 ? PermissionController.GetPermittedMenuTitles(en_MenuPermissionMode.Sub, SubIds.ToArray()): new List<string>();

				foreach(ToolStripMenuItem mainMenu in menuStrip.Items)
				{
					if(arrayMenus.IndexOf(mainMenu.Name) > -1)
						mainMenu.Enabled = true;

					foreach(ToolStripItem subMenu in mainMenu.DropDownItems)
					{
						if(arraySubMenus.IndexOf(subMenu.Name) != -1)
							subMenu.Enabled = true;
					}
				}

				// global level on/off
				var featureReportBuilder = SpiderDocsApplication.CurrentPublicSettings.feature_reportbuilder;
				SubMenu_ReportBuilder.Enabled = featureReportBuilder;

			}
			catch(Exception error)
			{
				logger.Error(error);
			}
		}

        //---------------------------------------------------------------------------------
        //delegate void delegateThread();
        async private void frmMDI_FormClosing(object sender, FormClosingEventArgs e)
		{
            try
            {
				SpiderDocsApplication.SaveAllSettings();

				//reg log
				if(SpiderDocsApplication.CurrentUserSettings.offline == false)
					UserController.registerLog(en_UserEvents.Logout, "");


                foreach(string dir in System.IO.Directory.GetDirectories(FileFolder.SendToOpeningPath))
                    System.IO.Directory.Delete(dir, true);

                ////Invoke(new delegateThread(Close));
                //foreach (var a in Application.OpenForms)
                //{
                //    if (a.GetType() == typeof(frmSplashScreen))
                //    {
                //        (a as frmSplashScreen).Close();
                //        Application.Run(new frmSplashScreen());
                //        return;
                //    }
                //}


                //TODO: check if local work space files are open or not and show alert dialog. it says 'To sync all of workspace file, you must save&exits. You are opening following files. Please close it.'
                System.Threading.CancellationTokenSource tokenSource = new System.Threading.CancellationTokenSource();
                //System.Threading.CancellationToken token = tokenSource.Token;

                var frmBusyTask = System.Threading.Tasks.Task.Run(() => Application.Run(new frmBusy("")), tokenSource.Token);

                // var removeTask=  System.Threading.Tasks.Task.Run(() => WorkSpaceSyncController.RemoveKnowissued());

                Cache.RemoveUsersCache(Cache.en_UKeys.DB_SyncMgr);

                await new Cache(SpiderDocsApplication.CurrentUserId).GetSyncMgr(FileFolder.GetUserFolder()).Sync();

                //await removeTask;

				FileFolder.DeleteHiddenFilesInWorkSpace();

                MMF.WriteData<int>(0, MMF_Items.UserId);

                tokenSource.Cancel();

                Application.ExitThread();
                //Application.Exit();
            }
			catch(Exception error)
			{
				logger.Error(error);
			}
		}

//---------------------------------------------------------------------------------
		private void ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem Sender = (ToolStripMenuItem)sender;
			Form frm = null;

			if(Sender == this.SubMenu_Groups)
				frm = new frmGroup();
			// else if(Sender == this.SubMenu_Folders)
			// 	frm = new frmFolder();
			else if(Sender == this.SubMenu_GroupsOfUsers)
				frm = new frmGroupUser();
			else if(Sender == this.SubMenu_Scan)
				frm = new frmScan();
			else if(Sender == this.SubMenu_Import)
				frm = new frmImportFiles();
			else if(Sender == this.SubMenu_DeletedDocs)
				frm = new frmDeletedFiles();
			else if(Sender == this.SubMenu_MenuAcess)
				frm = new frmControlPermission();
			else if(Sender == this.SubMenu_UserLog)
				frm = new frmReportUserLog();
			else if(Sender == this.SubMenu_DocumentTypes)
				frm = new frmDocumentType();
			else if(Sender == this.SubMenu_Permissions_Folders)
				frm = new frmFolderPermissions();
			else if(Sender == this.SubMenu_WorkSpace)
				frm = new frmWorkSpace();
			else if(Sender == this.SubMenu_Preferences)
				 frm = new frmPreferences();
			else if(Sender == this.SubMenu_DocumentAttributes)
				frm = new frmAttribute();
			else if(Sender == this.SubMenu_ChangePassword)
				frm = new frmChangePassword();
			else if(Sender == this.SubMenu_FooterSettings)
				frm = new frmFooterSettings();
			else if(Sender == this.SubMenu_Users)
				frm = new frmUser();
			else if(Sender == this.SubMenu_About)
				frm = new frmAbout();
            else if (Sender == this.SubMenu_Favourite)
                frm = new frmFavourite();
            else if (Sender == this.SubMenu_NotificationGroup)
                frm = new frmNotificationGroup();
            else if (Sender == this.SubMenu_ReportBuilder)
                frm = new frmReportBuilder();

            OpenForm(frm);
		}

		void OpenForm(Form frm)
		{
			try
			{
				bool found = false;

				foreach(Form OpenForm in Application.OpenForms)
				{
					if(OpenForm.GetType() == frm.GetType())
					{
						OpenForm.Activate();
						OpenForm.Focus();

						frm.Close();
						frm.Dispose();

						found = true;
						break;
					}
				}

				if(!found)
				{
					if(frm.FormBorderStyle != System.Windows.Forms.FormBorderStyle.FixedDialog)
						frm.MdiParent = this;

					frm.Icon = this.Icon;
					frm.Show();

					if(frm.FormBorderStyle != System.Windows.Forms.FormBorderStyle.FixedDialog)
					{
						frm.WindowState = FormWindowState.Minimized;
						frm.WindowState = FormWindowState.Maximized;
					}
				}
			}
			catch(Exception error)
			{
				MessageBox.Show("There was an error loading the form.", "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
				logger.Error(error);
				this.Close();
			}
		}

//---------------------------------------------------------------------------------
		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
		}

//---------------------------------------------------------------------------------
		[DllImport("User32.dll")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);
		private void SubMenu_Logout_Click(object sender, EventArgs e)
		{
			SpiderDocsApplication.SaveAllSettings();

			//reg log
			if (SpiderDocsApplication.CurrentUserSettings.offline == false)
				UserController.registerLog(en_UserEvents.Logout, "");

			ProcessStartInfo Info = new ProcessStartInfo();
			Info.Arguments = "restart logout";
			Info.WindowStyle = ProcessWindowStyle.Hidden;
			Info.CreateNoWindow = true;
			Info.FileName = Application.ExecutablePath;
			Process process = Process.Start(Info);
			SetForegroundWindow(process.MainWindowHandle);

			Close();
		}

//---------------------------------------------------------------------------------
		private void SubMenu_Exit_Click(object sender, EventArgs e)
		{
			//reg log
			if(SpiderDocsApplication.CurrentUserSettings.offline == false)
				UserController.registerLog(en_UserEvents.Logout, "");

			Close();
		}

//---------------------------------------------------------------------------------
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

//---------------------------------------------------------------------------------
		private void Menu_Sync_Click(object sender,EventArgs e)
		{
			TextExtraction.UpdateAllDocuments();
		}

		void SyncStatusUpdate(object args)
		{
			int[] vals = (int[])args;
			tsStatus.Text = "Status: Updating search index... " + vals[0] + " of " + vals[1];
		}

		void SyncStatusFinished(object args)
		{
			bool cancel = (bool)args;

			if(cancel)
				tsStatus.Text = "Status: Search index update is canceled.";
			else
				tsStatus.Text = "Status: Search index update is finished.";
		}

//---------------------------------------------------------------------------------
		private void SubMenu_Help_Click(object sender,EventArgs e)
		{
			Help.ShowHelp(this, "file://" + Application.StartupPath + "\\SpiderDocs.chm");
		}



        //---------------------------------------------------------------------------------
    }
}
