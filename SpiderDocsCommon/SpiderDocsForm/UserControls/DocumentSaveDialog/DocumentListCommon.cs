using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SpiderDocsModule;
using Spider.Drawing;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
//---------------------------------------------------------------------------------
	public class NewListBase : Spider.Forms.UserControlBase
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

		DataGridView _dtgFiles;
		protected DataGridView dtgFiles
		{
			get { return _dtgFiles; }
			set
			{
				value.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
				_dtgFiles = value;
			} 
		}
		
		protected IconManager icons = new IconManager();
		public List<Document> DocumentList = new List<Document>();

//---------------------------------------------------------------------------------
		protected void loadIcons(DataGridViewRowCollection rows, string idx)
		{
			try
			{
				foreach(DataGridViewRow row in rows)
				{
					string ext;

					if(row.Tag != null)
						ext = DocumentList[(int)row.Tag].extension;
					else
						ext = (string)row.Cells["c_extension"].Value;
					
					row.Cells[idx].Value = icons.GetSmallIcon(ext);
					row.Cells[idx].ToolTipText = ext;
				}
			}
			catch(Exception error)
			{
				logger.Error(error);
			}
		}
	}

//---------------------------------------------------------------------------------
	public class NewList : NewListBase
	{
		protected DatagridViewCheckBoxHeaderCell cbHeadNewList;
		List<int> SelectedTag = new List<int>();
		public bool EndPopulate = false;

//---------------------------------------------------------------------------------
		public enum en_FormMode
		{
			Normal = 0,
			Combine,

			Max
		}

//---------------------------------------------------------------------------------
		public List<Document> getSelectedDocs()
		{
			List<Document> ans = new List<Document>();

			foreach(DataGridViewRow Row in dtgFiles.SelectedRows)
				ans.Add(DocumentList[(int)Row.Tag]); 

			return ans;
		}

//---------------------------------------------------------------------------------
		protected List<Document> getCheckedDocs(string chkcol)
		{
			List<Document> ans = new List<Document>();

			List<DataGridViewRow> Rows = getCheckedRows(chkcol);
			foreach(DataGridViewRow Row in Rows)
				ans.Add(DocumentList[(int)Row.Tag]); 

			return ans;
		}

//---------------------------------------------------------------------------------
		protected List<DataGridViewRow> getCheckedRows(string chkcol)
		{
			List<DataGridViewRow> ans = new List<DataGridViewRow>();
			
			foreach(DataGridViewRow Row in dtgFiles.Rows)
			{
				if((bool)Row.Cells[chkcol].Value == true)
					ans.Add(Row); 
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		protected DataGridViewRow SelectChkedRow(int idx, string chkcol)
		{
			List<DataGridViewRow> rows = getCheckedRows(chkcol);
			
			dtgFiles.ClearSelection();
			rows[idx].Selected = true;

			return rows[idx];
		}

//---------------------------------------------------------------------------------
		protected List<Document> getPrevSelectedDocs()
		{
			List<Document> ans = new List<Document>();

			foreach(int tag in SelectedTag)
				ans.Add(DocumentList[tag]); 

			return ans;
		}

//---------------------------------------------------------------------------------
		public void GetSelectedTag()
		{
			SelectedTag.Clear();

			foreach(DataGridViewRow row in dtgFiles.SelectedRows)
				SelectedTag.Add((int)row.Tag);
		}

//---------------------------------------------------------------------------------
		protected void CellChangeColour(DataGridViewRow ctrl, string chkcol)
		{
			DataGridViewCell cell = ctrl.Cells[chkcol];

			if(cell.Visible)
			{
				cell.Style.BackColor = Color.LavenderBlush;
				cell.Selected = true;
				cell.DataGridView.BeginEdit(false);
			}
		}

//---------------------------------------------------------------------------------
		protected void CellClearColour(DataGridViewRow ctrl)
		{
			foreach(DataGridViewCell cell in ctrl.Cells)
			{
				if(cell.Visible)
					cell.Style.BackColor = Color.White;
			}
		}

//---------------------------------------------------------------------------------
	}
}
