namespace SpiderDocsForms.UserControls
{
    partial class FolderExploere
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFolderExplorer));            
            this.components = new System.ComponentModel.Container();
            this.treeViewFolderExplore = new System.Windows.Forms.TreeView();
            
            this.contextMenuStripFolderViewOption1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            
            this.contextMenuStripFolderViewOption2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripFolderViewOption2.SuspendLayout();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();

            this.imageListExplorer = new System.Windows.Forms.ImageList(this.components);

            this.contextMenuStripFolderViewOption1.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // treeViewFolderExplore
            // 




            
            this.treeViewFolderExplore.AllowDrop = true;
            this.treeViewFolderExplore.ContextMenuStrip = this.contextMenuStripFolderViewOption1;
            this.treeViewFolderExplore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFolderExplore.ImageIndex = 0;
            this.treeViewFolderExplore.ImageList = this.imageListExplorer;
            this.treeViewFolderExplore.Location = new System.Drawing.Point(0, 0);
            this.treeViewFolderExplore.Name = "treeViewFolderExplore";
            this.treeViewFolderExplore.SelectedImageIndex = 0;
            this.treeViewFolderExplore.Size = new System.Drawing.Size(300, 500);
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
            //this.imageListExplorer.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListExplorer.ImageStream")));
            //this.imageListExplorer.TransparentColor = System.Drawing.Color.Transparent;
            //this.imageListExplorer.Images.SetKeyName(0, "folder.png");
            //this.imageListExplorer.Images.SetKeyName(1, "folder.png");
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
            // FolderExploere
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeViewFolderExplore);
            this.Name = "FolderExploere";
            this.Size = new System.Drawing.Size(300, 500);
            this.contextMenuStripFolderViewOption1.ResumeLayout(false);
            this.contextMenuStripFolderViewOption2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TreeView treeViewFolderExplore;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFolderViewOption1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;

        private System.Windows.Forms.ContextMenuStrip contextMenuStripFolderViewOption2;
        private System.Windows.Forms.ImageList imageListExplorer;
    }
}
