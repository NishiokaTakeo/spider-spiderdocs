using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
	public partial class ScanDialog : Spider.Forms.FormBase
	{
//---------------------------------------------------------------------------------
		ImgPDFSaver Saver = new ImgPDFSaver();

		delegate void CallBackFunc();
		CallBackFunc Cb_updateProgress;

		Document CurrentDoc;
		string[] ImagePaths;
		bool FinalStep = false;
		bool mSaveAsNewVer = false;
		public SpiderDocsModule.ImgPDFSaver.en_status status = SpiderDocsModule.ImgPDFSaver.en_status.None;
		bool _Result = true;
		public bool Result	{ get{ return _Result; } }

        //List<OCRManager> ocrManagers = new List<OCRManager>();

//---------------------------------------------------------------------------------
		public ScanDialog()
		{
			InitializeComponent();

			Cb_updateProgress = updateProgress;

			Saver.onProgressUpdated += ImgPDFSaver_onUpdateState;
			Saver.onCompleted += ImgPDFSaver_onCompleted;

			Saver.onStartExtractImage += ImgPDFSaver_onUpdateState;
			//Saver.onRequestExtractedImageAdd += ImgPDFSaver_onUpdateState;
			Saver.onCompletedExtractImage += ImgPDFSaver_onCompletedExtractImage;
			Saver.onBeforeSave += ImgPDFSaver_onBeforeSave;

			Saver.onCompletedConvertToPDF += ImgPDFSaver_onUpdateState;
			Saver.onCompletedSaveFile += ImgPDFSaver_onUpdateState;

			Saver.onCancelled += ImgPDFSaver_onCancelled;
			
			updateProgress();
		}

//---------------------------------------------------------------------------------
		private void btnCancel_Click(object sender,EventArgs e)
		{
			this.btnCancel.Enabled = false;
			_Result = false;
			Saver.Abort();

			this.DialogResult = DialogResult.None;
		}

//---------------------------------------------------------------------------------
// Functions ----------------------------------------------------------------------
//---------------------------------------------------------------------------------
		public void StartPdfConversion(Document doc, string[] paths, bool SaveAsNewVer)
		{
			CurrentDoc = doc;
			ImagePaths = paths;
			mSaveAsNewVer = SaveAsNewVer;

			Task task = Task.Factory.StartNew(() => ExtractImage());

			this.ShowDialog();
		}

//---------------------------------------------------------------------------------
		void ExtractImage()
		{
			Saver.ExtractImage(ImagePaths);
		}

//---------------------------------------------------------------------------------
		void SaveFile()
		{
			FinalStep = true;

			ConvertedPDFSaveMode mode = ConvertedPDFSaveMode.InsertDocument;
			if(mSaveAsNewVer)
			{
				mode = ConvertedPDFSaveMode.UpdateDocument;

				if(CurrentDoc.id_status != en_file_Status.checked_out)
				{
					CurrentDoc.CheckOut(false);
					CurrentDoc.id_event = 0;
				}
			}

			CurrentDoc.id_user = SpiderDocsApplication.CurrentUserId;
			Saver.SaveFile(
				SpiderDocsApplication.CurrentUserId, 
				SpiderDocsApplication.CurrentServerSettings.localDb, 
				CurrentDoc, 
				Saver.ExtractedImagePaths, 
				mode, 
				FileFolder.GetTempFolder(), 
				false, 
				SpiderDocsApplication.CurrentUserGlobalSettings.ocr);
		}

//---------------------------------------------------------------------------------
		void updateProgress()
		{
			lblProgress.Text = "Progress: " + Saver.StatusMessage;
			ProgressBar.Value = Saver.ProgBarVal;
			ProgressBar.Visible = Saver.ShowProgBar;
			btnCancel.Visible = Saver.ShowAbort;
		}

//---------------------------------------------------------------------------------
// Callback functions -------------------------------------------------------------
//---------------------------------------------------------------------------------
		void ImgPDFSaver_onUpdateState()
		{
			if(this.Visible)
				Invoke(Cb_updateProgress);
		}

		private void ImgPDFSaver_onBeforeSave(List<SpiderDocsModule.Document> saving, SpiderDocsModule.ImgPDFSaver.ImgPDFSaverArg args)
		{
			foreach (var doc in saving)
			{
				if (!doc.isNotDuplicated(true))
				{
					System.Windows.Forms.MessageBox.Show(lib.msg_found_duplicate_docs, lib.msg_messabox_title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
					args.status = status = SpiderDocsModule.ImgPDFSaver.en_status.Aborting;
					args.errorThread = true;
					_Result = false;
					return;
				}

				if (doc.WarnForDupliate(true))
				{
					if (MessageBox.Show(lib.msg_warn_existing_file, lib.msg_messabox_title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
					{
						args.status = status = SpiderDocsModule.ImgPDFSaver.en_status.Aborting;
						args.errorThread = true;
						_Result = false;
						return;

					}
					else
						doc.hasAccepted = true;

				}
			}
		}

		//---------------------------------------------------------------------------------
		void ImgPDFSaver_onCompletedExtractImage()
		{
			if(this.Visible)
				Invoke(Cb_updateProgress);
		}

//---------------------------------------------------------------------------------
		void ImgPDFSaver_onCancelled()
		{
			Invoke(new CallBackFunc(this.Close));
		}

//---------------------------------------------------------------------------------
		void ImgPDFSaver_onCompleted(bool ans)
		{
			if(this.Visible)
			{
				Invoke(Cb_updateProgress);

				if (status == SpiderDocsModule.ImgPDFSaver.en_status.Aborting )
				{
					MessageBox.Show(lib.msg_error_aborted, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					//return;
				}
				if (!ans)
				{
					MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					Invoke(new CallBackFunc(this.Close));

				}else if(FinalStep)
				{
					Invoke(new CallBackFunc(this.Close));

				}else
				{
					SaveFile();
				}
			}
		}

//---------------------------------------------------------------------------------
	}
}
