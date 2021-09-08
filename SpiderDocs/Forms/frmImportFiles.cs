// This module should be merged to frmSaveDocExternal as it is doing same thing.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using SpiderDocsForms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using Document = SpiderDocsForms.Document;
using Spider.Drawing;
using NLog;
using System.Linq;

//---------------------------------------------------------------------------------
namespace SpiderDocs
{
	public partial class frmImportFiles : Form
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

//---------------------------------------------------------------------------------
		enum en_dtgLocalFileItems
		{
			Select = 0,
			Icon,
			Name,
			Size,
			Type,
			Path,
			Stastus,

			Max
		}

//---------------------------------------------------------------------------------
		int RowIdx = 0;
		int TagIdx = 0;
		bool EndPopulate = false;
		FileInfo infoFile;
		IconManager icon = new IconManager();

		DatagridViewCheckBoxHeaderCell cbHeader = new DatagridViewCheckBoxHeaderCell(true, false);

		List<DocumentAttribute> arrayAttribute = new List<DocumentAttribute>();
		List<DocumentProperty> PropertyList = new List<DocumentProperty>();
		//DocumentProperty OrigProperty = new DocumentProperty();
		List<int> SelectedTag = new List<int>();

		AttributeSearch uscAttribute = new AttributeSearch() { IncludeComboChildren=false};
		PropertyPanelNext tmp_PropertyPanel = new PropertyPanelNext();
        List<int[]> _notification_groups= new List<int[]>();

        //---------------------------------------------------------------------------------
        public frmImportFiles()
		{
			InitializeComponent();
			AddAttributeSearch();

			cbHeader.Value = "";
			cbHeader.Enabled = false;
			dtgLocalFile.Columns[(int)en_dtgLocalFileItems.Select].HeaderCell = cbHeader;

			dtgLocalFile.DragDrop += new DragEventHandler(ctlDrop);
			dtgLocalFile.DragEnter += new DragEventHandler(ctlEnter);

			tmp_PropertyPanel.Folder = cboFolder;
			tmp_PropertyPanel.Type = cboDocType;
			tmp_PropertyPanel.AttrPanel = uscAttribute;
            tmp_PropertyPanel.Title.Visible = true;

		}

		void AddAttributeSearch() // work around for a bug of VS designer which cannot deal with user contols properly
		{
			this.uscAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.uscAttribute.AutoScroll = true;
			this.uscAttribute.BackColor = System.Drawing.Color.Transparent;
			this.uscAttribute.Location = new System.Drawing.Point(9, 101);
			this.uscAttribute.Name = "uscAttribute";
			this.uscAttribute.Size = new System.Drawing.Size(276, 230);
			this.uscAttribute.TabIndex = 113;

			this.gpDetails.Controls.Add(this.uscAttribute);
		}

//---------------------------------------------------------------------------------
		private void frmImportFiles_Load(object sender, EventArgs e)
		{
			try
			{
				ckSaveLocal.Enabled = (SpiderDocsApplication.CurrentPublicSettings.allow_workspace);

                tmp_PropertyPanel.Setup(mode: PropertyPanelNext.en_FormMode.Multiple, folder_per: en_Actions.CheckIn_Out);

                InitNotificationGroup();

            }
            catch (Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default_open_form, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}

        //---------------------------------------------------------------------------------
        private void cboDocType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(cboDocType.SelectedIndex > 0)
			{
				uscAttribute.Enabled = true;
				uscAttribute.populateGrid(Convert.ToInt32(cboDocType.SelectedValue));
				uscAttribute.updateNow();

				if(EndPopulate)
				{
					EndPopulate = false;

					for(int i = 0; i < dtgLocalFile.SelectedRows.Count; i++)
					{
						DataGridViewRow row = dtgLocalFile.SelectedRows[i];
						DocumentProperty Property = PropertyList[(int)row.Tag];
						SetPropertyVal(Property);
                        Property.SetDateAttribute(DateTime.Now);

                        /*
						string FilePath = row.Cells[(int)en_dtgLocalFileItems.Path].Value.ToString();
						infoFile = new FileInfo(FilePath);

						Property.SetCreationDate(FilePath);
                        */
					}

					PopulateAttrVal();

					EndPopulate = true;
				}

			}else
			{
				uscAttribute.Enabled = false;
				uscAttribute.ClearPanel();
			}
		}

//---------------------------------------------------------------------------------
		private void dtgLocalFile_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				if(e.RowIndex >= 0 && e.ColumnIndex >= 0 && e.Button == MouseButtons.Right)
				{
					// If right unselected cell, select the cell.
					if(!dtgLocalFile.Rows[e.RowIndex].Selected)
					{
						dtgLocalFile.ClearSelection();
						dtgLocalFile.Rows[e.RowIndex].Selected = true;
					}

					//show menu
					System.Drawing.Rectangle r = dtgLocalFile.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
					this.menudtgLocalFile.Show((Control)sender, r.Left + e.X, r.Top + e.Y);
				}
			}
			catch(Exception error)
			{
				MessageBox.Show(lib.msg_error_file_menu, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
		}

//---------------------------------------------------------------------------------
		private void dtgLocalFile_SelectionChanged(object sender, EventArgs e)
		{
			if(!ckSameAtb.Checked && EndPopulate)
			{
				EndPopulate = false;

				ChgSelState();
				RstSelState();

				EndPopulate = true;
			}
		}

//---------------------------------------------------------------------------------
		private void ckSameAtb_CheckedChanged(object sender, EventArgs e)
		{
			EndPopulate = false;

			if(ckSameAtb.Checked)
				ChgSelState();
			else
				RstSelState();

			EndPopulate = true;
		}

//---------------------------------------------------------------------------------
		private void dtgLocalFile_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if(e.ColumnIndex == (int)en_dtgLocalFileItems.Select)
			{
				if((bool)dtgLocalFile.SelectedCells[0].Value == false)
					dtgLocalFile.SelectedCells[0].Value = true;
				else
					dtgLocalFile.SelectedCells[0].Value = false;

				cbHeader.ChkAllTick();
			}
		}

