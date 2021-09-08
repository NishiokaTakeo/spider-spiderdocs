using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using Spider.Forms;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
	public partial class DocumentSearch : NewListBase
	{
//---------------------------------------------------------------------------------
        static Logger logger = LogManager.GetCurrentClassLogger();

		public delegate void EventFunc();
		public delegate void _SearchDone(List<DTS_Document> dtSearch, bool AutoSrc);
		
		List<DTS_Document> _searchFunc = new List<DTS_Document>();

		Document SelectedDoc_;
		public Document SelectedDoc { get { return SelectedDoc_; } }
		
		bool multimode = true;

		// Turn to true while Auto Searching
		bool AutoSrc = false;

		// Turn to true when Auto Search has done and dose not become false after that
		public bool AutoSrcDone = false;
		
		private Rectangle dragBoxFromMouseDown;
		private int rowIndexFromMouseDown;

		public string ReasonTxt
		{
			get { return txtReason.Text; }
			set { }
		}

		public int Count
		{
			get { return dtgFiles.Rows.Count; }
			set { }
		}

//---------------------------------------------------------------------------------
		public DocumentSearch() : base()
		{
			InitializeComponent();

			this.dtgFiles = this.dtgVersionFiles;
			
			this.lblReason.Enabled = SpiderDocsApplication.CurrentPublicSettings.reasonNewVersion;
			this.txtReason.Enabled = this.lblReason.Enabled;

			dtgVersionFiles.AutoGenerateColumns = false;
			
			if (!this.DesignMode) cboVersionFolder.UseDataSource4AssignedMe(permission: en_Actions.CheckIn_Out);			
		}

//---------------------------------------------------------------------------------
		public void build()
		{
			// DTS_Folder FolderTable = new DTS_Folder(true, true, true);
			// cboVersionFolder.DataSource = FolderTable.GetDataTable("document_folder");
			// cboVersionFolder.DisplayMember = "document_folder";
			// cboVersionFolder.ValueMember = "id";
            //cboVersionFolder.SelectedValue = 10;
            DTS_DocumentType TypeTable = new DTS_DocumentType(true);
			cboVersionType.DataSource = TypeTable.GetDataTable("type");
			cboVersionType.DisplayMember = "type";
			cboVersionType.ValueMember = "id";
		}

//---------------------------------------------------------------------------------
		private void dtgVersionFiles_Click(object sender, EventArgs e)
		{
			dtgVersionFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
		}

//---------------------------------------------------------------------------------
		private void dtgVersionFiles_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{         
			loadIcons(dtgVersionFiles.Rows, "dtgVersionFiles_Icon");
			getDataFromGrid();
		}

//---------------------------------------------------------------------------------
		private void dtgVersionFiles_SelectionChanged(object sender, EventArgs e)
		{
			getDataFromGrid();
			//getFileInformation();
		}

//---------------------------------------------------------------------------------
		private void dtgVersionFiles_MouseDown(object sender, MouseEventArgs e)
		{
			// Get the index of the item the mouse is below.
			rowIndexFromMouseDown = dtgVersionFiles.HitTest(e.X, e.Y).RowIndex;

			if(rowIndexFromMouseDown != -1)
			{
				// Remember the point where the mouse down occurred. 
				// The DragSize indicates the size that the mouse can move 
				// before a drag event should be started.                
				Size dragSize = SystemInformation.DragSize;

				// Create a rectangle using the DragSize, with the mouse position being
				// at the center of the rectangle.
				dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
															   e.Y - (dragSize.Height / 2)), dragSize);
			}
			else
				// Reset the rectangle if the mouse is not over an item in the ListBox.
				dragBoxFromMouseDown = Rectangle.Empty;
		}

