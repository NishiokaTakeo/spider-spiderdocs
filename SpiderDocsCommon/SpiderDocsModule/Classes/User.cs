using System;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class User:ICloneable
	{
		Dictionary<int, string> MenuPermissionGroupNames;

		public int id { get; set; }
		public string login { get; set; }
		public string name { get; set; }
		public int id_permission { get; set; }
		public string email { get; set; }
		public bool reviewer { get; set; }
		public bool active { get; set; }
		public string password { get; set; }
		public string name_computer { get; set; }

		public string permission_str
		{
			get
			{
				string ans = "";

				if(MenuPermissionGroupNames == null)
					MenuPermissionGroupNames = PermissionController.GetMenuPermissionGroupNames();

				if(MenuPermissionGroupNames.ContainsKey(id_permission))
					ans = MenuPermissionGroupNames[id_permission];

				return ans;
			}
		}

//---------------------------------------------------------------------------------
		public User()
		{
			id = 0;
			name = "";
			name_computer = "";
		}
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //---------------------------------------------------------------------------------
    }
}
