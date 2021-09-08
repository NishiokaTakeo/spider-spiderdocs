using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using SpiderDocsModule;
using lib = SpiderDocsModule.Library;


namespace SpiderDocsForms.UserControls
{
    public partial class FolderExploere : UserControl
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        int _defaultWidth = 0, _defaultHeight = 0;
        int _treeDefWdith = 0, _treeDefHeigh = 0;
        TreeNode lastSelected = null;
        bool isFolderExpanding = false;
        public bool Editable = false;
        public List<Folder> FolderSouce { get; set; } = new List<Folder>();
        public Folder SelectedFolder { get; set; } = new Folder();

        List<TreeNode> AllNode { get; set; } = new List<TreeNode>();
        public en_Actions FilterPermission = en_Actions.None;

        TreeNode lastDragDestination = null;
        DateTime lastDragDestinationTime;
        public delegate void FolderHanlder(object sender, FolderEventArgs e);



        public class FolderEventArgs : EventArgs
        {

            public enum Proc
            {
                NONE = 0,
                ALL = 1,
                WITHOU_FOLDER = 2,
                FOLDER = 3
            }

            public Proc Instruction
            {
                get; set;
            }
            public FolderEventArgs(FolderEventArgs.Proc proc)
            {
                Instruction = proc;
            }
        }

        enum DragBy
        {
            TreeView = 1,
            GridView = 2,
            Explore = 3,
            Unknown = 99
        }

        public FolderExploere()
        {
            InitializeComponent();
        }

        public FolderExploere(int default_id_folder, List<Folder> source, bool editable = false)
        {
            InitializeComponent();

            //Editable = editable;

            //treeViewFolderExplore.BeginUpdate();
            //FolderSouce = source;

            //_defaultWidth = Width;
            //_defaultHeight = Height;
            //_treeDefWdith = this.treeViewFolderExplore.Width;
            //_treeDefHeigh = this.treeViewFolderExplore.Height;

            //loadFolderTreeView(default_id_folder);

            //treeViewFolderExplore.EndUpdate();
        }


        void loadFolderTreeView(int id_default = 0)
        {
            try
            {
                if (!Editable) this.treeViewFolderExplore.ContextMenuStrip = null;

                AddParents(id_default);

                List<Folder> Folders = FolderSouce.OrderBy(x => x.id_parent).ThenByDescending(x => x.document_folder).ToList();

                // Nothing to do if you cannot see any folders
                if (Folders.Count == 0) return;

                treeViewFolderExplore.Nodes.Clear();

                // Folders Level
                List<Folder> roots = ChildrenBy(Folders.Where(x => x.id > 0).ToList(), 0);

                foreach (Folder root in roots)
                {
                    TreeNode node = new TreeNode(root.document_folder) { Tag = root, Name = root.id.ToString() };

                    List<Folder> children = ChildrenBy(Folders, root.id);
                    if (Editable) node.ContextMenuStrip = contextMenuStripFolderViewOption2;
                    AddChildFolderNodes(node, children, Folders, Editable);

                    treeViewFolderExplore.Nodes.Add(node);
                    AllNode.Add(node);
                }

                Select(id_default);

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error");
            }
        }

