using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using Spider.Forms;
using Spider.Net;
using SpiderDocsModule;
using SpiderDocsServerModule;
using SpiderDocsApplication = SpiderDocsServerModule.SpiderDocsApplication;
using Lib = SpiderDocsModule.Library;

//---------------------------------------------------------------------------------
namespace SpiderDocsServer
{
	public partial class frmMain : Form
	{

		private void btnTestHostPort_Click(object sender, EventArgs e)
		{
			BackgroundWorker bw_socketPort = new BackgroundWorker();

			if(txtHostPort.Text == "")
			{
				txtHostPort.BackColor = Color.LavenderBlush;
				txtHostPort.Focus();
				MessageBox.Show("Please, enter the port number.", "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			}else
			{
				txtHostPort.BackColor = Color.White;

				lblMsgHost.Visible = false;
				pBoxloadinbgHostPort.Visible = true;
				pBoxloadinbgHostPort.Image = Properties.Resources.loading;

				disableControlsSocket();

				bw_socketPort.DoWork += new DoWorkEventHandler(bw_socketPort_DoWork);
				bw_socketPort.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_socketPort_RunWorkerCompleted);
				bw_socketPort.RunWorkerAsync(Convert.ToInt32(txtHostPort.Text));
			}
		}

		void bw_socketPort_DoWork(object sender, DoWorkEventArgs e)
		{
			int BackupPort = SpiderDocsApplication.ServiceSettings.Port;
			SpiderDocsApplication.ServiceSettings.Port = (int)e.Argument;

			try
			{
				TcpClient TcpScan = new TcpClient();
				TcpScan.Connect(CurrentIPAddress, SpiderDocsApplication.ServiceSettings.Port);
				e.Result = false;

			}
			catch
			{
				e.Result = true;
			}
			finally
			{
				SpiderDocsApplication.ServiceSettings.Port = BackupPort;
			}
		}

		void bw_socketPort_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			lblMsgHost.Visible = true;

			if((bool)e.Result)
			{
				lblMsgHost.Text = "Test Successful.";
				pBoxloadinbgHostPort.Image = Properties.Resources.Ok;

			}else
			{
				lblMsgHost.Text = "Test Failed.";
				pBoxloadinbgHostPort.Image = Properties.Resources.Error;
			}

			enableControlsSocket();
		}

//---------------------------------------------------------------------------------
		private void btnSaveHostPort_Click(object sender, EventArgs e)
		{
			SpiderDocsApplication.ServiceSettings.Port = int.Parse(txtHostPort.Text);
			SpiderDocsApplication.ServiceSettings.Save();
		}

//---------------------------------------------------------------------------------
		private void btnTestConn_Click(object sender, EventArgs e)
		{
			BackgroundWorker bw_database = new BackgroundWorker();

			if(validation())
			{
				lblMsg.Visible = false;
				pBoxLoading.Visible = true;
				pBoxLoading.Image = Properties.Resources.loading;

				disableControlsduringDbTest();
				bw_database.DoWork += new DoWorkEventHandler(bw_database_loadTest_DoWork);
				bw_database.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_database_RunWorkerCompleted);
				bw_database.RunWorkerAsync(saveConnectionDetails());
			}
		}

		void bw_database_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			lblMsg.Visible = true;
			
			if((bool)e.Result)
			{
				lblMsg.Text = "Test Successful.";
				pBoxLoading.Image = Properties.Resources.Ok;
				btnSave.Enabled = true;

			}else
			{
				lblMsg.Text = "Test Failed.";
				pBoxLoading.Image = Properties.Resources.Error;
				btnSave.Enabled = false;
			}

			btnTestConn.Enabled = true;
			enableControlsAfterDbTest();
		}

//---------------------------------------------------------------------------------
		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				SpiderDocsApplication.ConnectionSettings = saveConnectionDetails();
				SpiderDocsApplication.ConnectionSettings.Save();

				lblMsg.Text = "Connection parameters have been saved successfully";
				pBoxLoading.Image = Properties.Resources.Ok;

				tryDatabaseConnection();

			}
			catch(Exception error)
			{
				lblMsg.Text = "Operation not performed.";
				pBoxLoading.Image = Properties.Resources.Error;
				logger.Error(error);
			}
		}