//---------------------------------------------------------------------------------
		private void menuCheck_Click(object sender, EventArgs e)
		{
			foreach(DataGridViewRow row in dtgLocalFile.SelectedRows)
				row.Cells[(int)en_dtgLocalFileItems.Select].Value = true;
		}

//---------------------------------------------------------------------------------
		private void menuUncheck_Click(object sender, EventArgs e)
		{
			foreach(DataGridViewRow row in dtgLocalFile.SelectedRows)
				row.Cells[(int)en_dtgLocalFileItems.Select].Value = false;
		}

//---------------------------------------------------------------------------------
		private void dtgLocalFile_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			cbHeader.ChkAllTick();
		}

//---------------------------------------------------------------------------------
		private void dtgLocalFile_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			cbHeader.ChkAllTick();
		}

//---------------------------------------------------------------------------------
		private void frmImportFiles_Resize(object sender, EventArgs e)
		{
			lblSpace.Size = new System.Drawing.Size(120 + (-800 + this.Width), 16);
		}

//---------------------------------------------------------------------------------
		private void ckSaveLocal_CheckedChanged(object sender, EventArgs e)
		{
			if(ckSaveLocal.Checked)
			{
				cboFolder.Enabled = false;
				cboDocType.Enabled = false;
				uscAttribute.Enabled = false;
				ckSameAtb.Enabled = false;

			}else
			{
				cboFolder.Enabled = true;
				cboDocType.Enabled = true;

				if(cboDocType.SelectedIndex != 0)
					uscAttribute.Enabled = true;

				ckSameAtb.Enabled = true;
			}

			foreach(Control c in uscAttribute.Controls)
			{
				if(ckSaveLocal.Checked)
				{
					if(c.GetType() != typeof(Label))
						c.Enabled = false;
				}else
				{
					c.Enabled = true;
				}
			}
		}

//---------------------------------------------------------------------------------
// Import Documents ---------------------------------------------------------------
//---------------------------------------------------------------------------------
		private void frmImportFiles_Shown(object sender,EventArgs e)
		{
			OpenImportDialog();
			RstSelState();
		}

//---------------------------------------------------------------------------------
		private void btnSelectFiles_Click(object sender, EventArgs e)
		{
			OpenImportDialog();
		}

//---------------------------------------------------------------------------------
		private void ctlEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
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
        private void ctlDrop(object sender, DragEventArgs e)
		{
			//get all files selected
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

			EndPopulate = false;
			//loop
			foreach(string pathFile in files)
			{
				//get file details
				infoFile = new FileInfo(pathFile);

				//add file to the grid
				addFileGrid(pathFile);
			}
			EndPopulate = true;

			lblCount.Text = dtgLocalFile.RowCount.ToString();
		}

