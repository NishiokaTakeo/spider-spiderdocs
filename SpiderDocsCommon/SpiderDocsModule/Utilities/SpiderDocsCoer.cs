using System;
using Spider.Data;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class SpiderDocsCoer
	{
//---------------------------------------------------------------------------------
		public static string getSystemVersion()
		{
			string ans = "";

			SqlOperation sql = new SqlOperation("system_version", SqlOperationMode.Select);
			sql.Field("client_version");
			sql.Commit();

			while(sql.Read())
				ans = sql.Result("client_version");

			return ans;
		}

//---------------------------------------------------------------------------------
		public static int getIntSystemVersion()
		{
			return int.Parse(getSystemVersion().Replace(".", ""));
		}

//---------------------------------------------------------------------------------
		public static void updateClientVersion(string version)
		{
			SqlOperation sql = new SqlOperation("system_version", SqlOperationMode.Update);
			sql.Field("client_version", version);
			sql.Commit();
		}

//---------------------------------------------------------------------------------
		public static Array getUpdateFile()
		{
			SqlOperation sql = new SqlOperation("system_updates", SqlOperationMode.Select);
			sql.Where("version", SpiderDocsCoer.getSystemVersion());
			sql.OrderBy("date", SqlOperation.en_order_by.Descent);
			sql.Field("file_data");
			sql.SetMaxResult(1);

			sql.Commit();

			byte[] fileData = null;

			if(sql.Read())
				fileData = (byte[])sql.Result_Obj("file_data");

			return fileData;
		}

		/// <summary>
		/// Check if individual computer needs to update.
		/// </summary>
		/// <param name="nameComputer">Yes: true, otherwise No</param>
		/// <returns></returns>
		public static bool GetIndividualUpdateIsNecesarry(string nameComputer)
		{
			string ans = string.Empty;

			SqlOperation sql = new SqlOperation("system_updates_individual", SqlOperationMode.Select);
			sql.Where("name_computer", nameComputer );
			sql.Field("name_computer");
			sql.SetMaxResult(1);

			sql.Commit();

			if(sql.Read())
			{
				ans = sql.Result("name_computer");
			}

			return false == string.IsNullOrWhiteSpace(ans);
		}

		public static bool DeleteIndividualUpdateIsNecesarry(string nameComputer)
		{
			string ans = string.Empty;

			SqlOperation sql = new SqlOperation("system_updates_individual", SqlOperationMode.Delete);
			sql.Where("name_computer", nameComputer );

			sql.Commit();

			return true;
		}

//---------------------------------------------------------------------------------
	}
}