//---------------------------------------------------------------------------------
		private void rb_mode_CheckedChanged(object sender,EventArgs e)
		{
			UpdateSaveButtonEnabled();
		}

//---------------------------------------------------------------------------------
		private void btnTestEmailServer_Click(object sender, EventArgs e)
		{
			lblMsgEmail.Visible = false;
			pBoxloadingEmailServer.Visible = true;
			pBoxloadingEmailServer.Image = Properties.Resources.loading;

			disableControlsEmail();

			Spider.Net.Email email = new Spider.Net.Email(GetMailServerSetting());
			email.to.Add(txtEmailAccount.Text);
			email.subject = "Spider Docs Server - Email Test";
			email.body = email.subject;

			email.OnEmailSent = new Spider.Net.Email.EmailSent(a =>
			{
				lblMsgEmail.Visible = true;

				if(String.IsNullOrEmpty(a))
				{
					lblMsgEmail.Text = "Test Successful.";
					pBoxloadingEmailServer.Image = Properties.Resources.Ok;

				}else
				{
					lblMsgEmail.Text = "Test Failed.";
					pBoxloadingEmailServer.Image = Properties.Resources.Error;
                    logger.Error(a);
				}

				enableControlsEmail();				
			});

			email.Send();
		}

//---------------------------------------------------------------------------------
		private void btnSaveEmailsServer_Click(object sender, EventArgs e)
		{
			SMTPSettings wrk = GetMailServerSetting();

			if(wrk != null)
			{
				SpiderDocsApplication.MailSettingss.server = wrk;
				SpiderDocsApplication.MailSettingss.Save();

				lblMsgEmail.Text = "Email settings has been saved successfully";
				pBoxloadingEmailServer.Image = Properties.Resources.Ok;

			}else
			{
				lblMsgEmail.Visible = true;
				lblMsgEmail.Text = "Operation not performed.";
				pBoxloadingEmailServer.Visible = true;
				pBoxloadingEmailServer.Image = Properties.Resources.Error;
			}
		}

//---------------------------------------------------------------------------------
		// Disable the save button when changes are made because connection test should be done before save.
		private void txtDbControls_KeyUp(object sender, KeyEventArgs e)
		{
			UpdateSaveButtonEnabled();
		}

//---------------------------------------------------------------------------------
		private void txtOnlyDigit_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
				e.Handled = true;
		}

//---------------------------------------------------------------------------------
		private void txtHostPort_KeyUp(object sender, KeyEventArgs e)
		{
			if(SpiderDocsApplication.ServiceSettings.Port != Convert.ToInt32(txtHostPort.Text))
			{
				if(btnSocket.Text == "Stop")
					btnSocket.Enabled = false;

				btnSaveHostPort.Enabled = false;

			}else
			{
				btnSocket.Enabled = true;
				btnSaveHostPort.Enabled = true;
			}
		}

//---------------------------------------------------------------------------------
		void getMachineDetails()
		{
			try
			{
				lblHostName.Text = System.Environment.MachineName;
				lblIp.Text = CurrentIPAddress.ToString();

				if(activated)
				{
					if(SpiderDocsApplication.ServiceSettings.LastUpdateCheckedDate != new DateTime())
						lblLastTimeChecked.Text = SpiderDocsApplication.ServiceSettings.LastUpdateCheckedDate.ToString();
				}
			}
			catch(Exception error)
			{
				logger.Error(error);
			}
		}

//---------------------------------------------------------------------------------
		ConnectionSettings saveConnectionDetails()
		{
			ConnectionSettings ans = new ConnectionSettings();

			ans.database_address = txtDbServer.Text;
			ans.database_name = txtDbName.Text;
			ans.user = txtUser.Text;
			ans.password = txtPass.Text;
			ans.mode = getSvModeSettingVal();

			return ans;
		}

