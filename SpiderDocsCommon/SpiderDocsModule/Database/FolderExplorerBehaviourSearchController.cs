using System;
using System.Collections.Generic;
using System.Linq;
using Spider.Data;
using Spider.Types;
using System.Windows.Forms;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	
    public class FolderExplorerBehaviourSearchController
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
//---------------------------------------------------------------------------------
		
		public static readonly string[] tb_ExplorerDblClickBehaviour = new string[]
		{
            "id",
            "id_explorer_doubleclick_behaviour",
            "id_attr",
            "value_from"
        };

//---------------------------------------------------------------------------------
// this is an ordinary function to get attribute ----------------------------------
//---------------------------------------------------------------------------------
		
        public static List<ExplorerDblClickBehaviourSearch> GetDblClickBehaviour4Search(int idExploereDblClickBehaviour)
		{
            List<ExplorerDblClickBehaviourSearch> ans = new List<ExplorerDblClickBehaviourSearch>();

			SqlOperation sql;

			sql = new SqlOperation("explorer_doubleclick_behaviour_search", SqlOperationMode.Select);
			sql.Fields(tb_ExplorerDblClickBehaviour);

			sql.Where_In("id_explorer_doubleclick_behaviour", idExploereDblClickBehaviour);

			sql.Commit();

			while(sql.Read())
			{
                ExplorerDblClickBehaviourSearch wrk = new ExplorerDblClickBehaviourSearch();

				wrk.id = Convert.ToInt32(sql.Result_Obj("id"));
				wrk.id_explorer_doubleclick_behaviour = sql.Result<int>("id_explorer_doubleclick_behaviour");
                wrk.id_attr = sql.Result<int>("id_attr");
                wrk.value_from = sql.Result<string>("value_from");

                ans.Add(wrk);
			}

			return ans;
		}

    }
}
