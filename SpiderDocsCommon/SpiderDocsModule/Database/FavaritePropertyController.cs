using System;
using System.Linq;
using System.Collections.Generic;
using Spider.Data;
using NLog;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class FavaritePropertyController
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        //---------------------------------------------------------------------------------
		static readonly string[] tb_favarite_property = new string[]
		{
            "id",
            "id_user",
            "id_folder",
            "id_doc_type"
		};

        //---------------------------------------------------------------------------------
        static readonly string[] tb_favarite_property_item = new string[]
        {
            "id",
            "id_favourite_propery",
            "id_atb",
            "atb_value"
        };
        //---------------------------------------------------------------------------------
        public static int SaveFavatiteProperty(int userId, int id_folder, int id_doc_type, List<DocumentAttribute> attrs)
		{
            logger.Trace("Begin");

            int ans = 0;

            SqlOperation sql = new SqlOperation();

            try
            {
                // Delete First
                //sql.BeginTran();

                //FavariteProperty root = GetFavariteProperty(userId, sql);

                // if (root.id > 0)
                // {
                //     sql = new SqlOperation("favourite_property", SqlOperationMode.Delete);
                //     sql.Where("id_user", SpiderDocsApplication.CurrentUserId);
                //     sql.Commit();

                //     sql = new SqlOperation("favourite_property_item", SqlOperationMode.Delete);
                //     sql.Where("id_favourite_propery", root.id);

                //     sql.Commit();
                // }
                int deletedId = DeleteMyFavouriteProperty(userId);
                //if( deletedId > 0)
                //{

                    // Insert
                    sql = new SqlOperation("favourite_property", SqlOperationMode.Insert);
                    sql.Field("id_user", userId);
                    sql.Field("id_folder", id_folder);
                    sql.Field("id_doc_type", id_doc_type);

                    ans = sql.Commit<int>();

                    foreach (DocumentAttribute attr in attrs)
                    {



                        if ( attr.IsCombo())
                        {
                            foreach( var idCombo in (List<int>)attr.atbValue)
                            {
                                sql = new SqlOperation("favourite_property_item", SqlOperationMode.Insert);
                                sql.Field("id_favourite_propery", ans);
                                sql.Field("id_atb", attr.id);
                                sql.Field("atb_value", idCombo.ToString());
                                sql.Commit();
                            }
                        }
                        else
                        {
                            sql = new SqlOperation("favourite_property_item", SqlOperationMode.Insert);
                            sql.Field("id_favourite_propery", ans);
                            sql.Field("id_atb", attr.id);
                            sql.Field("atb_value", attr.atbValue.ToString());
                            sql.Commit();
                        }
                    }

                //}

                //sql.CommitTran();
            }catch(Exception ex)
            {
                logger.Error(ex);
                sql.RollBack();
            }

            return ans;
        }

        public static int DeleteMyFavouriteProperty (int userId)
		{
            logger.Trace("Begin");

            SqlOperation sql = new SqlOperation();
            int ans=0;
            try
            {
                // Delete First
                //sql.BeginTran();

                FavariteProperty root = GetFavariteProperty(userId, sql);

                if (root.id > 0)
                {
                    ans = root.id;

                    sql = new SqlOperation("favourite_property", SqlOperationMode.Delete);
                    sql.Where("id_user", SpiderDocsApplication.CurrentUserId);
                    sql.Commit();

                    sql = new SqlOperation("favourite_property_item", SqlOperationMode.Delete);
                    sql.Where("id_favourite_propery", root.id);

                    sql.Commit();
                }

                //sql.CommitTran();
            }catch(Exception ex)
            {
                logger.Error(ex);
                //sql.RollBack();
                return 0;
            }

            return ans;
        }

        //---------------------------------------------------------------------------------
        public static FavariteProperty GetFavariteProperty(int id_user, SqlOperation sql = null)
		{
            logger.Trace("Begin");

            FavariteProperty ans = new FavariteProperty();

            sql = sql == null ? new SqlOperation("favourite_property", SqlOperationMode.Select) : SqlOperation.GetSqlOperation(sql, "favourite_property", SqlOperationMode.Select);

            sql.Fields(tb_favarite_property);
            sql.Where("id_user", SpiderDocsApplication.CurrentUserId);
            sql.Commit();

            sql.Read();

            ans.id = sql.Result_Int("id");
            ans.id_user = sql.Result_Int("id_user");
            ans.id_folder = sql.Result_Int("id_folder");
            ans.id_doc_type = sql.Result_Int("id_doc_type");

            return ans;
        }

        //---------------------------------------------------------------------------------
        public static List<FavaritePropertyItem> GetFavaritePropertyItem(int id_favourite_propery, SqlOperation sql = null)
        {
            logger.Trace("Begin");

            List<FavaritePropertyItem> ans = new List<FavaritePropertyItem>();

            sql = sql == null ? new SqlOperation("favourite_property_item", SqlOperationMode.Select) : SqlOperation.GetSqlOperation(sql, "favourite_property_item", SqlOperationMode.Select);

            sql.Fields(tb_favarite_property_item);
            sql.Where("id_favourite_propery", id_favourite_propery);
            sql.Commit();

            while (sql.Read())
            {
                var attr = ans.FirstOrDefault(x => x.id_atb == sql.Result_Int("id_atb")) ?? new FavaritePropertyItem();
                var exists = attr.id > 0;

                attr.id = sql.Result_Int("id");
                attr.id_favourite_propery = sql.Result_Int("id_favourite_propery");
                attr.id_atb = sql.Result_Int("id_atb");
                attr.atb_value += (attr.atb_value == "" ? "": ",") + sql.Result("atb_value");

                if (!exists)
                    ans.Add(attr);
            }
            return ans;
        }
    }
}
