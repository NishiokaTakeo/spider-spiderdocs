using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using Spider.IO;
using SpiderDocsForms;
using SpiderDocsModule;
using Document = SpiderDocsForms.Document;
using lib = SpiderDocsModule.Library;
using System.Collections;
using Spider.Drawing;
using System.ComponentModel;
using System.Threading.Tasks;
using Spider.Office;
using Spider.Types;
using System.Globalization;
using Spider.Common;
using NLog;
using SpiderDocs.Forms.WorkSpace;
using System.Threading;
using Spider.Net;
using tv = MultiSelectTreeview;
using ZetaLongPaths;

//---------------------------------------------------------------------------------
namespace SpiderDocs
{
    public partial class frmWorkSpace : Spider.Forms.FormBase
    {
		//---------------------------------------------------------------------------------
		readonly string strInvalid = "";

		//---------------------------------------------------------------------------------
		DTS_Document table = new DTS_Document();
		DTS_DocumentVersion table_version = new DTS_DocumentVersion();
		DTS_DocumentHistoric table_historic = new DTS_DocumentHistoric();

		dtgBd_SearchMode PrevSearchMode = dtgBd_SearchMode.RecentDocuments;
		public string queryKeyword = "";
		public bool endPopuletedGrid = false;
		public bool endPopuletedGridLocal = false;
		public bool endPopuletedGridVersion = false;
		public bool flagChangeName = false;
		public bool workOffline = false;
		public bool cancelDtgCompleted;
		delegate void threadFunction();
		List<TreeNode> Exp2LastSelected = new List<TreeNode>();
		bool DbSelected = false;
        //bool isFolderExpanding = false;
        DataTable dtSearch;
		delegate void updateGrid();
		FullFileSystemWatcher watcher;

		/// <summary>
		/// Last draged node
		/// </summary>
		TreeNode lastDragDestination = null;

		/// <summary>
		/// What time Last draged node is dragged.
		/// </summary>
		DateTime lastDragDestinationTime;

		static Logger logger = LogManager.GetCurrentClassLogger();

		// Akira Added.
		public enum en_XMLHeader
		{
			Id = 1,
			Keyword,
			Name,
			Folder,
			CreatedDate,
			Author,
			Extension,
			DocType,
			Review,
			Max
		}
		public string[,] m_arrXMLHeader = new string[,] {
															{ ((int)en_XMLHeader.Id).ToString(), "lblId", "c_id_doc" },
															{ ((int)en_XMLHeader.Keyword).ToString(), "lblKeyword", "" },
															{ ((int)en_XMLHeader.Name).ToString(), "lblTitle", "c_title" },
															{ ((int)en_XMLHeader.Folder).ToString(), "lblFolder", "c_folder" },
															{ ((int)en_XMLHeader.CreatedDate).ToString(), "lblDate", "c_date" },
															{ ((int)en_XMLHeader.Author).ToString(), "lblAuthor", "c_author" },
															{ ((int)en_XMLHeader.Extension).ToString(), "lblExtension", "c_extension" },
															{ ((int)en_XMLHeader.DocType).ToString(), "lblDocType", "c_type_desc" },
															{ ((int)en_XMLHeader.Review).ToString(), "lblReview", "c_id_review" }
														   };

        WorkSpaceMgr syncMgr;
        System.Threading.Tasks.Task<WorkSpaceMgr> _syncing;

        public delegate void FolderHanlder(object sender, FolderEventArgs e);
		//public event FolderHanlder OnFolderChanged;
		public class FolderEventArgs : EventArgs
		{

			public enum Proc
			{
				NONE = 0,
				ALL = 1 ,
				WITHOU_FOLDER = 2,
				FOLDER = 3
			}

			public Proc Instruction
			{
				get;set;
			}
			public FolderEventArgs(FolderEventArgs.Proc proc)
			{
				Instruction = proc;
			}
		}

		enum DragBy {
			TreeView = 1,
			GridView = 2,
			Explore = 3 ,
			Unknown = 99
		}

		TreeNode _lastSelectedFolderNode = null;

        protected FormUserControlTimer m_Timer;

		System.Timers.Timer _timerWorkSpaceRefresh;
		System.Timers.Timer _timerBusy;
        //---------------------------------------------------------------------------------
        // Form ---------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        public frmWorkSpace()
		{
            logger.Trace("Begin");

            Initialize();

        }

        void Initialize()
		{
			logger.Trace("Begin");
			try
			{
				InitializeComponent();
                this.attributeSearch.IncludeComboChildren = false;
				this.attributeSearch.Search = true;
                this.attributeSearch.populateGrid();
                this.attributeSearch.KeyDown += attributeSearch_KeyDown;
                dtBegin.LostFocus += new System.EventHandler(dt_LostFocus);
				dtEnd.LostFocus += new System.EventHandler(dt_LostFocus);
                cboDocType.CheckBoxCheckedChanged += cboDocType_CheckBoxCheckedChanged;
				//OnFolderChanged += frmWorkSpace_OnFolderChanged;
				// Add Folder watcher code.
				watcher = new FullFileSystemWatcher(FileFolder.GetUserFolder(), OnWatchFolderChanged);

                // Akira: LabelName
                //string strPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Spider Docs", "header.xml");
                //setHeader(strPath);


                m_Timer = new FormUserControlTimer(this);

				_timerWorkSpaceRefresh = new System.Timers.Timer(1000);
				_timerWorkSpaceRefresh.Elapsed += (sender, e) => Invoke(new Action(() => loadWorkSpaceFiles()));

				// _timerBusy = new System.Timers.Timer(300);
				// _timerBusy.Elapsed += (sernder )

            }
            catch (Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default_open_form, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
        }


		//---------------------------------------------------------------------------------
		private void setHeader(string strPath)
		{
			logger.Trace("Begin");

			XmlParserV2 xmlv2 = new XmlParserV2(strPath, "index", "column");
			if (xmlv2 != null)
			{
				for (int i = 0; i < (m_arrXMLHeader.Length / 3); i++)
				{
					string strIndex = m_arrXMLHeader[i, 0];
					string strLabelName = m_arrXMLHeader[i, 1];
					string strHeaderName = m_arrXMLHeader[i, 2];
					if (!string.IsNullOrEmpty(strLabelName))
					{
						Label lbl = this.Controls.Find(strLabelName, true).FirstOrDefault() as Label;
						lbl.Text = xmlv2.GetValueByIndex(strIndex, "value") + ":";
					}
					if (!string.IsNullOrEmpty(strHeaderName))
					{
						int iColumnIndex = 0;
						foreach (DataGridViewColumn field in dtgBdFiles.Columns)
						{
							if (field.Name == strHeaderName)
							{
								dtgBdFiles.Columns[iColumnIndex].HeaderText = xmlv2.GetValueByIndex(strIndex, "shortvalue");
								break;
							}
							iColumnIndex++;
						}
					}
				}
			}
		}
        //---------------------------------------------------------------------------------
        async private void frmWorkSpace_Load(object sender, EventArgs e)
		{
            //MessageBox.Show("ok", lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);

            logger.Trace("Begin");

            search(dtgBd_SearchMode.RecentDocuments);



            //cboFolder.UseDataSource4AssignedMe();

            int TabHeight = (int)(splitContainer.Size.Height * 0.2);
			tbDbFiles.Size = new Size(tbDbFiles.Size.Width, TabHeight);
			tbLocalFiles.Size = new Size(tbLocalFiles.Size.Width, TabHeight);
			lblLocalId.Text = strInvalid;

			int Panelw = (splitContainer.Size.Width - splitContainer.SplitterWidth) / 2;
			splitContainer.SplitterDistance = Panelw;

			splitContainer2.Anchor = AnchorStyles.None;
			splitContainer3.Anchor = AnchorStyles.None;

			splitContainer2.Size = new Size(Panelw, splitContainer.Size.Height);
			splitContainer2.Location = new Point(0, 0);
			splitContainer2.SplitterDistance = (int)(splitContainer2.Height * 0.8);
			tbDbFiles.Height = (int)((splitContainer2.Height - splitContainer2.SplitterDistance - splitContainer2.SplitterWidth) * 0.98);

			splitContainer3.Size = splitContainer2.Size;
			splitContainer3.Location = splitContainer2.Location;
			splitContainer3.SplitterDistance = splitContainer2.SplitterDistance;
			tbLocalFiles.Height = tbDbFiles.Height;

			splitContainer.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left);
			splitContainer2.Anchor = splitContainer.Anchor;
			splitContainer3.Anchor = splitContainer.Anchor;

			dtgVersions.AutoGenerateColumns = false;
			dtgVersions.DataSource = table_version.GetDataTable();

			dtgHist.AutoGenerateColumns = false;
			dtgHist.DataSource = table_historic.GetDataTable();

			int end_x;

			end_x = lblBdDeadline.Left + lblBdDeadline.Width;
			lblBdIdTitle.Left = end_x - lblBdIdTitle.Width;
			lblBdSizeTitle.Left = end_x - lblBdSizeTitle.Width;
			lblBdTypeTitle.Left = end_x - lblBdTypeTitle.Width;
			lblBdDeadline.Left = end_x - lblBdDeadline.Width;

			lblBdId.Left = end_x;
			lblBdSize.Left = end_x;
			lblBdType.Left = end_x;
			lblBdDeadlineVal.Left = end_x;

			end_x = lblBdCurrentVersionTitle.Left + lblBdCurrentVersionTitle.Width;
			//lblBdCreatedTitle.Left = end_x - lblBdCreatedTitle.Width;
			lblBdUpdatedTitle.Left = end_x - lblBdUpdatedTitle.Width;
			lblBdCurrentVersionTitle.Left = end_x - lblBdCurrentVersionTitle.Width;
			lblBdBy.Left = end_x - lblBdBy.Width;

			//lblBdCreated.Left = end_x;
			lblBdUpdated.Left = end_x;
			lblBdCurrentVersion.Left = end_x;
			lblBdByVal.Left = end_x;

			end_x = lblTypeTitle.Left + lblTypeTitle.Width;
			lblLocalIdTitle.Left = end_x - lblLocalIdTitle.Width;
			lblSizeTitle.Left = end_x - lblSizeTitle.Width;
			lblStatusTitle.Left = end_x - lblStatusTitle.Width;
			lblTypeTitle.Left = end_x - lblTypeTitle.Width;

			lblLocalId.Left = end_x;
			lblSize.Left = end_x;
			lblStatus.Left = end_x;
			lblType.Left = end_x;

			end_x = lblCreatedTitle.Left + lblCreatedTitle.Width;
			lblModifieldTitle.Left = end_x - lblModifieldTitle.Width;

			lblCreated.Left = end_x;
			lblModifield.Left = end_x;

			if (workOffline)
			{
				dtgBdFiles.Enabled = false;
				tbDbFiles.Enabled = false;
				panelLeft.Enabled = false;
				splitContainer.Panel1.Enabled = false;
				splitContainer.SplitterDistance = 0;
				splitContainer.IsSplitterFixed = true;

			}
			else
			{
				try
				{
                    // setup tabcontrol
                    //new System.Threading.Thread(loadTreeView).Start();
                    // Remove temporary folder exploere tab
                    //tabControlSearch.TabPages.Remove(tabPage3);

                    //populate combo attributes (custom grid)
                    populateComboAttribute();

					//load Explorer TreeView
					//loadTreeView();

					cboReview.SelectedIndex = 0;
				}
				catch (Exception error)
				{
					logger.Error(error);
					MessageBox.Show(lib.msg_error_default_open_form, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					Close();
				}
			}


			List<Control> ctrls = new List<Control>();
			Control[] tmp = null;

			tmp = new Control[tbProperties.Controls.Count];
			tbProperties.Controls.CopyTo(tmp, 0);
			ctrls.AddRange(tmp);

			tmp = new Control[tbDetailLocalFiles.Controls.Count];
			tbDetailLocalFiles.Controls.CopyTo(tmp, 0);
			ctrls.AddRange(tmp);

			ctrls.Add(lblBdName);
			ctrls.Add(lblSystemFile);
			ctrls.Add(lblName);
			ctrls.Add(lblLocalFile);

			foreach (Control wrk in ctrls)
			{
				if (wrk.GetType() == typeof(Label) && ((Label)wrk).Text.Contains("WW"))
					((Label)wrk).Text = "";
			}

            syncMgr = new Cache(SpiderDocsApplication.CurrentUserId).GetSyncMgr(FileFolder.GetUserFolder());//new WorkSpaceMgr(FileFolder.GetUserFolder());
            await syncMgr.Sync();
        }

		//---------------------------------------------------------------------------------
		private void frmWorkSpace_Shown(object sender, EventArgs e)
		{
			logger.Trace("Begin");

			loadUserCustomization();

            //checks if user has permission on at least one folder
            List<Folder> folder = PermissionController.GetAssignedFolderLevel2(id_parent: 0, user_id: SpiderDocsApplication.CurrentUserId, id_permission: en_Actions.OpenRead);

            if (folder.Count > 0)
			{
                /* Commented out
                 * If MMF.ReadData<uint>(MMF_Items.GridUpdateCount) is updated, then automatically start searching.
                 * On first load, if(LastGridUpdateCount <= MMF.ReadData<uint>(MMF_Items.GridUpdateCount)) logic is always true. LastGridUpdateCount == 0
                 * So search occurs twice.
                 * search(dtgBd_SearchMode.RecentDocuments);
                 */

                FileFolder.ConvertOldWorkSpaceFormat();

                loadWorkSpaceFiles();

			}
			else
			{
				btnSearch.Enabled = false;
			}

            this.m_Timer.OnGridUpdateRequestReceived += GridUpdateRequestReceived;
			this.m_Timer.OnWorkSpaceUpdateRequestReceived += WorkSpaceUpdateRequestReceived;
			this.m_Timer.OnDMSFileOpenRequestReceived += DMSFileOpenRequestReceived;
        	this.m_Timer.OnSendToReceived += ExternalImportStart;
            this.m_Timer.OnPreferenceChanged += PreferenceIsChanged;
            this.m_Timer.OnSyncWordSpace += M_Timer_OnSyncWordSpace;


            //syncMgr.Stop();


            //syncMgr.Start();

            logger.Trace("End");
		}

        private void PreferenceIsChanged(object arg)
        {
            UpdateWorkSpaceLayout();
        }

        private void UpdateWorkSpaceLayout()
        {
            treeViewFolderExplore.Height = this.tabPage3.Height - 58;
            btnFolderCreate.Visible = true;

            if (!SpiderDocsApplication.CurrentUserGlobalSettings.enable_folder_creation_by_user)
            {
                treeViewFolderExplore.Height = this.tabPage3.Height;
                btnFolderCreate.Visible = false;
            }
        }

        //---------------------------------------------------------------------------------
        private void frmWorkSpace_FormClosing(object sender, FormClosingEventArgs e)
		{
			logger.Trace("Begin");


            //this.m_Timer = null;
            this.m_Timer.Dispose();

        }

		//---------------------------------------------------------------------------------
		private void frmWorkSpace_Resize(object sender, EventArgs e)
		{
			logger.Trace("Begin");

			dtgBdFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dtgLocalFile.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		}

		//---------------------------------------------------------------------------------
		private void frmWorkSpace_KeyDown(object sender, KeyEventArgs e)
		{
			logger.Trace("Begin");

			if (e.KeyCode == Keys.Enter)
			{
				foreach (Control oControl in attributeSearch.AttributeControls)
				{
					if (oControl.Focused)
					{
						click_search_button();
						return;
					}
				}
			}

			if (e.KeyCode == Keys.Enter && (txtId.Focused || txtKeyWord.Focused || txtTitle.Focused || cboFolder.Focused || cboDocType.Focused || cboReview.Focused || cboAuthor.Focused || cboExtension.Focused || dtBegin.Focused || dtEnd.Focused))
			{
				click_search_button();
				return;
			}
		}

		//---------------------------------------------------------------------------------
		private void splitContainer1_Paint(object sender, PaintEventArgs e)
		{
			logger.Trace("Begin");

			SpliContainerDots(sender, e);
		}

		//---------------------------------------------------------------------------------
		private void splitContainer2_Paint(object sender, PaintEventArgs e)
		{
			logger.Trace("Begin");

			SpliContainerDots(sender, e);
		}

		//---------------------------------------------------------------------------------
		private void splitContainer3_Paint(object sender, PaintEventArgs e)
		{
			logger.Trace("Begin");

			SpliContainerDots(sender, e);
		}

		//---------------------------------------------------------------------------------
		private void SpliContainerDots(object sender, PaintEventArgs e)
		{
			logger.Trace("Begin");

			var control = sender as SplitContainer;
			//paint the three dots'
			Point[] points = new Point[6];
			var w = control.Width;
			var h = control.Height;
			var d = control.SplitterDistance;
			var sW = control.SplitterWidth;

			//calculate the position of the points'
			if (control.Orientation == Orientation.Horizontal)
			{
				points[0] = new Point((w / 2), d + (sW / 2));
				points[1] = new Point(points[0].X - 20, points[0].Y);
				points[2] = new Point(points[0].X - 10, points[0].Y);
				points[3] = new Point(points[0].X + 10, points[0].Y);
				points[4] = new Point(points[0].X + 20, points[0].Y);
				points[5] = new Point(points[0].X + 30, points[0].Y);
			}
			else
			{
				points[0] = new Point(d + (sW / 2), (h / 2));
				points[1] = new Point(points[0].X, points[0].Y - 20);
				points[2] = new Point(points[0].X, points[0].Y - 10);
				points[3] = new Point(points[0].X, points[0].Y + 10);
				points[4] = new Point(points[0].X, points[0].Y + 20);
				points[5] = new Point(points[0].X, points[0].Y + 30);
			}

			foreach (Point p in points)
			{
				p.Offset(-2, -2);
				e.Graphics.FillEllipse(SystemBrushes.ControlDark, new Rectangle(p, new Size(3, 3)));

				p.Offset(1, 1);
				e.Graphics.FillEllipse(SystemBrushes.ControlLight, new Rectangle(p, new Size(3, 3)));
			}
		}

		//---------------------------------------------------------------------------------
		private void GridUpdateRequestReceived(object e)
		{
			logger.Trace("Begin");

			search(dtgBd_SearchMode.RecentDocuments);
		}

		//---------------------------------------------------------------------------------
		private void WorkSpaceUpdateRequestReceived(object e)
		{
			logger.Trace("Begin");

			//loadWorkSpaceFiles();
		}

		//---------------------------------------------------------------------------------
		private void DMSFileOpenRequestReceived(object e)
		{
			logger.Trace("Begin");

			OpenDmsFile((string)e);
		}

		private void ExternalImportStart(object e)
		{
			logger.Trace("Begin");

			if( ! Directory.Exists(FileFolder.SendToPendingPath)) return;

			string[] pendingList = Directory.GetDirectories(FileFolder.SendToPendingPath);

			foreach(string pending in pendingList )
			{
                string dest = FileFolder.SendToOpeningPath + pending.Split('\\').Last();
                FileFolder.MoveDirectory(pending, dest);

                List<string> files =  Directory.GetFileSystemEntries(dest).ToList();

                if (files.Count == 0) continue;

                var thread = new Thread(() => Application.Run(new frmSaveDocExternal(files, DocumentSaveButtons_FormMode.ExtFile)));

				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
				thread.Join();
			}
		}

        //---------------------------------------------------------------------------------
        //System.Threading.Tasks.Task pending = null;

		private void OnWatchFolderChanged(object source, FileSystemEventArgs e)
		{
			logger.Trace("Begin");

            // await System.Threading.Tasks.Task.Yield();

            // if (pending != null) return;

            // pending = Task.Delay(1000);
             try
			 {
			// 	Invoke(new Action(() => loadWorkSpaceFiles()));

            //     pending = null;

				_timerWorkSpaceRefresh.Stop();
				_timerWorkSpaceRefresh.Start();
				_timerWorkSpaceRefresh.AutoReset = false;

			 }
			 catch { }
		}

		//---------------------------------------------------------------------------------
		//private void treeViewMSExplorer_AfterSelect(object sender, TreeViewEventArgs e)
		//{
		//	logger.Trace("Begin");

		//	RefreshByExplorer();
		//}



		private void btnRefreshWorkSpace_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");

			try
			{
				Cache.RemoveAll();

                this.dtgBdFiles.ClearSelection();

                Invoke(new Action(() => loadWorkSpaceFiles()));


                //Thread thread = null;
                //try
                //{
                //    thread = new Thread(() => syncMgr?.Sync(true) );
                //    //thread.SetApartmentState(ApartmentState.STA);
                //    Thread_TreeViewFolder.Priority = ThreadPriority.Lowest;
                //    thread.Start();

                //    //thread.Join();
                //}
                //catch (ThreadAbortException) { }
                //catch (Exception ex)
                //{
                //    logger.Error(ex);
                //    //thread?.Join();
                //}

                StartFolderExplr();
            }
            catch { }
		}

		//---------------------------------------------------------------------------------

		/*
		 * Work Space - Commom
		 */
		#region WorkSpace - Common
		//---------------------------------------------------------------------------------
		delegate void FunctionPtr();
		FunctionPtr AfterloadRecentFiles = null;
		ArrayList AfterloadRecentFiles_Args = new ArrayList();
		ArrayList AftersearchDms_Args = new ArrayList();

		bool DmsFileOpening = false;
		IconManager icon = new IconManager();

        //---------------------------------------------------------------------------------
        void ExportFiles(bool pdf, params Document[] docs)
		{
			logger.Trace("Begin");

            //var exprted = StartDownload2TempAsync(pdf, docs);
            Task<string[]> task = Task<string[]>.Run(() => Download2Temp(pdf, docs));
            //string[] paths = task.Result;

            string SelectedPath = "";

			if (1 < docs.Length)
			{
				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
					SelectedPath = folderBrowserDialog.SelectedPath + "\\";

			}
			else if (docs.Length == 1)
			{
				if (pdf)
				{
					ExportFileDialog.FileName = Path.GetFileNameWithoutExtension(docs[0].title) + ".pdf";
					ExportFileDialog.Filter = FileFolder.GetExtensionFilterString(en_FilterType.PDF, true);

				}
				else
				{
					ExportFileDialog.FileName = docs[0].title;
					ExportFileDialog.Filter = FileFolder.GetExtensionFilterString(docs[0].extension, true);
				}

				if (ExportFileDialog.ShowDialog() == DialogResult.OK)
					SelectedPath = ExportFileDialog.FileName;
			}

			if (!String.IsNullOrEmpty(SelectedPath))
			{
                //string[] paths = await exprted;
                string[] paths = task.Result;

                for ( int i = 0; i< docs.Count() ; i ++)
                {
                    Document doc = docs[i];
                    string path = paths[i];


                    if (string.IsNullOrWhiteSpace(path)) continue;


                    string savepath = FileFolder.GetPath(SelectedPath), filename = Path.GetFileName(SelectedPath);
                    if (String.IsNullOrEmpty(filename)) filename = doc.title;

                    string moveTo = savepath + filename;

                    if (File.Exists(moveTo)) File.Delete(moveTo);


                    File.Move(path, moveTo);

                }

                getHistoric();
			}

            logger.Trace("End");
        }


        /// <summary>
        /// Download Export files.
        /// </summary>
        /// <returns>Paths where files are</returns>
        private string[] Download2Temp(bool pdf, Document[] docs)
        {
			logger.Trace("Begin");

            //Thread.Sleep(2000);
            //await Task.Delay(5000);
            //return new string[] { "a" }.ToArray();
            List<string> exprted = new List<string>();

			//Permission is checked before process.
			//en_Actions action = pdf ? en_Actions.Export_PDF : en_Actions.Export;
			//Dictionary<int, Dictionary<en_Actions, en_FolderPermission>> permissions = PermissionController.GetFoldersPermissions(SpiderDocsApplication.CurrentUserId, action);

            foreach (Document doc in docs)
            {
				/*
                if (!permissions.ContainsKey(doc.id_folder) || !permissions[doc.id_folder].ContainsKey(action))
                {
                    exprted.Add("");
                    continue;
                }
				*/

                string TmpPath = FileFolder.YeildNewFileName(FileFolder.TempPath + doc.title);

				logger.Debug("{0} is downloading to {1}",doc.title,TmpPath);

				doc.Export(TmpPath);

				if (pdf)
				{
					string yeild = FileFolder.YeildNewFileName(TmpPath);

                    PDFConverter.pdfconversion(TmpPath, yeild);

					exprted.Add(yeild);
				}else
				{
					exprted.Add(TmpPath);
				}

            }

			logger.Trace("End");
			return exprted.ToArray();

        }

