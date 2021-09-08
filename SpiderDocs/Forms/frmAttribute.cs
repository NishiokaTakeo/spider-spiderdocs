using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Spider.Types;
using SpiderDocsModule;
using SpiderDocsForms;
using lib = SpiderDocsModule.Library;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocs
{
	public partial class frmAttribute : Form
	{
		BindingList<DocumentAttribute> views = new BindingList<DocumentAttribute>();

		bool GridBinding = false;
        static Logger logger = LogManager.GetCurrentClassLogger();

		int SelectedAttrId 
		{
			get
			{
				if((0 < dtgAttribute.SelectedRows.Count)
				&& (dtgAttribute.SelectedRows[0].DataBoundItem != null))
				{
					return ((DocumentAttribute)dtgAttribute.SelectedRows[0].DataBoundItem).id;

				}else
				{
					return -1;
				}
			}
		}
//---------------------------------------------------------------------------------
		public frmAttribute()
		{
			InitializeComponent();
			dtgAttribute.AutoGenerateColumns = false;

			try
			{
				BindingSource AttributeTypes = new BindingSource(TypeUtilities.EnumToDictionary<en_AttrType>(en_AttrType.INVALID, en_AttrType.DatePeriod, en_AttrType.Max), null);

				cboFieldType.ValueMember = "Key";
				cboFieldType.DisplayMember = "Value";				
				cboFieldType.DataSource = AttributeTypes;

				Folder top_item = new Folder();
				top_item.document_folder = "--- Default ---";
				top_item.id = 0;

				List<Folder> folders = FolderController.GetFolders().OrderBy(a => a.document_folder).ToList(); 
				folders.Insert(0, top_item);

				cboFolder.DisplayMember = "document_folder";
				cboFolder.ValueMember = "id";
				cboFolder.DataSource = folders;
				cboFolder.SelectedIndex = 0;

				dataGridViewComboBoxColumn3.ValueMember = "Key";
				dataGridViewComboBoxColumn3.DisplayMember = "Value";
				dataGridViewComboBoxColumn3.DataSource = AttributeTypes;

				PopulateAttributes();

			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default_open_form, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}

//---------------------------------------------------------------------------------
		private void btnAdd_Click(object sender, EventArgs e)
		{
			dtgAttribute.ClearSelection();

			txtAttributeName.Clear();
			txtAttributeName.Focus();
			cboFieldType.SelectedIndex = 0;
			only_numbersCheckBox.Checked = false;
			max_lenghTextBox.Text = "20";
			requiredCheckBox.Checked = false;

			typeField();
		}

//---------------------------------------------------------------------------------
		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if(0 < SelectedAttrId)
				{
					if(validation("delete")
					&& MessageBox.Show("Are you sure you want to delete the current record?", lib.msg_messabox_title, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
					{
						DocumentAttributeController.DeleteAttribute(SelectedAttrId);
						PopulateAttributes();
					}
				}

			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				Cache.Remove(Cache.en_GKeys.DB_GetAttributes);
			}

			typeField();
		}

//---------------------------------------------------------------------------------
		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if(!validation("save"))
					return;

				DocumentAttribute attr;
				
				if(0 < SelectedAttrId)
					attr = views.FirstOrDefault(a => a.id == SelectedAttrId);
				else
					attr = new DocumentAttribute();

				attr.name = txtAttributeName.Text;
				attr.id_type = (en_AttrType)cboFieldType.SelectedValue;
				attr.only_numbers = only_numbersCheckBox.Checked;
				attr.max_lengh = int.Parse(max_lenghTextBox.Text);
				attr.position = 1;

				int SelectedFolderId = (int)cboFolder.SelectedValue;
				DocumentAttributeParams param = attr.GetParamsByFolder(SelectedFolderId, false);

				if(param == null)
				{
					param = new DocumentAttributeParams();
					param.id_folder = SelectedFolderId;
					attr.parameters.Add(param);
				}

				param.required = (ThreeStateBool)((int)requiredCheckBox.CheckState);

				DocumentAttributeController.UpdateOrInsertAttribute(attr);

				Utilities.refreshAttributes();
				PopulateAttributes();
				typeField();

			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				Cache.Remove(Cache.en_GKeys.DB_GetAttributes);
			}
		}

//---------------------------------------------------------------------------------
		private void txtAttributeName_KeyPress(object sender, KeyPressEventArgs e)
		{
			//accept *all number* *all letters* */\?_-
			if((e.KeyChar > 64 && e.KeyChar < 123)
			|| (e.KeyChar > 47 && e.KeyChar < 58)
			|| e.KeyChar == 8 || e.KeyChar == 32 || e.KeyChar == 45
			|| e.KeyChar == 46 || e.KeyChar == 47 || e.KeyChar == 63)
			{
				e.Handled = false;
			}

			// exceptions
			if(e.KeyChar == 94 || e.KeyChar == 96) //^
				e.Handled = true;
		}

//---------------------------------------------------------------------------------
		private void TextBoxesForDigits_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
				e.Handled = true;
		}