        void AddParents(int id_default)
        {
            if (!FolderSouce.Exists(x => x.id == id_default))
            {
                List<Folder> ancestorFolders = PermissionController.drillUpfoldersby(id_default, SpiderDocsApplication.CurrentUserId);

                FolderSouce.AddRange(ancestorFolders.Where(x => x.id_parent != 0));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="db">folders for searching. must be sorted</param>
        /// <param name="id">the id for parent</param>
        /// <returns></returns>
        List<Folder> ChildrenBy(List<Folder> db, int id)
        {
            List<Folder> ans = new List<Folder>();

            int idx = db.FindLastIndex(x => x.id_parent == id);

            if (idx == -1) return new List<Folder>();

            for (int i = idx; i >= 0; i--)
            {
                if (db[i].id_parent != id)
                    break;

                ans.Add(db[i]);

                db.RemoveAt(i);
            }
            return ans;
        }

        public void Select(int id_folder)
        {
            //List<TreeNode> list = new List<TreeNode>();

            //foreach (TreeNode folder in treeViewFolderExplore.Nodes)
            //    PrintNodesRecursive(folder, list);

            TreeNode[] nodes = treeViewFolderExplore.Nodes.Find(id_folder.ToString(), true);
            treeViewFolderExplore.SelectedNode = nodes[0];

            //bool found = false;
            //foreach ( TreeNode folder in list)
            //{
            //    if ( (folder.Tag as Folder).id == id_folder )
            //    {

            //        treeViewFolderExplore.SelectedNode = null;
            //        treeViewFolderExplore.SelectedNode = folder;

            //        folder.Expand();
            //        found = true;
            //        break;
            //    }
            //}

            //if (!found)
            //{

            //}
        }

        //public void PrintNodesRecursive(TreeNode oParentNode, List<TreeNode> list)
        //{
        //    foreach (TreeNode oSubNode in oParentNode.Nodes)
        //    {
        //        list.Add(oSubNode);

        //        PrintNodesRecursive(oSubNode,list);
        //    }
        //}

        void AddChildFolderNodes(TreeNode node, List<Folder> children, List<Folder> database, bool enabletooltips = true)
        {
            children.ForEach(fld => {

                string name_folder = fld.document_folder;

                if (UserGlobalSettings.IsDevelopper) name_folder += string.Format(" ({0})", fld.id);

                TreeNode me = new TreeNode(fld.document_folder) { Tag = fld, Name = fld.id.ToString() };
                //me.Tag = fld;

                if (enabletooltips) me.ContextMenuStrip = contextMenuStripFolderViewOption2;
                node.Nodes.Add(me);
                AllNode.Add(me);

                // add child
                if (fld.id_parent > 0)
                {
                    List<Folder> _children = ChildrenBy(database, fld.id);

                    if (_children.Count > 0)
                        AddChildFolderNodes(me, _children, database, enabletooltips);
                }
            });
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // return if not selected
            if (treeViewFolderExplore.SelectedNode == null) { SelectedFolder = new Folder(); return; }

            SelectedFolder = treeViewFolderExplore.SelectedNode.Tag as Folder;

            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SelectedFolder = new Folder();

            this.Hide();
        }

        private void treeViewFolderExplore_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lastSelected = treeViewFolderExplore.SelectedNode;
        }

        private void treeViewFolderExplore_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (isFolderExpanding) return;

            isFolderExpanding = true;
            try
            {
                FillChildrenAt(e.Node);
                e.Node.Expand();
            }
            catch { }
            finally
            {
                isFolderExpanding = false;
            }

        }

        private void treeViewFolderExplore_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            if (isFolderExpanding) return;

