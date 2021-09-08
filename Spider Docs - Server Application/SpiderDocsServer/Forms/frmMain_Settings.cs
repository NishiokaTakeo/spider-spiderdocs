using System;
using System.Windows.Forms;
using System.Drawing;
using System.ServiceProcess;
using Spider.Forms;
using SpiderDocsModule;
using SpiderDocsApplication = SpiderDocsServerModule.SpiderDocsApplication;
using Lib = SpiderDocsModule.Library;

//---------------------------------------------------------------------------------
namespace SpiderDocsServer
{
	public partial class frmMain : Form
	{
		private void btnSocket_Click(object sender, EventArgs e)
		{
			if(btnSocket.Text == "Start Service")
			{
				if(SpiderDocsApplication.ServiceSettings.Port <= 0)
				{
					txtHostPort.BackColor = Color.LavenderBlush;
					txtHostPort.Focus();
					MessageBox.Show("You need set up a port number before try start the service.", "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				else
				{
					txtHostPort.BackColor = Color.White;
				}

				//ServiceController service = new ServiceController(serviceName);
				try
				{
					//TimeSpan timeout = TimeSpan.FromMilliseconds(50000);

					//service.Start();
					//service.WaitForStatus(ServiceControllerStatus.Running, timeout);

                    ServiceManager sManager = new ServiceManager(serviceName);
                    sManager.Start();

                    lblServiceStatus.Text = "Running";
					lblMsgServiceStatus.Text = "Running";
					btnSocket.Text = "Stop Service";
				}
				catch(Exception error)
				{
					MessageBox.Show(error.Message, "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Error);
					logger.Error(error);
				}

			}
			else if(btnSocket.Text == "Stop Service")
			{
				//ServiceController service = new ServiceController(serviceName);
				try
				{
					//TimeSpan timeout = TimeSpan.FromMilliseconds(50000);

					//service.Stop();
					//service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    ServiceManager sManager = new ServiceManager(serviceName);
                    sManager.Stop();

                    lblServiceStatus.Text = "Stopped";
					lblMsgServiceStatus.Text = "Stopped";
					btnSocket.Text = "Start Service";
				}
				catch(Exception error)
				{
					MessageBox.Show(error.Message, "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Error);
					logger.Error(error);
				}
			}
		}

//---------------------------------------------------------------------------------
		private void cbRebuild_CheckedChanged(object sender,EventArgs e)
		{
		}

//---------------------------------------------------------------------------------
		private void ckFooter_CheckedChanged(object sender,EventArgs e)
		{
			GrpFooterMenu.Enabled = ckFooter.Checked;
		}

//---------------------------------------------------------------------------------
		private void btnSaveOptions_Click(object sender, EventArgs e)
		{
			string error = "";

			FormUtilities.PutErrorColour(txtMax_recents, false, true);
			if(txtMax_recents.Text == "")
			{
				FormUtilities.PutErrorColour(txtMax_recents, true);
				error = "Max Recents Files'is a required field.";
			}
			
			FormUtilities.PutErrorColour(txtMax_docs, false, true);
			if(txtMax_docs.Text == "")
			{
				FormUtilities.PutErrorColour(txtMax_docs, true);
				error = "Max files per Search' is a required field.";
			}

			if(String.IsNullOrEmpty(error))
			{
				SaveClientInfo();
				SavePublicSettings();
				MessageBox.Show("Options has been saved successfully", "Spider Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);

			}else
			{
				MessageBox.Show(error, Lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

//---------------------------------------------------------------------------------
		void SavePublicSettings()
		{
			SpiderDocsApplication.CurrentPublicSettings.maxDocs = int.Parse(txtMax_docs.Text);
			SpiderDocsApplication.CurrentPublicSettings.maxDocsRecents = int.Parse(txtMax_recents.Text);
			SpiderDocsApplication.CurrentPublicSettings.watermark = ckShow_watermarks.Checked;
			SpiderDocsApplication.CurrentPublicSettings.reasonNewVersion =ckReasonNewVersion.Checked;
			SpiderDocsApplication.CurrentPublicSettings.allow_workspace = ckAllowWorkSpace.Checked;
			SpiderDocsApplication.CurrentPublicSettings.allow_duplicatedName = ckAllowDuplicatedFilesNames.Checked;
			SpiderDocsApplication.CurrentPublicSettings.webService_address = txtWebServiceAddress.Text;
			SpiderDocsApplication.CurrentPublicSettings.add_footer = ckFooter.Checked;
				
			if(rbFooter_Option.Checked)
				SpiderDocsApplication.CurrentPublicSettings.footer_menu = en_footer_menu.show_option;
			else
				SpiderDocsApplication.CurrentPublicSettings.footer_menu = en_footer_menu.withFooter;

			SpiderDocsApplication.CurrentPublicSettings.Save();
		}

//---------------------------------------------------------------------------------
		void LoadPublicSettings()
		{
			txtMax_docs.Text = SpiderDocsApplication.CurrentPublicSettings.maxDocs.ToString();
			txtMax_recents.Text = SpiderDocsApplication.CurrentPublicSettings.maxDocsRecents.ToString();
			ckShow_watermarks.Checked = SpiderDocsApplication.CurrentPublicSettings.watermark;
			ckReasonNewVersion.Checked = SpiderDocsApplication.CurrentPublicSettings.reasonNewVersion;
			ckAllowWorkSpace.Checked = SpiderDocsApplication.CurrentPublicSettings.allow_workspace;
			ckAllowDuplicatedFilesNames.Checked = SpiderDocsApplication.CurrentPublicSettings.allow_duplicatedName;
			txtWebServiceAddress.Text = SpiderDocsApplication.CurrentPublicSettings.webService_address;

			ckFooter.Checked = SpiderDocsApplication.CurrentPublicSettings.add_footer;
			GrpFooterMenu.Enabled = ckFooter.Checked;

			rbFooter_Option.Checked = false;
			rbFooter_With.Checked = false;

			switch(SpiderDocsApplication.CurrentPublicSettings.footer_menu)
			{
			case en_footer_menu.show_option:
				rbFooter_Option.Checked = true;
				break;

			case en_footer_menu.withFooter:
				rbFooter_With.Checked = true;
				break;
			}
		}

//---------------------------------------------------------------------------------
	}

}
