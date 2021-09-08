using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using lib = SpiderDocsModule.Library;
using SpiderDocsForms;
using SpiderDocsModule;
//using SpiderDocs.Forms.Opening;
using System.ServiceProcess;
using NLog;
using System.Linq;

//---------------------------------------------------------------------------------
namespace SpiderDocs
{
	public partial class frmSplashScreen : Spider.Forms.FormBase
	{
		static Logger logger = LogManager.GetCurrentClassLogger();

//---------------------------------------------------------------------------------
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool SetForegroundWindow(IntPtr hWnd);

		bool m_minimized;
		bool m_logout = false;

		double m_dblOpacityIncrement = .05;
		const int TIMER_INTERVAL = 20;

		delegate void delegateThread();

		en_Stage m_stage = en_Stage.Start;
		enum en_Stage
		{
			Start = 0,
			LoadVals,
			WaitServer,
			ConnectServer,
			ConnectDb,
			LoginDb,
			OpenSystem,

			Max
		}

//---------------------------------------------------------------------------------
		public frmSplashScreen(
						bool minimized = false,
						bool logout = false)
		{
            logger.Trace("Begin");

            InitializeComponent();

			// Reset server setting.
			SpiderDocsApplication.CurrentServerSettings.LoadFromRegistory();
            SpiderDocsApplication.CurrentUserSettings = SpiderDocsModule.Factory.Instance4UserSettins();// new UserSettings();
			SpiderDocsApplication.CurrentUserSettings.Load();

			//set panel positions
			panelLogin.Location = new System.Drawing.Point(5, 164);
			panelError.Location = new System.Drawing.Point(30, 164);

			m_logout = logout;
			m_minimized = minimized;
		}

		public void UpdateServerSetting(ServerSettings OverrideServerSettings = null)
		{
			SpiderDocsApplication.CurrentServerSettings = OverrideServerSettings;
		}
		public void UpdateUserSettings(UserSettings OverrideUserSettings = null)
		{
			SpiderDocsApplication.CurrentUserSettings = OverrideUserSettings;
		}

//---------------------------------------------------------------------------------
		void frmSplashScreen_Load(object sender, EventArgs e)
		{
            logger.Trace("Begin");

            //set screen opacity
            Opacity = .0;
			timerOpacity.Interval = TIMER_INTERVAL;
			timerOpacity.Start();

			lblVersion.Text = "Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 5);

			// Update HKEY_CURRENT_USER's registory
			// Commented out as spider docs needs to have administrative privilege to perform this.
			// try
			// {
			// 	setup4application();
			// }
			// catch(Exception ex)
			// {
			// 	logger.Error(ex);
			// }
		}

