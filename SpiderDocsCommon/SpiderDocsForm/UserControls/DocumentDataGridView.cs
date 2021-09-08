// This code is messy and hard to maintain.
// All grid view controls to show document should use this control instead of having their own individual controls as
// it making this code too complicated.
// This user control should have columns and can be shared. Now each grid views in forms have columns but it is redandancy.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SpiderDocsModule;
using Spider.Types;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
	public enum en_DocumentDataGridViewMode
	{
		Database = 0,
		Local,
		Version,
		NewVersion,
		DocumentSearchForExternalFiles,

		Max
	}

//---------------------------------------------------------------------------------
	public class DocumentDataGridView : DocumentDataGridView<Document>
	{
	}

//---------------------------------------------------------------------------------
	public class DocumentDataGridView<Document> : DataGridView where Document : SpiderDocsForms.Document, new()
	{
		public en_DocumentDataGridViewMode Mode { get; set; }
		bool OriginalMultiSelect;

		bool _DraggingFromGrid = false;
		bool _columnIsClicked = false;
		public bool DraggingFromGrid { get { return _DraggingFromGrid; } }
		string[] fields	{ get { return (string[])tb_ColumnName[(int)Mode]; } }
		Point DraggingStartPoint = new Point();
        
        new public object DataSource
		{
			get { return base.DataSource; }
			set
			{
				this.SuspendLayout();
				base.DataSource = value;
				this.ResumeLayout();
			}
		}

		// fix it to DataGridViewColumnHeadersHeightSizeMode.AutoSize in here as someone changes this properly implicitly
		public new DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode
		{
			get { return base.ColumnHeadersHeightSizeMode; }
			set { base.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize; }
		}

//---------------------------------------------------------------------------------
		readonly static string[][] tb_ColumnName = new string[(int)en_DocumentDataGridViewMode.Max][]
		{
			new string[]
			{
				"c_id_doc",				// doc.id
				"c_id_version",			// doc.id_version
				"c_id_user",			// doc.id_user
				"c_id_folder",			// doc.id_folder
				"c_id_type",			// doc.id_docType
				"c_id_status",			// doc.id_status
				"c_mail_in_out_prefix",	//
				"c_title",				// doc.title
				"c_mail_subject",		//
				"c_mail_from",			//
				"c_mail_to",			//
				"c_mail_time",			//
				"c_mail_is_composed",	//
				"c_folder",				// doc.name_folder
				"c_type_desc",			// doc.name_docType
				"c_author",				// doc.author
				"c_version",			// doc.version
				"",						// doc.date
                //"c_date",				// doc.date
				"c_extension",			// doc.extension
				"c_id_review",			// doc.id_review
				"c_id_sp_status",		// doc.id_sp_status
				"",						// doc.path
				"",						// doc.id_event
				"",						// doc.reason
				"c_id_checkout_user",	// doc.id_checkout_user
				"c_size",				// doc.size
                "id_notification_group"	// doc.id_notification_group
			},
			new string[]
			{
				"dtgLocalFile_Id",		// doc.id
				"",						// doc.id_version
				"",						// doc.id_user
				"",						// doc.id_folder
				"",						// doc.id_docType
				"",						// doc.id_status
				"",						//
				"dtgLocalFile_Title",	// doc.title
				"dtgLocalFile_mail_subject",	//
				"dtgLocalFile_mail_from",		//
				"dtgLocalFile_mail_to",			//
				"dtgLocalFile_mail_time",		//
				"dtgLocalFile_mail_is_composed",//
				"",						// doc.name_folder
				"",						// doc.name_docType
				"",						// doc.author
				"dtgLocalFile_Numver",	// doc.version
				"dtgLocalFile_Date",	// doc.date
				"dtgLocalFile_Ext",		// doc.extension
				"",						// doc.id_review
				"",						// doc.id_sp_status
				"dtgLocalFile_Path",	// doc.path
				"",						// doc.id_event
				"",						// doc.reason
				"",						// doc.id_checkout_user
				"dtgLocalFile_Size",	// doc.size
                ""	                    // doc.id_notification_group
			},
			new string[]
			{
				"iddoc",				// doc.id
				"id",					// doc.id_version
				"c_user",				// doc.id_user
				"",						// doc.id_folder
				"",						// doc.id_docType
				"",						// doc.id_status
				"",						//
				"",						// doc.title
				"",
				"",
				"",
				"",
				"",
				"",						// doc.name_folder
				"",						// doc.name_docType
				"",						// doc.author
				"version",				// doc.version
				"",						// doc.date
				"",						// doc.extension
				"",						// doc.id_review
				"",						// doc.id_sp_status
				"",						// doc.path
				"c_event",				// doc.id_event
				"reason",				// doc.reason
				"",						// doc.id_checkout_user
				"",						// doc.size
                ""	                    // doc.id_notification_group
			},
			new string[]
			{
				"dtgNewVersion_Id",			// doc.id
				"dtgNewVersion_id_version",	// doc.id_version
				"dtgNewVersion_id_user",	// doc.id_user
				"dtgNewVersion_id_folder",	// doc.id_folder
				"dtgNewVersion_id_type",	// doc.id_docType
				"dtgNewVersion_id_status",	// doc.id_status
				"",							//
				"dtgNewVersion_Name",		// doc.title
				"",
				"",
				"",
				"",
				"",
				"dtgNewVersion_Folder",		// doc.name_folder
				"dtgNewVersion_DocType",	// doc.name_docType
				"dtgNewVersion_Author",		// doc.author
				"dtgNewVersion_Version",	// doc.version
				"",							// doc.date
				"dtgNewVersion_extension",	// doc.extension
				"",							// doc.id_review
				"",							// doc.id_sp_status
				"",							// doc.path
				"",							// doc.id_event
				"",							// doc.reason
				"",							// doc.id_checkout_user
				"",							// doc.size
                ""	                        // doc.id_notification_group
			},
			new string[]
			{
				"c_id_doc",				// doc.id
				"c_id_version",			// doc.id_version
				"c_id_user",			// doc.id_user
				"c_id_folder",			// doc.id_folder
				"c_id_type",			// doc.id_docType
				"c_id_status",			// doc.id_status
				"",						//
				"c_title",				// doc.title
				"",
				"",
				"",
				"",
				"",
				"c_folder",				// doc.name_folder
				"c_docType",			// doc.name_docType
				"c_author",				// doc.author
				"c_version",			// doc.version
				"",						// doc.date
				"c_extension",			// doc.extension
				"c_id_review",			// doc.id_review
				"c_id_sp_status",		// doc.id_sp_status
				"",						// doc.path
				"",						// doc.id_event
				"",						// doc.reason
				"c_CheckOutUser",		// doc.id_checkout_user
				"",						// doc.size
                ""	                    // doc.id_notification_group
			}
		};

//---------------------------------------------------------------------------------
		public DocumentDataGridView() : base()
		{
			base.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			OriginalMultiSelect = this.MultiSelect;
            AllowUserToOrderColumns = true;
            AllowUserToResizeColumns = true;
        }

        //---------------------------------------------------------------------------------
        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
		{
			base.OnDataBindingComplete(e);



		}

//---------------------------------------------------------------------------------
		public Document GetSelectedDocument()
		{
			Document ans = null;

			if(0 < this.SelectedRows.Count)
				ans = GetDocument(this.SelectedRows[0].Index);

			return ans;
		}

		public List<Document> GetSelectedDocuments()
		{
			List<Document> ans = new List<Document>();

			foreach(DataGridViewRow row in this.SelectedRows)
				ans.Add(GetDocument(row.Index));

			return ans;
		}

//---------------------------------------------------------------------------------
		public Document GetDocument(int idx)
		{
			return GetDocument(this.Rows[idx]);
		}

		Document GetDocument(DataGridViewRow row)
		{
			Document doc = new Document();

			int i = 0;

			if(!String.IsNullOrEmpty(fields[i])) doc.id = TypeUtilities.ConvertFromObject<int>(row.Cells[fields[i]].Value);	i++;

			if(!String.IsNullOrEmpty(fields[i]))
			{
				doc.id_version = TypeUtilities.ConvertFromObject<int>(row.Cells[fields[i]].Value);
				doc.id_latest_version = doc.id_version;
			}
			i++;

			if(!String.IsNullOrEmpty(fields[i])) doc.id_user = TypeUtilities.ConvertFromObject<int>(row.Cells[fields[i]].Value); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.id_folder = TypeUtilities.ConvertFromObject<int>(row.Cells[fields[i]].Value); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.id_docType = TypeUtilities.ConvertFromObject<int>(row.Cells[fields[i]].Value); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.id_status = (en_file_Status)TypeUtilities.ConvertFromObject<int>(row.Cells[fields[i]].Value); i++;

			i++; // skip this line

			if(!String.IsNullOrEmpty(fields[i])) doc.title = TypeUtilities.ConvertFromObject<string>(row.Cells[fields[i]].Value); i++;

			if(!String.IsNullOrEmpty(fields[i]))
			{
				string val = TypeUtilities.ConvertFromObject<string>(row.Cells[fields[i]].Value);

				if(!String.IsNullOrEmpty(val))
				{
					DocumentAttribute attr = new DocumentAttribute();

					attr.SystemAttributeType = SystemAttributes.MailSubject;
					attr.id_type = en_AttrType.Label;
					attr.atbValue = val;
					doc.Attrs.Add(attr);
				}
			}
			i++;

			if(!String.IsNullOrEmpty(fields[i]))
			{
				string val = TypeUtilities.ConvertFromObject<string>(row.Cells[fields[i]].Value);

				if(!String.IsNullOrEmpty(val))
				{
					DocumentAttribute attr = new DocumentAttribute();

					attr.SystemAttributeType = SystemAttributes.MailFrom;
					attr.id_type = en_AttrType.Label;
					attr.atbValue = val;
					doc.Attrs.Add(attr);
				}
			}
			i++;

			if(!String.IsNullOrEmpty(fields[i]))
			{
				string val = TypeUtilities.ConvertFromObject<string>(row.Cells[fields[i]].Value);

				if(!String.IsNullOrEmpty(val))
				{
					DocumentAttribute attr = new DocumentAttribute();

					attr.SystemAttributeType = SystemAttributes.MailTo;
					attr.id_type = en_AttrType.Label;
					attr.atbValue = val;
					doc.Attrs.Add(attr);
				}
			}
			i++;

			if(!String.IsNullOrEmpty(fields[i]))
			{
				DateTime val = TypeUtilities.ConvertFromObject<DateTime>(row.Cells[fields[i]].Value);

				if((val != null) && (val != new DateTime()))
				{
					DocumentAttribute attr = new DocumentAttribute();

					attr.SystemAttributeType = SystemAttributes.MailTime;
					attr.id_type = en_AttrType.DateTime;
					attr.atbValue = val;
					doc.Attrs.Add(attr);
				}
			}
			i++;


			if(!String.IsNullOrEmpty(fields[i]))
			{
				string val = TypeUtilities.ConvertFromObject<string>(row.Cells[fields[i]].Value);

				if(!String.IsNullOrEmpty(val))
				{
					DocumentAttribute attr = new DocumentAttribute();

					attr.SystemAttributeType = SystemAttributes.MailIsComposed;
					attr.id_type = en_AttrType.ChkBox;

					if(val == "True")
						attr.atbValue = en_AttrCheckState.True;
					else
						attr.atbValue = en_AttrCheckState.False;

					doc.Attrs.Add(attr);
				}
			}
			i++;

			if(!String.IsNullOrEmpty(fields[i])) doc.name_folder = TypeUtilities.ConvertFromObject<string>(row.Cells[fields[i]].Value); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.name_docType = TypeUtilities.ConvertFromObject<string>(row.Cells[fields[i]].Value); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.author = TypeUtilities.ConvertFromObject<string>(row.Cells[fields[i]].Value); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.version = TypeUtilities.ConvertFromObject<int>((row.Cells[fields[i]].Value.ToString().Replace("V ",""))); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.date = TypeUtilities.ConvertFromObject<DateTime>(row.Cells[fields[i]].Value); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.extension = TypeUtilities.ConvertFromObject<string>(row.Cells[fields[i]].Value); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.id_review = TypeUtilities.ConvertFromObject<int>(row.Cells[fields[i]].Value); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.id_sp_status = (en_file_Sp_Status)TypeUtilities.ConvertFromObject<int>(row.Cells[fields[i]].Value); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.path = TypeUtilities.ConvertFromObject<string>(row.Cells[fields[i]].Value); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.id_event = TypeUtilities.ConvertFromObject<int>(row.Cells[fields[i]].Value); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.reason = TypeUtilities.ConvertFromObject<string>(row.Cells[fields[i]].Value); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.id_checkout_user = TypeUtilities.ConvertFromObject<int>(row.Cells[fields[i]].Value); i++;
			if(!String.IsNullOrEmpty(fields[i])) doc.size = TypeUtilities.ConvertFromObject<int>(row.Cells[fields[i]].Value); i++;
            //if(!String.IsNullOrEmpty(fields[i]) && !string.IsNullOrEmpty(row.Cells[fields[i]].Value.ToString())) doc.id_notification_group = TypeUtilities.ConvertFromObject<int>(row.Cells[fields[i]].Value); i++;

            return doc;
		}

//---------------------------------------------------------------------------------
		public void UpdateRow(int idx)
		{
			string[] fields = (string[])tb_ColumnName[(int)Mode];

			if(!String.IsNullOrEmpty(fields[0]))
			{
				int id = TypeUtilities.ConvertFromObject<int>(this.Rows[idx].Cells[fields[0]].Value);

				Document doc = DocumentController<Document>.GetDocument(id);
				doc.author = UserController.GetUser(true, doc.id_user).name;
				SetRow(doc, this.Rows[idx]);
			}
		}

		public void UpdateRow(Document doc, int idx)
		{
			SetRow(doc, this.Rows[idx]);
		}

//---------------------------------------------------------------------------------
		public void InsertRow(Document doc)
		{
			int idx = this.Rows.Add();
			SetRow(doc, this.Rows[idx]);
		}

//---------------------------------------------------------------------------------
		void SetRow(Document doc, DataGridViewRow row)
		{
			string[] fields = (string[])tb_ColumnName.GetValue((int)Mode);

			int i = 0;

			if(!String.IsNullOrEmpty(fields[i]) && (0 < doc.id)) row.Cells[fields[i]].Value = doc.id; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.id_version; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.id_user; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.id_folder; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.id_docType; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.id_status; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = ""; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.title; i++;

			if(!String.IsNullOrEmpty(fields[i]))
			{
				DocumentAttribute attr = doc.Attrs.FirstOrDefault(a => a.SystemAttributeType == SystemAttributes.MailSubject);

				if(attr != null)
					row.Cells[fields[i]].Value = attr.atbValue;
			}
			i++;

			if(!String.IsNullOrEmpty(fields[i]))
			{
				DocumentAttribute attr = doc.Attrs.FirstOrDefault(a => a.SystemAttributeType == SystemAttributes.MailFrom);

				if(attr != null)
					row.Cells[fields[i]].Value = attr.atbValue;
			}
			i++;

			if(!String.IsNullOrEmpty(fields[i]))
			{
				DocumentAttribute attr = doc.Attrs.FirstOrDefault(a => a.SystemAttributeType == SystemAttributes.MailTo);

				if(attr != null)
					row.Cells[fields[i]].Value = attr.atbValue;
			}
			i++;

			if(!String.IsNullOrEmpty(fields[i]))
			{
				DocumentAttribute attr = doc.Attrs.FirstOrDefault(a => a.SystemAttributeType == SystemAttributes.MailTime);

				if(attr != null)
					row.Cells[fields[i]].Value = attr.atbValue;
			}
			i++;

			if(!String.IsNullOrEmpty(fields[i]))
			{
				DocumentAttribute attr = doc.Attrs.FirstOrDefault(a => a.SystemAttributeType == SystemAttributes.MailIsComposed);

				if(attr != null)
					row.Cells[fields[i]].Value = attr.atbValue;
			}
			i++;

			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.name_folder; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.name_docType; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.author; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.version; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.date; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.extension; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.id_review; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.id_sp_status; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.path; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.id_event; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.reason; i++;
			if(!String.IsNullOrEmpty(fields[i])) row.Cells[fields[i]].Value = doc.id_checkout_user; i++;

			if(!String.IsNullOrEmpty(fields[i]) && (row.Cells[fields[i]].ValueType == typeof(string)))
			{
				row.Cells[fields[i]].Value = doc.size;

				long len = doc.size / 1024;

				if(len < 1)
					row.Cells[fields[i]].Value = "< 1 KB";
				else
					row.Cells[fields[i]].Value = len.ToString() + " KB";
			}
			i++;

			row.DefaultCellStyle.BackColor = Color.White;
		}

//---------------------------------------------------------------------------------
		protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex > -1)
			{
                _columnIsClicked = false;
                bool bInsideSelected = false;

				if(e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					foreach (DataGridViewRow row in this.SelectedRows)
					{
						if (row.Index == e.RowIndex)
						{
							bInsideSelected = true;
							break;
						}
					}
				}

				if (!bInsideSelected)
				{
					base.OnCellMouseDown(e);
				}
				
			}
			else
			{
				_columnIsClicked = true;
				base.OnCellMouseDown(e);
			}
		}

        //---------------------------------------------------------------------------------
        //protected override void OnCellMouseUp(DataGridViewCellMouseEventArgs e)
        //{
        //          //if(!OriginalMultiSelect)
        //          //{ 
        //          //    if (e.Button != System.Windows.Forms.MouseButtons.Right && _columnIsClicked == false)
        //          //    {
        //          //        if ((Control.ModifierKeys & (Keys.Control | Keys.Shift)) == 0)
        //          //        {
        //          //            if (e.RowIndex > -1)
        //          //            {
        //          //                this.ClearSelection();
        //          //                this.Rows[e.RowIndex].Selected = true;
        //          //            }
        //          //        }
        //          //    }
        //          //}
        //          //base.OnCellMouseUp(e);
        //}

        //---------------------------------------------------------------------------------
        bool OnSelectionChangedProcessing = false;
		protected override void OnSelectionChanged(EventArgs e)
		{
            if (!OnSelectionChangedProcessing
			//&& OriginalMultiSelect
			&& this.SelectedRows.Count > 0)
			{
				OnSelectionChangedProcessing = true;
				if ((Control.ModifierKeys & (Keys.Control | Keys.Shift)) == 0)
				{
					int idx = this.SelectedRows[this.SelectedRows.Count - 1].Index;
					this.ClearSelection();
					this.Rows[idx].Selected = true;

                    base.OnSelectionChanged(e);
                }

				OnSelectionChangedProcessing = false;
			}
		}

//---------------------------------------------------------------------------------
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				_DraggingFromGrid = true;
				DraggingStartPoint = new System.Drawing.Point(e.X, e.Y);

			}
			else
			{
				_DraggingFromGrid = false;
			}
		}

//---------------------------------------------------------------------------------
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if(DraggingFromGrid)
				_DraggingFromGrid = false;
		}

//---------------------------------------------------------------------------------
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if(_DraggingFromGrid)
			{
				if((Math.Abs(e.X - DraggingStartPoint.X) > (SystemInformation.DragSize.Width / 2))
				|| (Math.Abs(e.Y - DraggingStartPoint.Y) > (SystemInformation.DragSize.Height / 2)))
				{
					if (this.Mode == en_DocumentDataGridViewMode.Database && _columnIsClicked == false)
					{
						StartDragging();
					}
					_DraggingFromGrid = false;
				}
			}

		}

//---------------------------------------------------------------------------------
		void StartDragging()
		{

			DocumentDataObject dataObj = new DocumentDataObject(this.GetSelectedDocuments().Cast<SpiderDocsForms.Document>().ToList());
			this.DoDragDrop(dataObj, DragDropEffects.Copy);
		}

//---------------------------------------------------------------------------------
	}
}