            isFolderExpanding = true;
            try
            {

                FillChildrenAt(e.Node);

                e.Node.Expand();
            }
            catch { }
            finally
            {
                isFolderExpanding = false;
            }
        }

        void FillChildrenAt(TreeNode node)
        {
            Folder root = (node.Tag as Folder);
            List<Folder> folders = PermissionController.GetAssignedFolderLevel1(root.id, SpiderDocsApplication.CurrentUserId).OrderBy(x => x.id_parent).ThenByDescending(x => x.document_folder).ToList();

            folders.ForEach(x =>
            {
                if (FolderSouce.FindIndex(y => y.id == x.id) == -1) FolderSouce.Add(x);
            });

            node.Nodes.Clear();

            List<Folder> children = ChildrenBy(folders, root.id);
            AddChildFolderNodes(node, children, folders, Editable);

        }


        private void frmFolderExplorer_ResizeEnd(object sender, EventArgs e)
        {
            int changedWidth = this.Width - _defaultWidth;
            int changedHeight = this.Height - _defaultHeight;

            if (changedWidth > 0)
                treeViewFolderExplore.Width = _treeDefWdith + changedWidth;
            else
            {
                treeViewFolderExplore.Width = _treeDefWdith;
                Width = _defaultWidth;
            }

            if (changedHeight > 0)
                treeViewFolderExplore.Height = _treeDefHeigh + changedHeight;
            else
            {
                treeViewFolderExplore.Height = _treeDefHeigh;
                Height = _defaultHeight;
            }
        }

        #region TreeView

        /// <summary>
        /// When Dragged Node is droped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewFolderExplore_DragDrop(object sender, DragEventArgs e)
        {
            logger.Trace("Begin");

            if (!Editable) return;

            // Retrieve the client coordinates of the drop location.
            Point targetPoint = treeViewFolderExplore.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNode targetNode = treeViewFolderExplore.GetNodeAt(targetPoint);

            DragBy by = DetermineDragBy(e);

            switch (by)
            {
                case DragBy.TreeView:
                    Drop4TreeView(targetNode, e);   //move folder
                    break;

                case DragBy.GridView:
                    //Drop4GridView(targetNode, e);   //move document
                    break;

                case DragBy.Explore:
                    //Drop4Explore(targetNode, e);    //move document
                    break;
            }
        }

        /// <summary>
        /// When Drag Over
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewFolderExplore_DragOver(object sender, DragEventArgs e)
        {
            logger.Trace("Begin");

            if (!Editable) return;

            // Retrieve the client coordinates of the mouse position.
            Point targetPoint = treeViewFolderExplore.PointToClient(new Point(e.X, e.Y));

            // Select the node at the mouse position.
            TreeNode destinationNode = treeViewFolderExplore.GetNodeAt(targetPoint);

            if (destinationNode == null) return;

            //if we are on a new object, reset our timer
            //otherwise check to see if enough time has passed and expand the destination node
            if (destinationNode != lastDragDestination)
            {
                if (null != lastDragDestination) lastDragDestination.ForeColor = treeViewFolderExplore.ForeColor;    // restore gray out to normal

                lastDragDestination = destinationNode;
                lastDragDestinationTime = DateTime.Now;
            }
            else
            {
                TimeSpan hoverTime = DateTime.Now.Subtract(lastDragDestinationTime);
                if (hoverTime.TotalMilliseconds > 500)
                {
                    //treeViewFolderExplore.SelectedNode = destinationNode;
                    destinationNode.ForeColor = Color.Gray; // gray out on draging over node

                    destinationNode.Expand();
                }
            }
        }
        /// <summary>
        /// When Drag is end
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewFolderExplore_DragLeave(object sender, EventArgs e)
        {
            if (!Editable) return;

            if (null != lastDragDestination) lastDragDestination.ForeColor = treeViewFolderExplore.ForeColor;    // restore gray out to normal
            //Exp2LastSelected.Clear();
        }

        DragBy DetermineDragBy(DragEventArgs e)
        {
            logger.Trace("Begin");
            try
            {
                // GridView when dragged is Document Object
                if (e.Data.GetType() == typeof(DocumentDataObject)) return DragBy.GridView;

                // Explore when dragged has at the least one system file.
                try
                {
                    string[] dragged = (string[])(e.Data.GetData(DataFormats.FileDrop)); // dragged files
                    if (FileFolder.GetFilesByPath(dragged).Count > 0) return DragBy.Explore;
                }
                catch { }

                // TreeView when dragged is TreeNode
                try
                {
                    if (((TreeNode)e.Data.GetData(typeof(TreeNode))).GetType() == typeof(TreeNode)) return DragBy.TreeView;
                }
                catch { }

            }
            catch
            {
                return DragBy.Unknown;
            }
            return DragBy.Unknown;
        }

        void Drop4TreeView(TreeNode targetNode, DragEventArgs e)
        {
            logger.Trace("Begin");

            // Retrieve the node that was dragged.
            TreeNode draggedNode;
            try
            {
                draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            }
            catch { return; }

            // set 0 if null
            if (targetNode == null)
            {
                // Here is update Folder ID
                Folder fld = (draggedNode.Tag as Folder);

                if (!FolderController.IsUniqueFolderName(fld.document_folder, 0))
                {
                    MessageBox.Show(lib.msg_existing_folder, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                draggedNode.Remove();
                treeViewFolderExplore.Nodes.Insert(0, draggedNode);

                FolderController.UpdateParent(fld.id, 0);
                fld.id_parent = 0;

                return;
            }
            // Confirm that the node at the drop location is not
            // the dragged node or a descendant of the dragged node.
            if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
            {
                // If it is a move operation, remove the node from its current
                // location and add it to the node at the drop location.
                if (e.Effect == DragDropEffects.Move)
                {
                    // Here is update Folder ID
                    Folder chld = (draggedNode.Tag as Folder)
                        , prnt = (targetNode.Tag as Folder);

                    if (!FolderController.IsUniqueFolderName(chld.document_folder, prnt.id))
                    {
                        MessageBox.Show(lib.msg_existing_folder, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);


                    FolderController.UpdateParent(chld.id, prnt.id);
                    chld.id_parent = prnt.id;

                    //frmWorkSpace_OnFolderChanged(this, new FolderEventArgs(FolderEventArgs.Proc.WITHOU_FOLDER));
                }

                // Expand the node at the location
                // to show the dropped node.
                //targetNode.Expand();
            }
        }

        /// <summary>
        /// Begining of Drag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewFolderExplore_DragEnter(object sender, DragEventArgs e)
        {
            logger.Trace("Begin");

            if (!Editable) return;

            e.Effect = e.AllowedEffect;
        }


        /// <summary>
        /// When Create at root
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            logger.Trace("Begin");
            CreateFolderAtTreeView();
        }

        /// <summary>
        /// Create Folder.
        /// </summary>
        /// <param name="slctNode">it will be parent</param>
        void CreateFolderAtTreeView(TreeNode slctNode = null)
        {
            logger.Trace("Begin");
            SpiderDocs.Forms.WorkSpace.frmEnterText frm = new SpiderDocs.Forms.WorkSpace.frmEnterText();
            frm.StartPosition = FormStartPosition.CenterParent;

            frm.ShowDialog(this);

            string name = frm.txtText.Text;

            if (string.IsNullOrWhiteSpace(name)) return;

            frm.Dispose();

            // Folder trying to be created. id_parent will be set if stctNode argument is passed.
            Folder fld = new Folder(0, name, slctNode == null ? 0 : (slctNode.Tag as Folder).id);

            if (!FolderController.IsUniqueFolderName(name, fld.id_parent))
            {
                MessageBox.Show(lib.msg_existing_folder, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Add Folder
            int fld_id = FolderController.Save(fld);

            TreeNode node = new TreeNode(fld.document_folder);
            node.Tag = fld;
            if (Editable) node.ContextMenuStrip = contextMenuStripFolderViewOption2;

            // Add Permission
            // AddPermission(fld_id);

            // Add Node
            if (slctNode != null)
                slctNode.Nodes.Add(node);
            else
                treeViewFolderExplore.Nodes.Add(node);

            if (node.Parent != null) OpenTreeViewAt(node.Parent);

            //frmWorkSpace_OnFolderChanged(this, new FolderEventArgs(FolderEventArgs.Proc.NONE));
        }

        /// <summary>
        /// When Folder rename is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            logger.Trace("Begin");

            RenameFolderAtTreeView(treeViewFolderExplore.SelectedNode);
        }

        void RenameFolderAtTreeView(TreeNode slctNode)
        {
            if (slctNode == null) return;

            Folder crntFolder = slctNode.Tag as Folder;

            SpiderDocs.Forms.WorkSpace.frmEnterText frm = new SpiderDocs.Forms.WorkSpace.frmEnterText();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.txtText.Text = crntFolder.document_folder;
            frm.ShowDialog(this);

            string name = frm.txtText.Text;

            frm.Dispose();

            // No change
            if (crntFolder.document_folder == name) return;


            //  Error if name is empty
            if (string.IsNullOrWhiteSpace(name))
            {
                // TODO: Error Message
                MessageBox.Show(lib.msg_required_folder_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            };

            // Update
            Folder fld = slctNode.Tag as Folder;

            // If change name ( case insensitive )
            // And that name is assigned another folder already
            if (crntFolder.document_folder.ToLower() != name.ToLower()
                    && !FolderController.IsUniqueFolderName(name, fld.id_parent))
            {
                MessageBox.Show(lib.msg_existing_folder, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Finally, update new name
            fld.document_folder = slctNode.Text = name;

            // Add Folder
            FolderController.Save(fld);

            //frmWorkSpace_OnFolderChanged(this, new FolderEventArgs(FolderEventArgs.Proc.WITHOU_FOLDER));

        }

        /// <summary>
        /// When Add clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            logger.Trace("Begin");
            CreateFolderAtTreeView(treeViewFolderExplore.SelectedNode);
            treeViewFolderExplore.SelectedNode?.Expand();
        }

        /// <summary>
        /// When remove is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            logger.Trace("Begin");

            DeleteFolderAtTreeView(treeViewFolderExplore.SelectedNode);
        }

        void DeleteFolderAtTreeView(TreeNode slctNode)
        {
            if (slctNode == null) return;

            Folder crntFolder = (slctNode.Tag as Folder);

            if (crntFolder.id == 0) return;

            // All folders can be removed ?
            List<Folder> full = FolderController.DrillDownFoldersBy(crntFolder.id);
            if (!PermissionController.hasFoldersPermissionByUser(full.Select(x => x.id).ToArray(), SpiderDocsApplication.CurrentUserId, en_Actions.Delete))
            {
                logger.Warn("{0} doesn't have permission for {1} on {2},{3}",SpiderDocsApplication.CurrentUserId,en_Actions.Delete,string.Join(",",full.Select(x => x.id).ToArray()),crntFolder.id);
                MessageBox.Show(lib.msg_permission_denied, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }

            DialogResult result = (MessageBox.Show(lib.msg_ask_delete_folder, lib.msg_messabox_title, MessageBoxButtons.YesNo, MessageBoxIcon.Question));
            if (result == DialogResult.Yes)
            {
                // Remove the folder
                List<TreeNode> nodes = GetNodesBy(slctNode);
                nodes.Add(slctNode);
                nodes.Reverse();
                slctNode.Remove();
                treeViewFolderExplore.SelectedNode = null; // Unselect removed folder.

                BackgroundWorker thRmDir = new BackgroundWorker();
                thRmDir.DoWork += (object sender, DoWorkEventArgs e) => RemoveFolder(nodes);
                thRmDir.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
                {
                    //search(dtgBd_SearchMode.RecentDocuments);

                    //frmWorkSpace_OnFolderChanged(this, new FolderEventArgs(FolderEventArgs.Proc.WITHOU_FOLDER));

                    MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);

                };

                thRmDir.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Get all child nodes
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        List<TreeNode> GetNodesBy(TreeNode nodes)
        {
            List<TreeNode> children = new List<TreeNode>();
            List<TreeNode> current = new List<TreeNode>();
            foreach (TreeNode node in nodes.Nodes)
            {
                //children.AddRange(GetNodesBy(node));
                if (node.Nodes.Count > 0) children.AddRange(GetNodesBy(node));

                current.Add(node);
            }

            children.AddRange(current);

            return children;
        }
        void RemoveFolder(List<TreeNode> nodes)
        {
            foreach (TreeNode node in nodes)
            {
                Folder fld = node.Tag as Folder;
                if (RemoveFolder(fld.id)) { /*node.Remove(); */}
            }

        }

        bool RemoveFolder(int id_folder)
        {
            logger.Trace("Begin");

            bool removed = true;
            // Do nothing if you don't have permission to execute
            bool hasPermission = PermissionController.CheckPermission(id_folder, en_Actions.Delete);
            if (!hasPermission) return false;


            // Delete Files under the folder
            List<Document> docs = DocumentController.GetBy(id_folder: id_folder);
            for (int i = docs.Count - 1; i >= 0; i--)
            {
                Document doc = docs[i];

                bool ok = doc.cancelCheckOut();

                if (ok)
                {
                    logger.Info("Document is removed: {0}", doc.id);
                    if (logger.IsDebugEnabled) logger.Debug("{0}", Newtonsoft.Json.JsonConvert.SerializeObject(doc));

                    doc.Remove("Deleted by Folder Exploere", SpiderDocsApplication.CurrentUserId);
                }
                else
                {
                    removed = false;
                }
            }

            // Remove the folder
            FolderController.Delete(id_folder);

            return removed;
        }

        ///// <summary>
        ///// Grant Full Permission to folder
        ///// </summary>
        ///// <param name="folder_id"></param>
        //void AddPermission(int folder_id)
        //{
        //    logger.Trace("Begin");
        //    PermissionController.AssignFolder(en_FolderPermissionMode.Group, folder_id, Group.ALL);

        //    en_FolderPermissionMode mode = en_FolderPermissionMode.Group;

        //    Dictionary<en_Actions, en_FolderPermission> permissions = new Dictionary<en_Actions, en_FolderPermission>();

        //    foreach (en_Actions actn in Enum.GetValues(typeof(en_Actions)))
        //    {
        //        permissions.Add((en_Actions)actn, en_FolderPermission.Allow);
        //    }

        //    PermissionController.AddPermission(folder_id, Group.ALL, mode, permissions);

        //}


        void OpenTreeViewAt(TreeNode node)
        {
            Folder root = (node.Tag as Folder);

            List<Folder> folders = PermissionController.GetAssignedFolderLevel1(root.id, SpiderDocsApplication.CurrentUserId, FilterPermission).OrderBy(x => x.id_parent).ThenByDescending(x => x.document_folder).ToList();

            node.Nodes.Clear();

            List<Folder> children = ChildrenBy(folders, root.id);
            AddChildFolderNodes(node, children, folders, Editable);

            node.Expand();
        }


        /// <summary>
        /// Determine whether one node is a parent
        /// or ancestor of a second node.
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns></returns>
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            logger.Trace("Begin");
            // Check the parent node of the second node.
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node,
            // call the ContainsNode method recursively using the parent of
            // the second node.
            return ContainsNode(node1, node2.Parent);
        }


        private void treeViewFolderExplore_MouseDown(object sender, MouseEventArgs e)
        {
            logger.Trace("Begin");

            //btnFolderCreate.Enabled = true;
            //btnFolderDelete.Enabled = true;
            //btnFolderRename.Enabled = true;

            // If you click on the root layer then true
            TreeNode node = treeViewFolderExplore.GetNodeAt(treeViewFolderExplore.PointToClient(Cursor.Position));
            treeViewFolderExplore.SelectedNode = node;

            if (node == null)
            {
                // You can only create a folder
                //btnFolderDelete.Enabled = false;
                //btnFolderRename.Enabled = false;

                // Deselect the node
                if (treeViewFolderExplore.SelectedNode == null) return;

                treeViewFolderExplore.SelectedNode.ForeColor = treeViewFolderExplore.ForeColor;
                treeViewFolderExplore.SelectedNode.BackColor = treeViewFolderExplore.BackColor;
                treeViewFolderExplore.SelectedNode = null;
            }
        }
        /// <summary>
        /// Begining of Drag Folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewFolderExplore_ItemDrag(object sender, ItemDragEventArgs e)
        {
            logger.Trace("Begin");
            if (!Editable) return;

            // Move the dragged node when the left mouse button is used.
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }


        private void TreeViewFolderExplore_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            var node = e.Node;
            Folder root = (node.Tag as Folder);

            List<Folder> folders = PermissionController.GetAssignedFolderLevel1(root.id, SpiderDocsApplication.CurrentUserId, FilterPermission).OrderBy(x => x.id_parent).ThenByDescending(x => x.document_folder).ToList();

            node.Nodes.Clear();

            List<Folder> children = ChildrenBy(folders, root.id);
            AddChildFolderNodes(node, children, folders, Editable);


            //// TreeNode node = e.Node;
            //// Folder root = (node.Tag as Folder);
            //// List<Folder> folders = PermissionController.GetAssignedFolderLevel1(root.id, SpiderDocsApplication.CurrentUserId, (int)en_Actions.CheckIn_Out).OrderBy(x => x.id_parent).ThenByDescending(x => x.document_folder).ToList();

            //// node.Nodes.Clear();

            //// List<Folder> children = ChildrenBy(folders, root.id);
            //// AddChildFolderNodes(node, children, folders);
            //// //await Task.Run(() => { AddChildFolderNodes(node, children, folders); });

            //if (!isFolderExpanding) treeViewFolderExplore_NodeMouseDoubleClick(null, new TreeNodeMouseClickEventArgs(e.Node,new MouseButtons(),0,0,0));

        }


        #endregion
    }
}
