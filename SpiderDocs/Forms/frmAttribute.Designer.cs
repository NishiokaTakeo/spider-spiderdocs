namespace SpiderDocs
{
    partial class frmAttribute
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
			System.Windows.Forms.Label lbFolder;
			System.Windows.Forms.Label label5;
			System.Windows.Forms.Label label4;
			System.Windows.Forms.Label nameLabel;
			System.Windows.Forms.Label id_typeLabel;
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dtgAttribute = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewCheckBoxColumn6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.StripToolButtons = new System.Windows.Forms.ToolStrip();
			this.btnAdd = new System.Windows.Forms.ToolStripButton();
			this.btnDelete = new System.Windows.Forms.ToolStripButton();
			this.btnSave = new System.Windows.Forms.ToolStripButton();
			this.lbNewAttribute = new System.Windows.Forms.ToolStripLabel();
			this.plEntries = new System.Windows.Forms.Panel();
			this.cboFolder = new System.Windows.Forms.ComboBox();
			this.cboFieldType = new System.Windows.Forms.ComboBox();
			this.btnAddList = new System.Windows.Forms.Button();
			this.txtAttributeName = new System.Windows.Forms.TextBox();
			this.only_numbersCheckBox = new System.Windows.Forms.CheckBox();
			this.max_lenghLabel = new System.Windows.Forms.Label();
			this.max_lenghTextBox = new System.Windows.Forms.TextBox();
			this.requiredCheckBox = new System.Windows.Forms.CheckBox();
			lbFolder = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			nameLabel = new System.Windows.Forms.Label();
			id_typeLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dtgAttribute)).BeginInit();
			this.StripToolButtons.SuspendLayout();
			this.plEntries.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbFolder
			// 
			lbFolder.AutoSize = true;
			lbFolder.ForeColor = System.Drawing.SystemColors.ControlText;
			lbFolder.Location = new System.Drawing.Point(293, 6);
			lbFolder.Name = "lbFolder";
			lbFolder.Size = new System.Drawing.Size(39, 13);
			lbFolder.TabIndex = 125;
			lbFolder.Text = "Folder:";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.ForeColor = System.Drawing.SystemColors.ControlText;
			label5.Location = new System.Drawing.Point(331, 30);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(71, 13);
			label5.TabIndex = 123;
			label5.Text = "Only numbers";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.ForeColor = System.Drawing.SystemColors.ControlText;
			label4.Location = new System.Drawing.Point(331, 50);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(57, 13);
			label4.TabIndex = 122;
			label4.Text = "Mandatory";
			// 
			// nameLabel
			// 
			nameLabel.AutoSize = true;
			nameLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			nameLabel.Location = new System.Drawing.Point(24, 8);
			nameLabel.Name = "nameLabel";
			nameLabel.Size = new System.Drawing.Size(38, 13);
			nameLabel.TabIndex = 117;
			nameLabel.Text = "Name:";
			// 
			// id_typeLabel
			// 
			id_typeLabel.AutoSize = true;
			id_typeLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			id_typeLabel.Location = new System.Drawing.Point(7, 32);
			id_typeLabel.Name = "id_typeLabel";
			id_typeLabel.Size = new System.Drawing.Size(55, 13);
			id_typeLabel.TabIndex = 120;
			id_typeLabel.Text = "Field type:";
			// 
			// dtgAttribute
			// 
			this.dtgAttribute.AllowUserToAddRows = false;
			this.dtgAttribute.AllowUserToDeleteRows = false;
			this.dtgAttribute.AllowUserToOrderColumns = true;
			this.dtgAttribute.AllowUserToResizeRows = false;
			this.dtgAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dtgAttribute.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dtgAttribute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dtgAttribute.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewComboBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewCheckBoxColumn3,
            this.dataGridViewCheckBoxColumn4,
            this.dataGridViewCheckBoxColumn5,
            this.dataGridViewCheckBoxColumn6});
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dtgAttribute.DefaultCellStyle = dataGridViewCellStyle5;
			this.dtgAttribute.Location = new System.Drawing.Point(0, 104);
			this.dtgAttribute.MultiSelect = false;
			this.dtgAttribute.Name = "dtgAttribute";
			this.dtgAttribute.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dtgAttribute.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dtgAttribute.RowHeadersVisible = false;
			this.dtgAttribute.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dtgAttribute.Size = new System.Drawing.Size(734, 312);
			this.dtgAttribute.TabIndex = 103;
			this.dtgAttribute.SelectionChanged += new System.EventHandler(this.dtgAttribute_SelectionChanged);
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
			this.dataGridViewTextBoxColumn2.DataPropertyName = "name";
			this.dataGridViewTextBoxColumn2.HeaderText = "Name";
			this.dataGridViewTextBoxColumn2.MinimumWidth = 100;
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			// 
			// dataGridViewComboBoxColumn3
			// 
			this.dataGridViewComboBoxColumn3.DataPropertyName = "int_id_type";
			this.dataGridViewComboBoxColumn3.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
			this.dataGridViewComboBoxColumn3.HeaderText = "Type";
			this.dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
			this.dataGridViewComboBoxColumn3.ReadOnly = true;
			this.dataGridViewComboBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewComboBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.DataPropertyName = "position";
			this.dataGridViewTextBoxColumn4.HeaderText = "Position";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Visible = false;
			this.dataGridViewTextBoxColumn4.Width = 60;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "period_research";
			this.dataGridViewTextBoxColumn5.HeaderText = "Default period";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.DataPropertyName = "max_lengh";
			this.dataGridViewTextBoxColumn6.HeaderText = "Length";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			this.dataGridViewTextBoxColumn6.Width = 80;
			// 
			// dataGridViewCheckBoxColumn2
			// 
			this.dataGridViewCheckBoxColumn2.DataPropertyName = "system_field";
			this.dataGridViewCheckBoxColumn2.HeaderText = "System field";
			this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
			this.dataGridViewCheckBoxColumn2.ReadOnly = true;
			this.dataGridViewCheckBoxColumn2.Visible = false;
			this.dataGridViewCheckBoxColumn2.Width = 90;
			// 
			// dataGridViewCheckBoxColumn3
			// 
			this.dataGridViewCheckBoxColumn3.DataPropertyName = "required";
			this.dataGridViewCheckBoxColumn3.FalseValue = "0";
			this.dataGridViewCheckBoxColumn3.HeaderText = "Mandatory";
			this.dataGridViewCheckBoxColumn3.IndeterminateValue = "2";
			this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
			this.dataGridViewCheckBoxColumn3.ReadOnly = true;
			this.dataGridViewCheckBoxColumn3.ThreeState = true;
			this.dataGridViewCheckBoxColumn3.TrueValue = "1";
			this.dataGridViewCheckBoxColumn3.Width = 90;
			// 
			// dataGridViewCheckBoxColumn4
			// 
			this.dataGridViewCheckBoxColumn4.DataPropertyName = "only_numbers";
			this.dataGridViewCheckBoxColumn4.HeaderText = "Only numbers";
			this.dataGridViewCheckBoxColumn4.Name = "dataGridViewCheckBoxColumn4";
			this.dataGridViewCheckBoxColumn4.ReadOnly = true;
			this.dataGridViewCheckBoxColumn4.Width = 90;
			// 
			// dataGridViewCheckBoxColumn5
			// 
			this.dataGridViewCheckBoxColumn5.DataPropertyName = "appear_query";
			this.dataGridViewCheckBoxColumn5.HeaderText = "Show in queries";
			this.dataGridViewCheckBoxColumn5.Name = "dataGridViewCheckBoxColumn5";
			this.dataGridViewCheckBoxColumn5.ReadOnly = true;
			this.dataGridViewCheckBoxColumn5.Visible = false;
			// 
			// dataGridViewCheckBoxColumn6
			// 
			this.dataGridViewCheckBoxColumn6.DataPropertyName = "appear_input";
			this.dataGridViewCheckBoxColumn6.HeaderText = "Show in inputs";
			this.dataGridViewCheckBoxColumn6.Name = "dataGridViewCheckBoxColumn6";
			this.dataGridViewCheckBoxColumn6.ReadOnly = true;
			this.dataGridViewCheckBoxColumn6.Visible = false;
			// 
			// StripToolButtons
			// 
			this.StripToolButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.StripToolButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnDelete,
            this.btnSave,
            this.lbNewAttribute});
			this.StripToolButtons.Location = new System.Drawing.Point(0, 0);
			this.StripToolButtons.Name = "StripToolButtons";
			this.StripToolButtons.ShowItemToolTips = false;
			this.StripToolButtons.Size = new System.Drawing.Size(742, 25);
			this.StripToolButtons.TabIndex = 110;
			// 
			// btnAdd
			// 
			this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnAdd.Image = global::SpiderDocs.Properties.Resources.add2;
			this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(23, 22);
			this.btnAdd.Text = "toolStripButton1";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnDelete.Image = global::SpiderDocs.Properties.Resources.delete;
			this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(23, 22);
			this.btnDelete.Text = "toolStripButton2";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnSave
			// 
			this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnSave.Image = global::SpiderDocs.Properties.Resources.salvar;
			this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(23, 22);
			this.btnSave.Text = "toolStripButton3";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// lbNewAttribute
			// 
			this.lbNewAttribute.ForeColor = System.Drawing.Color.Red;
			this.lbNewAttribute.Name = "lbNewAttribute";
			this.lbNewAttribute.Size = new System.Drawing.Size(117, 22);
			this.lbNewAttribute.Text = "*** New Attribute ***";
			this.lbNewAttribute.Visible = false;
			// 
			// plEntries
			// 
			this.plEntries.Controls.Add(lbFolder);
			this.plEntries.Controls.Add(this.cboFolder);
			this.plEntries.Controls.Add(label5);
			this.plEntries.Controls.Add(label4);
			this.plEntries.Controls.Add(this.cboFieldType);
			this.plEntries.Controls.Add(this.btnAddList);
			this.plEntries.Controls.Add(this.txtAttributeName);
			this.plEntries.Controls.Add(this.only_numbersCheckBox);
			this.plEntries.Controls.Add(this.max_lenghLabel);
			this.plEntries.Controls.Add(this.max_lenghTextBox);
			this.plEntries.Controls.Add(this.requiredCheckBox);
			this.plEntries.Controls.Add(nameLabel);
			this.plEntries.Controls.Add(id_typeLabel);
			this.plEntries.Location = new System.Drawing.Point(0, 28);
			this.plEntries.Name = "plEntries";
			this.plEntries.Size = new System.Drawing.Size(532, 70);
			this.plEntries.TabIndex = 113;
			// 
			// cboFolder
			// 
			this.cboFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboFolder.Location = new System.Drawing.Point(338, 3);
			this.cboFolder.Name = "cboFolder";
			this.cboFolder.Size = new System.Drawing.Size(186, 21);
			this.cboFolder.TabIndex = 124;
			this.cboFolder.SelectedIndexChanged += new System.EventHandler(this.cboFolder_SelectedIndexChanged);
			// 
			// cboFieldType
			// 
			this.cboFieldType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboFieldType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboFieldType.FormattingEnabled = true;
			this.cboFieldType.Location = new System.Drawing.Point(65, 27);
			this.cboFieldType.Name = "cboFieldType";
			this.cboFieldType.Size = new System.Drawing.Size(186, 21);
			this.cboFieldType.TabIndex = 115;
			this.cboFieldType.SelectedIndexChanged += new System.EventHandler(this.cboFieldType_SelectedIndexChanged);
			// 
			// btnAddList
			// 
			this.btnAddList.Enabled = false;
			this.btnAddList.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAddList.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
			this.btnAddList.Location = new System.Drawing.Point(254, 27);
			this.btnAddList.Margin = new System.Windows.Forms.Padding(0);
			this.btnAddList.Name = "btnAddList";
			this.btnAddList.Size = new System.Drawing.Size(20, 20);
			this.btnAddList.TabIndex = 118;
			this.btnAddList.Text = "+";
			this.btnAddList.UseVisualStyleBackColor = true;
			this.btnAddList.Click += new System.EventHandler(this.btnAddList_Click);
			// 
			// txtAttributeName
			// 
			this.txtAttributeName.Location = new System.Drawing.Point(66, 3);
			this.txtAttributeName.MaxLength = 16;
			this.txtAttributeName.Name = "txtAttributeName";
			this.txtAttributeName.Size = new System.Drawing.Size(208, 20);
			this.txtAttributeName.TabIndex = 113;
			this.txtAttributeName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAttributeName_KeyPress);
			// 
			// only_numbersCheckBox
			// 
			this.only_numbersCheckBox.ForeColor = System.Drawing.Color.White;
			this.only_numbersCheckBox.Location = new System.Drawing.Point(314, 24);
			this.only_numbersCheckBox.Name = "only_numbersCheckBox";
			this.only_numbersCheckBox.Size = new System.Drawing.Size(104, 24);
			this.only_numbersCheckBox.TabIndex = 116;
			this.only_numbersCheckBox.UseVisualStyleBackColor = true;
			// 
			// max_lenghLabel
			// 
			this.max_lenghLabel.AutoSize = true;
			this.max_lenghLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.max_lenghLabel.Location = new System.Drawing.Point(423, 31);
			this.max_lenghLabel.Name = "max_lenghLabel";
			this.max_lenghLabel.Size = new System.Drawing.Size(59, 13);
			this.max_lenghLabel.TabIndex = 121;
			this.max_lenghLabel.Text = "Max lengh:";
			// 
			// max_lenghTextBox
			// 
			this.max_lenghTextBox.Location = new System.Drawing.Point(484, 27);
			this.max_lenghTextBox.MaxLength = 3;
			this.max_lenghTextBox.Name = "max_lenghTextBox";
			this.max_lenghTextBox.Size = new System.Drawing.Size(38, 20);
			this.max_lenghTextBox.TabIndex = 114;
			this.max_lenghTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxesForDigits_KeyPress);
			// 
			// requiredCheckBox
			// 
			this.requiredCheckBox.ForeColor = System.Drawing.Color.White;
			this.requiredCheckBox.Location = new System.Drawing.Point(314, 47);
			this.requiredCheckBox.Name = "requiredCheckBox";
			this.requiredCheckBox.Size = new System.Drawing.Size(104, 22);
			this.requiredCheckBox.TabIndex = 119;
			this.requiredCheckBox.ThreeState = true;
			this.requiredCheckBox.UseVisualStyleBackColor = true;
			// 
			// frmAttribute
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(742, 427);
			this.Controls.Add(this.plEntries);
			this.Controls.Add(this.StripToolButtons);
			this.Controls.Add(this.dtgAttribute);
			this.MinimumSize = new System.Drawing.Size(750, 400);
			this.Name = "frmAttribute";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Attributes";
			((System.ComponentModel.ISupportInitialize)(this.dtgAttribute)).EndInit();
			this.StripToolButtons.ResumeLayout(false);
			this.StripToolButtons.PerformLayout();
			this.plEntries.ResumeLayout(false);
			this.plEntries.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.DataGridView dtgAttribute;
		private System.Windows.Forms.ToolStrip StripToolButtons;
		private System.Windows.Forms.ToolStripButton btnAdd;
		private System.Windows.Forms.ToolStripButton btnDelete;
		private System.Windows.Forms.ToolStripButton btnSave;
		private System.Windows.Forms.ToolStripLabel lbNewAttribute;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn4;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn5;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn6;
		private System.Windows.Forms.Panel plEntries;
		private System.Windows.Forms.ComboBox cboFolder;
		private System.Windows.Forms.ComboBox cboFieldType;
		private System.Windows.Forms.Button btnAddList;
		private System.Windows.Forms.TextBox txtAttributeName;
		private System.Windows.Forms.CheckBox only_numbersCheckBox;
		private System.Windows.Forms.Label max_lenghLabel;
		private System.Windows.Forms.TextBox max_lenghTextBox;
		private System.Windows.Forms.CheckBox requiredCheckBox;
    }
}