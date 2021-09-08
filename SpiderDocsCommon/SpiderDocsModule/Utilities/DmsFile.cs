using System;
using System.IO;
using System.Collections.Generic;
using Spider.Net;
using NLog;
using System.Linq;

//-----------------------------------------------------------
namespace SpiderDocsModule
{
	public enum en_DmsFile
	{
		Version = 0,

		Id,
		VersionId,

		Max
	}

	public class DmsFile<T> where T : Document
	{
        static Logger logger = LogManager.GetCurrentClassLogger();

		public string AppPath;
		public string ConnStr;

//-----------------------------------------------------------
		static string[] DmsFileTitle = 
		{
			"+Dms",
				"+Header",
					"Version",
					"-",
				"+Document",
					"Id",
					"VersionId",
				"-",
			"-"
		};

//-----------------------------------------------------------
		string[] Value = new string[(int)en_DmsFile.Max]
		{
			// Initial Values

			// Header
			"1",	// Version

			// Document
			" ",	// Id
			" "		// VersionId
		};

//-----------------------------------------------------------
		public void ReadDmsFile(string path)
		{
			XmlParser m_XmlParser = new XmlParser();
			XmlParser.XmlSetting DmsFileArg = new XmlParser.XmlSetting();
			DmsFileArg.val = Value;
			DmsFileArg.ele = DmsFileTitle;

			m_XmlParser.ReadXml(path, DmsFileArg);
		}
		
//-----------------------------------------------------------
		void WriteDmsFile(string path)
		{
			XmlParser m_XmlParser = new XmlParser();
			XmlParser.XmlSetting DmsFileArg = new XmlParser.XmlSetting();
			DmsFileArg.val = Value;
			DmsFileArg.ele = DmsFileTitle;

			m_XmlParser.WriteXml(path, DmsFileArg);
		}

//-----------------------------------------------------------
		public string GetVal(en_DmsFile idx)
		{
			string ans = "";

			if((int)idx < Value.Length)
				ans = Value[(int)idx];

			return ans;
		}

//-----------------------------------------------------------
		void SetVal(string val, en_DmsFile idx)
		{
			if((int)idx < Value.Length)
				Value[(int)idx] = val;
		}

//---------------------------------------------------------------------------------
// Static Methods -----------------------------------------------------------------
//---------------------------------------------------------------------------------
		public static List<string> SaveDmsFile(T doc, string path)
		{
			List<T> docs = new List<T>();
			docs.Add(doc);

			return SaveDmsFile(docs, path);
		}

//---------------------------------------------------------------------------------
		public static List<string> SaveDmsFile(List<T> docs, string path)
		{
			string wrk = "";
			List<string> names = new List<string>();			
			List<string> fullpath = new List<string>();	

			if(!String.IsNullOrEmpty(path))
			{
				foreach(T doc in docs)
				{
					wrk = GetDmsFileName(doc);
					names.Add(wrk);
				}
			
				fullpath = SaveDmsFile(docs, path, names);
			}

			return fullpath;
		}

//---------------------------------------------------------------------------------
		static List<string> SaveDmsFile(List<T> docs, string path, List<string> names)
		{
			List<string> ans = new List<string>();
			DmsFile<Document> dms = new DmsFile<Document>();

			int i = 0;
			foreach(string name in names)
			{
				dms.SetVal(docs[i].id.ToString(), en_DmsFile.Id);
				if(0 < docs[i].id_version)
					dms.SetVal(docs[i].id_version.ToString(), en_DmsFile.VersionId);

				string fullpath = path + name;
				ans.Add(fullpath);
				dms.WriteDmsFile(fullpath);

				i++;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public static List<string> MakeDmsFile(List<T> docs, string path)
		{
			List<string> filePath;

			filePath = SaveDmsFile(docs, path);

			return filePath;
		}

//---------------------------------------------------------------------------------
		public static void MailDmsFile(T doc, List<string> ToList, string subject, string body)
		{
			List<T> docs = new List<T>();
			docs.Add(doc);

			MailDmsFile(docs, ToList, subject, body);
		}

        //---------------------------------------------------------------------------------
        public static void MailDmsFile(List<T> docs, List<string> ToList, string subject, string body)
        {
            List<string> filePath = MakeDmsFile(docs, FileFolder.GetTempFolder());
            
            try { 
                if ((subject == "") && (filePath.Count == 1))
                    subject = Path.GetFileName(filePath[0]);

                MailSettingss MailSettingss = new MailSettingss();
                MailSettingss.Load();
                
                Spider.Net.Email email = new Spider.Net.Email(MailSettingss.server);
                email.to = ToList;
                email.subject = subject;
                email.body = body;
                email.attachments = filePath;
                email.MultiThread = false;
                email.Send();
                
                logger.Info("An email is sent successfully, {0} -> {1}", string.Join(",", docs.Select(x => x.id)) , string.Join(",", ToList));
            }
            catch (Exception ex)
            {
                logger.Info(ex,"Sending email failed");

            }

        }

        //---------------------------------------------------------------------------------
        public static string GetDmsFileName(T doc)
		{
			return Path.GetFileNameWithoutExtension(doc.title) + "_" +
					"ID" + doc.id + "_" +
					"V" + doc.version +
					".dms";
		}

//---------------------------------------------------------------------------------
	}
}
