namespace SpiderDocs
{
    partial class frmReportUserLog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.cboUser = new System.Windows.Forms.ComboBox();
			this.userBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
			this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSearch = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.img = new System.Windows.Forms.DataGridViewImageColumn();
			this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.eventDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.iddocDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.versionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.obsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.frmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.viewuserlogBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.ckDocEvent = new System.Windows.Forms.CheckBox();
			this.ckSystemEvent = new System.Windows.Forms.CheckBox();
			this.lblNum = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.userBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.viewuserlogBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// cboUser
			// 
			this.cboUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboUser.DataSource = this.userBindingSource;
			this.cboUser.DisplayMember = "name";
			this.cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cboUser.FormattingEnabled = true;
			this.cboUser.Location = new System.Drawing.Point(7, 37);
			this.cboUser.Name = "cboUser";
			this.cboUser.Size = new System.Drawing.Size(173, 21);
			this.cboUser.TabIndex = 1;
			this.cboUser.ValueMember = "id";
			// 
			// dateTimeStart
			// 
			this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimeStart.Location = new System.Drawing.Point(193, 38);
			this.dateTimeStart.Name = "dateTimeStart";
			this.dateTimeStart.Size = new System.Drawing.Size(98, 20);
			this.dateTimeStart.TabIndex = 3;
			// 
			// dateTimeEnd
			// 
			this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimeEnd.Location = new System.Drawing.Point(300, 38);
			this.dateTimeEnd.Name = "dateTimeEnd";
			this.dateTimeEnd.Size = new System.Drawing.Size(98, 20);
			this.dateTimeEnd.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Location = new System.Drawing.Point(4, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 15);
			this.label1.TabIndex = 76;
			this.label1.Text = "User:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Location = new System.Drawing.Point(190, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(36, 15);
			this.label2.TabIndex = 77;
			this.label2.Text = "Date:";
			// 
			// btnSearch
			// 
			this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSearch.Image = global::SpiderDocs.Properties.Resources.search_b_icon;
			this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSearch.Location = new System.Drawing.Point(566, 9);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(57, 53);
			this.btnSearch.TabIndex = 85;
			this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSearch.UseCompatibleTextRendering = true;
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.img,
            this.nameDataGridViewTextBoxColumn,
            this.eventDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.iddocDataGridViewTextBoxColumn,
            this.versionDataGridViewTextBoxColumn,
            this.obsDataGridViewTextBoxColumn,
            this.frmDataGridViewTextBoxColumn});
			this.dataGridView1.DataSource = this.viewuserlogBindingSource;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gainsboro;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView1.Location = new System.Drawing.Point(2, 71);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(713, 275);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
			// 
			// img
			// 
			this.img.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.img.HeaderText = "";
			this.img.MinimumWidth = 20;
			this.img.Name = "img";
			this.img.ReadOnly = true;
			this.img.Width = 20;
			// 
			// nameDataGridViewTextBoxColumn
			// 
			this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
			this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
			this.nameDataGridViewTextBoxColumn.MinimumWidth = 100;
			this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
			this.nameDataGridViewTextBoxColumn.ReadOnly = true;
			this.nameDataGridViewTextBoxColumn.Width = 200;
			// 
			// eventDataGridViewTextBoxColumn
			// 
			this.eventDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.eventDataGridViewTextBoxColumn.DataPropertyName = "event";
			this.eventDataGridViewTextBoxColumn.HeaderText = "Event";
			this.eventDataGridViewTextBoxColumn.MinimumWidth = 100;
			this.eventDataGridViewTextBoxColumn.Name = "eventDataGridViewTextBoxColumn";
			this.eventDataGridViewTextBoxColumn.ReadOnly = true;
			this.eventDataGridViewTextBoxColumn.Width = 120;
			// 
			// dateDataGridViewTextBoxColumn
			// 
			this.dateDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dateDataGridViewTextBoxColumn.DataPropertyName = "date";
			this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
			this.dateDataGridViewTextBoxColumn.MinimumWidth = 130;
			this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
			this.dateDataGridViewTextBoxColumn.ReadOnly = true;
			this.dateDataGridViewTextBoxColumn.Width = 130;
			// 
			// iddocDataGridViewTextBoxColumn
			// 
			this.iddocDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.iddocDataGridViewTextBoxColumn.DataPropertyName = "id_doc";
			this.iddocDataGridViewTextBoxColumn.HeaderText = "Id Doc";
			this.iddocDataGridViewTextBoxColumn.MinimumWidth = 60;
			this.iddocDataGridViewTextBoxColumn.Name = "iddocDataGridViewTextBoxColumn";
			this.iddocDataGridViewTextBoxColumn.ReadOnly = true;
			this.iddocDataGridViewTextBoxColumn.Width = 60;
			// 
			// versionDataGridViewTextBoxColumn
			// 
			this.versionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.versionDataGridViewTextBoxColumn.DataPropertyName = "version";
			this.versionDataGridViewTextBoxColumn.HeaderText = "Version";
			this.versionDataGridViewTextBoxColumn.MinimumWidth = 60;
			this.versionDataGridViewTextBoxColumn.Name = "versionDataGridViewTextBoxColumn";
			this.versionDataGridViewTextBoxColumn.ReadOnly = true;
			this.versionDataGridViewTextBoxColumn.Width = 60;
			// 
			// obsDataGridViewTextBoxColumn
			// 
			this.obsDataGridViewTextBoxColumn.DataPropertyName = "obs";
			this.obsDataGridViewTextBoxColumn.HeaderText = "Comments";
			this.obsDataGridViewTextBoxColumn.MinimumWidth = 100;
			this.obsDataGridViewTextBoxColumn.Name = "obsDataGridViewTextBoxColumn";
			this.obsDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// frmDataGridViewTextBoxColumn
			// 
			this.frmDataGridViewTextBoxColumn.DataPropertyName = "frm";
			this.frmDataGridViewTextBoxColumn.HeaderText = "frm";
			this.frmDataGridViewTextBoxColumn.Name = "frmDataGridViewTextBoxColumn";
			this.frmDataGridViewTextBoxColumn.ReadOnly = true;
			this.frmDataGridViewTextBoxColumn.Visible = false;
			// 
			// pictureBox3
			// 
			this.pictureBox3.Image = global::SpiderDocs.Properties.Resources.editing;
			this.pictureBox3.Location = new System.Drawing.Point(418, 38);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(20, 20);
			this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox3.TabIndex = 89;
			this.pictureBox3.TabStop = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::SpiderDocs.Properties.Resources.icon_workspace;
			this.pictureBox2.Location = new System.Drawing.Point(418, 14);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(20, 20);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox2.TabIndex = 88;
			this.pictureBox2.TabStop = false;
			// 
			// ckDocEvent
			// 
			this.ckDocEvent.AutoSize = true;
			this.ckDocEvent.Checked = true;
			this.ckDocEvent.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckDocEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ckDocEvent.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ckDocEvent.Location = new System.Drawing.Point(444, 40);
			this.ckDocEvent.Name = "ckDocEvent";
			this.ckDocEvent.Size = new System.Drawing.Size(110, 17);
			this.ckDocEvent.TabIndex = 87;
			this.ckDocEvent.Text = "Document events";
			this.ckDocEvent.UseVisualStyleBackColor = true;
			// 
			// ckSystemEvent
			// 
			this.ckSystemEvent.AutoSize = true;
			this.ckSystemEvent.Checked = true;
			this.ckSystemEvent.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckSystemEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ckSystemEvent.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ckSystemEvent.Location = new System.Drawing.Point(444, 17);
			this.ckSystemEvent.Name = "ckSystemEvent";
			this.ckSystemEvent.Size = new System.Drawing.Size(95, 17);
			this.ckSystemEvent.TabIndex = 86;
			this.ckSystemEvent.Text = "System events";
			this.ckSystemEvent.UseVisualStyleBackColor = true;
			// 
			// lblNum
			// 
			this.lblNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNum.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblNum.Location = new System.Drawing.Point(0, 347);
			this.lblNum.Name = "lblNum";
			this.lblNum.Size = new System.Drawing.Size(158, 15);
			this.lblNum.TabIndex = 90;
			this.lblNum.Text = "lblNum";
			// 
			// frmReportUserLog
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(722, 373);
			this.Controls.Add(this.cboUser);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblNum);
			this.Controls.Add(this.dateTimeEnd);
			this.Controls.Add(this.pictureBox3);
			this.Controls.Add(this.dateTimeStart);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.ckDocEvent);
			this.Controls.Add(this.ckSystemEvent);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.dataGridView1);
			this.ForeColor = System.Drawing.Color.White;
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(730, 400);
			this.Name = "frmReportUserLog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Logs";
			this.Load += new System.EventHandler(this.frmReportUserLog_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReportUserLog_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.userBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.viewuserlogBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboUser;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.BindingSource userBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox ckDocEvent;
		private System.Windows.Forms.CheckBox ckSystemEvent;
		private System.Windows.Forms.BindingSource viewuserlogBindingSource;
        private System.Windows.Forms.Label lblNum;
		private System.Windows.Forms.DataGridViewImageColumn img;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn eventDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn iddocDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn obsDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn frmDataGridViewTextBoxColumn;
    }
}