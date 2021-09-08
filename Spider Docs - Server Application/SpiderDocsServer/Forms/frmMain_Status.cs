using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Collections;
using System.ServiceProcess;
using System.Diagnostics;
using SpiderDocsModule;
using SpiderDocsApplication = SpiderDocsServerModule.SpiderDocsApplication;
using Lib = SpiderDocsModule.Library;

//---------------------------------------------------------------------------------
namespace SpiderDocsServer
{
	public partial class frmMain : Form
	{
		private void btnActiveProduct_Click(object sender, EventArgs e)
		{
			if(txtClientId.Text == "")
			{
				MessageBox.Show("Enter the Client Id", Lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				txtClientId.Focus();
				return;
			}

			try
			{
				ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
				WebReference.ServiceSoapClient webservice = new WebReference.ServiceSoapClient();

				string clientName = "";
				clientName = webservice.Activation(txtClientId.Text, maskProduct_key.Text, lblCurrentVersion.Text);

				if(clientName != "")
				{
					SaveClientInfo(clientName);
					SwitchClientInfoControls(SwitchClientInfoControlsMode.Activated);
					btnActiveProduct.Enabled = false;
					btnSocket.Enabled = true;

					if(connected)
						btnCheckUpdates.Enabled = true;

					getMachineDetails();
					checkServiceStatus();
					
				}else
				{
					SwitchClientInfoControls(SwitchClientInfoControlsMode.NoProductId);
				}
			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(error.Message, Lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

//---------------------------------------------------------------------------------
		void checkServiceStatus()
		{
			string error = "";
			string args = "";
			ServiceController service = new ServiceController(serviceName);

			try
			{
				args = "/C sc config \"Spider Docs Server\" start= delayed-auto";

				if(service.Status.ToString().ToUpper() == "RUNNING")
				{
					lblServiceStatus.Text = "Running";
					lblMsgServiceStatus.Text = "Running";
					btnSocket.Text = "Stop Service";
				}
				else if(service.Status.ToString().ToUpper() == "STOPPED")
				{
					lblServiceStatus.Text = "Stopped";
					lblMsgServiceStatus.Text = "Stopped";
					btnSocket.Text = "Start Service";
				}
			}
			catch(Exception e)
			{
				error = e.Message;
			}

			if(error.Contains("not found"))
			{
				error = "";

				try
				{
					args = "/C sc create \"Spider Docs Server\" start= delayed-auto binPath= \"" + Path.GetDirectoryName(Application.ExecutablePath) + "\\SpiderDocsWinService.exe\"";

					lblServiceStatus.Text = "Stopped";
					lblMsgServiceStatus.Text = "Stopped";
					btnSocket.Text = "Start Service";
				}
				catch(Exception e)
				{
					error = e.Message;
				}
			}

			if(!String.IsNullOrEmpty(args))
			{
				Process process = new Process();
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.WindowStyle = ProcessWindowStyle.Hidden;
				startInfo.FileName = "cmd.exe";
				startInfo.Arguments = args;
				process.StartInfo = startInfo;
				process.Start();
			}

			if(!String.IsNullOrEmpty(error))
				MessageBox.Show(error, Lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

//---------------------------------------------------------------------------------	
		void SaveClientInfo()
		{
			SaveClientInfo(SpiderDocsApplication.ServiceSettings.ClientName);
		}

		void SaveClientInfo(string ClientName)
		{
			SpiderDocsApplication.ServiceSettings.ClientId = txtClientId.Text;
			SpiderDocsApplication.ServiceSettings.ClientName = ClientName;
			SpiderDocsApplication.ServiceSettings.ProductKey = maskProduct_key.Text;
			SpiderDocsApplication.ServiceSettings.Save();
		}

//---------------------------------------------------------------------------------
	}
}
