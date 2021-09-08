using System;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Windows.Forms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using Spider;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
	public partial class Document : SpiderDocsModule.Document
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

//---------------------------------------------------------------------------------
		public Document() : base()
		{
		}

        //---------------------------------------------------------------------------------
        public string ContainerFolder()
        {
            return System.IO.Path.GetDirectoryName(FileFolder.GetUserFolder() + this.PathWithVersionIdFolder);
        }

        public bool Open()
		{
			bool ans = false;
            string expPath = string.Empty;

            if ( base.IsActionAllowed(en_Actions.OpenRead, SpiderDocsApplication.CurrentUserId) || FolderController.isArhived(this.id_folder))
                expPath = base.Open(SpiderDocsApplication.CurrentUserId);

            if (!String.IsNullOrEmpty(expPath))
			{
				//check if this file is an old version
				//Add Watermark for ".docx and .pdf" documents (only for old versions)
				if(((Convert.ToUInt32(this.version) < DocumentController.GetDocument(this.id).version) || this.id_status == en_file_Status.archived) && SpiderDocsApplication.CurrentPublicSettings.watermark)
				{
					try
					{
						if(FileFolder.extensionsForWord.Contains(this.extension))
						{
							using(WordprocessingDocument Wddoc = WordprocessingDocument.Open(expPath, true))
								WaterMark.addWaterMark_word(Wddoc, this.version, this.id_status);
						}

						if(this.extension == ".pdf")
							WaterMark.addWaterMark_pdf(expPath, this.id.ToString(), this.version.ToString());

					}
					catch(Exception error)
					{
						logger.Error(error);
					}
				}

				try
				{
					//open the file
					//exclude Outlook .msg file as Outlook is very tricky to deal with read only files
					if(Path.GetExtension(expPath).ToLower() != ".msg")
						File.SetAttributes(expPath, File.GetAttributes(expPath) | FileAttributes.ReadOnly);

					Task task = Task.Factory.StartNew(() => Process.Start(expPath));
				}
				catch(Exception error)
				{
					if(error.Message.IndexOf("The process cannot access the file") != -1)
						MessageBox.Show(lib.msg_file_blocked, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);                
					else
						MessageBox.Show(lib.msg_error_default + lib.msg_error_description + error.Message, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				ans = true;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public bool CheckOut(bool footer)
		{
			return base.CheckOut(SpiderDocsApplication.CurrentUserId, footer);
		}

		public bool CheckOut(bool open, bool footer, bool save = true)
		{
			bool ans = false;

			// if status is not "checkout"
			if(this.id_status != en_file_Status.checked_out)
			{
				try
				{
					DocumentController.MergeMissingProperties(this);

					if(save)
					{
						string path = FileFolder.GetUserFolder() + this.PathWithVersionIdFolder;
						ans = base.CheckOut(SpiderDocsApplication.CurrentUserId, footer, path);

						if(ans)
						{
							this.path = path;
							MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);
						}

					}else
					{
						ans = base.CheckOut(SpiderDocsApplication.CurrentUserId, false);
					}
			
					if(!ans)
					{
						if(this.id_status == en_file_Status.archived)
							MessageBox.Show(lib.msg_checkout_archived, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
						else
							MessageBox.Show(lib.msg_permissio_denied, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);

					}else
					{
						if(save && footer)
							Footer.AddFooter(this);

						if(save && open)
							Process.Start(this.path);

						this.id_status = en_file_Status.checked_out;
					}
				}
				catch(Exception error)
				{
					MessageBox.Show(lib.msg_error_default + lib.msg_error_description + error.Message, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

			}else if(this.id_checkout_user != SpiderDocsApplication.CurrentUserId)
			{
				frmMessageBox frm = new frmMessageBox(this);
				frm.ShowDialog();
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public string AddDocument()
		{
			return base.AddDocument(SpiderDocsApplication.CurrentUserId, SpiderDocsApplication.CurrentServerSettings.localDb);
		}

//---------------------------------------------------------------------------------
		public string AddVersion()
		{
			string ans = "";

			if(this.id_status != en_file_Status.checked_out)
				ans = lib.msg_permission_file_not_checkOut;
			else if(this.id_checkout_user != SpiderDocsApplication.CurrentUserId)
				ans = lib.msg_permissio_different_owner;
			else
				ans = base.AddVersion(SpiderDocsApplication.CurrentUserId, SpiderDocsApplication.CurrentServerSettings.localDb);

			return ans;
		}

//---------------------------------------------------------------------------------
		public string UpdateProperty(bool add_history_log = true)
		{
			return base.UpdateProperty(SpiderDocsApplication.CurrentUserId, add_history_log);
		}

//---------------------------------------------------------------------------------
		public void Export(string path)
		{
			base.Export(path, SpiderDocsApplication.CurrentUserId);
        }

        public override bool isNotDuplicated(bool careOfcnv = false)
        {            
            if (!base.isNotDuplicated(careOfcnv))
            {
                MessageBox.Show(lib.msg_found_duplicate_docs, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        /// <summary>
        /// Warn if same document name is placed in same folder was duplicated
        /// THIS IS NO LONGER USED AS MARTIN'S REQUEST
        /// </summary>
        /// <param name="careOfcnv"></param>
        /// <returns></returns>
        public bool __WarnForDuplicate(bool careOfcnv = false)
        {
            // if (base.WarnForDupliate(careOfcnv))
            // {
            //    if (MessageBox.Show(lib.msg_warn_existing_file, lib.msg_messabox_title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
            //        return false;
            //    else
            //        this.hasAccepted = true;
            // }

            return true;
        }

        //---------------------------------------------------------------------------------
    }
}