//---------------------------------------------------------------------------------
		private void dtgVersionFiles_MouseMove(object sender, MouseEventArgs e)
		{
			if((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				// If the mouse moves outside the rectangle, start the drag.
				if(dragBoxFromMouseDown != Rectangle.Empty &&
					!dragBoxFromMouseDown.Contains(e.X, e.Y))
				{
					// Proceed with the drag and drop, passing in the list item.                    
					DragDropEffects dropEffect = dtgVersionFiles.DoDragDrop(
					dtgVersionFiles.Rows[rowIndexFromMouseDown],
					DragDropEffects.Link);
				}
			}
		}

//---------------------------------------------------------------------------------
		public event EventFunc VersionSearch_Click;
		private void btnVersionSearch_Click(object sender, EventArgs e)
		{
			if(VersionSearch_Click != null)
				VersionSearch_Click();
		}

//---------------------------------------------------------------------------------
		private void getDataFromGrid()
		{
			if((dtgVersionFiles.RowCount > 0) && (dtgVersionFiles.CurrentRow != null))
			{
				if(SelectedDoc_ == null)
					SelectedDoc_ = new Document();
				
				//get values from grid
				SelectedDoc_.id = Convert.ToInt32(dtgVersionFiles.Rows[dtgVersionFiles.CurrentRow.Index].Cells["c_id_doc"].Value);
				SelectedDoc_.id_folder = Convert.ToInt32(dtgVersionFiles.Rows[dtgVersionFiles.CurrentRow.Index].Cells["c_id_folder"].Value);
				SelectedDoc_.id_status = (en_file_Status)Convert.ToInt32(dtgVersionFiles.Rows[dtgVersionFiles.CurrentRow.Index].Cells["c_id_status"].Value);

				if(dtgVersionFiles.Rows[dtgVersionFiles.CurrentRow.Index].Cells["c_id_review"].Value != DBNull.Value)
					SelectedDoc_.id_review = Convert.ToInt32(dtgVersionFiles.Rows[dtgVersionFiles.CurrentRow.Index].Cells["c_id_review"].Value);
				else
					SelectedDoc_.id_review = -1;
					
				SelectedDoc_.version = Convert.ToInt32(Convert.ToString(dtgVersionFiles.Rows[dtgVersionFiles.CurrentRow.Index].Cells["c_Version"].Value).Replace("V", ""));
				SelectedDoc_.title = Convert.ToString(dtgVersionFiles.Rows[dtgVersionFiles.CurrentRow.Index].Cells["c_title"].Value);
				SelectedDoc_.id_docType = Convert.ToInt32(dtgVersionFiles.Rows[dtgVersionFiles.CurrentRow.Index].Cells["c_id_type"].Value);

				SelectedDoc_.id_sp_status = en_file_Sp_Status.normal;
			}else
			{
				SelectedDoc_ = null;
			}
		}

//---------------------------------------------------------------------------------
		private void getFileInformation()
		{
			if(SelectedDoc_ != null)
			{
				txtVersionId.Text = SelectedDoc_.id.ToString();
				txtVersionName.Text = SelectedDoc_.title;
				cboVersionFolder.SelectedValue = SelectedDoc_.id_folder;
				cboVersionType.SelectedValue = SelectedDoc_.id_docType;
			}
		}

//---------------------------------------------------------------------------------
		public bool chkReason()
		{
			bool ans = true;

			//check reason
			FormUtilities.PutErrorColour(txtReason, false, true);
			if(SpiderDocsApplication.CurrentPublicSettings.reasonNewVersion && string.IsNullOrWhiteSpace(txtReason.Text))
			{
				FormUtilities.PutErrorColour(txtReason, true);
				
				//MessageBox.Show(lib.msg_required_reason, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw new DocException(lib.msg_messabox_title,lib.msg_required_reason);
				//ans = false;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		// Normal search by pushing Search button.
		public void VersionSearch(string TitleWithExtension)
		{
			// Build the SQL sentence.
			List<SearchCriteria[]> criterias = new List<SearchCriteria[]>();
			criterias.Add(buildQuery(TitleWithExtension));

			VersionSearchStart(criterias);	// Start
		}

//---------------------------------------------------------------------------------
		// Search for all files which are registered on 'dtgNewVersion'.
		public void AutoVersionSearch(string TitleWithExtension)
		{
			if(AutoSrcDone)
				return;

			txtVersionName.Text = Path.GetFileNameWithoutExtension(TitleWithExtension);

			multimode = false;

			AutoVersionSearch(new List<string>() {TitleWithExtension});
		}

		public void AutoVersionSearch(List<string> TitlesWithExtension)
		{
			if(AutoSrcDone)
				return;

			List<SearchCriteria[]> criterias = new List<SearchCriteria[]>();

			AutoSrcDone = true;
			AutoSrc = true;		// Auto search ON

			// Make SQL sentences for all files.
			foreach(string TitleWithExtension in TitlesWithExtension)
			{
				if(!String.IsNullOrEmpty(TitleWithExtension))
					criterias.Add(buildQuery(TitleWithExtension));
			}
			
			VersionSearchStart(criterias);	// Start
		}

//---------------------------------------------------------------------------------
		public event EventFunc SearchStart;
		public void VersionSearchStart(List<SearchCriteria[]> criterias)
		{
			if(SearchStart != null)
				SearchStart();

			//start thread
			BackgroundWorker thread_search = new BackgroundWorker();
			thread_search.DoWork += new DoWorkEventHandler(thread_search_DoWork);
			thread_search.RunWorkerCompleted += new RunWorkerCompletedEventHandler(thread_search_WorkDone);
			thread_search.RunWorkerAsync(criterias);
		}

        //---------------------------------------------------------------------------------
        /// <summary>
        /// Find a file you seek or save as from DB and replace it to the grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void thread_search_DoWork(object sender, DoWorkEventArgs e)
		{
			List<SearchCriteria[]> criterias = (List<SearchCriteria[]>)e.Argument;
			int maxDocs = SpiderDocsApplication.CurrentPublicSettings.maxDocs;

			if(1 < criterias.Count)
				maxDocs = 1;

			_searchFunc.Clear();
            
            // search each file name in gridview
			foreach(SearchCriteria[] criteria in criterias)
			{
				DTS_Document searchFunc = new DTS_Document();
				searchFunc.Criteria.Add(criteria[0]);
				searchFunc.Criteria.Add(criteria[1]);
				searchFunc.Select(maxDocs);

				string seekingName = "";
				if(txtVersionName.Text.Trim() != "")
					seekingName = txtVersionName.Text.Trim();
				else if( true == AutoSrc && 0 < criteria[0].AbsoluteTitles.Count)
                    seekingName = criteria[0].AbsoluteTitles[0];
                else if(false == AutoSrc && 0 < criteria[0].Titles.Count)
					seekingName = criteria[0].Titles[0];

				DataTable foundAll = searchFunc.GetDataTable();

				if(0 < foundAll.Rows.Count)
				{
					foundAll.DefaultView.Sort = "title";

					foreach(DataRow curFound in foundAll.Rows)
					{
						if(curFound["title"].ToString() == seekingName)
						{
							int current_idx = foundAll.Rows.IndexOf(curFound);

							DataRow NewRow = foundAll.NewRow();
							NewRow.ItemArray = curFound.ItemArray;

							foundAll.Rows.RemoveAt(current_idx);
							foundAll.Rows.InsertAt(NewRow, 0);

							break;
						}
					}
				}

				_searchFunc.Add(searchFunc);
			}
		}

//---------------------------------------------------------------------------------
		public event _SearchDone SearchDone;
		void thread_search_WorkDone(object sender, RunWorkerCompletedEventArgs e)
		{
			if(e.Error == null)
			{
				if((0 < _searchFunc.Count) && (!AutoSrc || !multimode))
					dtgVersionFiles.DataSource = _searchFunc[0].GetDataTable();	//populate grid

				if(SearchDone != null)
					SearchDone(_searchFunc, AutoSrc);

			}else
			{
				if(SearchDone != null)
					SearchDone(null, false);
				
				//Utilities.regLog(e.Error.ToString());
                logger.Error(e.Error);
				MessageBox.Show(lib.msg_error_search, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			AutoSrc = false;	// Auto Search OFF
			multimode = true;	// Just initialize for next search
		}

//---------------------------------------------------------------------------------
		private SearchCriteria[] buildQuery(string TitleWithExtension)
		{
			SearchCriteria[] criteria = new SearchCriteria[2];
			criteria[0] = new SearchCriteria();

			if(txtVersionId.Text.Trim() != "")
			{
				int id;

				if(int.TryParse(txtVersionId.Text, out id))
					criteria[0].DocIds.Add(id);
			}

			criteria[0].Extensions.Add(Path.GetExtension(TitleWithExtension));
			criteria[0].Statuses.Add(en_file_Status.checked_in);


			if(AutoSrc)
				criteria[0].AbsoluteTitles.Add(TitleWithExtension);
			else if(txtVersionName.Text.Trim() != "")
				criteria[0].Titles.Add(txtVersionName.Text.Trim());

			if(cboVersionFolder.SelectedIndex > 0)
				criteria[0].FolderIds.Add(Convert.ToInt32(cboVersionFolder.SelectedValue));
			// else
			// 	criteria[0].FolderIds = PermissionController.FilterByPermission(SpiderDocsApplication.CurrentUserId,new Cache(SpiderDocsApplication.CurrentUserId).GetAssignedFolderToUser(),en_Actions.CheckIn_Out).Select(x => x.id).ToList();

			if(cboVersionType.SelectedIndex > 0)
				criteria[0].DocTypeIds.Add(Convert.ToInt32(cboVersionType.SelectedValue));

			criteria[0].SpStatuses.Add(en_file_Sp_Status.normal);

			criteria[1] = Utilities.ObjectClone(criteria[0]);
			criteria[1].SpStatuses.Clear();
			criteria[1].SpStatuses.Add(en_file_Sp_Status.review);
			criteria[1].SpStatuses.Add(en_file_Sp_Status.review_overdue);
			criteria[1].Review.UserIds.Add(SpiderDocsApplication.CurrentUserId);
			criteria[1].MergeType = en_SearchCriteriaMergeType.Bottom;

			return criteria;
		}

//---------------------------------------------------------------------------------
		public void Clear()
		{
			if(dtgVersionFiles.DataSource != null)
				((DataTable)(dtgVersionFiles.DataSource)).Rows.Clear();

			txtVersionId.Text = "";
			txtVersionName.Text = "";
			cboVersionFolder.SelectedIndex = 0;
			cboVersionType.SelectedIndex = 0;
		}

//---------------------------------------------------------------------------------
	}
}
