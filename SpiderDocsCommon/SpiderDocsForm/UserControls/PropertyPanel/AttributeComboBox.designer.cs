namespace SpiderDocsForms {
	partial class AttributeComboBox {
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.cboComboBox = new SpiderCustomComponent.CheckComboBox();
            this.SuspendLayout();
            //
            // btnAdd
            //
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.AutoSize = true;
            this.btnAdd.Location = new System.Drawing.Point(170, -1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(23, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            //
            // cboComboBox
            //
            this.cboComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboComboBox.CheckOnClick = true;
            this.cboComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboComboBox.DropDownHeight = 1;
            this.cboComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboComboBox.FormattingEnabled = true;
            this.cboComboBox.IntegralHeight = false;
            this.cboComboBox.Location = new System.Drawing.Point(0, 0);
            this.cboComboBox.MultiSelectable = true;
            this.cboComboBox.Name = "cboComboBox";
            this.cboComboBox.Size = new System.Drawing.Size(169, 21);
            this.cboComboBox.TabIndex = 2;
            this.cboComboBox.ValueSeparator = ", ";
            this.cboComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbo_sample_KeyDown);
            this.cboComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbo_sample_KeyPress);
            //
            // AttributeComboBox
            //
            this.Controls.Add(this.cboComboBox);
            this.Controls.Add(this.btnAdd);
            this.Name = "AttributeComboBox";
            this.Size = new System.Drawing.Size(195, 23);
            this.Load += new System.EventHandler(this.AttributeComboBox_Load);
            this.BackColorChanged += new System.EventHandler(this.AttributeComboBox_BackColorChanged);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        //public PresentationControls.CheckBoxComboBox cboComboBox;
        public SpiderCustomComponent.CheckComboBox cboComboBox;
	}
}
