namespace SpiderDocsForms {
	partial class DocumentListInsert {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentListInsert));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this._dtgFiles = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.dtgFiles_Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dtgFiles_Icon = new System.Windows.Forms.DataGridViewImageColumn();
            this.dtgFiles_File = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgFiles_arrow = new System.Windows.Forms.DataGridViewImageColumn();
            this.dtgFiles_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._dtgFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // _dtgFiles
            // 
            this._dtgFiles.AllowUserToAddRows = false;
            this._dtgFiles.AllowUserToDeleteRows = false;
            this._dtgFiles.AllowUserToResizeRows = false;
            this._dtgFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dtgFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dtgFiles.BackgroundColor = System.Drawing.Color.White;
            this._dtgFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this._dtgFiles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dtgFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._dtgFiles.ColumnHeadersHeight = 20;
            this._dtgFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._dtgFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dtgFiles_Select,
            this.dtgFiles_Icon,
            this.dtgFiles_File,
            this.dtgFiles_arrow,
            this.dtgFiles_Name});
            this._dtgFiles.GridColor = System.Drawing.Color.Gainsboro;
            this._dtgFiles.Location = new System.Drawing.Point(3, 16);
            this._dtgFiles.Name = "_dtgFiles";
            this._dtgFiles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dtgFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this._dtgFiles.RowHeadersVisible = false;
            this._dtgFiles.RowHeadersWidth = 20;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            this._dtgFiles.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this._dtgFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dtgFiles.Size = new System.Drawing.Size(426, 326);
            this._dtgFiles.TabIndex = 535;
            this._dtgFiles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgFiles_CellClick);
            this._dtgFiles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgFiles_CellContentClick);
            this._dtgFiles.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgFiles_CellEndEdit);
            this._dtgFiles.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dtgFiles_RowsRemoved);
            this._dtgFiles.SelectionChanged += new System.EventHandler(this.dtgFiles_SelectionChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 515;
            this.label6.Text = "Name:";
            // 
            // dtgFiles_Select
            // 
            this.dtgFiles_Select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgFiles_Select.FillWeight = 39F;
            this.dtgFiles_Select.HeaderText = "";
            this.dtgFiles_Select.Name = "dtgFiles_Select";
            this.dtgFiles_Select.ReadOnly = true;
            this.dtgFiles_Select.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgFiles_Select.Width = 25;
            // 
            // dtgFiles_Icon
            // 
            this.dtgFiles_Icon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dtgFiles_Icon.HeaderText = "";
            this.dtgFiles_Icon.MinimumWidth = 20;
            this.dtgFiles_Icon.Name = "dtgFiles_Icon";
            this.dtgFiles_Icon.ReadOnly = true;
            this.dtgFiles_Icon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgFiles_Icon.Width = 20;
            // 
            // dtgFiles_File
            // 
            this.dtgFiles_File.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgFiles_File.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgFiles_File.FillWeight = 107.2304F;
            this.dtgFiles_File.HeaderText = "Current File Name";
            this.dtgFiles_File.MinimumWidth = 80;
            this.dtgFiles_File.Name = "dtgFiles_File";
            this.dtgFiles_File.ReadOnly = true;
            this.dtgFiles_File.Width = 178;
            // 
            // dtgFiles_arrow
            // 
            this.dtgFiles_arrow.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle3.NullValue")));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.dtgFiles_arrow.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtgFiles_arrow.FillWeight = 30F;
            this.dtgFiles_arrow.HeaderText = " ";
            this.dtgFiles_arrow.Image = global::SpiderDocsForms.Properties.Resources.arrow_r;
            this.dtgFiles_arrow.MinimumWidth = 24;
            this.dtgFiles_arrow.Name = "dtgFiles_arrow";
            this.dtgFiles_arrow.ReadOnly = true;
            this.dtgFiles_arrow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgFiles_arrow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dtgFiles_arrow.Width = 30;
            // 
            // dtgFiles_Name
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.OrangeRed;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Salmon;
            this.dtgFiles_Name.DefaultCellStyle = dataGridViewCellStyle4;
            this.dtgFiles_Name.FillWeight = 107.2304F;
            this.dtgFiles_Name.HeaderText = "New Document Name";
            this.dtgFiles_Name.MinimumWidth = 80;
            this.dtgFiles_Name.Name = "dtgFiles_Name";
            // 
            // DocumentListInsert
            // 
            this.Controls.Add(this._dtgFiles);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DocumentListInsert";
            this.Size = new System.Drawing.Size(432, 345);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ((System.ComponentModel.ISupportInitialize)(this._dtgFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView _dtgFiles;
		public System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dtgFiles_Select;
        private System.Windows.Forms.DataGridViewImageColumn dtgFiles_Icon;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgFiles_File;
        private System.Windows.Forms.DataGridViewImageColumn dtgFiles_arrow;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgFiles_Name;
    }
}
