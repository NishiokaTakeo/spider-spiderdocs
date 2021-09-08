using System;
using System.Windows.Forms;
using SpiderDocsModule;
using Spider.IO;
using lib = SpiderDocsModule.Library;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
	public partial class Document : SpiderDocsModule.Document
	{
//---------------------------------------------------------------------------------
		public bool IsUpdateAllowed(int SrcVerNo = 0, bool IgnoreCheckout = false)
		{
			if(SrcVerNo <= 0)
				SrcVerNo = this.version;
			
			bool ans = true;

			if(IsActionAllowed(en_Actions.CheckIn_Out) == false)
			{
				ans = false;

				//check status
				//-------------------------------------------------------------------------------------------------
				switch(id_status)
				{
				case en_file_Status.readOnly: //ReadOnly
					MessageBox.Show(lib.msg_permissio_readOnly, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;

				case en_file_Status.archived: //Archived
					MessageBox.Show(lib.msg_permissio_archived, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;

				case en_file_Status.deleted: //Deleted
					MessageBox.Show(lib.msg_permissio_deleted, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;

				case en_file_Status.checked_out:
					MessageBox.Show(lib.msg_permissio_checkIn, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;

				default:
					switch(id_sp_status)
					{
					case en_file_Sp_Status.review:
					case en_file_Sp_Status.review_overdue:
						MessageBox.Show(lib.msg_permission_review_checkOut, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;

					default:
						MessageBox.Show(lib.msg_permission_file_not_checkOut, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;
					}
					break;
				}

			}else
			{
				switch(id_status)
				{
				case en_file_Status.checked_in: //Checked In
					// nothing to do
					break;

				case en_file_Status.checked_out: //Checked-out
                    // file has been checked out by current user or you are administrator. ( [dbo].[group].[is_admin] = true )
                    if (SpiderDocsApplication.CurrentUserId != id_checkout_user
                        && !((List<int>)GroupController.GetGroupId(SpiderDocsApplication.CurrentUserId)).Exists(x => GroupController.IsAdmin(x) == true))
					{
						ans = false;
						MessageBox.Show(lib.msg_permissio_different_owner, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

					break;
				}
				
				if(ans)
				{
					//check same version
					//------------------------------------------------------------------------------------------------------------------------
					if(SrcVerNo != version)
					{
						ans = false;
						MessageBox.Show(lib.msg_permissio_different_version, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		override public bool IsActionAllowed(en_Actions action, int user_id = -1, Dictionary<en_Actions, en_FolderPermission> permissions = null)
		{
			if(user_id <= 0)
				user_id = SpiderDocsApplication.CurrentUserId;

			bool ans = base.IsActionAllowed(action, user_id, permissions);

			if(ans)
			{
				switch(action)
				{
				case en_Actions.CheckIn_Out_Foot:
					if(FileFolder.OfficeCheck(this.extension) != en_OfficeType.Word)
						ans = false;
					break;

				case en_Actions.SendByEmail:
					if(!SpiderDocsApplication.IsOutlook)
						ans = false;
					break;

				case en_Actions.SendByEmail_PDF:
					if((FileFolder.OfficeCheck(this.extension) == en_OfficeType.NotOffice) || !SpiderDocsApplication.IsOutlook)
						ans = false;
					break;

				case en_Actions.Export_PDF:
					if(FileFolder.OfficeCheck(this.extension) == en_OfficeType.NotOffice)
						ans = false;
					break;
				}
			}

			return ans;
		}
        
		
	//---------------------------------------------------------------------------------
		public bool cancelCheckOut()
		{
			logger.Trace("Begin");
			
			//check status
			if (this.IsUpdateAllowed(this.version))
			{
				string path = FileFolder.GetUserFolder() + this.id_version.ToString();
				if (System.IO.Directory.Exists(path) && !FileFolder.DeleteFilesAndThisDir(path))
				{
					MessageBox.Show(lib.msg_file_blocked, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}

				//open connection
				//SqlOperation sql = new SqlOperation();
				//sql.BeginTran();

				try
				{
                    //this.id_event = EventIdController.GetEventId(en_Events.Cancel);
                    //this.id_status = en_file_Status.checked_in;

                    //DocumentController.InsertOrUpdateDocument(sql, this, false);
                    //HistoryController.SaveDocumentHistoric(sql, this);

                    ////commit transaction
                    //sql.CommitTran();
                    base.CancelCheckOut();

                    if ((this.id_sp_status != en_file_Sp_Status.review)
					&& (this.id_sp_status != en_file_Sp_Status.review_overdue))
					{
						sendEmailFileTracked();
					}

					return true;
				}
				catch (Exception error)
				{
					//sql.RollBack();
					logger.Error(error);
					throw error;
				}

			}

			return false;
		}

		//---------------------------------------------------------------------------------
		// ETC ----------------------------------------------------------------------------
		//---------------------------------------------------------------------------------
		public void sendEmailFileTracked()
		{
			logger.Trace("Begin");
			List<string> arrayTracked = DocumentController.GetTrackingUserEmail(this.id);

			if (arrayTracked.Count > 0)
			{
				try
				{
					List<string> ToList = new List<string>();

					if (SpiderDocsApplication.CurrentMailSettings.send)
					{
						for (int i = 0; i < arrayTracked.Count; i++)
						{
							string To = arrayTracked[i];
							ToList.Add(To);

						}

						string Subject = String.Format(lib.msg_mail_available_Title, this.title);
						string Body = String.Format(lib.msg_mail_available_Body, this.id, this.title, this.name_folder, this.version);

						DmsFile.MailDmsFile(this, ToList, Subject, Body);

						DocumentController.RemoveDocumentTracked(this.id);
					}
				}
				catch (Exception error)
				{
					logger.Error(error);
				}
			}
		}
//---------------------------------------------------------------------------------
	}
}
