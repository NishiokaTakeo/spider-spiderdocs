namespace SpiderDocs
{
    partial class frmFolder
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
            System.Windows.Forms.Label document_folderLabel;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFolder));
            this.dtgFolder = new System.Windows.Forms.DataGridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.document_folderBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.txtFolderName = new System.Windows.Forms.TextBox();
            this.document_folderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folder = new System.Windows.Forms.DataGridViewImageColumn();
            this.dtgFolder_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgFolder_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            document_folderLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document_folderBindingNavigator)).BeginInit();
            this.document_folderBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.document_folderBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // document_folderLabel
            // 
            document_folderLabel.AutoSize = true;
            document_folderLabel.Location = new System.Drawing.Point(8, 39);
            document_folderLabel.Name = "document_folderLabel";
            document_folderLabel.Size = new System.Drawing.Size(39, 13);
            document_folderLabel.TabIndex = 8;
            document_folderLabel.Text = "Folder:";
            // 
            // dtgFolder
            // 
            this.dtgFolder.AllowUserToAddRows = false;
            this.dtgFolder.AllowUserToDeleteRows = false;
            this.dtgFolder.AllowUserToResizeColumns = false;
            this.dtgFolder.AllowUserToResizeRows = false;
            this.dtgFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgFolder.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgFolder.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgFolder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgFolder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.folder,
            this.dtgFolder_ID,
            this.dtgFolder_Name});
            this.dtgFolder.DataSource = this.document_folderBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgFolder.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgFolder.Location = new System.Drawing.Point(0, 60);
            this.dtgFolder.Name = "dtgFolder";
            this.dtgFolder.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgFolder.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgFolder.RowHeadersVisible = false;
            this.dtgFolder.RowHeadersWidth = 20;
            this.dtgFolder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgFolder.Size = new System.Drawing.Size(387, 304);
            this.dtgFolder.TabIndex = 5;
            this.dtgFolder.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.document_folderDataGridView_DataBindingComplete);
            this.dtgFolder.SelectionChanged += new System.EventHandler(this.dtgFolder_SelectionChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            // 
            // document_folderBindingNavigator
            // 
            this.document_folderBindingNavigator.AddNewItem = this.btnAdd;
            this.document_folderBindingNavigator.BackColor = System.Drawing.SystemColors.Control;
            this.document_folderBindingNavigator.BindingSource = this.document_folderBindingSource;
            this.document_folderBindingNavigator.CountItem = null;
            this.document_folderBindingNavigator.DeleteItem = null;
            this.document_folderBindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.document_folderBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnDelete,
            this.btnSave});
            this.document_folderBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.document_folderBindingNavigator.MoveFirstItem = null;
            this.document_folderBindingNavigator.MoveLastItem = null;
            this.document_folderBindingNavigator.MoveNextItem = null;
            this.document_folderBindingNavigator.MovePreviousItem = null;
            this.document_folderBindingNavigator.Name = "document_folderBindingNavigator";
            this.document_folderBindingNavigator.PositionItem = null;
            this.document_folderBindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.document_folderBindingNavigator.Size = new System.Drawing.Size(392, 25);
            this.document_folderBindingNavigator.TabIndex = 6;
            this.document_folderBindingNavigator.Text = "bindingNavigator1";
            // 
            // txtFolderName
            // 
            this.txtFolderName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.document_folderBindingSource, "document_folder", true));
            this.txtFolderName.Location = new System.Drawing.Point(50, 34);
            this.txtFolderName.MaxLength = 200;
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new System.Drawing.Size(280, 20);
            this.txtFolderName.TabIndex = 9;
            this.txtFolderName.Validating += new System.ComponentModel.CancelEventHandler(this.txtFolderName_Validating);
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.RightToLeftAutoMirrorImage = true;
            this.btnAdd.Size = new System.Drawing.Size(23, 22);
            this.btnAdd.Text = "Add new";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RightToLeftAutoMirrorImage = true;
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save Data";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Width = 35;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "document_folder";
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn2.HeaderText = "ID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // folder
            // 
            this.folder.HeaderText = "";
            this.folder.Image = ((System.Drawing.Image)(resources.GetObject("folder.Image")));
            this.folder.Name = "folder";
            this.folder.ReadOnly = true;
            this.folder.Width = 35;
            // 
            // dtgFolder_ID
            // 
            this.dtgFolder_ID.DataPropertyName = "id";
            this.dtgFolder_ID.HeaderText = "ID";
            this.dtgFolder_ID.Name = "dtgFolder_ID";
            this.dtgFolder_ID.ReadOnly = true;
            this.dtgFolder_ID.Visible = false;
            // 
            // dtgFolder_Name
            // 
            this.dtgFolder_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dtgFolder_Name.DataPropertyName = "document_folder";
            this.dtgFolder_Name.HeaderText = "Name";
            this.dtgFolder_Name.Name = "dtgFolder_Name";
            this.dtgFolder_Name.ReadOnly = true;
            this.dtgFolder_Name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn2.Image")));
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Width = 35;
            // 
            // frmFolder
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(392, 373);
            this.Controls.Add(document_folderLabel);
            this.Controls.Add(this.txtFolderName);
            this.Controls.Add(this.document_folderBindingNavigator);
            this.Controls.Add(this.dtgFolder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "frmFolder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Folders";
            this.Load += new System.EventHandler(this.frmFolder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document_folderBindingNavigator)).EndInit();
            this.document_folderBindingNavigator.ResumeLayout(false);
            this.document_folderBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.document_folderBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgFolder;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.BindingSource document_folderBindingSource;
        private System.Windows.Forms.BindingNavigator document_folderBindingNavigator;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.TextBox txtFolderName;
		private System.Windows.Forms.ToolStripButton btnAdd;
		private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewImageColumn folder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgFolder_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgFolder_Name;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
    }
}