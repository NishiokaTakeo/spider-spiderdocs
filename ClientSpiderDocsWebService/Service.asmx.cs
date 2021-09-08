using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace ClientSpiderDocsWebService
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {


        string sql = "";
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["spiderdocs"].ConnectionString;

        public Service()
        {

        }

        [WebMethod]
        public string Activation(string clientID, string productKey, string currentVersion)
        {

            string clientName = "";

            SqlConnection sqlConn = new SqlConnection(conn);
            sqlConn.Open();

            SqlCommand sqlCommand;
            SqlDataReader dr;

            sql = " select client_name from client where id = '" + clientID + "'and product_key = '" + productKey + "'   and [enable] = 1";
            sqlCommand = new SqlCommand(sql, sqlConn);
            dr = sqlCommand.ExecuteReader();
            dr.Read();


            if(dr.HasRows)
                clientName = dr["client_name"].ToString();

            dr.Close();


            if(clientName != "")
            {
                //sql = " update client set date_activated = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "', current_version = '" + currentVersion + "' where id = '" + clientID + "'";
                sql = " update client set date_activated = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where id = '" + clientID + "'";
                sqlCommand = new SqlCommand(sql, sqlConn);
                sqlCommand.ExecuteNonQuery();
            }

            sqlConn.Close();

            return clientName;
        }

        
        [WebMethod]
        public string checkUpdate(string clientID, string currentVersion)
        {

            string data = "";

            SqlConnection sqlConn = new SqlConnection(conn);
            sqlConn.Open();

            SqlCommand sqlCommand;
            SqlDataReader dr;


            sql = " insert into client_update_check (client_id) values ('" + clientID + "')";
            sqlCommand = new SqlCommand(sql, sqlConn);
            sqlCommand.ExecuteNonQuery();

            sql = " select cu.date, u.version, u.url_download, u.url_details ";
            sql = sql + " from client_update cu ";
            sql = sql + " inner join updates u on cu.update_id = u.id";
            sql = sql + " where client_id = '" + clientID + "' and date is null";
            sql = sql + " order by  u.id desc";
            sqlCommand = new SqlCommand(sql, sqlConn);
            dr = sqlCommand.ExecuteReader();
            dr.Read();

            if(dr.HasRows)
            {
                data = "1;" + dr["version"].ToString() + ";" + dr["url_download"].ToString() + ";" + dr["url_details"].ToString();
            }
            else
            {
                data = "2";
            }

            dr.Close();
            sqlConn.Close();

            return data;
        }


        [WebMethod]
        public void downloadDone(string clientID, string currentVersion)
        {

            SqlConnection sqlConn = new SqlConnection(conn);
            sqlConn.Open();

            SqlCommand sqlCommand;

            sql = " update client_update set [date] = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "'  where client_id = '" + clientID + "' and [date] is null";
            sqlCommand = new SqlCommand(sql, sqlConn);
            sqlCommand.ExecuteNonQuery();

            SqlCommand sqlCommand1;

            sql = " update client set current_version = '" + currentVersion + "' where id = '" + clientID + "'";
            sqlCommand1 = new SqlCommand(sql, sqlConn);
            sqlCommand1.ExecuteNonQuery();

            sqlConn.Close();

        }



    }
}
