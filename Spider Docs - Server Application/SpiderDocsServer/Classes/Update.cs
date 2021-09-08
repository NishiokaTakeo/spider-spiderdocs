using System;
using System.Text;
using System.Net;
using System.IO;
using System.Data.Common;
using Spider.Data;
using SpiderDocsModule;
using SpiderDocsApplication = SpiderDocsServerModule.SpiderDocsApplication;
using NLog;
//---------------------------------------------------------------------------------
namespace SpiderDocsServer
{
	public class Update
	{
		string CurrentVersionNumber;
		public bool IsNewVersionAvailable = false;
		public string NewVersionNumber;
		string Url;
		string FileName { get { return "update_" + NewVersionNumber + ".zip"; } }
		string SavePath { get { return FileFolder.GetExecutePath() + @"updates\"; } }
		string SaveFullPath { get { return SavePath + NewVersionNumber; } }
        static Logger logger = LogManager.GetCurrentClassLogger();

//---------------------------------------------------------------------------------
		public Update(string current_version)
		{
			CurrentVersionNumber = current_version;
		}

//---------------------------------------------------------------------------------
		public string GetUpdateInfo(string id)
		{
			string ans = "";
			string[] eachInfo;

			try
			{
				// connecting to spider server
				ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
				WebReference.ServiceSoapClient webservice = new WebReference.ServiceSoapClient();
				eachInfo = webservice.checkUpdate(id, CurrentVersionNumber).Split(';');

				IsNewVersionAvailable = (eachInfo[0] == "1" ? true : false);
				if(IsNewVersionAvailable)
				{
					NewVersionNumber = eachInfo[1];
					Url = eachInfo[2];
				}

			}catch(Exception error)
			{
				logger.Error(error);
				ans = error.ToString();
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public string CreateWorkDirectory()
		{
			string ans = "";

			if(FileFolder.DeleteFilesAndThisDir(SaveFullPath))
			{
				try
				{
					Directory.CreateDirectory(SaveFullPath);

				}catch(Exception error)
				{
					ans = error.Message;
				}

			}else
			{
				ans = "removing was failed.";
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public string Download(Action<long,long> update_progress)
		{
			string ans = "";
			HttpWebRequest webRequest;
			HttpWebResponse webResponse;
			Stream strResponse;
			Stream strLocal;

			using(WebClient wcDownload = new WebClient())
			{
				try
				{
					webRequest = (HttpWebRequest)WebRequest.Create(Url);
					webRequest.Credentials = CredentialCache.DefaultCredentials;
					webResponse = (HttpWebResponse)webRequest.GetResponse();
					Int64 fileSize = webResponse.ContentLength;

					strResponse = wcDownload.OpenRead(Url);
					strLocal = new FileStream(SaveFullPath + "\\" + FileName, FileMode.Create, FileAccess.Write, FileShare.None);

					Stream s = webResponse.GetResponseStream(); //Source
					MemoryStream ms = new MemoryStream((int)(webResponse as HttpWebResponse).ContentLength); //Destination

					int bytesSize = 0;
					byte[] downBuffer = new byte[2048];

					while((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
					{
						strLocal.Write(downBuffer, 0, bytesSize);

						if(update_progress != null)
							update_progress(strLocal.Length, fileSize);

						ms.Write(downBuffer, 0, bytesSize);
					}

					byte[] bytes = ms.ToArray();

					saveUpdate(bytes, NewVersionNumber);

					strResponse.Close();
					webResponse.Close();
					strLocal.Close();
				}
				catch(Exception error)
				{
					ans = error.ToString();
				}

				return ans;
			}
		}

//---------------------------------------------------------------------------------
		void saveUpdate(byte[] bytes, string  version)
		{
			try
			{
				string[] fields = new string[]
				{
					"version",
					"file_data"
				};

				object[] vals = new object[]
				{
					version,
					bytes
				};			
				
				SqlOperation sql = new SqlOperation("system_updates", SqlOperationMode.Insert);
				sql.Fields(fields, vals);
				sql.Commit();
			}
			catch(Exception error)
			{
				logger.Error(error);
			}
		}

//---------------------------------------------------------------------------------
		public string ExtructUpdate()
		{
			string ans = "";

			try
			{
				using(var zip = Ionic.Zip.ZipFile.Read(SaveFullPath + "\\" + FileName))
					zip.ExtractAll(SaveFullPath, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
			}
			catch(Exception error)
			{
				ans = error.ToString();
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public string RunSqlScript()
		{
			string ans = "";

			string BaseUrl = Url.Replace(Path.GetFileName(Url), "");

			int iCurrentVersion = int.Parse(CurrentVersionNumber.ToString().Replace(".", ""));
			int iNewVersion = int.Parse(NewVersionNumber.ToString().Replace(".", ""));

			string ScriptPath = SavePath + "script\\";
			if(!Directory.Exists(ScriptPath))
				Directory.CreateDirectory(ScriptPath);

			using (var client = new WebClient())
			{
				for(int i = iCurrentVersion + 1; i <= iNewVersion; i++)
				{
					string TargetVersion = i.ToString()[0] + "." + i.ToString()[1] + "." + i.ToString()[2];
					string filename = TargetVersion + ".sql";

					if(File.Exists(ScriptPath + filename))
					{
						//break;
						File.Delete(ScriptPath + filename);
					}

					string ScriptUrl = "";
					switch(SpiderDocsApplication.ConnectionSettings.mode)
					{
					case DbManager.en_sql_mode.ms_sql:
					default:
						ScriptUrl += new Uri(BaseUrl + "sql_server_script/" + filename);
						break;

					case DbManager.en_sql_mode.my_sql:
						ScriptUrl += new Uri(BaseUrl + "mysql_script/" + filename);
						break;				
					}	

					try
					{
						if( System.IO.File.Exists(ScriptPath + filename)) System.IO.File.Delete(ScriptPath + filename);
						
						client.DownloadFile(ScriptUrl, ScriptPath + filename);

						ans = RunSqlScript(ScriptPath + filename);

						if(!String.IsNullOrEmpty(ans))
							break;

					}catch {}
				}

				if(String.IsNullOrEmpty(ans))
				{
					//update new version number
					SqlOperation sql = new SqlOperation("system_version", SqlOperationMode.Update);
					sql.Field("client_version", NewVersionNumber);
					sql.Commit();
				}
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		string RunSqlScript(string FilePath)
		{
			string ans = "";
            
            string query = "";
            try
			{
				if(File.Exists(FilePath))
				{
					DbManager DbManager = new DbManager(
										SpiderDocsApplication.ConnectionSettings.conn, 
										SpiderDocsApplication.ConnectionSettings._mode);

					DbConnection sqlConn = DbManager.GetSqlConnection();
					DbCommand command;
					
					bool begin = false;

					//proccess all sql script
					StringBuilder strStatement = new StringBuilder(File.ReadAllText(FilePath).ToString());
					StringReader lines = new StringReader(strStatement.ToString());


                    while (lines.Peek() > -1)
					{
						string wrk = lines.ReadLine();

						if(wrk.Contains("START_SQL_SCRIPT"))
						{
							query = "";
							begin = true;

						}else if(begin)
						{
							if(wrk.Contains("END_SQL_SCRIPT"))
							{
								command = DbManager.GetSqlCommand(query, sqlConn);
								command.ExecuteNonQuery();
								begin = false;

							}else
							{
								query += (wrk + " ");
							}
						}
					}

					sqlConn.Close();
				}
			}
			catch(Exception error)
			{
				ans = query + error.ToString();
			}

			return ans;
		}

//---------------------------------------------------------------------------------
	}
}
