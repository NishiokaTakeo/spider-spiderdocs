using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpiderDocsModule;
using NLog;
namespace SpiderDocsForms
{
    public partial class FolderComboBox : ComboBox
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        //frmFolderExplorer _explorer ;
        List<Folder> folders ;

        en_Actions _permission = en_Actions.OpenRead;

        public void _OnDataSourceChanged()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(_OnDataSourceChanged));
                return;
            }

            this.OnDataSourceChanged(new EventArgs());
        }

        public FolderComboBox()
        {
            DropDownHeight = 1;

            InitializeComponent();
        }

        public FolderComboBox(IContainer container)
        {
            DropDownHeight = 1;

            container.Add(this);

            InitializeComponent();
        }

        public void UseDataSource4AssignedMe(int id_default = 0, en_Actions permission = en_Actions.OpenRead)
        {
            _permission = permission;

            folders = PermissionController.GetAssignedFolderLevel1(0, SpiderDocsApplication.CurrentUserId, _permission);

            FillMissingFolders(id_default, ref folders );

            folders.Insert(0, new Folder(0, "", 0));

            BindSourceAsync(id_default);

        }

        /// <summary>
        /// Append missing folders by id_selected
        /// </summary>
        /// <param name="id_selected">FolderId to seek</param>
        /// <param name="source">Source to update missing folders</param>
        /// <returns>true if id_selected exists, false if it is others</returns>
        bool FillMissingFolders(int id_selected, ref List<Folder> source)
        {
            var db = source.ToList();

            List<Folder> ancestorFolders = PermissionController.drillUpfoldersby(id_selected, SpiderDocsApplication.CurrentUserId, _permission);

            if (ancestorFolders.Count() == 0) return false;

            source.AddRange(ancestorFolders.Where(x => !db.Exists(y => y.id == x.id)));

            return true;
        }

        void BindSourceAsync(int id_default = 0)
        {
            DataSource = folders;

            _OnDataSourceChanged();//this.OnDataSourceChanged(new EventArgs());

            //_explorer = _explorer ?? new frmFolderExplorer(id_default, (List<Folder>)this.DataSource);

        }


        private void Folder_MouseClick(object sender, MouseEventArgs e)
        {
            int beforeIdFolder = (SelectedValue == null || ((List<Folder>)DataSource).Find(x => x.id == (int)SelectedValue) == null) ? 0 : (int)SelectedValue;

            frmFolderExplorer explorer = new frmFolderExplorer(beforeIdFolder, ((List<Folder>)this.DataSource).ToList(), _permission);
            //explorer.DataBind();

            explorer.ShowDialog(this);

            try
            {

                int selectedId = explorer.SelectedFolder.id;

                if (selectedId == 0)
                {
                    return;
                }
                else
                {

                    UpdateDisplay(selectedId);

                }

                _OnDataSourceChanged();//this.OnDataSourceChanged(new EventArgs());

                if (beforeIdFolder != selectedId)
                {
                    RaiseOnSelectChange();
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            finally
            {

            }
        }

        public void UpdateDisplay(int selectedId)
        {
            List<Folder> sets = this.DataSource as List<Folder>;

            if (!sets.Exists(x => x.id == selectedId))
            {
                // Append missing folders
                if( FillMissingFolders(selectedId, ref sets ) == false)
                {
                    //false means it has been removed.
                    var available = sets.Where(x => x.id_parent == 0).FirstOrDefault();
                    SelectedValue = available?.id;

                    return;
                }

                this.DataSource = null;
                this.DisplayMember = "document_folder";
                this.ValueMember = "id";
                this.DataSource = sets;
            }

            SelectedValue = selectedId;
        }

        public int _selectedvalue = 0;
        public new object SelectedValue{
            get
            {
                return _selectedvalue;
            }
            set
            {
                SelectedIndex = IndexById((int)value);
                _selectedvalue = (int)value;
            }
        }

        int IndexById(int id)
        {
            int index = ((List<Folder>)DataSource).FindIndex(x => x.id == id);
            return index;
        }

        void RaiseOnSelectChange()
		{
			this.OnSelectedValueChanged(new EventArgs());
			this.OnSelectedIndexChanged(new EventArgs());
			this.OnSelectionChangeCommitted(new EventArgs());
		}
	}
}