//---------------------------------------------------------------------------------
		void getConnectionDetails()
		{
			if(!String.IsNullOrEmpty(SpiderDocsApplication.ConnectionSettings.database_address))
			{
				txtDbServer.Text = SpiderDocsApplication.ConnectionSettings.database_address;
				txtDbName.Text = SpiderDocsApplication.ConnectionSettings.database_name;
				txtUser.Text = SpiderDocsApplication.ConnectionSettings.user;
				txtPass.Text = SpiderDocsApplication.ConnectionSettings.password;

				switch(SpiderDocsApplication.ConnectionSettings.mode)
				{
				case DbManager.en_sql_mode.ms_sql:
				default:
					this.rb_mode1.Checked = true;
					break;

				case DbManager.en_sql_mode.my_sql:
					this.rb_mode2.Checked = true;
					break;
				}
			}
		}

//---------------------------------------------------------------------------------
		DbManager.en_sql_mode getSvModeSettingVal()
		{
			DbManager.en_sql_mode ans = DbManager.en_sql_mode.ms_sql;
			string dmy = "";

			getSvModeSetting(ref ans, ref dmy);

			return ans;
		}

		string getSvModeSettingStr()
		{
			DbManager.en_sql_mode dmy = DbManager.en_sql_mode.ms_sql;
			string ans = "";

			getSvModeSetting(ref dmy, ref ans);

			return ans;
		}

		void getSvModeSetting(ref DbManager.en_sql_mode val, ref string str)
		{
			if(rb_mode2.Checked)
				val = DbManager.en_sql_mode.my_sql;
			else
				val = DbManager.en_sql_mode.ms_sql;

			str = DbManager.GetServerModeStr(val);
		}

//---------------------------------------------------------------------------------
		SMTPSettings GetMailServerSetting()
		{
			SMTPSettings wrk = null;

			try
			{
				wrk = new SMTPSettings();

				wrk.ServerAddress = txtEmailServer.Text;
				wrk.User = txtEmailAccount.Text;
				wrk.Password = txtEmailPassword.Text;
				wrk.Port = int.Parse(txtEmailServerPort.Text);
				wrk.SSL = ckSSL.Checked;

			}catch(Exception error)
			{
				wrk = null;
				logger.Error(error);
			}

			return wrk;
		}

//---------------------------------------------------------------------------------
		void getEmailSetting()
		{
			try
			{
				txtEmailServer.Text = SpiderDocsApplication.MailSettingss.server.ServerAddress;
				txtEmailAccount.Text = SpiderDocsApplication.MailSettingss.server.User;
				txtEmailPassword.Text = SpiderDocsApplication.MailSettingss.server.Password;
				txtEmailServerPort.Text = SpiderDocsApplication.MailSettingss.server.Port.ToString();
				ckSSL.Checked = SpiderDocsApplication.MailSettingss.server.SSL;
			}
			catch(Exception error)
			{
				logger.Error(error);
			}
		}

//---------------------------------------------------------------------------------
		enum SwitchClientInfoControlsMode
		{
			Activated,
			NotActivated,
			NoProductId
		}

		void SwitchClientInfoControls(SwitchClientInfoControlsMode mode)
		{
			bool enable = false;

			txtClientId.Text = "";
			maskProduct_key.Text = "";
			panelActive.BackColor = Color.LightCoral;

			switch(mode)
			{
			case SwitchClientInfoControlsMode.Activated:
				txtClientId.Text = SpiderDocsApplication.ServiceSettings.ClientId;
				lblActivation.Text = "Product active - " + SpiderDocsApplication.ServiceSettings.ClientName;
				maskProduct_key.Text = SpiderDocsApplication.ServiceSettings.ProductKey;
				panelActive.BackColor = Color.MediumAquamarine;
				enable = true;
				break;

			case SwitchClientInfoControlsMode.NotActivated:
				lblActivation.Text = "Client Id not found.";
				break;

			case SwitchClientInfoControlsMode.NoProductId:
				lblActivation.Text = "Product not activated";
				break;
			}

			btnSocket.Enabled = enable;
			btnActiveProduct.Enabled = !enable;
			txtClientId.Enabled = !enable;
			maskProduct_key.Enabled = !enable;
		}

