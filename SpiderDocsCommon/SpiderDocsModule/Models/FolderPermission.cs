using System;
using System.Collections.Generic;

namespace SpiderDocsModule
{
    public class FolderPermission : Dictionary<en_Actions, en_FolderPermission>,ICloneable
	{    
		public int FolderId {get;set;} = 0;
        public FolderPermission() { }
        public FolderPermission(int id_folder)
        {
            FolderId = id_folder;
        }

        //public ActionPermission Permissions {get;set;} = new ActionPermission();
		public object Clone()
    	{
         	return this.MemberwiseClone();
    	}
        // public void AddPermission(en_Actions action, en_FolderPermission permission)
        // {
        //     Permissions.Add(action,permission);
        // }
	}
}


