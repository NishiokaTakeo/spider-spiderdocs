using SpiderDocsForms;

namespace SpiderDocs
{
    partial class frmFavourite
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFavourite));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMessageTitle = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.propertyPanelNext = new SpiderDocsForms.PropertyPanelNext();
            this.btnClear = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblMessageTitle);
            this.panel1.Location = new System.Drawing.Point(-6, -6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 31);
            this.panel1.TabIndex = 118;
            // 
            // lblMessageTitle
            // 
            this.lblMessageTitle.AutoSize = true;
            this.lblMessageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageTitle.Location = new System.Drawing.Point(12, 10);
            this.lblMessageTitle.Name = "lblMessageTitle";
            this.lblMessageTitle.Size = new System.Drawing.Size(193, 15);
            this.lblMessageTitle.TabIndex = 4;
            this.lblMessageTitle.Text = "Create favourite default properties.";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(32, 322);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 23);
            this.btnSave.TabIndex = 1001;
            this.btnSave.Text = "    Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(133, 322);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 23);
            this.btnClose.TabIndex = 1002;
            this.btnClose.Text = "Close";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // propertyPanelNext
            // 
            this.propertyPanelNext.FormMode = SpiderDocsForms.PropertyPanelNext.en_FormMode.Multiple;
            this.propertyPanelNext.IsSameAttribute = false;
            this.propertyPanelNext.Location = new System.Drawing.Point(-1, 28);
            this.propertyPanelNext.Name = "propertyPanelNext";
            this.propertyPanelNext.Size = new System.Drawing.Size(336, 287);
            this.propertyPanelNext.TabIndex = 1003;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(234, 322);
            this.btnClear.Name = "btnClear";
            this.btnClear.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnClear.Size = new System.Drawing.Size(97, 23);
            this.btnClear.TabIndex = 1004;
            this.btnClear.Text = "Clear";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmFavourite
            // 
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(335, 349);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.propertyPanelNext);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFavourite";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Favourite Properties";
            this.Load += new System.EventHandler(this.frmFileProperties_Load);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.Label lblMessageTitle;
        private PropertyPanelNext propertyPanelNext;
        internal System.Windows.Forms.Button btnClear;
    }
}