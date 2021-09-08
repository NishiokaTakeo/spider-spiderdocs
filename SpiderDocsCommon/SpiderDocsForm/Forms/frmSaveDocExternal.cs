using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using Spider.Drawing;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
	public partial class frmSaveDocExternal : Spider.Forms.FormBase
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

//---------------------------------------------------------------------------------
		IconManager icons = new IconManager();
		public bool success;

		//Document CurrentDocument = new Document();
		DocumentProperty PropertyPanelValues = new DocumentProperty();

		List<string> paths = new List<string>();

		//PropertyPanel AttrPanel;
		DocumentSaveButtons SaveBtn;
		//DocumentSearch dtgVerSearch;
		TabControl tabControl;
		TabPage tabNewFile;
		//TabPage tabNewVersion;

		DocumentSaveButtons_FormMode FormMode;

        int DefaultFolderID { get; set; } = 0;
        readonly int m_userid = SpiderDocsApplication.CurrentUserId;
        readonly bool m_localDb = SpiderDocsApplication.CurrentServerSettings.localDb;

//---------------------------------------------------------------------------------

        /// <summary>
        /// Open from Add-ins.
        /// </summary>
        /// <param name="file_path"></param>
        /// <param name="mode"></param>
		public frmSaveDocExternal(string file_path, DocumentSaveButtons_FormMode mode) : base()
		{
            logger.Trace("Begin");

            CommonConstructor(new List<string>() { file_path }, mode);
		}

        /// <summary>
        /// Open from workspace, sendto.
        /// </summary>
        /// <param name="file_path"></param>
        /// <param name="mode"></param>
		public frmSaveDocExternal(List<string> file_path, DocumentSaveButtons_FormMode mode) : base()
		{
            logger.Trace("Begin");

            CommonConstructor(file_path, mode);
		}

		void CommonConstructor(List<string> file_path, DocumentSaveButtons_FormMode mode)
		{
            logger.Trace("Begin");
            
            // frmSaveDocExternal is called from multiple threads.
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-AU");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

            InitializeComponent();

			paths = file_path;
			FormMode = mode; //FormMode = DocumentSaveButtons_FormMode.AddIn;

             tabControl = _tabControl;
			tabNewFile = _tabNewFile;
			//tabNewVersion = _tabNewVersion;
			//AttrPanel = _AttrPanel;
			//dtgVerSearch = _dtgVerSearch;
			SaveBtn = _SaveBtn;
		}