		//---------------------------------------------------------------------------------
		private void openDocReadonly(Document doc)
		{
			logger.Trace("Begin");

			if (doc.Open())
			{
				//refresh grid historic
				try
				{
					getHistoric();
				}
				catch (Exception error)
				{
					logger.Error(error);
				}

			}
			else
			{
				MessageBox.Show(lib.msg_permissio_denied, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		//---------------------------------------------------------------------------------
		private void saveNewFile(List<Document> docs)
		{
			logger.Trace("Begin");

			frmSaveDocExternal frm = new frmSaveDocExternal(docs.Select(a => a.path).ToList(), DocumentSaveButtons_FormMode.Delete);
			frm.ShowDialog();

			//load recent files
			if (frm.success)
			{
				//foreach (Document doc in docs)
				//{
				//	if (File.Exists(doc.path) && doc.path.Contains(FileFolder.GetUserFolder()))
				//		File.SetAttributes(doc.path, (File.GetAttributes(doc.path) | FileAttributes.Hidden | FileAttributes.ReadOnly));
				//}

				search(dtgBd_SearchMode.RecentDocuments);

                MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);
			}
		}

		//---------------------------------------------------------------------------------
		async private System.Threading.Tasks.Task<bool> saveNewVersion(List<Document> docs, bool refresh)
		{
			logger.Trace("Begin:{0}, {1}",docs.Count,refresh);

			bool ans = true;
			string descReason = "";

			docs = DocumentController.MergeMissingProperties(docs);

            foreach (var doc in docs)
            {
                var latest = DocumentController.GetDocument(doc.id);
                doc.id_latest_version = latest.id_latest_version;
                doc.version = latest.version;
            }

            if (SpiderDocsApplication.CurrentPublicSettings.reasonNewVersion)
			{
				frmReasonNewVersion form = new frmReasonNewVersion(docs);
				form.ShowDialog();
				ans = form.result;
				descReason = form.reason;
			}
            logger.Trace("Reason Answer:{0}",ans);

			if (ans)
			{
                try
                {
                    Task<bool>[] tasks = new Task<bool>[0];
                    int i = 0;
                    foreach (Document doc in docs)
				    {
                        if(logger.IsDebugEnabled) logger.Debug("doc:{0}", Newtonsoft.Json.JsonConvert.SerializeObject(doc));

						FileFolder.HideFolder(doc.ContainerFolder());

                        en_file_Status old_status = doc.id_status;
					    doc.reason = descReason;

                        string error = doc.AddVersion();
					    if (error == "")
					    {
						    string path = FileFolder.GetPath(doc.path);

						    if ((path.Contains(FileFolder.GetUserFolder())
						    && !String.IsNullOrEmpty(path.Replace(FileFolder.GetUserFolder(), "")))
						    || (path.Contains(FileFolder.TempPath)
						    && !String.IsNullOrEmpty(path.Replace(FileFolder.TempPath, ""))))
						    {
                                //Delete folder.

							    //bool result = FileFolder.DeleteFilesAndThisDir(FileFolder.GetPath(doc.path));

							    //if (!result && path.Contains(FileFolder.GetUserFolder()))
								   // File.SetAttributes(doc.path, (File.GetAttributes(doc.path) | FileAttributes.Hidden | FileAttributes.ReadOnly));

						    }
						    else
						    {
                                // Delete a file

							    //FileFolder.DeleteFile(doc.path);
						    }

						    //Check id this document is tracked and send email
						    if ((old_status != en_file_Status.checked_in)
						    && (doc.id_sp_status != en_file_Sp_Status.review)
						    && (doc.id_sp_status != en_file_Sp_Status.review_overdue))
						    {
							    doc.sendEmailFileTracked();
						    }
                            //await System.Threading.Tasks.Task.Delay(2500);

                            // Remove remote file.
                            var lfile = new WorkSpaceFile(doc.path, FileFolder.GetUserFolder());
                            if (lfile.LinkId > 0)
                            {
                                Task<bool>[] newTasks = new Task<bool>[tasks.Length + 1];
                                tasks.CopyTo(newTasks, 0);

                                newTasks[tasks.Length] = syncMgr.Delete(new WorkSpaceFile(doc.path, FileFolder.GetUserFolder()));
                                tasks = newTasks;
                            }

                        }
					    else
					    {
						    MessageBox.Show(lib.msg_error_default + lib.msg_error_description + error, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

						    ans = false;
					    }

						//FileFolder.HideFolder(doc.ContainerFolder());
				    }

                    //load recent files if necessary
                    if (refresh)
					    search(dtgBd_SearchMode.RecentDocuments);

                    await System.Threading.Tasks.Task.WhenAll(tasks);

                    MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);
                }
                catch(Exception ex)
                {
                    logger.Error(ex);
                }

            }

            logger.Trace("End");

            return ans;
		}

		//---------------------------------------------------------------------------------

		private void cancelCheckOut(Document doc)
		{
			logger.Trace("Begin");

			try
			{
				bool ok = doc.cancelCheckOut();

				if( ok )
				{
					//load recent file
					//loadRecentFileByDoc(doc.id);
					//if( IsListing(doc.id) )
					//	search(dtgBd_SearchMode.RecentDocuments);

					MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);
				}
			}
			catch(Exception error)
			{
				MessageBox.Show(lib.msg_error_default + lib.msg_error_description + error.Message, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//---------------------------------------------------------------------------------
		// Dms File -----------------------------------------------------------------------
		//---------------------------------------------------------------------------------
		void OpenDmsFile(string path)
		{
			logger.Trace("Begin");
			if (DmsFileOpening || !File.Exists(path))
				return;

			DmsFileOpening = true;

			int id;

			DmsFile dms = new DmsFile();
			dms.ReadDmsFile(path);

			if (int.TryParse(dms.GetVal(en_DmsFile.Id), out id))
			{
				if ((dtSearch != null) && !dtSearch.Rows.Contains(id))
				{
					AftersearchDms_Args.Clear();
					AftersearchDms_Args.Add(dms);
					AftersearchDms_Args.Add(id);

					Document doc = DocumentController.GetDocument(Convert.ToInt32(AftersearchDms_Args[1]));

					SearchCriteria criteria = new SearchCriteria();
					criteria.DocIds.Add(Convert.ToInt32(AftersearchDms_Args[1]));
					criteria.MergeType = en_SearchCriteriaMergeType.Top;
					table.Criteria.Clear();
					table.Criteria.Add(criteria);
					AfterloadRecentFiles = AfterSearchDms;

					search(dtgBd_SearchMode.Normal);

				}
				else
				{
					SelectDocumentByDmsFile(dms, id);
				}
			}
		}

		//---------------------------------------------------------------------------------
		void AfterSearchDms()
		{
			logger.Trace("Begin");
			SelectDocumentByDmsFile((DmsFile)AftersearchDms_Args[0], (int)AftersearchDms_Args[1]);
			AfterloadRecentFiles_Args.Clear();
		}

		//---------------------------------------------------------------------------------
		void SelectDocumentByDmsFile(DmsFile dms, int id)
		{
			logger.Trace("Begin");
			bool ans = true;

			Document doc = new Document();
			int RowIdx = GetRowIdxByDocId(id);

			if (RowIdx < 0)
			{
				ans = false;

			}
			else
			{
				doc = dtgBdFiles.GetDocument(RowIdx);
				dtgBdFiles.ClearSelection();

				dtgBdFiles.Rows[RowIdx].Selected = true;
				dtgBdFiles.CurrentCell = dtgBdFiles.Rows[RowIdx].Cells[0];
				dtgBdFiles.FirstDisplayedScrollingRowIndex = RowIdx;
				Invoke(new updateGrid(UpdateDataBaseGridView));
			}

			if (ans && int.TryParse(dms.GetVal(en_DmsFile.VersionId), out id))
			{
				if (doc.id_version != id)
				{
					tbDbFiles.SelectedTab = tbVersion;
					Invoke(new updateGrid(UpdateVersionGridView));
					RowIdx = GetVersionRowIdxById(id);

					if (0 < RowIdx)
					{
						dtgVersions.Rows[RowIdx].Selected = true;
						dtgVersions.FirstDisplayedScrollingRowIndex = RowIdx;

					}
					else
					{
						ans = false;
					}
				}
			}

			if (!ans)
				MessageBox.Show(lib.msg_not_find_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

			DmsFileOpening = false;
		}

		//---------------------------------------------------------------------------------
		// DataGridView -------------------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void clearDataGrid(DataGridView dtg)
		{
			logger.Trace("Begin");
			try
			{
				int i;
				int rows = dtg.Rows.Count;

				for (i = 0; i < rows; i++)
					dtg.Rows.Remove(dtg.Rows[0]);
			}
			catch (Exception error)
			{
				logger.Error(error);
			}
		}

		//---------------------------------------------------------------------------------
		private bool CheckMousePosdtg(DataGridView dtg, int selidx)
		{
			logger.Trace("Begin");
			bool ans = false;

			foreach (DataGridViewRow row in dtg.SelectedRows)
			{
				if (row.Index == selidx)
				{
					ans = true;
					break;
				}
			}

			return ans;
		}

		// //---------------------------------------------------------------------------------
		// // ETC ----------------------------------------------------------------------------
		// //---------------------------------------------------------------------------------
		// private void sendEmailFileTracked(Document doc)
		// {
		// 	logger.Trace("Begin");
		// 	List<string> arrayTracked = DocumentController.GetTrackingUserEmail(doc.id);

		// 	if (arrayTracked.Count > 0)
		// 	{
		// 		try
		// 		{
		// 			List<string> ToList = new List<string>();

		// 			if (SpiderDocsApplication.CurrentMailSettings.send)
		// 			{
		// 				for (int i = 0; i < arrayTracked.Count; i++)
		// 				{
		// 					string To = arrayTracked[i];
		// 					ToList.Add(To);

		// 				}

		// 				string Subject = String.Format(lib.msg_mail_available_Title, doc.title);
		// 				string Body = String.Format(lib.msg_mail_available_Body, doc.id, doc.title, doc.name_folder, doc.version);

		// 				DmsFile.MailDmsFile(doc, ToList, Subject, Body);

		// 				DocumentController.RemoveDocumentTracked(doc.id);
		// 			}
		// 		}
		// 		catch (Exception error)
		// 		{
		// 			logger.Error(error);
		// 		}
		// 	}
		// }

		//---------------------------------------------------------------------------------

		#endregion


		/*
		 * Work Space - Custom
		 */
		#region WorkSpace - Custom

		//---------------------------------------------------------------------------------

		/// <summary>
		/// Occurs when setting icon is cliecked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbCustomSearchFields_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			// Do nothing if Tab isn't attribute tab.
			if (tabControlSearch.SelectedTab != tabPage2) return;

			// Isn't in editing yet.
			if (panelCustomSearchFields.Visible == false)
			{
				// show search field when it isn't visible.
				loadSearchFields(true);
			}
			else
			{
				// is in editing.
				try
				{
					saveCustomSettings(false);
					loadSearchFields(false);
				}
				catch (Exception error)
				{
					logger.Error(error);
					MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		//---------------------------------------------------------------------------------
		// Database Grid Columns ----------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void ck_c_CheckedChanged(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			string col = "";
			CheckBox Target = (CheckBox)sender;

			if (Target == ck_c_id)
				col = "c_id_doc";
			else if (Target == ck_c_name)
				col = "c_title";
			else if (Target == ck_c_folder)
				col = "c_folder";
			else if (Target == ck_c_docType)
				col = "c_type_desc";
			else if (Target == ck_c_author)
				col = "c_author";
			else if (Target == ck_c_version)
				col = "c_version";
			else if (Target == ck_c_date)
				col = "c_date";
			else if (Target == ck_c_name)
				col = "c_id_doc";
			else if (Target == ck_c_name)
				col = "c_id_doc";
			else if (Target == ck_c_name)
				col = "c_id_doc";
			else if (Target == ck_c_name)
				col = "c_id_doc";
			else if (Target == ck_c_name)
				col = "c_id_doc";

			if (!Target.Checked)
				dtgBdFiles.Columns[col].Visible = false;
			else
				dtgBdFiles.Columns[col].Visible = true;
			/*

			 */
			//saveCustomSettings(false);
			//loadDbGridFiles();
		}

		//---------------------------------------------------------------------------------
		// Panel Controls -----------------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void ck_CheckedChanged(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			Control Target = null;
			Control Target2 = null;

			if (sender == ck_id)
				Target = txtId;
			else if (sender == ck_keyword)
				Target = txtKeyWord;
			else if (sender == ck_name)
				Target = txtTitle;
			else if (sender == ck_folder)
				Target = cboFolder;
			else if (sender == ck_date)
			{
				Target = dtBegin;
				Target2 = dtEnd;
			}
			else if (sender == ck_author)
				Target = cboAuthor;
			else if (sender == ck_extension)
				Target = cboExtension;
			else if (sender == ck_docType)
				Target = cboDocType;
			else if (sender == ck_Review)
				Target = cboReview;

			Target.Enabled = ((CheckBox)sender).Checked;
			if (Target2 != null)
				Target2.Enabled = ((CheckBox)sender).Checked;

			if (Target.GetType() == typeof(TextBox))
				((TextBox)Target).Text = "";
		}

		//---------------------------------------------------------------------------------
		// Save/Load ----------------------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void loadUserCustomization()
		{
			logger.Trace("Begin");
			//select attribute
			cboAttributes.SelectedValue = SpiderDocsApplication.WorkspaceCustomize.c_atb_id;

			//customization
			loadSearchFields(false);
			loadDbGridFiles();
			loadGridSize();
			cboFolder.DropDownWidth = SpiderDocsApplication.WorkspaceCustomize.cboFolder_width;
			//cboFolder.SetDropDownWidth(SpiderDocsApplication.WorkspaceCustomize.cboFolder_width); FolderExplorer Ammendement
			cboAuthor.SetDropDownWidth(SpiderDocsApplication.WorkspaceCustomize.cboAuthor_width);
			cboExtension.SetDropDownWidth(SpiderDocsApplication.WorkspaceCustomize.cboExtension_width);
			cboDocType.SetDropDownWidth(SpiderDocsApplication.WorkspaceCustomize.cboDocType_width);
			tabControlSearch.SelectedIndex = (int)SpiderDocsApplication.WorkspaceCustomize.OpenedPanel;

            /* these logic is wirrten in loadSearchFields(false); we do not have to call twice
			populateComboFolder();
			populateComboAuthor();
			populateComboExtension();
			populateComboDocType();*/

            //UpdateWorkSpaceLayout();

        }

		//---------------------------------------------------------------------------------
		private void loadGridSize()
		{
			logger.Trace("Begin");
			if (SpiderDocsApplication.WorkspaceGridsizeSettings.db_grid_full)
				splitContainer.SplitterDistance = splitContainer.Size.Width;
			else if (SpiderDocsApplication.WorkspaceGridsizeSettings.local_grid_full)
			{
				// work around - if only set to 0, the contents are not shown.
				splitContainer.SplitterDistance = 10;
				splitContainer.Refresh();
				splitContainer.SplitterDistance = 0;
				splitContainer.Refresh();
			}
			else if (0 < SpiderDocsApplication.WorkspaceGridsizeSettings.splitDistance)
				splitContainer.SplitterDistance = (int)(((float)SpiderDocsApplication.WorkspaceGridsizeSettings.splitDistance / 100) * splitContainer.Width);
		}

		//---------------------------------------------------------------------------------
		private void loadSearchFields(bool ShowCustomizePanel)
		{
			logger.Trace("Begin");
			bool Changed = (panelCustomSearchFields.Visible != ShowCustomizePanel);
			int x = lblDocType.Left + lblDocType.Width;
			int start_y = 10;
			int y = start_y;
			int InputBoxWidth = tabPage2.Width - x - font_size;

			flpCustomGrid.Visible = ShowCustomizePanel;
			btnSearch.Enabled = !ShowCustomizePanel;

			if (Changed)
			{
				if (ShowCustomizePanel)
				{
					pbCustomSearchFields.Image = Properties.Resources.salvar;
					dtgBdFiles.Size = new System.Drawing.Size(dtgBdFiles.Size.Width, dtgBdFiles.Size.Height - flpCustomGrid.Height);
					dtgBdFiles.Location = new System.Drawing.Point(dtgBdFiles.Location.X, dtgBdFiles.Location.Y + flpCustomGrid.Height);
					InputBoxWidth -= panelCustomSearchFields.Width;

				}
				else
				{
					pbCustomSearchFields.Image = Properties.Resources.icon_options;
					dtgBdFiles.Size = new System.Drawing.Size(dtgBdFiles.Size.Width, dtgBdFiles.Size.Height + flpCustomGrid.Height);
					dtgBdFiles.Location = new System.Drawing.Point(dtgBdFiles.Location.X, dtgBdFiles.Location.Y - flpCustomGrid.Height);
				}
			}

			y = loadSearchFields(txtId, lblId, ck_id, (ShowCustomizePanel || SpiderDocsApplication.WorkspaceCustomize.f_id), x, y, InputBoxWidth);
			y = loadSearchFields(txtKeyWord, lblKeyword, ck_keyword, (ShowCustomizePanel || SpiderDocsApplication.WorkspaceCustomize.f_keyword), x, y, InputBoxWidth);
			y = loadSearchFields(txtTitle, lblTitle, ck_name, (ShowCustomizePanel || SpiderDocsApplication.WorkspaceCustomize.f_name), x, y, InputBoxWidth);
			y = loadSearchFields(cboFolder, lblFolder, ck_folder, (ShowCustomizePanel || SpiderDocsApplication.WorkspaceCustomize.f_folder), x, y, InputBoxWidth);
			y = loadSearchFields(dtBegin, dtEnd, lblDate, ck_date, (ShowCustomizePanel || SpiderDocsApplication.WorkspaceCustomize.f_date), x, y, InputBoxWidth);
			y = loadSearchFields(cboAuthor, lblAuthor, ck_author, (ShowCustomizePanel || SpiderDocsApplication.WorkspaceCustomize.f_author), x, y, InputBoxWidth);
			y = loadSearchFields(cboExtension, lblExtension, ck_extension, (ShowCustomizePanel || SpiderDocsApplication.WorkspaceCustomize.f_extension), x, y, InputBoxWidth);
			y = loadSearchFields(cboDocType, lblDocType, ck_docType, (ShowCustomizePanel || SpiderDocsApplication.WorkspaceCustomize.f_docType), x, y, InputBoxWidth);
			y = loadSearchFields(cboReview, lblReview, ck_Review, (ShowCustomizePanel || SpiderDocsApplication.WorkspaceCustomize.f_Review), x, y, InputBoxWidth);

			if (SpiderDocsApplication.WorkspaceCustomize.f_folder)
				populateComboFolder();

			if (SpiderDocsApplication.WorkspaceCustomize.f_author)
				populateComboAuthor();

			if (SpiderDocsApplication.WorkspaceCustomize.f_extension)
				populateComboExtension();

			if (SpiderDocsApplication.WorkspaceCustomize.f_docType)
				populateComboDocType();

			//search fields
			ck_id.Checked = SpiderDocsApplication.WorkspaceCustomize.f_id;
			ck_keyword.Checked = SpiderDocsApplication.WorkspaceCustomize.f_keyword;
			ck_name.Checked = SpiderDocsApplication.WorkspaceCustomize.f_name;
			ck_folder.Checked = SpiderDocsApplication.WorkspaceCustomize.f_folder;
			ck_date.Checked = SpiderDocsApplication.WorkspaceCustomize.f_date;
			ck_author.Checked = SpiderDocsApplication.WorkspaceCustomize.f_author;
			ck_extension.Checked = SpiderDocsApplication.WorkspaceCustomize.f_extension;
			ck_docType.Checked = SpiderDocsApplication.WorkspaceCustomize.f_docType;
			ck_Review.Checked = SpiderDocsApplication.WorkspaceCustomize.f_Review;

			if (ShowCustomizePanel)
				tabControlSearch.Location = new System.Drawing.Point(3, panel6.Location.Y + panel6.Size.Height + 3);

			btnClear.Location = new System.Drawing.Point(168, y);

			y += 25;
			int diff = this.attributeSearch.Location.Y - y;
			attributeSearch.Location = new System.Drawing.Point(this.attributeSearch.Location.X, y);
			attributeSearch.Size = new Size(attributeSearch.Size.Width, attributeSearch.Size.Height + diff);

			panelCustomSearchFields.Location = new System.Drawing.Point(txtTitle.Location.X + txtTitle.Size.Width + 10, start_y);
			panelCustomSearchFields.Visible = ShowCustomizePanel;
		}

		// this is for date text boxes
		int loadSearchFields(Control dtBegin, Control dtEnd, Control Label, Control Checkbox, bool Enabled, int x, int y, int width)
		{
			logger.Trace("Begin");
			if (Enabled)
				dtEnd.Location = new System.Drawing.Point(x + dtBegin.Width + half_font_size, y);

			dtEnd.Visible = Enabled;
			dtEnd.Enabled = Enabled;

			y = loadSearchFields(dtBegin, Label, Checkbox, Enabled, x, y, dtBegin.Width);

			return y;
		}

		// this is for normal text boxes
		int loadSearchFields(Control Input, Control Label, Control Checkbox, bool Enabled, int x, int y, int width)
		{
			logger.Trace("Begin");
			if (Enabled)
			{
				Input.Location = new System.Drawing.Point(x, y);
				Input.Width = width;
				Label.Location = new System.Drawing.Point(3, y);
				Checkbox.Location = new System.Drawing.Point(Checkbox.Location.X, y - 5);
				y += 25;
			}

			Input.Visible = Enabled;
			Input.Enabled = Enabled;
			Label.Visible = Enabled;

			return y;
		}

		//---------------------------------------------------------------------------------
		private void loadDbGridFiles()
		{
			logger.Trace("Begin");
			ck_c_id.Checked = SpiderDocsApplication.WorkspaceCustomize.c_id;
			ck_c_name.Checked = SpiderDocsApplication.WorkspaceCustomize.c_name;
			ck_c_folder.Checked = SpiderDocsApplication.WorkspaceCustomize.c_folder;
			ck_c_docType.Checked = SpiderDocsApplication.WorkspaceCustomize.c_docType;
			ck_c_author.Checked = SpiderDocsApplication.WorkspaceCustomize.c_author;
			ck_c_version.Checked = SpiderDocsApplication.WorkspaceCustomize.c_version;
			ck_c_date.Checked = SpiderDocsApplication.WorkspaceCustomize.c_date;

			dtgBdFiles.Columns["c_id_doc"].Visible = SpiderDocsApplication.WorkspaceCustomize.c_id;
			dtgBdFiles.Columns["c_id_doc"].Width = SpiderDocsApplication.WorkspaceCustomize.c_id_width;
			dtgBdFiles.Columns["c_id_doc"].DisplayIndex = SpiderDocsApplication.WorkspaceCustomize.c_id_position;

			dtgBdFiles.Columns["c_mail_in_out_prefix"].Visible = (SpiderDocsApplication.WorkspaceCustomize.c_name && SpiderDocsApplication.WorkspaceCustomize.c_mail_in_out_prefix);
			dtgBdFiles.Columns["c_mail_in_out_prefix"].DisplayIndex = SpiderDocsApplication.WorkspaceCustomize.c_name_position - 1;

			dtgBdFiles.Columns["c_title"].Visible = SpiderDocsApplication.WorkspaceCustomize.c_name;
			dtgBdFiles.Columns["c_title"].Width = SpiderDocsApplication.WorkspaceCustomize.c_name_width;
			dtgBdFiles.Columns["c_title"].DisplayIndex = SpiderDocsApplication.WorkspaceCustomize.c_name_position;

			dtgBdFiles.Columns["c_mail_from"].Visible = SpiderDocsApplication.WorkspaceCustomize.c_mail_from;
			dtgBdFiles.Columns["c_mail_from"].Width = SpiderDocsApplication.WorkspaceCustomize.c_mail_from_width;
			dtgBdFiles.Columns["c_mail_from"].DisplayIndex = SpiderDocsApplication.WorkspaceCustomize.c_mail_from_position;

			dtgBdFiles.Columns["c_mail_to"].Visible = SpiderDocsApplication.WorkspaceCustomize.c_mail_to;
			dtgBdFiles.Columns["c_mail_to"].Width = SpiderDocsApplication.WorkspaceCustomize.c_mail_to_width;
			dtgBdFiles.Columns["c_mail_to"].DisplayIndex = SpiderDocsApplication.WorkspaceCustomize.c_mail_to_position;

			dtgBdFiles.Columns["c_mail_time"].Visible = SpiderDocsApplication.WorkspaceCustomize.c_mail_time;
			dtgBdFiles.Columns["c_mail_time"].Width = SpiderDocsApplication.WorkspaceCustomize.c_mail_time_width;
			dtgBdFiles.Columns["c_mail_time"].DisplayIndex = SpiderDocsApplication.WorkspaceCustomize.c_mail_time_position;

			dtgBdFiles.Columns["c_folder"].Visible = SpiderDocsApplication.WorkspaceCustomize.c_folder;
			dtgBdFiles.Columns["c_folder"].Width = SpiderDocsApplication.WorkspaceCustomize.c_folder_width;
			dtgBdFiles.Columns["c_folder"].DisplayIndex = SpiderDocsApplication.WorkspaceCustomize.c_folder_position;

			dtgBdFiles.Columns["c_type_desc"].Visible = SpiderDocsApplication.WorkspaceCustomize.c_docType;
			dtgBdFiles.Columns["c_type_desc"].Width = SpiderDocsApplication.WorkspaceCustomize.c_docType_width;
			dtgBdFiles.Columns["c_type_desc"].DisplayIndex = SpiderDocsApplication.WorkspaceCustomize.c_docType_position;

			dtgBdFiles.Columns["c_author"].Visible = SpiderDocsApplication.WorkspaceCustomize.c_author;
			dtgBdFiles.Columns["c_author"].Width = SpiderDocsApplication.WorkspaceCustomize.c_author_width;
			dtgBdFiles.Columns["c_author"].DisplayIndex = SpiderDocsApplication.WorkspaceCustomize.c_author_position;

			dtgBdFiles.Columns["c_version"].Visible = SpiderDocsApplication.WorkspaceCustomize.c_version;
			dtgBdFiles.Columns["c_version"].Width = SpiderDocsApplication.WorkspaceCustomize.c_version_width;
			dtgBdFiles.Columns["c_version"].DisplayIndex = SpiderDocsApplication.WorkspaceCustomize.c_version_position;

			dtgBdFiles.Columns["c_date"].Visible = SpiderDocsApplication.WorkspaceCustomize.c_date;
			dtgBdFiles.Columns["c_date"].Width = SpiderDocsApplication.WorkspaceCustomize.c_date_width;
			dtgBdFiles.Columns["c_date"].DisplayIndex = SpiderDocsApplication.WorkspaceCustomize.c_date_position;

			if (0 < SpiderDocsApplication.WorkspaceCustomize.c_atb_id)
			{
                var atr = DocumentAttributeController.GetAttribute(SpiderDocsApplication.WorkspaceCustomize.c_atb_id);

                logger.Debug("Custom Attr is {0},ID is {1}", atr.name, SpiderDocsApplication.WorkspaceCustomize.c_atb_id);

                dtgBdFiles.Columns["c_atb"].Visible = true;
				dtgBdFiles.Columns["c_atb"].HeaderText = atr.name;
				dtgBdFiles.Columns["c_atb"].Width = SpiderDocsApplication.WorkspaceCustomize.c_atb_width;
				dtgBdFiles.Columns["c_atb"].DisplayIndex = SpiderDocsApplication.WorkspaceCustomize.c_atb_position;

			}
			else
			{
				dtgBdFiles.Columns["c_atb"].Visible = false;
			}
		}

		//---------------------------------------------------------------------------------
		private void saveCustomSettings(bool do_search)
		{
			logger.Trace("Begin");
            try {
			    bool c_mail_in_out_prefix_current = SpiderDocsApplication.WorkspaceCustomize.c_mail_in_out_prefix;

			    SpiderDocsApplication.WorkspaceCustomize.f_id = ck_id.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.f_keyword = ck_keyword.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.f_name = ck_name.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.f_folder = ck_folder.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.f_date = ck_date.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.f_author = ck_author.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.f_extension = ck_extension.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.f_docType = ck_docType.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.f_Review = ck_Review.Checked;

			    SpiderDocsApplication.WorkspaceCustomize.c_id = ck_c_id.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.c_id_width = dtgBdFiles.Columns["c_id_doc"].Width;
			    SpiderDocsApplication.WorkspaceCustomize.c_id_position = dtgBdFiles.Columns["c_id_doc"].DisplayIndex;

			    SpiderDocsApplication.WorkspaceCustomize.c_mail_in_out_prefix = c_mail_in_out_prefix_current;

			    SpiderDocsApplication.WorkspaceCustomize.c_name = ck_c_name.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.c_name_width = dtgBdFiles.Columns["c_title"].Width;
			    SpiderDocsApplication.WorkspaceCustomize.c_name_position = dtgBdFiles.Columns["c_title"].DisplayIndex;

			    SpiderDocsApplication.WorkspaceCustomize.c_mail_from = dtgBdFiles.Columns["c_mail_from"].Visible;
			    SpiderDocsApplication.WorkspaceCustomize.c_mail_from_width = dtgBdFiles.Columns["c_mail_from"].Width;
			    SpiderDocsApplication.WorkspaceCustomize.c_mail_from_position = dtgBdFiles.Columns["c_mail_from"].DisplayIndex;

			    SpiderDocsApplication.WorkspaceCustomize.c_mail_to = dtgBdFiles.Columns["c_mail_to"].Visible;
			    SpiderDocsApplication.WorkspaceCustomize.c_mail_to_width = dtgBdFiles.Columns["c_mail_to"].Width;
			    SpiderDocsApplication.WorkspaceCustomize.c_mail_to_position = dtgBdFiles.Columns["c_mail_to"].DisplayIndex;

			    SpiderDocsApplication.WorkspaceCustomize.c_mail_time = dtgBdFiles.Columns["c_mail_time"].Visible;
			    SpiderDocsApplication.WorkspaceCustomize.c_mail_time_width = dtgBdFiles.Columns["c_mail_time"].Width;
			    SpiderDocsApplication.WorkspaceCustomize.c_mail_time_position = dtgBdFiles.Columns["c_mail_time"].DisplayIndex;

			    SpiderDocsApplication.WorkspaceCustomize.c_folder = ck_c_folder.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.c_folder_width = dtgBdFiles.Columns["c_folder"].Width;
			    SpiderDocsApplication.WorkspaceCustomize.c_folder_position = dtgBdFiles.Columns["c_folder"].DisplayIndex;

			    SpiderDocsApplication.WorkspaceCustomize.c_docType = ck_c_docType.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.c_docType_width = dtgBdFiles.Columns["c_type_desc"].Width;
			    SpiderDocsApplication.WorkspaceCustomize.c_docType_position = dtgBdFiles.Columns["c_type_desc"].DisplayIndex;

			    SpiderDocsApplication.WorkspaceCustomize.c_author = ck_c_author.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.c_author_width = dtgBdFiles.Columns["c_author"].Width;
			    SpiderDocsApplication.WorkspaceCustomize.c_author_position = dtgBdFiles.Columns["c_author"].DisplayIndex;

			    SpiderDocsApplication.WorkspaceCustomize.c_version = ck_c_version.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.c_version_width = dtgBdFiles.Columns["c_version"].Width;
			    SpiderDocsApplication.WorkspaceCustomize.c_version_position = dtgBdFiles.Columns["c_version"].DisplayIndex;

			    SpiderDocsApplication.WorkspaceCustomize.c_date = ck_c_date.Checked;
			    SpiderDocsApplication.WorkspaceCustomize.c_date_width = dtgBdFiles.Columns["c_date"].Width;
			    SpiderDocsApplication.WorkspaceCustomize.c_date_position = dtgBdFiles.Columns["c_date"].DisplayIndex;

			    if (cboAttributes?.SelectedIndex > 0)
			    {
				    int val = Convert.ToInt32(cboAttributes.SelectedValue);
				    if (SpiderDocsApplication.WorkspaceCustomize.c_atb_id != val)
					    do_search = true;

				    SpiderDocsApplication.WorkspaceCustomize.c_atb_id = val;
				    SpiderDocsApplication.WorkspaceCustomize.c_atb_width = dtgBdFiles.Columns["c_atb"].Width;
				    SpiderDocsApplication.WorkspaceCustomize.c_atb_position = dtgBdFiles.Columns["c_atb"].DisplayIndex;

			    }
			    else
			    {
				    SpiderDocsApplication.WorkspaceCustomize.c_atb_id = 0;
				    SpiderDocsApplication.WorkspaceCustomize.c_atb_width = 0;
				    SpiderDocsApplication.WorkspaceCustomize.c_atb_position = 0;
			    }

			    // SpiderDocsApplication.WorkspaceCustomize.cboFolder_width = cboFolder.DropDownControl.Width;FolderExplorer Ammendement
			    SpiderDocsApplication.WorkspaceCustomize.cboFolder_width = cboFolder.DropDownWidth;
			    //SpiderDocsApplication.WorkspaceCustomize.cboAuthor_width = cboAuthor.DropDownControl.Width;
			    SpiderDocsApplication.WorkspaceCustomize.cboAuthor_width = cboAuthor.DropDownControl.Width;
			    SpiderDocsApplication.WorkspaceCustomize.cboExtension_width = cboExtension.DropDownControl.Width;
			    SpiderDocsApplication.WorkspaceCustomize.cboDocType_width = cboDocType.DropDownControl.Width;

			    SpiderDocsApplication.WorkspaceCustomize.OpenedPanel = OpenedPanel.Explorer;// (OpenedPanel)tabControlSearch.SelectedIndex;

			    SpiderDocsApplication.WorkspaceCustomize.Save();


			    //save curret grids size
			    if (!SpiderDocsApplication.WorkspaceGridsizeSettings.db_grid_full && !SpiderDocsApplication.WorkspaceGridsizeSettings.local_grid_full)
				    SpiderDocsApplication.WorkspaceGridsizeSettings.splitDistance = (int)((((float)splitContainer.SplitterDistance / splitContainer.Width) * 100));

			    SpiderDocsApplication.WorkspaceGridsizeSettings.Save();


			    loadDbGridFiles();

			    if (do_search)
				    search(PrevSearchMode);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }

        }

		//---------------------------------------------------------------------------------

		#endregion


		/*
		 * Work Space - dtgBd
		 */
		#region WorkSpace dtgBd
		//---------------------------------------------------------------------------------
		bool dtgBdFiles_rightClick;
		int dtgBdFiles_ri;

		//---------------------------------------------------------------------------------
		// Events (DataGridView) ----------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void dtgBdFiles_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			dtgBdFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

		//---------------------------------------------------------------------------------
		private void dtgBdFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			logger.Trace("Begin");
			if (e.RowIndex > -1)
			{
				switch (SpiderDocsApplication.CurrentUserGlobalSettings.double_click)
				{
					case en_DoubleClickBehavior.OpenToRead:
					default:
						// Wait for preview as Office interops tend to close opening file
						if (!BsrPreview.busy)
							openDocReadonly(dtgBdFiles.GetSelectedDocument());
						break;

					case en_DoubleClickBehavior.CheckOut:
						CheckOutFile(false, true);
						break;

					case en_DoubleClickBehavior.CheckOutFooter:
						CheckOutFile(true, true);
						break;
				}
			}
		}

		//---------------------------------------------------------------------------------
		private void dtgBdFiles_SelectionChanged(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			DbSelected = true;

			getBdFileInformation();

			// Load preview only when the preview tab is selected.
			if (tbDbFiles.SelectedTab.Name == "tbPreview")
				ReloadDbPreview();
		}

		//---------------------------------------------------------------------------------
		private void dtgBdFiles_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			logger.Trace("Begin");
			try
			{
				if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right)
				{
					//get the position to show the menu
					Rectangle r = dtgBdFiles.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

					// Select single file or an unselected file.
					if ((dtgBdFiles.SelectedRows.Count == 1)
					|| (!CheckMousePosdtg(dtgBdFiles, e.RowIndex)))
					{
						//one line selected
						dtgBdFiles_rightClick = true;
						dtgBdFiles_ri = e.RowIndex;
						dtgBdFiles.CurrentCell = dtgBdFiles[e.ColumnIndex, e.RowIndex];
						dtgBdFiles.Rows[e.RowIndex].Selected = true;
						dtgBdFiles_rightClick = false;

						//----------hide/show menu--------------------------------------------------------------
						Document bd_file = dtgBdFiles.GetSelectedDocument();

                        Dictionary<en_Actions, en_FolderPermission>  permissions = PermissionController.GetFolderPermissions(bd_file.id_folder, SpiderDocsApplication.CurrentUserId);

                        //check out (edit)
                        menuDbCheckOut.Enabled = menuDbRename.Enabled = bd_file.IsActionAllowed(en_Actions.CheckIn_Out, SpiderDocsApplication.CurrentUserId, permissions);

						//check out with footer (edit)
						if (SpiderDocsApplication.CurrentPublicSettings.add_footer && (SpiderDocsApplication.CurrentPublicSettings.footer_menu == en_footer_menu.show_option))
						{
							menuDbCheckOutFooter.Enabled = bd_file.IsActionAllowed(en_Actions.CheckIn_Out_Foot, SpiderDocsApplication.CurrentUserId, permissions);

						}
						else
						{
							menuDbCheckOutFooter.Visible = false;
							menuDbCheckOutNoFooter.Visible = false;
						}

						//Import New Version
						menuDbImportNewVersion.Enabled = bd_file.IsActionAllowed(en_Actions.ImportNewVer, SpiderDocsApplication.CurrentUserId, permissions);

                        //Open (read only)
                        menuDbOpenRead.Enabled = bd_file.IsActionAllowed(en_Actions.OpenRead, SpiderDocsApplication.CurrentUserId, permissions);

						//Export
						menuDbExport.Enabled = bd_file.IsActionAllowed(en_Actions.Export, SpiderDocsApplication.CurrentUserId, permissions);

						//Export as PDF
						menuDbExportPdf.Enabled = bd_file.IsActionAllowed(en_Actions.Export_PDF, SpiderDocsApplication.CurrentUserId, permissions);

						//Send by email
						menuDbSendByEmail.Enabled = bd_file.IsActionAllowed(en_Actions.SendByEmail, SpiderDocsApplication.CurrentUserId, permissions);

						//Send by email as PDF
						menuDbSendByEmailPdf.Enabled = bd_file.IsActionAllowed(en_Actions.SendByEmail_PDF, SpiderDocsApplication.CurrentUserId, permissions);

						//Delete Document
						menuDbDelete.Enabled = bd_file.IsActionAllowed(en_Actions.Delete, SpiderDocsApplication.CurrentUserId, permissions);

						//Archive Document
						menuDbArchive.Enabled = bd_file.IsActionAllowed(en_Actions.Archive, SpiderDocsApplication.CurrentUserId, permissions);

						//Cancel Check Out
						menuDbCancelCheckOut.Enabled = bd_file.IsActionAllowed(en_Actions.CancelCheckOut, SpiderDocsApplication.CurrentUserId, permissions);

						//Review
						if (SpiderDocsApplication.CurrentServerSettings.localDb)
							menuDbReview.Visible = false;
						else
							menuDbReview.Enabled = bd_file.IsActionAllowed(en_Actions.Review, SpiderDocsApplication.CurrentUserId, permissions);
						//--------------------------------------------------------------------------------------

						//Property
						menuDbProperties.Enabled = true;

						menuDbGoToFolder.Enabled = true;

					}
					else
					{
						//multiple lines selected
						if (dtgBdFiles.SelectedRows.Count > 1)
						{
							//options available for multiple selection
							menuDbExport.Enabled = true;
							menuDbExportPdf.Enabled = (SpiderDocsApplication.IsExcel || SpiderDocsApplication.IsWord || SpiderDocsApplication.IsPowPnt);
							menuDbSendByEmail.Enabled = SpiderDocsApplication.IsOutlook;
							menuDbSendByEmailPdf.Enabled = (SpiderDocsApplication.IsExcel || SpiderDocsApplication.IsWord || SpiderDocsApplication.IsPowPnt) && SpiderDocsApplication.IsOutlook;

							menuDbCheckOut.Enabled = true;
							menuDbCheckOutFooter.Enabled = SpiderDocsApplication.IsWord;
							menuDbCancelCheckOut.Enabled = true;
							menuDbDelete.Enabled = true;
							menuDbArchive.Enabled = true;

							//not availables for multiple selection
							menuDbSaveAs.Enabled = false;
							menuDbImportNewVersion.Enabled = false;
							menuDbOpenRead.Enabled = false;
							menuDbReview.Enabled = false;
                            menuDbRename.Enabled = false;
                            //Properties is now able to selected after version 1.8.8
                            menuDbProperties.Enabled = true;

							menuDbGoToFolder.Enabled = false;
						}
					}

					menuDtgSystemFiles.Show((Control)sender, r.Left + e.X, r.Top + e.Y);
				}
			}
			catch (Exception error)
			{
				MessageBox.Show(lib.msg_error_file_menu, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
		}

		//---------------------------------------------------------------------------------
		bool dtgBdFiles_DataBindingComplete_Processing = false;
		private void dtgBdFiles_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			logger.Trace("Begin");
			if (!dtgBdFiles_DataBindingComplete_Processing && !cancelDtgCompleted)
			{
				dtgBdFiles_DataBindingComplete_Processing = true;

				// dtg
				try
				{
					endPopuletedGridVersion = true;
					loadIconsDtgBase();

					if (dtgBdFiles.Rows.Count == 0)
					{
						clearDataGrid(dtgHist);
						clearDataGrid(dtgVersions);
						//lblBdCreated.Text = "";
						lblBdCurrentVersion.Text = "";
						lblBdId.Text = "";
						lblBdUpdated.Text = "";
						lblBdName.Text = "";
						lblBdSize.Text = "";
						lblBdType.Text = "";
						pictureBoxBd.Visible = false;

					}
					else
					{
						dtgBdFiles_ri = 0;
						dtgBdFiles_rightClick = true;
						getBdFileInformation();
					}

					foreach (DataGridViewRow row in dtgBdFiles.Rows)
						UpdateRowAppearance(row);

					lblSystemFile.Text = "Files: " + dtgBdFiles.Rows.Count;
				}
				catch (Exception error)
				{
					logger.Error(error);
				}

				dtgBdFiles_DataBindingComplete_Processing = false;
			}

			cancelDtgCompleted = false;
		}

		//---------------------------------------------------------------------------------
		private void dtgBdFiles_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
		{
			//logger.Trace("Begin");
			if (e.RowIndex < 0)
				return;

			e.ToolTipText = "";
			Document doc = dtgBdFiles.GetDocument(e.RowIndex);

			switch (doc.id_status)
			{
				case en_file_Status.checked_out:
					if (0 < doc.id_checkout_user)
					{
						User user = UserController.GetUser(false, doc.id_checkout_user);
						e.ToolTipText += string.Format("Checked out by {0} {1}",user.name,user.active ? "":"( REMOVED USER )");
					}
					break;
			}
		}

		//---------------------------------------------------------------------------------
		private void dtgBdFiles_MouseUp(object sender, MouseEventArgs e)
		{
			logger.Trace("Begin");
			if (dtgBdFiles.DraggingFromGrid)
				getHistoric();
		}

		//---------------------------------------------------------------------------------
		// Events (Menu) ------------------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void menuDbSendByEmail_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			SendByEmail(false);
		}

		//---------------------------------------------------------------------------------
		private void menuDbSendByEmailPdf_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			SendByEmail(true);
		}

		//---------------------------------------------------------------------------------
		private void menuDbSendByEmailDms_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			DmsFile.MailDmsFile(dtgBdFiles.GetSelectedDocuments(), "", "");
		}

