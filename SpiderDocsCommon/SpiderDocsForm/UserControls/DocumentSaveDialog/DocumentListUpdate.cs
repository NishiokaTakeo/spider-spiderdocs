using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
	public partial class NewVerList : NewList
	{
//---------------------------------------------------------------------------------
		public NewVerList() : base()
		{
			InitializeComponent();

			dtgFiles = dtgNewVersion;
		}

//---------------------------------------------------------------------------------
		// Make grid for importing new documents.
		public void build(List<string> filePath)
		{
			cbHeadNewList = new DatagridViewCheckBoxHeaderCell(true, false);
			cbHeadNewList.Value = "";
			dtgNewVersion.Columns["dtgNewVersion_Select"].HeaderCell = cbHeadNewList;

			// Add all files to the grid
			int i = 0;
			foreach(string wrk in filePath)
			{
				Document doc = new Document();
				doc.path = wrk;

				// Add new row
				dtgNewVersion.Rows.Add();
				DataGridViewRow row = dtgNewVersion.Rows[dtgNewVersion.Rows.GetLastRow(DataGridViewElementStates.None)];

				row.Cells["dtgNewVersion_Select"].Value = true;
				row.Cells["dtgNewVersion_File"].Value = doc.filename;
				row.Cells["dtgNewVersion_extension"].Value = doc.extension;
				row.Cells["dtgNewVersion_Arrow"].Value = ">>";
				row.Tag = i;

				DocumentList.Add(doc);
				i++;
			}

			loadIcons(dtgNewVersion.Rows, "dtgNewVersion_Icon");	// Set icons
			cbHeadNewList.ChkAllTick();
		}

//---------------------------------------------------------------------------------
		private void dtgNewVersion_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Link;
		}

//---------------------------------------------------------------------------------
		private void dtgNewVersion_DragDrop(object sender, DragEventArgs e)
		{
			int rowIndexOfItemUnderMouseToDrop;

			// The mouse locations are relative to the screen, so they must be
			// converted to client coordinates.
			Point clientPoint = dtgNewVersion.PointToClient(new Point(e.X, e.Y));

			// Get the row index of the item the mouse is below.
			rowIndexOfItemUnderMouseToDrop =
				dtgNewVersion.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

			// If the drag operation was a move then remove and insert the row.
			if(e.Effect == DragDropEffects.Link)
			{
				DataTable wrk = new DataTable();

				// Convert from DataGridViewRow to DataRowView
				DataGridViewRow row = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

				int target_idx = (int)dtgNewVersion.Rows[rowIndexOfItemUnderMouseToDrop].Tag;
				Document target_doc = DocumentList[target_idx];

				Document dropped_doc = (row.DataGridView as DocumentDataGridView).GetDocument(row.Index);

				// If file types match then link to 'dtgNewVersion'.
				if(target_doc.extension != dropped_doc.extension)
				{
					MessageBox.Show(lib.msg_type_mismatch, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

				}else
				{
					string path_backup = DocumentList[target_idx].path;

					DocumentList[target_idx] = dropped_doc;
					DocumentList[target_idx].path = path_backup;

					UpdateGrid();
				}
			}
		}

//---------------------------------------------------------------------------------
		// File selection check box ON / OFF
		private void dtgNewVersion_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if(e.ColumnIndex == dtgNewVersion.Columns["dtgNewVersion_Select"].Index)
			{
				if((bool)dtgNewVersion.SelectedCells[0].Value == false)
					dtgNewVersion.SelectedCells[0].Value = true;
				else
					dtgNewVersion.SelectedCells[0].Value = false;

				cbHeadNewList.ChkAllTick();
			}
		}

//---------------------------------------------------------------------------------
		private void dtgNewVersion_RowsRemoved(object sender,DataGridViewRowsRemovedEventArgs e)
		{
			cbHeadNewList.ChkAllTick();
		}

//---------------------------------------------------------------------------------
		public List<Document> getCheckedDocs()
		{
			return getCheckedDocs("dtgNewVersion_Select");
		}

//---------------------------------------------------------------------------------
		public List<DataGridViewRow> getCheckedRows()
		{
			return getCheckedRows("dtgNewVersion_Select");
		}

