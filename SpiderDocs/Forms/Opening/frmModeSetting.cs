using System;
using System.Windows.Forms;
using SpiderDocsForms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;

//---------------------------------------------------------------------------------
namespace SpiderDocs
{
	public partial class frmModeSetting : Spider.Forms.FormBase
	{
        //---------------------------------------------------------------------------------
        ApplicationSettings _setting;

        string SelectedServer
        {
            get
            {
                return rbServer.Checked ? txtServer.Text : txtServer2.Text;
            }
        }

        int SelectedPort
        {
            get
            {
                try
                {
                    return Convert.ToInt32(rbServer.Checked ? txtPort.Text : txtPort2.Text);
                }
                catch
                {
                    var def = new ApplicationSettings();
                    return rbServer.Checked ? def.UpdateServerPort: def.UpdateServerPort2;
                }

            }
        }

        bool SelectedOffline {
            get
            {
                return rbServer.Checked ? ckWorkOffline.Checked : ckWorkOffline2.Checked;
            }
        }

        bool SelectedAutoConnect {
            get
            {
                return rbServer.Checked ? ckAutoConnection.Checked : ckAutoConnection2.Checked;
            }
        }


        public frmModeSetting()
		{
			InitializeComponent();
		}

//---------------------------------------------------------------------------------
		private void frmModeSetting_Load(object sender, EventArgs e)
		{
            _setting =  new ApplicationSettings();

            rbStandalone.Checked = _setting.localDb;
            rbServer.Checked = _setting.UpdateServerChecked;

            // Server 1
            txtPort.Text = _setting.UpdateServerPort.ToString();
			txtServer.Text = _setting.UpdateServer;

			ckWorkOffline.Checked = _setting.offline;
			ckAutoConnection.Checked = _setting.autoConnect;

			//if userName blank cannot use "Work offline" button
			ckWorkOffline.Enabled = (SpiderDocsApplication.CurrentUserName != "" ? true : false);


			// Server 2
			rbServer2.Checked = _setting.UpdateServer2Checked;
			txtPort2.Text = _setting.UpdateServerPort2.ToString();
			txtServer2.Text = _setting.UpdateServer2;

			ckWorkOffline2.Checked = _setting.offline2;
			ckAutoConnection2.Checked = _setting.autoConnect2;

			//if userName blank cannot use "Work offline" button
			ckWorkOffline2.Enabled = (SpiderDocsApplication.CurrentUserName != "" ? true : false);

            ModeChanged();


			// if multi address feature is disable then all server2 controls disable.
			var featureMultiaddress = SpiderDocsApplication.CurrentPublicSettings?.feature_multiaddress ?? _setting.PreviousMultiAddress;
			rbServer2.Enabled = txtPort2.Enabled = txtServer2.Enabled = ckWorkOffline2.Enabled = ckAutoConnection2.Enabled = featureMultiaddress;

			if(false == featureMultiaddress)
			{
				rbServer.Checked = true;
				ModeChanged();

				ApplyChanges();
			}
		}

//---------------------------------------------------------------------------------
		private void rbStandalone_CheckedChanged(object sender, EventArgs e)
		{
			ModeChanged();
		}

//---------------------------------------------------------------------------------
		private void rbServer_CheckedChanged(object sender, EventArgs e)
		{
			ModeChanged();
		}

//---------------------------------------------------------------------------------
		private void txtPort_KeyPress_1(object sender, KeyPressEventArgs e)
		{
			if(!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
				e.Handled = true;
		}

//---------------------------------------------------------------------------------
		private void btnOpen_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			if(ServerCredentialCheck())
				Close();
		}

//---------------------------------------------------------------------------------
		private void frmModeSetting_KeyDown(object sender, KeyEventArgs e)
		{
			if((e.KeyCode == Keys.Enter) && ServerCredentialCheck())
				Close();
		}

//---------------------------------------------------------------------------------
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

//---------------------------------------------------------------------------------
		void ModeChanged()
		{
            if (rbStandalone.Checked)
			{
                this.plServer.Enabled = false;
                this.plServer2.Enabled = false;
            }
            else if(rbServer.Checked)
			{
				this.plServer.Enabled = true;
                this.plServer2.Enabled = false;
            }
            else if(rbServer2.Checked)
            {
                this.plServer2.Enabled = true;
                this.plServer.Enabled = false;
            }
		}

//---------------------------------------------------------------------------------
		bool ServerCredentialCheck()
		{
			bool ans = true;

            //get IP from hostName and validate IP Address
            string strIPAddress = Utilities.CheckIpAddress(txtServer.Text);

			//validade IP and Port Number
			if((strIPAddress == "") || (txtPort.Text == ""))
			{
				if(!SpiderDocsApplication.CurrentServerSettings.localDb)
				{
					ans = false;
					MessageBox.Show(lib.msg_required_server_address, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}

			}else
			{


            }

            if(ans)
            {
                ApplicationSettings setting = new ApplicationSettings();
                setting.localDb = rbStandalone.Checked;

                // Server 1
                setting.UpdateServerChecked = rbServer.Checked;
                setting.UpdateServer = txtServer.Text;
                setting.UpdateServerPort = Convert.ToInt32(txtPort.Text);
                setting.offline = ckWorkOffline.Checked;
                setting.autoConnect = ckAutoConnection.Checked;

                // Server 2
                setting.UpdateServer2Checked = rbServer2.Checked;
                setting.UpdateServer2 = txtServer2.Text;
                setting.UpdateServerPort2 = Convert.ToInt32(txtPort2.Text);
                setting.offline2 = ckWorkOffline2.Checked;
                setting.autoConnect2 = ckAutoConnection2.Checked;

                setting.SaveAsJson();

                // Uses for application.
				ApplyChanges();

			}

            return ans;
		}

		void ApplyChanges()
		{
			// Uses for application.
			SpiderDocsApplication.CurrentServerSettings.server = SelectedServer;
			SpiderDocsApplication.CurrentServerSettings.port = SelectedPort;
			SpiderDocsApplication.CurrentServerSettings.localDb = rbStandalone.Checked;
			SpiderDocsApplication.CurrentUserSettings.offline = SelectedOffline;
			SpiderDocsApplication.CurrentUserSettings.autoConnect = SelectedAutoConnect;
			SpiderDocsApplication.CurrentServerSettings.Save();
		}

		private void rbServer2_CheckedChanged(object sender, EventArgs e)
        {
            ModeChanged();
        }

        //---------------------------------------------------------------------------------
    }
}
