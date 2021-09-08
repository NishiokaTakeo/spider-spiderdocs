using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using SpiderDocsForms;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;
using NLog;
using ReportBuilder.Controllers;
using ReportBuilder.Models.Report;
using ReportBuilder.Models;

namespace SpiderDocs
{
	public partial class frmReportBuilder : Form
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		int /*idGroup, */idEditNewGroup;
        DTS_ViewDocumentGroup dtSearch = new DTS_ViewDocumentGroup();


        int selectedReport_id
        {
            get
			{
				if (lvReportList.SelectedItems.Count == 0) return 0;

				var report = ((ReportBuilder.Models.Report.Reporting_Report)lvReportList.SelectedItems[0].Tag);

				return report.Id;
			}
        }

        public frmReportBuilder()
		{
			InitializeComponent();

            //populate grid groups
            //groupBindingSource.DataSource = DA_Group.GetDataTable();

            //populateDocumentGrid(selectedNGroup_id);
            //this.cmbNotificationGroup.DataSource = NotificationGroupController.GetGroups();
            //foreach (var row in NotificationGroupController.GetGroups())
            //{
            //    this.cmbNotificationGroup.Items.Add(row);

            //}
        }

        ReportController rptORM = new ReportController();

        private void frmReportBuilder_Load(object sender, EventArgs e)
		{
			try
			{
				lblSelectedCategory.Text = string.Empty;


				populateReports();

                populateCategory();
                /*
                //populate grid groups
                //populateUserList();
                populateGroupList();
                populateUserOfGroupList();

                //lblGroups.Text  = "Notification Groups (" + cmbNotificationGroup.Items.Count.ToString() + ")";
				//lblGroups.Refresh();

                cmbNotificationGroup_SelectedIndexChanged(cmbNotificationGroup, new EventArgs());
                //dtgGroups.ReadOnly = false;

                //foreach(DataGridViewRow row in dtgGroups.Rows)
                //{
                //    row.Cells[2].ReadOnly = false;
                //}
                */
            }
			catch(Exception error)
			{
				logger.Error(error);
				MessageBox.Show(lib.msg_error_default_open_form, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}

        private void frmReportBuilder_FormClosed(object sender, FormClosedEventArgs e)
        {
            rptORM.Dispose();
        }



        void populateCategory()
        {
            using (var ctr = new ReportController())
            {
                var categories = ctr.GetFieldsCategory();
                tscmbCategory.ComboBox.DataSource = categories;
                //tscmbCategory.Items.AddRange(categories.ToArray());
                tscmbCategory.ComboBox.DisplayMember = "Display_Name";
                tscmbCategory.ComboBox.ValueMember = "Id";
            }

            //tscmbCategory

        }
        void populateReports()
        {
            lvFields.SelectedItems.Clear();
            lvFilter.SelectedItems.Clear();
            lvReportList.Items.Clear();

            //using (var ctr = new ReportController())
            //{
                var reports = Cache.RptReports();//ctr.GetReports("");

                foreach(var rpt in reports)
                {
                    ListViewItem item = new ListViewItem("");
                    item.SubItems.Add(rpt.Report_Name);
                    item.Tag = rpt;
                    //item.ImageIndex = 1;
                    lvReportList.Items.Add(item);
                }
            //}
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
			//this.txtGroup.Visible = false;

			//if (lvGroup_id == -1 || lvGroup_id == 0 || lvGroup_id == 1 )
   //         {
			//	MessageBox.Show(lib.msg_error_no_select, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

			//	this.lbNewReport.Visible = false;
   //             this.txtGroup.Text = string.Empty;

			//	return;
   //         }


   //         // TODO: check if document is assigned as this notification.

   //         DialogResult result = MessageBox.Show(lib.msg_ask_delete_notification_group, lib.msg_messabox_title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

   //         if (result != DialogResult.Yes) return;

   //         DocumentNotificationGroupController.RemoveNotificationGroup(ids_group: new int[] { lvGroup_id });

   //         NotificationGroupController.DeleteGroup(lvGroup_id);

   //         NotificationGroupController.DeleteUserGroup(lvGroup_id);

   //         DA_Group = new DTS_NotificationGroup();
   //         groupBindingSource.DataSource = DA_Group.GetDataTable();

   //         logger.Info("Notification Group with {0} and associated tables records have been removed.", lvGroup_id);

   //         populateGroupList();

   //         cmbNotificationGroup_SelectedIndexChanged(cmbNotificationGroup, new EventArgs());

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
			try
			{
				if (!validation())
				{
					groupBindingSource.CancelEdit();
					return;
				}

				Validate();
				groupBindingSource.EndEdit();

				//if (0 < idEditNewGroup)
				//    NotificationGroupController.SaveGroup(new NotificationGroup() { id = idEditNewGroup, group_name = this.txtGroup.Text });
				//else
				//    NotificationGroupController.SaveGroup(new NotificationGroup() { group_name = this.txtGroup.Text });

				var categoryId = (tscmbCategory.SelectedItem as Reporting_Category).Id;

				if (0 < idEditNewGroup)
				{
					using (var ctr = new ReportController())
					{
						ctr.UpdateReport(idEditNewGroup, tstxtReports.Text, true, categoryId);
					}
				}
				else
				{
					using (var ctr = new ReportController())
					{
						var id = ctr.AddEmptyReport(SpiderDocsApplication.CurrentUserId);
						ctr.UpdateReport(id, tstxtReports.Text, true, categoryId);
					}
				}

				Cache.Remove(Cache.en_GKeys.DB_RptReports);

				populateReports();

				this.lbNewReport.Visible = tslblReportName.Visible = tstxtReports.Visible = tslblCategory.Visible = tscmbCategory.Visible = false;
			}
			catch (Exception error)
			{
				MessageBox.Show(lib.msg_error_default, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				logger.Error(error);
			}
			finally
			{

			}

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
			this.tstxtReports.Text = string.Empty;

            tslblReportName.Enabled = tstxtReports.Enabled = tslblCategory.Enabled = tscmbCategory.Enabled = true;

            tslblReportName.Visible = tstxtReports.Visible = tslblCategory.Visible = tscmbCategory.Visible = true;

            this.lbNewReport.Visible = true;

            idEditNewGroup = 0;

        }



        //private void lvGroup_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        lvConditions.SelectedItems.Clear();

        //        Group item = (Group)e.Item.Tag;


        //        //lvUsersOfGroup.Items.Clear();
        //        //populateUserOfGroupList(item.id);
        //    }
        //    catch (Exception error)
        //    {
        //        logger.Error(error);
        //    }
        //}


        /// <summary>
        /// Add Group Picture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbAddField_Click(object sender, EventArgs e)
        {

            List<int> ids = new List<int>();
            ListViewItem selected = null;
            if (0 < lvFields.SelectedItems.Count)
            {
                //addGroupPermission(en_NGroup.Group);
                foreach (ListViewItem item in lvFields.SelectedItems)
                {
                    var selectedField = (Reporting_Fields)item.Tag;
                    ids.Add(selectedField.Id);

                    selected = item;
                }

                rptORM.AddReportFields(idEditNewGroup, ids);

                populateReportFields(idEditNewGroup);

                // removed added field from the list
                if (selected != null)
                    lvFields.Items.Remove(selected);
            }

        }

        //---------------------------------------------------------------------------------
        private void pbAddFilter_Click(object sender, EventArgs e)
        {
            List<int> ids = new List<int>();
            ListViewItem selected = null;

            if (0 < lvFilter.SelectedItems.Count)
            {
                //addGroupPermission(en_NGroup.User);

                foreach (ListViewItem item in lvFilter.SelectedItems)
                {
                    var selectedField = (Reporting_Fields)item.Tag;
                    ids.Add(selectedField.Id);

                    //rptORM.DeleteReportFilter(selectedField.Id);
                    rptORM.AddEmptyReportFilter(idEditNewGroup);

                    var filter = rptORM.GetReportFiltersByReportId(idEditNewGroup).OrderByDescending(x => x.Id).First();

                    rptORM.UpdateReportFilter(filter.Id, selectedField.Id, 0, "", "", filter.Filter_Order, filter.Filter_Group, Reporting_Report_Filter.COND[0]);

                    selected = item;

                }

                //rptORM.AddEmptyReportFilter(idEditNewGroup);

            }

            populateReportFilters(idEditNewGroup);

            // removed added field from the list
            if (selected != null)
                lvFilter.Items.Remove(selected);


        }


        private void dgvFields_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

			//var row = dgvFields.Rows[e.RowIndex];
			//int fieldId = 0;
			//foreach (DataGridViewCell c in row.Cells)
			//{
			//	if (int.TryParse(c.Value.ToString(), out fieldId))
			//	{
			//		break;
			//	}
			//}

			var row = this.dgvFields.Rows[e.RowIndex];
			int fieldId = Convert.ToInt32(row.Cells["ID"].Value);

			if (fieldId == 0) return;

			if (e.ColumnIndex == dgvFields.Columns["Delete"].Index)
            {
                //Do something with your button.

                rptORM.DeleteReportField(fieldId);
                //var a = row["Id"];
                //row.Tag

                populateReportFields(idEditNewGroup);

                populateFields();
            }

			else if (e.ColumnIndex == this.dgvFields.Columns["Up"].Index)
			// Up
			{
				rptORM.MoveReportFieldSort(fieldId, -1);

				populateReportFields(idEditNewGroup);

			}

			else if (e.ColumnIndex == this.dgvFields.Columns["Down"].Index)
			// Down
			{
				rptORM.MoveReportFieldSort(fieldId, 1);

				populateReportFields(idEditNewGroup);

			}

		}



        private void dgvFilters_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

			var col = this.dgvFilters.Columns;

			//Do something with your button.
			var row = this.dgvFilters.Rows[e.RowIndex];
			int fieldId = Convert.ToInt32(row.Cells["ID"].Value);

			if (fieldId == 0) return;

			if (e.ColumnIndex == this.dgvFilters.Columns["Delete"].Index)
            {
                rptORM.DeleteReportFilter(fieldId);

				populateReportFilters(idEditNewGroup);

                populateFilters();

            }
			else if ( e.ColumnIndex == this.dgvFilters.Columns["And/Or"].Index || e.ColumnIndex == this.dgvFilters.Columns["Comparator"].Index)
			{
				var editingControl = this.dgvFilters.EditingControl as DataGridViewComboBoxEditingControl;

				if (editingControl != null)
					editingControl.DroppedDown = true;
			}
			//else if (e.ColumnIndex == this.dgvFilters.Columns["Up"].Index)
			//// Up
			//{
			//	//rptORM.MoveReportFieldSort(fieldId, 1);

			//	//populateFilters();

			//}
			//else if (e.ColumnIndex == this.dgvFilters.Columns["Down"].Index)
			//// Down
			//{
			//	//rptORM.MoveReportFieldSort(fieldId, -1);

			//	//populateFilters();

			//}
			//else if (e.ColumnIndex == this.dgvFilters.Columns["Order"].Index)
			//// Sort
			//{


			//}
			//else if (e.ColumnIndex == this.dgvFilters.Columns["Group"].Index)
			//// Group
			//{


			//}
		}

		//void setupReportFieldDataTable()
		//{

		//    System.Data.DataTable dt = new System.Data.DataTable();
		//    dt.Columns.Add("Id");
		//    dt.Columns.Add("Display_Name");

		//    dtgBdFiles.DataSource = dt;

		//    //Delete button
		//    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
		//    btnDelete.Name = "Delete";
		//    btnDelete.Text = "Delete";
		//    btnDelete.UseColumnTextForButtonValue = true;

		//    //int columnIndex = 2;
		//    if (dtgBdFiles.Columns["Delete"] == null)
		//    {
		//        dtgBdFiles.Columns.Add(btnDelete);
		//    }

		//    dtgBdFiles.Columns["Id"].DisplayIndex = 2;
		//    dtgBdFiles.Columns["Id"].Visible = false;
		//    dtgBdFiles.Columns["Display_Name"].DisplayIndex = 1;
		//    dtgBdFiles.Columns["Display_Name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
		//    dtgBdFiles.Columns["Delete"].DisplayIndex = 0;
		//    dtgBdFiles.Columns["Delete"].Width = 100;
		//}

		void populateReportFields(int reportId)
        {

            var reportFields = rptORM.GetReportFieldsByReportId(reportId);

            // System.Data.DataTable dt = new System.Data.DataTable();
            // dt.Columns.Add("Id");
            // dt.Columns.Add("Display_Name");
			// dt.Columns.Add("Up", typeof(byte[]));
			// dt.Columns.Add("Down", typeof(byte[]));
			// dt.Columns.Add("Delete");

			// //dt.Columns.Add("Delete");

			// reportFields.ForEach(x =>
            // {
            //     System.Data.DataRow row = dt.NewRow();

            //     //  row[0] = x.Id;
            //     //  row[1] = x.Display_Name;

            //     dt.Rows.Add(row);

            // });

            dgvFields.DataSource = CreateFieldDataTable(reportFields.Count);

			for (int i = 0; i < reportFields.Count(); i++)
			{
				FillFieldRow(i, reportFields[i]);

				// int cntColumn = 0;
				// var rptField = reportFields[i];
				// //var comparators = rptORM.GetComparators(rptField.Field_Id);
				// //var field = fields.Find(f => f.Id == rptField.Field_Id);
				// //var filter = rptField;
				// DataGridViewTextBoxCell cellId = new DataGridViewTextBoxCell();
				// this.dgvFields[cntColumn, i] = cellId;
				// this.dgvFields[cntColumn, i].Value = rptField.Id;
				// this.dgvFields[cntColumn, i].ReadOnly = true;

				// cntColumn++;

				// DataGridViewTextBoxCell cellDisplayText = new DataGridViewTextBoxCell();
				// this.dgvFields[cntColumn, i] = cellDisplayText;
				// this.dgvFields[cntColumn, i].Value = rptField.Display_Name.Trim();
				// this.dgvFields[cntColumn, i].ReadOnly = true;

				// cntColumn++;

				// // Up
				// var imageConverter = new System.Drawing.ImageConverter();

				// DataGridViewImageCell cellImageUp = new DataGridViewImageCell();
				// this.dgvFields[cntColumn, i] = cellImageUp;
				// this.dgvFields[cntColumn, i].Value = imageConverter.ConvertTo(Properties.Resources.up, typeof(byte[]));


				// cntColumn++;

				// // Down
				// DataGridViewImageCell cellImageDown  = new DataGridViewImageCell();
				// this.dgvFields[cntColumn, i] = cellImageDown;
				// //this.dgvFilters[cntColumn, i].Value = (System.Drawing.Image)Properties.Resources.down;
				// this.dgvFields[cntColumn, i].Value = imageConverter.ConvertTo(Properties.Resources.down, typeof(byte[]));

				// cntColumn++;


				// ////Delete button
				// DataGridViewButtonCell btnDelete = new DataGridViewButtonCell();
				// //btnDelete.Value = "Delete";
				// //btnDelete.Name = "Delete";
				// //btnDelete.Text = "Delete";
				// btnDelete.UseColumnTextForButtonValue = true;
				// this.dgvFields[cntColumn, i] = btnDelete;
				// this.dgvFields[cntColumn, i].Value = "Delete";

			}

			////Delete button
			//DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
			//btnDelete.Name = "Delete";
			//btnDelete.Text = "Delete";
			//btnDelete.UseColumnTextForButtonValue = true;

			//////int columnIndex = 2;
			//if (dgvFields.Columns["Delete"] == null)
			//{
			//	dgvFields.Columns.Add(btnDelete);
			//}
			//colunm orders and others
			//dgvFields.Columns["Id"].DisplayIndex = 0;
			dgvFields.Columns["Id"].Visible = false;
            //dgvFields.Columns["Display_Name"].DisplayIndex = 1;
            dgvFields.Columns["Display_Name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dgvFields.Columns["Up"].Resizable = DataGridViewTriState.False;
			dgvFields.Columns["Up"].Width = 35;
			dgvFields.Columns["Down"].Width = 35;
			dgvFields.Columns["Down"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dgvFields.Columns["Down"].Resizable = DataGridViewTriState.False;
            //dgvFields.Columns["Delete"].DisplayIndex = 2;
            dgvFields.Columns["Delete"].Width = 100;
			dgvFields.Columns["Delete"].HeaderText = "";
        }


		System.Data.DataTable CreateFieldDataTable(int defaultRows = 0)
		{

			//DataGridViewImageColumn enrollIcon = new DataGridViewImageColumn();
			//enrollIcon.Width = 100;
			//enrollIcon.Image = Properties.Resources.up;
    		System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Display_Name");
			dt.Columns.Add("Up", typeof(byte[]));
			dt.Columns.Add("Down", typeof(byte[]));
			dt.Columns.Add("Delete");

			// Default field
			for (int i = 0; i < defaultRows; i++)
			{
				System.Data.DataRow row = dt.NewRow();

				dt.Rows.Add(row);
			}

			return dt;
		}

		void FillFieldRow(int i, Reporting_Report_Fields rptField )
		{
				int cntColumn = 0;
				//var rptField = reportFields[i];
				//var comparators = rptORM.GetComparators(rptField.Field_Id);
				//var field = fields.Find(f => f.Id == rptField.Field_Id);
				//var filter = rptField;
				DataGridViewTextBoxCell cellId = new DataGridViewTextBoxCell();
				this.dgvFields[cntColumn, i] = cellId;
				this.dgvFields[cntColumn, i].Value = rptField.Id;
				this.dgvFields[cntColumn, i].ReadOnly = true;

				cntColumn++;

				DataGridViewTextBoxCell cellDisplayText = new DataGridViewTextBoxCell();
				this.dgvFields[cntColumn, i] = cellDisplayText;
				this.dgvFields[cntColumn, i].Value = rptField.Display_Name.Trim();
				this.dgvFields[cntColumn, i].ReadOnly = true;

				cntColumn++;

				// Up
				var imageConverter = new System.Drawing.ImageConverter();

				DataGridViewImageCell cellImageUp = new DataGridViewImageCell();
				this.dgvFields[cntColumn, i] = cellImageUp;
				this.dgvFields[cntColumn, i].Value = imageConverter.ConvertTo(Properties.Resources.up, typeof(byte[]));


				cntColumn++;

				// Down
				DataGridViewImageCell cellImageDown  = new DataGridViewImageCell();
				this.dgvFields[cntColumn, i] = cellImageDown;
				//this.dgvFilters[cntColumn, i].Value = (System.Drawing.Image)Properties.Resources.down;
				this.dgvFields[cntColumn, i].Value = imageConverter.ConvertTo(Properties.Resources.down, typeof(byte[]));

				cntColumn++;


				////Delete button
				DataGridViewButtonCell btnDelete = new DataGridViewButtonCell();
				//btnDelete.Value = "Delete";
				//btnDelete.Name = "Delete";
				//btnDelete.Text = "Delete";
				btnDelete.UseColumnTextForButtonValue = true;
				this.dgvFields[cntColumn, i] = btnDelete;
				this.dgvFields[cntColumn, i].Value = "Delete";
		}



		/// <summary>
		/// populate custom filters
		/// </summary>
		/// <param name="reportId"></param>
		void populateReportFilters(int reportId)
        {

			try
			{
				this.dgvFilters.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFilters_CellValueChanged);

				var report = Cache.RptReports().Find(r => r.Id == reportId); //rptORM.GetReportById(reportId);
				var reportFields = rptORM.GetReportFiltersByReportId(reportId);
				//var fieldsDropdown = Cache.RptDropDownFields();

				var dt = CreateFilterDataTable( reportFields.Count() );


				this.dgvFilters.DataSource = dt;
				//this.dgvFilters.Columns[0].Width = 200;

				var fields = Cache.RptFieldsByCategory(report.Category_Id); // rptORM.GetFieldsByCategory(report.Category_Id);

				for (int i = 0; i < reportFields.Count(); i++)
				{

					//int cntColumn = 0;
					var rptField = reportFields[i];
					//var comparators = Cache.RptComparators(rptField.Field_Id);
					var field = fields.Find(f => f.Id == rptField.Field_Id);
					//var filter = rptField;

					FillFilterRow(i, field, rptField);
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}
			finally
			{
				this.dgvFilters.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFilters_CellValueChanged);
			}

			//dgvFilters.Columns["Up"].Resizable = DataGridViewTriState.False;
			//dgvFilters.Columns["Up"].Width = 30;
			//dgvFilters.Columns["Down"].Width = 30;
			//dgvFilters.Columns["Down"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			//dgvFilters.Columns["Down"].Resizable = DataGridViewTriState.False;
			dgvFilters.Columns["ID"].Visible = false;
			dgvFilters.Columns["Sort"].Visible = false;
			dgvFilters.Columns["Group"].Visible = false;
			dgvFilters.Columns["Delete"].HeaderText = "";

		}


		System.Data.DataTable CreateFilterDataTable(int defaultRows = 0)
		{

			//DataGridViewImageColumn enrollIcon = new DataGridViewImageColumn();
			//enrollIcon.Width = 100;
			//enrollIcon.Image = Properties.Resources.up;
			System.Data.DataTable dt = new System.Data.DataTable();
			dt.Columns.Add("ID");
			dt.Columns.Add("And/Or");
			dt.Columns.Add("Field");
			dt.Columns.Add("Comparator");
			dt.Columns.Add("Value");
			dt.Columns.Add("Value2");
			dt.Columns.Add("Sort");
			dt.Columns.Add("Group");
			dt.Columns.Add("Delete");

			// Default field
			for (int i = 0; i < defaultRows; i++)
			{
				System.Data.DataRow row = dt.NewRow();

				dt.Rows.Add(row);
			}

			return dt;
		}

		void FillFilterRow(int i, Reporting_Fields field, Reporting_Report_Filter rptField )
		{
			int cntColumn = 0;
			//var rptField = reportFields[i];
			var comparators = Cache.RptFieldComparators(rptField.Field_Id); // rptORM.GetComparators(rptField.Field_Id);
			//var field = fields.Find(f => f.Id == rptField.Field_Id);
			var filter = rptField;
			var fieldsDropdown = Cache.RptDropDownFields();

			//// Up
			//var imageConverter = new System.Drawing.ImageConverter();

			//DataGridViewImageCell cellImageUp = new DataGridViewImageCell();
			//this.dgvFilters[cntColumn, i] = cellImageUp;
			//this.dgvFilters[cntColumn, i].Value = imageConverter.ConvertTo(Properties.Resources.up, typeof(byte[]));


			//cntColumn++;

			//// Down
			//DataGridViewImageCell cellImageDown  = new DataGridViewImageCell();
			//this.dgvFilters[cntColumn, i] = cellImageDown;
			////this.dgvFilters[cntColumn, i].Value = (System.Drawing.Image)Properties.Resources.down;
			//this.dgvFilters[cntColumn, i].Value = imageConverter.ConvertTo(Properties.Resources.down, typeof(byte[]));


			//cntColumn++;

			// ID
			DataGridViewTextBoxCell TextBoxCell0 = new DataGridViewTextBoxCell();
			this.dgvFilters[cntColumn, i] = TextBoxCell0;
			this.dgvFilters[cntColumn, i].Value = rptField.Id;
			this.dgvFilters[cntColumn++, i].ReadOnly = true;

			// And Or
			DataGridViewComboBoxCell ComboBoxCell = new DataGridViewComboBoxCell();
			ComboBoxCell.Items.AddRange(new string[] { "AND", "OR" });
			this.dgvFilters[cntColumn, i] = ComboBoxCell;
			if (i == 0)
			{
				this.dgvFilters[cntColumn, i].Value = "";

				this.dgvFilters[cntColumn, i].ReadOnly = true;
			}
			else this.dgvFilters[cntColumn, i].Value = filter?.Conditional ?? "AND";

			cntColumn++;

			// Field, READ ONLY
			DataGridViewTextBoxCell TextBoxCell = new DataGridViewTextBoxCell();
			this.dgvFilters[cntColumn, i] = TextBoxCell;
			this.dgvFilters[cntColumn, i].Value = field.Display_Name;
			this.dgvFilters[cntColumn, i].ReadOnly = true;

			cntColumn++;

			// Comparator
			DataGridViewComboBoxCell cellDropDown = new DataGridViewComboBoxCell();
			cellDropDown.Items.AddRange(comparators.Select(x => x.Display_Value).ToArray());
			this.dgvFilters[cntColumn, i] = cellDropDown;
			this.dgvFilters[cntColumn, i].Value = comparators.Find(c => c.Id == (filter?.Comparator_Id ?? 0))?.Display_Value ?? comparators.Select(x => x.Display_Value).First();

			cntColumn++;

			// Value depends on field type
			var dropdown = fieldsDropdown.Find(x => x.Field_Id == rptField.Field_Id);
			var comparator = comparators.Find(c => c.Id == rptField.Comparator_Id);
			if (dropdown != null)
			{
				var keyvalue = rptORM.GetReportDropdownFieldsList(dropdown);
				var names = keyvalue.Select(x => x.Item2).ToArray();

				var textDropdown = keyvalue.Find(x => x.Item1 == filter?.Value_1)?.Item2 ?? "";

				DataGridViewComboBoxCell ComboBoxCell3 = new DataGridViewComboBoxCell();
				ComboBoxCell3.Items.AddRange(names);
				this.dgvFilters[cntColumn, i] = ComboBoxCell3;
				this.dgvFilters[cntColumn, i].Value = textDropdown;

			}
			else
			{
				// Text, Number, Bool
				if (new[] { 1, 2, 3, 4 }.ToList().Contains(rptField.Field.Field_Type_Id))
				{
					DataGridViewTextBoxCell TextBoxCell3 = new DataGridViewTextBoxCell();
					this.dgvFilters[cntColumn, i] = TextBoxCell3;
					this.dgvFilters[cntColumn, i].Value = filter.Value_1;
				}
				//else if (new[] { 3 }.ToList().Contains(rptField.Field.Field_Type_Id))
				//{

				//}

			}

			cntColumn++;

			// value2
			DataGridViewTextBoxCell cellVal2 = new DataGridViewTextBoxCell();
			this.dgvFilters[cntColumn, i] = cellVal2;
			this.dgvFilters[cntColumn, i].Value = filter.Value_2;
			this.dgvFilters[cntColumn, i].ReadOnly = !new[] { 7 }.ToList().Contains(rptField.Comparator_Id);

			cntColumn++;

			// Sort
			DataGridViewTextBoxCell cellSort = new DataGridViewTextBoxCell();
			this.dgvFilters[cntColumn, i] = cellSort;
			this.dgvFilters[cntColumn, i].Value = rptField.Filter_Order;
			this.dgvFilters[cntColumn, i].ReadOnly = true;

			cntColumn++;

			// Group
			DataGridViewTextBoxCell cellGroup = new DataGridViewTextBoxCell();
			this.dgvFilters[cntColumn, i] = cellGroup;
			this.dgvFilters[cntColumn, i].Value = rptField.Filter_Group;
			this.dgvFilters[cntColumn, i].ReadOnly = true;

			cntColumn++;

			////Delete button
			DataGridViewButtonCell btnDelete = new DataGridViewButtonCell();
			//btnDelete.Value = "Delete";
			//btnDelete.Name = "Delete";
			//btnDelete.Text = "Delete";
			btnDelete.UseColumnTextForButtonValue = true;
			this.dgvFilters[cntColumn, i] = btnDelete;
			this.dgvFilters[cntColumn, i].Value = "Delete";

			cntColumn++;
		}

        void populateDocumentGrid(int id_ngroup)
        {
            dtSearch.Select(id_ngroup);

            //dtgBdFiles.DataSource = dtSearch.GetDataTable();
        }




        private void btnDelReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedReport_id == 0)
                {
                    MessageBox.Show(lib.msg_error_no_select, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

				if (lvReportList.SelectedItems.Count == 0)
					return;

				var result = (MessageBox.Show("Are you sure you want to delete this group/user?", "Spider Docs", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question));

                if (result == DialogResult.Yes)
                {
					using (var ctr = new ReportController())
					{
						foreach (ListViewItem eachItem in this.lvReportList.SelectedItems)
						{
							int i = 0;
							//delete from base
							ctr.DeleteReport(((ReportBuilder.Models.Report.Reporting_Report)eachItem.Tag).Id);

							lvFields.Items.Clear();
							lvFilter.Items.Clear();

							i = dgvFields.Rows.Count;
							while(--i > -1)
								dgvFields.Rows.RemoveAt(i);

							i = dgvFilters.Rows.Count;
							while (--i > -1)
								dgvFilters.Rows.RemoveAt(i);

							//delete from grid
							lvReportList.Items.RemoveAt(eachItem.Index);
						}
					}


				}
            }
            catch (Exception error)
            {
                logger.Error(error);
            }
            finally
            {
				Cache.Remove(Cache.en_GKeys.DB_RptReports);
			}
        }

        private void lvUsersOfGroup_Click(object sender, EventArgs e)
        {

        }



        private bool validation()
        {
            if (tstxtReports.Text == "")
            {
                tstxtReports.BackColor = System.Drawing.Color.LavenderBlush;
                tstxtReports.Focus();
                MessageBox.Show(lib.msg_required_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;

            }
            else
            {
                tstxtReports.BackColor = System.Drawing.Color.White;
            }

            if (tscmbCategory.SelectedIndex == -1)
            {
                tscmbCategory.BackColor = System.Drawing.Color.LavenderBlush;
                tscmbCategory.Focus();
                MessageBox.Show(lib.msg_required_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;

            }
            else
            {
                tscmbCategory.BackColor = System.Drawing.Color.White;
            }


            return true;
        }

        private void lvReportList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            if (!e.IsSelected)
                return;

            var rpt = (Reporting_Report)e.Item.Tag;

            //var category = rptORM.GetFieldsByCategory(rpt.Id);
			var category = rptORM.GetFieldsCategory().Find(x => x.Id == rpt.Category_Id);
			lblSelectedCategory.Text = $"{category?.Display_Name.Trim()}";

			idEditNewGroup = rpt.Id;

            lvFields.Items.Clear();
            lvFilter.Items.Clear();

            // reload for fileds and filters
            populateFields();

            populateFilters();

            populateReportFields(rpt.Id);

            populateReportFilters(rpt.Id);
        }

        void populateFields()
        {
            this.lvFields.Items.Clear();

            var rpt = Cache.RptReports().Find(r => r.Id == idEditNewGroup); //rptORM.GetReportById(idEditNewGroup);
            var categories = Cache.RptFieldsByCategory(rpt.Category_Id); //rptORM.GetFieldsByCategory(rpt.Category_Id);
            var reportCategories = rptORM.GetReportFieldsByReportId(idEditNewGroup);


            categories = categories.Where(f =>
            {
                return false == reportCategories.Select(x => x.Field_Id).ToList().Contains(f.Id);

            }).ToList();

            foreach (var cate in categories)
            {
                var item = new ListViewItem("");
                item.SubItems.Add( cate.Display_Name );
                item.Tag = cate;
                item.ImageIndex = 0;

                this.lvFields.Items.Add(item);
            }
        }

        void populateFilters()
        {
            this.lvFilter.Items.Clear();

            var rpt = Cache.RptReports().Find(r => r.Id == idEditNewGroup);//rptORM.GetReportById(idEditNewGroup);
            var filters = Cache.RptFieldsByCategory(rpt.Category_Id); //rptORM.GetFieldsByCategory(rpt.Category_Id);
            var reportCategories = rptORM.GetReportFiltersByReportId(idEditNewGroup);

            filters = filters.Where(f =>
            {
                return false == reportCategories.Select(x => x.Field_Id).ToList().Contains(f.Id);

            }).ToList();

            foreach (var filter in filters)
            {
                var item = new ListViewItem("");
                item.SubItems.Add( filter.Display_Name );
                item.Tag = filter;
                item.ImageIndex = 0;

                this.lvFilter.Items.Add(item);
            }
        }


        private void dgvFilters_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            switch(e.ColumnIndex)
            {

                case 1: // And OR
                case 3: // Comparator
                case 4: // Value1
                case 5: // Value1

                    var filters = rptORM.GetReportFiltersByReportId(idEditNewGroup);
                    var idFilter = Convert.ToInt32(this.dgvFilters.Rows[e.RowIndex].Cells[0].Value);

                    var filter = filters.Find(f => f.Id == idFilter);
                    if (filter != null)
                    {
						// Get cells values
                        var andOr = this.dgvFilters.Rows[e.RowIndex].Cells["And/Or"].Value.ToString();
                        var comparatorDisplay = this.dgvFilters.Rows[e.RowIndex].Cells["Comparator"].Value.ToString();
                        var value = this.dgvFilters.Rows[e.RowIndex].Cells["Value"].Value.ToString();
                        var value2 = this.dgvFilters.Rows[e.RowIndex].Cells["Value2"].Value.ToString();
						var sort = Convert.ToInt32(this.dgvFilters.Rows[e.RowIndex].Cells["Sort"].Value);
						var group = Convert.ToInt32(this.dgvFilters.Rows[e.RowIndex].Cells["Group"].Value);


                        var fieldsDropdown = Cache.RptDropDownFields();
                        var dropdown = fieldsDropdown.Find(x => x.Field_Id == filter.Field_Id);
                        if (dropdown != null)
                        {
                            var keyvalue = rptORM.GetReportDropdownFieldsList(dropdown);

                            var idDropdown = keyvalue.Find(x => x.Item2 == this.dgvFilters.Rows[e.RowIndex].Cells["Value"].Value.ToString())?.Item1;
                            value = idDropdown;
                        }

                        var comparator = Cache.RptComparators().Find(x => x.Display_Value == comparatorDisplay);

                        rptORM.UpdateReportFilter(idFilter, filter.Field_Id, comparator?.Id ?? 0, value, value2, sort, group, andOr);

						populateReportFilters(idEditNewGroup);
                    }

                    break;

                case 0: // ID
                    break;
                case 2: // Field
                    break;
                case 6: // Delete
                    break;

            }


        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string pathSelected = "";


            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                pathSelected = folderBrowserDialog.SelectedPath;

                var path = rptORM.CreateDynamicReport(idEditNewGroup, pathSelected);

                MessageBox.Show("CSV report has been placed", lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

		private void dgvFilters_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				var hitTestInfo = dgvFilters.HitTest(e.X, e.Y);
				if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
					dgvFilters.BeginEdit(true);
				else
					dgvFilters.EndEdit();
			}
		}
	}
}