//---------------------------------------------------------------------------------
		void UpdateSaveButtonEnabled()
		{
			if((SpiderDocsApplication.ConnectionSettings.database_address != txtDbServer.Text)
			|| (SpiderDocsApplication.ConnectionSettings.database_name != txtDbName.Text)
			|| (SpiderDocsApplication.ConnectionSettings.user != txtUser.Text)
			|| (SpiderDocsApplication.ConnectionSettings.password != txtPass.Text)
			|| (SpiderDocsApplication.ConnectionSettings.mode != getSvModeSettingVal()))
			{
				btnSave.Enabled = false;

			}else
			{
				btnSave.Enabled = true;
			}
		}

//----------------------------------------------------------------------
		void enableControls()
		{
			btnTestConn.Enabled = true;
			txtDbServer.Enabled = true;
			txtDbName.Enabled = true;
			txtUser.Enabled = true;
			txtPass.Enabled = true;
			rb_mode1.Enabled = true;
			rb_mode2.Enabled = true;

			btnSaveOptions.Enabled = true;

			if(activated)
				btnCheckUpdates.Enabled = true;
		}

		void disableControls()
		{
			btnSave.Enabled = false;
			btnSaveOptions.Enabled = false;
			btnCheckUpdates.Enabled = false;
			btnSocket.Enabled = false;
		}

//---------------------------------------------------------------------------------
		void enableControlsSocket()
		{
			btnSaveHostPort.Enabled = true;
			btnTestHostPort.Enabled = true;
			txtHostPort.Enabled = true;
		}

		void disableControlsSocket()
		{
			btnTestHostPort.Enabled = false;
			txtHostPort.Enabled = false;
		}

//---------------------------------------------------------------------------------
		void disableControlsduringDbTest()
		{
			btnTestConn.Enabled = false;
			txtDbServer.Enabled = false;
			txtDbName.Enabled = false;
			txtUser.Enabled = false;
			txtPass.Enabled = false;
			btnSave.Enabled = false;
			rb_mode1.Enabled = false;
			rb_mode2.Enabled = false;
		}

		void enableControlsAfterDbTest()
		{
			btnTestConn.Enabled = true;
			txtDbServer.Enabled = true;
			txtDbName.Enabled = true;
			txtUser.Enabled = true;
			txtPass.Enabled = true;
			btnSave.Enabled = true;
			rb_mode1.Enabled = true;
			rb_mode2.Enabled = true;
		}

//---------------------------------------------------------------------------------
		void enableControlsEmail()
		{
			btnTestEmailServer.Enabled = true;
			btnSaveEmailsServer.Enabled = true;
		}

		void disableControlsEmail()
		{
			btnTestEmailServer.Enabled = false;
			btnSaveEmailsServer.Enabled = false;
		}

//---------------------------------------------------------------------------------
		bool validation()
		{
			bool ans = true;
			string error = "";

			FormUtilities.PutErrorColour(txtPass, false, true);
			if(txtPass.Text == "")
			{
				FormUtilities.PutErrorColour(txtPass, true);
				error = "Please, enter the password.";
			}

			FormUtilities.PutErrorColour(txtUser, false, true);
			if(txtUser.Text == "")
			{
				FormUtilities.PutErrorColour(txtUser, true);
				error = "Please, enter the User Name.";
			}

			FormUtilities.PutErrorColour(txtDbName, false, true);
			if(txtDbName.Text == "")
			{
				FormUtilities.PutErrorColour(txtDbName, true);
				error = "Please, enter the Database Name.";
			}

			FormUtilities.PutErrorColour(txtDbServer, false, true);
			if(txtDbServer.Text == "")
			{
				FormUtilities.PutErrorColour(txtDbServer, true);
				error = "Please, enter the Database Server.";
			}

			if(!String.IsNullOrEmpty(error))
			{
				MessageBox.Show(error, "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				ans = false;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
	}
}
