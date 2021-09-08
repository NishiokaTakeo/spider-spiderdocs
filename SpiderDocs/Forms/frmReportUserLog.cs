using System;
using System.Windows.Forms;
using SpiderDocsForms;
using SpiderDocsModule;
using NLog;

namespace SpiderDocs
{
	public partial class frmReportUserLog : Form
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		DTS_User DA_User = new DTS_User(AddBlankOnTop: true);
		DTS_UserLogs DA_UserLogs = new DTS_UserLogs();

		public frmReportUserLog()
		{
			InitializeComponent();

			userBindingSource.DataSource = DA_User.GetDataTable("name");
			viewuserlogBindingSource.DataSource = DA_UserLogs.GetDataTable();
		}

		private void frmReportUserLog_Load(object sender, EventArgs e)
		{
			dateTimeStart.Value = (dateTimeStart.Value - new TimeSpan(30, 00, 00, 00));
			search();
		}

		private void frmReportUserLog_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter) { search(); }
		}

		private void dataGridView1_Sorted(object sender, EventArgs e)
		{
			InsertImageEvent();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			search();
		}

		private void search()
		{
			if(!ckDocEvent.Checked && !ckSystemEvent.Checked)
			{
				clearDataGrid(dataGridView1);
				return;
			}

			string filter = " (date >= '" + dateTimeStart.Value.ToString("yyyy-MM-dd 00:00") + "' and date <= '" + dateTimeEnd.Value.ToString("yyyy-MM-dd 23:59") + "')";
		   
			if(0 < Convert.ToInt32(cboUser.SelectedValue))
				filter = filter + " and id_user = " + Convert.ToInt32(cboUser.SelectedValue);

			if(!ckSystemEvent.Checked)
				filter = filter + " and frm <> 1";

			if(!ckDocEvent.Checked)
				filter = filter + " and frm <> 2";

			DA_UserLogs.Select();

			viewuserlogBindingSource.Filter = filter;
			lblNum.Text = "Logs: " + dataGridView1.RowCount;

			InsertImageEvent();
		}

		private void clearDataGrid(DataGridView dtg)
		{
			try
			{
				int rows = dtg.Rows.Count;

				for(int i = 0; i < rows; i++)
					dtg.Rows.Remove(dtg.Rows[0]);
			}
			catch(Exception error)
			{
				logger.Error(error);
			}
		}

		private void InsertImageEvent()
		{
			foreach(DataGridViewRow rows in dataGridView1.Rows)
			{
				if(Convert.ToInt32(rows.Cells[7].Value) == 1)
				{
					rows.Cells[0].Value = Properties.Resources.icon_workspace;
					rows.Cells[0].ToolTipText = "system events";

				}else
				{
					rows.Cells[0].Value = Properties.Resources.editing;
					rows.Cells[0].ToolTipText = "Document events";
				}

				if((rows.Cells[4].Value.ToString() != "") && (Convert.ToInt32(rows.Cells[4].Value) == 0))
					rows.Cells[4].Value = "";

				if((rows.Cells[5].Value.ToString() != "") && (Convert.ToInt32(rows.Cells[5].Value) == 0))
				rows.Cells[5].Value = "";
			}
		}
   }
}
