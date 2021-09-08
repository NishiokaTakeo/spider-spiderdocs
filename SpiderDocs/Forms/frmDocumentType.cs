using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SpiderDocsModule;
using SpiderDocsForms;
using lib = SpiderDocsModule.Library;
using NLog;
using System.Linq;

namespace SpiderDocs
{
	public partial class frmDocumentType : Form
	{
		int itemIndex;
		int idType;
		bool endLoad = false;
		decimal mousePosition1;
		decimal mousePosition2;
		DTS_DocumentType DA_DocumentType = new DTS_DocumentType(false);
        static Logger logger = LogManager.GetCurrentClassLogger();

		public frmDocumentType()
		{
			InitializeComponent();

			document_typeBindingSource.DataSource = DA_DocumentType.GetDataTable("type");
		}

		private void frmAttribute_Load(object sender, EventArgs e)
		{
			try
			{
				if(document_typeBindingSource.Count == 0) { btnSave.Enabled = false; btnDelete.Enabled = false; }

				endLoad = true;
			}
			catch(Exception error)
			{
				logger.Error(error);
			}
		}



        public void populateAttributeExternal()
		{
			if(dtgDocType.Rows.Count > 0)
			{
				endLoad = false;
				populateAttribute(idType);
				endLoad = true;
			}
		}

		private void populateAttribute(int id)
		{
			try
			{
                dragAndDropListView.Rows.Clear();                
                dragAndDropListView.Enabled = true;
				btnDelete.Enabled = true;

				List<DocumentAttribute> arrayAttribute = DocumentAttributeController.GetAttributesCache(true);
				List<AttributeDocType> Assigneds = DocumentAttributeController.GetIdListByDocType(doc_type_id: id);

                foreach (var assigned in Assigneds)
				{
                    var attr = arrayAttribute.Find(a => a.id == assigned.id_attribute);
                    AddItem(true, attr, assigned.duplicate_chk);
                    btnDelete.Enabled = false;
				}
                
                arrayAttribute = arrayAttribute.FindAll(a => !Assigneds.Select(x => x.id_attribute).ToList().Contains(a.id));
				foreach(DocumentAttribute attr in arrayAttribute)
				{
                    AddItem(false, attr, false);
				}
			}
			catch(Exception error)
			{
				logger.Error(error);
			}
		}

		ListViewItem GetListItem(DocumentAttribute src)
		{
			ListViewItem item = new ListViewItem("");
			item.SubItems.Add(src.name);
			item.SubItems.Add(DocumentAttribute.GetTypeName(src.id_type));
			item.Tag = src.id;
			item.ImageIndex = 0;

			return item;
		}

        void AddItem(bool chked, DocumentAttribute src, bool duplicate_chk)
        {
            int idx = dragAndDropListView.Rows.Count;
            dragAndDropListView.Rows.Add(chked, src.name, DocumentAttribute.GetTypeName(src.id_type), duplicate_chk);
            dragAndDropListView.Rows[idx].Tag = src.id;

        }
        private void dragAndDropListView1_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				mousePosition2 = Cursor.Position.Y;

				if(mousePosition1 < mousePosition2)
					dragAndDropListView.Rows.RemoveAt(itemIndex);
				else
					dragAndDropListView.Rows.RemoveAt(itemIndex + 1);

				List<int> arrayId = new List<int>();

				foreach(ListViewItem item in dragAndDropListView.Rows)
				{
					if(item.Checked == true)
						arrayId.Add((int)item.Tag);
				}

