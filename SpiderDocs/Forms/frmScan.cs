using System;
using System.Windows.Forms;
using System.Collections.Generic;
using SpiderDocsForms;
using SpiderDocsModule;
using Document = SpiderDocsForms.Document;
using lib = SpiderDocsModule.Library;
using NLog;
using SpiderDocsModule.Classes.Scanners;
using System.Linq;

//---------------------------------------------------------------------------------
namespace SpiderDocs
{
    public partial class frmScan : Spider.Forms.FormBase
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

		//SpiderDocsModule.ImgPDFSaver.en_status status = SpiderDocsModule.ImgPDFSaver.en_status.None;
		bool CtrlEnable = true;

        // Call Back to the UI thread
        delegate void CallBackFunc();
        delegate void CallBackFuncBool(bool arg);
        delegate void SClose();

        CallBackFuncBool Cb_changeStatusComponents;
        CallBackFunc Cb_updateProgress;

        ToolStripButton btnAbort;
        frmScannerController scannerCtr;

        //SpiderDocsForms.ImgPDFSaver Saver = new SpiderDocsForms.ImgPDFSaver();
        List<OCRManager> ocrs = new List<OCRManager>();
        static bool ShowedTwainErr = false;

        public bool scanEnable { get { return SpiderDocsModule.Classes.Scanners.Scanner.scanEnable(); } }

        DTS_DocumentType DA_DocumentType = new DTS_DocumentType(true);		
        List<DocumentAttribute> arrayAttribute = new List<DocumentAttribute>();

        //PropertyPanelBase tmp_PropertyPanel = new PropertyPanelBase();

//---------------------------------------------------------------------------------
        public frmScan()
        {
            InitializeComponent();

            //cboFolder.UseDataSource4AssignedMe();

            Cb_changeStatusComponents = changeStatusComponents;
            Cb_updateProgress = updateProgress;

            //Saver.onProgressUpdated += ImgPDFSaver_onUpdateState;
            //Saver.onCompleted += ImgPDFSaver_onCompleted;

            //Saver.onStartExtractImage += ImgPDFSaver_onStartExtractImage;
            //Saver.onRequestExtractedImageAdd += ImgPDFSaver_onRequestExtractedImageAdd;
            //Saver.onCompletedExtractImage += ImgPDFSaver_onCompletedExtractImage;
			//Saver.onBeforeSave += ImgPDFSaver_onBeforeSave;
   //         Saver.onCompletedConvertToPDF += ImgPDFSaver_onCompletedConvertToPDF;
   //         Saver.onCompletedSaveFile += ImgPDFSaver_onCompletedSaveFile;
            
            btnAbort = new ToolStripButton("Cancel", Properties.Resources.delete, btnAbort_ButtonClick);
            btnAbort.AutoSize = true;
            this.statusStrip.Items.Add(btnAbort);

            UpdateStatusStrip();
            updateProgress();

            this.PropertyPanel.tmp_cboNotificationGroup = this.cboNotificationGroup;

            //tmp_PropertyPanel.tmp_txtTitle = txtTitle;
            //tmp_PropertyPanel.tmp_cboFolder = cboFolder;
            //tmp_PropertyPanel.tmp_cboDocType = cboDocType;
            //tmp_PropertyPanel.tmp_uscAttribute = uscAttribute;
            //tmp_PropertyPanel.Initialize();
        }


