namespace SpiderDocs
{
    partial class frmDocumentType
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
            System.Windows.Forms.Label idLabel;
            System.Windows.Forms.Label typeLabel;
            this.document_typeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.document_typeBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.txtDocType = new System.Windows.Forms.TextBox();
            this.dtgDocType = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dragAndDropListView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.attributes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duplicate_chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            idLabel = new System.Windows.Forms.Label();
            typeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.document_typeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document_typeBindingNavigator)).BeginInit();
            this.document_typeBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDocType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dragAndDropListView)).BeginInit();
            this.SuspendLayout();
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Enabled = false;
            idLabel.Location = new System.Drawing.Point(190, 98);
            idLabel.Name = "idLabel";
            idLabel.Size = new System.Drawing.Size(18, 13);
            idLabel.TabIndex = 2;
            idLabel.Text = "id:";
            idLabel.Visible = false;
            // 
            // typeLabel
            // 
            typeLabel.AutoSize = true;
            typeLabel.Location = new System.Drawing.Point(5, 30);
            typeLabel.Name = "typeLabel";
            typeLabel.Size = new System.Drawing.Size(34, 13);
            typeLabel.TabIndex = 4;
            typeLabel.Text = "Type:";
            // 
            // document_typeBindingNavigator
            // 
            this.document_typeBindingNavigator.AddNewItem = this.btnAdd;
            this.document_typeBindingNavigator.BackColor = System.Drawing.SystemColors.Control;
            this.document_typeBindingNavigator.BindingSource = this.document_typeBindingSource;
            this.document_typeBindingNavigator.CountItem = null;
            this.document_typeBindingNavigator.DeleteItem = null;
            this.document_typeBindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.document_typeBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnDelete,
            this.btnSave});
            this.document_typeBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.document_typeBindingNavigator.MoveFirstItem = null;
            this.document_typeBindingNavigator.MoveLastItem = null;
            this.document_typeBindingNavigator.MoveNextItem = null;
            this.document_typeBindingNavigator.MovePreviousItem = null;
            this.document_typeBindingNavigator.Name = "document_typeBindingNavigator";
            this.document_typeBindingNavigator.PositionItem = null;
            this.document_typeBindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.document_typeBindingNavigator.Size = new System.Drawing.Size(738, 25);
            this.document_typeBindingNavigator.TabIndex = 0;
            this.document_typeBindingNavigator.Text = "bindingNavigator1";
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = global::SpiderDocs.Properties.Resources.add2;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.RightToLeftAutoMirrorImage = true;
            this.btnAdd.Size = new System.Drawing.Size(23, 22);
            this.btnAdd.Text = "Add new";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = global::SpiderDocs.Properties.Resources.delete;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RightToLeftAutoMirrorImage = true;
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::SpiderDocs.Properties.Resources.salvar;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save Data";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtDocType
            // 
            this.txtDocType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.document_typeBindingSource, "type", true));
            this.txtDocType.Location = new System.Drawing.Point(5, 46);
            this.txtDocType.MaxLength = 80;
            this.txtDocType.Name = "txtDocType";
            this.txtDocType.Size = new System.Drawing.Size(222, 20);
            this.txtDocType.TabIndex = 5;
            this.txtDocType.Validating += new System.ComponentModel.CancelEventHandler(this.txtDocType_Validating);
            // 
            // dtgDocType
            // 
            this.dtgDocType.AllowUserToAddRows = false;
            this.dtgDocType.AllowUserToDeleteRows = false;
            this.dtgDocType.AllowUserToResizeColumns = false;
            this.dtgDocType.AllowUserToResizeRows = false;
            this.dtgDocType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgDocType.AutoGenerateColumns = false;
            this.dtgDocType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDocType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dtgDocType.DataSource = this.document_typeBindingSource;
            this.dtgDocType.Location = new System.Drawing.Point(5, 69);
            this.dtgDocType.MultiSelect = false;
            this.dtgDocType.Name = "dtgDocType";
            this.dtgDocType.ReadOnly = true;
            this.dtgDocType.RowHeadersVisible = false;
            this.dtgDocType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgDocType.Size = new System.Drawing.Size(390, 451);
            this.dtgDocType.TabIndex = 1;
            this.dtgDocType.SelectionChanged += new System.EventHandler(this.dtgDocType_SelectionChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(448, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Link each attribute with the Document type.";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(448, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Drag and Drop to change order they will apear.";
            // 
            // dragAndDropListView
            // 
            this.dragAndDropListView.AllowUserToAddRows = false;
            this.dragAndDropListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dragAndDropListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dragAndDropListView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chk,
            this.attributes,
            this.type,
            this.duplicate_chk});
            this.dragAndDropListView.Location = new System.Drawing.Point(401, 69);
            this.dragAndDropListView.Name = "dragAndDropListView";
            this.dragAndDropListView.Size = new System.Drawing.Size(337, 451);
            this.dragAndDropListView.TabIndex = 28;
            this.dragAndDropListView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dragAndDropListView_CellMouseUp);
            this.dragAndDropListView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dragAndDropListView_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "type";
            this.dataGridViewTextBoxColumn2.HeaderText = "Type";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.FillWeight = 25F;
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 25;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Attributes";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FillWeight = 80F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Field Type";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 80;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.FillWeight = 80F;
            this.dataGridViewCheckBoxColumn2.HeaderText = "Mandatory";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.Width = 80;
            // 
            // chk
            // 
            this.chk.FillWeight = 25F;
            this.chk.HeaderText = "";
            this.chk.Name = "chk";
            this.chk.Width = 25;
            // 
            // attributes
            // 
            this.attributes.FillWeight = 110F;
            this.attributes.HeaderText = "Attributes";
            this.attributes.Name = "attributes";
            this.attributes.ReadOnly = true;
            this.attributes.Width = 110;
            // 
            // type
            // 
            this.type.FillWeight = 80F;
            this.type.HeaderText = "Field Type";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Width = 80;
            // 
            // duplicate_chk
            // 
            this.duplicate_chk.FillWeight = 80F;
            this.duplicate_chk.HeaderText = "No Duplicate";
            this.duplicate_chk.Name = "duplicate_chk";
            this.duplicate_chk.Width = 80;
            // 
            // frmDocumentType
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(738, 525);
            this.Controls.Add(this.dragAndDropListView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgDocType);
            this.Controls.Add(typeLabel);
            this.Controls.Add(this.txtDocType);
            this.Controls.Add(this.document_typeBindingNavigator);
            this.Controls.Add(idLabel);
            this.MinimumSize = new System.Drawing.Size(577, 466);
            this.Name = "frmDocumentType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Document Types";
            this.Load += new System.EventHandler(this.frmAttribute_Load);
            ((System.ComponentModel.ISupportInitialize)(this.document_typeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document_typeBindingNavigator)).EndInit();
            this.document_typeBindingNavigator.ResumeLayout(false);
            this.document_typeBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDocType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dragAndDropListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.BindingSource document_typeBindingSource;
        private System.Windows.Forms.BindingNavigator document_typeBindingNavigator;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnDelete;
		private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.TextBox txtDocType;
		private System.Windows.Forms.DataGridView dtgDocType;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridView dragAndDropListView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk;
        private System.Windows.Forms.DataGridViewTextBoxColumn attributes;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewCheckBoxColumn duplicate_chk;
    }
}