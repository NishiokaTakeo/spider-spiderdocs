using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lib = SpiderDocsModule.Library;
using SpiderDocsModule;
using NLog;

namespace SpiderDocsForms
{
	public partial class frmAttributeComboItem : Spider.Forms.FormBase
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

		public string       nameCombo       = "";   
		public int          id_atb          = 0;
		public int          id_item         = 0;
		public string       lastItem        = "";
		public bool         newItem         = false; 
		DTS_AttributeComboItem table;

		public frmAttributeComboItem()
		{
			InitializeComponent();

			dtgCustomTable.AutoGenerateColumns = false;
			btnAdd.Enabled      = true;
			btnDelete.Enabled   = true;
		}

		private void frmAttributeComboItem_Load(object sender, EventArgs e)
		{
			loadGrid();

			txtItem.Focus();
			lblCustom.Text = nameCombo;
			txtItem.Text  = "";
			id_item = 0;
			btnDelete.Enabled = false;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			id_item = 0;
			txtItem.Text = "";
			btnAdd.Enabled = false;
			txtItem.Focus();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				if(id_item != 0)
				{
					if(DocumentAttributeController.IsComboItemUsed(id_atb, id_item))
					{
						MessageBox.Show(lib.msg_attribute_item_used, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					dtgCustomTable.ClearSelection();
					table.Delete(id_item);
					loadGrid();

					id_item = 0;
					txtItem.Text = "";
					btnDelete.Enabled = false;

					MessageBox.Show(lib.msg_sucess_deleted, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch(Exception error)
			{
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if(txtItem.Text == "")
				{
					txtItem.BackColor = Color.LavenderBlush;
					txtItem.Focus();
					MessageBox.Show(lib.msg_required_item, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				else
					txtItem.BackColor = Color.White;

				if(id_item == 0)
				{
					newItem = true;
					lastItem = txtItem.Text;

					dtgCustomTable.ClearSelection();
					table.Insert(id_atb, lastItem);
					loadGrid();

					id_item             = 0;
					txtItem.Text        = "";
					btnDelete.Enabled   = false;
				}
				else
				{
					newItem = false;
					table.Update(id_item, txtItem.Text);
					dtgCustomTable.Rows[dtgCustomTable.CurrentRow.Index].Cells[2].Value = txtItem.Text;
				}

				btnAdd.Enabled = true;

			}
			catch(Exception error)
			{
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);    
				logger.Error(error);
			}
		}

		private void loadGrid()
		{
			table = new DTS_AttributeComboItem(id_atb, TopBlank: false);
			dtgCustomTable.DataSource = table.GetDataTable();
			lblReg.Text = "Regs: " + dtgCustomTable.Rows.Count;
		}

		private void dtgCustomTable_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				id_item = Convert.ToInt32(dtgCustomTable.Rows[dtgCustomTable.CurrentRow.Index].Cells[0].Value);
				txtItem.Text = Convert.ToString(dtgCustomTable.Rows[dtgCustomTable.CurrentRow.Index].Cells[2].Value);
				newItem = false;
				btnDelete.Enabled = true;
				btnAdd.Enabled = true;
			}
			catch(Exception)
			{ }
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		public string getLastRecord()
		{
			return lastItem;
		}
	}
}
