using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
	public partial class DocumentListInsert : NewList
	{
        Logger logger = LogManager.GetCurrentClassLogger();

        //---------------------------------------------------------------------------------	
        public DocumentListInsert()
		{
            logger.Trace("Begin");

            InitializeComponent();

			this.dtgFiles = _dtgFiles;
		}

//---------------------------------------------------------------------------------
		// Make grid for importing new documents.
		public void build(List<string> filePath)
		{
            logger.Trace("Begin");

            cbHeadNewList = new DatagridViewCheckBoxHeaderCell(true, false);
			cbHeadNewList.Value = "";
			dtgFiles.Columns["dtgFiles_Select"].HeaderCell = cbHeadNewList;

			EndPopulate = false;
			// Add all files to the grid
			int i = 0;
			foreach(string wrk in filePath)
			{
				// Add new row
				Document doc = new Document();
				doc.path = wrk;
				doc.SetCreationDate();

				dtgFiles.Rows.Add();
				DataGridViewRow row = dtgFiles.Rows[dtgFiles.Rows.GetLastRow(DataGridViewElementStates.None)];

				row.Cells["dtgFiles_Select"].Value = true;
				row.Cells["dtgFiles_File"].Value = doc.filename;
				row.Cells["dtgFiles_Name"].Value = doc.filename;
				row.Tag = i;

				DocumentList.Add(doc);
				i++;
			}
			EndPopulate = true;

			loadIcons(dtgFiles.Rows, "dtgFiles_Icon");	// Set icons
			cbHeadNewList.ChkAllTick();
			GetSelectedTag();
		}

//---------------------------------------------------------------------------------
		public event EventHandler SelectionChanged;
		private void dtgFiles_SelectionChanged(object sender, EventArgs e)
		{
            //bubble the event up to the parent
            if (this.SelectionChanged != null)
				this.SelectionChanged(this, e);
		}

//---------------------------------------------------------------------------------
		private void dtgFiles_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if(e.ColumnIndex == dtgFiles.Columns["dtgFiles_Name"].Index)
			{
				dtgFiles.BeginEdit(false);	// Change to edit state when click 'Name' cells.
			}
		}

//---------------------------------------------------------------------------------
		private void dtgFiles_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			Document doc = DocumentList[(int)dtgFiles.Rows[e.RowIndex].Tag];

			if(dtgFiles.Columns[e.ColumnIndex].Name == "dtgFiles_Name")
			{
				if((dtgFiles.Rows[e.RowIndex].Cells["dtgFiles_Name"].Value == null)
				|| (String.IsNullOrWhiteSpace((string)dtgFiles.Rows[e.RowIndex].Cells["dtgFiles_Name"].Value)))
				{
					doc.title = "";

				}else
				{
					doc.title = (string)dtgFiles.Rows[e.RowIndex].Cells["dtgFiles_Name"].Value + doc.extension;
				}
			}
		}

//---------------------------------------------------------------------------------
		// File selection check box ON / OFF
		private void dtgFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if(e.ColumnIndex == dtgFiles.Columns["dtgFiles_Select"].Index)
			{
				if((bool)dtgFiles.SelectedCells[0].Value == false)
					dtgFiles.SelectedCells[0].Value = true;
				else
					dtgFiles.SelectedCells[0].Value = false;

				cbHeadNewList.ChkAllTick();
			}
		}

//---------------------------------------------------------------------------------
		private void dtgFiles_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			cbHeadNewList.ChkAllTick();
		}

//---------------------------------------------------------------------------------
		public void setPropertyToSelectedDocs(DocumentProperty src)
		{
            logger.Trace("Begin");

            List<Document> docs = getPrevSelectedDocs();
			int size = docs.Count;

			for(int i = 0; i < size; i++)
				docs[i].PropertyCopy(src);
		}

//---------------------------------------------------------------------------------
		public void setDateToSelectedDocs()
		{
            logger.Trace("Begin");

            List<Document> docs = getPrevSelectedDocs();
			int size = docs.Count;

			for(int i = 0; i < size; i++)
				docs[i].SetCreationDate();
		}

//---------------------------------------------------------------------------------
		public List<Document> getCheckedDocs()
		{
            logger.Trace("Begin");

            return getCheckedDocs("dtgFiles_Select");
		}