//---------------------------------------------------------------------------------
		private void dtgAttribute_SelectionChanged(object sender, EventArgs e)
		{
			if(!GridBinding)
			{
				UpdateSelectedAttr();
				typeField();
			}
		}

//---------------------------------------------------------------------------------
		private void cboFieldType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(!GridBinding)
				typeField();
		}

//---------------------------------------------------------------------------------
		private void cboFolder_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(!GridBinding)
			{
				UpdateGrid();
				typeField();
			}
		}

//---------------------------------------------------------------------------------
		private void btnAddList_Click(object sender, EventArgs e)
		{
			frmAttributeComboItem frm = new frmAttributeComboItem();
			frm.id_atb = SelectedAttrId;
			frm.nameCombo = txtAttributeName.Text;
			frm.ShowDialog();
		}

//-----------------------------------------------------------------
		void PopulateAttributes()
		{
            List<DocumentAttribute> attrs = DocumentAttributeController.GetAttributes(true).OrderBy(x => x.atbValue_str).ToList();

            views = new BindingList<DocumentAttribute>(attrs);
			//views = new BindingList<DocumentAttributeView>(DocumentAttributeController.GetAttributes<DocumentAttributeView>(true));
			dtgAttribute.ClearSelection();
			UpdateGrid();
		}

		void UpdateGrid()
		{
			GridBinding = true;

			// this is a tric to prevent flicking when the grid is updated
			dtgAttribute.VirtualMode = true;

			int selected_idx = 0;

			if(0 < dtgAttribute.SelectedRows.Count)
				selected_idx = dtgAttribute.SelectedRows[0].Index;

			foreach(DocumentAttribute wrk in views)
				wrk.FolderId = Convert.ToInt32(cboFolder.SelectedValue);

			dtgAttribute.DataSource = views;

			if(0 < selected_idx)
				dtgAttribute.Rows[selected_idx].Selected = true;

			UpdateSelectedAttr();
			typeField();

			dtgAttribute.VirtualMode = false;
			GridBinding = false;
		}

//-----------------------------------------------------------------
		void UpdateSelectedAttr()
		{
			if(0 < SelectedAttrId)
			{
				DocumentAttribute view = views.FirstOrDefault(a => a.id == SelectedAttrId);

				txtAttributeName.Text = view.name;
				cboFieldType.SelectedValue = (int)view.id_type;
				only_numbersCheckBox.Checked = view.only_numbers;
				max_lenghTextBox.Text = view.max_lengh.ToString();
				requiredCheckBox.CheckState = (CheckState)view.required;
			}
		}

