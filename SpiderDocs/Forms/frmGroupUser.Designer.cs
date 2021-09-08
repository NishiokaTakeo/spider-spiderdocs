namespace SpiderDocs
{
    partial class frmGroupUser
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGroupUser));
			this.dtgGroups = new System.Windows.Forms.DataGridView();
			this.groupBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.pbCheckAll = new System.Windows.Forms.PictureBox();
			this.pbUncheckAll = new System.Windows.Forms.PictureBox();
			this.lblGroups = new System.Windows.Forms.Label();
			this.lblUsers = new System.Windows.Forms.Label();
			this.dtgUsers = new System.Windows.Forms.DataGridView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.c_img = new System.Windows.Forms.DataGridViewImageColumn();
			this.c_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.c_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.c_ck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
			this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.groupnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dtgGroups)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbCheckAll)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbUncheckAll)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtgUsers)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dtgGroups
			// 
			this.dtgGroups.AllowUserToAddRows = false;
			this.dtgGroups.AllowUserToDeleteRows = false;
			this.dtgGroups.AllowUserToResizeColumns = false;
			this.dtgGroups.AllowUserToResizeRows = false;
			this.dtgGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.dtgGroups.AutoGenerateColumns = false;
			this.dtgGroups.BackgroundColor = System.Drawing.Color.FloralWhite;
			this.dtgGroups.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dtgGroups.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dtgGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dtgGroups.ColumnHeadersVisible = false;
			this.dtgGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.idDataGridViewTextBoxColumn,
            this.groupnameDataGridViewTextBoxColumn});
			this.dtgGroups.DataSource = this.groupBindingSource;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gainsboro;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dtgGroups.DefaultCellStyle = dataGridViewCellStyle2;
			this.dtgGroups.Location = new System.Drawing.Point(12, 96);
			this.dtgGroups.MultiSelect = false;
			this.dtgGroups.Name = "dtgGroups";
			this.dtgGroups.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dtgGroups.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dtgGroups.RowHeadersVisible = false;
			this.dtgGroups.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Silver;
			this.dtgGroups.RowTemplate.Height = 40;
			this.dtgGroups.RowTemplate.ReadOnly = true;
			this.dtgGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dtgGroups.Size = new System.Drawing.Size(330, 499);
			this.dtgGroups.TabIndex = 12;
			this.dtgGroups.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
			this.dtgGroups.SelectionChanged += new System.EventHandler(this.dtgGroups_SelectionChanged);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "group.png");
			this.imageList1.Images.SetKeyName(1, "Preppy-icon.png");
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::SpiderDocs.Properties.Resources.big_groups;
			this.pictureBox2.Location = new System.Drawing.Point(14, 55);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(40, 40);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 8;
			this.pictureBox2.TabStop = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::SpiderDocs.Properties.Resources.Preppy_icon2;
			this.pictureBox1.Location = new System.Drawing.Point(377, 52);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(35, 40);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			// 
			// pbCheckAll
			// 
			this.pbCheckAll.Image = ((System.Drawing.Image)(resources.GetObject("pbCheckAll.Image")));
			this.pbCheckAll.Location = new System.Drawing.Point(695, 75);
			this.pbCheckAll.Name = "pbCheckAll";
			this.pbCheckAll.Size = new System.Drawing.Size(18, 18);
			this.pbCheckAll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbCheckAll.TabIndex = 13;
			this.pbCheckAll.TabStop = false;
			this.toolTip1.SetToolTip(this.pbCheckAll, "Check all");
			this.pbCheckAll.Click += new System.EventHandler(this.pbCheckAll_Click);
			// 
			// pbUncheckAll
			// 
			this.pbUncheckAll.Image = ((System.Drawing.Image)(resources.GetObject("pbUncheckAll.Image")));
			this.pbUncheckAll.Location = new System.Drawing.Point(675, 75);
			this.pbUncheckAll.Name = "pbUncheckAll";
			this.pbUncheckAll.Size = new System.Drawing.Size(18, 18);
			this.pbUncheckAll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbUncheckAll.TabIndex = 48;
			this.pbUncheckAll.TabStop = false;
			this.toolTip1.SetToolTip(this.pbUncheckAll, "Uncheck all");
			this.pbUncheckAll.Click += new System.EventHandler(this.pbUncheckAll_Click);
			// 
			// lblGroups
			// 
			this.lblGroups.AutoSize = true;
			this.lblGroups.Location = new System.Drawing.Point(56, 80);
			this.lblGroups.Name = "lblGroups";
			this.lblGroups.Size = new System.Drawing.Size(106, 13);
			this.lblGroups.TabIndex = 36;
			this.lblGroups.Text = "Groups\\Departments";
			// 
			// lblUsers
			// 
			this.lblUsers.AutoSize = true;
			this.lblUsers.Location = new System.Drawing.Point(416, 79);
			this.lblUsers.Name = "lblUsers";
			this.lblUsers.Size = new System.Drawing.Size(34, 13);
			this.lblUsers.TabIndex = 38;
			this.lblUsers.Text = "Users";
			// 
			// dtgUsers
			// 
			this.dtgUsers.AllowUserToAddRows = false;
			this.dtgUsers.AllowUserToDeleteRows = false;
			this.dtgUsers.AllowUserToResizeColumns = false;
			this.dtgUsers.AllowUserToResizeRows = false;
			this.dtgUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.dtgUsers.BackgroundColor = System.Drawing.Color.FloralWhite;
			this.dtgUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dtgUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dtgUsers.ColumnHeadersVisible = false;
			this.dtgUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_img,
            this.c_id,
            this.c_name,
            this.c_ck});
			this.dtgUsers.Location = new System.Drawing.Point(377, 95);
			this.dtgUsers.MultiSelect = false;
			this.dtgUsers.Name = "dtgUsers";
			this.dtgUsers.RowHeadersVisible = false;
			this.dtgUsers.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Silver;
			this.dtgUsers.RowTemplate.Height = 28;
			this.dtgUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dtgUsers.Size = new System.Drawing.Size(338, 500);
			this.dtgUsers.TabIndex = 44;
			this.dtgUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgPermission_CellContentClick);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label7);
			this.panel1.Location = new System.Drawing.Point(-13, -11);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(752, 47);
			this.panel1.TabIndex = 45;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label7.Location = new System.Drawing.Point(26, 22);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(189, 15);
			this.label7.TabIndex = 26;
			this.label7.Text = "Choose the users for each Group.";
			// 
			// dataGridViewImageColumn1
			// 
			this.dataGridViewImageColumn1.HeaderText = "";
			this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
			this.dataGridViewImageColumn1.Width = 35;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
			this.dataGridViewTextBoxColumn1.HeaderText = "id";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.Visible = false;
			// 
			// dataGridViewImageColumn2
			// 
			this.dataGridViewImageColumn2.HeaderText = "";
			this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
			this.dataGridViewImageColumn2.ReadOnly = true;
			this.dataGridViewImageColumn2.Width = 35;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "id";
			this.dataGridViewTextBoxColumn2.HeaderText = "Id";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Visible = false;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn3.DataPropertyName = "name";
			this.dataGridViewTextBoxColumn3.HeaderText = "Name";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			// 
			// dataGridViewCheckBoxColumn1
			// 
			this.dataGridViewCheckBoxColumn1.DataPropertyName = "ck";
			this.dataGridViewCheckBoxColumn1.HeaderText = "";
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.Width = 20;
			// 
			// c_img
			// 
			this.c_img.HeaderText = "";
			this.c_img.Name = "c_img";
			this.c_img.ReadOnly = true;
			this.c_img.Width = 35;
			// 
			// c_id
			// 
			this.c_id.DataPropertyName = "id";
			this.c_id.HeaderText = "Id";
			this.c_id.Name = "c_id";
			this.c_id.ReadOnly = true;
			this.c_id.Visible = false;
			// 
			// c_name
			// 
			this.c_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.c_name.DataPropertyName = "name";
			this.c_name.HeaderText = "Name";
			this.c_name.Name = "c_name";
			this.c_name.ReadOnly = true;
			// 
			// c_ck
			// 
			this.c_ck.DataPropertyName = "ck";
			this.c_ck.HeaderText = "";
			this.c_ck.Name = "c_ck";
			this.c_ck.Width = 20;
			// 
			// Column1
			// 
			this.Column1.HeaderText = "";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.Width = 35;
			// 
			// idDataGridViewTextBoxColumn
			// 
			this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
			this.idDataGridViewTextBoxColumn.HeaderText = "id";
			this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
			this.idDataGridViewTextBoxColumn.ReadOnly = true;
			this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // groupnameDataGridViewTextBoxColumn
            // 
            this.groupnameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.groupnameDataGridViewTextBoxColumn.DataPropertyName = "group_name";
            this.groupnameDataGridViewTextBoxColumn.HeaderText = "group_name";
            this.groupnameDataGridViewTextBoxColumn.Name = "groupnameDataGridViewTextBoxColumn";
            this.groupnameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// frmGroupUser
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(737, 618);
			this.Controls.Add(this.pbUncheckAll);
			this.Controls.Add(this.pbCheckAll);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.dtgUsers);
			this.Controls.Add(this.lblUsers);
			this.Controls.Add(this.lblGroups);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.dtgGroups);
			this.MinimumSize = new System.Drawing.Size(745, 645);
			this.Name = "frmGroupUser";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Groups / Users";
			this.Load += new System.EventHandler(this.frmGroupUser_Load);
			((System.ComponentModel.ISupportInitialize)(this.dtgGroups)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbCheckAll)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbUncheckAll)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtgUsers)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView dtgGroups;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.BindingSource groupBindingSource;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblGroups;
        private System.Windows.Forms.Label lblUsers;
        private System.Windows.Forms.DataGridView dtgUsers;
        private System.Windows.Forms.DataGridViewImageColumn c_img;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_name;
        private System.Windows.Forms.DataGridViewCheckBoxColumn c_ck;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pbCheckAll;
		private System.Windows.Forms.PictureBox pbUncheckAll;
		private System.Windows.Forms.DataGridViewImageColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn groupnameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
    }
}