		void setup4application()
		{
            logger.Trace("Begin");

            new ApplicationSettings().MigrateIfDefault();

			//Create send to
			SpiderDocsModule.Setup.createSendTo(System.Environment.GetFolderPath(Environment.SpecialFolder.SendTo));

			bool Office2013 = false, Office2010 = false,  Office2016 = false;

			Office2016 = SpiderDocsModule.Setup.DetectOffice("2016");
			Office2013 = SpiderDocsModule.Setup.DetectOffice("2013");
			Office2010 = SpiderDocsModule.Setup.DetectOffice("2010");

			if (Office2013 || Office2010 || Office2016)
			{
				//
				// Enable always addin
				//

				//SpiderDocsModule.Setup.InstallAddin();

				if (Office2016)
				{
					SpiderDocsModule.Setup.ResetResiliency("16.0");
					SpiderDocsModule.Setup.AddOrDeleteDoNotDisableAddinList("16.0", false);
				}

				if (Office2013)
				{
					SpiderDocsModule.Setup.ResetResiliency("15.0");
					SpiderDocsModule.Setup.AddOrDeleteDoNotDisableAddinList("15.0", false);
				}

				if (Office2010)
				{
					SpiderDocsModule.Setup.ResetResiliency("14.0");
					SpiderDocsModule.Setup.AddOrDeleteDoNotDisableAddinList("14.0", false);
				}

			}
			else
			{
				// If Office is not installed, delete add-ins.
				//
				// Remove addin
				//

				//SpiderDocsModule.Setup.DeleteAddin();
				SpiderDocsModule.Setup.AddOrDeleteDoNotDisableAddinList("14.0", true);
				SpiderDocsModule.Setup.AddOrDeleteDoNotDisableAddinList("15.0", true);
				SpiderDocsModule.Setup.AddOrDeleteDoNotDisableAddinList("16.0", true);
			}
		}

//---------------------------------------------------------------------------------
		void timerOpacity_Tick(object sender, EventArgs e)
		{
            logger.Trace("Begin");

            if (this.Opacity < 1)
			{
				this.Opacity += m_dblOpacityIncrement;

			}else
			{
				timerOpacity.Stop();

				//get local variables
				m_stage = en_Stage.LoadVals;
				UpdatePanel();

				txtUser.Text = SpiderDocsApplication.CurrentUserName;
				ckSavepass.Checked = SpiderDocsApplication.CurrentUserSettings.save_pass;

				if(SpiderDocsApplication.CurrentUserSettings.offline || !SpiderDocsApplication.CurrentUserSettings.autoConnect)
				{
					m_stage = en_Stage.WaitServer;
					UpdatePanel();

				}else
				{
					Task task = Task.Factory.StartNew(() => bw_DoWork());
				}
			}
		}

//---------------------------------------------------------------------------------
		void bw_DoWork()
		{
            logger.Trace("Begin");

            bool IsShowServerSetting = true;

			// check auto connection or credential is unknown
			if (SpiderDocsApplication.CurrentServerSettings.localDb)
			{
				IsShowServerSetting = false;

			}else if(!String.IsNullOrEmpty(SpiderDocsApplication.CurrentServerSettings.server)
				  && (0 < SpiderDocsApplication.CurrentServerSettings.port))
			{
				IsShowServerSetting = false;
			}

			if(IsShowServerSetting)
			{
				m_stage = en_Stage.WaitServer;

				//waiting for server details
				Invoke(new delegateThread(UpdatePanel));

			}else
			{
				m_stage = en_Stage.ConnectServer;

				//trying to connect to the server
				Invoke(new delegateThread(UpdatePanel));

				if(SpiderDocsApplication.CurrentServerSettings.localDb || !SpiderDocsApplication.CurrentUserSettings.offline)
				{
					SpiderDocsServer server = new SpiderDocsServer(SpiderDocsApplication.CurrentServerSettings);
					server.onConnected += onConnectedServer;
					server.onConnectionErr += onConnectionServerErr;
					server.Connect();

				}else
				{
					openSystem(true);
				}
			}
		}

//---------------------------------------------------------------------------------
		void pbChangeServer_Click(object sender, EventArgs e)
		{
            logger.Trace("Begin");

            m_stage = en_Stage.WaitServer;

			//waiting for server details
			Invoke(new delegateThread(UpdatePanel));
		}

//---------------------------------------------------------------------------------
		void frmSplashScreen_KeyDown(object sender, KeyEventArgs e)
		{
            logger.Trace("Begin");

            if (e.KeyCode == Keys.Enter)
			{
				if(txtPassword.Visible == true)
				{
					UserSettings UserSettings = LoginCredentialCheck();

					if(UserSettings != null)
						login(UserSettings);
				}
			}
		}

//---------------------------------------------------------------------------------
		void onConnectedServer(ServerSettings ServerSettings, bool ConnectionChk)
		{
			if(logger.IsTraceEnabled) logger.Trace("[Begin] ServerSettings:{0}", Newtonsoft.Json.JsonConvert.SerializeObject(ServerSettings));

            ServerSettings.Save();

            SpiderDocsApplication.CurrentServerSettings = ServerSettings;
			//SpiderDocsApplication.CurrentServerSettings.Save();

			m_stage = en_Stage.ConnectDb;
			SpiderDocsApplication.CurrentUserSettings.offline = false;

			SharedMMF.WriteData<string>(SpiderDocsApplication.CurrentServerSettings.server, SharedMMF_Items.ServerAddress);
			SharedMMF.WriteData<int>(SpiderDocsApplication.CurrentServerSettings.port, SharedMMF_Items.ServerPort);

			if(ConnectionChk)
			{
				SpiderDocsApplication.CurrentPublicSettings = new PublicSettings();
				SpiderDocsApplication.CurrentPublicSettings.Load();

				SpiderDocsApplication.CurrentMailSettings = new MailSettingss();
				SpiderDocsApplication.CurrentMailSettings.Load();

				// Override server setting.
				// Use always intranet server setting if multiaddress features is enabled then .
				OverrideSetting(SpiderDocsApplication.CurrentPublicSettings);

				// Save db side value.
				ApplicationSettings settingApp = new ApplicationSettings();
				settingApp.PreviousMultiAddress = SpiderDocsApplication.CurrentPublicSettings.feature_multiaddress;
				settingApp.SaveAsJson();

				if (VersionCheck())
				{
					//Add important registry.It is registred when SpiderDoc.exe is installed. but somehow it does not. so I put here.
					//string[] version = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion.Split('.');
					//SpiderDocsForms.SpiderDocsApplication.AddInstallationInfo(version[0] + "." + version[1] + "." + version[2]);

					if(!SpiderDocsApplication.CurrentUserSettings.autoLogin
					|| String.IsNullOrEmpty(SpiderDocsApplication.CurrentUserName)
					|| String.IsNullOrEmpty(SpiderDocsApplication.CurrentUserSettings.pass)
					|| m_logout)
					{
						//show login.
						Invoke(new delegateThread(showLoginCredentials));

					}else
					{
						login(SpiderDocsApplication.CurrentUserSettings);
					}
				}

			}else
			{
				Invoke(new delegateThread(ShowError));
			}
		}

//---------------------------------------------------------------------------------
		bool VersionCheck()
		{
            logger.Trace("Begin");
            bool ans = true;
			CurrentVersionState CurrentVersionState = SpiderDocsApplication.CheckCurrentVersionState();

			if(CurrentVersionState == CurrentVersionState.Newer)
			{
				MessageBox.Show(lib.msg_error_newer_version, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				ans = false;

			}else if(CurrentVersionState == CurrentVersionState.Older)
			{
				if(SpiderDocsApplication.CurrentPublicSettings.auto_update)
				{
					if(MessageBox.Show(lib.msg_update, lib.msg_messabox_title, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
					{
						//check service, then start service if it is stopping

						Utilities.LoopUntilOfficeClosed();

						Start2Update();

						/*
						// Run UpdateWaitDialog.exe and close current process. The Spider Docs Auto Update service should do update automatically.
						Process process = new Process();
						ProcessStartInfo startInfo = new ProcessStartInfo();
						startInfo.FileName = FileFolder.GetExecutePath() + "UpdateWaitDialog.exe";
						startInfo.Arguments = "start_wait " + SpiderDocsCoer.getSystemVersion() + " \"" + FileFolder.GetExecuteFileName() + "\"";
						process.StartInfo = startInfo;
						process.Start();
						*/
					}

				}else
				{
					MessageBox.Show(lib.msg_auto_update_disabled, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				Invoke(new delegateThread(Close));
				ans = false;
			}

			return ans;
		}

		void Start2Update()
		{
            logger.Trace("Begin");

            string workingdir = Path.GetTempPath() + "spiderdocs-update\\";
			string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			FileFolder.CreateFolder(workingdir, false);

			string[] macthes = { "*.dll", "*.config", "*.xml", "UpdateWaitDialog.exe" };
			foreach (string pattern in macthes)
			{
				foreach (string target in Directory.GetFiles(path, pattern))
				{
					try
					{
						File.Copy(target, workingdir + Path.GetFileName(target), true);
						logger.Debug("Copied to working directory : {0}", target);
					}
					catch { }
				}
			}

			Process process = new Process();
			ProcessStartInfo startInfo = new ProcessStartInfo();

			startInfo.FileName = workingdir + "UpdateWaitDialog.exe";
			startInfo.Arguments = string.Format("wait {0} \"{1}\"", Cache.getSystemVersion(), FileFolder.GetExecuteFileName());
			startInfo.WorkingDirectory = workingdir;
			process.StartInfo = startInfo;
			process.Start();

		}

//---------------------------------------------------------------------------------
		void onConnectionServerErr()
		{
            logger.Trace("Begin");

            MessageBox.Show(lib.msg_error_conection_lost, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

			//Could not establish connection to SpiderDocs server.
			Invoke(new delegateThread(ShowError));
		}

//---------------------------------------------------------------------------------
		void btnLogin_Click(object sender, EventArgs e)
		{
            logger.Trace("Begin");

			Cache.RemoveAll();

            UserSettings UserSettings = LoginCredentialCheck();

			if(UserSettings != null)
				login(UserSettings);
		}

//---------------------------------------------------------------------------------
		UserSettings LoginCredentialCheck()
		{
            logger.Trace("Begin");

            UserSettings ans = null;

			if((txtUser.Text == "") || (txtPassword.Text == ""))
			{
				MessageBox.Show(lib.msg_required_credential, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

			}else
			{
				UserSettings UserSettings = Utilities.ObjectClone(SpiderDocsApplication.CurrentUserSettings);
				UserSettings.save_pass = ckSavepass.Checked;
				UserSettings.userName = txtUser.Text;
				UserSettings.pass = new Crypt().Encrypt(txtPassword.Text);

				ans = UserSettings;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		void login(UserSettings UserSettings)
		{
            logger.Trace("Begin");

            m_stage = en_Stage.LoginDb;
			Invoke(new delegateThread(UpdatePanel));

			try
			{
				//int userId = UserController.Login(UserSettings.userName, UserSettings.pass);
				int userId = UserController.LoginActive(UserSettings.userName, UserSettings.pass);

				//checking credentials
				if(userId != 0)
				{
					SpiderDocsApplication.CurrentUserSettings = UserSettings;
					SpiderDocsApplication.CurrentUserSettings.Save();

					MMF.WriteData<int>(userId, MMF_Items.UserId);
					SpiderDocsApplication.CurrentUserGlobalSettings = new UserGlobalSettings(userId);
					SpiderDocsApplication.CurrentUserGlobalSettings.Load();
					SpiderDocsApplication.WorkspaceGridsizeSettings = new cl_WorkspaceGridsize(userId);
					SpiderDocsApplication.WorkspaceGridsizeSettings.Load();
					SpiderDocsApplication.WorkspaceCustomize = new cl_WorkspaceCustomize(userId);
					SpiderDocsApplication.WorkspaceCustomize.Load();

					m_stage = en_Stage.OpenSystem;
					Invoke(new delegateThread(UpdatePanel));

					// Update last computer name
					User user = UserController.GetUser(true, userId);
					user.name_computer = Environment.MachineName;
					UserController.UpdatetUser(user);

					openSystem(false);

				}else
				{
					UserController.registerLog(en_UserEvents.LoginFaile, "");

					//login or password incorrect and show login.
					Invoke(new delegateThread(ShowError));
				}
			}
			catch(Exception error)
			{
				logger.Error(error);
				Invoke(new delegateThread(ShowError));
			}
		}

//---------------------------------------------------------------------------------
		void openSystem(bool workOffline)
		{
            logger.Trace("Begin");

            bool ans = true;

			if(!workOffline)
			{
				//create user folder and temp folder
				ans = (FileFolder.CreateTempFolder() && FileFolder.CreateUserFolder());

				if(ans)
				{
					//save event log
					try { UserController.registerLog(en_UserEvents.Login, ""); }
					catch { }

				}else
				{
					MessageBox.Show("Spider Docs could not create folder for the current user. \n " +
									"Please check if you have permission to write to the following folder: \n" +
									FileFolder.GetUserFolder() + "\n" + FileFolder.TempPath + "\n",
									"Permission to create folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}

			if(ans)
			{
				//hide this form
				Invoke(new delegateThread(Hide));
				Invoke(new delegateThread(OpenMDI));

			}else
			{
				Invoke(new delegateThread(Close));
			}
		}

//---------------------------------------------------------------------------------
		void OpenMDI()
		{
            logger.Trace("Begin");

            //open main form
            frmMDI MDI = new frmMDI();
			MDI.minimized = m_minimized;
			MDI.Show();
		}

//---------------------------------------------------------------------------------
		void btnClose_Click(object sender, EventArgs e)
		{
            Close();
		}

//---------------------------------------------------------------------------------
		protected override void OnPaintBackground(PaintEventArgs e)
		{
            logger.Trace("Begin");

            Graphics gfx = e.Graphics;
			gfx.DrawImage(Properties.Resources.loginScreen, new Rectangle(0, 0, this.Width, this.Height));
		}

//---------------------------------------------------------------------------------
// Update UI as per sateges -------------------------------------------------------
//---------------------------------------------------------------------------------
		void UpdatePanel()
		{
            logger.Trace("Begin");

            switch (m_stage)
			{
			case en_Stage.LoadVals:
				lblAction.Text = "Loading variables...";
				pbar.Visible = true;
				pbar.Value = 15;
				break;

			case en_Stage.WaitServer:
				panelLogin.Visible  = false;
				panelError.Visible = false;

				lblAction.Text = "Waiting for server details...";
				pbar.Visible = true;
				pbar.Value = 20;

				frmModeSetting ModeSetting = new frmModeSetting();
				ModeSetting.ShowDialog();

				if(ModeSetting.DialogResult == DialogResult.OK)
					Task.Factory.StartNew(() => bw_DoWork());

				break;

			case en_Stage.ConnectServer:
				lblAction.Text = "Connecting to the server...";
				pbar.Value = 30;
				System.Threading.Thread.Sleep(100);
				break;

			case en_Stage.ConnectDb:
				lblAction.Text = "Connecting to the database...";
				pbar.Value = 50;
				System.Threading.Thread.Sleep(100);
				break;

			case en_Stage.LoginDb:
				lblAction.Text = "Checking credentials...";
				pbar.Value = 75;
				System.Threading.Thread.Sleep(100);
				break;

			case en_Stage.OpenSystem:
				lblAction.Text = "Authorized.";
				lblAction.Refresh();
				pbar.Value = 100;
				pbar.Refresh();
				System.Threading.Thread.Sleep(200);
				break;
			}

			lblAction.Refresh();
			pbar.Refresh();
		}

//---------------------------------------------------------------------------------
		void ShowError()
		{
            logger.Trace("Begin");

            switch (m_stage)
			{
			default:
			case en_Stage.LoadVals:
			case en_Stage.WaitServer:
			case en_Stage.ConnectServer:
				panelError.Visible  = true;
				label3.Text = "Could not establish connection to SpiderDocs server.";
				break;

			case en_Stage.ConnectDb:
				lblAction.Text = "";
				lblAction.Refresh();

				pbar.Visible = false;
				pbar.Refresh();

				panelLogin.Visible  = false;
				panelError.Visible  = true;
				break;

			case en_Stage.LoginDb:
			case en_Stage.OpenSystem:
				lblLogin.Visible = true;
				lblLogin.Text = "User name or password incorrect.";
				lblLogin.Refresh();

				showLoginCredentials();
				break;
			}
		}

//---------------------------------------------------------------------------------
		void showLoginCredentials()
		{
            logger.Trace("Begin");

            panelLogin.Visible = true;
			panelLogin.Refresh();
			panelError.Visible = false;
			txtUser.Focus();

			SetForegroundWindow(this.Handle);
		}

		/// <summary>
		/// Override setting. SHould use here if you write feature base enable/disable logic
		/// </summary>
		/// <param name="publicSetting"></param>
		static void OverrideSetting(PublicSettings publicSetting)
		{
			ApplicationSettings setting = new ApplicationSettings();

			var enabledMultiAddress = setting.PreviousMultiAddress;
			if (false == enabledMultiAddress)
			{
				SpiderDocsApplication.CurrentServerSettings.server = setting.UpdateServer;
				SpiderDocsApplication.CurrentServerSettings.port = setting.UpdateServerPort;
				SpiderDocsApplication.CurrentUserSettings.offline = setting.offline;
				SpiderDocsApplication.CurrentUserSettings.autoConnect = setting.autoConnect;
				SpiderDocsApplication.CurrentServerSettings.Save();
			}
			else
			{
				// load as usuall
				SpiderDocsApplication.CurrentServerSettings.server = setting.SelectedServer;
				SpiderDocsApplication.CurrentServerSettings.port = setting.SelectedPort;
				SpiderDocsApplication.CurrentUserSettings.offline = setting.SelectedOffline;
				SpiderDocsApplication.CurrentUserSettings.autoConnect = setting.SelectedAutoConnect;
				SpiderDocsApplication.CurrentServerSettings.Save();
			}
		}
		//---------------------------------------------------------------------------------
	}
}
