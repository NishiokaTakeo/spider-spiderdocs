using System;
using System.Data;
using System.Linq;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class DTS_Folder : DTS_Base
	{
//---------------------------------------------------------------------------------
		bool f_combo;
		bool f_permitted;
		int[] PermittedIds;
		 
//---------------------------------------------------------------------------------
        public DTS_Folder(bool combo, bool permitted, bool only_edit_permitted_folders = false, params int[] ids): this(SpiderDocsApplication.CurrentUserId,combo, permitted, only_edit_permitted_folders, ids)
        {
            
        }

		public DTS_Folder(int userId, bool combo, bool permitted, bool only_edit_permitted_folders = false, params int[] ids)
		{
			f_combo = combo;
			f_permitted = permitted;

			table_name = FolderController.table_name;
			fields = FolderController.fields.ToList();

			if(f_permitted)
				PermittedIds = PermissionController.GetAssignedFolderToUserCache(userId, only_edit_permitted_folders: only_edit_permitted_folders).Select(x=>x.id).ToArray();

			Select(ids);
		}

//---------------------------------------------------------------------------------
		public void Select(params int[] ids)
		{
			if(0 < ids.Length)
				ids = ids.Where(a => PermittedIds.Contains(a)).ToArray();
			else
				ids = PermittedIds;

			if(!f_permitted || (0 < ids.Length))
			{
				base.Select("id", ids);
				this.table = this.table.AsEnumerable().OrderBy(a => a["document_folder"]).CopyToDataTable();

				if(f_combo)
				{
					DataRow toInsert = this.table.NewRow();
					toInsert.ItemArray = new object[] {0, " "};
					this.table.Rows.InsertAt(toInsert, 0);
				}
			}
		}

//---------------------------------------------------------------------------------
	}
}
