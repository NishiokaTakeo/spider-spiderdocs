namespace SpiderDocsForms {
	public partial class NewVerList
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
		if(disposing && (components != null)) {
		components.Dispose();
		}
		base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label8 = new System.Windows.Forms.Label();
            this.dtgNewVersion = new SpiderDocsForms.DocumentDataGridView();
            this.dtgNewVersion_Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dtgNewVersion_Icon = new System.Windows.Forms.DataGridViewImageColumn();
            this.dtgNewVersion_File = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgNewVersion_Arrow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgNewVersion_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgNewVersion_id_version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgNewVersion_id_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgNewVersion_id_folder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgNewVersion_id_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgNewVersion_id_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgNewVersion_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgNewVersion_Folder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgNewVersion_DocType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgNewVersion_Author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgNewVersion_Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgNewVersion_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgNewVersion_extension = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgNewVersion)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(3, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(238, 13);
            this.label8.TabIndex = 540;
            this.label8.Text = "Select the file you are saving the new version";
            // 
            // dtgNewVersion
            // 
            this.dtgNewVersion.AllowDrop = true;
            this.dtgNewVersion.AllowUserToAddRows = false;
            this.dtgNewVersion.AllowUserToDeleteRows = false;
            this.dtgNewVersion.AllowUserToResizeRows = false;
            this.dtgNewVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgNewVersion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgNewVersion.BackgroundColor = System.Drawing.Color.White;
            this.dtgNewVersion.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtgNewVersion.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgNewVersion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgNewVersion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgNewVersion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dtgNewVersion_Select,
            this.dtgNewVersion_Icon,
            this.dtgNewVersion_File,
            this.dtgNewVersion_Arrow,
            this.dtgNewVersion_Id,
            this.dtgNewVersion_id_version,
            this.dtgNewVersion_id_user,
            this.dtgNewVersion_id_folder,
            this.dtgNewVersion_id_type,
            this.dtgNewVersion_id_status,
            this.dtgNewVersion_Name,
            this.dtgNewVersion_Folder,
            this.dtgNewVersion_DocType,
            this.dtgNewVersion_Author,
            this.dtgNewVersion_Version,
            this.dtgNewVersion_Date,
            this.dtgNewVersion_extension});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgNewVersion.DefaultCellStyle = dataGridViewCellStyle5;
            this.dtgNewVersion.GridColor = System.Drawing.Color.Gainsboro;
            this.dtgNewVersion.Location = new System.Drawing.Point(4, 38);
            this.dtgNewVersion.Mode = SpiderDocsForms.en_DocumentDataGridViewMode.NewVersion;
            this.dtgNewVersion.MultiSelect = false;
            this.dtgNewVersion.Name = "dtgNewVersion";
            this.dtgNewVersion.ReadOnly = true;
            this.dtgNewVersion.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgNewVersion.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dtgNewVersion.RowHeadersVisible = false;
            this.dtgNewVersion.RowHeadersWidth = 20;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            this.dtgNewVersion.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dtgNewVersion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgNewVersion.Size = new System.Drawing.Size(452, 356);
            this.dtgNewVersion.TabIndex = 541;
            this.dtgNewVersion.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgNewVersion_CellContentClick);
            this.dtgNewVersion.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dtgNewVersion_RowsRemoved);
            this.dtgNewVersion.DragDrop += new System.Windows.Forms.DragEventHandler(this.dtgNewVersion_DragDrop);
            this.dtgNewVersion.DragOver += new System.Windows.Forms.DragEventHandler(this.dtgNewVersion_DragOver);
            // 
            // dtgNewVersion_Select
            // 
            this.dtgNewVersion_Select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgNewVersion_Select.HeaderText = "";
            this.dtgNewVersion_Select.Name = "dtgNewVersion_Select";
            this.dtgNewVersion_Select.ReadOnly = true;
            this.dtgNewVersion_Select.Width = 25;
            // 
            // dtgNewVersion_Icon
            // 
            this.dtgNewVersion_Icon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgNewVersion_Icon.HeaderText = "";
            this.dtgNewVersion_Icon.MinimumWidth = 20;
            this.dtgNewVersion_Icon.Name = "dtgNewVersion_Icon";
            this.dtgNewVersion_Icon.ReadOnly = true;
            this.dtgNewVersion_Icon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgNewVersion_Icon.Width = 20;
            // 
            // dtgNewVersion_File
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgNewVersion_File.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgNewVersion_File.FillWeight = 40F;
            this.dtgNewVersion_File.HeaderText = "File";
            this.dtgNewVersion_File.MinimumWidth = 2;
            this.dtgNewVersion_File.Name = "dtgNewVersion_File";
            this.dtgNewVersion_File.ReadOnly = true;
            // 
            // dtgNewVersion_Arrow
            // 
            this.dtgNewVersion_Arrow.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgNewVersion_Arrow.FillWeight = 20F;
            this.dtgNewVersion_Arrow.HeaderText = "";
            this.dtgNewVersion_Arrow.Name = "dtgNewVersion_Arrow";
            this.dtgNewVersion_Arrow.ReadOnly = true;
            this.dtgNewVersion_Arrow.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgNewVersion_Arrow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dtgNewVersion_Arrow.Width = 20;
            // 
            // dtgNewVersion_Id
            // 
            this.dtgNewVersion_Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgNewVersion_Id.DataPropertyName = "id";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgNewVersion_Id.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtgNewVersion_Id.FillWeight = 42F;
            this.dtgNewVersion_Id.HeaderText = "Id";
            this.dtgNewVersion_Id.MinimumWidth = 42;
            this.dtgNewVersion_Id.Name = "dtgNewVersion_Id";
            this.dtgNewVersion_Id.ReadOnly = true;
            this.dtgNewVersion_Id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgNewVersion_Id.Width = 42;
            // 
            // dtgNewVersion_id_version
            // 
            this.dtgNewVersion_id_version.DataPropertyName = "id_version";
            this.dtgNewVersion_id_version.HeaderText = "id_version";
            this.dtgNewVersion_id_version.Name = "dtgNewVersion_id_version";
            this.dtgNewVersion_id_version.ReadOnly = true;
            this.dtgNewVersion_id_version.Visible = false;
            // 
            // dtgNewVersion_id_user
            // 
            this.dtgNewVersion_id_user.DataPropertyName = "id_user";
            this.dtgNewVersion_id_user.HeaderText = "id_user";
            this.dtgNewVersion_id_user.Name = "dtgNewVersion_id_user";
            this.dtgNewVersion_id_user.ReadOnly = true;
            this.dtgNewVersion_id_user.Visible = false;
            // 
            // dtgNewVersion_id_folder
            // 
            this.dtgNewVersion_id_folder.DataPropertyName = "id_folder";
            this.dtgNewVersion_id_folder.HeaderText = "id_folder";
            this.dtgNewVersion_id_folder.Name = "dtgNewVersion_id_folder";
            this.dtgNewVersion_id_folder.ReadOnly = true;
            this.dtgNewVersion_id_folder.Visible = false;
            // 
            // dtgNewVersion_id_type
            // 
            this.dtgNewVersion_id_type.DataPropertyName = "id_type";
            this.dtgNewVersion_id_type.HeaderText = "id_type";
            this.dtgNewVersion_id_type.Name = "dtgNewVersion_id_type";
            this.dtgNewVersion_id_type.ReadOnly = true;
            this.dtgNewVersion_id_type.Visible = false;
            // 
            // dtgNewVersion_id_status
            // 
            this.dtgNewVersion_id_status.DataPropertyName = "id_status";
            this.dtgNewVersion_id_status.HeaderText = "id_status";
            this.dtgNewVersion_id_status.Name = "dtgNewVersion_id_status";
            this.dtgNewVersion_id_status.ReadOnly = true;
            this.dtgNewVersion_id_status.Visible = false;
            // 
            // dtgNewVersion_Name
            // 
            this.dtgNewVersion_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dtgNewVersion_Name.DataPropertyName = "title";
            this.dtgNewVersion_Name.FillWeight = 40F;
            this.dtgNewVersion_Name.HeaderText = "Name";
            this.dtgNewVersion_Name.MinimumWidth = 2;
            this.dtgNewVersion_Name.Name = "dtgNewVersion_Name";
            this.dtgNewVersion_Name.ReadOnly = true;
            // 
            // dtgNewVersion_Folder
            // 
            this.dtgNewVersion_Folder.DataPropertyName = "document_folder";
            this.dtgNewVersion_Folder.FillWeight = 20F;
            this.dtgNewVersion_Folder.HeaderText = "Folder";
            this.dtgNewVersion_Folder.MinimumWidth = 2;
            this.dtgNewVersion_Folder.Name = "dtgNewVersion_Folder";
            this.dtgNewVersion_Folder.ReadOnly = true;
            // 
            // dtgNewVersion_DocType
            // 
            this.dtgNewVersion_DocType.DataPropertyName = "type";
            this.dtgNewVersion_DocType.FillWeight = 20F;
            this.dtgNewVersion_DocType.HeaderText = "Doc. Type";
            this.dtgNewVersion_DocType.MinimumWidth = 80;
            this.dtgNewVersion_DocType.Name = "dtgNewVersion_DocType";
            this.dtgNewVersion_DocType.ReadOnly = true;
            this.dtgNewVersion_DocType.Visible = false;
            // 
            // dtgNewVersion_Author
            // 
            this.dtgNewVersion_Author.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgNewVersion_Author.DataPropertyName = "name";
            this.dtgNewVersion_Author.FillWeight = 10F;
            this.dtgNewVersion_Author.HeaderText = "Author";
            this.dtgNewVersion_Author.MinimumWidth = 80;
            this.dtgNewVersion_Author.Name = "dtgNewVersion_Author";
            this.dtgNewVersion_Author.ReadOnly = true;
            this.dtgNewVersion_Author.Visible = false;
            this.dtgNewVersion_Author.Width = 80;
            // 
            // dtgNewVersion_Version
            // 
            this.dtgNewVersion_Version.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgNewVersion_Version.DataPropertyName = "version";
            this.dtgNewVersion_Version.FillWeight = 10F;
            this.dtgNewVersion_Version.HeaderText = "Version";
            this.dtgNewVersion_Version.MinimumWidth = 50;
            this.dtgNewVersion_Version.Name = "dtgNewVersion_Version";
            this.dtgNewVersion_Version.ReadOnly = true;
            this.dtgNewVersion_Version.Visible = false;
            this.dtgNewVersion_Version.Width = 50;
            // 
            // dtgNewVersion_Date
            // 
            this.dtgNewVersion_Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgNewVersion_Date.DataPropertyName = "date";
            dataGridViewCellStyle4.Format = "dd/MM/yyyy";
            dataGridViewCellStyle4.NullValue = null;
            this.dtgNewVersion_Date.DefaultCellStyle = dataGridViewCellStyle4;
            this.dtgNewVersion_Date.FillWeight = 10F;
            this.dtgNewVersion_Date.HeaderText = "Date";
            this.dtgNewVersion_Date.MinimumWidth = 120;
            this.dtgNewVersion_Date.Name = "dtgNewVersion_Date";
            this.dtgNewVersion_Date.ReadOnly = true;
            this.dtgNewVersion_Date.Visible = false;
            this.dtgNewVersion_Date.Width = 120;
            // 
            // dtgNewVersion_extension
            // 
            this.dtgNewVersion_extension.DataPropertyName = "extension";
            this.dtgNewVersion_extension.HeaderText = "extension";
            this.dtgNewVersion_extension.Name = "dtgNewVersion_extension";
            this.dtgNewVersion_extension.ReadOnly = true;
            this.dtgNewVersion_extension.Visible = false;
            // 
            // NewVerList
            // 
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtgNewVersion);
            this.Name = "NewVerList";
            this.Size = new System.Drawing.Size(460, 398);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ((System.ComponentModel.ISupportInitialize)(this.dtgNewVersion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.Label label8;
		public DocumentDataGridView dtgNewVersion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dtgNewVersion_Select;
        private System.Windows.Forms.DataGridViewImageColumn dtgNewVersion_Icon;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_File;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_Arrow;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_id_version;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_id_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_id_folder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_id_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_id_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_Folder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_DocType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_Author;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgNewVersion_extension;
    }
}
