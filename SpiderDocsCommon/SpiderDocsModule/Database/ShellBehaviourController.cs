using System;
using System.Linq;
using System.Collections.Generic;
using Spider.Data;
using Spider.Net;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
    public class ShellBehaviourController
    {
        static string[] fields = new string[]
        {
            "id",
            "extension",
            "override_behaviour"
        };

        public static Models.ShellBehaviour Get(string extension, Models.ShellBehaviour.en_Shell behaviour)
        {
            Models.ShellBehaviour ans = new Models.ShellBehaviour();

            SqlOperation sql = new SqlOperation("shell_behaviour", SqlOperationMode.Select);

            sql.Where("extension", extension);
            sql.Where("override_behaviour", (int)behaviour);
            sql.Fields(fields);
            sql.Commit();

            while (sql.Read())
            {
                ans = new Models.ShellBehaviour()
                {
                    id = int.Parse(sql["id"]),
                    extension = sql["extension"].ToString(),
                    //default_behaviour = (Models.ShellBehaviour.en_Shell)int.Parse(sql["default_behaviour"]),
                    override_behaviour = (Models.ShellBehaviour.en_Shell)int.Parse(sql["override_behaviour"])
                };
            }

            return ans;
        }
    }
}