		//---------------------------------------------------------------------------------
		void frmScan_Load(object sender, EventArgs e)
        {

            scannerCtr = new frmScannerController();
            //scannerCtr.OnStartLoadImage += Saver.StartExtractImageFromScanner;
            //scannerCtr.OnLoadingImage += Saver.UpdatedExtractImage;
            //scannerCtr.OnLoadedImage += Saver.RequestExtractedImageAdd;
            //scannerCtr.OnWorkDone += Saver.CompletedExtractImageFromScanner;
            scannerCtr.OnLoadedImage += ScannerCtr_OnLoadedImage;
            scannerCtr.OnWorkDone += ScannerCtr_OnWorkDone;

            //populate combos
            //populateComboFolder();
            populateComboDocType();

            ckSaveWorkSpace.Enabled = SpiderDocsApplication.CurrentPublicSettings.allow_workspace;
            
            if(Scanner.scanEnable() == false)
            {
                if(!ShowedTwainErr)
                {
                    MessageBox.Show(lib.msg_error_twain_driver, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ShowedTwainErr = true;
                }

                btnSelectScan.Enabled = false;
                btnScan.Enabled = false;
            }

            this.PropertyPanel.FolderFilterPermission = en_Actions.CheckIn_Out;
            this.PropertyPanel.Setup(mode: PropertyPanel.en_FormMode.Single);
        }

        private void ScannerCtr_OnLoadedImage(object filename)
        {
            //ocrs.Add(new OCRManager((string)filename));
            ImgPDFSaver_onRequestExtractedImageAdd((string)filename);
        }

        public void ScannerClose()
        {
            scannerCtr.Close();
        }

        private void ScannerCtr_OnWorkDone(IScanner scn)
        {
            if (scannerCtr.InvokeRequired)
                scannerCtr.Invoke(new SClose(ScannerClose));
            else
                scannerCtr.Close();
        }

        //---------------------------------------------------------------------------------
        private void frmScan_Shown(object sender,EventArgs e)
        {
            //int start_x = this.propertyPanel1.tmp_lblDocType.Left + this.propertyPanel1.tmp_lblDocType.Width + half_font_size;
            //int width = (this.propertyPanel1.tmp_uscAttribute.Left + this.propertyPanel1.tmp_uscAttribute.Width - half_font_size) - start_x;

            ////gpProperty.Height = imageBoxCtrl.Height - plSaveOptions.Height - half_font_size;

            //this.propertyPanel1.tmp_txtTitle.Left = start_x;
            //this.propertyPanel1.tmp_cboFolder.Left = start_x;
            //this.propertyPanel1.tmp_cboDocType.Left = start_x;

            //this.propertyPanel1.tmp_txtTitle.Width = width;
            //this.propertyPanel1.tmp_cboFolder.Width = width;
            //this.propertyPanel1.tmp_cboDocType.Width = width;			
            
            //// workaround for a Windows bug which cannot move the following controls properly when make this form maximized
            ckSaveSeparately.Left = half_font_size;
            ckSaveWorkSpace.Left = ckSaveSeparately.Left;
            cbSaveLocal.Left = ckSaveSeparately.Left;
        }

//---------------------------------------------------------------------------------
        private void frmScan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!CtrlEnable)
            {
                MessageBox.Show(lib.msg_error_close_scan, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

//@@Mori: Should be replaced with the property panel user control.
//---------------------------------------------------------------------------------
// Building document property panel ------------------------------------------------
//---------------------------------------------------------------------------------
        void cboDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(cboDocType.SelectedIndex > 0)
            //{
            //    uscAttribute.Enabled = true;
            //    uscAttribute.populateGrid(Convert.ToInt32(cboDocType.SelectedValue));

            //}else
            //{
            //    uscAttribute.Enabled = false;
            //    uscAttribute.ClearPanel();
            //}
        }


//---------------------------------------------------------------------------------
        void populateComboDocType()
        {
            DA_DocumentType.Select();
        }

//---------------------------------------------------------------------------------
// Open image files ---------------------------------------------------------------
//---------------------------------------------------------------------------------
        void btnSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                SpiderOpenFileDialog dialog = new SpiderOpenFileDialog();
                dialog.Multiselect = true;
                dialog.Filter =
                    "Image Files |*.BMP;*.JPG;*.GIF;*.PNG;*.TIF;";
                dialog.Title = "Select a image file";

                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    Array.Sort(dialog.FileNames);
                    //Saver.ExtractImage(dialog.FileNames);
                    foreach(string filename in dialog.FileNames)
                    {
                        //ocrs.Add(new OCRManager(filename));
                        ImgPDFSaver_onRequestExtractedImageAdd(filename);
                    }
                    
                }
            }
            catch(Exception error)
            {
                logger.Error(error);
            }
        }