//---------------------------------------------------------------------------------
		public List<string> GetUnassignedFileNames()
		{
			List<string> ans = new List<string>();

			int i = 0;
			foreach(Document doc in DocumentList)
			{
				if(doc.id <= 0)
					ans.Add(dtgFiles.Rows[i].Cells["dtgNewVersion_File"].Value.ToString());

				i++;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public void ChangeFormMode(en_FormMode mode)
		{
			bool NormalMode = (mode == en_FormMode.Normal);

			dtgFiles.Columns["dtgNewVersion_Arrow"].Visible = NormalMode;
			dtgFiles.Columns["dtgNewVersion_Id"].Visible = NormalMode;
			dtgFiles.Columns["dtgNewVersion_Name"].Visible = NormalMode;
			dtgFiles.Columns["dtgNewVersion_Folder"].Visible = NormalMode;
			dtgFiles.AllowDrop = NormalMode;
		}


        /*
            don't need to check whether doc.id is empty or not.
            selected tab is 'Save New Version' doesn't force importing doc must have verson which already imported in database before.

                public bool validation()
                {
                    bool ans = true;

                    // Return cells' color to white.
                    foreach(DataGridViewRow Row in dtgNewVersion.Rows)
                        CellClearColour(Row);

                    List<Document> docs = getCheckedDocs();
                    List<DataGridViewRow> ChkRows = getCheckedRows();

                    // Loop for all rows.
                    int count = 0;
                    foreach(Document doc in docs)
                    {
                        string err = "";	// Error message.

                        // If there is file which dose not have file ID. -> Error
                        if(doc.id <= 0)
                        {
                            err = lib.msg_no_file_selected_NewVer;
                            ans = false;

                        }else if(1 < docs.Count(a => a.id == doc.id))
                        {
                            err = lib.msg_existing_file;
                            ans = false;
                        }

                        // Error
                        if(!ans)
                        {
                            // Change cells' color.
                            CellChangeColour(ChkRows[count], "dtgNewVersion_Name");
                            MessageBox.Show(err, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        count++;
                    }

                    return ans;
                }
        */
        //---------------------------------------------------------------------------------

        /// <summary>
        /// Restore original document info from path.
        /// When you call Link2dtgNewVersion method, file name is changed ( depend on Save As PDF). This method restores to original
        /// </summary>
        public void Restore4dtgNewVersion()
        {            
            for (int i = 0; i < DocumentList.Count; i++)
            {
                Document wrk = DocumentList[i];

                    string path_backup = wrk.path;

                wrk.title = System.IO.Path.GetFileName(path_backup);
            }

            UpdateGrid();
        }


        public void Link2dtgNewVersion(List<DTS_Document> src)
		{

                for (int i = 0; i < src.Count; i++)
                {
                    List<Document> wrk = src[i].GetDocuments<Document>();

                    if (0 < wrk.Count)
                    {
                        string path_backup = DocumentList[i].path;

                        DocumentList[i] = wrk[0];
                        DocumentList[i].path = path_backup;
                    }
                     else
                     // Not Found this file by the search. Should remove Name and Folder
                     {

					 	DocumentList[i].id = 0; // not found
					// 	DocumentList[i].id_version = 0;
                    //     DocumentList[i].title = "";
                    //     DocumentList[i].id_folder = 0;
                     }
                }

            UpdateGrid();


            //if(src.Count == DocumentList.Count)
            //{
            //	for(int i = 0; i < DocumentList.Count; i++)
            //	{
            //		List<Document> wrk = src[i].GetDocuments<Document>();

            //		if(0 < wrk.Count)
            //		{
            //			string path_backup = DocumentList[i].path;

            //			DocumentList[i] = wrk[0];
            //			DocumentList[i].path = path_backup;
            //		}
            //	}
            //}

            //UpdateGrid();
        }

        //---------------------------------------------------------------------------------
        private Document GetDocumentFromRow(DataGridViewRow row)
		{
			Document doc = new Document();

			doc.id = Convert.ToInt32(row.Cells["dtgNewVersion_Id"].Value);
			doc.id_version = Convert.ToInt32(row.Cells["dtgNewVersion_id_version"].Value);
			doc.id_user = Convert.ToInt32(row.Cells["dtgNewVersion_id_user"].Value);
			doc.id_folder = Convert.ToInt32(row.Cells["dtgNewVersion_id_folder"].Value);
			doc.id_docType = Convert.ToInt32(row.Cells["dtgNewVersion_id_type"].Value);
			doc.id_status = (en_file_Status)Convert.ToInt32(row.Cells["dtgNewVersion_id_status"].Value);
			doc.id_sp_status = en_file_Sp_Status.normal;
			doc.title = (string)row.Cells["dtgNewVersion_Name"].Value;
			doc.author = (string)row.Cells["dtgNewVersion_Author"].Value;
			doc.version = Convert.ToInt32(Convert.ToString(row.Cells["dtgNewVersion_Version"].Value).Replace("V", ""));

			return doc;
		}

//---------------------------------------------------------------------------------
		private void UpdateGrid()
		{
			foreach(DataGridViewRow Row in dtgNewVersion.Rows)
			{
				Document doc = DocumentList[(int)Row.Tag];

				if(0 < doc.id)
				{
					Row.Cells["dtgNewVersion_Id"].Value = doc.id;
					Row.Cells["dtgNewVersion_id_version"].Value = doc.id_version;
					Row.Cells["dtgNewVersion_id_user"].Value = doc.id_user;
					Row.Cells["dtgNewVersion_id_folder"].Value = doc.id_folder;
					Row.Cells["dtgNewVersion_id_type"].Value = doc.id_docType;
					Row.Cells["dtgNewVersion_id_status"].Value = doc.id_status;
					Row.Cells["dtgNewVersion_Name"].Value = doc.title;
					Row.Cells["dtgNewVersion_Folder"].Value = doc.name_folder;
					Row.Cells["dtgNewVersion_DocType"].Value = doc.id_docType;
					Row.Cells["dtgNewVersion_Author"].Value = doc.author;
					Row.Cells["dtgNewVersion_Version"].Value = doc.version;
					Row.Cells["dtgNewVersion_Date"].Value = doc.date;
				}
                else
                {                    
                    Row.Cells["dtgNewVersion_Id"].Value = string.Empty;
                    Row.Cells["dtgNewVersion_id_version"].Value = string.Empty;
                    Row.Cells["dtgNewVersion_id_user"].Value = string.Empty;
                    Row.Cells["dtgNewVersion_id_folder"].Value = string.Empty;
                    Row.Cells["dtgNewVersion_id_type"].Value = string.Empty;
                    Row.Cells["dtgNewVersion_id_status"].Value = string.Empty;
                    Row.Cells["dtgNewVersion_Name"].Value = string.Empty;
                    Row.Cells["dtgNewVersion_Folder"].Value = string.Empty;
                    Row.Cells["dtgNewVersion_DocType"].Value = string.Empty;
                    Row.Cells["dtgNewVersion_Author"].Value = string.Empty;
                    Row.Cells["dtgNewVersion_Version"].Value = string.Empty;
                    Row.Cells["dtgNewVersion_Date"].Value = string.Empty;
                }
            }
		}

//---------------------------------------------------------------------------------
	}
}

