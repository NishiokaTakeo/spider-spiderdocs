using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.ServiceProcess;
using System.Reflection;
using Spider.Data;
using SpiderDocsServerModule;
using SpiderDocsModule;
using SpiderDocsApplication = SpiderDocsServerModule.SpiderDocsApplication;
using Lib = SpiderDocsModule.Library;
using NLog;
//---------------------------------------------------------------------------------
namespace SpiderDocsServer
{
	public partial class frmMain : Form
	{
        static Logger logger = LogManager.GetCurrentClassLogger();
        static readonly string serviceName = "Spider Docs Server";
		bool connected = false;
		IPAddress CurrentIPAddress { get { return Dns.GetHostByName(Dns.GetHostName()).AddressList[0]; } }
		bool activated { get
            {
                bool ans = false;

                try
                {
                    return !String.IsNullOrEmpty(SpiderDocsApplication.ServiceSettings.ClientId);
                }
                catch(Exception ex)
                {
                    logger.Error(ex);
                    return ans;
                }


            } }
        

//---------------------------------------------------------------------------------
		public frmMain(bool minimized)
		{
			InitializeComponent();

			if(minimized)
			{
				WindowState = FormWindowState.Minimized;
				ShowInTaskbar = false;
			}

			tsVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
		private void frmMain_Load(object sender, EventArgs e)
		{
			//check activation
			SwitchClientInfoControls(SwitchClientInfoControlsMode.NotActivated);

			if(activated)
			{
				try
				{
					getMachineDetails();
					getConnectionDetails();
					checkServiceStatus();
					SwitchClientInfoControls(SwitchClientInfoControlsMode.Activated);
					txtHostPort.Text = (SpiderDocsApplication.ServiceSettings.Port == 0 ? "" : SpiderDocsApplication.ServiceSettings.Port.ToString());

					tryDatabaseConnection();
				}
				catch(Exception error)
				{
					logger.Error(error);
				}
			}
		}

//---------------------------------------------------------------------------------
		private void frmMain_Resize(object sender, EventArgs e)
		{
			if(WindowState == FormWindowState.Minimized)
			{
				Visible = false;

				notifyIcon.BalloonTipText = serviceName;
				notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
				notifyIcon.ShowBalloonTip(50);
			}
		}

//---------------------------------------------------------------------------------
		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Visible = true;
			this.WindowState = FormWindowState.Normal;
			this.ShowInTaskbar = true;
			BringToFront();
		}

//---------------------------------------------------------------------------------
		private void notifyIcon_MouseMove(object sender, MouseEventArgs e)
		{
			notifyIcon.Text = "Spider Docs - Service: " + getServiceStatus();
			notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
		}

//---------------------------------------------------------------------------------
		void tryDatabaseConnection()
		{
			statusLabel.Text = "Database Status: Not Connected";

			if(!String.IsNullOrEmpty(SpiderDocsApplication.ConnectionSettings.conn))
			{
				disableControlsduringDbTest();

				BackgroundWorker bw_database_loadTest = new BackgroundWorker();
				bw_database_loadTest.DoWork += new DoWorkEventHandler(bw_database_loadTest_DoWork);
				bw_database_loadTest.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_database_loadTest_RunWorkerCompleted);
				bw_database_loadTest.RunWorkerAsync(saveConnectionDetails());
			}
		}

		void bw_database_loadTest_DoWork(object sender, DoWorkEventArgs e)
		{
			ConnectionSettings BackupConnectionSettings = SpiderDocsApplication.ConnectionSettings;
			SpiderDocsApplication.ConnectionSettings = (ConnectionSettings)e.Argument;

			try
			{
				SqlOperation sql = new SqlOperation("document", SqlOperationMode.Select_Scalar);
				sql.GetCountId();
				connected = true;
				e.Result = true;
			}
			catch
			{
				connected = false;
				e.Result = false;

			}finally
			{
				SpiderDocsApplication.ConnectionSettings = BackupConnectionSettings;
			}
		}

		void bw_database_loadTest_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			enableControlsAfterDbTest();

			if((bool)e.Result)
			{
				SpiderDocsApplication.ConnectionSettings = saveConnectionDetails();
				SpiderDocsApplication.ConnectionSettings.Save();

				SpiderDocsApplication.LoadAllSettings2();

				LoadPublicSettings();
				getEmailSetting();

				lblCurrentVersion.Text = Cache.getSystemVersion();
				enableControls();

				statusLabel.Text = "Database Status: Connected";
			   
			}else
			{
				disableControls();
			}
		}

//---------------------------------------------------------------------------------
		string getServiceStatus()
		{
			ServiceController service = new ServiceController(serviceName);
			try
			{
				return service.Status.ToString();
			}
			catch(Exception error)
			{
				logger.Error(error);
				return "Error";
			}
		}

//---------------------------------------------------------------------------------
	}
}
