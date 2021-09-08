namespace SpiderDocsForms
{
    partial class frmAttributeComboItem
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAttributeComboItem));
			this.dtgCustomTable = new System.Windows.Forms.DataGridView();
			this.max_lengh = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.period_research = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
			this.btnAdd = new System.Windows.Forms.ToolStripButton();
			this.btnDelete = new System.Windows.Forms.ToolStripButton();
			this.btnSave = new System.Windows.Forms.ToolStripButton();
			this.txtItem = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblTitle = new System.Windows.Forms.Label();
			this.lblCustom = new System.Windows.Forms.Label();
			this.lblReg = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dtgCustomTable)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
			this.bindingNavigator1.SuspendLayout();
			this.SuspendLayout();
			//
			// dtgCustomTable
			//
			this.dtgCustomTable.AllowUserToAddRows = false;
			this.dtgCustomTable.AllowUserToDeleteRows = false;
			this.dtgCustomTable.AllowUserToResizeColumns = false;
			this.dtgCustomTable.AllowUserToResizeRows = false;
			this.dtgCustomTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dtgCustomTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dtgCustomTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dtgCustomTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.max_lengh,
            this.Column1,
            this.period_research});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dtgCustomTable.DefaultCellStyle = dataGridViewCellStyle2;
			this.dtgCustomTable.Location = new System.Drawing.Point(12, 80);
			this.dtgCustomTable.MultiSelect = false;
			this.dtgCustomTable.Name = "dtgCustomTable";
			this.dtgCustomTable.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dtgCustomTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dtgCustomTable.RowHeadersVisible = false;
			this.dtgCustomTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dtgCustomTable.Size = new System.Drawing.Size(330, 229);
			this.dtgCustomTable.TabIndex = 2;
			this.dtgCustomTable.SelectionChanged += new System.EventHandler(this.dtgCustomTable_SelectionChanged);
			//
			// max_lengh
			//
			this.max_lengh.DataPropertyName = "id";
			this.max_lengh.HeaderText = "id";
			this.max_lengh.Name = "max_lengh";
			this.max_lengh.ReadOnly = true;
			this.max_lengh.Visible = false;
			this.max_lengh.Width = 60;
			//
			// Column1
			//
			this.Column1.DataPropertyName = "id_atb";
			this.Column1.HeaderText = "id_atb";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.Visible = false;
			//
			// period_research
			//
			this.period_research.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.period_research.DataPropertyName = "value";
			this.period_research.HeaderText = "Value";
			this.period_research.Name = "period_research";
			this.period_research.ReadOnly = true;
			//
			// bindingNavigator1
			//
			this.bindingNavigator1.AddNewItem = this.btnAdd;
			this.bindingNavigator1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.bindingNavigator1.CountItem = null;
			this.bindingNavigator1.DeleteItem = this.btnDelete;
			this.bindingNavigator1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnDelete,
            this.btnSave});
			this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
			this.bindingNavigator1.MoveFirstItem = null;
			this.bindingNavigator1.MoveLastItem = null;
			this.bindingNavigator1.MoveNextItem = null;
			this.bindingNavigator1.MovePreviousItem = null;
			this.bindingNavigator1.Name = "bindingNavigator1";
			this.bindingNavigator1.PositionItem = null;
			this.bindingNavigator1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.bindingNavigator1.Size = new System.Drawing.Size(354, 25);
			this.bindingNavigator1.TabIndex = 3;
			this.bindingNavigator1.Text = "bindingNavigator1";
			//
			// btnAdd
			//
			this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
			this.btnAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.RightToLeftAutoMirrorImage = true;
			this.btnAdd.Size = new System.Drawing.Size(23, 22);
			this.btnAdd.Text = "Add new";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			//
			// btnDelete
			//
			this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnDelete.Enabled = false;
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
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
			this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(23, 22);
			this.btnSave.Text = "toolStripButton1";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			//
			// txtItem
			//
			this.txtItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtItem.Location = new System.Drawing.Point(84, 54);
			this.txtItem.MaxLength = 80;
			this.txtItem.Name = "txtItem";
			this.txtItem.Size = new System.Drawing.Size(235, 20);
			this.txtItem.TabIndex = 4;
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Location = new System.Drawing.Point(9, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Item name:";
			//
			// lblTitle
			//
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblTitle.Location = new System.Drawing.Point(9, 31);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(50, 15);
			this.lblTitle.TabIndex = 6;
			this.lblTitle.Text = "Combo:";
			//
			// lblCustom
			//
			this.lblCustom.AutoSize = true;
			this.lblCustom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCustom.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCustom.Location = new System.Drawing.Point(81, 31);
			this.lblCustom.Name = "lblCustom";
			this.lblCustom.Size = new System.Drawing.Size(62, 15);
			this.lblCustom.TabIndex = 7;
			this.lblCustom.Text = "lblCustom";
			//
			// lblReg
			//
			this.lblReg.AutoSize = true;
			this.lblReg.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblReg.Location = new System.Drawing.Point(12, 313);
			this.lblReg.Name = "lblReg";
			this.lblReg.Size = new System.Drawing.Size(13, 13);
			this.lblReg.TabIndex = 8;
			this.lblReg.Text = "0";
			//
			// frmAttributeComboItem
			//
			this.AutoSize = true;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(354, 335);
			this.Controls.Add(this.txtItem);
			this.Controls.Add(this.lblReg);
			this.Controls.Add(this.lblCustom);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.bindingNavigator1);
			this.Controls.Add(this.dtgCustomTable);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAttributeComboItem";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Attributes combo items";
			this.Load += new System.EventHandler(this.frmAttributeComboItem_Load);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			((System.ComponentModel.ISupportInitialize)(this.dtgCustomTable)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
			this.bindingNavigator1.ResumeLayout(false);
			this.bindingNavigator1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgCustomTable;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCustom;
        private System.Windows.Forms.Label lblReg;
        private System.Windows.Forms.DataGridViewTextBoxColumn max_lengh;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn period_research;

    }
}