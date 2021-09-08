using System;
using System.Drawing;
using System.Windows.Forms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using NLog;
using System.Collections.Generic;
using System.Linq;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
    public enum DocumentSaveButtons_FormMode
	{
		Normal = 0,
		AddIn,
		ExtFile,
		Delete,

		Max
	}

	public enum DocumentSaveButtons_ScanMode
	{
		NoScan = 0,
		Scan,

		Max
	}

//---------------------------------------------------------------------------------
	public partial class DocumentSaveButtons : Spider.Forms.UserControlBase
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		public delegate void EventFunc();

		public bool IsChangeName
		{
			get { return rdChangeName.Checked; }
			set { }
		}

		public bool IsDeleteFile
		{
			get { return rdDeleteFile.Checked; }
			set { }
		}

		public bool IsSaveLocal
		{
			get { return cbSaveLoc.Checked; }
			set { }
		}

		public bool IsSavedAsPdf
		{
			get { return chkPDF.Checked; }
			set { chkPDF.Checked = value; }
		}

        public bool SetSavedAsPdfWithoutRisingEvent
        {
            set
            {
                chkPDF.CheckedChanged -= new System.EventHandler(this.chkPDF_CheckedChanged);
                chkPDF.Checked = value;
                chkPDF.CheckedChanged += new System.EventHandler(this.chkPDF_CheckedChanged);
            }
        }


        public bool IsMerged
        {
            get { return cbMerge.Checked; }
        }

		public bool SetIsMergedWithoutRisingEvent
        {
            set
            {
                cbMerge.CheckedChanged -= new System.EventHandler(this.cbMerge_CheckedChanged);
                cbMerge.Checked = value;
                cbMerge.CheckedChanged += new System.EventHandler(this.cbMerge_CheckedChanged);
            }
        }

        public CheckBox ChkPDF
        {
            get { return this.chkPDF;  }
        }

        public CheckBox chkMerged
        {
            get { return cbMerge; }
        }
        public string SavePath
		{
			get
			{
				if(cbSaveLoc.Checked && !Utilities.CheckSavePath(txtBrowse.Text))
					return "";
				else
					return txtBrowse.Text;
			}

			set { }
		}

		DocumentSaveButtons_FormMode _FormMode = DocumentSaveButtons_FormMode.Normal;
		public DocumentSaveButtons_FormMode FormMode
		{
			get { return _FormMode; }
			set { }
		}

		DocumentSaveButtons_ScanMode _ScanMode = DocumentSaveButtons_ScanMode.NoScan;
		public DocumentSaveButtons_ScanMode ScanMode
		{
			get { return _ScanMode ; }
			set { }
		}

//---------------------------------------------------------------------------------
		public enum en_SaveMode
		{
			NewDoc = 0,
			NewVer,

			Max
		}
		string[] _numberOfFiles = new string[] { };

		en_SaveMode _SaveMode = en_SaveMode.NewDoc;
		public en_SaveMode SaveMode
		{
			get { return _SaveMode; }
			set { }
		}

        public int[] GetNotificationGroupID()
        {
            return (int[])this.cboNotificationGroup.getComboValue<DocumentAttributeCombo>().Select(a => a.id).ToArray();
        }

        public void SetNotificationGroupVisible(bool visible)
        {
            this.cboNotificationGroup.Visible = this.lblNotificationGroup.Visible = visible;
        }