//---------------------------------------------------------------------------------
		private void frmSaveDocExternal_Load(object sender, EventArgs e)
		{
            logger.Trace("Begin");

            DocumentSaveButtons_ScanMode ScanMode = DocumentSaveButtons_ScanMode.NoScan;
			foreach(string path in paths)
			{
				// Scan check box is enable if there is least one image file
				if(FileFolder.extensionsForScan.Contains(Path.GetExtension(path).ToLower()))
				{
					ScanMode = DocumentSaveButtons_ScanMode.Scan;
					break;
				}
			}

			build(FormMode, ScanMode);

			dtgNewDocList.build(paths);
			dtgNewDocList.SelectionChanged += dtgNewDocList_SelectionChanged;

			dtgNewVerList.build(paths);
            AttrPanel.Setup(mode : PropertyPanelNext.en_FormMode.Multiple, id_folder : DefaultFolderID, folder_per:en_Actions.CheckIn_Out);
            //AttrPanel.build(PropertyPanel.en_FormMode.Multiple);
			AttrPanel.SameAtb_CheckedChanged += SameAtb_CheckedChanged;

            SaveBtn_chkPDF_CheckedChanged();
        }


        void build(DocumentSaveButtons_FormMode FormMode, DocumentSaveButtons_ScanMode ScanMode)
		{
            logger.Trace("Begin");

            try
            {
				dtgVerSearch.build();
				dtgVerSearch.SearchDone += SearchDone;
				dtgVerSearch.SearchStart += VersionSearchStart;
				dtgVerSearch.VersionSearch_Click += VersionSearch_Click;

				AttrPanel.DocType_Changed += AttrPanel_DocTypeChanged;

				SaveBtn.SaveBtn_SaveClick += SaveBtn_SaveClick;
				SaveBtn.SaveBtn_CancelClick += SaveBtn_CancelClick;

				int old_Height = SaveBtn.Height;

                SaveBtn.ChangeFormMode(FormMode, ScanMode,paths.ToArray());
                SaveBtn.SaveBtn_PDF_Mode_Changed += SaveBtn_chkPDF_CheckedChanged;

                tabControl.Anchor &= ~AnchorStyles.Bottom;
				SaveBtn.Anchor &= ~AnchorStyles.Bottom;
				SaveBtn.Anchor |= AnchorStyles.Top;
				this.Height -= (old_Height - SaveBtn.Height);
				tabControl.Anchor |= AnchorStyles.Bottom;
				SaveBtn.Anchor |= AnchorStyles.Bottom;
				SaveBtn.Anchor &= ~AnchorStyles.Top;



                // Version up doesn't support more than one files.
                //if (paths.Count > 1 && tabControl.TabPages.Count > 1)
                //{
                //	tabControl.TabPages.RemoveAt(1);
                //}

                // if( paths.Count == 1)
                // 	SaveBtn.chkMerged.Visible = false;
            }
			catch(System.Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default_open_form + error.ToString(), lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}

//---------------------------------------------------------------------------------
		private void frmSaveDocExternal_Shown(object sender, EventArgs e)
		{
            logger.Trace("Begin");

            AttrPanel.Width = this.SaveNewFileContainer.Panel2.Width;

            this.BringToFront();

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Activate();

        }

        //---------------------------------------------------------------------------------
        protected void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
            logger.Trace("Begin");

            switch (tabControl.SelectedIndex)
			{
			case 0:
                    SaveBtn.SetNotificationGroupVisible(true);
                    SaveBtn.ChangeSaveMode(DocumentSaveButtons.en_SaveMode.NewDoc);
				break;

			case 1:
                SaveBtn.SetNotificationGroupVisible(false);

                SaveBtn.ChangeSaveMode(DocumentSaveButtons.en_SaveMode.NewVer);

				SearchExistingFiles();
                // if (SaveBtn.IsSavedAsPdf)
                // {
                //     dtgVerSearch.AutoVersionSearch(dtgNewVerList.DocumentList.FirstOrDefault()?.title_without_ext + ".pdf"); //AttrPanel.Title.Text//dtgVerSearch.AutoVersionSearch(AttrPanel.Title.Text + ".pdf");
                // }
                // else
                // {
                //     dtgVerSearch.AutoVersionSearch(dtgNewVerList.DocumentList.Select(a => a.title).ToList());
                // }
				break;
			}
		}

//---------------------------------------------------------------------------------
		void SaveBtn_chkPDF_CheckedChanged()
		{
            logger.Trace("Begin");

            dtgVerSearch.Clear();

            //Layout change
            if (SaveBtn.IsSavedAsPdf && SaveBtn.IsMerged
                || dtgNewDocList.getCheckedDocs().All(x => x.extension.ToLower() == ".pdf") && SaveBtn.IsMerged)
			{
				//CurrentDocument.extension = ".pdf";

				dtgNewDocList.ChangeFormMode(NewList.en_FormMode.Combine);
				dtgNewVerList.ChangeFormMode(NewList.en_FormMode.Combine);
				AttrPanel.ChangeFormMode(PropertyPanelNext.en_FormMode.Multiple_PDF);

				Document doc = dtgNewDocList.getCheckedDocs().FirstOrDefault();


                AttrPanel.Title.Text = doc.title_without_ext;

                //dtgVerSearch.AutoSrcDone = false;
            }
            else
			{
				//CurrentDocument.extension = null;

				dtgNewDocList.ChangeFormMode(NewList.en_FormMode.Normal);
				dtgNewVerList.ChangeFormMode(NewList.en_FormMode.Normal);
				AttrPanel.ChangeFormMode(PropertyPanelNext.en_FormMode.Multiple);
			}

            dtgVerSearch.AutoSrcDone = false;


            //var ocrable = dtgNewVerList.DocumentList.All(doc => FileFolder.CanOCR(doc.title));

            //if (SaveBtn.IsSavedAsPdf && ocrable == false)
            //{
            //    SaveBtn.SetSavedAsPdfWithoutRisingEvent = false;

            //    MessageBox.Show("All files must be OCRable. Will be automatically canceled.", lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    return;
            //}

            // Search Documents
             if (tabControl.SelectedIndex == 1)
             {
                SearchExistingFiles();
            }
        }

		/// <summary>
		/// Search importing as versionup files from DB. Will be versionup if they exists in database.
		/// </summary>
        void SearchExistingFiles()
        {
			var ocrable = dtgNewVerList.DocumentList.All( doc => FileFolder.CanOCR(doc.title) );

            //if (ocrable == false )
            //{
            //    SaveBtn.IsSavedAsPdf = false;
            //}

            if (ocrable && SaveBtn.IsSavedAsPdf)
			{

				if (SaveBtn.IsMerged)
				{
                    // Serch only first one with PDF.
					dtgVerSearch.AutoVersionSearch(dtgNewVerList.DocumentList.FirstOrDefault()?.title_without_ext + ".pdf");
				}
				else
				{
                    // Serch all with PDF.
                    dtgVerSearch.AutoVersionSearch(dtgNewVerList.DocumentList.Select(a => a.title_without_ext + ".pdf").ToList());
				}
			}
			else
			{
                // extension as it was.
				dtgVerSearch.AutoVersionSearch(dtgNewVerList.DocumentList.Select(a => a.title_without_ext + System.IO.Path.GetExtension(a.path)).ToList());
			}
        }

        //---------------------------------------------------------------------------------
        void dtgNewDocList_SelectionChanged(object sender, EventArgs e)
		{
            logger.Trace("Begin");

            if (dtgNewDocList.EndPopulate && !AttrPanel.IsSameAttribute)
			{
				dtgNewDocList.EndPopulate = false;

				ChgSelState();
				RstSelState();

				dtgNewDocList.EndPopulate = true;
			}
		}