				DocumentTypeController.UpdateAttributeInDocType(arrayId, idType);
			}
			catch(Exception error)
			{ 
				logger.Error(error);
			}
		}

		private void dragAndDropListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			itemIndex       = e.Item.Index;
			mousePosition1  = Cursor.Position.Y;
		}

		private void dragAndDropListView1_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			try
			{
				if(endLoad)
				{
					int id_atb = Convert.ToInt32(dragAndDropListView.Rows[e.Index].Tag);

					if(e.NewValue == CheckState.Checked)
					{
						//save
						if(DocumentTypeController.IsAttributeValueExists(id_atb, idType) == false)
							DocumentTypeController.AssignAttributeToDocType(id_atb, idType);

						btnDelete.Enabled = false;
					}
					else
					{
						//delete
						DocumentTypeController.RemoveAttributeFromDocType(id_atb, idType);
					}
				}
			}
			catch(Exception error)
			{
				logger.Error(error);
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			txtDocType.Focus();
			btnAdd.Enabled = false;
			btnDelete.Enabled = true;
			btnSave.Enabled = true;
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if(((DataRowView)document_typeBindingSource.Current).IsNew == false)
				{
					int id_type = Convert.ToInt32(dtgDocType.Rows[dtgDocType.CurrentRow.Index].Cells[0].Value);
					bool hasFiles = DocumentTypeController.IsDocTypeUsed(id_type);

					if(hasFiles)
					{
						MessageBox.Show(lib.msg_doc_type_used, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);
						return;
					}

					if(MessageBox.Show(lib.msg_ask_delete_record, lib.msg_messabox_title, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
						deteleDocType();
				}else
				{
					deteleDocType();
				}
			}
			catch(Exception error)
			{
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
		}

		private void deteleDocType()
		{
			int idx = Utilities.ConvDataRowId(((DataRowView)document_typeBindingSource.Current).Row.ItemArray[0]);
			
			document_typeBindingSource.RemoveCurrent();
			Validate();
			document_typeBindingSource.EndEdit();

			if(0 < idx)
				DA_DocumentType.Delete(idx);

			btnAdd.Enabled = true;

			if(dtgDocType.RowCount == 0)
			{
				btnSave.Enabled = false;
				btnDelete.Enabled = false;
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if(!validation())
				{
					document_typeBindingSource.CancelEdit();

				}else
				{
					Validate();
					document_typeBindingSource.EndEdit();

					int idx = Utilities.ConvDataRowId(((DataRowView)document_typeBindingSource.Current).Row.ItemArray[0]);
					if(0 < idx)
						DA_DocumentType.Update((DataRowView)document_typeBindingSource.Current);
					else
						DA_DocumentType.Insert((DataRowView)document_typeBindingSource.Current);

					Utilities.RefreshDocumentTypes();
				}

				btnAdd.Enabled = true;
			}
			catch(Exception error)
			{
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
		}

		private bool validation()
		{
			try
			{
				if(txtDocType.Text == "")
				{
					txtDocType.BackColor = Color.LavenderBlush;
					txtDocType.Focus();
					MessageBox.Show(lib.msg_required_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;

				}else
				{
					txtDocType.BackColor = Color.White;
				}

				//check unique folder name
				if(DocumentTypeController.IsDocTypeExists(txtDocType.Text, Utilities.ConvDataRowId(((DataRowView)document_typeBindingSource.Current).Row.ItemArray[0])))
				{
					MessageBox.Show(lib.msg_existing_doc_type, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}

				return true;
			}
			catch(Exception error)
			{
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
				return false;
			}
		}

		private void dtgDocType_SelectionChanged(object sender, EventArgs e)
		{
			//remove pending chances
			if(((DataRowView)document_typeBindingSource.Current != null))
			{
				if(((DataRowView)document_typeBindingSource.Current).IsNew == false)
				{
					foreach(DataRowView rowItem in document_typeBindingSource)
					{
						if((rowItem.Row.ItemArray[0] == DBNull.Value) || (Convert.ToInt32(rowItem.Row.ItemArray[0]) < 0))
						{
							rowItem.Delete();
							btnAdd.Enabled = true;
						}
					}
				}
			}

			if((dtgDocType.Rows.Count > 0) && (dtgDocType.Rows[dtgDocType.CurrentRow.Index].Cells[0].Value != DBNull.Value))
			{
				endLoad = false;
				idType = Convert.ToInt32(dtgDocType.Rows[dtgDocType.CurrentRow.Index].Cells[0].Value);
				populateAttribute(idType);
				endLoad = true;
			}
		}

		private void txtDocType_Validating(object sender, CancelEventArgs e)
		{
			if((DataRowView)document_typeBindingSource.Current != null)
			{
				if(Utilities.ConvDataRowId(((DataRowView)document_typeBindingSource.Current).Row.ItemArray[0]) > 0)
				{
					if(validation() == false)
					{
						document_typeBindingSource.CancelEdit();
						return;
					}
				}
			}
		}
        bool _bf_chkAtb = false;
        bool _bf_dupchk = false;
        private void dragAndDropListView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
			const int atbIdx = 0, dupchkIdx = 3 ;
			
            if (e.RowIndex == -1) return;

            if (e.ColumnIndex == 0 || e.ColumnIndex == 3)
            {
                // Handle checkbox state change here               
                try
                {
                    if (endLoad)
                    {
                        var selectedRow = dragAndDropListView.Rows[e.RowIndex];
                        int id_atb = Convert.ToInt32(dragAndDropListView.Rows[e.RowIndex].Tag);
                        bool chkAtb = bool.Parse(selectedRow.Cells[atbIdx].Value.ToString());
                        bool dupchk = bool.Parse(selectedRow.Cells[dupchkIdx].Value.ToString());    //No Duplication


                        //if ( (_bf_dupchk && !dupchk) || (_bf_chkAtb && !chkAtb))
                        //{
							List<int> id_atbs = new List<int>();

							for (int i = 0; i < dragAndDropListView.Rows.Count; i++)
							{
								var row = dragAndDropListView.Rows[i];

								if ( bool.Parse(row.Cells[atbIdx].Value.ToString()) && bool.Parse(row.Cells[dupchkIdx].Value.ToString()))
									id_atbs.Add((int)dragAndDropListView.Rows[i].Tag);
							}

							List<int> typesOfAtb = DocumentAttributeController.GetIdListByDocType(doc_type_id: idType).Select(x => x.id_attribute).ToList();
                            string atbs = string.Join(",", id_atbs/*typesOfAtb.Where(_id_atb => _id_atb != id_atb)*/);
                            bool canogo = canCheckOff("", idType, atbs);

                            if(!canogo)
                            {
                                MessageBox.Show(lib.msg_found_duplicate_docs, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

                                populateAttributeExternal();
                                return;
                            }

                            // Warn if documents with same title is same directory.
                            //if(dupchk && hasWarnForCheckOff())
                            //{
                            //    if (MessageBox.Show(lib.msg_warn_type_existing_file, lib.msg_messabox_title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
                            //    {
                            //        populateAttributeExternal();
                            //        return;
                            //    }
                            //}

                        //}

                        //delete
                        DocumentTypeController.RemoveAttributeFromDocType(id_atb, idType);
                        if(chkAtb == true )
                        {
                            //save
                            DocumentTypeController.AssignAttributeToDocType(id_atb, idType, dupchk);
                        }

                        btnDelete.Enabled = false;

                    }
                }
                catch (Exception error)
                {
                    logger.Error(error);
                }
            }
        }

		bool canCheckOff(string title, int id_type, string id_atbs)
		{
			int dup_count = StoredProcedureController.canUnCheckDuplicate(title, id_type, id_atbs);

			return dup_count == 0 || dup_count == 1;
		}

        /// <summary>
        /// Check document with same name has more than one in same folder.
        /// THIS IS ABOLISHED
        /// </summary>
        /// <returns>true: no duplication.</returns>
        //bool hasWarnForCheckOff()
        //{
        //    int dup_count = StoredProcedureController.warnUnCheckDuplicate();

        //    return (dup_count == 0 || dup_count == 1) == false;
        //}

        private void dragAndDropListView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;

            // End of edition on each click on column of checkbox
            if (e.ColumnIndex == 0 || e.ColumnIndex == 3)
            {
                var selectedRow = dragAndDropListView.Rows[e.RowIndex];
                _bf_chkAtb = bool.Parse(selectedRow.Cells[0].Value.ToString());
                _bf_dupchk = bool.Parse(selectedRow.Cells[3].Value.ToString());

                dragAndDropListView.EndEdit();
            }



        }
    }
}
