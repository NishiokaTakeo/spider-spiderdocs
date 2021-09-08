using System;
using System.Linq;
using System.Collections.Generic;

namespace SpiderDocsModule
{
	public class TopMenuItemController : BaseController
	{
		static TableInformation main_table = new TableInformation(
			"system_menu",
			new string[]
			{
				"menu",
				"menu_internal_name"
			}
		);

		static TableInformation sub_table = new TableInformation(
			"system_submenu",
			new string[]
			{
				"submenu",
				"submenu_internal_name",
				"id_menu",
				"id_order"
			}
		);

		public static List<TopMenuItem> GetMenu()
		{
			List<TopMenuItem> main_menu = new List<TopMenuItem>();
			SqlOperation sql;

			sql = Get(main_table);
			while(sql.Read())
			{
				TopMenuItem wrk = new TopMenuItem();

				wrk.id = sql.Result<int>(main_table.tb_id_field);
				wrk.name = sql.Result("menu");
				wrk.internal_name = sql.Result("menu_internal_name");

				main_menu.Add(wrk);
			}

			sql = Get(sub_table);
			while(sql.Read())
			{
				TopMenuItem parent = main_menu.Find(a => a.id == sql.Result<int>("id_menu"));
				if(parent != null)
				{
					TopMenuItem wrk = new TopMenuItem();

					wrk.id = sql.Result<int>(main_table.tb_id_field);
					wrk.name = sql.Result("submenu");
					wrk.internal_name = sql.Result("submenu_internal_name");
					wrk.order = sql.Result<int>("id_order");

					parent.SubItems.Add(wrk);
				}
			}

			foreach(TopMenuItem menu in main_menu)
				menu.SubItems = menu.SubItems.OrderBy(a => a.order).ToList();

			return main_menu;
		}
	}
}
