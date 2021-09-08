using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using lib = SpiderDocsModule.Library;
using SpiderDocsModule;
using Spider.Forms;
using System.Linq;

namespace SpiderDocsForms
{
    public class PropertyPanelBase : Spider.Forms.UserControlBase
    {
        public TextBox tmp_txtTitle = new TextBox();
        public FolderComboBox tmp_cboFolder = new FolderComboBox();
        public ComboBox tmp_cboDocType = new ComboBox();
        public SpiderCustomComponent.CheckComboBox tmp_cboNotificationGroup = new SpiderCustomComponent.CheckComboBox();
        public AttributeSearch tmp_uscAttribute = new AttributeSearch();
        //bool initializing = false;

        public PropertyPanelBase()
        {
            tmp_txtTitle.Visible = false;
        }
        public en_Actions FolderFilterPermission { get; set; }
        
        public void Initialize(int id_doctype = 0, int id_folder = 0, int[] ids_ngroup = null)
        {
            tmp_cboDocType.DisplayMember = "type";
            tmp_cboDocType.ValueMember = "id";
            //populate combo folders
            tmp_cboFolder.DisplayMember = "document_folder";
            tmp_cboFolder.ValueMember = "id";

            //tmp_cboNotificationGroup.DisplayMember = "group_name";
            //tmp_cboNotificationGroup.ValueMember = "id";

            _UpdateDocTypeList(id_doctype);
            _UpdateFolderList(id_folder, FolderFilterPermission);
            _UpdateNotificationList(ids_ngroup);

            tmp_cboDocType.SelectedIndexChanged += cboDocType_SelectedIndexChanged;
            tmp_cboFolder.SelectedIndexChanged += new System.EventHandler(cboFolder_SelectedIndexChanged);

        }

		public void UpdateFolderList(int id_folder)
		{
			_UpdateFolderList(id_folder);

			cboFolder_SelectedIndexChanged();
		}

		void _UpdateFolderList(int id_folder, en_Actions permission = 0)
        {

            tmp_cboFolder.UseDataSource4AssignedMe(id_folder, permission);

            var folders = (List<Folder>)tmp_cboFolder.DataSource;
            if ((0 < id_folder) && folders.Exists(a => a.id == id_folder))
                tmp_cboFolder.SelectedValue = id_folder;
            else
                tmp_cboFolder.SelectedIndex = 0;
            /*
            if(id_folder > 0 )
                tmp_cboFolder.SelectedValue = id_folder;
            */

            tmp_uscAttribute.FolderId = (int)tmp_cboFolder.SelectedValue;        
		}

        void _UpdateNotificationList(int[] id_groups)
        {

            List<NotificationGroup> group = NotificationGroupController.GetGroups();
            int[] ids = group.Select(x => x.id).ToArray();

            tmp_cboNotificationGroup.Clear();

            DTS_NotificationGroup DA_NotifiationGroup = new DTS_NotificationGroup();
            DA_NotifiationGroup.Select();
            var table = DA_NotifiationGroup.GetDataTable();

            foreach (System.Data.DataRow row in table.Rows)
            {
                int id = int.Parse(row["id"].ToString());
                bool selected = id_groups?.ToList().Exists(x => x == id) ?? false;

                tmp_cboNotificationGroup.AddItem(new DocumentAttributeCombo()
                {
                    id = id,
                    text = row["group_name"].ToString(),
                    Selected = selected
                }, selected);
                
            }
            //populateComboNGroup(group.Select( x => x.id).ToArray());

            //DTS_NotificationGroup DA_NotifiationGroup = new DTS_NotificationGroup();
            //DA_NotifiationGroup.Select();            
            //var table= DA_NotifiationGroup.GetDataTable();
            //var newrow = table.NewRow();
            //newrow[0] = 0; newrow[1] = "None";
            //table.Rows.InsertAt(newrow, 0);
            //tmp_cboNotificationGroup.DataSource = table;

            //if ((0 < id_groups) && group.Exists(a => a.id == id_groups))
            //    tmp_cboNotificationGroup.SelectedValue = id_groups;
            //else
            //    tmp_cboNotificationGroup.SelectedIndex = 0;

        }

