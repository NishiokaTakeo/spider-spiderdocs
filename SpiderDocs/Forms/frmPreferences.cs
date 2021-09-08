using System;
using System.Windows.Forms;
using SpiderDocsModule;
using SpiderDocsForms;
using lib = SpiderDocsModule.Library;
using NLog;
//---------------------------------------------------------------------------------
namespace SpiderDocs
{
	public partial class frmPreferences : Spider.Forms.FormBase
	{
        static Logger logger = LogManager.GetCurrentClassLogger();
		public frmPreferences()
		{
			InitializeComponent();
		}

//---------------------------------------------------------------------------------
		private void frmPreferences_Load(object sender, EventArgs e)
		{
			try
			{
				ckSaveCredentials.Checked = SpiderDocsApplication.CurrentUserSettings.autoLogin;
				ckOCR.Checked = SpiderDocsApplication.CurrentUserGlobalSettings.ocr;
                ckStardLogOn.Checked = SpiderDocsApplication.CurrentUserSettings.AutoStartup;
				ckShowImportDialogNewMail.Checked = SpiderDocsApplication.CurrentUserGlobalSettings.show_import_dialog_new_mail;
                chkDefaultMerge.Checked = SpiderDocsApplication.CurrentUserGlobalSettings.default_pdf_merge;
                //chkPDFMerge.Checked = SpiderDocsApplication.CurrentUserGlobalSettings.pdf_merge;
                cbEnableFolderCreationByUser.Checked = SpiderDocsApplication.CurrentUserGlobalSettings.enable_folder_creation_by_user;

                switch (SpiderDocsApplication.CurrentUserGlobalSettings.double_click)
				{
				case en_DoubleClickBehavior.OpenToRead:
				default:
					this.rb_readonly.Checked = true;
					break;

				case en_DoubleClickBehavior.CheckOut:
					this.rb_checkout.Checked = true;
					break;

				case en_DoubleClickBehavior.CheckOutFooter:
					this.rb_checkoutfooter.Checked = true;
					break;
				}
                // Akira Added: exclude archive status
                ckExcludeArchive.Checked = SpiderDocsApplication.CurrentUserGlobalSettings.exclude_archive;

				cbOCRDefault.Checked = SpiderDocsApplication.CurrentUserGlobalSettings.default_ocr_import;

				ApplicationSettings setting = new ApplicationSettings();
				this.txtServer.Text = setting.UpdateServer2 ;
				this.txtPort.Text = setting.UpdateServerPort2.ToString();

				var enabledmultiaddress = SpiderDocsApplication.CurrentPublicSettings.feature_multiaddress;
				txtServer.Enabled = txtPort.Enabled = enabledmultiaddress;

			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default_open_form, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}

//---------------------------------------------------------------------------------
		private void btnSave_Click(object sender, EventArgs e)
		{
			SpiderDocsApplication.CurrentUserSettings.autoLogin = ckSaveCredentials.Checked;

			if(!ckSaveCredentials.Checked)
				SpiderDocsApplication.CurrentUserSettings.pass = "";

			SpiderDocsApplication.CurrentUserGlobalSettings.ocr = ckOCR.Checked;
            SpiderDocsApplication.CurrentUserSettings.AutoStartup = ckStardLogOn.Checked;
			SpiderDocsApplication.CurrentUserGlobalSettings.show_import_dialog_new_mail = ckShowImportDialogNewMail.Checked;
            SpiderDocsApplication.CurrentUserGlobalSettings.exclude_archive = ckExcludeArchive.Checked;
			SpiderDocsApplication.CurrentUserGlobalSettings.default_ocr_import = cbOCRDefault.Checked;
            //SpiderDocsApplication.CurrentUserGlobalSettings.pdf_merge = chkPDFMerge.Checked;
            SpiderDocsApplication.CurrentUserGlobalSettings.default_pdf_merge = chkDefaultMerge.Checked;
            SpiderDocsApplication.CurrentUserGlobalSettings.enable_folder_creation_by_user = cbEnableFolderCreationByUser.Checked;

            if (rb_checkout.Checked)
				SpiderDocsApplication.CurrentUserGlobalSettings.double_click = en_DoubleClickBehavior.CheckOut;
			else if(rb_checkoutfooter.Checked)
				SpiderDocsApplication.CurrentUserGlobalSettings.double_click = en_DoubleClickBehavior.CheckOutFooter;
			else
				SpiderDocsApplication.CurrentUserGlobalSettings.double_click = en_DoubleClickBehavior.OpenToRead;

			SpiderDocsApplication.CurrentUserSettings.Save();
			SpiderDocsApplication.CurrentUserGlobalSettings.Save();

			int port2=0;
			ApplicationSettings setting = new ApplicationSettings();
			setting.UpdateServer2 = this.txtServer.Text;
			if( int.TryParse(this.txtPort.Text, out port2)) setting.UpdateServerPort2 = port2;
			setting.SaveAsJson();

            MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.PreferenceChanged);

            Close();
		}

//---------------------------------------------------------------------------------
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

//---------------------------------------------------------------------------------
	}
}