//---------------------------------------------------------------------------------
// Scan images --------------------------------------------------------------------
//---------------------------------------------------------------------------------
        void btnSelectScan_Click(object sender, EventArgs e)
        {
            
            // Keep always latest
            using (frmScannerList frmList = new frmScannerList(Scanner.GetScanners(false), scannerCtr.iScanner)) { 

                frmList.ShowDialog();

                IScanner selected = frmList.ChosenScanner();

                scannerCtr.SetScanner(selected);
            }
        }

        //---------------------------------------------------------------------------------
        void btnScan_Click(object sender, EventArgs e)
        {
            if( scannerCtr.iScanner.CheckOnline())
            { 
                scannerCtr.ShowDialog();
            }
            else
            {
                //MessageBox.Show(lib.msg_error_scanner_ofline, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

//---------------------------------------------------------------------------------
// Save PDF files -----------------------------------------------------------------
//---------------------------------------------------------------------------------
        void btnSave_Click(object sender, EventArgs e)
        {
            bool ans = true;

            changeStatusComponents(true);

            ocrs.Clear();

            imageBoxCtrl.GetSelectedFilePaths().ForEach(path => ocrs.Add(new OCRManager(path)));

            // Check1: Number of selected files should be more than 0
            if (ocrs.Count == 0)
            {
                MessageBox.Show(lib.msg_no_page_selected, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ans = false;
            }
            
            // Check2: Validation
            if(ans && !PropertyPanel.IsAllFieldsEntered(".pdf", ckSaveWorkSpace.Checked))
            {
                ans = false;
            }

            // Check3: Check if user wants to save to local or not. If so, check set path.
            if(ans && !ckSaveWorkSpace.Checked && cbSaveLocal.Checked && !Utilities.CheckSavePath(txtBrowse.Text))
            {
                ans = false;
            }

            
            // Finally, Save a file
            if (ans)
            {
                string[] output = GetPDFs(ckSaveSeparately.Checked);

                if ( SaveScannedDocument(output))
                    ImgPDFSaver_onCompletedConvertToPDF();

                ImgPDFSaver_onCompleted(ans);
                
                //ConvertedPDFSaveMode mode = ConvertedPDFSaveMode.InsertDocument;

                //string SavePath = "";
                //if(ckSaveWorkSpace.Checked)
                //{
                //    mode = ConvertedPDFSaveMode.DoNotSaveToDB;
                //    SavePath = FileFolder.GetUserFolder();

                //}else if(cbSaveLocal.Checked)
                //{
                //    SavePath = txtBrowse.Text + "\\";

                //}else
                //{
                //    SavePath = FileFolder.TempPath;
                //}

                //doc.id_user = SpiderDocsApplication.CurrentUserId;
                //Saver.SaveFile(
                //    SpiderDocsApplication.CurrentUserId,
                //    SpiderDocsApplication.CurrentServerSettings.localDb,
                //    doc, 
                //    imageBoxCtrl.GetSelectedFilePaths(), 
                //    mode, 
                //    SavePath, 
                //    ckSaveSeparately.Checked, 
                //    SpiderDocsApplication.CurrentUserGlobalSettings.ocr);

                //int[] ids_group = PropertyPanel.tmp_cboNotificationGroup.getComboValue<DocumentAttributeCombo>().Select(a => a.id).ToArray();

                //DocumentNotificationGroupController.SaveOne(null, doc.id, ids_group);
            }
            else
            {
                changeStatusComponents(true);
            }
        }

        string[] GetPDFs(bool separetely)
        {
            try { 
                string[] output = new string[ocrs.Count];

                if (separetely)
                {
                    for (int i = 0; i < ocrs.Count; i++)
                        output[i] = ocrs[i].GetPDF();

                    return output;
                }
                else
                {
                    string merged = OCRManager.GetPDFWithMerge(
                                                    imageBoxCtrl.GetSelectedFilePaths().ToArray(), 
                                                    SpiderDocsApplication.CurrentUserGlobalSettings.ocr
                                    );

                    return new string[] { merged };

                }
            }catch(Exception ex)
            {
                logger.Error(ex);
            }

            return new string[] { };

        }
        /// <summary>
        /// Save scanned files
        /// </summary>
        /// <param name="output"></param>
        /// <returns>true:success</returns>
        bool SaveScannedDocument(string[] output)
        {
            bool ans = true;

            ImgPDFSaver.en_status status = ImgPDFSaver.en_status.Finish;

            arrayAttribute = PropertyPanel.tmp_uscAttribute.getAttributeValues();

            Document doc = new Document();
            List<string> paths = imageBoxCtrl.GetSelectedFilePaths();

            doc.title = PropertyPanel.tmp_txtTitle.Text + ".pdf";
            doc.id_folder = Convert.ToInt32(PropertyPanel.tmp_cboFolder.SelectedValue);
            doc.id_docType = Convert.ToInt32(PropertyPanel.tmp_cboDocType.SelectedValue);
            doc.id_event = EventIdController.GetEventId(en_Events.Scan);
            //doc.id_notification_group = Convert.ToInt32(PropertyPanel.tmp_cboNotificationGroup.SelectedValue);//
            doc.Attrs = arrayAttribute;
            
            string SavePath = "";
            if (ckSaveWorkSpace.Checked)
            {
                SavePath = FileFolder.GetUserFolder();
            }
            else
            {
                SavePath = cbSaveLocal.Checked ? txtBrowse.Text + "\\" : FileFolder.TempPath;

                // Pass if no duplication                
                if(hasDuplication(output,doc, out status))
                    return false;

                // Save
                if (status == ImgPDFSaver.en_status.Finish)
                {
                    foreach (var source in output)
                    {
                        doc.path = source;
                        doc.AddDocument();

                        int[] ids_group = PropertyPanel.tmp_cboNotificationGroup.getComboValue<DocumentAttributeCombo>().Select(a => a.id).ToArray();
                        DocumentNotificationGroupController.SaveOne(null, doc.id, ids_group);

                    }

                    ans = true;
                }
                else
                {

                    ans = false;
                }
            }
            
            ImgPDFSaver_onCompletedSaveFile(status);


            if (!ans) return ans;

            foreach (var source in output)
                System.IO.File.Move(source, SavePath + "\\" + System.IO.Path.GetFileName(source));

            return true;
        }

        /// <summary>
        /// Check if imprting documents will duplicate
        /// </summary>
        /// <param name="impr"></param>
        /// <param name="doc"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool hasDuplication(string[] impr, Document doc, out ImgPDFSaver.en_status status)
        {
            status = ImgPDFSaver.en_status.Finish;

            // Importing two or more
            if( impr.Count() > 1 )
            {                    
                var dupDeclare = DocumentAttributeController.GetIdListByDocType(doc.id_docType);
                var duplicateErr = dupDeclare.Exists(x => x.duplicate_chk);
                
                if( duplicateErr)
                {
                    status = ImgPDFSaver.en_status.Aborted;
                    MessageBox.Show(lib.msg_found_duplicate_docs, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return true;
            }

            if (!doc.isNotDuplicated(true) || !doc.__WarnForDuplicate(true))
            {
                status = ImgPDFSaver.en_status.Aborted;
                return true;
            }


            return false;
        }

//---------------------------------------------------------------------------------
// Callback functions -------------------------------------------------------------
//---------------------------------------------------------------------------------
        void ImgPDFSaver_onUpdateState()
        {
            if(this.Visible)
                Invoke(Cb_updateProgress);
        }

//---------------------------------------------------------------------------------
        void ImgPDFSaver_onStartExtractImage()
        {
            if(this.Visible)
            {
                Invoke(Cb_changeStatusComponents, false);
                Invoke(Cb_updateProgress);
            }
        }

//---------------------------------------------------------------------------------
        void ImgPDFSaver_onRequestExtractedImageAdd(string path)
        {
            if(this.Visible)
            {
                imageBoxCtrl.AddPicture(path);
                Invoke(Cb_updateProgress);

                ImgPDFSaver_onCompletedExtractImage();
            }
        }

//---------------------------------------------------------------------------------
        void ImgPDFSaver_onCompletedExtractImage()
        {
           if(this.Visible)
           {
               Invoke(Cb_updateProgress);
               Invoke(new CallBackFunc(addImg));
           }
        }
		//private void ImgPDFSaver_onBeforeSave(List<SpiderDocsModule.Document> saving, SpiderDocsModule.ImgPDFSaver.ImgPDFSaverArg args)
		//{
		//	foreach (var doc in saving)
		//	{
		//		if (!doc.isNotDuplicated(true))
		//		{
		//			System.Windows.Forms.MessageBox.Show(lib.msg_found_duplicate_docs, lib.msg_messabox_title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
		//			args.status = status = SpiderDocsModule.ImgPDFSaver.en_status.Aborting;
		//			args.errorThread = true;
		//			return;
		//		}

		//		if (doc.WarnForDupliate(true) )
		//		{
		//			if (MessageBox.Show(lib.msg_warn_existing_file, lib.msg_messabox_title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
		//			{
		//				args.status = status = SpiderDocsModule.ImgPDFSaver.en_status.Aborting;
		//				args.errorThread = true;
		//				return;
		//			}
		//			else
		//				doc.hasAccepted = true;
		//		}
		//	}
		//}

		void addImg()
        {
            int cnt = imageBoxCtrl.CommitAddPicture();

            //update pags count
            lblTotPagCount.Text = (Convert.ToInt32(lblTotPagCount.Text) + cnt).ToString();
            lblSelPagCount.Text = (Convert.ToInt32(lblSelPagCount.Text) + cnt).ToString();
        }

//---------------------------------------------------------------------------------
        void ImgPDFSaver_onCompletedConvertToPDF()
        {
            if(this.Visible)
            {
                Invoke(Cb_updateProgress);

                Invoke(new CallBackFunc(updateTotalPages));
                Invoke(new CallBackFunc(updateSelectedPages));
                imageBoxCtrl.imageBox.Image = null;
            }
        }

//---------------------------------------------------------------------------------
        void ImgPDFSaver_onCompletedSaveFile(ImgPDFSaver.en_status status)
        {
			if (status == ImgPDFSaver.en_status.Aborting)
			{
				MessageBox.Show(lib.msg_error_aborted, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

            if(this.Visible)
            {
                Invoke(Cb_updateProgress);
                Invoke(new CallBackFunc(removeCurrentImg));

                if(ckSaveWorkSpace.Checked)
                    MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);
                else
                    MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.GridUpdateCount);
            }

            MessageBox.Show(lib.msg_sucess_imported_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);

            updateTotalPages();
            updateSelectedPages();

            progressBar = new System.Windows.Forms.ToolStripProgressBar();
        }

        void removeCurrentImg()
        {
            imageBoxCtrl.RemoveSelectedFiles();
        }
            
//---------------------------------------------------------------------------------
        void ImgPDFSaver_onCompleted(bool ans)
        {
            Invoke(Cb_updateProgress);
            Invoke(Cb_changeStatusComponents, true); //components disable to enable 

            if(!ans)
                MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

//---------------------------------------------------------------------------------
// Save to local ------------------------------------------------------------------
//---------------------------------------------------------------------------------
        void ckSaveWorkSpace_CheckedChanged(object sender, EventArgs e)
        {
            
            if(ckSaveWorkSpace.Checked)
            {
                this.PropertyPanel.tmp_cboFolder.Enabled = false;
                this.PropertyPanel.tmp_cboDocType.Enabled = false;
                this.PropertyPanel.tmp_uscAttribute.Enabled = false;
            }
            else
            {
                this.PropertyPanel.tmp_cboFolder.Enabled = true;
                this.PropertyPanel.tmp_cboDocType.Enabled = true;

                if(this.PropertyPanel.tmp_cboDocType.SelectedIndex != 0)
                    this.PropertyPanel.tmp_uscAttribute.Enabled = true;
            }

            foreach(Control c in this.PropertyPanel.tmp_uscAttribute.Controls)
            {
                if(ckSaveWorkSpace.Checked)
                {
                    if(c.GetType() != typeof(Label))
                        c.Enabled = false;
                }
                else
                {
                    c.Enabled = true;
                }
            }

            LocSaveEnableChk();
        }

//---------------------------------------------------------------------------------
        void cbSaveLoc_CheckedChanged(object sender, EventArgs e)
        {
            LocSaveEnableChk();
        }

//---------------------------------------------------------------------------------
        void LocSaveEnableChk()
        {
            if(!ckSaveWorkSpace.Checked)
            {
                cbSaveLocal.Enabled = true;
                txtBrowse.Enabled = cbSaveLocal.Checked;
                btnBrowse.Enabled = cbSaveLocal.Checked;

            }else
            {
                cbSaveLocal.Enabled = false;
                txtBrowse.Enabled = false;
                btnBrowse.Enabled = false;
            }
        }

//---------------------------------------------------------------------------------
// Other Controls -----------------------------------------------------------------
//---------------------------------------------------------------------------------
        void btnSelect_Click(object sender, EventArgs e)
        {
            imageBoxCtrl.selectAllFiles();

            lblSelPagCount.Text = imageBoxCtrl.Count.ToString();
        }

//---------------------------------------------------------------------------------
        void btnDeselect_Click(object sender, EventArgs e)
        {
            imageBoxCtrl.desselectAllFiles();

            lblSelPagCount.Text = "0";  
        }

//---------------------------------------------------------------------------------
        void btnDelete_Click(object sender, EventArgs e)
        {
            if(lblSelPagCount.Text != "0")
            {
                var result = (MessageBox.Show("Are you sure you want to delete all selected file(s)?", "Spider Docs", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                if(result == DialogResult.Yes)
                    imageBoxCtrl.deletSelectedPag();

                //update pags count
                updateTotalPages();
                updateSelectedPages();
            }
        }

//---------------------------------------------------------------------------------
        void btnImgRotate_Click(object sender, EventArgs e)
        {
            try
            {
                if(imageBoxCtrl.Count != 0)
                {
                    imageBoxCtrl.ImageRotate();
                    imageBoxCtrl.imageBox.Refresh();
                }
            }
            catch {}
        }

//---------------------------------------------------------------------------------
        void txtTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Utilities.checkKeychar((int)e.KeyChar);
        }

//---------------------------------------------------------------------------------
        void btnBrowse_Click(object sender,EventArgs e)
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
        void btnAbort_ButtonClick(object sender, EventArgs e)
        {
            //Saver.Abort();
        }

//---------------------------------------------------------------------------------
        void frmScan_Resize(object sender, EventArgs e)
        {
            UpdateStatusStrip();
        }

//---------------------------------------------------------------------------------
        void frmScan_FormClosed(object sender,FormClosedEventArgs e)
        {
            imageBoxCtrl.clearImage();
            FileFolder.DeleteTempFiles("Scan_");
        }

//---------------------------------------------------------------------------------
// Functions ----------------------------------------------------------------------
//---------------------------------------------------------------------------------
        void updateProgress()
        {
            //lblProgressText.Text = Saver.StatusMessage;
            //progressBar.Value = Saver.ProgBarVal;
            //progressBar.Visible = Saver.ShowProgBar;
            //btnAbort.Visible = Saver.ShowAbort;
        }
    
//---------------------------------------------------------------------------------
        void updateTotalPages()
        {
            lblTotPagCount.Text = imageBoxCtrl.Count.ToString();
        }

//---------------------------------------------------------------------------------
        void updateSelectedPages()
        {
            lblSelPagCount.Text = imageBoxCtrl.GetSelectedPageCnt().ToString();
        }

//---------------------------------------------------------------------------------
        void UpdateStatusStrip()
        {
            int TotalWidth = 0;

            foreach(ToolStripItem item in statusStrip.Items)
            {
                if(item.Name != "lblSpace")
                    TotalWidth += item.Width;
            }

            lblSpace.Size = new System.Drawing.Size(this.Width - TotalWidth - 15, lblSpace.Height);
        }

//---------------------------------------------------------------------------------
        // Enable / Disable controls.
        void changeStatusComponents(bool all)
        {
            CtrlEnable = !CtrlEnable;
            
            toolStripMenu.Enabled = CtrlEnable;
            imageBoxCtrl.Enabled = CtrlEnable;

            if(all)
            {
                gpProperty.Enabled = CtrlEnable;
                ckSaveSeparately.Enabled = CtrlEnable;
                ckSaveWorkSpace.Enabled = CtrlEnable;
                
                if(ckSaveWorkSpace.Enabled)
                {
                    LocSaveEnableChk();

                }else
                {
                    cbSaveLocal.Enabled = false;
                    txtBrowse.Enabled = false;
                    btnBrowse.Enabled = false;
                }
            }
        }

//---------------------------------------------------------------------------------
    }
}
