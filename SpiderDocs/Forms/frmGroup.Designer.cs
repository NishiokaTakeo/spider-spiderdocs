namespace SpiderDocs
{
    partial class frmGroup
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
            System.Windows.Forms.Label group_nameLabel;
            System.Windows.Forms.Label obsLabel;
            this.groupBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.groupBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.dtgGroup = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.txtGroupComments = new System.Windows.Forms.TextBox();
            idLabel = new System.Windows.Forms.Label();
            group_nameLabel = new System.Windows.Forms.Label();
            obsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.groupBindingNavigator)).BeginInit();
            this.groupBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new System.Drawing.Point(263, 284);
            idLabel.Name = "idLabel";
            idLabel.Size = new System.Drawing.Size(18, 13);
            idLabel.TabIndex = 2;
            idLabel.Text = "id:";
            // 
            // group_nameLabel
            // 
            group_nameLabel.AutoSize = true;
            group_nameLabel.Location = new System.Drawing.Point(5, 34);
            group_nameLabel.Name = "group_nameLabel";
            group_nameLabel.Size = new System.Drawing.Size(99, 13);
            group_nameLabel.TabIndex = 4;
            group_nameLabel.Text = "Group\\Department:";
            // 
            // obsLabel
            // 
            obsLabel.AutoSize = true;
            obsLabel.Location = new System.Drawing.Point(5, 57);
            obsLabel.Name = "obsLabel";
            obsLabel.Size = new System.Drawing.Size(59, 13);
            obsLabel.TabIndex = 6;
            obsLabel.Text = "Comments:";
            // 
            // groupBindingNavigator
            // 
            this.groupBindingNavigator.AddNewItem = this.btnAdd;
            this.groupBindingNavigator.BackColor = System.Drawing.SystemColors.Control;
            this.groupBindingNavigator.BindingSource = this.groupBindingSource;
            this.groupBindingNavigator.CountItem = null;
            this.groupBindingNavigator.DeleteItem = null;
            this.groupBindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.groupBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnDelete,
            this.btnSave});
            this.groupBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.groupBindingNavigator.MoveFirstItem = null;
            this.groupBindingNavigator.MoveLastItem = null;
            this.groupBindingNavigator.MoveNextItem = null;
            this.groupBindingNavigator.MovePreviousItem = null;
            this.groupBindingNavigator.Name = "groupBindingNavigator";
            this.groupBindingNavigator.PositionItem = null;
            this.groupBindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.groupBindingNavigator.Size = new System.Drawing.Size(552, 25);
            this.groupBindingNavigator.TabIndex = 0;
            this.groupBindingNavigator.Text = "bindingNavigator1";
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
            // dtgGroup
            // 
            this.dtgGroup.AllowUserToAddRows = false;
            this.dtgGroup.AllowUserToDeleteRows = false;
            this.dtgGroup.AllowUserToOrderColumns = true;
            this.dtgGroup.AllowUserToResizeRows = false;
            this.dtgGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgGroup.AutoGenerateColumns = false;
            this.dtgGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgGroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dtgGroup.DataSource = this.groupBindingSource;
            this.dtgGroup.Location = new System.Drawing.Point(0, 80);
            this.dtgGroup.MultiSelect = false;
            this.dtgGroup.Name = "dtgGroup";
            this.dtgGroup.ReadOnly = true;
            this.dtgGroup.RowHeadersVisible = false;
            this.dtgGroup.RowHeadersWidth = 20;
            this.dtgGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgGroup.Size = new System.Drawing.Size(544, 238);
            this.dtgGroup.TabIndex = 1;
            this.dtgGroup.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dtgGroup_DataBindingComplete);
            this.dtgGroup.SelectionChanged += new System.EventHandler(this.dtgGroup_SelectionChanged);
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
            this.dataGridViewTextBoxColumn2.DataPropertyName = "group_name";
            this.dataGridViewTextBoxColumn2.FillWeight = 35F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Group\\Departament";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 220;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "obs";
            this.dataGridViewTextBoxColumn3.FillWeight = 65F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Comments";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 260;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // idTextBox
            // 
            this.idTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.groupBindingSource, "id", true));
            this.idTextBox.Location = new System.Drawing.Point(335, 281);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 3;
            // 
            // txtGroupName
            // 
            this.txtGroupName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.groupBindingSource, "group_name", true));
            this.txtGroupName.Location = new System.Drawing.Point(111, 31);
            this.txtGroupName.MaxLength = 50;
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(206, 20);
            this.txtGroupName.TabIndex = 5;
            this.txtGroupName.Validating += new System.ComponentModel.CancelEventHandler(this.txtGroupName_Validating);
            // 
            // txtGroupComments
            // 
            this.txtGroupComments.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.groupBindingSource, "obs", true));
            this.txtGroupComments.Location = new System.Drawing.Point(111, 54);
            this.txtGroupComments.MaxLength = 200;
            this.txtGroupComments.Name = "txtGroupComments";
            this.txtGroupComments.Size = new System.Drawing.Size(383, 20);
            this.txtGroupComments.TabIndex = 7;
            // 
            // frmGroup
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(552, 329);
            this.Controls.Add(this.dtgGroup);
            this.Controls.Add(idLabel);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(group_nameLabel);
            this.Controls.Add(this.txtGroupName);
            this.Controls.Add(obsLabel);
            this.Controls.Add(this.txtGroupComments);
            this.Controls.Add(this.groupBindingNavigator);
            this.MinimumSize = new System.Drawing.Size(560, 356);
            this.Name = "frmGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Groups/Departments";
            this.Load += new System.EventHandler(this.frmGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBindingNavigator)).EndInit();
            this.groupBindingNavigator.ResumeLayout(false);
            this.groupBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.BindingSource groupBindingSource;
        private System.Windows.Forms.BindingNavigator groupBindingNavigator;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.DataGridView dtgGroup;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox txtGroupName;
		private System.Windows.Forms.TextBox txtGroupComments;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

    }
}