//---------------------------------------------------------------------------------
		public DocumentSaveButtons()
		{
			InitializeComponent();

			this.MinimumSize = new Size(0, this.plButtons.Height);

            UserSettings us = SpiderDocsModule.Factory.Instance4UserSettins();// new UserSettings();
			us.Load();

			if ( SpiderDocsApplication.CurrentUserGlobalSettings.ocr)
				this.chkPDF.Checked = SpiderDocsApplication.CurrentUserGlobalSettings.default_ocr_import;


            //this.cbMerge.Visible = SpiderDocsApplication.CurrentUserGlobalSettings.pdf_merge;
            //if (SpiderDocsApplication.CurrentUserGlobalSettings.pdf_merge)
            //{
            //    this.cbMerge.Checked = SpiderDocsApplication.CurrentUserGlobalSettings.default_pdf_merge;
            //}

            this.cbMerge.Checked = SpiderDocsApplication.CurrentUserGlobalSettings.default_pdf_merge;

            InitNotificationGroup();
        }

        void InitNotificationGroup()
        {
            List<NotificationGroup> group = NotificationGroupController.GetGroups();
            int[] ids = group.Select(x => x.id).ToArray();

            cboNotificationGroup.Clear();

            DTS_NotificationGroup DA_NotifiationGroup = new DTS_NotificationGroup();
            DA_NotifiationGroup.Select();
            var table = DA_NotifiationGroup.GetDataTable();

            foreach (System.Data.DataRow row in table.Rows)
            {
                int id = int.Parse(row["id"].ToString());

                cboNotificationGroup.AddItem(new DocumentAttributeCombo()
                {
                    id = id,
                    text = row["group_name"].ToString(),
                    Selected = false
                }, false);

            }
        }


        //---------------------------------------------------------------------------------
        public event EventFunc SaveBtn_PDF_Mode_Changed;
		public void chkPDF_CheckedChanged(object sender, EventArgs e)
		{

            if (_numberOfFiles.Length > 1 )
            {
                if (this.chkPDF.Checked || _numberOfFiles.All(x => System.IO.Path.GetExtension(x).ToLower() == ".pdf"))
                    this.cbMerge.Visible = true;// this.chkPDF.Checked;
                else
                    this.cbMerge.Visible = this.SetIsMergedWithoutRisingEvent = false;
            }

            SaveBtn_PDF_Mode_Changed?.Invoke();
        }

        private void cbMerge_CheckedChanged(object sender, EventArgs e)
        {
            SaveBtn_PDF_Mode_Changed?.Invoke();
        }

        //---------------------------------------------------------------------------------
        public event EventFunc SaveBtn_SaveClick;
		private void btnSave_Click(object sender, EventArgs e)
		{
			// Check if user wants to save to local or not. If so, check set path.
			if
			(
				(
					   _FormMode != DocumentSaveButtons_FormMode.AddIn
					|| !cbSaveLoc.Checked
					||  Utilities.CheckSavePath(txtBrowse.Text)
				)
				&&
				(SaveBtn_SaveClick != null)
			)
			{
				SaveBtn_SaveClick();
			}
		}

//---------------------------------------------------------------------------------
		public event EventFunc SaveBtn_CancelClick;
		private void btnCancel_Click(object sender, EventArgs e)
		{
			if(SaveBtn_CancelClick != null)
				SaveBtn_CancelClick();
		}

//---------------------------------------------------------------------------------
		// Save Local check box ON / OFF
		private void cbSaveLoc_CheckedChanged(object sender, EventArgs e)
		{
			btnBrowse.Enabled = cbSaveLoc.Checked;
		}

//---------------------------------------------------------------------------------
		private void btnBrowse_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog browser = new FolderBrowserDialog();

			browser.Description = lib.msg_required_doc_folder;

			if(txtBrowse.Text == "")
				browser.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			else
				browser.SelectedPath = txtBrowse.Text;

			if(browser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				txtBrowse.Text = browser.SelectedPath;
		}

//---------------------------------------------------------------------------------
		public void ChangeSaveMode(en_SaveMode val)
		{
			_SaveMode = val;
			gpAfterSave.Enabled = (SaveMode == en_SaveMode.NewDoc);
		}

//---------------------------------------------------------------------------------
		public void ChangeFormMode(DocumentSaveButtons_FormMode form, DocumentSaveButtons_ScanMode scan,string[] numberOfFiles = null)
		{
			_FormMode = form;
			_ScanMode = scan;
			_numberOfFiles = numberOfFiles;

            switch (_FormMode)
			{
			case DocumentSaveButtons_FormMode.Normal:
			case DocumentSaveButtons_FormMode.Delete:
				gpAfterSave.Visible = false;
				plSaveLocal.Visible = false;

				this.Height = plButtons.Height + chkPDF.Height;

				break;

			case DocumentSaveButtons_FormMode.ExtFile:
				gpAfterSave.Visible = true;
				plSaveLocal.Visible = false;
				break;

			case DocumentSaveButtons_FormMode.AddIn:
				gpAfterSave.Visible = false;
				plSaveLocal.Visible = true;
				break;
			}

            logger.Debug("SCAN:{0}, OCR:{1}", ScanMode, SpiderDocsApplication.CurrentUserGlobalSettings.ocr);

            if ((ScanMode != DocumentSaveButtons_ScanMode.Scan) || !SpiderDocsApplication.CurrentUserGlobalSettings.ocr)
			{
				chkPDF.Visible = cbMerge.Visible = false;
				//             this.Height -= chkPDF.Height;
				//             plSaveLocal.Top -= chkPDF.Height;
				//             gpAfterSave.Top -= chkPDF.Height;
				chkPDF.Checked = false;

            }

			 if( _numberOfFiles.Length == 1 || !chkPDF.Checked)
				cbMerge.Visible = cbMerge.Checked = false;

            chkPDF_CheckedChanged(this.chkPDF, new EventArgs());

            chkPDF.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            plSaveLocal.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			gpAfterSave.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
		}

		private void DocumentSaveButtons_Resize(object sender,EventArgs e)
		{
			int margin = new TextBox().Font.Height;

			this.SuspendLayout();
			rdMaintain.Left = rdChangeName.Width + rdChangeName.Left + margin;
			rdDeleteFile.Left = rdMaintain.Width + rdMaintain.Left + margin;
			this.ResumeLayout();
		}

//---------------------------------------------------------------------------------
	}
}