//---------------------------------------------------------------------------------
		public List<DataGridViewRow> getCheckedRows()
		{
            logger.Trace("Begin");

            return getCheckedRows("dtgFiles_Select");
		}

//---------------------------------------------------------------------------------
		public List<DataGridViewCell> getCheckedNameCells()
		{
            logger.Trace("Begin");

            List<DataGridViewCell> ans = new List<DataGridViewCell>();

			List<DataGridViewRow> rows = getCheckedRows();
			foreach(DataGridViewRow row in rows)
				ans.Add(row.Cells["dtgFiles_Name"]);

			return ans;
		}

//---------------------------------------------------------------------------------
		public DocumentProperty getCommonPropertyVals()
		{
            logger.Trace("Begin");

            if (dtgFiles.SelectedRows.Count <= 0)
				return null;

			List<Document> docs = getSelectedDocs();
			DocumentProperty wrk = new DocumentProperty();
			wrk.PropertyCopy(docs[0]);

			if(1 < docs.Count)
				wrk = DocumentProperty.PropertyGetSameVal(wrk, docs.Cast<DocumentProperty>().ToList());

			return wrk;
		}

//---------------------------------------------------------------------------------
		public DataGridViewRow SelectChkedRow(int idx)
		{
			return SelectChkedRow(idx, "dtgFiles_Select");
		}

//---------------------------------------------------------------------------------
		public void ClearCellColor()
		{
			dtgFiles.ClearSelection();

			// Return cells' color to white.
			foreach(DataGridViewRow Row in dtgFiles.Rows)
				CellClearColour(Row);
		}

//---------------------------------------------------------------------------------
		// A common procedure
		public bool validation(bool local, bool scan)
		{
            logger.Trace("Begin");

            List<Document> docs = getCheckedDocs();
			List<DataGridViewRow> ChkRows = getCheckedRows();

			int count = 0;
			foreach(Document doc in docs)
			{
				if(!scan)
				{
					//Title
					if((String.IsNullOrWhiteSpace(doc.title))
					|| (Path.GetFileNameWithoutExtension(doc.title) == ""))
					{
						CellChangeColour(ChkRows[count], "dtgFiles_Name");
						MessageBox.Show(lib.msg_required_doc_title, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return false;
					}
					else if(!FileFolder.IsValidFileName(doc.title))
					{
						CellChangeColour(ChkRows[count], "dtgFiles_Name");
						MessageBox.Show(lib.msg_wrong_file_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
					else
					{
						if(!SpiderDocsApplication.CurrentPublicSettings.allow_duplicatedName && !DuplicatedTitleChk(docs, count))
							return false;
					}

				}else
				{
					//Non Image file
					if(!FileFolder.extensionsForScan.Contains(doc.extension))
					{
						CellChangeColour(ChkRows[count], "dtgFiles_File");
						MessageBox.Show(lib.msg_not_all_image_files, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return false;
					}
				}

				// comming here means OK
				CellClearColour(ChkRows[count]);
				count++;
			}

			return true;
		}

//---------------------------------------------------------------------------------
		public void ChangeFormMode(en_FormMode mode)
		{
            logger.Trace("Begin");

            if (mode == en_FormMode.Normal)
			{
				dtgFiles.Columns["dtgFiles_Name"].Visible = true;

			}else
			{
				dtgFiles.Columns["dtgFiles_Name"].Visible = false;
			}
		}

//---------------------------------------------------------------------------------
		bool DuplicatedTitleChk(List<Document> docs, int count)
		{
			bool ans = true;
			string keyValue = docs[count].title;

			if(1 < docs.Count(a => a.title == keyValue))
			{
				CellChangeColour(dtgFiles.SelectedRows[count], "dtgFiles_Name");
				MessageBox.Show(lib.msg_existing_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	
				ans = false;
			}

			return ans;		
		}

//---------------------------------------------------------------------------------
		delegate void updateGrid();
		public void update()
		{
			Invoke(new updateGrid(updateGridFunc));	
		}

		void updateGridFunc()
		{
			dtgFiles.Update();
		}

//---------------------------------------------------------------------------------
	}
}