//---------------------------------------------------------------------------------
		void AttrPanel_DocTypeChanged(object sender, EventArgs e)
		{
            logger.Trace("Begin");

            if (dtgNewDocList.EndPopulate && !AttrPanel.IsSameAttribute)
			{
				dtgNewDocList.EndPopulate = false;

				List<Document> docs = dtgNewDocList.getSelectedDocs();

				foreach(Document doc in docs)
					AttrPanel.GetPropertyVal(doc);

				dtgNewDocList.setDateToSelectedDocs();
				AttrPanel.PopulateAttrVal(dtgNewDocList.getCommonPropertyVals());

				dtgNewDocList.EndPopulate = true;
			}
		}

//---------------------------------------------------------------------------------
		void SameAtb_CheckedChanged(object sender, EventArgs e)
		{
            logger.Trace("Begin");

            dtgNewDocList.EndPopulate = false;

			if(((CheckBox)sender).Checked)
				ChgSelState();
			else
				RstSelState();

			dtgNewDocList.EndPopulate = true;
		}

//---------------------------------------------------------------------------------
		async void SaveBtn_SaveClick()
		{
            logger.Trace("Begin");

            bool ans = false;
			try{
				// Disable form controlls
				reverseEnableStatus();

				if(SaveBtn.SaveMode == DocumentSaveButtons.en_SaveMode.NewDoc)
					ans = await SaveDocument();
				else
					ans = await UpdateDocument();


				// If succeeded, do closing tasks.
				if (ans)
				{
					MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.GridUpdateCount);

						//success message
						MessageBox.Show(lib.msg_sucess_save_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);


					// Close this diaglog.
					Close(true);
				}
				else
				{
					//Close(true);

				}

			}
			catch(DocException ex)
			{
                logger.Error(ex);

				ex.Log();

				MessageBox.Show(ex.MyMessage, ex.MyTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Enable form controlls
                reverseEnableStatus();
            }
		}

//---------------------------------------------------------------------------------
		void SaveBtn_CancelClick()
		{
            logger.Trace("Begin");

            Close(false);
		}

//---------------------------------------------------------------------------------
		void VersionSearch_Click()
		{
            logger.Trace("Begin");

            if (SaveBtn.IsSavedAsPdf)
			{
				logger.Debug("Search File Name: {0}",AttrPanel.Title.Text + ".pdf");

				dtgVerSearch.VersionSearch(AttrPanel.Title.Text + ".pdf");

			}else
			{
                dtgNewVerList.Restore4dtgNewVersion();

                List<Document> ans = dtgNewVerList.getSelectedDocs();
				if(0 < ans.Count)
					dtgVerSearch.VersionSearch(ans[0].title);
			}
		}

//---------------------------------------------------------------------------------
		void VersionSearchStart()
		{
            logger.Trace("Begin");

            //disable components
            reverseEnableStatus();
		}