//---------------------------------------------------------------------------------
		private void OpenImportDialog()
		{
			SpiderOpenFileDialog importFileDialog = new SpiderOpenFileDialog();
			importFileDialog.Multiselect = true;
			importFileDialog.RestoreDirectory = true;
			importFileDialog.Title = "Import file";

			if(importFileDialog.ShowDialog() == DialogResult.Cancel)
				return;

            foreach (String pathFile in importFileDialog.FileNames)
            {
                if (IsInGrid(pathFile))
                {
                    MessageBox.Show(lib.msg_existing_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
            }

			EndPopulate = false;
			//loop
			foreach(String pathFile in importFileDialog.FileNames)
			{
				infoFile = new FileInfo(pathFile);

				//add file to the grid
				addFileGrid(pathFile);

			}
			EndPopulate = true;

			lblCount.Text = dtgLocalFile.RowCount.ToString();
		}

//---------------------------------------------------------------------------------

        private bool IsInGrid(string pathFile)
        {


            for (int i = 0; i < dtgLocalFile.Rows.Count; i++)
            {
                var row = dtgLocalFile.Rows[i];
                if (Path.GetFileName(pathFile) == Path.GetFileName(row.Cells[(int)en_dtgLocalFileItems.Path].Value.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

		private void addFileGrid(string pathFile)
		{
			FileInfo vFile = new FileInfo(pathFile);

			//add information in datagrid
			PropertyList.Add(new DocumentProperty());
			dtgLocalFile.Rows.Add();
			dtgLocalFile.Rows[RowIdx].Cells[(int)en_dtgLocalFileItems.Select].Value = true;
			dtgLocalFile.Rows[RowIdx].Cells[(int)en_dtgLocalFileItems.Icon].Value = icon.GetSmallIcon(vFile.Extension);
			dtgLocalFile.Rows[RowIdx].Cells[(int)en_dtgLocalFileItems.Name].Value = vFile.Name;
			dtgLocalFile.Rows[RowIdx].Cells[(int)en_dtgLocalFileItems.Size].Value = (FileFolder.GetFileSizeInKB(pathFile) + " KB");
			dtgLocalFile.Rows[RowIdx].Cells[(int)en_dtgLocalFileItems.Type].Value = vFile.Extension;
			dtgLocalFile.Rows[RowIdx].Cells[(int)en_dtgLocalFileItems.Path].Value = vFile.FullName;
			dtgLocalFile.Rows[RowIdx].Cells[(int)en_dtgLocalFileItems.Stastus].Value = "Pending...";
			dtgLocalFile.Rows[RowIdx].Tag = TagIdx;
			RowIdx++;
			TagIdx++;

			cbHeader.ChkAllTick();
		}

//---------------------------------------------------------------------------------
// Save Documents -----------------------------------------------------------------
//---------------------------------------------------------------------------------
		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				bool Valid = false;

				ChgSelState();
				RstSelState();

				//get file path from dtg
				List<string> arrayPath = new List<string>();
				List<int> arrayIndex = new List<int>();
				List<int> arrayTag = new List<int>();
                List<int[]> arraNGIDs = new List<int[]>();

                foreach (DataGridViewRow row in dtgLocalFile.Rows)
				{
					if((bool)row.Cells[(int)en_dtgLocalFileItems.Select].Value)
					{
						dtgLocalFile.ClearSelection();
						row.Selected = true;

						Invoke(new Action(() => updateGrid()));

						string path = row.Cells[(int)en_dtgLocalFileItems.Path].Value.ToString();
						arrayPath.Add(path);
						arrayIndex.Add(row.Index);
						arrayTag.Add((int)row.Tag);
                        //arraNGIDs.Add(_notification_groups[(int)row.Tag];

                        tmp_PropertyPanel.Title.Text = Path.GetFileNameWithoutExtension(path);
						Valid = tmp_PropertyPanel.IsAllFieldsEntered(Path.GetExtension(path), ckSaveLocal.Checked);
						if(!Valid)
							break;
					}
				}

				if(Valid == false)
					return;

				//There are files to be saved?
				if(arrayPath.Count == 0)
				{
					MessageBox.Show(lib.msg_no_file_selected, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				//save folder id and doc type id into an array
				int FolderId = Convert.ToInt32(cboFolder.SelectedValue);
				int DocTypeId = Convert.ToInt32(cboDocType.SelectedValue);


                //disable components(avoid cross thread)
                enableComponents(false);

				//get attributes value
				if(ckSaveLocal.Checked == false)
					arrayAttribute = uscAttribute.getAttributeValues();

				//start thread to save files
				BackgroundWorker threadSaveFiles = new BackgroundWorker();
				threadSaveFiles.DoWork += async (object sender2, DoWorkEventArgs e2) => await saveFiles(sender2,e2);
				threadSaveFiles.RunWorkerCompleted += new RunWorkerCompletedEventHandler(saveFiles_Completed);
				threadSaveFiles.RunWorkerAsync(new object[] { arrayPath, arrayIndex, FolderId, DocTypeId, arrayTag/*, arraNGIDs */});

			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

        Document Fill(int n, string filePath, int id_folder, int id_docType, List<int> arrayTag)
        {
            Document objDoc = new Document();

            objDoc.path = ConvToSearchableFileIfPossible(filePath);
            objDoc.id_event = EventIdController.GetEventId(en_Events.Import);
            if (ckSameAtb.Checked)
            {
                objDoc.id_folder = id_folder;
                objDoc.id_docType = id_docType;
            }
            else
            {
                objDoc.id_folder = PropertyList[arrayTag[n]].id_folder;
                objDoc.id_docType = PropertyList[arrayTag[n]].id_docType;
                //objDoc.id_notification_group = PropertyList[arrayTag[n]].id_notification_group;
                arrayAttribute = PropertyList[arrayTag[n]].Attrs;
            }

            objDoc.Attrs = arrayAttribute;

            return objDoc;
        }

        //---------------------------------------------------------------------------------
        async System.Threading.Tasks.Task<bool> saveFiles(object sender, DoWorkEventArgs e)
		{
			List<string> arrayPath = (List<string>)((object[])e.Argument)[0];
			List<int> arrayIndex = (List<int>)((object[])e.Argument)[1];
			int FolderId = (int)((object[])e.Argument)[2];
			int DocTypeId = (int)((object[])e.Argument)[3];
			List<int> arrayTag = (List<int>)((object[])e.Argument)[4];
            //List<int[]> NotificationID = (List<int[]>)((object[])e.Argument)[5];

            int n = 0;
			int totfiles = arrayPath.Count;
			e.Result = true;

			List<Document> docs = new List<Document>();

            foreach (string filePath in arrayPath)
            {

                Document objDoc = Fill(n, filePath, FolderId, DocTypeId, arrayTag);

                if (ckSaveLocal.Checked)
                {
                    docs.Add(objDoc);

                    n++;
                    continue;
                }

                if (!objDoc.isNotDuplicated(true))
                {
                    //MessageBox.Show(lib.msg_duplicate_title, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    e.Result = 100;
                    return false;
                }

				if (!objDoc.__WarnForDuplicate(true))
				{
					e.Result = 101; // no dialog

                    //if (MessageBox.Show(lib.msg_warn_existing_file, lib.msg_messabox_title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
                    //	return;
                    //else
                    //	objDoc.hasAccepted = true;
                    return false;
				}

				docs.Add(objDoc);

			   n++;
            }

            n = 0;
            System.Threading.Tasks.Task<string>[] tasks = new System.Threading.Tasks.Task<string>[docs.Count];
            int i = 0;
            foreach (Document doc in docs)
			{
				bool ans = true;

				//update progress bar
				Invoke(new Action(() => updateProgress("Saving...", (Convert.ToInt32(n * 100) / totfiles))));

                infoFile = new FileInfo(doc.path);

				if(ckSaveLocal.Checked)
				{
					tasks[i] = Utilities.CopyToWorkSpace(infoFile.FullName, rdDeleteFile.Checked);
                    i++;

                }
                else
				{
                    string error = doc.AddDocument();

					if(error == "")
					{
						MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.GridUpdateCount);

						int[] ids_ngroup = GetNotificationGrp(arrayTag[n]);

                        DocumentNotificationGroupController.SaveOne(null, doc.id, ids_ngroup);

                    }
                    else
					{
                        logger.Error(error);
						ans = false;
					}
				}

				if(ans)
				{
					//update datagrid status columm
					Invoke(new Action(() => updateGridStatusColumm(arrayIndex[n] - n )));

					//After save
					AlterFile(infoFile.FullName);

				}else
				{
					e.Result = false;
				}

				n++;
				RowIdx--;

			}

            if (ckSaveLocal.Checked == false)
                arrayAttribute.Clear();
            else
                await System.Threading.Tasks.Task.WhenAll(tasks);

			return true;
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

            // can make searchable file
            OCRManager ocrManager = new OCRManager(path);
            ocrManager.GetPDF(dest);

            return dest;
        }

        //---------------------------------------------------------------------------------
        void saveFiles_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
		    if (e.Result.GetType() == typeof(int))
            {
                switch (e.Result)
                {
                    case 100:
                        MessageBox.Show(lib.msg_found_duplicate_docs, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else if(e.Error == null)
			{
				//update progress bar
				Invoke(new Action(() => updateProgress("Completed!", 100)));

                MessageBox.Show(lib.msg_sucess_imported_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if(!((bool)e.Result))
			{
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

			}else
			{
				//update progress bar
				Invoke(new Action(() => updateProgress("Operation not performed.", 0 )));
			}

			//enable components
			enableComponents(true);
		}

//---------------------------------------------------------------------------------
		private void AlterFile(string pathFile)
		{
			try
			{
				FileInfo fi = new FileInfo(pathFile);

				if(ckSaveLocal.Checked == false)
				{
					if(rdChangeName.Checked == true)
						File.Move(fi.FullName, fi.DirectoryName + "\\DMS- " + fi.Name.ToString());

					if(rdDeleteFile.Checked == true)
					{
						fi.IsReadOnly = false;
						fi.Delete();
					}

				}else if(rdChangeName.Checked)
				{
					File.Move(fi.FullName, fi.DirectoryName + "\\DMS- " + fi.Name.ToString());
				}
			}
			catch(IOException error)
			{
				logger.Error(error);
			}
		}

//---------------------------------------------------------------------------------
// methods ------------------------------------------------------------------------
//---------------------------------------------------------------------------------
		void PopulateAttrVal()
		{
			if(!ckSameAtb.Checked
			&& !ckSaveLocal.Checked
			&& (0 < dtgLocalFile.SelectedRows.Count))
			{
				DocumentProperty wrk = new DocumentProperty();

                if (1 < dtgLocalFile.SelectedRows.Count)
                {
                    List<DocumentProperty> Selected = new List<DocumentProperty>();

                    foreach (DataGridViewRow row in dtgLocalFile.SelectedRows)
                        Selected.Add(PropertyList[(int)row.Tag]);


                    wrk.PropertyCopy(Selected[0]);
					wrk = Document.PropertyGetSameVal(wrk, Selected);
                }
                else
				{
					wrk = PropertyList[(int)dtgLocalFile.SelectedRows[0].Tag];

				}



                tmp_PropertyPanel.ClearError();

                if (wrk.id_folder > -1)  cboFolder.SelectedValue = wrk.id_folder;
				if (wrk.id_docType > -1)  cboDocType.SelectedValue = wrk.id_docType;

                //int idx = (int)dtgLocalFile.SelectedRows[0].Tag;
                //var ls = cboNotificationGroup.Items.Cast<DocumentAttributeCombo>().ToList();

                //int i = -1;
                //if(_notification_groups.ElementAtOrDefault(idx) != null)
                //{
                //    ls.ForEach(item => {
                //        ++i;

                //        if( _notification_groups.Contains( new int[] { item.id }))
                //            cboNotificationGroup.SetItemChecked(i, true);
                //        else
                //            cboNotificationGroup.SetItemChecked(i, false);

                //    });
                //}
                //else
                //{
                //    ls.ForEach(item => {
                //        cboNotificationGroup.SetItemChecked(++i, false);
                //    });
                //}

                if (0 < wrk.id_docType)
					uscAttribute.populateGrid(wrk);

				uscAttribute.updateNow();
			}
		}

//---------------------------------------------------------------------------------
		void GetSelectedTag()
		{
			SelectedTag.Clear();

			foreach(DataGridViewRow row in dtgLocalFile.SelectedRows)
				SelectedTag.Add((int)row.Tag);
		}

//---------------------------------------------------------------------------------
		void SetPropertyVal(DocumentProperty dst)
		{
			if(cboDocType.SelectedValue != null)
				dst.id_docType = Convert.ToInt32(cboDocType.SelectedValue);

			if(cboFolder.SelectedValue != null)
				dst.id_folder =  Convert.ToInt32(cboFolder.SelectedValue);

			dst.Attrs = uscAttribute.getAttributeValuesCopy();

            //if ( cboNotificationGroup.SelectedValue != null)
            //dst.id_notification_group = Convert.ToInt32(cboNotificationGroup.SelectedValue);
        }

//---------------------------------------------------------------------------------
		void enableComponents(bool enable)
		{
			toolStripMenu.Enabled = enable;
			gpDetails.Enabled = enable;
			gpAfterSave.Enabled = enable;

			if(!ckSaveLocal.Checked)
				ckSameAtb.Enabled = enable;

			if(SpiderDocsApplication.CurrentPublicSettings.allow_workspace)
				ckSaveLocal.Enabled = enable;
		}

//---------------------------------------------------------------------------------
		void ChgSelState()
		{
			//DocumentProperty wrk = new DocumentProperty();
			//SetPropertyVal(wrk);

			//if(!Document.PropertyCompare(OrigProperty, wrk))
			//{
				foreach(int tag in SelectedTag)
                {
					SetPropertyVal(PropertyList[tag]);

                    AddNotificationGrp(tag,cboNotificationGroup.getComboValue<DocumentAttributeCombo>().Select(a => a.id).ToArray());
                }
            //}
		}

        void AddNotificationGrp(int idx,int[] groups)
        {
            for(int i = 0; i <= idx; i ++)
            {
                try { var dummy = _notification_groups[i]; } catch { _notification_groups.Insert(i, new int[] { }); }
            }
            _notification_groups[idx] = cboNotificationGroup.getComboValue<DocumentAttributeCombo>().Select(a => a.id).ToArray();
        }

		/// <summary>
		/// Get notification group ids user selected. This care for sameAtb check value.
		/// </summary>
		/// <param name="idx">Index for notification group array</param>
		/// <returns></returns>
		int[] GetNotificationGrp(int idx)
		{
			int[] ans = new int[] { };

			int[] ngrpIds = cboNotificationGroup.getComboValue<DocumentAttributeCombo>().Select(a => a.id).ToArray();


			if (this.ckSameAtb.Checked)
			{
				ans = ngrpIds;
			}
			else
			{
				ans = _notification_groups[idx];
			}

			return ans;
		}
//---------------------------------------------------------------------------------
		void RstSelState()
		{
			GetSelectedTag();
			PopulateAttrVal();
            RstNotificationGroup();

            //OrigProperty = new DocumentProperty();
            //SetPropertyVal(OrigProperty);


        }

        void RstNotificationGroup()
        {
            if (dtgLocalFile.SelectedRows.Count == 0) return;

            int idx = (int)dtgLocalFile.SelectedRows[0].Tag;
            var ls = cboNotificationGroup.Items.Cast<DocumentAttributeCombo>().ToList();

            int i = -1;
            if (_notification_groups.ElementAtOrDefault(idx) != null)
            {
                ls.ForEach(item => {
                    ++i;

                    if (_notification_groups[idx].Contains(item.id ))
                        cboNotificationGroup.SetItemChecked(i, true);
                    else
                        cboNotificationGroup.SetItemChecked(i, false);

                });
            }
            else
            {
                ls.ForEach(item => {
                    cboNotificationGroup.SetItemChecked(++i, false);
                });
            }
        }
//---------------------------------------------------------------------------------
// update controls-----------------------------------------------------------------
//---------------------------------------------------------------------------------
		void updateProgress(string txt, int barValue)
		{
			if(txt != "")
				lblProgressText.Text = txt;

			progressBar.Value = barValue;
		}

//---------------------------------------------------------------------------------
		void updateGridStatusColumm(int rowIndex)
		{
			dtgLocalFile.Rows.RemoveAt(rowIndex);
		}

//---------------------------------------------------------------------------------
		void updateGrid()
		{
			dtgLocalFile.Update();
		}

//---------------------------------------------------------------------------------
	}
}
