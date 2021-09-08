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
	
    public class FolderExplorerBehaviourController
	{
        static Logger logger = LogManager.GetCurrentClassLogger();
//---------------------------------------------------------------------------------
		
		public static readonly string[] tb_ExplorerDblClickBehaviour = new string[]
		{
            "id",
            "id_folder",
            "id_behaviour"
		};

//---------------------------------------------------------------------------------
// this is an ordinary function to get attribute ----------------------------------
//---------------------------------------------------------------------------------
		
        public static List<ExplorerDblClickBehaviour> GetDblClickBehaviour(int id_folder)
		{
            List<ExplorerDblClickBehaviour> ans = new List<ExplorerDblClickBehaviour>();

			SqlOperation sql;

			sql = new SqlOperation("explorer_doubleclick_behaviour", SqlOperationMode.Select);
			sql.Fields(tb_ExplorerDblClickBehaviour);

			sql.Where_In("id_folder", id_folder);
			//sql.Where("id_behaviour", (int)enBehaviour);

			sql.Commit();

			while(sql.Read())
			{
                ExplorerDblClickBehaviour wrk = new ExplorerDblClickBehaviour();

				wrk.id = Convert.ToInt32(sql.Result_Obj("id"));
				wrk.id_folder = sql.Result<int>("id_folder");
				wrk.id_behaviour =  (ExplorerDblClickBehaviour.en_Behaviour)Convert.ToInt32(sql.Result_Obj("id_behaviour"));

				ans.Add(wrk);
			}

			return ans;
		}

    }
}