//---------------------------------------------------------------------------------
		void SearchDone(List<DTS_Document> searchFnc, bool AutoSrc)
		{
            logger.Trace("Begin");

            reverseEnableStatus();

            // Should auto search or result of search has count
            if (!AutoSrc || (searchFnc == null) || (SaveBtn.IsSavedAsPdf && SaveBtn.IsMerged)) return;

            // verionup with as it is
            if((0 < searchFnc.Count) && !SaveBtn.IsSavedAsPdf)
            {
                dtgNewVerList.Link2dtgNewVersion(searchFnc);    // populate results to 'dtgNewVersion'.

                List<string> UnassignedFiles = dtgNewVerList.GetUnassignedFileNames();
                if (0 < UnassignedFiles.Count)
                {
                    string filenames = String.Join("\n", UnassignedFiles);
                    string msg = String.Format(lib.msg_not_find_files_ext, filenames);

                    MessageBox.Show(msg, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                dtgNewVerList.Link2dtgNewVersion(searchFnc);    // populate results to 'dtgNewVersion'.

                List<string> UnassignedFiles = dtgNewVerList.GetUnassignedFileNames();
                if (0 < UnassignedFiles.Count)
                {
                    string filenames = String.Join("\n", UnassignedFiles);
                    string msg = String.Format(lib.msg_not_find_files_ext, filenames);

                    MessageBox.Show(msg, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

//---------------------------------------------------------------------------------
        public void SetDefaultFolderID(int folderid)
        {
            logger.Trace("Begin");

            DefaultFolderID = folderid;
            //AttrPanel.UpdateFolderList(folderid);
            //AttrPanel.m_cboFolder.SelectedValue = folderid;
        }

		async System.Threading.Tasks.Task<bool> SaveDocument()
		{
            logger.Trace("Begin");

            bool ans = false;

			ChgSelState();
			RstSelState();

			dtgNewDocList.ClearCellColor();
			if(!dtgNewDocList.validation(false, SaveBtn.IsSavedAsPdf))
				return false;

			List<Document> docs = dtgNewDocList.getCheckedDocs();
            List<string[]> removes = docs.Select(x => new string[] { x.title, x.path }).ToList();

            ans = CheckMandatryAndDuplication(docs.ToArray(), SaveBtn.IsSavedAsPdf, this.SaveBtn.IsMerged);

            if (SaveBtn.IsSavedAsPdf || this.SaveBtn.IsMerged)
			{
                //
                // Save document as PDF
                //

                if (ans)
                    ans = SavePDFAfterScan(docs);
			}else
			{
                //
                // Save document straightfoward(keeping same extension)
                //

				if(ans)
                    ans = SaveFile(docs);
			}

			if(ans)
			{
                //
                // Removed working files
                //

                var tasks = new System.Threading.Tasks.Task[removes.Count]; int i = 0;

                foreach (string[] doc in removes)
					tasks[i++] = AfterSaveAsync(doc[1], doc[0]);
                    //AfterSave(doc[1], doc[0]);

                await System.Threading.Tasks.Task.WhenAll(tasks);
			}

			return ans;
		}

        System.Threading.Tasks.Task<bool> AfterSaveAsync(string path, string title)
        {
            return System.Threading.Tasks.Task.Run(() => AfterSave(path, title));
        }

        bool CheckMandatryAndDuplication(Document[] docs, bool saveAsPDF,bool merge )
        {
            bool ans = false;

            if(merge)
            {
                ans = AttrPanel.IsAllFieldsEntered(".pdf", false);

            }
            else
            {
                if (docs.Count() <= 0)
                {
                    MessageBox.Show(lib.msg_no_file_selected, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                for (int i = 0; i < docs.Count(); i++)
                {
                    Document doc = docs[i];
                    DataGridViewRow row = dtgNewDocList.SelectChkedRow(i);
                    ans = AttrPanel.IsAllFieldsEntered(row.Cells["dtgFiles_Name"], saveAsPDF ? ".pdf" : doc.extension, false);

                    if (!ans) return false;
                }
            }

            return ans;
        }
        bool SaveFile(List<Document> docs)
        {
            bool ans = false;

            foreach (Document doc in docs)
            {
                if (!NoDuplicate(doc) || !doc.__WarnForDuplicate(true))
                {
                    return false;
                }
            }


            foreach (Document doc in docs)
            {
                ans = SaveDocument(doc);

                if (!ans)
                    break;
            }

            return ans;
        }

        bool SavePDFAfterScan(List<Document>  docs)
        {
            bool ans = false;
            DocumentProperty PropertyPanelValues = new DocumentProperty();
            AttrPanel.GetPropertyVal(PropertyPanelValues);

            Document doc = new Document();
            doc.PropertyCopy(PropertyPanelValues);
            doc.title = AttrPanel.Title.Text + ".pdf";

            // Save as one file (merge)
            if (this.SaveBtn.IsMerged)
            {
                ImgPDFSaver saver = new ImgPDFSaver(docs.Select(x => x.path).ToArray(), SaveBtn.IsSavedAsPdf);
                doc.path = saver.Merge();// OCRManager.GetPDFWithMerge(docs.Select(x => x.path).ToArray(), SaveBtn.IsSavedAsPdf);

                if (!doc.isNotDuplicated(true)) return false;
                if (!doc.__WarnForDuplicate(true)) return false;

                doc.AddDocument();
                DocumentNotificationGroupController.SaveOne(null, doc.id, this.SaveBtn.GetNotificationGroupID());

                ans = true;
            }
            else
            {
                /*
                 * Save separetely
                 *
                 */

                for (int i = 0; i < docs.Count; i++)
                {
                    var tmp = docs[i];

                    tmp.title = tmp.title_without_ext + ".pdf";

                    if (AttrPanel.IsSameAttribute)
                        tmp.PropertyCopy(AttrPanel.getCurrentProperty());

                    ImgPDFSaver saver = new ImgPDFSaver(new string[] { tmp.path }, SaveBtn.IsSavedAsPdf);
                    tmp.path = saver.Run().First();
                    //OCRManager ocr = new OCRManager(tmp.path, SaveBtn.IsSavedAsPdf);
                    //tmp.path = ocr.GetPDF();

                    if (!tmp.isNotDuplicated(true)) return false;
                    if (!tmp.__WarnForDuplicate(true)) return false;
                }

                docs.ForEach(d =>
                {
                    d.AddDocument();

                    DocumentNotificationGroupController.SaveOne(null, d.id, this.SaveBtn.GetNotificationGroupID());
                });

                ans = true;
            }

            return ans;
        }

        //---------------------------------------------------------------------------------
        bool SaveDocument(Document objDoc)
		{
            Cache.RemoveAll();

            logger.Trace("Begin");

            bool ans = false;

			if(AttrPanel.IsSameAttribute)
				objDoc.PropertyCopy(AttrPanel.getCurrentProperty());

			objDoc.id_event = EventIdController.GetEventId(en_Events.Created);
            //objDoc.id_notification_group = this.SaveBtn.GetNotificationGroupID();

            string error = objDoc.AddDocument();

			if(error == "")
			{
				ans = true;
                DocumentNotificationGroupController.SaveOne(null, objDoc.id, this.SaveBtn.GetNotificationGroupID());

			}else
			{
				//If error because the file is busy
				if(error.IndexOf("cannot access the file") != -1)
					MessageBox.Show(lib.msg_file_opened, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				else
					MessageBox.Show(lib.msg_error_save_file + lib.msg_error_description + error, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

				ans = false;
			}

			return ans;
		}

        bool NoDuplicate(Document objDoc)
        {
            logger.Trace("Begin");

            if (AttrPanel.IsSameAttribute)
                objDoc.PropertyCopy(AttrPanel.getCurrentProperty());

            objDoc.id_event = EventIdController.GetEventId(en_Events.Created);

            if (!objDoc.isNotDuplicated(true))
            {
                return false;
            }



			return true;
        }

        //---------------------------------------------------------------------------------
        async System.Threading.Tasks.Task<bool> UpdateDocument()
		{
            logger.Trace("Begin");

            await System.Threading.Tasks.Task.Yield();

            bool ans = validationNewVersion();

			// return if false valiation result
			if(!ans) return ans;

            //Document you want to import.
			List<Document> docs = dtgNewVerList.getCheckedDocs();

            // These files will be deleted after the task.
            var removes = docs.Select(x => new string[] { x.path, x.title }).ToArray();

            if (SaveBtn.IsMerged)
            // Merge file
            {

                Document selected = dtgVerSearch.SelectedDoc;

                if ( selected == null )
                {
                    throw new DocException(lib.msg_messabox_title, lib.msg_error_search_require);
                }

                selected = DocumentController.MergeMissingProperties(selected);

                ImgPDFSaver saver = new ImgPDFSaver(docs.Select(a => a.path).ToArray(), SaveBtn.IsSavedAsPdf);
                selected.path = saver.Merge();

                if (selected.id_status != en_file_Status.checked_out)
                {
                    selected.CheckOut(false, false, false);
                    selected.id_event = 0;
                }

                //selected.AddDocument();
                ans = selected.AddVersion() == string.Empty ;

                //ans = true;

                //            ScanDialog scan = new ScanDialog();
                //            scan.StartPdfConversion(selected, docs.Select(a => a.path).ToArray(), true);

                //ans = scan.Result;
            }
            else
            // Version up individually
            {


                // Error if a document you want to import could't find doc from database AND you didn't selected searched document
                Document selected = dtgVerSearch.SelectedDoc;

                if (
                    docs.Count == 0
                    || docs.First().id <= 0 && selected == null)
                {
                    throw new DocException(lib.msg_messabox_title, lib.msg_error_search_require);
                }

				string error = "";

				for(int i = 0; i < docs.Count; i++)
				{
					Document doc = docs[i];
                    string path = doc.path;

                    doc =  DocumentController.MergeMissingProperties(selected == null ? doc: selected);

                    if (doc.id == -1) { ans = false; break; }

                    // Conver to PDF
                    if(SaveBtn.IsSavedAsPdf)
                    {
                        //ImgPDFSaver saver = new ImgPDFSaver(docs.Select(a => a.path).ToArray(), SaveBtn.IsSavedAsPdf);
                        ImgPDFSaver saver = new ImgPDFSaver( new string[] { path }, SaveBtn.IsSavedAsPdf);
                        saver.Run(true).ForEach(file =>
                        {
                            doc.path = path = file;
                        });
                    }

                    doc.Load(path);

                    //save data
                    doc.reason = dtgVerSearch.ReasonTxt;

                    if (doc.id_status != en_file_Status.checked_out)
                    {
                        doc.CheckOut(false, false, false);
                        doc.id_event = 0;
                    }

                    error = doc.AddVersion();

                    if (!String.IsNullOrEmpty(error))
                    {
                        ans = false;
                        break;
                    }

					dtgNewVerList.DocumentList[i] = doc;
                }

				if(!ans)
					MessageBox.Show(lib.msg_error_save_file + "\n" + error, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			if(ans)
			{
				// foreach(var pathTitle in removes)
				// 	AfterSave(pathTitle[0], pathTitle[1]);

                var tasks = new System.Threading.Tasks.Task[removes.Length]; int i = 0;

                foreach(var pathTitle in removes)
					tasks[i++] = AfterSaveAsync(pathTitle[0], pathTitle[1]);
                    //AfterSave(doc[1], doc[0]);

                await System.Threading.Tasks.Task.WhenAll(tasks);
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		bool validationNewVersion()
		{
            logger.Trace("Begin");

            bool ans = false;
            List<Document> perCheck = new List<Document>();

            if (dtgVerSearch.chkReason())
            {

                // Merge PDF
                if(SaveBtn.IsMerged)
                {
                    //Merge option behaviur is same as save new. But importing file must be existed in spider docs.

                    if (false == Check4OCRable())
                    {
                        MessageBox.Show(lib.msg_not_all_ocrable, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }

                    if ( false == (dtgVerSearch.SelectedDoc != null))
                    {
                        MessageBox.Show(lib.msg_no_file_selected_NewVer, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }

                    return CheckNewVerHasPermission(new List<Document>() { dtgVerSearch.SelectedDoc });

                }

                // Save As PDF individually
                else if (SaveBtn.IsSavedAsPdf)
                {

                    if (false == Check4OCRable())
                    {
                        MessageBox.Show(lib.msg_not_all_ocrable, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return false;
                    }

                    //if ( false == CheckHasIDs(out perCheck))
                    //{
                    //    MessageBox.Show("All files must exist in spider docs", lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //    return false;
                    //}


                    return Check4NewVerCoomon();
                }

                // Save as normal
                else
                {
                    return Check4NewVerCoomon();
                }
            }
            else
            {
                // Error. Dialog is in the chkReason method.
            }

            return ans;
        }

        /// <summary>
        /// New Ver validation - check all files are OCRable
        /// </summary>
        /// <returns></returns>
        bool Check4OCRable()
        {
            if (dtgNewVerList.getCheckedDocs().All(x => FileFolder.CanOCR(x.title)))
            {

                return true;

            }
            else
            {
                //MessageBox.Show(lib.msg_not_all_ocrable, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        /// <summary>
        /// Show error dialog for new ver checking
        /// </summary>
        /// <param name="perCheck"></param>
        bool Check4NewVerCoomon()
        {
            bool ok = false;
            List<Document> perCheck = new List<Document>();

            if (dtgNewVerList.getCheckedDocs().Count() == 1)
            {
                //ok = CheckHasIDs(out perCheck) || dtgVerSearch.SelectedDoc != null;
                ok = false;
                // a document in search box has more priority than auto search.
                if (ok = dtgVerSearch.SelectedDoc != null)
                {
                    perCheck.Add(dtgVerSearch.SelectedDoc);
                }
                else if( ok = CheckHasIDs(out perCheck))
                {
                    // OK
                }
                else
                {
                    MessageBox.Show(lib.msg_no_file_selected_NewVer, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return ok = false;
                }

                //if (dtgNewVerList.getCheckedDocs().All(x => x.id > 0))
                //{
                //    perCheck = dtgNewVerList.getCheckedDocs();
                //    ok = true;
                //}
                //else
                //{
                //    if (dtgVerSearch.SelectedDoc != null)
                //    {
                //        perCheck.Add(dtgVerSearch.SelectedDoc);
                //        ok = true;
                //    }
                //    else
                //    {
                //        MessageBox.Show(lib.msg_no_file_selected_NewVer, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return false;
                //    }
                //}

            }
            else if (dtgNewVerList.getCheckedDocs().Count() > 1)
            {
                ok = CheckHasIDs(out perCheck);
                if(false == ok)
                {
                    MessageBox.Show(lib.msg_not_find_files, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return ok;
                }
            }

            //if (dtgNewVerList.getCheckedDocs().Count() > 1)
            //{
            //    if (dtgNewVerList.getCheckedDocs().All(x => x.id > 0))
            //    {
            //        perCheck = dtgNewVerList.getCheckedDocs();
            //        ok = true;
            //    }
            //    else
            //    {
            //        MessageBox.Show(lib.msg_not_find_files, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return false;
            //    }

            //}

            ok = CheckNewVerHasPermission(perCheck);

            //if (HasNewVerPermission(perCheck.ToArray()))
            //// one of them do not have to perform new ver task.
            //{
            //    ok = true;
            //}
            //else
            //{
            //    ok = false;

            //    if (dtgVerSearch.SelectedDoc.id_status == en_file_Status.checked_out) //check status
            //        MessageBox.Show(lib.msg_file_checkedOut, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    else
            //        MessageBox.Show(lib.msg_permissio_denied, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);



            //}

            return ok;
        }

        /// <summary>
        /// New Ver Validation - check all ids are filled.
        /// </summary>
        /// <param name="perCheck"></param>
        /// <returns></returns>
        bool CheckHasIDs(out List<Document> perCheck)
        {
            perCheck = new List<Document>();

            if (dtgNewVerList.getCheckedDocs().All(x => x.id > 0))
            {
                perCheck = dtgNewVerList.getCheckedDocs();
                return true;
            }
            else
            {
                //MessageBox.Show(lib.msg_not_find_files, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// New Ver validation - check permission
        /// </summary>
        /// <param name="perCheck"></param>
        /// <returns></returns>
        bool CheckNewVerHasPermission(List<Document> perCheck)
        {
            if (HasNewVerPermission(perCheck.ToArray()))
            // one of them do not have to perform new ver task.
            {
                return  true;
            }
            else
            {

                if (dtgVerSearch.SelectedDoc.id_status == en_file_Status.checked_out) //check status
                    MessageBox.Show(lib.msg_file_checkedOut, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(lib.msg_permissio_denied, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        /// <summary>
        /// Check if all of documents have right permission to new ver
        /// </summary>
        /// <param name="docs"></param>
        /// <returns></returns>
        bool HasNewVerPermission(Document[] docs)
        {
            bool has = docs.All(doc => doc.IsActionAllowed(en_Actions.CheckIn_Out));

            return has;
        }

//---------------------------------------------------------------------------------
		async System.Threading.Tasks.Task<bool> AfterSave(string path, string title)
		{
            logger.Trace("Begin");

            await System.Threading.Tasks.Task.Yield();

            // After save
            DocumentSaveButtons_FormMode FrmMode = SaveBtn.FormMode;
			if(FrmMode == DocumentSaveButtons_FormMode.ExtFile)
			{
				if(SaveBtn.IsChangeName)
					FileFolder.RenameFile(path, "DMS- " + Path.GetFileName(path));
				else if(SaveBtn.IsDeleteFile)
					FileFolder.DeleteFile(path);

			}else if(FrmMode == DocumentSaveButtons_FormMode.Delete)
			{
				//FileFolder.DeleteFile(path);
                var syncMgr = new Cache(SpiderDocsApplication.CurrentUserId).GetSyncMgr(FileFolder.GetUserFolder());//new WorkSpaceMgr(FileFolder.GetUserFolder());
                //syncMgr.Delete(new WorkSpaceFile(path, FileFolder.GetUserFolder())).Wait();
                await syncMgr.Delete(new WorkSpaceFile(path, FileFolder.GetUserFolder()));
            }

			if(SaveBtn.IsSaveLocal)
			{
				string SavePath = SaveBtn.SavePath;

				if(!String.IsNullOrEmpty(SavePath))
				{
					SavePath = SavePath + "\\" + title;
					File.Copy(path, SavePath);
				}
			}

            return true;
		}

//---------------------------------------------------------------------------------
		void ChgSelState()
		{
            logger.Trace("Begin");

            DocumentProperty wrk = new DocumentProperty();

			// save current values on the panel to wrk
			AttrPanel.GetPropertyVal(wrk);

			// compare if current values have been changed since when the document is selected
			if(!DocumentProperty.PropertyCompare(PropertyPanelValues, wrk))
				dtgNewDocList.setPropertyToSelectedDocs(wrk); // save current values to selected Document variable in an array in dtgNewDocList
		}

//---------------------------------------------------------------------------------
		void RstSelState()
		{
            logger.Trace("Begin");

            dtgNewDocList.GetSelectedTag();
			AttrPanel.PopulateAttrVal(dtgNewDocList.getCommonPropertyVals());

			AttrPanel.GetPropertyVal(PropertyPanelValues);
		}

        /// <summary>
        /// Enable / Disable controls.
        /// </summary>
		void reverseEnableStatus()
		{
            logger.Trace("Begin");

            tabControl.Enabled = !tabControl.Enabled;
			SaveBtn.Enabled = !SaveBtn.Enabled;
		}

//---------------------------------------------------------------------------------
		public List<Document> GetCheckedDocuments()
		{
            logger.Trace("Begin");

            if (SaveBtn.SaveMode == DocumentSaveButtons.en_SaveMode.NewDoc)
				return dtgNewDocList.getCheckedDocs();
			else
				return dtgNewVerList.getCheckedDocs();
		}

//---------------------------------------------------------------------------------
		void Close(bool ans)
		{
            logger.Trace("Begin");

            success = ans;
			base.Close();
		}

//---------------------------------------------------------------------------------
		// Save the current scale value
		// ScaleControl() is called during the Form's constructor
		private SizeF scale = new SizeF(1.0f, 1.0f);
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
            logger.Trace("Begin");

            scale = new SizeF(scale.Width * factor.Width, scale.Height * factor.Height);
			base.ScaleControl(factor, specified);
		}

		// Recursively search for SplitContainer controls
		private void Fix(Control c)
		{
            logger.Trace("Begin");

            foreach (Control child in c.Controls)
			{
				if (child is SplitContainer)
				{
					SplitContainer sp = (SplitContainer)child;
					Fix(sp);
					Fix(sp.Panel1);
					Fix(sp.Panel2);
				}
				else
				{
					Fix(child);
				}
			}
		}

		private void Fix(SplitContainer sp)
		{
            logger.Trace("Begin");

            // Scale factor depends on orientation
            float sc = (sp.Orientation == Orientation.Vertical) ? scale.Width : scale.Height;
			if (sp.FixedPanel == FixedPanel.Panel1)
			{
				sp.SplitterDistance = (int)Math.Round((float)sp.SplitterDistance * sc);
			}
			else if (sp.FixedPanel == FixedPanel.Panel2)
			{
				int cs = (sp.Orientation == Orientation.Vertical) ? sp.Panel2.ClientSize.Width :sp.Panel2.ClientSize.Height;
				int newcs = (int)((float)cs * sc);
				sp.SplitterDistance -= (newcs - cs);
			}
		}

	}
}
