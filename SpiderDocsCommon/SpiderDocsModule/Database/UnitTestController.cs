using System;
using System.Linq;
using System.Collections.Generic;
using Spider.Data;
using NLog;
//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class UnitTestController
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        public static bool IsTestDatabase()
        {
            SqlOperation sql = new SqlOperation("unit_test", SqlOperationMode.Select);
            sql.Field("debug");

            sql.Commit();

            bool isDebug = false;
            while (sql.Read())
            {
                isDebug = sql.Result<bool>("debug");
            }

            return isDebug;
        }

    }
}