//---------------------------------------------------------------------------------
		private void typeField()
		{
			plEntries.SuspendLayout();
			this.StripToolButtons.SuspendLayout();

			//----- buttons -----
			lbNewAttribute.Visible = false;
			cboFolder.Enabled = false;
			btnAdd.Enabled = false;
			btnDelete.Enabled = false;
			btnSave.Enabled = false;

			// a row is being selected -> normal mode
			if(0 < SelectedAttrId)
			{
				cboFolder.Enabled = true;
				btnAdd.Enabled = true;
				btnDelete.Enabled = true;
				btnSave.Enabled = true;
			}
			// no selected rows -> adding mode
			else
			{
				lbNewAttribute.Visible = true;

				if(0 < cboFolder.Items.Count)
					cboFolder.SelectedIndex = 0;

				btnDelete.Enabled = true;
				btnSave.Enabled = true;
			}

			//----- entries -----
			txtAttributeName.Enabled = false;
			cboFieldType.Enabled = false;
			btnAddList.Enabled = false;
			only_numbersCheckBox.Enabled = false;
			max_lenghTextBox.Enabled = false;
			requiredCheckBox.Enabled = false;
			requiredCheckBox.ThreeState = false;
			cboFolder.Enabled = false;

			if((en_AttrType)(cboFieldType.SelectedValue) != en_AttrType.Label)
			{
				requiredCheckBox.Enabled = true;

				// if edit mode (not new input)
				if(0 < SelectedAttrId)
				{
					cboFolder.Enabled = true;

					// if folder is selected.
					if(0 < cboFolder.SelectedIndex)
						requiredCheckBox.ThreeState = true;
				}
			}

			// if a folder is not selected
			if(cboFolder.SelectedIndex <= 0)
			{
				txtAttributeName.Enabled = true;

				// the field type can be changed only when adding new item
				if(SelectedAttrId <= 0)
					cboFieldType.Enabled = true;

				switch((en_AttrType)(cboFieldType.SelectedValue))
				{
				case en_AttrType.Text: //textbox
					max_lenghTextBox.Enabled = true;
					only_numbersCheckBox.Enabled = true;
					break;

				case en_AttrType.Combo:
				case en_AttrType.ComboSingleSelect:
					btnAddList.Enabled = true;
					break;
				}
			}

			plEntries.ResumeLayout();
			this.StripToolButtons.ResumeLayout();
		}

//---------------------------------------------------------------------------------
		private bool validation(string action)
		{
			if(action == "save")
			{
				if(txtAttributeName.Text == "")
				{
					txtAttributeName.BackColor = Color.LavenderBlush;
					txtAttributeName.Focus();
					MessageBox.Show(lib.msg_required_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;

				} else
				{
					txtAttributeName.BackColor = Color.White;
				}


				if(cboFieldType.SelectedIndex == -1)
				{
					cboFieldType.BackColor = Color.LavenderBlush;
					cboFieldType.Focus();
					MessageBox.Show(lib.msg_required_type, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;

				} else
				{
					cboFieldType.BackColor = Color.White;
				}

			}

			if((action == "delete") && (0 < SelectedAttrId))
			{
				bool hasFiles = DocumentAttributeController.IsAttributeValueExists(SelectedAttrId);
				if(hasFiles)
				{
					MessageBox.Show(lib.msg_delete_Attribute_Doc, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}

				hasFiles = DocumentAttributeController.IsAttributeValueExists(SelectedAttrId);
				if(hasFiles)
				{
					MessageBox.Show(lib.msg_delete_Attribute_ComboBox, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}

				hasFiles = DocumentAttributeController.IsComboItemChildren(SelectedAttrId);
				if(hasFiles)
				{
					MessageBox.Show(lib.msg_delete_Attribute_ComboBox, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}

				hasFiles = FooterController.IsAttributeUsedByFooter(SelectedAttrId);
				if(hasFiles)
				{
					MessageBox.Show(lib.msg_delete_Attribute_Footter, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}

				if(views.Count == 1)
				{
					MessageBox.Show(lib.msg_delete_All_Items, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
			}

			return true;
		}
//---------------------------------------------------------------------------------
	}
}