        protected int UpdateDocTypeList(int id_doctype)
		{
			_UpdateDocTypeList(id_doctype);

			cboDocType_SelectedIndexChanged();

			return tmp_cboDocType.SelectedIndex;
		}
		void _UpdateDocTypeList(int id_doctype){
						DTS_DocumentType DA_DocumentType = new DTS_DocumentType(true);
			DA_DocumentType.Select();

			tmp_cboDocType.DataSource = DA_DocumentType.GetDataTable("type");

			if(0 < id_doctype)
				tmp_cboDocType.SelectedValue = id_doctype;
			else
				tmp_cboDocType.SelectedIndex = 0;

		}
		public void ClearError(object objTitle = null)
		{
			if(objTitle != null)
				FormUtilities.PutErrorColour(objTitle, false, true);

			FormUtilities.PutErrorColour(tmp_cboFolder, false, true);
			FormUtilities.PutErrorColour(tmp_cboDocType, false, true);
		}

		public bool IsAllFieldsEntered(string extension, bool local)
		{
			if(tmp_txtTitle.Visible)
				return IsAllFieldsEntered(this.tmp_txtTitle, extension, local);
			else
				return IsAllFieldsEntered(null, extension, local);
		}

		public bool IsAllFieldsEntered(object objTitle, string extension, bool local)
		{
			bool ans = true;
			ClearError(objTitle);

			//Title
			if(objTitle != null)
			{
				string title = "";

				Control control = objTitle as Control;
				if(control != null)
					title = control.Text;

				DataGridViewCell cell = objTitle as DataGridViewCell;
				if(cell != null)
					title = cell.Value.ToString();

				if(String.IsNullOrEmpty(title))
				{
					if(ans)
						MessageBox.Show(lib.msg_required_doc_title, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					FormUtilities.PutErrorColour(objTitle, ans);
					ans = false;

				}else if(!FileFolder.IsValidFileName(title))
				{
					if(ans)
						MessageBox.Show(lib.msg_wrong_file_name, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

					FormUtilities.PutErrorColour(objTitle, ans);
					ans = false;

				}else if(!local
					  && !SpiderDocsApplication.CurrentPublicSettings.allow_duplicatedName
				      //&& (0 < tmp_cboDocType.SelectedIndex)
					  && (DocumentController.IsDocumentExists(Convert.ToInt32(tmp_cboFolder.SelectedValue), title + extension)))
				{
					//Check if already exist file in the same folder with the same name
					if(ans)
						MessageBox.Show(lib.msg_existing_file, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					FormUtilities.PutErrorColour(objTitle, ans);
					ans = false;
				}
			}

			//Folder
			if(!local && ((tmp_cboFolder.SelectedValue == null) || ((int)tmp_cboFolder.SelectedValue <= 0)))
			{
				if(ans)
					MessageBox.Show(lib.msg_required_doc_folder, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				FormUtilities.PutErrorColour(tmp_cboFolder, ans);
				ans = false;
			}

			//Document type
			//if(!local && ((tmp_cboDocType.SelectedValue == null) || ((int)tmp_cboDocType.SelectedValue <= 0)))
			//{
			//	if(ans)
			//		MessageBox.Show(lib.msg_required_doc_type, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

			//	FormUtilities.PutErrorColour(tmp_cboDocType, ans);
			//	ans = false;
			//}

			//attributes
			if(!local)
			{
				bool attr_check = tmp_uscAttribute.checkMandadoryAttributes(ans);
				ans = (ans == true ? attr_check : ans);
			}

			return ans;
		}

		virtual protected void cboDocType_SelectedIndexChanged(object sender, EventArgs e)
		{
			cboDocType_SelectedIndexChanged();
		}

		void cboDocType_SelectedIndexChanged()
		{
			//if((tmp_cboDocType.SelectedValue != null) && (0 < (int)tmp_cboDocType.SelectedValue))
			if ((tmp_cboDocType.SelectedValue != null))
				tmp_uscAttribute.populateGrid((int)tmp_cboDocType.SelectedValue);
		}

		private void cboFolder_SelectedIndexChanged(object sender, EventArgs e)
		{
			cboFolder_SelectedIndexChanged();
		}

		void cboFolder_SelectedIndexChanged()
		{
			if((tmp_cboFolder.SelectedValue != null) && (0 < (int)tmp_cboFolder.SelectedValue))
				tmp_uscAttribute.FolderId = (int)tmp_cboFolder.SelectedValue;

			if((tmp_cboDocType.SelectedValue != null) && (0 < (int)tmp_cboDocType.SelectedValue))
				tmp_uscAttribute.populateGrid(tmp_uscAttribute.getAttributeValuesCopy(), (int)tmp_cboDocType.SelectedValue);
		}
	}
}
