using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SpiderDocsForms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocs
{
	public partial class frmFooterSettings : Form
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

//---------------------------------------------------------------------------------
		enum en_lvFooterSubItem
		{
			Name = 0,
			Type,

			Max
		}

//---------------------------------------------------------------------------------
		int field_id;
		en_FooterType field_type;
		int lvIdx;
		
		bool execRoutine = true;

		DocumentAttribute[] Attributes;
		DTS_Footer DA_Footer = new DTS_Footer();
		DataTable TB_Footer = new DataTable();

//---------------------------------------------------------------------------------
		public frmFooterSettings()
		{
			InitializeComponent();
		}

//---------------------------------------------------------------------------------
		private void frmFooterSettings_Load(object sender, EventArgs e)
		{
			try
			{
				DA_Footer.Select();
				TB_Footer = DA_Footer.GetDataTable();

				Attributes = DocumentAttributeController.GetAttributesCache(true).ToArray();

				populateAttributes();
				populateFooter();

			}
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default_open_form, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}

//---------------------------------------------------------------------------------
		private void btnExcluir_Click(object sender, EventArgs e)
		{
			try
			{
			    if(lvFooter.SelectedItems.Count == 0)
			        return;

				lvFooter.SuspendLayout();
				execRoutine = false;
			    foreach(ListViewItem eachItem in this.lvFooter.SelectedItems)
			    {
					DA_Footer.Delete((int)eachItem.Tag, (en_FooterType)eachItem.SubItems[0].Tag);
			        lvFooter.Items.RemoveAt(eachItem.Index);
			    }
				execRoutine = true;
				lvFooter.ResumeLayout();

				syncFooter();

			}
			catch(Exception error)
			{
			     logger.Error(error);
			}
		}

//---------------------------------------------------------------------------------
		private void ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if(execRoutine)
			{
				lvFooter.AllowReorder = true;

				field_id = (int)e.Item.Tag;
				field_type = (en_FooterType)e.Item.SubItems[0].Tag;

				if(((DragNDrop.DragAndDropListView)sender).Name == "lvFooter")
					lvIdx = e.ItemIndex;
				else
					lvIdx = -1;

				if((((DragNDrop.DragAndDropListView)sender).Name != "lvFooter") && chkDuplicatedItem())
					lvFooter.AllowReorder = false;
			}
		}

//---------------------------------------------------------------------------------
		private void lvFooter_DragDrop(object sender, DragEventArgs e)
		{
			syncFooter();
		}

//---------------------------------------------------------------------------------
		private void pictureBox2_Click(object sender,EventArgs e)
		{
			InsertToFooterList();
		}

//---------------------------------------------------------------------------------
		private void lvAttributes_DoubleClick(object sender,EventArgs e)
		{
			InsertToFooterList();
		}

//---------------------------------------------------------------------------------
		private void populateAttributes()
		{
			lvAttributes.Items.Clear();

			int i = 0;
			foreach(string wrk in DTS_Footer.tb_RegAttr)
			{
				ListViewItem item;
				item = new ListViewItem("");

				item.Text = wrk;	// Name
				item.Tag = i++;		// ID

				item.SubItems.Add(DTS_Footer.tb_FooterType[(int)en_FooterType.RegAttr]);
				item.SubItems[0].Tag = en_FooterType.RegAttr;

				lvAttributes.Items.Add(item);
			}

			foreach(DocumentAttribute wrk in Attributes)
			{
				ListViewItem item;
				item = new ListViewItem("");

				item.Text = wrk.name;			// Name
				item.Tag = wrk.id;	// ID

				item.SubItems.Add(DTS_Footer.tb_FooterType[(int)en_FooterType.Attr]);
				item.SubItems[0].Tag = en_FooterType.Attr;

				lvAttributes.Items.Add(item);
			}
		}

//---------------------------------------------------------------------------------
		private void populateFooter()
		{
			lvFooter.Items.Clear();
			foreach(DataRow wrk in TB_Footer.Rows)
			{
				ListViewItem item;
				item = new ListViewItem("");

				en_FooterType type = (en_FooterType)int.Parse(wrk["field_type"].ToString());
				int id = int.Parse(wrk["field_id"].ToString());

				item.Text = setFooterName(type, id);	// Name
				item.Tag = id;							// ID

				item.SubItems.Add(DTS_Footer.tb_FooterType[(int)type]);	// Type Name
				item.SubItems[0].Tag = (int)type;						// Type ID

				lvFooter.Items.Add(item);
			}
		}

//---------------------------------------------------------------------------------
		private string setFooterName(en_FooterType type, int id)
		{
			string ans = "";

			switch(type)
			{
			case en_FooterType.RegAttr:
				ans = DTS_Footer.tb_RegAttr[id];
				break;

			case en_FooterType.Attr:
				foreach(DocumentAttribute wrk in Attributes)
				{
					if(wrk.id == id)
					{
						ans = wrk.name;
						break;
					}
				}

				break;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		private void syncFooter()
		{
		    try
		    {
				FooterController.DeleteFooter();
				foreach(ListViewItem item in lvFooter.Items)
				{
					Footer wrk = new Footer();
					wrk.field_id = (int)item.Tag;
					wrk.field_type = (en_FooterType)Convert.ToInt32(item.SubItems[0].Tag);
					FooterController.AddFooter(wrk);
				}

				DA_Footer.Select();
		        lvFooter.AllowReorder = false;

		    }
		    catch(Exception error)
		    {
		        logger.Error(error);
		    }
		}

//---------------------------------------------------------------------------------
		private void InsertToFooterList()
		{
			if((0 < lvAttributes.SelectedItems.Count) && !chkDuplicatedItem())
			{
				ListViewItem wrk = (ListViewItem)lvAttributes.SelectedItems[0].Clone();

				if(0 < lvFooter.SelectedItems.Count)
				{
					int idx = lvFooter.SelectedItems[0].Index;

					lvFooter.Items.Insert(idx, wrk);
					lvFooter.Items[idx].Selected = true;

				}else
				{
					lvFooter.Items.Add(wrk);
				}

				syncFooter();
			}
		}

//---------------------------------------------------------------------------------
		bool chkDuplicatedItem()
		{
			//check if user or group is already in the list
			int cnt = lvFooter.Items.Count;
			for(int i = 0; i < cnt; i++)
			{
				ListViewItem eachItem = lvFooter.Items[i];

			    if(
				    ((int)eachItem.Tag == field_id) &&
					((en_FooterType)eachItem.SubItems[0].Tag == field_type)
				  )
			    {
			        return true;
			    }
			}

			return false;
		}

//---------------------------------------------------------------------------------	 
	}
}
