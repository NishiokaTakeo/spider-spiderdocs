namespace SpiderDocsForms
{
    partial class frmFolderExplorer
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
            if (disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFolderExplorer));
            this.treeViewFolderExplore = new MultiSelectTreeview.MultiSelectTreeview();
            this.contextMenuStripFolderViewOption1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListExplorer = new System.Windows.Forms.ImageList(this.components);
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.contextMenuStripFolderViewOption2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripFolderViewOption1.SuspendLayout();
            this.contextMenuStripFolderViewOption2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewFolderExplore
            // 
            this.treeViewFolderExplore.AllowDrop = true;
            this.treeViewFolderExplore.ContextMenuStrip = this.contextMenuStripFolderViewOption1;
            this.treeViewFolderExplore.ImageIndex = 0;
            this.treeViewFolderExplore.ImageList = this.imageListExplorer;
            this.treeViewFolderExplore.Location = new System.Drawing.Point(1, 36);
            this.treeViewFolderExplore.Name = "treeViewFolderExplore";
            this.treeViewFolderExplore.SelectedImageIndex = 0;
            this.treeViewFolderExplore.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("treeViewFolderExplore.SelectedNodes")));
            this.treeViewFolderExplore.Size = new System.Drawing.Size(246, 285);
            this.treeViewFolderExplore.TabIndex = 0;
            this.treeViewFolderExplore.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeViewFolderExplore_BeforeExpand);
            this.treeViewFolderExplore.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewFolderExplore_ItemDrag);
            this.treeViewFolderExplore.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFolderExplore_AfterSelect);
            this.treeViewFolderExplore.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewFolderExplore_NodeMouseDoubleClick);
            this.treeViewFolderExplore.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewFolderExplore_DragDrop);
            this.treeViewFolderExplore.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeViewFolderExplore_DragEnter);
            this.treeViewFolderExplore.DragOver += new System.Windows.Forms.DragEventHandler(this.treeViewFolderExplore_DragOver);
            this.treeViewFolderExplore.DragLeave += new System.EventHandler(this.treeViewFolderExplore_DragLeave);
            this.treeViewFolderExplore.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewFolderExplore_MouseDown);
            // 
            // contextMenuStripFolderViewOption1
            // 
            this.contextMenuStripFolderViewOption1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3});
            this.contextMenuStripFolderViewOption1.Name = "contextMenuStripTreeViewOptions";
            this.contextMenuStripFolderViewOption1.Size = new System.Drawing.Size(145, 26);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(144, 22);
            this.toolStripMenuItem3.Text = "Create Folder";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // imageListExplorer
            // 
            this.imageListExplorer.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListExplorer.ImageStream")));
            this.imageListExplorer.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListExplorer.Images.SetKeyName(0, "folder.png");
            this.imageListExplorer.Images.SetKeyName(1, "folder.png");
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(3, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(84, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // contextMenuStripFolderViewOption2
            // 
            this.contextMenuStripFolderViewOption2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.contextMenuStripFolderViewOption2.Name = "contextMenuStripTreeViewOptions";
            this.contextMenuStripFolderViewOption2.Size = new System.Drawing.Size(118, 70);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItem4.Text = "Rename";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItem5.Text = "Add";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItem6.Text = "Remove";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // frmFolderExplorer
            // 
            this.ClientSize = new System.Drawing.Size(247, 321);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.treeViewFolderExplore);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFolderExplorer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Folder Explorer";
            this.Shown += new System.EventHandler(this.frmFolderExplorer_Shown);
            this.ResizeEnd += new System.EventHandler(this.frmFolderExplorer_ResizeEnd);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.contextMenuStripFolderViewOption1.ResumeLayout(false);
            this.contextMenuStripFolderViewOption2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MultiSelectTreeview.MultiSelectTreeview treeViewFolderExplore;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFolderViewOption1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;

private System.Windows.Forms.ContextMenuStrip contextMenuStripFolderViewOption2;            
 private System.Windows.Forms.ImageList imageListExplorer;       
 
    }
}