		//---------------------------------------------------------------------------------
		private void menuDbExport_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			ExportFiles(false, dtgBdFiles.GetSelectedDocuments().ToArray());
		}

		//---------------------------------------------------------------------------------
		private void menuDbExportPdf_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			ExportFiles(true, dtgBdFiles.GetSelectedDocuments().ToArray());
		}

		//---------------------------------------------------------------------------------
		private void menuDbCheckOut_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			if (!SpiderDocsApplication.CurrentPublicSettings.add_footer)
				CheckOutFile(false, false);
			else if (SpiderDocsApplication.CurrentPublicSettings.footer_menu == en_footer_menu.withFooter)
				CheckOutFile(true, false);
		}

		//---------------------------------------------------------------------------------
		private void menuDbCheckOutFooter_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			CheckOutFile(true, false);
		}

		//---------------------------------------------------------------------------------
		private void menuDbCheckOutWithoutFooter_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			CheckOutFile(false, false);
		}

		//---------------------------------------------------------------------------------
		private void menuDbDmsFile_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			DmsFile.SaveDmsFile(dtgBdFiles.GetSelectedDocuments(), "");
		}

		//---------------------------------------------------------------------------------
		private void menuDbReview_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			Document doc = dtgBdFiles.GetSelectedDocument();
			frmReview frm = new frmReview(doc.id);

			DialogResult ans = frm.ShowDialog();

			if (ans == System.Windows.Forms.DialogResult.OK)
				dtgBdFiles.SelectedRows[0].Cells["c_id_review"].Value = doc.id_review;

			switch (doc.id_status)
			{
				case en_file_Status.checked_out:
					dtgBdFiles.SelectedRows[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
					dtgBdFiles.SelectedRows[0].DefaultCellStyle.ForeColor = Color.Gray;
					dtgBdFiles.SelectedRows[0].Cells["c_id_status"].Value = en_file_Status.checked_out;
					dtgBdFiles.SelectedRows[0].Cells["c_id_checkout_user"].Value = SpiderDocsApplication.CurrentUserId;
					break;

				default:
					switch (doc.id_sp_status)
					{
						case en_file_Sp_Status.review:
							dtgBdFiles.SelectedRows[0].DefaultCellStyle.BackColor = Color.Lime;
							dtgBdFiles.SelectedRows[0].Cells["c_id_sp_status"].Value = en_file_Sp_Status.review;
							break;

						case en_file_Sp_Status.review_overdue:
							dtgBdFiles.SelectedRows[0].DefaultCellStyle.BackColor = Color.Yellow;
							dtgBdFiles.SelectedRows[0].Cells["c_id_sp_status"].Value = en_file_Sp_Status.review_overdue;
							break;

						default:
							dtgBdFiles.SelectedRows[0].DefaultCellStyle.BackColor = Color.White;
							dtgBdFiles.SelectedRows[0].Cells["c_id_sp_status"].Value = en_file_Sp_Status.normal;
							break;
					}
					break;
			}

			//refresh grid historic
			try
			{
				getHistoric();
			}
			catch (Exception error)
			{
				logger.Error(error);
			}

			getBdFileInformation();
		}

		//---------------------------------------------------------------------------------
		private void menuDbOpenRead_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			openDocReadonly(dtgBdFiles.GetSelectedDocument());
		}

		//---------------------------------------------------------------------------------
		private void menuDbProperties_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			frmFileProperties frm;


			//change dtgBdFiles.GetSelectedDocument to dtgBdFiles.GetSelectedDocuments so that you can get multiple files.
			List<Document> docs = dtgBdFiles.GetSelectedDocuments();

			//pass the docs list to constractor
			if (docs.Count == 1)
				frm = new frmFileProperties(docs[0].id);
			else
				frm = new frmFileProperties(docs.Select(a => a.id).ToArray());

            //frm.SetComboCache(this.attributeSearch.ControlCaches);

            //Document doc = dtgBdFiles.GetSelectedDocument();
            //frmFileProperties frm = new frmFileProperties(doc.id);
            frm.Icon = Icon;

			if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				//loadTreeView();
                //StartRegacyExplr();

                search(dtgBd_SearchMode.Normal);
			}
		}

		//---------------------------------------------------------------------------------
		private void menuDbDelete_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			frmDeleteFile frm = new frmDeleteFile();
			bool ans = false;

			foreach (DataGridViewRow row in dtgBdFiles.SelectedRows)
			{
				Document doc = dtgBdFiles.GetDocument(row.Index);

				doc = DocumentController.GetDocument(doc.id);
				if (doc.IsActionAllowed(en_Actions.Delete))
				{
					frm.AddDeleteFile(doc, row.Index);
					ans = true;
				}
			}

			if (ans)
			{
				frm.parent = this;
				frm.ShowDialog();
			}
		}

		//---------------------------------------------------------------------------------
		private void menuDbArchive_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			var result = (MessageBox.Show("Are you sure you want to archive this file? \n \n Once archived the document never could be changed again.", "Spider Docs", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

			if (result == DialogResult.Yes)
			{
				foreach (DataGridViewRow row in dtgBdFiles.SelectedRows)
				{
					Document doc = dtgBdFiles.GetDocument(row.Index);
					doc = DocumentController.GetDocument(doc.id);

					if (doc.IsActionAllowed(en_Actions.Archive))
					{
						SqlOperation sql = new SqlOperation();

						try
						{
							if ((doc.id_sp_status == en_file_Sp_Status.review)
							|| (doc.id_sp_status == en_file_Sp_Status.review_overdue))
							{
								Review review = new Review(doc.id);
								review.FinalizeReview();
							}

							doc.id_status = en_file_Status.archived;
							doc.id_event = EventIdController.GetEventId(en_Events.Archive);

							sql.BeginTran();
							DocumentController.InsertOrUpdateDocument(sql, doc, false);
							HistoryController.SaveDocumentHistoric(sql, doc);
							sql.CommitTran();

							//change line color and status in current row
							row.DefaultCellStyle.BackColor = Color.SeaShell;
							row.DefaultCellStyle.ForeColor = Color.Gray;
							row.Cells["c_id_status"].Value = en_file_Status.archived;
						}
						catch (Exception error)
						{
							sql.RollBack();
							logger.Error(error);
							MessageBox.Show(lib.msg_error_default + lib.msg_error_description + error.Message, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}

					//cancel event
					cancelDtgCompleted = true;
				}

				getBdFileInformation();
				getHistoric();			//refresh grid historic
			}
		}

		//---------------------------------------------------------------------------------
		private void menuDbSaveAs_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			saveNewFile(dtgBdFiles.GetSelectedDocuments());
		}

		//---------------------------------------------------------------------------------
		private void menuDbCancelCheckOut_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");

            var FnCancel = new Func<Document,System.Threading.Tasks.Task<bool>>((Document doc) =>
            {
                return System.Threading.Tasks.Task.Run<bool>(async () =>
                {
                    if (doc.IsActionAllowed(en_Actions.CancelCheckOut))
                    {
                        var wrkFile = new WorkSpaceFile(FileFolder.GetUserFolder() + doc.id_version.ToString() + "\\" + doc.title, FileFolder.GetUserFolder());

                        var result = await syncMgr.Delete(wrkFile);

                        cancelCheckOut(doc);


                        return result;
                    }
                    else
                    {
                        return await System.Threading.Tasks.Task.Run(() => false);
                    }
                });

            });

			List<Document> docs = dtgBdFiles.GetSelectedDocuments();
			docs = DocumentController.GetDocument(docs.Select(a => a.id).ToArray());

            var tasks = new List<System.Threading.Tasks.Task>();
			foreach (Document doc in docs)
			{
                tasks.Add(FnCancel(doc));
            }

            System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

            foreach (Document doc in docs)
            {
                if (IsListing(doc.id))
                {
                    search(dtgBd_SearchMode.RecentDocuments);
                    break;
                }
            }



        }

        //---------------------------------------------------------------------------------
        async private void menuDbImportNewVersion_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			Document SelectedDoc = dtgBdFiles.GetSelectedDocument();
			SpiderOpenFileDialog importFileDialog = new SpiderOpenFileDialog();

			//get file from Dialog
			importFileDialog.Filter = FileFolder.GetExtensionFilterString(SelectedDoc.extension, false);
			if (importFileDialog.ShowDialog() == DialogResult.Cancel)
				return;

			dtgBdFiles_ri = dtgBdFiles.SelectedRows[0].Index;
			getBdFileInformation();

			if (SelectedDoc.CheckOut(false, false, false))
			{
				SelectedDoc.path = importFileDialog.FileName;
				SelectedDoc.id_event = 0;

				List<Document> docs = new List<Document>();
				docs.Add(SelectedDoc);

				if (await saveNewVersion(docs, false))
				{
					//refresh line on grid
					updateRecordOnGrid(SelectedDoc.id);

					//success message
					MessageBox.Show(lib.msg_sucess_imported_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1 ,MessageBoxOptions.DefaultDesktopOnly);
				}
			}
		}

		//---------------------------------------------------------------------------------
		// Events (Others) ----------------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void btnExport_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			List<int> ColumnIdx = new List<int>();
			List<string> sHeaders = new List<string>();

			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = FileFolder.GetExtensionFilterString(en_FilterType.Excel, true);
			sfd.FileName = "FileList.xls";

			if (sfd.ShowDialog() == DialogResult.OK)
			{
				try
				{
					for (int j = 0; j < dtgBdFiles.Columns.Count; j++)
					{
						string sHeader = "";

						if (dtgBdFiles.Columns[j].Visible)
							sHeader = Convert.ToString(dtgBdFiles.Columns[j].HeaderText);

						if (!String.IsNullOrEmpty(sHeader))
						{
							ColumnIdx.Add(j);
							sHeaders.Add(sHeader);
						}
					}

					string[,] values = new string[dtgBdFiles.RowCount + 1, ColumnIdx.Count];

					for (int j = 0; j < ColumnIdx.Count; j++)
						values[0, j] = sHeaders[j];

					for (int i = 0; i <= dtgBdFiles.RowCount - 2; i++)
					{
						int j = 0;

						foreach (int idx in ColumnIdx)
						{
							values[i + 1, j] = Convert.ToString(dtgBdFiles[idx, i].Value);
							j++;
						}
					}
					File.Delete(sfd.FileName);

					SpiderExcel excel = new SpiderExcel();
					excel.AutoLoad(values);
					excel.Save(sfd.FileName);
					excel.Close();
				}
				catch(Exception error)
				{
					logger.Error(error);
					MessageBox.Show(lib.msg_error_save_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		//---------------------------------------------------------------------------------
		// Functions (For DataGridView) ---------------------------------------------------
		//---------------------------------------------------------------------------------
		private void loadIconsDtgBase()
		{
			logger.Trace("Begin");
			foreach (DataGridViewRow row in dtgBdFiles.Rows)
			{
				en_file_Sp_Status status = en_file_Sp_Status.normal;
				if (row.Cells["c_id_sp_status"].Value != DBNull.Value)
					status = (en_file_Sp_Status)Convert.ToInt32(row.Cells["c_id_sp_status"].Value);

				if (status == en_file_Sp_Status.review_overdue)
					row.Cells["c_img"].Value = Properties.Resources.exclamation;
				else
					row.Cells["c_img"].Value = icon.GetSmallIcon(Convert.ToString(row.Cells["c_extension"].Value));
			}
		}

		//---------------------------------------------------------------------------------
		public void updateRecordOnGrid(int id_doc)
		{
			logger.Trace("Begin");
			try
			{
				dtgBdFiles.UpdateRow(dtgBdFiles.CurrentRow.Index);
				UpdateRowAppearance(dtgBdFiles.CurrentRow);

				//versions and historic
				refreshDbGrids();

			}
			catch(Exception error)
			{
				logger.Error(error);
				search(dtgBd_SearchMode.RecentDocuments);
			}
		}

		//---------------------------------------------------------------------------------
		private void getBdFileInformation()
		{
			logger.Trace("Begin");
			try
			{
				int rowIndex;

				endPopuletedGrid = false;
				if (endPopuletedGridVersion)
				{
					if (dtgBdFiles_rightClick)
					{
						rowIndex = dtgBdFiles_ri;

					}
					else
					{
						if (dtgBdFiles.SelectedRows.Count == 0)
							return;

						rowIndex = dtgBdFiles.CurrentRow.Index;
					}

					//get versions and historic-----
					refreshDbGrids();

					dtgBdFiles_rightClick = false;
					endPopuletedGrid = true;
				}
			}
			catch (Exception error)
			{
				MessageBox.Show(lib.msg_error_getting_fileDeatils, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
		}

		//---------------------------------------------------------------------------------
		void UpdateDataBaseGridView()
		{
			logger.Trace("Begin");
			dtgBdFiles.Update();
			getBdFileInformation();
		}

		//---------------------------------------------------------------------------------
		// private void loadRecentFileByDoc(int id_doc)
		// {
		// 	logger.Trace("Begin");
		// 	bool found = false;

		// 	//check if the current id is in the gridview
		// 	if (id_doc != 0)
		// 	{
		// 		foreach (DataGridViewRow rows in dtgBdFiles.Rows)
		// 		{
		// 			if (Convert.ToInt32(rows.Cells["c_id_doc"].Value) == id_doc)
		// 				found = true;
		// 		}

		// 	}
		// 	else
		// 	{
		// 		found = true;
		// 	}

		// 	if (found)
		// 		search(dtgBd_SearchMode.RecentDocuments);
		// }

		private bool IsListing(int id_doc)
		{
			logger.Trace("Begin");
			bool found = false;

			if (id_doc != 0)
			{
				foreach (DataGridViewRow rows in dtgBdFiles.Rows)
				{
					if (Convert.ToInt32(rows.Cells["c_id_doc"].Value) == id_doc)
						found = true;
				}

			}
			else
			{
				found = true;
			}

			return found;
		}

		//---------------------------------------------------------------------------------
		private int GetRowIdxByDocId(int doc_id)
		{
			logger.Trace("Begin");
			int ans = -1;

			foreach (DataGridViewRow row in dtgBdFiles.Rows)
			{
				if (Convert.ToInt32(row.Cells["c_id_doc"].Value) == doc_id)
				{
					ans = row.Index;
					break;
				}
			}

			return ans;
		}

		//---------------------------------------------------------------------------------
		private void showBoxCheckOut()
		{
			logger.Trace("Begin");
			frmMessageBox frm = new frmMessageBox(dtgBdFiles.GetSelectedDocument());
			frm.ShowDialog();
		}

		//---------------------------------------------------------------------------------
		void UpdateRowAppearance(DataGridViewRow row)
		{
			//logger.Trace("Begin");
			Document doc = dtgBdFiles.GetDocument(row.Index);

			DocumentAttribute attr;

			attr = doc.Attrs.FirstOrDefault(a => a.id == (int)SystemAttributes.MailIsComposed);
			if (attr == null)
				row.Cells["c_mail_in_out_prefix"].Value = "";
			else if ((en_AttrCheckState)attr.atbValue == en_AttrCheckState.True)
				row.Cells["c_mail_in_out_prefix"].Value = "OUT:";
			else
				row.Cells["c_mail_in_out_prefix"].Value = "IN:";

			attr = doc.Attrs.FirstOrDefault(a => a.id == (int)SystemAttributes.MailTime);
			if (attr != null)
				row.Cells["c_mail_time"].Value = attr.atbValue;

			if (doc.id_status == en_file_Status.checked_out)
			{
				row.DefaultCellStyle.BackColor = Color.WhiteSmoke;
				row.DefaultCellStyle.ForeColor = Color.Gray;

			}
			else if (doc.id_status == en_file_Status.archived)
			{
				row.DefaultCellStyle.BackColor = Color.SeaShell;
				row.DefaultCellStyle.ForeColor = Color.Gray;

			}
			else
			{
				if (doc.id_sp_status == en_file_Sp_Status.review)
				{
					row.DefaultCellStyle.BackColor = Color.Lime;

				}
				else if (doc.id_sp_status == en_file_Sp_Status.review_overdue)
				{
					row.DefaultCellStyle.BackColor = Color.Yellow;

				}
				else
				{
					row.DefaultCellStyle.BackColor = Color.White;
				}
			}
		}

		//---------------------------------------------------------------------------------
		// for header customize
		void populateComboAttribute()
		{
			logger.Trace("Begin");
			List<DocumentAttribute> attrs = DocumentAttributeController.GetAttributesCache(true);

			DocumentAttribute top = new DocumentAttribute();
			top.name = "--- Please Select ---";
			attrs.Insert(0, top);

			cboAttributes.DataSource = attrs;
			cboAttributes.DisplayMember = "name";
			cboAttributes.ValueMember = "id";
		}

		//---------------------------------------------------------------------------------
		// Functions (For Menu) -----------------------------------------------------------
		//---------------------------------------------------------------------------------
		async private void CheckOutFile(bool footer, bool open)
		{
			logger.Trace("Begin");

            var tasks = new List<System.Threading.Tasks.Task>();

			// Sync logic must be different function to be cool
            var FnInit = new Func<string, System.Threading.Tasks.Task<bool>>((string filePath) =>
            {
                return System.Threading.Tasks.Task.Run(() => syncMgr.InitSync(new WorkSpaceFile(filePath, FileFolder.GetUserFolder())));
            });

			string[] paths = new string[dtgBdFiles.SelectedRows.Count]; int i = 0; // asyncronous tasks

            try
            {

				List<Document> docs = dtgBdFiles.GetSelectedDocuments();

				// check the document has already checked out.
				var allGoodToProced = await AllStatusAre(en_file_Status.checked_in, docs.Select( x =>x.id).ToArray());
				if (false == allGoodToProced)
				{
					MessageBox.Show(lib.msg_checkout_already2, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

					click_search_button();

					return;
				}

                syncMgr.Stop();

                //busyDialog = CreateBusy();

                foreach (DataGridViewRow row in dtgBdFiles.SelectedRows)
                {
                    Document doc = dtgBdFiles.GetDocument(row.Index);

                    cancelDtgCompleted = true;
                    if (doc.CheckOut(open, footer))
                    {
                        paths[i++] = doc.ContainerFolder();

                        FileFolder.HideFolder(doc.ContainerFolder());

                        //await System.Threading.Tasks.Task.Delay(3000);

                        dtgBdFiles.UpdateRow(row.Index);
                        UpdateRowAppearance(row);

                        // TODO: Sync
                        tasks.Add(FnInit(doc.path));// syncMgr.InitSync(new WorkSpaceFile(doc.path, FileFolder.GetUserFolder()));
                                                    //               //var wfile = new WorkSpaceFile(doc.path, FileFolder.GetUserFolder());
                                                    ////wfile.InitSync();
                    }

                }

                await System.Threading.Tasks.Task.WhenAll(tasks);

                //refresh frid historic
                getHistoric();
                getBdFileInformation();

                //dialog.Dispose();

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            finally
            {

                //tokenSource?.Cancel();

                syncMgr.Start();

				foreach (var path in paths) FileFolder.UnHideFolder(path);
            }

        }

		async System.Threading.Tasks.Task<bool> AllStatusAre(en_file_Status checkStatus, int[] ids )
		{
			var max = ids.Length;
			var tasks = new System.Threading.Tasks.Task<Document>[max];
			int i =0;

			// check the document has already checked out.
			List<Document> docs = dtgBdFiles.GetSelectedDocuments();

			var getDocAsync = new Func<int, System.Threading.Tasks.Task<Document>>
			(
				async (int id) => await System.Threading.Tasks.Task.Run(() => DocumentController.GetDocument(id))
			);

			foreach (var doc in docs)
			{
				tasks[i++] = getDocAsync(doc.id);
			}

			var ans = await System.Threading.Tasks.Task.WhenAll(tasks);

			var allGood = ans.All(x => x.id_status == checkStatus);

			return allGood;
		}

		//---------------------------------------------------------------------------------
		void SendByEmail(bool pdf)
		{
			logger.Trace("Begin");
			string tempPath;
			List<string> path = new List<string>();
			List<int> FileIdVersions = new List<int>();

			List<Document> docs = dtgBdFiles.GetSelectedDocuments();
			foreach (Document doc in docs)
			{
				if (doc.IsActionAllowed(en_Actions.SendByEmail))
				{
					//export to temp folder
					tempPath = FileFolder.TempPath + doc.title;

					DocumentController.LoadBinaryData(doc);
					doc.Save(tempPath);

					if (pdf && doc.IsActionAllowed(en_Actions.SendByEmail_PDF))
						tempPath = PDFConverter.pdfconversion(tempPath, FileFolder.TempPath);

					FileIdVersions.Add(doc.id_version);
					path.Add(tempPath);
				}
			}

			if (path.Count > 0)
			{
				try
				{
					string subject = "";

					if (path.Count == 1)
						subject = Path.GetFileName(path[0].ToString());

					//open outlook
					new Email().OpenNewEmail(subject, "", path);

					//register historic
					Document doc = new Document();
					doc.id_event = EventIdController.GetEventId(en_Events.Email);
					doc.id_user = SpiderDocsApplication.CurrentUserId;

					SqlOperation sql = new SqlOperation();
					sql.BeginTran();

					foreach (int fileIdVersion in FileIdVersions)
					{
						doc.id_version = fileIdVersion;
						HistoryController.SaveDocumentHistoric(sql, doc);
					}

					sql.CommitTran();

					//refresh grid historic
					getHistoric();

				}
				catch (Exception error)
				{
					logger.Error(error);
					MessageBox.Show(lib.msg_error_default + lib.msg_error_description + error.Message, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		//---------------------------------------------------------------------------------
		// Tab Control --------------------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void tbDbFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			refreshDbGrids();
		}

		//---------------------------------------------------------------------------------
		private void tbLocalFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			if (endPopuletedGrid)
				getLocalFileInformation();
		}

		//---------------------------------------------------------------------------------
		private void refreshDbGrids()
		{
			logger.Trace("Begin");
			//refresh grids
			getProperties();

			endPopuletedGrid = false;
			getVersion();
			endPopuletedGrid = true;

			getHistoric();

			if (tbDbFiles.SelectedTab.Name == "tbPreview")
			{
				if (DbSelected)
					ReloadDbPreview();
				else
					ReloadWsPreview();
			}
		}

		//---------------------------------------------------------------------------------
		public void getProperties()
		{
			logger.Trace("Begin");
			if (tbDbFiles.SelectedTab.Name == "tbProperties")
			{
				Document doc = dtgBdFiles.GetSelectedDocument();

				if (doc != null)
				{
					pictureBoxBd.Visible = true;
					pictureBoxBd.Image = icon.GetLargeIcon(doc.extension);

					lblBdId.Text = doc.id.ToString();
					lblBdName.Text = doc.title;
					lblBdCurrentVersion.Text = "V" + doc.version;
					//lblBdCreated.Text = doc.date.ToString(ConstData.DATE_TIME);

					History history = HistoryController.GetLatestHistory(doc.id_version,
																		 en_Events.Chkin, en_Events.Created, en_Events.SaveNewVer,
																		 en_Events.Rollback, en_Events.NewVer, en_Events.Archive,
																		 en_Events.ChgAttr, en_Events.ChgDT, en_Events.ChgFolder,
																		 en_Events.ChgName, en_Events.Import, en_Events.Property,
																		 en_Events.Scan);
					if (history != null)
						lblBdUpdated.Text = history.date.ToString(ConstData.DATE);
					else
						lblBdUpdated.Text = Library.msg_no_history;

					lblBdType.Text = doc.extension;

					long len = doc.size / 1024;
					if (len < 1)
						lblBdSize.Text = "< 1 KB";
					else
						lblBdSize.Text = len.ToString() + " KB";

					lblBdDeadlineVal.Text = strInvalid;
					lblBdDeadlineVal.ForeColor = SystemColors.WindowText;
					lblBdByVal.Text = strInvalid;

					if ((doc.id_sp_status == en_file_Sp_Status.review)
					|| (doc.id_sp_status == en_file_Sp_Status.review_overdue))
					{
						Review review = ReviewController.GetReview(doc.id);
						if (review != null)
						{
							if (review.status == en_ReviewStaus.UnReviewed)
							{
								lblBdDeadlineVal.Text = review.deadline.ToString(ConstData.DATE);

								if (doc.id_sp_status == en_file_Sp_Status.review_overdue)
									lblBdDeadlineVal.ForeColor = Color.Red;
								else
									lblBdDeadlineVal.ForeColor = SystemColors.WindowText;

								List<User> users = UserController.GetUser(false, false, review.review_users.Select(a => a.id_user).ToArray());
								lblBdByVal.Text = String.Join(", ", users.Select(a => a.name).ToArray());
							}
						}
					}
				}
			}
		}

		//---------------------------------------------------------------------------------
		public void getVersion()
		{
			logger.Trace("Begin");
			if (tbDbFiles.SelectedTab.Name == "tbVersion")
			{
				endPopuletedGrid = false;
				dtgVersions.SuspendLayout();

				Document doc = dtgBdFiles.GetSelectedDocument();
				if (doc != null)
				{
					int id = dtgBdFiles.GetSelectedDocument().id;
					table_version.Select(id);
				}

				dtgVersions.ResumeLayout();
				endPopuletedGrid = true;
			}
		}

		//---------------------------------------------------------------------------------
		public void getHistoric()
		{
			logger.Trace("Begin");
			if (tbDbFiles.SelectedTab.Name == "tbHistoric")
			{
				Document doc = dtgBdFiles.GetSelectedDocument();
				if (doc != null)
				{
					dtgHist.SuspendLayout();
					table_historic.Select(0, doc.id);
					dtgHist.ResumeLayout();
				}
			}
		}

		//---------------------------------------------------------------------------------
		private void ReloadDbPreview()
		{
			// Show preview
			if (0 < dtgBdFiles.SelectedRows.Count)
			{
				DataGridViewRow row = dtgBdFiles.CurrentRow;
				Document doc = dtgBdFiles.GetSelectedDocument();

				if (!BsrPreview.busy && doc.IsActionAllowed(en_Actions.Export))
				{
					string filepath = FileFolder.TempPath + "Preview_" + doc.title;

					FileFolder.DeleteTempFiles("Preview_*");

					if (!File.Exists(filepath))
					{
						DocumentController.LoadBinaryData(doc);
						doc.Save(filepath);
					}

					BsrPreview.FilePath = filepath;
					BsrPreview.LoadDocument();
				}
			}
		}

		//---------------------------------------------------------------------------------
		#endregion


		/*
		 * Work Space - dtgLocal
		 */
		#region WorkSpace - dtgLocal
		//---------------------------------------------------------------------------------
		int dtgLocalFiles_ri;
		//int numberLocalFiles = 0;

		//---------------------------------------------------------------------------------
		// Events (DataGridView) ----------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void dtgLocalFile_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			dtgLocalFile.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
		}

		//---------------------------------------------------------------------------------
		private void dtgLocalFile_MouseClick(object sender, MouseEventArgs e)
		{
			logger.Trace("Begin");
			if (e.Button == MouseButtons.Right)
			{
				menuLocalCheckIn.Enabled = false;
				menuLocalExport.Enabled = false;
				menuLocalExportPdf.Enabled = false;
				menuLocalSendByEmail.Enabled = false;
				menuLocalSendByEmailPdf.Enabled = false;
				menuLocalDeleteFile.Enabled = false;

				Point pt = dtgLocalFile.PointToScreen(e.Location);
				menuDtgLocalFiles.Show(pt);
			}
		}

		//---------------------------------------------------------------------------------
		private void dtgLocalFile_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			logger.Trace("Begin");
			try
			{
				if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0) && (e.Button == MouseButtons.Right))
				{
					// Select single file or an unselected file.
					if ((dtgLocalFile.SelectedRows.Count == 1)
					|| (!CheckMousePosdtg(dtgLocalFile, e.RowIndex)))
					{
						dtgLocalFiles_ri = e.RowIndex;
						dtgLocalFile.CurrentCell = dtgLocalFile[e.ColumnIndex, e.RowIndex];
						dtgLocalFile.Rows[e.RowIndex].Selected = true;
						string ext = dtgLocalFile.Rows[e.RowIndex].Cells["dtgLocalFile_Ext"].Value.ToString().ToLower();

						menuLocalCheckIn.Enabled = (workOffline ? false : true);

						menuLocalExport.Enabled = true;
						menuLocalSendByEmail.Enabled = SpiderDocsApplication.IsOutlook;

						if (FileFolder.OfficeCheck(ext) != en_OfficeType.NotOffice)
						{
							menuLocalExportPdf.Enabled = true;
							menuLocalSendByEmailPdf.Enabled = SpiderDocsApplication.IsOutlook;
						}

						getLocalFileInformation();

						if (0 < dtgLocalFile.GetSelectedDocument().id)
							menuLocalChangeFileName.Enabled = false;
						else
							menuLocalChangeFileName.Enabled = true;

						menuLocalDeleteFile.Enabled = (workOffline ? false : true);

					}
					else
					{
						menuLocalCheckIn.Enabled = (workOffline ? false : true);
						menuLocalExport.Enabled = true;
						menuLocalExportPdf.Enabled = (SpiderDocsApplication.IsExcel || SpiderDocsApplication.IsWord || SpiderDocsApplication.IsPowPnt);
						menuLocalSendByEmail.Enabled = SpiderDocsApplication.IsOutlook;
						menuLocalSendByEmailPdf.Enabled = (SpiderDocsApplication.IsExcel || SpiderDocsApplication.IsWord || SpiderDocsApplication.IsPowPnt) && SpiderDocsApplication.IsOutlook;
						menuLocalChangeFileName.Enabled = false;
						menuLocalDeleteFile.Enabled = (workOffline ? false : true);
					}

					//show menu
					Rectangle r = dtgLocalFile.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
					this.menuDtgLocalFiles.Show((Control)sender, r.Left + e.X, r.Top + e.Y);
				}
			}
			catch (Exception error)
			{
				MessageBox.Show(lib.msg_error_file_menu, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
		}

        //---------------------------------------------------------------------------------

		private void dtgLocalFile_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			logger.Trace("Begin");
			// Wait for preview as Office interops tend to close opening file
			if ((BsrPreview.busy) || (e.RowIndex == -1))
				return;

			try
			{
				int idVersion = 0;

				//get file details
				string docName = Convert.ToString(dtgLocalFile.Rows[dtgLocalFile.CurrentRow.Index].Cells["dtgLocalFile_Title"].Value);
				string path = Convert.ToString(dtgLocalFile.Rows[dtgLocalFile.CurrentRow.Index].Cells["dtgLocalFile_Path"].Value);

                var behavour = ShellBehaviourController.Get(Path.GetExtension(path), SpiderDocsModule.Models.ShellBehaviour.en_Shell.Open);
                ShellBehaviour ok = new ShellBehaviour(behavour);
                int processId = ok.Run(path);


				syncMgr.FileOpened(new WorkSpaceFile(path, FileFolder.GetUserFolder()), processId);


                //Task task = Task.Factory.StartNew(() => Process.Start(path));

            }
			catch (Exception error)
			{
				MessageBox.Show(lib.msg_error_default + lib.msg_error_description + error.Message, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
		}

		//---------------------------------------------------------------------------------
		private void dtgLocalFile_SelectionChanged(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			DbSelected = false;

			if (endPopuletedGridLocal)
			{
				getLocalFileInformation();
				ReloadWsPreview();
			}
		}

		//---------------------------------------------------------------------------------
		private void dtgLocalFile_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			logger.Trace("Begin");
			if (flagChangeName)
			{
				renameLocalFile();
				e.Cancel = true;
			}
		}

		//---------------------------------------------------------------------------------
		private void dtgLocalFile_KeyDown(object sender, KeyEventArgs e)
		{
			logger.Trace("Begin");
			if (e.KeyCode == Keys.Enter & flagChangeName)
			{
				if (!renameLocalFile())
					dtgLocalFile.CancelEdit();

				flagChangeName = false;
			}
		}

		//---------------------------------------------------------------------------------
		private void dtgLocalFile_KeyPress(object sender, KeyPressEventArgs e)
		{
			logger.Trace("Begin");
			e.Handled = Utilities.checkKeychar((int)e.KeyChar);
		}

		//---------------------------------------------------------------------------------
		// Events (Menu) ------------------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void menuDtgLocalFiles_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			logger.Trace("Begin");
			menuDtgLocalFiles.Close();
		}


		/// <summary>
		/// CheckIn
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async private void menuLocalCheckIn_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");

			List<Document> docs = dtgLocalFile.GetSelectedDocuments();
			bool newfile = false;
			bool ans = true;

			if (docs[0].id <= 0)
				newfile = true;

            // Stop, if one of documents have been discard check out in somewhere.
            foreach (var doc in docs)
            {
                if (newfile) continue;

                var exists = DocumentController.GetDocument(doc.id);

                if (exists.id_status == en_file_Status.checked_in)
                {
                    MessageBox.Show(lib.msg_checkout_already, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            foreach (Document doc in docs)
			{
				if (FileFolder.IsFileLocked(doc.path))
				{
					MessageBox.Show(lib.msg_file_blocked, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					ans = false;

				}
				else if ((newfile && (0 < doc.id)) || (!newfile && (0 >= doc.id)))
				{
					ans = false;
				}

				if (!ans)
					break;
			}

			if (ans)
			{
				if (newfile)
					saveNewFile(docs); //first version
				else
					await saveNewVersion(docs, true); //existing version
            }
		}

		//---------------------------------------------------------------------------------
		private void menuLocalSendByEmail_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			LocalSendByEmail(false);
		}

		//---------------------------------------------------------------------------------
		private void menuLocalSendByEmailPdf_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			LocalSendByEmail(true);
		}

		//---------------------------------------------------------------------------------
		private async void menuLocalDeleteFile_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			bool checkout = false;
			DialogResult result;

			List<int> delfileid = new List<int>();
			List<string> delfilepath = new List<string>();

			foreach (DataGridViewRow row in dtgLocalFile.SelectedRows)
			{
				int file_id = 0;

				if ((row.Cells["dtgLocalFile_Id"].Value != null) && (0 < (int)row.Cells["dtgLocalFile_Id"].Value))
					file_id = Convert.ToInt32(row.Cells["dtgLocalFile_Id"].Value);

				//check if this file is from check out
				if (0 < file_id)
					checkout = true;

				delfileid.Add(file_id);
				delfilepath.Add(Convert.ToString(row.Cells["dtgLocalFile_Path"].Value));
			}

			if (checkout)
				result = (MessageBox.Show("This action will cancel the check out from this file \n Are you sure you want to delete this file?", "Spider Docs", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
			else
				result = (MessageBox.Show("Are you sure you want to delete this file?", "Spider Docs", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

			if (result == DialogResult.Yes)
			{
				int cntDelFiles = delfileid.Count;
				Document doc = new Document();

                var FnDel = new Func<int, string,int, System.Threading.Tasks.Task<bool>>(async (int i, string filePath, int verOrNot) =>
                  {

                      var wrkFile = new WorkSpaceFile(filePath, FileFolder.GetUserFolder());
                      bool ans;
                      if (verOrNot <= 0)
                      {
                        // New file


                        // Delete and cancel Check out should apply database as well.
                        //if (!syncMgr.Delete(wrkFile))//if (!FileFolder.DeleteFile(delfilepath[i]))
                        //MessageBox.Show(lib.msg_file_blocked, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        ans = await syncMgr.Delete(wrkFile);


                      }
                      else
                      {
                        // Existing file


                        ans = await syncMgr.Delete(wrkFile);

                          doc = DocumentController.GetDocument(verOrNot);
                          cancelCheckOut(doc);
                      }

                      // Make deleting files as hidden so that it can be unshown in work space.
                      MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);
                      return ans;

                  });


                var paths = new string[cntDelFiles+1];
                var tasks = new List<System.Threading.Tasks.Task<bool>>();
				for (int i = 0; i < cntDelFiles; i++)
				{
                    paths[i] = System.IO.Path.GetDirectoryName(delfilepath[i]);
                    FileFolder.HideFolder(paths[i]);

                    //File.SetAttributes(delfilepath[i], File.GetAttributes(delfilepath[i]) | FileAttributes.Hidden);

                    tasks.Add(FnDel(i, delfilepath[i], delfileid[i]));
                    //MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);
                }

                // Make deleting files as hidden so that it can be unshown in work space.
                //MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);

                //this.dtgLocalFile.Enabled = false;
                bool[] okay = await System.Threading.Tasks.Task.WhenAll<bool>(tasks.ToArray());
                //this.dtgLocalFile.Enabled = true;

                // Must notice a user if one of files was locked. ( opening a file )
                if (okay.IndexOf(false) > -1)
                {
                    MessageBox.Show(lib.msg_file_blocked, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (IsListing(doc.id))
                    search(dtgBd_SearchMode.RecentDocuments);

                MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);

            }
		}

		//---------------------------------------------------------------------------------
		private void menuLocalExport_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			LocalExport(false);
		}

		//---------------------------------------------------------------------------------
		private void menuLocalExportPdf_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			LocalExport(true);
		}

		//---------------------------------------------------------------------------------
		private void changeFileNameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			flagChangeName = true;

			dtgLocalFile.Rows[dtgLocalFile.CurrentRow.Index].Cells["dtgLocalFile_Title"].ReadOnly = false;
			dtgLocalFile.Rows[dtgLocalFile.CurrentRow.Index].Cells["dtgLocalFile_Title"].Value = dtgLocalFile.GetSelectedDocument().title_without_ext;
			dtgLocalFile.BeginEdit(true);
			dtgLocalFile.RefreshEdit();

            /*
             * Rename a file and sync the changes
             * TODO: write rename function. Be carefull. there are two patterns. a file exists only local and both.
             */
            //string path = (string)dtgLocalFile.Rows[dtgLocalFile.CurrentRow.Index].Cells["dtgLocalFile_Path"].Value;
            //var wfile = new WorkSpaceFile(path);
            //wfile.RenameTo(dtgLocalFile.GetSelectedDocument().title);
		}

        //---------------------------------------------------------------------------------
        // Functions (For DataGridView) ---------------------------------------------------
        //---------------------------------------------------------------------------------
        void loadWorkSpaceFiles()
        {
			logger.Trace("Begin");
			try
			{
				//FileFolder.DeleteHiddenFilesInWorkSpace();

				FileInfo[] files = FileFolder.GetAllFilesFromWorkSpace();

				endPopuletedGridLocal = false;

				dtgLocalFile.Rows.Clear();

				foreach (FileInfo file in files)
				{
					//logger.Debug("Loading file:{0}",file.FullName);
					Document fileDetais = null;

					if (!workOffline)
						fileDetais = FileFolder.GetDocumentFromWorkSpace(file.FullName);

					if (fileDetais == null)
					{
						fileDetais = new Document();
						fileDetais.path = file.FullName;
					}

					fileDetais.size = file.Length;
					fileDetais.date = file.CreationTime;

					dtgLocalFile.InsertRow(fileDetais);
				}

				if (dtgLocalFile.RowCount != 0)
					lblLocalFile.Text = "Files: " + dtgLocalFile.Rows.Count;

				loadIconsDtgLocal();
				endPopuletedGridLocal = true;

				getLocalFileInformation();

			}
			catch (Exception error)
			{
				//MessageBox.Show(lib.msg_error_load_local_files, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
			finally
			{
				endPopuletedGridLocal = true;
				endPopuletedGrid = true;
			}
		}

		//---------------------------------------------------------------------------------
		void getLocalFileInformation()
		{
			logger.Trace("Begin");
			if ((dtgLocalFile.RowCount > 0) && endPopuletedGridLocal)
			{
				Document doc = dtgLocalFile.GetSelectedDocument();

				if ((doc != null) && File.Exists(doc.path))
				{
					lblLocalId.Visible = true;
					lblLocalId.Text = (0 < doc.id ? doc.id.ToString() : "");
					lblName.Visible = true;
					lblName.Text = doc.title;
					lblSize.Visible = true;

					FileInfo fileinfo = new FileInfo(doc.path);
					long Len = fileinfo.Length / 1024;

					if (Len < 1)
						lblSize.Text = "< 1 KB";
					else
						lblSize.Text = Convert.ToString(Len) + " KB";

					lblType.Visible = true;
					lblType.Text = doc.extension;
					lblCreated.Visible = true;
					lblCreated.Text = fileinfo.CreationTime.ToString(ConstData.DATE);

					lblModifield.Visible = true;
					lblModifield.Text = fileinfo.LastWriteTime.ToString(ConstData.DATE);
					pictureBoxLocal.Visible = true;
					pictureBoxLocal.Image = icon.GetLargeIcon(doc.extension);

					if (0 < doc.id)
						lblStatus.Text = "File in edition (Check out)";
					else
						lblStatus.Text = "New document";
				}
			}
		}

		//---------------------------------------------------------------------------------
		private void loadIconsDtgLocal()
		{
			logger.Trace("Begin");
			try
			{
				foreach (DataGridViewRow row in dtgLocalFile.Rows)
				{
					string extension = Convert.ToString(row.Cells["dtgLocalFile_Ext"].Value);
					row.Cells["dtgLocalFile_Status"].Value = icon.GetSmallIcon(extension);
					row.Cells["dtgLocalFile_Status"].ToolTipText = extension;

					if ((row.Cells["dtgLocalFile_Id"].Value != null) && (0 < (int)row.Cells["dtgLocalFile_Id"].Value))
					{
						row.Cells["dtgLocalFile_Icon"].Value = Properties.Resources.ResourceManager.GetObject("editing");
						row.Cells["dtgLocalFile_Icon"].ToolTipText = "File in edition";
					}
					else
					{
						row.Cells["dtgLocalFile_Icon"].Value = Properties.Resources.add;
						row.Cells["dtgLocalFile_Icon"].ToolTipText = "New document";
					}
				}
			}
			catch (Exception error)
			{
				logger.Error(error);
			}
		}

		//---------------------------------------------------------------------------------
		// Functions (For Menu) -----------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void LocalExport(bool pdf)
		{
			logger.Trace("Begin");
			try
			{
				//Exporting multiple files------------------------------------------------------
				if (dtgLocalFile.SelectedRows.Count > 1)
				{
					if (folderBrowserDialog.ShowDialog() == DialogResult.Cancel)
						return;

					foreach (DataGridViewRow row in dtgLocalFile.SelectedRows)
					{
						string FilePath = Convert.ToString(row.Cells["dtgLocalFile_Path"].Value);
						string Name = Convert.ToString(row.Cells["dtgLocalFile_Title"].Value);
						string ExpPath = "";

						if (pdf)
						{
							ExpPath = folderBrowserDialog.SelectedPath + "\\" + Path.GetFileNameWithoutExtension(Name) + ".pdf";
							ExpPath = PDFConverter.pdfconversion(FilePath, ExpPath, false);
						}

						if (ExpPath == "")
						{
							ExpPath = folderBrowserDialog.SelectedPath + "\\" + Name;
							File.Copy(FilePath, ExpPath, true);
						}
					}

					//Exporting single files------------------------------------------------------
				}
				else
				{
					Document doc = dtgLocalFile.GetSelectedDocument();

					string original_ext = Path.GetExtension(doc.title).ToLower();

					if (pdf)
					{
						ExportFileDialog.FileName = Path.GetFileNameWithoutExtension(doc.title) + ".pdf";
						ExportFileDialog.Filter = FileFolder.GetExtensionFilterString(en_FilterType.PDF, true);

					}
					else
					{
						ExportFileDialog.FileName = doc.title;
						ExportFileDialog.Filter = FileFolder.GetExtensionFilterString(doc.extension, true);
					}

					if (ExportFileDialog.ShowDialog() == DialogResult.Cancel)
						return;

					string SelectedPath = ExportFileDialog.FileName.Replace(Path.GetFileName(ExportFileDialog.FileName), "");
					string SelectedName = Path.GetFileNameWithoutExtension(ExportFileDialog.FileName) + original_ext;
					string ExpPath = "";

					if (pdf)
					{
						ExpPath = SelectedPath + Path.GetFileNameWithoutExtension(SelectedName) + ".pdf";
						ExpPath = PDFConverter.pdfconversion(doc.path, ExpPath, false);
					}

					if (ExpPath == "")
					{
						ExpPath = SelectedPath + SelectedName;
						File.Copy(doc.path, ExpPath, true);
					}
				}
			}
			catch (Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default + lib.msg_error_description + error.Message, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//---------------------------------------------------------------------------------
		void LocalSendByEmail(bool pdf)
		{
			logger.Trace("Begin");
			List<string> path = new List<string>();

			foreach (DataGridViewRow row in dtgLocalFile.SelectedRows)
			{
				string srcPath = Convert.ToString(row.Cells["dtgLocalFile_Path"].Value);

				if (pdf)
				{
					string ans = PDFConverter.pdfconversion(srcPath, FileFolder.TempPath, false);

					if (ans != "")
						srcPath = ans;
				}

				path.Add(srcPath);
			}

			try
			{
				//Sending multiple files------------------------------------------------------
				string subject = "";

				if (path.Count == 1)
					subject = Path.GetFileName(path[0].ToString());

				new Email().OpenNewEmail(subject, "", path);
			}
			catch (Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default + lib.msg_error_description + error.Message, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//---------------------------------------------------------------------------------
		private bool renameLocalFile()
		{
			logger.Trace("Begin");
			bool ans = false;

            try
            {
                syncMgr.Stop();

                int idx = dtgLocalFile.CurrentRow.Index;

                dtgLocalFile.EndEdit();

                DataGridViewCell dtgLocalFile_Title = dtgLocalFile.Rows[idx].Cells["dtgLocalFile_Title"];
                object dtgLocalFile_Title_Value = dtgLocalFile_Title.Value;
                object dtgLocalFile_Ext_Value = dtgLocalFile.Rows[idx].Cells["dtgLocalFile_Ext"].Value;

                if ((dtgLocalFile_Title_Value != null)
                && !String.IsNullOrEmpty(dtgLocalFile_Title_Value.ToString())
                && FileFolder.IsValidFileName(dtgLocalFile_Title_Value.ToString()))
                {
                    //add an extension as user editted it without the extension
                    dtgLocalFile_Title_Value += Convert.ToString(dtgLocalFile_Ext_Value);

                    Document doc = dtgLocalFile.GetSelectedDocument();
                    doc.title = Convert.ToString(dtgLocalFile_Title_Value);

                    string path = FileFolder.RenameFile(doc.path, doc.title);

                    if (!String.IsNullOrEmpty(path))
                    {
                        doc.path = path;

                        dtgLocalFile.UpdateRow(doc, dtgLocalFile.CurrentRow.Index);
                        dtgLocalFile.RefreshEdit();


                        /*
						* Rename a file and sync the changes
						* TODO: write rename function. Be carefull. there are two patterns. a file exists only local and both.
						*/
                        syncMgr.ReName( new WorkSpaceFile(path, FileFolder.GetUserFolder()) );
                        //var wfile = new WorkSpaceFile(path, FileFolder.GetUserFolder());
                        //wfile.RenameTo();

                        ans = true;

                    }
                    else
                    {
                        MessageBox.Show(lib.msg_file_blocked, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                dtgLocalFile_Title.ReadOnly = true;
                flagChangeName = false;

                MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);
            }
            catch (Exception error)
            {
                logger.Error(error);
                MessageBox.Show(lib.msg_error_default + lib.msg_error_description + error.Message, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                syncMgr.Start();
            }
			return ans;
		}

		//---------------------------------------------------------------------------------
		// Tab Control --------------------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void ReloadWsPreview()
		{
			logger.Trace("Begin");
			if (!BsrPreview.busy && (dtgLocalFile.CurrentRow != null))
			{
				// Show preview
				DataGridViewRow row = dtgLocalFile.CurrentRow;
				BsrPreview.FilePath = Convert.ToString(row.Cells["dtgLocalFile_Path"].Value);

				if (tbDbFiles.SelectedTab.Name == "tbPreview")
					BsrPreview.LoadDocument();
			}
		}

		//---------------------------------------------------------------------------------
		#endregion


		/*
		* Work Space - dtgVersions
		*/
		#region WorkSpace - dtgVersions
		//---------------------------------------------------------------------------------
		private void dtgVersions_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			logger.Trace("Begin");
			try
			{
				if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right)
				{
					dtgVersions.CurrentCell = dtgVersions[e.ColumnIndex, e.RowIndex];
					dtgVersions.Rows[e.RowIndex].Selected = true;

					Rectangle r = dtgVersions.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
					menuDtgVersionFiles.Show((Control)sender, r.Left + e.X, r.Top + e.Y);

					//Check permission at the selected file
					//-----------------------------------hide/show menu---------------------------------------------
					Document bd_file = dtgBdFiles.GetSelectedDocument();

					//Open (read only)
					menuVersionOpen.Enabled = bd_file.IsActionAllowed(en_Actions.OpenRead);

					//Export
					menuVersionExport.Enabled = bd_file.IsActionAllowed(en_Actions.Export);

					//Export to HardDisk as PDF
					menuVersionExportHardDiskPdf.Enabled = bd_file.IsActionAllowed(en_Actions.Export_PDF);

					//Send by email
					menuVersionSendEmail.Enabled = bd_file.IsActionAllowed(en_Actions.SendByEmail);

					//Send by email as PDF
					menuVersionSendEmailPdf.Enabled = bd_file.IsActionAllowed(en_Actions.SendByEmail_PDF);

					//rollback version
					if (dtgVersions.RowCount == 1 ||
						GetDocumentFromRowVer().version == DocumentController.GetDocument(bd_file.id).version ||
						bd_file.IsActionAllowed(en_Actions.Rollback) == false)
					{
						menuVersionRollback.Enabled = false;

					}
					else
					{
						menuVersionRollback.Enabled = true;
					}
				}
			}
			catch (Exception error)
			{
				MessageBox.Show(lib.msg_error_file_menu, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
		}

		//---------------------------------------------------------------------------------
		private void dtgVersions_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
		{
			logger.Trace("Begin");
			openDocReadonly(GetDocumentFromRowVer());
		}

		//---------------------------------------------------------------------------------
		// Events (Menu) ------------------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void menuDtgVersionFiles_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			logger.Trace("Begin");
			menuDtgVersionFiles.Close();
		}

		//---------------------------------------------------------------------------------
		private void menuVersionSendEmail_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			VersionSendEmail(false);
		}

		//---------------------------------------------------------------------------------
		private void menuVersionSendEmailPdf_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			VersionSendEmail(true);
		}

		//---------------------------------------------------------------------------------
		private void menuVersionOpen_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			openDocReadonly(GetDocumentFromRowVer());
		}

		//---------------------------------------------------------------------------------
		private void menuVersionExportHardDisk_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			menuDtgVersionFiles.Close();
			VersionExportHardDisk(false);
		}

		//---------------------------------------------------------------------------------
		private void menuVersionExportHardDiskPdf_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			VersionExportHardDisk(true);
		}

		//---------------------------------------------------------------------------------
		private void menuVersionExportWorkSpace_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			//check if file already existis
			string pathDest = FileFolder.GetAvailableFileName(FileFolder.GetUserFolder() + dtgBdFiles.GetSelectedDocument().title);
			GetDocumentFromRowVer().Save(pathDest);

			MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);
		}

		//---------------------------------------------------------------------------------
		private void rollbackToolStripMenuItem_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			DialogResult result = (MessageBox.Show("Are you sure you want to rollback to this version?", "Spider Docs", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

			if (result == DialogResult.Yes)
				rollBackVersion();
		}

		//---------------------------------------------------------------------------------
		private void menuVersionDms_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			DmsFile.SaveDmsFile(GetDocumentFromRowVer(), "");
		}

		//---------------------------------------------------------------------------------
		private void menuVersionSendEmailDms_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			List<Document> docs = new List<Document>();
			docs.Add(GetDocumentFromRowVer());

			DmsFile.MailDmsFile(docs, "", "");
		}

		//---------------------------------------------------------------------------------
		// Functions (For DataGridView) ---------------------------------------------------
		//---------------------------------------------------------------------------------
		private Document GetDocumentFromRowVer()
		{
			logger.Trace("Begin");
			Document CurrentDoc = dtgBdFiles.GetSelectedDocument();
			Document VersionDoc = dtgVersions.GetSelectedDocument();

			CurrentDoc.id_version = VersionDoc.id_version;
			CurrentDoc.version = VersionDoc.version;

			return CurrentDoc;
		}

		//---------------------------------------------------------------------------------
		private int GetVersionRowIdxById(int version_id)
		{
			logger.Trace("Begin");
			int ans = -1;

			foreach (DataGridViewRow row in dtgVersions.Rows)
			{
				if (Convert.ToInt32(row.Cells["id_version"].Value) == version_id)
				{
					ans = row.Index;
					break;
				}
			}

			return ans;
		}

		//---------------------------------------------------------------------------------
		// Functions (For Menu) -----------------------------------------------------------
		//---------------------------------------------------------------------------------
		void VersionExportHardDisk(bool pdf)
		{
			logger.Trace("Begin");
			try
			{
				ExportFiles(pdf, GetDocumentFromRowVer());
			}
			catch (Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default + lib.msg_error_description + error.Message, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//---------------------------------------------------------------------------------
		void VersionSendEmail(bool pdf)
		{
			logger.Trace("Begin");
			try
			{
				Document selected_doc = dtgBdFiles.GetSelectedDocument();
				string tempPath = FileFolder.TempPath + selected_doc.title;

				selected_doc = GetDocumentFromRowVer();
				selected_doc = DocumentController.GetDocument(selected_doc.id, id_version: selected_doc.id_version, data: true);
				selected_doc.Save(tempPath);

				if (pdf)
					tempPath = PDFConverter.pdfconversion(tempPath, FileFolder.TempPath);

				// open outlook
				List<string> path = new List<string>();
				path.Add(tempPath);

				new Email().OpenNewEmail(selected_doc.filenameWithExt, "", path);

				//register historic
				selected_doc.id_event = EventIdController.GetEventId(en_Events.Email);
				HistoryController.SaveDocumentHistoric(null, selected_doc);

			}
			catch (Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default + lib.msg_error_description + error.Message, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//---------------------------------------------------------------------------------
		private void rollBackVersion()
		{
			logger.Trace("Begin");
			Document doc = dtgBdFiles.GetSelectedDocument();
			string reason = "Rollback to a previous version";

			bool ans = true;

			if (SpiderDocsApplication.CurrentPublicSettings.reasonNewVersion)
			{
				frmReasonNewVersion form = new frmReasonNewVersion(doc);
				form.ShowDialog();
				ans = form.result;
				reason = form.reason;
			}

			if (ans)
			{
				Document LatestDoc = dtgBdFiles.GetSelectedDocument();
				Document SelectedDoc = GetDocumentFromRowVer();

				string wrk = DocumentController.RollBackVersion(SelectedDoc.id, SelectedDoc.id_version, LatestDoc.version, reason);
				refreshDbGrids();
				search(dtgBd_SearchMode.RecentDocuments);

				if (!String.IsNullOrEmpty(wrk))
				{
					//Utilities.regLog(wrk);
					logger.Error(wrk);
					MessageBox.Show(lib.msg_error_default + lib.msg_error_description + ans, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		//---------------------------------------------------------------------------------
		void UpdateVersionGridView()
		{
			logger.Trace("Begin");
			tbDbFiles.Update();
			dtgVersions.Update();
		}

		//---------------------------------------------------------------------------------
		#endregion


		/*
		 * Work Space - SidePanel
		 */
		#region WorkSpace - SidePanel
		public enum dtgBd_SearchMode
		{
			Normal,
			RecentDocuments
		}
		//---------------------------------------------------------------------------------
		enum en_cboReviewVal
		{
			NA = 0,
			Unreviewed,
			Reviewed,
			UnreviewedAll,
			ReviewedAll,

			Max
		}

		//---------------------------------------------------------------------------------
		List<string> sqlWhereLs = new List<string>();
		List<string> sqlOrderLs = new List<string>();
		string[] sqlFullText = new string[2];

		//---------------------------------------------------------------------------------
		// Panel Events (buttons) ---------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			search(dtgBd_SearchMode.RecentDocuments);
		}

		//---------------------------------------------------------------------------------
		private void btnClear_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			ClearAllCriterias();

			click_search_button();
		}

		//---------------------------------------------------------------------------------
		private void pbDbGridFull_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			dtgBdFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			splitContainer.SplitterDistance = splitContainer.Size.Width;
			SpiderDocsApplication.WorkspaceGridsizeSettings.db_grid_full = true;
			SpiderDocsApplication.WorkspaceGridsizeSettings.local_grid_full = false;
			SpiderDocsApplication.WorkspaceGridsizeSettings.Save();
		}

		//---------------------------------------------------------------------------------
		private void pbLocalGridFull_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			dtgLocalFile.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			splitContainer.SplitterDistance = 0;
			SpiderDocsApplication.WorkspaceGridsizeSettings.db_grid_full = false;
			SpiderDocsApplication.WorkspaceGridsizeSettings.local_grid_full = true;
			SpiderDocsApplication.WorkspaceGridsizeSettings.Save();
		}

		//---------------------------------------------------------------------------------
		private void pbShowBothSides_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			dtgBdFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dtgLocalFile.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			splitContainer.SplitterDistance = (splitContainer.Size.Width / 2);
			SpiderDocsApplication.WorkspaceGridsizeSettings.db_grid_full = false;
			SpiderDocsApplication.WorkspaceGridsizeSettings.local_grid_full = false;
			SpiderDocsApplication.WorkspaceGridsizeSettings.Save();
		}

		//---------------------------------------------------------------------------------
		// Panel Events (search criteria) -------------------------------------------------
		//---------------------------------------------------------------------------------
		private void txtId_KeyPress(object sender, KeyPressEventArgs e)
		{
			logger.Trace("Begin");
			if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
				e.Handled = true;
		}

		//---------------------------------------------------------------------------------
		private void cbo_KeyDown(object sender, KeyEventArgs e)
		{
			logger.Trace("Begin");
			if (e.KeyCode == Keys.Delete)
				e.Handled = true;
		}

		//---------------------------------------------------------------------------------
		private void cboDocType_CheckBoxCheckedChanged(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			List<int> varIds = cboDocType.getComboValue<DocumentAttributeCombo>().Select(a => a.id).ToList();

			if (0 < varIds.Count)
				attributeSearch.populateGrid(varIds.ToArray());
			else
				attributeSearch.populateGrid();
		}

		//---------------------------------------------------------------------------------
		// Panel Events (search criteria - Dates) -----------------------------------------
		//---------------------------------------------------------------------------------
		private void dt_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			int x;
			string CalendarStr = "";

			if (sender == dtBegin)
			{
				CalendarStr = "calendar_dtBegin";
				x = 68;

			}
			else
			{
				CalendarStr = "calendar_dtEnd";
				x = 156;
			}

			MaskedTextBox mask = ((MaskedTextBox)sender);
			MonthCalendar cld = (MonthCalendar)panelLeft.Controls[CalendarStr];

			if (cld == null)
			{
				MonthCalendar calendar = new MonthCalendar();
				calendar.Location = new System.Drawing.Point(mask.Location.X - x, mask.Location.Y + 18);
				calendar.Name = CalendarStr;
				calendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(calendar_DateSelected);
				panelLeft.Controls.Add(calendar);
				calendar.BringToFront();

			}
			else
			{
				panelLeft.Controls.RemoveByKey(CalendarStr);
			}
		}

		//---------------------------------------------------------------------------------
		private void dt_LostFocus(object sender, System.EventArgs e)
		{
			logger.Trace("Begin");
			string CalendarStr = "";

			if (sender == dtBegin)
				CalendarStr = "calendar_dtBegin";
			else
				CalendarStr = "calendar_dtEnd";

			try
			{
				MonthCalendar cld = (MonthCalendar)panelLeft.Controls[CalendarStr];

				if (cld != null)
				{
					if ((cld.ContainsFocus) == false)
						panelLeft.Controls.Remove(cld);
				}
			}
			catch (Exception error)
			{
				logger.Error(error);
			}
		}

		//---------------------------------------------------------------------------------
		void dtEnd_KeyDown(object sender, KeyEventArgs e)
		{
			logger.Trace("Begin");
			toolTipats.Hide(dtEnd);
		}

		//---------------------------------------------------------------------------------
		private void calendar_DateSelected(object sender, DateRangeEventArgs e)
		{
			logger.Trace("Begin");
			string dd, mm, yyyy, date;
			MonthCalendar calendar = (MonthCalendar)sender;

			dd = calendar.SelectionStart.Day.ToString();
			mm = calendar.SelectionStart.Month.ToString();
			yyyy = calendar.SelectionStart.Year.ToString();

			if (calendar.SelectionStart.Day.ToString().Length == 1)
				dd = "0" + calendar.SelectionStart.Day.ToString();

			if (calendar.SelectionStart.Month.ToString().Length == 1)
				mm = "0" + calendar.SelectionStart.Month.ToString();

			date = dd + mm + yyyy;

			MaskedTextBox maskTxt = (MaskedTextBox)this.tabPage2.Controls[calendar.Name.Replace("calendar_", "")];
			maskTxt.Text = date;

			panelLeft.Controls.Remove(calendar);
		}

		//---------------------------------------------------------------------------------
		private bool dateValidated()
		{
			logger.Trace("Begin");
			if (dtBegin.Text != Library.INVALID_DATE)
			{
				try
				{
					DateTime.ParseExact(dtBegin.Text, ConstData.DATE, CultureInfo.InvariantCulture);
				}
				catch (Exception)
				{
					dtBegin.Focus();
					dtBegin.BackColor = Color.LavenderBlush;
					toolTipats.Show(lib.msg_date_format, dtBegin, dtBegin.Location.X - 90, dtBegin.Location.Y - 210, 4000);
					return true;
				}

				dtBegin.BackColor = Color.White;
			}

			dtBegin.BackColor = SystemColors.Window;

			if (dtEnd.Text != Library.INVALID_DATE)
			{
				try
				{
					Convert.ToDateTime(dtEnd.Text);
				}
				catch (Exception)
				{
					dtEnd.Focus();
					dtEnd.BackColor = Color.LavenderBlush;
					toolTipats.Show(lib.msg_date_format, dtEnd, dtEnd.Location.X - 150, dtEnd.Location.Y - 210, 4000);
					return true;
				}

				dtEnd.BackColor = Color.White;
			}

			dtEnd.BackColor = SystemColors.Window;

			return false;
		}

		//---------------------------------------------------------------------------------
		// Panel Events (add new search criteria to here) -----------------------------------------
		//---------------------------------------------------------------------------------
		private void ClearAllCriterias()
		{
			logger.Trace("Begin");
			this.txtId.Text = "";
			this.txtKeyWord.Text = "";
			//cboFolder.ClearSelection(); FolderExplorer Ammendement
			cboFolder.SelectedValue = 0;
			this.txtTitle.Text = "";
			this.dtBegin.Text = "";
			this.dtEnd.Text = "";
			cboAuthor.ClearSelection();
			cboExtension.ClearSelection();
			cboDocType.ClearSelection();

			if (0 < cboDocType.Items.Count)
				cboDocType.ClearSelection();//cboDocType.SelectedIndex = 0;

			if (0 < cboReview.Items.Count)
				cboReview.SelectedIndex = 0;

			attributeSearch.populateGrid();
		}

		//---------------------------------------------------------------------------------
		// Populate Controls --------------------------------------------------------------
		//---------------------------------------------------------------------------------
		public void populateComboFolder()
		{
			logger.Trace("Begin");
			/*
			List<Folder> folders = FolderController.GetFolders(true, PermissionController.GetAssignedFolderToUser().ToArray());
			cboFolder.DataSource = new ListSelectionWrapper<Folder>(folders, "document_folder", ",");
			cboFolder.ValueMember = "Selected";
			cboFolder.DisplayMemberSingleItem = "Name";
			cboFolder.DisplayMember = "NameConcatenated";
			*/
			/*
			FolderController.GetFolders(true, PermissionController.GetAssignedFolderToUser().ToArray()).ForEach(u =>
			{


				cboFolder.AddItem(new DocumentAttributeCombo()
				{
					id = u.id,
					//id_atb { set; get; }
					text = u.document_folder,
					Selected = false
				}, false);

			});FolderExplorer Ammendement
			*/
		}

		//---------------------------------------------------------------------------------
		public void populateComboExtension()
		{
			logger.Trace("Begin");
            /*
			List<string> items = DocumentController.GetExistingExtensions();
			cboExtension.DataSource = new ListSelectionWrapper<string>(items, "", ",");
			cboExtension.ValueMember = "Selected";
			cboExtension.DisplayMemberSingleItem = "Name";
			cboExtension.DisplayMember = "NameConcatenated";
			*/

            cboExtension.Clear();

            int i = 0;
            DocumentController.GetExistingExtensions().ForEach(u =>
			{
				cboExtension.AddItem(new DocumentAttributeCombo()
				{
					id = i++,
					//id_atb { set; get; }
					text = u,
					Selected = false
				}, false);
			});
		}

		//---------------------------------------------------------------------------------
		public void populateComboAuthor()
		{
			logger.Trace("Begin");
            /*
			List<User> users = UserController.GetUser(true, false, true);
			cboAuthor.DataSource = new ListSelectionWrapper<User>(users, "name", ",");
			cboAuthor.ValueMember = "Selected";
			//cboAuthor.DisplayMemberSingleItem = "Name";
			cboAuthor.DisplayMember = "NameConcatenated";
			//cboComboBox.Text = cboComboBox.SelectedItemsText;

			*/
            cboAuthor.Clear();
            UserController.GetUser(true, false).ForEach(u =>
			{
				cboAuthor.AddItem(new DocumentAttributeCombo()
				{
					id = u.id,
					//id_atb { set; get; }
					text = u.name,
					Selected = false
				}, false);
			});
		}

		//---------------------------------------------------------------------------------
		public void populateComboDocType()
		{
			logger.Trace("Begin");
            /*
			List<DocumentType> doc_types = DocumentTypeController.DocumentType(true);
			cboDocType.DataSource = new ListSelectionWrapper<DocumentType>(doc_types, "type", ",");
			cboDocType.ValueMember = "Selected";
			cboDocType.DisplayMemberSingleItem = "Name";
			cboDocType.DisplayMember = "NameConcatenated";
			*/
            cboDocType.Clear();
            DocumentTypeController.DocumentType(true).ForEach(u =>
			{
				cboDocType.AddItem(new DocumentAttributeCombo()
				{
					id = u.id,
					//id_atb { set; get; }
					text = u.type,
					Selected = false
				}, false);
			});

		}

		//---------------------------------------------------------------------------------
		// Search -------------------------------------------------------------------------
		//---------------------------------------------------------------------------------
		private void btnSearch_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			click_search_button();
		}

		private void click_search_button()
		{
			logger.Trace("Begin");
			//set up search criteria from UI
			SearchCriteria Criteria = new SearchCriteria();

			// Document Id
			string wrk;

			table.Criteria.Clear();

			Criteria.StrDocIds = txtId.Text;

			// Keyword
			wrk = txtKeyWord.Text;
			if (!String.IsNullOrEmpty(wrk))
				Criteria.Keywords.Add(wrk);

			// Title
			wrk = txtTitle.Text;
			if (!String.IsNullOrEmpty(wrk))
				Criteria.Titles.Add(wrk);

			//Criteria.FolderIds = cboFolder.getComboValue<DocumentAttributeCombo>().Select(a => a.id).ToList(); // Folder Folder Explorer Ammendement
			if (cboFolder.SelectedValue != null && (int)cboFolder.SelectedValue > 0)
				Criteria.FolderIds = new List<int> { (int)cboFolder.SelectedValue };

			// Date
			Period Date = new Period();

			if (dtBegin.Text != Library.INVALID_DATE)
				Date.SetDateFromString(from: dtBegin.Text);

			if (dtEnd.Text != Library.INVALID_DATE)
				Date.SetDateFromString(to: dtEnd.Text);

			if ((Date.From != new DateTime()) || (Date.To != new DateTime()))
				Criteria.Date.Add(Date);

			Criteria.UserIds = cboAuthor.getComboValue<DocumentAttributeCombo>().Select(a => a.id).ToList(); // Auther
			Criteria.Extensions = cboExtension.getComboValue<DocumentAttributeCombo>().Select(a => a.text).ToList(); // Extension
			Criteria.DocTypeIds = cboDocType.getComboValue<DocumentAttributeCombo>().Select(a => a.id).ToList(); // Document Type

			// Review
			if ((en_cboReviewVal)cboReview.SelectedIndex != en_cboReviewVal.NA)
			{
				if (((en_cboReviewVal)cboReview.SelectedIndex == en_cboReviewVal.Unreviewed) || ((en_cboReviewVal)cboReview.SelectedIndex == en_cboReviewVal.UnreviewedAll))
					Criteria.Review.Status = en_ReviewStaus.UnReviewed;
				else
					Criteria.Review.Status = en_ReviewStaus.Reviewed;

				if (((en_cboReviewVal)cboReview.SelectedIndex == en_cboReviewVal.Unreviewed) || ((en_cboReviewVal)cboReview.SelectedIndex == en_cboReviewVal.Reviewed))
					Criteria.Review.UserIds.Add(SpiderDocsApplication.CurrentUserId);
			}
			else
			{
				Criteria.Review.Status = en_ReviewStaus.INVALID;
			}

			// Attributes
			foreach (DocumentAttribute attr in attributeSearch.getAttributeValues())
				Criteria.AttributeCriterias.Add(attr);

			Criteria = excludeArchive(Criteria);

			// set criteria to DTS_Document class
			table.Criteria.Add(Criteria);

			search(dtgBd_SearchMode.Normal);
		}

		//---------------------------------------------------------------------------------
		private void search(dtgBd_SearchMode mode, int id_docToSelect = 0)
		{
			logger.Trace("Begin");
			if (dateValidated())
				return;

			btnSearch.Enabled = false;
			btnRefresh.Enabled = false;

            //table.Delete();

            if (mode == dtgBd_SearchMode.RecentDocuments)
			{
				SearchCriteria criteria = new SearchCriteria();
				criteria = excludeArchive(criteria);
				table.SetRecentDocumentsCriteria(criteria);
			}
			//selectedSearchTab = tabControlSearch.SelectedIndex;
			table.CustomColumnId = SpiderDocsApplication.WorkspaceCustomize.c_atb_id;

			BackgroundWorker thread_search = new BackgroundWorker();
			thread_search.DoWork += new DoWorkEventHandler(thread_search_DoWork);
			thread_search.RunWorkerCompleted += new RunWorkerCompletedEventHandler(thread_search_WorkDone);
            thread_search.RunWorkerAsync(id_docToSelect);

			PrevSearchMode = mode;
		}

		//---------------------------------------------------------------------------------
		void thread_search_DoWork(object sender, DoWorkEventArgs e)
		{
			logger.Trace("Begin");
			//show loading gif on the serach button
			Invoke(new threadFunction(showLoadingSearch));

			endPopuletedGridVersion = false;

			table.Select();
			dtSearch = table.GetDataTable();

            e.Result = (int)e.Argument; //idDocSelect

        }

		//---------------------------------------------------------------------------------
		void thread_search_WorkDone(object sender, RunWorkerCompletedEventArgs e)
		{
			logger.Trace("Begin");
			if (e.Error == null)
			{
				//populate grid
				dtgBdFiles.DataSource = dtSearch;

				if (AfterloadRecentFiles != null)
				{
					AfterloadRecentFiles();
					AfterloadRecentFiles = null;
					AfterloadRecentFiles_Args.Clear();
				}

                if ((int)e.Result > 0)
                {
                    var fond = dtSearch.Select(" id = " + (int)e.Result).FirstOrDefault();
                    if (fond != null)
                    {
                        dtgBdFiles.ClearSelection();
                        for ( int i =0; i< dtgBdFiles.Rows.Count; i++)
                        {
                            System.Windows.Forms.DataGridViewRow row = dtgBdFiles.Rows[i];
                            if( (int)row.Cells["c_id_doc"].Value == (int)e.Result)
                            {
                                row.Selected = true;
                            }
                        }
                        //dtgBdFiles.Rows
                        //    .OfType<DataGridViewRow>()
                        //     .Where(x => (int)x.Cells["c_id_doc"].Value == (int)e.Result)
                        //     .ToArray<DataGridViewRow>()[0]
                        //     .Selected = true;
                    }
                }
            }
			else
			{
				logger.Error(e.Error,"System Error");
				//SpiderDocsModule.Utilities.regLog(e.Error.ToString());
				MessageBox.Show(lib.msg_error_search, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			//show components
			btnSearch.Enabled = true;
			btnRefresh.Enabled = true;
			pboxLoadingSearch.Visible = false;
		}

		//---------------------------------------------------------------------------------
		private void showLoadingSearch()
		{
			logger.Trace("Begin");
			pboxLoadingSearch.Visible = true;
		}

		//---------------------------------------------------------------------------------
		private void showLoadingRecent()
		{
			logger.Trace("Begin");
		}

		//---------------------------------------------------------------------------------
		void attributeSearch_KeyDown(object sender, KeyEventArgs e)
		{
			logger.Trace("Begin");
			click_search_button();
		}
		//---------------------------------------------------------------------------------
		SearchCriteria excludeArchive(SearchCriteria criteria)
		{
			logger.Trace("Begin");
			if (SpiderDocsApplication.CurrentUserGlobalSettings.exclude_archive)
			{
				criteria.ExcludeStatuses.Add(en_file_Status.archived);
				criteria.ExcludeStatuses.Add(en_file_Status.deleted);
			}
			return criteria;
		}
		#endregion



		/*
		 * Work Space - TreeView
		 */
		#region WorkSpace - TreeView
		//---------------------------------------------------------------------------------
		struct st_SelectedTxt
		{
			public int Level;
			public string text_L0;
			public string text_L1;
			public string text_L2;
		}

        //---------------------------------------------------------------------------------
        public delegate void Add(tv.MultiSelectTreeview treeView, TreeNode node);
        public delegate void Sort();
		public void Add1(TreeNode node)
		{
			logger.Trace("Begin");
			treeViewMSExplorer.Nodes.Add(node);
		}

		private string GetUIAttrName(en_AttrType type, DocumentAttribute attr ,List<DocumentAttributeCombo> db)
		{
			logger.Trace("Begin");
			if( !DocumentAttribute.IsComboTypes(attr.id_type)) return attr.atbValue_str;

			var hit = db.FirstOrDefault(a => ((List<int>)attr.atbValue).Contains(a.id));
			if (hit == null) return attr.atbValue_str;

			return hit.text;
		}

		// private void treeViewMSExplorer_Clear()
		// {
		// 	logger.Trace("Begin");
		// 	if (treeViewMSExplorer.InvokeRequired)
		// 	{
		// 		treeViewMSExplorer.Invoke(new MethodInvoker(treeViewMSExplorer_Clear));
		// 		return;
		// 	}

		// 	treeViewMSExplorer.Nodes.Clear();
		// }

		// private void treeViewMSExplorer_SuspendLayout()
		// {
		// 	logger.Trace("Begin");
		// 	if (treeViewMSExplorer.InvokeRequired)
		// 	{
		// 		treeViewMSExplorer.Invoke(new MethodInvoker(treeViewMSExplorer_SuspendLayout));
		// 		return;
		// 	}

		// 	treeViewMSExplorer.SuspendLayout();
		// }


		public void Add2(tv.MultiSelectTreeview treeview, TreeNode node)
		{
            //logger.Trace("Begin");
            treeview.Nodes.Add(node);
		}

		private void treeView_Clear(tv.MultiSelectTreeview treeview )
		{
			logger.Trace("Begin");
			if (treeview.InvokeRequired)
			{
                treeview.Invoke((MethodInvoker)delegate { treeView_Clear(treeview); });
                return;
			}

            treeview.Nodes.Clear();
		}

		private void treeView_SuspendLayout(tv.MultiSelectTreeview treeview)
		{
			logger.Trace("Begin");
			if (treeview.InvokeRequired)
			{
                treeview.Invoke((MethodInvoker) delegate { treeView_SuspendLayout(treeview); });
				return;
			}

            treeview.SuspendLayout();
		}


        public void Add3(TreeNode node)
        {
            this.treeViewArchivedFolder.Nodes.Add(node);
        }

        private void treeViewArchiveFolderExplore_Clear()
        {
            logger.Trace("Begin");
            if (treeViewArchivedFolder.InvokeRequired)
            {
                treeViewArchivedFolder.Invoke(new MethodInvoker(treeViewArchiveFolderExplore_Clear));
                return;
            }

            treeViewArchivedFolder.Nodes.Clear();
        }

        private void treeViewArchiveFolderExplore_SuspendLayout()
        {
            logger.Trace("Begin");

            if (treeViewArchivedFolder.InvokeRequired)
            {
                treeViewArchivedFolder.Invoke(new MethodInvoker(treeViewArchiveFolderExplore_SuspendLayout));
                return;
            }

            treeViewArchivedFolder.SuspendLayout();
        }


        private void TreeView_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            logger.Trace("Begin");

            List<Folder> folders;

            var treeview = ((tv.MultiSelectTreeview)sender);

            var node = e.Node;
            Folder root = (node.Tag as Folder);

			switch (treeview.Name)
			{
				case "treeViewFolderExplore":
					folders = PermissionController.GetAssignedFolderLevel1(root.id, SpiderDocsApplication.CurrentUserId, en_Actions.OpenRead).OrderBy(x => x.id_parent).ThenByDescending(x => x.document_folder).ToList();
					break;

				case "treeViewArchivedFolder":
					folders = PermissionController.GetArchiveFolderLevel1(root.id).OrderBy(x => x.id_parent).ThenByDescending(x => x.document_folder).ToList();
					break;

				default:
					logger.Error("New exploere found: {0}", treeview.Name);
					throw new NotImplementedException("New exploere found: " + treeview.Name);
			}

			node.Nodes.Clear();
            try
            {
			    List<Folder> children = ChildrenBy(folders, root.id);
			    AddChildFolderNodes(node, children, folders);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            //// TreeNode node = e.Node;
            //// Folder root = (node.Tag as Folder);
            //// List<Folder> folders = PermissionController.GetAssignedFolderLevel1(root.id, SpiderDocsApplication.CurrentUserId, (int)en_Actions.CheckIn_Out).OrderBy(x => x.id_parent).ThenByDescending(x => x.document_folder).ToList();

            //// node.Nodes.Clear();

            //// List<Folder> children = ChildrenBy(folders, root.id);
            //// AddChildFolderNodes(node, children, folders);
            //// //await Task.Run(() => { AddChildFolderNodes(node, children, folders); });

            //if (!isFolderExpanding) treeViewFolderExplore_NodeMouseDoubleClick(null, new TreeNodeMouseClickEventArgs(e.Node,new MouseButtons(),0,0,0));

        }

        private void treeViewFolderExplore_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //isFolderExpanding = true;
            try
            {
                OpenTreeViewAt(e.Node);
            }
            catch { }
            finally
            {
                ///isFolderExpanding = false;
            }

            // TreeNode node = e.Node;
            // Folder root = (node.Tag as Folder);
            // List<Folder> folders = PermissionController.GetAssignedFolderLevel1(root.id, SpiderDocsApplication.CurrentUserId, (int)en_Actions.CheckIn_Out).OrderBy(x => x.id_parent).ThenByDescending(x => x.document_folder).ToList();

            // node.Nodes.Clear();

            // List<Folder> children = ChildrenBy(folders, root.id);
            // AddChildFolderNodes(node, children, folders);

            // e.Node.Expand();
        }

		void OpenTreeViewAt(TreeNode node )
		{
            Folder root = (node.Tag as Folder);

            List<Folder> folders = PermissionController.GetAssignedFolderLevel1(root.id, SpiderDocsApplication.CurrentUserId, en_Actions.OpenRead).OrderBy(x => x.id_parent).ThenByDescending(x => x.document_folder).ToList();

            node.Nodes.Clear();

            List<Folder> children = ChildrenBy(folders, root.id);
            AddChildFolderNodes(node, children, folders);

            node.Expand();
		}

        public void loadFolderTreeView(object args)
		{
            logger.Trace("Begin");
            try
			{
                int id_default = 0;
                tv.MultiSelectTreeview treeView = null;

                if (args != null)
                {
                    treeView = (tv.MultiSelectTreeview) ((object[])args)[0];
                    id_default = (int)((object[])args)[1];
                }

                treeView_SuspendLayout(treeView);

				List<Folder> Folders = PermissionController.GetAssignedFolderLevel1(0, SpiderDocsApplication.CurrentUserId, en_Actions.OpenRead, false);

                AddParents(id_default, Folders, en_Actions.OpenRead, false);

                treeView.ContextMenuStrip = null;

                // Nothing to do if you cannot see any folders
                if (Folders.Count == 0) return;

				treeView_Clear(treeView);

                // Folders Level
                Folders = Folders.OrderBy(x => x.id_parent).ThenByDescending(x => x.document_folder).ToList();

                List<Folder> roots = ChildrenBy(Folders, 0);
                foreach (Folder root in roots)
				{
					/*
					if (FolderController.CountDoc(root.id) <= 0)
						continue;
					*/
					string name_folder = root.document_folder;

					if (UserGlobalSettings.IsDevelopper) name_folder += string.Format(" ({0})", root.id);

                    TreeNode node = new TreeNode(name_folder) { Tag = root, Name = root.id.ToString() };
                    node.Tag = root;

                    List<Folder> children = ChildrenBy(Folders,root.id);

					AddChildFolderNodes(node,children,Folders);

                    treeView.Invoke(new Add(Add2), new object[] { treeView,node });
                }

                if( id_default > 0)
                {
                    ExpandFolderTo(treeView, id_default);
                }

                treeView.ResumeLayout();
            }
            catch(ThreadAbortException) {}
            catch (Exception ex)
			{
				logger.Error(ex, "Error");
			}
        }


        /// <summary>
        /// Expand folders with no event trigger forward
        /// </summary>
        void ExpandFolderTo(tv.MultiSelectTreeview treeview, int id_folder  = 0)
        {

            List<int> drillup = new List<int>();
            int searchId = id_folder;
            TreeNode node;


            try
            {
                /*
                 * Stores all parent node's folder id
                 * Loop until reaching to the root. Root does't have parent so just ignore.
                 */
                drillup.Add(searchId);
                while ((node = treeview.Nodes.Find(searchId.ToString(), true).FirstOrDefault()) != null
                        && node.Parent != null)
                {
                    drillup.Add(searchId = int.Parse(node.Parent.Name));
                }


                // Search node from Top to Bottom and select a folder programatically.
                for ( int i = drillup.Count() -1 ; i >= 0; i--)
                {

                    node = treeview.Nodes.Find(drillup[i].ToString(), true).FirstOrDefault();

                    if (node != null)
                    {
                        treeview.SelectedNode = node;

                        // Last folder is just select.
                        if ( i > 0 )
                            treeview.SelectedNode.Expand();
                    }

                }


				// Ensure last node is visible
				if( node != null)
					node.EnsureVisible();


            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

		/// <summary>
		/// Appends folders that from id_default until root.
		/// </summary>
		/// <param name="id_default"></param>
		/// <param name="folderSouce"></param>
		/// <param name="permission"></param>
        void AddParents(int id_default, List<Folder> folderSouce, en_Actions permission = en_Actions.OpenRead, bool archived = false)
        {

            List<Folder> ancestorFolders = PermissionController.drillUpfoldersby(id_default, SpiderDocsApplication.CurrentUserId, permission, archived);
            List<Folder> willbeAddd = ancestorFolders.Where(x =>
                                                                    // Exclude root folders
                                                                    x.id_parent != 0

                                                                    // Not exists in the source
                                                                    && !folderSouce.Exists(xx => xx.id == x.id))
                                                        .ToList();

            // Get folders on same layer.
            int id_parent = ancestorFolders.Find(x => x.id == id_default)?.id_parent ?? 0;
            if (id_parent > 0)
            {
                willbeAddd.AddRange(PermissionController.GetAssignedFolderLevel1(id_parent, SpiderDocsApplication.CurrentUserId, permission, archived)
                            .Where(x =>
                                        // Exclude root folders
                                        x.id_parent != 0

                                        // Not exists in the source
                                        && !folderSouce.Exists(xx => xx.id == x.id))
                            .ToList());
            }

            if (willbeAddd.Count > 0) folderSouce.AddRange(willbeAddd);

            //If nodes are added then dummy on the same layer should be removed.
            RemoveDummyNodes(ref folderSouce);
        }

        void RemoveDummyNodes(ref List<Folder> FolderSouce)
        {
            int[] idParents = FolderSouce.Select(x => x.id_parent).ToArray();

			// Actual removing logic. Remove dummy folders on each of layers.
			foreach (int id in idParents)
            {
                if ( FolderSouce.Count(x => x.id_parent == id) >= 2 )
				{
                    FolderSouce.RemoveAll(rem =>

											rem.id_parent == id

											&& rem.id == -9999

											&& rem.document_folder == "dummy");
				}
            }

        }


        public void loadArchiveFolderTreeView(object args)
		{
            logger.Trace("Begin");
            try
			{
                int id_default = 0;
                tv.MultiSelectTreeview treeView = null;

                if (args != null)
                {
                    treeView = (tv.MultiSelectTreeview) ((object[])args)[0];
                    id_default = (int)((object[])args)[1];
                }

                treeView_SuspendLayout(treeView);

				List<Folder> Folders =  PermissionController.GetArchiveFolderLevel1(0);

                AddParents(id_default, Folders, en_Actions.OpenRead, true);

                treeView.ContextMenuStrip = null;

                // Nothing to do if you cannot see any folders
                if (Folders.Count == 0) return;

				treeView_Clear(treeView);

                // Folders Level
                Folders = Folders.OrderBy(x => x.id_parent).ThenByDescending(x => x.document_folder).ToList();
				var rootId = LastParentBy(Folders, id_default);

                List<Folder> roots = ChildrenBy(Folders, rootId ); // TODo
                foreach (Folder root in roots)
				{
					/*
					if (FolderController.CountDoc(root.id) <= 0)
						continue;
					*/
					string name_folder = root.document_folder;

					if (UserGlobalSettings.IsDevelopper) name_folder += string.Format(" ({0})", root.id);

                    TreeNode node = new TreeNode(name_folder) { Tag = root, Name = root.id.ToString() };
                    node.Tag = root;

                    List<Folder> children = ChildrenBy(Folders,root.id);

					AddChildFolderNodes(node,children,Folders);

                    treeView.Invoke(new Add(Add2), new object[] { treeView,node });
                }

                if( id_default > 0)
                {
                    ExpandFolderTo(treeView, id_default);
                }

                treeView.ResumeLayout();
            }
            catch(ThreadAbortException) {}
            catch (Exception ex)
			{
				logger.Error(ex, "Error");
			}
        }

        /// <summary>
        /// Search folders that has same parent
        /// </summary>
        /// <param name="db">folders for searching. must be SORTED</param>
        /// <param name="id_parent">the id for parent</param>
        /// <returns></returns>
        List<Folder> ChildrenBy(List<Folder> db, int id_parent)
        {
            List<Folder> ans = new List<Folder>();

            int idx = db.FindLastIndex(x => x.id_parent == id_parent);

            if (idx == -1) return new List<Folder>();

            for (int i = idx; i >= 0; i--)
            {
                if (db[i].id_parent != id_parent)
                    break;

                ans.Add(db[i]);

                db.RemoveAt(i);
            }
            return ans;
        }

        /// <summary>
        /// Search folder id until parent is not found
        /// </summary>
        /// <param name="db">folders for searching.</param>
        /// <param name="id_folder">the id for start</param>
        /// <returns></returns>
        int LastParentBy(List<Folder> db, int id_folder)
        {
            List<Folder> ans = new List<Folder>();
			var searchID= id_folder;

			var folder = new Folder();
            do
            {
                folder = db.Find(x => x.id == searchID);

                if (folder != null)
                    searchID = folder.id_parent;
                else break;

            } while (folder.id_parent > 0);

            return searchID;
        }

		void AddChildFolderNodes(TreeNode node , List<Folder> children, List<Folder> database, bool enabletooltips = false)
		{
            //logger.Trace("Begin");

            children.ForEach(fld =>
            {
                string name_folder = fld.document_folder;

                if (UserGlobalSettings.IsDevelopper) name_folder += string.Format(" ({0})", fld.id);

                TreeNode me = new TreeNode(name_folder) { Tag = fld, Name = fld.id.ToString() };
                me.Tag = fld;
                if( enabletooltips)  me.ContextMenuStrip = contextMenuStripFolderViewOption2;
                node.Nodes.Add(me);

                // add child
                if (fld.id_parent > 0)
                {
                    List<Folder> _children = ChildrenBy(database, fld.id);

                    if (_children.Count > 0)
                        AddChildFolderNodes(me, _children, database,enabletooltips);
                }

            });
        }

		//---------------------------------------------------------------------------------
		private void CollectSelectedNodes(TreeNode src, List<st_SelectedTxt> Old_SelectedNodes, List<TreeNode> SelectedNodes)
		{
			logger.Trace("Begin");
			// If it has children, call this function recursively
			if (0 < src.Nodes.Count)
			{
				foreach (TreeNode wrk in src.Nodes)
					CollectSelectedNodes(wrk, Old_SelectedNodes, SelectedNodes);
			}

			// Get current node's texts according to its level
			st_SelectedTxt NodeInfo = GetSelectedItemInfo(src);

			// Check if the node was selected before by comparing node texts
			bool selected = Old_SelectedNodes.Exists((item) =>
			{
				bool ans = false;

				if
				(
					(NodeInfo.Level == item.Level) &&
					(NodeInfo.text_L0 == item.text_L0) &&
					(NodeInfo.text_L1 == item.text_L1) &&
					(NodeInfo.text_L2 == item.text_L2)
				)
					ans = true;

				return ans;
			});

			if (selected)
				SelectedNodes.Add(src);
		}

		//---------------------------------------------------------------------------------
		// Just getting texts along with its parents
		private st_SelectedTxt GetSelectedItemInfo(TreeNode item)
		{
			logger.Trace("Begin");
			st_SelectedTxt text = new st_SelectedTxt();

			text.Level = item.Level;

			switch (text.Level)
			{
				case 2:
					text.text_L0 = item.Parent.Parent.Text;
					text.text_L1 = item.Parent.Text;
					text.text_L2 = item.Text;
					break;

				case 1:
					text.text_L0 = item.Parent.Text;
					text.text_L1 = item.Text;
					text.text_L2 = "";
					break;

				case 0:
					text.text_L0 = item.Text;
					text.text_L1 = "";
					text.text_L2 = "";
					break;
			}

			return text;
		}

		//---------------------------------------------------------------------------------
		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			treeViewMSExplorer.CollapseAll();
			//treeViewFolderExplore.CollapseAll();
		}

		//---------------------------------------------------------------------------------
		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			treeViewMSExplorer.ExpandAll();
			//treeViewFolderExplore.ExpandAll();
		}

		private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
		{
			logger.Trace("Begin");

		}


		private void treeViewFolderExplore_AfterSelect(object sender, TreeViewEventArgs e)
		{
			logger.Trace("Begin");


            // To stay first selected place during the drag&drop
            // store you selected  for next
            logger.Debug("Set selected folder: {0}", treeViewFolderExplore.SelectedNode.Text);
            Exp2LastSelected.Clear();
            treeViewFolderExplore.SelectedNodes.ForEach((item) =>
            {
                Exp2LastSelected.Add(item);
            });

            //var selected = dtgBdFiles.GetSelectedDocument();
            //RefreshByFolderExplorer(treeViewFolderExplore.SelectedNode,false, selected?.id ?? 0);
		}

		//---------------------------------------------------------------------------------
		/// <summary>
		/// Refresh list of Displayed  document by selected folder
		/// </summary>
		void RefreshByFolderExplorer(TreeNode node, bool archived = false, int selectDocID = 0)
		{
			logger.Trace("Begin");

			///*
			// Do not retrieve document list if you are already on that folder.
			// */
			//var selectedDocument = dtgBdFiles.GetSelectedDocument();
			//if( selectedDocument?.id_folder.ToString() == node.Name )
			//{
			//    return;
			//}
			
			// Selected on blank
			if (node == null)
				return;

            /*
             * Retrive documents
             */
            try
            {
                List<SearchCriteria> searchCriteriaArray = new List<SearchCriteria>();

			    SearchCriteria criteria = new SearchCriteria();

			    criteria.FolderIds.Add(((Folder)node.Tag).id);
                criteria.Archived = archived;

			    searchCriteriaArray.Add(criteria);

			    table.Criteria = searchCriteriaArray;

			    search(dtgBd_SearchMode.Normal, selectDocID);
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }

        }




		/// <summary>
		/// Begining of Drag Folder
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeViewFolderExplore_ItemDrag(object sender, ItemDragEventArgs e)
		{
			logger.Trace("Begin");
			// Move the dragged node when the left mouse button is used.
			if (e.Button == MouseButtons.Left)
			{
				DoDragDrop(e.Item, DragDropEffects.Move);
			}
		}

        /// <summary>
        /// Begining of Drag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewFolderExplore_DragEnter(object sender, DragEventArgs e)
        {
            logger.Trace("Begin");

            DragBy by = DetermineDragBy(e);

            // Nothing to do if moving folder within folder exploere.
            switch (by)
            {
                case DragBy.TreeView:
                    break;

                case DragBy.GridView:
                case DragBy.Explore:
                    e.Effect = e.AllowedEffect;
                    break;
            }
        }

        /// <summary>
        /// When Drag Over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewFolderExplore_DragOver(object sender, DragEventArgs e)
		{
			logger.Trace("Begin");


            DragBy by = DetermineDragBy(e);
            // Nothing to do if moving folder within folder exploere.
            switch (by)
            {
                case DragBy.TreeView:
                    return;
            }

            // Retrieve the client coordinates of the mouse position.
            Point targetPoint = treeViewFolderExplore.PointToClient(new Point(e.X, e.Y));

			// Select the node at the mouse position.
			TreeNode  destinationNode = treeViewFolderExplore.GetNodeAt(targetPoint);

			if( destinationNode == null) return;

			//if we are on a new object, reset our timer
			//otherwise check to see if enough time has passed and expand the destination node
			if (destinationNode != lastDragDestination)
			{
                if( null != lastDragDestination ) lastDragDestination.ForeColor = treeViewFolderExplore.ForeColor;    // restore gray out to normal

                lastDragDestination = destinationNode;
				lastDragDestinationTime = DateTime.Now;
			}
			else
			{
				TimeSpan hoverTime = DateTime.Now.Subtract(lastDragDestinationTime);
				if (hoverTime.TotalMilliseconds > 500)
				{
                    //treeViewFolderExplore.SelectedNode = destinationNode;
                    destinationNode.ForeColor = Color.Gray; // gray out on draging over node

                    destinationNode.Expand();
				}
			}
		}
        /// <summary>
        /// When Drag is end
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewFolderExplore_DragLeave(object sender, EventArgs e)
        {
            if (null != lastDragDestination) lastDragDestination.ForeColor = treeViewFolderExplore.ForeColor;    // restore gray out to normal
            //Exp2LastSelected.Clear();
        }

        DragBy DetermineDragBy(DragEventArgs e)
		{
			logger.Trace("Begin");
			try{
				// GridView when dragged is Document Object
				if( e.Data.GetType() == typeof(DocumentDataObject)) return DragBy.GridView;

				// Explore when dragged has at the least one system file.
				try
				{
					string[] dragged = (string[])(e.Data.GetData(DataFormats.FileDrop)); // dragged files
					if (FileFolder.GetFilesByPath(dragged).Count > 0) return DragBy.Explore;
				}
				catch { }

				// TreeView when dragged is TreeNode
				try
				{
					if(((TreeNode)e.Data.GetData(typeof(TreeNode))).GetType() == typeof(TreeNode)) return DragBy.TreeView;
				}
				catch {  }

			}catch{
				return DragBy.Unknown;
			}
			return DragBy.Unknown;
		}

		void Drop4TreeView(TreeNode targetNode,DragEventArgs e)
		{
            return;

			logger.Trace("Begin");

			// Retrieve the node that was dragged.
			TreeNode draggedNode;
			try
			{
				draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
			}
			catch { return; }

			// set 0 if null
			if (targetNode == null)
			{
                // Here is update Folder ID
                Folder fld = (draggedNode.Tag as Folder);

                if (!FolderController.IsUniqueFolderName(fld.document_folder, 0))
                {
                    MessageBox.Show(lib.msg_existing_folder, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                draggedNode.Remove();
				treeViewFolderExplore.Nodes.Insert(0, draggedNode);

				FolderController.UpdateParent(fld.id, 0);
				fld.id_parent = 0;

				return;
			}
			// Confirm that the node at the drop location is not
			// the dragged node or a descendant of the dragged node.
			if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
			{
				// If it is a move operation, remove the node from its current
				// location and add it to the node at the drop location.
				if (e.Effect == DragDropEffects.Move)
				{
                    // Here is update Folder ID
                    Folder chld = (draggedNode.Tag as Folder)
                        , prnt = (targetNode.Tag as Folder);

                    if (!FolderController.IsUniqueFolderName(chld.document_folder, prnt.id))
                    {
                        MessageBox.Show(lib.msg_existing_folder, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    draggedNode.Remove();
					targetNode.Nodes.Add(draggedNode);


					FolderController.UpdateParent(chld.id, prnt.id);
					chld.id_parent = prnt.id;

					//frmWorkSpace_OnFolderChanged(this, new FolderEventArgs(FolderEventArgs.Proc.WITHOU_FOLDER));
				}

				// Expand the node at the location
				// to show the dropped node.
				//targetNode.Expand();
			}
		}

		void Drop4GridView(TreeNode targetNode, DragEventArgs e)
		{
			logger.Trace("Begin");
			// Do nothing if folder isn't selected
			if (targetNode == null) return;

			Folder prnt = (targetNode.Tag as Folder);

            if (false == PermissionController.CheckPermission(prnt.id, en_Actions.CheckIn_Out, SpiderDocsApplication.CurrentUserId))
            {
                MessageBox.Show(lib.msg_permissio_denied, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<Document> grid_docs = ((DocumentDataObject)e.Data).GetOrigin();

			// Stop working if one of docs's status are checked out by other user
			if( grid_docs.Where(x =>

					// checkin
					x.id_status == en_file_Status.checked_in

					// or checked out by me
					|| (x.id_status == en_file_Status.checked_out && x.id_checkout_user == SpiderDocsApplication.CurrentUserId)).Count()

				    // not equal all selected documents
				    != grid_docs.Count()
			)
			{
				MessageBox.Show(lib.msg_invalid_drop_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			List<Document> docs = DocumentController<Document>.GetDocument(grid_docs.Select(x => x.id).ToArray());

            List<Document> docs2 = new List<Document>();

            //check first
            for (int i = 0; i < docs.Count; i++)
            {
                Document doc = docs[i];

                doc.id_folder = prnt.id;

                FillDragDropProperty(prnt.id, ref doc);


                if (!doc.isNotDuplicated())
                {
                    //MessageBox.Show(lib.msg_found_duplicate_docs, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                docs2.Add(doc);
            }

			foreach(var doc in docs2)
			{
				if (!doc.__WarnForDuplicate())
				{
                    //if (MessageBox.Show(lib.msg_warn_existing_file, lib.msg_messabox_title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
                    //	return;
                    //else
                    //{
                    //	doc.hasAccepted = true;
                    //}
                    return;
				}
			}

			//if (docs2.Exists(validate => validate.WarnForDupliate()))
			//{
			//	if (MessageBox.Show(lib.msg_warn_existing_file, lib.msg_messabox_title, MessageBoxButtons.YesNo, MessageBoxIcon.Error) != System.Windows.Forms.DialogResult.Yes)
			//		return;
			//	else
			//	{
			//		docs2.hasAccepted = true;
			//	}
			//}

            // update
            docs2.ForEach( doc => {

				try
				{
                    doc.UpdateProperty();

                }
				catch(Exception ex){ logger.Error(ex); }
			});

			//treeViewFolderExplore.SelectedNode = targetNode;
			//targetNode.Expand();
   //         //RefreshByExplorer();

            if( Exp2LastSelected.Count > 0)
            {

                /*
                 * The document's folder has been moved.
                 * Then selectedNode's folder == dtgBdFiles.GetSelectedDocument() 's folder.
                 * To show the moved folder's document, should call clear selection
                 */
                dtgBdFiles.ClearSelection();


                logger.Debug("Restore selection to {0}", Exp2LastSelected.LastOrDefault()?.Text);
                treeViewFolderExplore.SelectedNode = Exp2LastSelected.LastOrDefault();

                //targetNode.Expand();
                RefreshByFolderExplorer(treeViewFolderExplore.SelectedNode);
                targetNode.ForeColor = treeViewFolderExplore.ForeColor;
                //Exp2LastSelected.Clear();
            }
        }

		/// <summary>
		/// Save document to underneath a folder you selected.
		/// </summary>
		/// <param name="targetNode"></param>
		/// <param name="e"></param>
		void Drop4Explore(TreeNode targetNode, DragEventArgs e)
		{
			logger.Trace("Begin");

			try
			{
				// Foleder ID that dragged
				int folder_id = (targetNode == null) ? 0 : (targetNode.Tag as Folder).id;
                Folder selected = (targetNode == null) ? new Folder() : targetNode.Tag as Folder;

                if (false == PermissionController.CheckPermission(selected.id, en_Actions.CheckIn_Out, SpiderDocsApplication.CurrentUserId))
                {
                    MessageBox.Show(lib.msg_permissio_denied, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get system file paths by dragged
                string[] dragged = (string[])(e.Data.GetData(DataFormats.FileDrop)); // dragged files
				List<string> filePaths = FileFolder.GetFilesByPath(dragged); // Get files from passed paths

                List<Document> docs = new List<Document>();

                // check first
                foreach (string path in filePaths)
                {

                    Document doc = new Document();
                    doc.path = ConvToSearchableFileIfPossible(path);  //convert a file to searchable file.
                    doc.id_event = EventIdController.GetEventId(en_Events.Import);
                    doc.id_folder = selected.id;

                    FillDragDropProperty(selected.id, ref doc);

                    if (!doc.isNotDuplicated(true))
                    {
                        //MessageBox.Show(lib.msg_found_duplicate_docs, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

					if (!doc.__WarnForDuplicate(true))
					{
                        //if (MessageBox.Show(lib.msg_warn_existing_file, lib.msg_messabox_title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
                        //	return;
                        //else
                        //	doc.hasAccepted = true;
                        return;
					}

                    docs.Add(doc);
                }

                foreach (Document doc in docs)
				{
					try
					{
                        doc.AddDocument();
					}
					catch(Exception ex)
					{
						logger.Error(ex);
					}
				}

				treeViewFolderExplore.SelectedNode = targetNode;
				//targetNode.Expand();

			}
			catch(Exception ex) { logger.Error(ex); }
		}

        string ConvToSearchableFileIfPossible(string path)
        {
			if (!SpiderDocsApplication.CurrentUserGlobalSettings.ocr) return path;
			if (!SpiderDocsApplication.CurrentUserGlobalSettings.default_ocr_import) return path;

			string dest = FileFolder.GetAvailableFileName(FileFolder.GetTempFolder() + Path.GetFileName(path));
			var ext = dest.Substring(dest.LastIndexOf("."));
			dest = dest.Replace(ext, ".pdf");

			// Scan check box is enable if there is least one image file
			if (!FileFolder.extensionsForScan.Contains(Path.GetExtension(path).ToLower()))
				return path;

			Thread thread = null;
            try
            {

                thread = new Thread(() => { try { new frmBusy().ShowDialog(); } catch { } });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

                OCRManager ocrManager = new OCRManager(path);
                ocrManager.GetPDF(dest);

                thread.Abort();
            }
            catch (ThreadAbortException) { }
			catch (Exception ex)
			{
				logger.Error(ex);
				thread?.Abort();
            }

            return dest;
        }

        void FillDragDropProperty(int id_folder,ref Document doc)
        {
			var hasDef = DragDropTypeController.GetBy(id_folder);

			if (hasDef != null)
				doc.id_docType = hasDef.id_type;

            List<DocumentAttribute> _attrs = DragDropAttributeController.GetDragDropAttribute(id_folder);

            doc.Attrs = DocumentAttributeController.Merge(doc.Attrs, _attrs);
        }

		/// <summary>
		/// When Dragged Node is droped
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeViewFolderExplore_DragDrop(object sender, DragEventArgs e)
		{
			logger.Trace("Begin");

            // Retrieve the client coordinates of the drop location.
            Point targetPoint = treeViewFolderExplore.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = treeViewFolderExplore.GetNodeAt(targetPoint);             // Retrieve the node at the drop location.

            //TreeNode targetNode = hitTest.Node;
            if ( !IsClickOnText(treeViewFolderExplore, targetNode, targetPoint))
            {
                targetNode = null;
            }

            DragBy by = DetermineDragBy(e);

			switch(by){
				case DragBy.TreeView:
					Drop4TreeView(targetNode, e);   //move folder
				break;

				case DragBy.GridView:
					Drop4GridView(targetNode, e);   //move document
				break;

				case DragBy.Explore:
					Drop4Explore(targetNode, e);    //move document from windows explore
				break;
			}
		}


		/// <summary>
		/// Determine whether one node is a parent
		/// or ancestor of a second node.
		/// </summary>
		/// <param name="node1"></param>
		/// <param name="node2"></param>
		/// <returns></returns>
		private bool ContainsNode(TreeNode node1, TreeNode node2)
		{
			logger.Trace("Begin");
			// Check the parent node of the second node.
			if (node2.Parent == null) return false;
			if (node2.Parent.Equals(node1)) return true;

			// If the parent node is not null or equal to the first node,
			// call the ContainsNode method recursively using the parent of
			// the second node.
			return ContainsNode(node1, node2.Parent);
		}

		/// <summary>
		/// When Create at root
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			CreateFolderAtTreeView();
		}

		/// <summary>
		/// Create Folder.
		/// </summary>
		/// <param name="slctNode">it will be parent</param>
		void CreateFolderAtTreeView(TreeNode slctNode = null,string name = "")
		{
			logger.Trace("Begin");
			//frmEnterText frm = new frmEnterText();
			//frm.StartPosition = FormStartPosition.CenterParent;

			//frm.ShowDialog(this);

			//string name = frm.txtText.Text;

			if (string.IsNullOrWhiteSpace(name)) return;

			//frm.Dispose();

			// Folder trying to be created. id_parent will be set if stctNode argument is passed.
			Folder fld = new Folder(0, name, slctNode == null ? 0 : (slctNode.Tag as Folder).id );

			if (!FolderController.IsUniqueFolderName(name, fld.id_parent))
			{
				MessageBox.Show(lib.msg_existing_folder, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Add Folder
			int fld_id = FolderController.Save(fld);

			TreeNode node = new TreeNode(fld.document_folder);
			node.Tag = fld;
            //node.ContextMenuStrip = contextMenuStripFolderViewOption2;

            // Add Permission
            // AddPermission(fld_id);
            if (fld.id_parent == 0)
                PermissionController.GrantFullPermission(fld_id);

            // Add Node
            if ( slctNode != null)
				slctNode.Nodes.Add(node);
			else
				treeViewFolderExplore.Nodes.Add(node);

			if(node.Parent != null) OpenTreeViewAt(node.Parent);

			frmWorkSpace_OnFolderChanged(this, new FolderEventArgs(FolderEventArgs.Proc.NONE));
		}

		///// <summary>
		///// Grant Full Permission to folder
		///// </summary>
		///// <param name="folder_id"></param>
		//void AddPermission(int folder_id)
		//{
		//	logger.Trace("Begin");
		//	//PermissionController.AssignFolder(en_FolderPermissionMode.Group, folder_id, Group.ALL);

		//	en_FolderPermissionMode mode = en_FolderPermissionMode.Group;

		//	Dictionary<en_Actions, en_FolderPermission> permissions = new Dictionary<en_Actions, en_FolderPermission>();

		//	foreach (en_Actions actn in Enum.GetValues(typeof(en_Actions)))
		//	{
		//		permissions.Add((en_Actions)actn, en_FolderPermission.Allow);
		//	}

		//	PermissionController.AddPermission(folder_id, Group.ALL, mode, permissions);

  //      }

		// private void treeViewFolderExplore_MouseClick(object sender, MouseEventArgs e)
		// {
		// 	logger.Trace("Begin");

		// 	TreeNode node = treeViewFolderExplore.GetNodeAt(treeViewFolderExplore.PointToClient(Cursor.Position));
		// 	if( node == null )
		// 	{
		// 		treeViewFolderExplore.SelectedNode = null;
		// 	}

		// }

		/// <summary>
		/// When remove is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripMenuItem6_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");

			DeleteFolderAtTreeView(treeViewFolderExplore.SelectedNode);
		}

		void DeleteFolderAtTreeView(TreeNode slctNode)
		{
            if (slctNode == null) return;

			Folder crntFolder = (slctNode.Tag as Folder);

			if( crntFolder.id == 0) return;

			// Has Delete Permission ?
			if( !PermissionController.CheckPermission(crntFolder.id,en_Actions.Delete))
			{
				MessageBox.Show(lib.msg_permissio_denied,lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Question);
				return;
			}

			// All folders can be removed ?
			List<Folder> full = FolderController.DrillDownFoldersBy(crntFolder.id);
			Folder NotPermit = full.Where( f =>  !PermissionController.CheckPermission(f.id,en_Actions.Delete)).FirstOrDefault() ?? new Folder();
			if( NotPermit.id > 0 )
			{
				logger.Warn("{0} doesn't have permission for {1} on {2},{3}",SpiderDocsApplication.CurrentUserId,en_Actions.Delete,string.Join(",",full.Select(x => x.id).ToArray()),crntFolder.id);
				MessageBox.Show(lib.msg_permission_denied,lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Question);
				return;
			}

			DialogResult result = (MessageBox.Show(lib.msg_ask_delete_folder, lib.msg_messabox_title, MessageBoxButtons.YesNo, MessageBoxIcon.Question));
			if (result == DialogResult.Yes)
			{
				// Remove the folder
				List<TreeNode> nodes = GetNodesBy(slctNode);
				nodes.Add(slctNode);
				nodes.Reverse();
                slctNode.Remove();
                treeViewFolderExplore.SelectedNode = null; // Unselect removed folder.

                BackgroundWorker thRmDir = new BackgroundWorker();
                thRmDir.DoWork += (object sender, DoWorkEventArgs e) => RemoveFolder(nodes);
                thRmDir.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
				{
                    search(dtgBd_SearchMode.RecentDocuments);

                    frmWorkSpace_OnFolderChanged(this, new FolderEventArgs(FolderEventArgs.Proc.WITHOU_FOLDER));

                    MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);

                };

                thRmDir.RunWorkerAsync();
            }
		}

        /// <summary>
        /// Get all child nodes
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        List<TreeNode> GetNodesBy(TreeNode nodes)
		{
			List<TreeNode> children = new List<TreeNode>();
            List<TreeNode> current = new List<TreeNode>();
            foreach ( TreeNode node in nodes.Nodes)
			{
				//children.AddRange(GetNodesBy(node));
                if (node.Nodes.Count > 0) children.AddRange(GetNodesBy(node));

                current.Add(node);
			}

            children.AddRange(current);

            return children;
		}
        void RemoveFolder(List<TreeNode> nodes)
        {
            foreach (TreeNode node in nodes)
            {
                Folder fld = node.Tag as Folder;
                if (RemoveFolder(fld.id)) { /*node.Remove(); */
            }
        }

        }

        bool RemoveFolder(int id_folder)
		{
			logger.Trace("Begin");

			bool removed = true;
			// Do nothing if you don't have permission to execute
			bool hasPermission = PermissionController.CheckPermission(id_folder,en_Actions.Delete);
			if ( !hasPermission ) return false;


			// Delete Files under the folder
			List<Document> docs = DocumentController.GetBy(id_folder:id_folder);
			for(int i = docs.Count -1; i >= 0 ; i--)
			{
				Document doc = docs[i];

				bool ok = doc.cancelCheckOut();

				if( ok )
				{
					logger.Info("Document is removed: {0}",doc.id);
					if(logger.IsDebugEnabled) logger.Debug("{0}", Newtonsoft.Json.JsonConvert.SerializeObject(doc));

                    doc.Remove("Deleted by Folder Exploere",SpiderDocsApplication.CurrentUserId);
				}
				else
				{
					removed = false;
				}
			}

			// Remove the folder
			FolderController.Delete(id_folder);

			return removed;
		}

		///// <summary>
		///// When open context menu in Folder Exploere
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//void contextMenuStripFolderViewOption2_Opening(object sender, CancelEventArgs e)
		//{
		//	logger.Trace("Begin");

		//	// Retrieve the client coordinates of the drop location.
		//	Point targetPoint = treeViewFolderExplore.PointToClient(Cursor.Position);

		//	// Retrieve the node at the drop location.
		//	TreeNode targetNode = treeViewFolderExplore.GetNodeAt(targetPoint);

		//	if (targetNode == null) return;

		//	TreeNode slctNode = ( treeViewFolderExplore.SelectedNode = targetNode ) ;

		//	Folder crntFolder = (slctNode.Tag as Folder);
		//	bool hasCld = false;
		//	// Dont allow to remove if node you selected has child
		//	foreach (Folder fld in FolderController.GetFolders())
		//	{
		//		if (fld.id_parent == crntFolder.id) {
		//			hasCld = true;
		//			break;
		//		}
		//	}

		//	contextMenuStripFolderViewOption2.Items[2].Visible = !hasCld;
		//}

		/// <summary>
		/// When Folder rename is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripMenuItem4_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");

			RenameFolderAtTreeView(treeViewFolderExplore.SelectedNode);
		}

		void RenameFolderAtTreeView(TreeNode slctNode)
		{
            if (slctNode == null) return;

			Folder crntFolder = slctNode.Tag as Folder;

			frmEnterText frm = new frmEnterText();
			frm.StartPosition = FormStartPosition.CenterParent;
			frm.txtText.Text = crntFolder.document_folder;
			frm.ShowDialog(this);

			string name = frm.txtText.Text;

			frm.Dispose();

			// No change
			if (crntFolder.document_folder == name) return;


			//  Error if name is empty
			if (string.IsNullOrWhiteSpace(name))
			{
				// TODO: Error Message
				MessageBox.Show(lib.msg_required_folder_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

				return;
			};

			// Update
			Folder fld = slctNode.Tag as Folder;

			// If change name ( case insensitive )
			// And that name is assigned another folder already
			if (crntFolder.document_folder.ToLower() != name.ToLower()
					&& !FolderController.IsUniqueFolderName(name, fld.id_parent))
			{
				MessageBox.Show(lib.msg_existing_folder, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Finally, update new name
			fld.document_folder = slctNode.Text = name;

			// Add Folder
			FolderController.Save(fld);

			frmWorkSpace_OnFolderChanged(this, new FolderEventArgs(FolderEventArgs.Proc.WITHOU_FOLDER));

		}

		/// <summary>
		/// When Add clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripMenuItem5_Click(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			CreateFolderAtTreeView(treeViewFolderExplore.SelectedNode);
			treeViewFolderExplore.SelectedNode?.Expand();
		}


		private System.Threading.Thread Thread_TreeViewFolder { get; set; }

		private void frmWorkSpace_Activated(object sender, EventArgs e)
		{
			logger.Trace("Begin");
			StartFolderExplr();
            cboFolder.UseDataSource4AssignedMe(0, en_Actions.OpenRead);
        }

		void StartFolderExplr()
		{
			logger.Trace("Begin");

            Thread_TreeViewFolder?.Abort();

            treeViewFolderExplore.SelectedNode = null;

            Thread_TreeViewFolder = new System.Threading.Thread(new ParameterizedThreadStart(this.loadFolderTreeView));
            Thread_TreeViewFolder.Priority = ThreadPriority.Lowest;
            Thread_TreeViewFolder.Start(new object[] { this.treeViewFolderExplore, 0 });

            Thread_TreeViewFolder = new System.Threading.Thread(new ParameterizedThreadStart(this.loadArchiveFolderTreeView));
            Thread_TreeViewFolder.Priority = ThreadPriority.Lowest;
            Thread_TreeViewFolder.Start(new object[] { this.treeViewArchivedFolder, 0 });
		}

		void frmWorkSpace_OnFolderChanged(object sender, FolderEventArgs e)
		{
			logger.Trace("Begin");
			UpdateFolderComboBox();

			switch ( e.Instruction )
			{
				case FolderEventArgs.Proc.ALL:
					StartFolderExplr();

					//StartRegacyExplr();
					break;

				case FolderEventArgs.Proc.WITHOU_FOLDER:
					//StartRegacyExplr();
					break;
				case FolderEventArgs.Proc.FOLDER:
					StartFolderExplr();
					break;

				case FolderEventArgs.Proc.NONE:

					break;
			}


			// Close Opened Form due to keep latest structure
			List<Form> frms = new List<Form>();
			foreach (Form OpenForm in Application.OpenForms)
			{
				switch (OpenForm.GetType().Name)
				{
					case "frmFolderPermissions":
                    // case "frmFolder":
                    case "frmScan":
                        frms.Add(OpenForm);
						break;
                }
			}

			frms.ForEach(x => x.Close());
		}

		//System.Threading.Thread Thread_CboFolder {get;set;}
		/// <summary>
		/// Update Folder ComboBox
		/// </summary>
		void UpdateFolderComboBox()
		{
            logger.Trace("Begin");
			new Cache(SpiderDocsApplication.CurrentUserId).Remove(Cache.en_UKeys.DB_GetAssignedFolderToUser);
			cboFolder.UseDataSource4AssignedMe(0, en_Actions.OpenRead);

			// return ;
			// Thread_CboFolder?.Abort();

			// Thread_CboFolder = new System.Threading.Thread(()=> { cboFolder.UseDataSource4AssignedMe();  });
			// Thread_CboFolder.Priority = ThreadPriority.Lowest;
			// Thread_CboFolder.Start();

            // //Task.Run ( ()=> cboFolder.UseDataSource4AssignedMe() );
        }

		/// <summary>
		/// When FolderCreate is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void btnFolderCreate_Click(object sender, EventArgs e)
        {
			logger.Trace("Begin");

            try
            {
                List<Folder> folders = new List<Folder>();
                string entered = string.Empty;

                //Shows the dialog to search existing folders.
                frmEnterText frm = new frmEnterText() { AutoCloseByEnter = false,ShowInTaskbar = false, StartPosition = FormStartPosition.Manual };

                frm.Location = new Point(
                    (this.Location.X + this.Width / 2) - (frm.Width / 2),
                    (this.Location.Y + this.Height / 2) - 200
                );

                frmSimpleList suggested = new frmSimpleList() { Visible = false, StartPosition = FormStartPosition.Manual };

                ///
                /// Entering the folder name
                ///
                frm.Entering += (object o, frmEnterText.ThreeWordsArgs a) => {

                    // Select suggestion item.
                    if (a.Args.KeyCode == Keys.Down || a.Args.KeyCode == Keys.Tab || a.Args.KeyCode == Keys.PageDown)
                    {
                        suggested.Focus();
                        suggested.Next(); return;
                    }
                    else if (a.Args.KeyCode == Keys.Up || a.Args.KeyCode == Keys.PageUp)
                    {
                        suggested.Focus();
                        suggested.Prev(); return;
                    }
                    else if (a.Args.KeyCode == Keys.Left || a.Args.KeyCode == Keys.Right || a.Args.KeyCode == Keys.Home || a.Args.KeyCode == Keys.End)
                    {
                        return;
                    }

                    // Update suggestion list
                    folders = FolderController.SearchBy(a.Text, a.Text.Length < 3 ? 3 : 0);

                    if ( folders.Count ==0)
                    {
                        suggested.Visible = false;
                        return;
                    }

                    suggested.Update(folders.Select(x => new frmSimpleList.Item(0,x.document_folder)).Distinct());

                    //suggested.StartPosition = FormStartPosition.Manual;

                    var loc = this.PointToScreen(frm.Location);
                    loc.Y = frm.Bottom - 7; loc.X += 40;
                    suggested.Location = loc;
                    suggested.BringToFront();
                    suggested.Visible = true;

                    frm.Focus();
                };

                ///
                /// When double click on an item on the suggestion
                ///
                suggested.ExposeDblClickEvent += (object s, EventArgs e2) =>
                {

                    string selected = ((frmSimpleList.Item)((ListBox)s).SelectedItem).Text.ToString();

                    frm.SetText(selected);
                    suggested.Visible = false;
                    frm.Focus();
                };

				///
				/// Getting typed folder name.
				///
                frm.Commit += (object s, KeyPressEventArgs e2) =>
                {
                    string text = ((TextBox)s).Text;
                    var item = suggested.GetSelected();

                    if (item != null)
                        text = item.Text.ToString();

                    if (suggested.HasItem(text))
                    {
                        entered = text;

                        suggested.Close();

                        frm.Close();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(lib.msg_match_folder_name, lib.msg_messabox_title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                };

                suggested.Show(this);
                suggested.Hide();

                frm.ShowDialog(this);

                string name = entered;// frm.txtText.Text;

                suggested.Dispose();
                frm.Dispose();

                if (string.IsNullOrWhiteSpace(name)) return;

                CreateFolderAtTreeView(treeViewFolderExplore.SelectedNode, name);

                treeViewFolderExplore.SelectedNode?.Expand();
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }
        }

        /// <summary>
        /// When FolderDelete is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFolderDelete_Click(object sender, EventArgs e)
        {
			logger.Trace("Begin");
			DeleteFolderAtTreeView(treeViewFolderExplore.SelectedNode);
        }

		/// <summary>
		/// When FolderRename is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void btnFolderRename_Click(object sender, EventArgs e)
        {
			logger.Trace("Begin");
			RenameFolderAtTreeView(treeViewFolderExplore.SelectedNode);
        }

        DateTime lastDblClickDate;
        TreeNode lastDblClickDateNode;
        private void treeViewFolderExplore_MouseDown(object sender, MouseEventArgs e)
        {
            logger.Trace("Begin");

			btnFolderCreate.Enabled = true;
			btnFolderDelete.Enabled = true;
			btnFolderRename.Enabled = true;

			//
			// Apply double click behaviour if it has defined.
            TreeNode targetNode = treeViewFolderExplore.GetNodeAt(new Point(e.X, e.Y));

			if (targetNode == null)
				return;

            if (
				// Double click time is user preference. (default:500ms)
				(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmssfff")) - long.Parse(lastDblClickDate.ToString("yyyyMMddHHmmssfff"))) <= SystemInformation.DoubleClickTime

                &&

				// Tread double click if clicked on same folder name.
                targetNode.Text == lastDblClickDateNode?.Text
                )
            {
                ApplyDblClickBehaviour(e);
            }
            else
            {
                // normal search
                var selected = dtgBdFiles.GetSelectedDocument();
                //RefreshByFolderExplorer(treeViewFolderExplore.SelectedNode, false, selected?.id ?? 0);
                RefreshByFolderExplorer(targetNode, false, selected?.id ?? 0);

            }

            lastDblClickDate = DateTime.Now;
            lastDblClickDateNode = targetNode;


            // If you click on the root layer then truetreeViewFolderExplore.PointToClient
            TreeNode node = treeViewFolderExplore.GetNodeAt(treeViewFolderExplore.PointToClient(Cursor.Position));
            if (node == null)
			{
				// You can only create a folder
				btnFolderDelete.Enabled = false;
				btnFolderRename.Enabled = false;

				// Deselect the node
                if( treeViewFolderExplore.SelectedNode == null )  return;

				treeViewFolderExplore.SelectedNode.ForeColor = treeViewFolderExplore.ForeColor;
				treeViewFolderExplore.SelectedNode.BackColor = treeViewFolderExplore.BackColor;
				treeViewFolderExplore.SelectedNode = null;
            }
        }

        private string searchterm = "";             // what to search for
        private DateTime LastSearch = DateTime.Now; // resets searchterm if last input is older than 1 second.

        /// <summary>
        /// Auto Select nodes in folder explore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void treeView_KeyPress(object sender, KeyPressEventArgs e)
		{
			var treeview = (tv.MultiSelectTreeview)sender;

            if (string.IsNullOrEmpty(e.KeyChar.ToString())) return;

            var keyCode = e.KeyChar;
            // reset searchterm if any "special" key is pressed
            if ( false == char.IsNumber( keyCode ) &&  false == ((keyCode >= 'a' && keyCode <= 'z') || (keyCode >= 'A' && keyCode <= 'Z')))
			{
				searchterm = "";

				return;
			}

            // Rest to start.
            // reset searcj chars if you blanked more than system double click time.
            if ( ( long.Parse(DateTime.Now.ToString("yyyyMMddHHmmssfff")) - long.Parse(LastSearch.ToString("yyyyMMddHHmmssfff"))) > 1000)
			{
                searchterm = "";
			}

            LastSearch = DateTime.Now;
            searchterm += (char)e.KeyChar;

            // Find Last select node.
            var selected = Exp2LastSelected.FirstOrDefault(); // Exp2LastSelected.FirstOrDefault() ?? treeview.SelectedNode;

			// Get nodes that same layer from treeview.Nodes.
			var sameLayerNodes = selected.Parent == null ? treeview.Nodes : selected.Parent.Nodes;

			// Search nodes by input characters from above
			var found = SearchTreeView( sameLayerNodes, searchterm.ToLower(), selected );

            if (found == null)
            {
                treeview.SelectedNode = selected;
                return;
            }

			// Set the selectedNodes as it found.
			treeview.SelectedNode = found ?? selected;
            treeview.SelectedNode.EnsureVisible();

        }


        /// <summary>
        /// Find nodes by text
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="searchterm"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        private TreeNode SearchTreeView(TreeNodeCollection nodes, string searchterm, TreeNode selected)
        {
            //bool start = false;
            foreach (TreeNode node in nodes)
			{

				//if ( node == selected)
				//{
				//	start = true;
				//}

				//if ( start == false )
				//{
				//	continue;
				//}

				if( false == node.Text.ToLower().StartsWith(searchterm) )
				{
					continue;
				}

				return node;

			}

			return null;
        }

        private void treeViewArchivedFolder_AfterSelect(object sender, TreeViewEventArgs e)
        {
            logger.Trace("Begin");


            // To stay first selected place during the drag&drop
            // store you selected  for next
            logger.Debug("Set selected folder: {0}", this.treeViewArchivedFolder.SelectedNode.Text);
            Exp2LastSelected.Clear();
            treeViewArchivedFolder.SelectedNodes.ForEach((item) =>
            {
                Exp2LastSelected.Add(item);
            });

            RefreshByFolderExplorer(treeViewArchivedFolder.SelectedNode,true);
        }
        #endregion

        private void menuDbRename_Click(object sender, EventArgs e)
        {
			Document doc = dtgBdFiles.GetSelectedDocument();

            doc.Attrs = DocumentAttributeController.SetToDocument(doc).Attrs;

            frmEnterText frm = new frmEnterText(doc.title_without_ext,doc.extension) { LabelText = "New File Name" };
            frm.StartPosition = FormStartPosition.CenterParent;

            frm.ShowDialog(this);

            string name = frm.txtText.Text;

            if (string.IsNullOrWhiteSpace(name) || name.Trim() == doc.title_without_ext.Trim()) return;

            frm.Dispose();

			doc.title = name + doc.extension;

            // Check if no duplication
            if (!doc.isNotDuplicated())
            {
                //System.Windows.Forms.MessageBox.Show(lib.msg_found_duplicate_docs, lib.msg_messabox_title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            if (!doc.__WarnForDuplicate(true))
            {
                return;
                //if (MessageBox.Show(lib.msg_warn_existing_file, lib.msg_messabox_title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
                //{
                //    return;
                //}
                //else
                //    doc.hasAccepted = true;
            }

            string ans = doc.UpdateProperty();

            if (!String.IsNullOrEmpty(ans))
            {
                //Utilities.regLog(ans);
                logger.Error("System Error {0}", ans);
                MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            search(dtgBd_SearchMode.Normal);

        }

        private void dtgBdFiles_MouseClick(object sender, MouseEventArgs e)
        {
            var ht = dtgBdFiles.HitTest(e.X, e.Y);

            if (ht.Type == DataGridViewHitTestType.None)
            {
                this.dtgBdFiles.ClearSelection();
            }
        }

        private void splitContainer2_Panel1_Click(object sender, EventArgs e)
        {
            this.dtgBdFiles.ClearSelection();
        }


        private bool IsClickOnText(TreeView treeView, TreeNode node, Point location)
        {
            var hitTest = treeViewFolderExplore.HitTest(location);

            return hitTest.Node == node
                && hitTest.Location == TreeViewHitTestLocations.Label;
        }
        private void treeViewFolderExplore_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            logger.Trace("Begin");

            if (!IsClickOnText(treeViewFolderExplore, e.Node, e.Location))
            {
                treeViewFolderExplore.SelectedNode = null;
                treeViewFolderExplore.SelectedNodes = null;
                //MessageBox.Show("click");
            }
        }

        private void treeViewFolderExplore_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            logger.Trace("Begin");


            //if (e.Action == TreeViewAction.ByMouse)
            //{
            var position = treeViewFolderExplore.PointToClient(Cursor.Position);
            e.Cancel = !IsClickOnText(treeViewFolderExplore, e.Node, position);
            //}
        }

        private void menuDbGoToFolder_Click(object sender, EventArgs e)
        {

            // Goto Folder works only one document is selected.
            if (dtgBdFiles.GetSelectedDocuments().Count() > 1)
                return;

            // Get selected document for folder ID
            var selectedDocument = dtgBdFiles.GetSelectedDocument();

			var folder = FolderController.GetFolder(selectedDocument.id_folder);
			if (folder.archived)
			{
				// Show Folder Exploere
				this.tabControlSearch.SelectedIndex = 2;

				Invoke(new Action(() => loadArchiveFolderTreeView(new object[] { this.treeViewArchivedFolder, selectedDocument.id_folder })));
			}
			else
			{
				// Show Folder Exploere
				this.tabControlSearch.SelectedIndex = 1;

				Invoke(new Action(() => loadFolderTreeView(new object[] { this.treeViewFolderExplore, selectedDocument.id_folder })));
			}
        }


        /// <summary>
        /// Sync work space
        /// </summary>
        /// <param name="arg"></param>
        async private void M_Timer_OnSyncWordSpace(object arg)
        {
            logger.Trace("Workspace sync has been started");

            try
            {
                if (syncMgr.CanSync()) await syncMgr.Sync();
            }
            catch(Exception ex)
            {
				logger.Error(ex);
            }

            return;
        }

        /// <summary>
        /// Apply double click behaviour on treeview.
        /// This manges by explorer_doubleclick_behaviour table. the behaviour will only apply just below the id_folder.
        /// </summary>
        /// <param name="e"></param>
        private void ApplyDblClickBehaviour(System.Windows.Forms.MouseEventArgs e)
        {
            List<int> db = new List<int>();
            List<ExplorerDblClickBehaviour> inherited = new List<ExplorerDblClickBehaviour>();


            TreeNode targetNode = treeViewFolderExplore.GetNodeAt(new Point(e.X, e.Y));

            if (targetNode == null) return;

            // Foleder ID that double clicked
            Folder slcFolder = (targetNode == null) ? new Folder() : targetNode.Tag as Folder;


            var flders = PermissionController.drillUpfoldersby(slcFolder.id, SpiderDocsApplication.CurrentUserId);


            // The behaviour only applies just underneath folder of the difinition.
            // i > 0 means ignore selected folder it's self
            for( var i = flders.Count() -1; i > 0 ; i-- )
            {
                var id = flders[i].id;

                var behaviour = FolderExplorerBehaviourController.GetDblClickBehaviour(id).FirstOrDefault();
                if(behaviour != null)
                {
                    // check found behaviour's id_folder and selected folder's id is underneath.
                    if ( slcFolder.id == flders[i - 1].id)
                    {

                        switch (behaviour.id_behaviour)
                        {
                            case ExplorerDblClickBehaviour.en_Behaviour.Search:
                                var action = FolderExplorerBehaviourSearchController.GetDblClickBehaviour4Search(behaviour.id);

                                Console.WriteLine("[Dbl Click behaviour] Run");

                                ExplorerDblClickBehaviourAct(action, slcFolder);

                                return;
                        }

                    }
                }

            }
        }

		/// <summary>
		/// Double click behaviour for search ( type:1 )
		/// </summary>
		/// <param name="actions"></param>
		/// <param name="slcFolder"></param>
        private void ExplorerDblClickBehaviourAct(List<ExplorerDblClickBehaviourSearch> actions, Folder slcFolder)
        {
            logger.Trace("Begin");

            //set up search criteria from UI
            SearchCriteria Criteria = new SearchCriteria();

            table.Criteria.Clear();

            // Attributes
            foreach (ExplorerDblClickBehaviourSearch attr in actions)
            {

                if (attr.value_from == "##CUR_FLD_NAME##")
                {
                        Criteria.AttributeCriterias.Add(new DocumentAttribute() { id = attr.id_attr, atbValue = slcFolder.document_folder });
                }
            }

            if (Criteria.AttributeCriterias.Count == 0) return;
            Criteria = excludeArchive(Criteria);

            // set criteria to DTS_Document class
            table.Criteria.Add(Criteria);

            search(dtgBd_SearchMode.Normal);
        }

		/// <summary>
		/// show progress bar
		/// </summary>
		/// <returns>cancellation token</returns>
		System.Threading.CancellationTokenSource CreateBusy()
		{
			// _timerBusy.Stop();
			// _timerBusy.Start();
			// _timerBusy.AutoReset= false;

			 System.Threading.CancellationTokenSource tokenSource = new System.Threading.CancellationTokenSource();

			var frmBusyTask = System.Threading.Tasks.Task.Run(() => Application.Run(new frmBusy("")), tokenSource.Token);

			return tokenSource;
		}

    }
}