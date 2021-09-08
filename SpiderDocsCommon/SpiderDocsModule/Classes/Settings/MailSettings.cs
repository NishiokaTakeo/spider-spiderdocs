using System;
using Spider.Data;
using Spider.Net;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
//---------------------------------------------------------------------------------
// mail server settings from database ---------------------------------------------
//---------------------------------------------------------------------------------
	public class MailSettingss
	{
		public SMTPSettings server = new SMTPSettings();
		public bool send;

//---------------------------------------------------------------------------------
		public void Load()
		{
            MailSettingss setting = Cache.MailSettingss_Load();
            this.server = setting.server;
            this.send = setting.send;

   //         // From database (email settings)
   //         SqlOperation sql = new SqlOperation("email_server", SqlOperationMode.Select);
			//sql.Fields("server", "email_account", "password", "port", "ssl", "send");
			//sql.Commit();
			//sql.Read();

			//server.ServerAddress = sql.Result("server");
			//server.User = sql.Result("email_account");
			//server.Password = sql.Result("password");
			//server.Port = sql.Result_Int("port");
			//server.SSL = Convert.ToBoolean(sql.Result("ssl"));
			//send = Convert.ToBoolean(sql.Result("send"));
		}

//---------------------------------------------------------------------------------
		public void Save()
		{
            MailSettingController.Save(this);

            // email settings
   //         SqlOperation sql = new SqlOperation("email_server", SqlOperationMode.Update);

			//sql.Field("server", server.ServerAddress);
			//sql.Field("email_account", server.User);
			//sql.Field("password", server.Password);
			//sql.Field("port", server.Port);
			//sql.Field("ssl", (server.SSL == true ? 1 : 0));
			//sql.Field("send", (send == true ? 1 : 0));

			//sql.Commit();
		}

//---------------------------------------------------------------------------------
	}

    public class MailSettingController
    {
        //---------------------------------------------------------------------------------
        static public MailSettingss Load()
        {
            // From database (email settings)
            SqlOperation sql = new SqlOperation("email_server", SqlOperationMode.Select);
            sql.Fields("server", "email_account", "password", "port", "ssl", "send");
            sql.Commit();
            sql.Read();

            MailSettingss setting = new MailSettingss();

            setting.server.ServerAddress = sql.Result("server");
            setting.server.User = sql.Result("email_account");
            setting.server.Password = sql.Result("password");
            setting.server.Port = sql.Result_Int("port");
            setting.server.SSL = Convert.ToBoolean(sql.Result("ssl"));
            setting.send = Convert.ToBoolean(sql.Result("send"));

            return setting;
        }

        //---------------------------------------------------------------------------------
        static public void Save(MailSettingss setting )
        {
            // email settings
            SqlOperation sql = new SqlOperation("email_server", SqlOperationMode.Update);

            sql.Field("server", setting.server.ServerAddress);
            sql.Field("email_account", setting.server.User);
            sql.Field("password", setting.server.Password);
            sql.Field("port", setting.server.Port);
            sql.Field("ssl", (setting.server.SSL == true ? 1 : 0));
            sql.Field("send", (setting.send == true ? 1 : 0));

            sql.Commit();
        }

        //---------------------------------------------------------------------------------
    }

}
