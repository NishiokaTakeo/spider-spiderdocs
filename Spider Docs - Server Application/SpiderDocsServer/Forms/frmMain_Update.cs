using System;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.ServiceProcess;
using SpiderDocsModule;
using SpiderDocsApplication = SpiderDocsServerModule.SpiderDocsApplication;
using Lib = SpiderDocsModule.Library;

//---------------------------------------------------------------------------------
namespace SpiderDocsServer
{
	public partial class frmMain : Form
	{
		Update update = null;

//---------------------------------------------------------------------------------
		private void btnCheckUpdates_Click(object sender, EventArgs e)
		{
			//disable buttons
			btnCheckUpdates.Enabled = false;
			btnInstallUpdate.Enabled = false;

			// Create a new thread that calls the Download() method
			Thread thrDownload = new Thread(Download);

			// Start the thread, and thus call Download()
			thrDownload.Start();

			//update date
			lblLastTimeChecked.Text = DateTime.Now.ToString();

			//save date
			SpiderDocsApplication.ServiceSettings.LastUpdateCheckedDate = DateTime.Now;
			SpiderDocsApplication.ServiceSettings.Save();
		}

//---------------------------------------------------------------------------------
		void Download()
		{
			update = new Update(lblCurrentVersion.Text);
			string error = "";
			bool suspend_process = false;

			//---------- get update information from the server ----------
			Invoke(new Action(() =>
			{
				lblMsgUpdate.Visible = true;
				lblMsgUpdate.Text = "Looking for updates...";
				pBoxLoadinbgUpdate.Visible = true;
			}));

			error = update.GetUpdateInfo(txtClientId.Text);
			if(!String.IsNullOrEmpty(error))
			{
				error = "Failed to connect to update servers, please try again later.\n \n" + error;
				suspend_process = true;
			}

			//---------- create working directory ----------
			if(!suspend_process)
			{
				if(update.IsNewVersionAvailable)
				{
					Invoke(new Action(() =>
					{
						lblMsgUpdate.Text = "The version " + update.NewVersionNumber + " is available...downloading!";
					}));

					error = update.CreateWorkDirectory();

					if(!String.IsNullOrEmpty(error))
					{
						error = "Can't create or remove the update directory.\n \n" + error;
						suspend_process = true;
					}

				}else
				{
					Invoke(new Action(() =>
					{
						lblMsgUpdate.Text = "There is no updates available";
						pBoxLoadinbgUpdate.Visible = false;
						btnCheckUpdates.Enabled = true;
					}));

					suspend_process = true;
				}
			}

			//---------- download the update file ----------
			if(!suspend_process)
			{
				error = update.Download(new Action<long,long>((BytesRead, TotalBytes) =>
				{
					Invoke(new Action(() =>
					{
						// Calculate the download progress in percentages
						int PercentProgress = Convert.ToInt32((BytesRead * 100) / TotalBytes);
						// Make progress on the progress bar
						prgDownload.Value = PercentProgress;
						// Display the current progress on the form
						lblProgress.Text = "Downloaded " + BytesRead + " out of " + TotalBytes + " (" + PercentProgress + "%)";
					}));
				}));

				if(!String.IsNullOrEmpty(error))
				{
					error = "Can't download the update files from the server,please try again later.\n \n" + error;

					Invoke(new Action(() =>
					{
						btnCheckUpdates.Enabled = true;
						btnInstallUpdate.Enabled = false;
						lblMsgUpdate.Text = "Download fail!";
						pBoxLoadinbgUpdate.Visible = false;
					}));

					suspend_process = true;

				}else
				{
					Invoke(new Action(() =>
					{
						btnCheckUpdates.Enabled = true;
						btnInstallUpdate.Enabled = true;
						lblMsgUpdate.Text = "Download completed!";
						pBoxLoadinbgUpdate.Visible = false;
					}));
				}
			}

			if(!String.IsNullOrEmpty(error))
			{
				//Utilities.regLog(error);
                logger.Error(error);
				MessageBox.Show(error, Lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnCheckUpdates.Enabled = true;
			}
		}

//---------------------------------------------------------------------------------
		private void btnInstallUpdate_Click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show(
					"Once applied all the client will be disconnected from the Spider Docs and they will be asked to update their version at the next connection. \n \nDo you want to proceed?",
					Lib.msg_messabox_title,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);

			if(result == DialogResult.Yes)
				applyUpdate();
		}

//---------------------------------------------------------------------------------
		void applyUpdate()
		{
            ServiceManager sManager = new ServiceManager(serviceName);

			string error = "";
			bool suspend_process = false;
			//TimeSpan timeout  = TimeSpan.FromMilliseconds(1000);
			//ServiceController service = new ServiceController(serviceName);

			//---------- stopping Spider Docs win service ----------
			try
			{
                sManager.Stop();
                //stop service
    //            if (service.Status.ToString().ToUpper() == "RUNNING")
				//{
				//	service.Stop();
				//	service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
					lblServiceStatus.Text = "Stopped";
					lblMsgServiceStatus.Text = "Stopped";
					btnSocket.Text = "Start Service";
				//}
			}
			catch(Exception e)
			{
				suspend_process = true;
				error = e.Message;
			}

			//---------- Unziping Files ----------
			if(!suspend_process)
			{
                try
                {
				    error = update.ExtructUpdate();

				    if(!String.IsNullOrEmpty(error))
					    suspend_process = true;
                }
                catch (Exception e)
                {
                    error = e.Message;
                }
            }

			//---------- applying database changes ----------
			if(!suspend_process)
			{
                try
                {
                    error = update.RunSqlScript();

                    if (!String.IsNullOrEmpty(error))
                        suspend_process = true;
                }
                catch (Exception e)
                {
                    error = e.Message;
                }
            }

			//---------- Confirm update (web service) ----------
			if(!suspend_process)
			{			
				try
				{
					//connecting to the webservice
					ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
					WebReference.ServiceSoapClient webservice = new WebReference.ServiceSoapClient();
					webservice.downloadDone(txtClientId.Text, update.NewVersionNumber);

				}
				catch(Exception e)
				{
					suspend_process = true;
					error = e.Message;
				}
			}

			//---------- restart service ----------
			if(!suspend_process)
			{			
				try
				{
                    //start the service again
                    sManager.Start();

     //               service.Start();
					//service.WaitForStatus(ServiceControllerStatus.Running, timeout);
					lblServiceStatus.Text = "Running";
					lblMsgServiceStatus.Text = "Running";
					btnSocket.Text = "Stop Service";
				}
				catch(Exception e)
				{
					suspend_process = true;
					error = e.Message;
				}
			}

			//---------- update components ----------
			if(!suspend_process)
			{
				lblCurrentVersion.Text = update.NewVersionNumber;
				prgDownload.Value = 0;
				lblProgress.Text = "Download progress";
				lblMsgUpdate.Text = "Your system is up to date!";
				btnInstallUpdate.Enabled = false;
			}

			if(!String.IsNullOrEmpty(error))
			{
				//Utilities.regLog(error);
                logger.Error(error);
				MessageBox.Show(error, Lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
//---------------------------------------------------------------------------------

	}
}
