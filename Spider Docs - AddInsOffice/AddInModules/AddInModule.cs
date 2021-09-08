using System;
using System.Windows.Forms;
using System.IO;
using SpiderDocsForms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
//using SpiderDocsApplication = SpiderDocsForms.SpiderDocsApplication;
using Document = SpiderDocsForms.Document;
using Spider.Drawing;
using NLog;

//---------------------------------------------------------------------------------
namespace AddInModules
{
	public class Utilities : SpiderDocsForms.Utilities
	{
	}

//---------------------------------------------------------------------------------
	public class AddInModule
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		public static bool SaveDocument(string strDocPath, object activeDocument, Func<string,Document,bool> SaveOfficeDocument)
		{
			Document dummy;
			return SaveDocument(strDocPath, activeDocument,SaveOfficeDocument, out dummy);
		}

		public static bool SaveDocument(string strDocPath, object activeDocument, Func<string,Document,bool> SaveOfficeDocument, out Document doc)
		{
			bool ans = false;
			doc = null;

			if(activeDocument == null)		//check if there is a file opened
			{
				MessageBox.Show("You have no document opened", "SpiderDocs - Office Integration");
				return ans;
			}
			else if(SpiderDocsApplication.CurrentUserId <= 0)	//check if user is logged
			{
				return ans;
			}

			if(File.Exists(strDocPath))
			{
				if(0 < (File.GetAttributes(strDocPath) & FileAttributes.ReadOnly))
				{
					MessageBox.Show(lib.msg_permissio_readOnly + "\nPlease make sure if this file has been checked in already.", lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				}else
				{
                    Cache.RemoveAll();

                    // save current file before import
                    SaveOfficeDocument("", null);

					//check if file is from check out or new file
					doc = FileFolder.GetDocumentFromWorkSpace(strDocPath);

					// Work Space file
					if(doc != null)
					{
						int old_id_version = doc.id_version;
						ans = saveNewVersion(doc);

						if(ans)
						{
							string new_full_path = FileFolder.GetTempFolder() + doc.PathWithVersionIdFolder;
							string new_path = FileFolder.GetPath(new_full_path);
							string old_checked_out_path = FileFolder.GetUserFolder() + old_id_version.ToString();

							Directory.CreateDirectory(new_path);
							SaveOfficeDocument(new_full_path, doc);

							if(!FileFolder.DeleteFilesAndThisDir(old_checked_out_path))
								File.SetAttributes(new_path, (File.GetAttributes(new_path) | FileAttributes.Hidden | FileAttributes.ReadOnly));

							File.SetAttributes(new_full_path, File.GetAttributes(new_full_path) | FileAttributes.ReadOnly);

							MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);
						}
					}
					// saved file
					else
					{
						doc = saveNewDocument(strDocPath, DocumentSaveButtons_FormMode.Normal);

						if(doc != null)
						{
							SaveOfficeDocument("", doc);
							ans = true;
						}
					}
				}

			// not saved file -> new file
			}else
			{
				strDocPath = FileFolder.GetAvailableFileName(SpiderDocsModule.FileFolder.GetTempFolder() + strDocPath);
				SaveOfficeDocument(strDocPath, null);

				doc = saveNewDocument(strDocPath, DocumentSaveButtons_FormMode.AddIn);

				if(doc != null)
				{
					ans = true;
					SaveOfficeDocument("", doc);
				}
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		static bool OpenForm(Document doc)
		{
			bool ans = false;

			frmReasonNewVersion reason = new frmReasonNewVersion(doc);
			reason.ShowDialog();

			if(reason.result)
			{
				ans = true;
				doc.reason = reason.reason;
			}

			reason.Dispose();
			return ans;
		}

//---------------------------------------------------------------------------------
		static Document saveNewDocument(string strDocPath, DocumentSaveButtons_FormMode frmMode)
		{
			Document ans = null;

			//show form
			frmSaveDocExternal frm;
			frm = new frmSaveDocExternal(strDocPath, frmMode);
			frm.ShowDialog();

			if(frm.success)
				ans = frm.GetCheckedDocuments()[0];

			frm.Dispose();
			return ans;
		}

//---------------------------------------------------------------------------------
		static bool saveNewVersion(Document objDoc)
		{
			bool ans = false;

			try
			{
				Document wrk = DocumentController.GetDocument(objDoc.id);
				objDoc = DocumentController.MergeMissingProperties(objDoc);

				if(objDoc.IsUpdateAllowed(wrk.version)
				&& (!SpiderDocsApplication.CurrentPublicSettings.reasonNewVersion || OpenForm(objDoc)))
				{
					if(objDoc.id_status == en_file_Status.checked_in)
						objDoc.CheckOut(false, false, false);

					objDoc.id_event = 0;
					string error = objDoc.AddVersion();

					if(!String.IsNullOrEmpty(error))
					{
						MessageBox.Show(error, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

					}else
					{
						ans = true;
						MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.GridUpdateCount);

						//success message
						MessageBox.Show(lib.msg_sucess_save_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
			catch(System.Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_save_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static bool SaveWorkspace(string strDocPath, object activeDocument, Func<string,Document,bool> SaveOfficeDocument)
		{
			if(activeDocument == null)
			{
				MessageBox.Show("You have no document opened", "SpiderDocs - Office Integration");
				return false;
			}

			string workSpace_path = FileFolder.GetUserFolder();
			string outputFile_path = "";

			try
			{

                dynamic officeDocument = activeDocument;
                var path = officeDocument.FullName;

                if (System.IO.File.Exists(path))
                {
                    var wFile = new WorkSpaceFile(path, workSpace_path);
                    string shortHash = wFile.GetVerZeroFolderName();

                    //check file name
                    workSpace_path = workSpace_path + shortHash + "\\";
                }
            }
			catch(Exception ex)
            {
                logger.Error(ex);
            }

            // not saved file -> new file
            if (!File.Exists(strDocPath))
			{
				outputFile_path = FileFolder.GetAvailableFileName(workSpace_path + strDocPath);

                logger.Debug("Saving path: {0}", outputFile_path);

                SaveOfficeDocument(outputFile_path, null);
				//activeDocument.Close();

			}else
			{
				//check if this file has DMS data or not
				Document doc = FileFolder.GetDocumentFromWorkSpace(strDocPath);
				outputFile_path = workSpace_path + Path.GetFileName(strDocPath);

				if(doc == null)	// saved file
					outputFile_path = FileFolder.GetAvailableFileName(outputFile_path);
                //else
                    // Work Space file

                logger.Debug("Saving path: {0}", outputFile_path);

                SaveOfficeDocument(outputFile_path, doc);
			}

			MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);
			MessageBox.Show(lib.msg_sucess_save_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);

			return true;
		}

//---------------------------------------------------------------------------------
		public static string AddExtension(string src, string ext)
		{
			if(String.IsNullOrEmpty(Path.GetExtension(src)))
				src += ext;

			return src;
		}

//---------------------------------------------------------------------------------
		public static bool IsSaveWorkspaceEnabled(string path)
		{
			if((String.IsNullOrEmpty(path) || AddInModules.FileFolder.GetDocumentFromWorkSpace(path) == null)
			&& (SpiderDocsApplication.CurrentPublicSettings != null)
			&& SpiderDocsApplication.CurrentPublicSettings.allow_workspace)
			{
				return true;

			}else
			{
				return false;
			}
		}

//---------------------------------------------------------------------------------
	}
}
