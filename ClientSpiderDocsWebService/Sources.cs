using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Diagnostics;
using ClientSpiderDocsWebService.DTO;
using SpiderDocsModule;

//---------------------------------------------------------------------------------
//namespace SpiderDocs
//{
//    public class Sources : SourcesBase
//	{
//	}
//}

//---------------------------------------------------------------------------------
namespace ClientSpiderDocsWebService
{
    public class Sources/* : SpiderDocs.Sources*/
	{
//---------------------------------------------------------------------------------
		static string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["spiderDocs"].ConnectionString;
		static string sql;

//---------------------------------------------------------------------------------
		public static Document[] getDocumentsByFolder(int id_folder, int id_user)
		{
			DTS_Document DA_Document = new DTS_Document();
			//DA_Document.PermitFolder = getAllPermitFolders(id_user);
			//DA_Document.where.Add("AND id_folder = " + id_folder);
			//DA_Document.Select(true);

            var c = new SearchCriteria()
            {
                FolderIds = new List<int>() { id_folder }
            };

            DA_Document.Criteria.Add(c);
            DA_Document.Select();

            DataTable table = DA_Document.GetDataTable();

            return DA_Document.GetDocuments<Document>().Select(x =>
            {
                x.documentPermission = new DocumentPermission(
                    true,
                    true
                    );

                return x;

            }).ToArray();


   //         List<Document> documents = new List<Document>();
			//foreach(DataRow row in table.Rows)
			//{
			//	Document userDocuments = new Document();

			//	userDocuments.LoadDataRow(row);

   //             DocumentPermission dp =  new DocumentPermission(
   //             	Sources.CheckPermission(id_folder, id_user, en_Actions.Delete),
   //             	Sources.CheckPermission(id_folder, id_user, en_Actions.Archive)
   //             );
                
   //             userDocuments.documentPermission = dp;

			//	documents.Add(userDocuments);
			//}

			////SqlConnection sqlConn = new SqlConnection(strConn);

			////try
			////{

			////	sql = "       SELECT id,id_version,title,LTRIM(replace(version,'V','')),id_status,extension";
			////	sql = sql + " FROM        view_document";
			////	sql = sql + " WHERE       (id_folder = " + id_folder + ")";

			////	sql = sql + " ORDER BY title";

			////	SqlCommand sqlCommand = new SqlCommand(sql, sqlConn);
			////	sqlConn.Open();


			////	SqlDataReader dr = sqlCommand.ExecuteReader();


			////	while(dr.Read())
			////	{
			//	//	Document userDocuments = new Document();
			//	//	userDocuments.id = dr.GetInt32(0);
			//	//	userDocuments.id_version = dr.GetInt32(1);
			//	//	userDocuments.title = dr.GetString(2);
			//	//	userDocuments.version = Int32.Parse(dr.GetString(3));
			//	//	userDocuments.id_status = (int)((en_file_Status)dr.GetInt32(4));
			//	//	userDocuments.extension = dr.GetString(5);
					


			//	//	DocumentPermission dp =  new DocumentPermission(
			//	//	Sources.CheckPermission(id_folder, id_user, en_Actions.Delete),
			//	//	Sources.CheckPermission(id_folder, id_user, en_Actions.Archive)
			//	//	);


			//	//	userDocuments.documentPermission = dp;

			//	//	documents.Add(userDocuments);

			//	//}


			////}
			////catch(Exception e)
			////{
			////	Console.WriteLine(e.Message);
			////}
			////finally
			////{

			////	sqlConn.Close();

			////}

			//return documents.ToArray();
		}

		public static Folder[] getFolders(int id_user)
		{
            return SpiderDocsModule.PermissionController.GetAssignedFolderToUser(id_user).Select(x => new Folder() { folder_id = x.id, folder_name = x.document_folder }).ToArray();


   //         List<Folder> folders = new List<Folder>();
			//DataTable table = SpiderDocs.PermissionController.GetAssignedFolderToUser()(id_user);

            

   //         foreach (DataRow row in table.Rows)
			//{
			//	Folder userFolder = new Folder();
			//	userFolder.folder_id = Convert.ToInt32(row["id"]);
			//	userFolder.folder_name = row["document_folder"].ToString();
			//	folders.Add(userFolder);
			//}

			//return folders.ToArray();
		}

//---------------------------------------------------------------------------------
		public static Document[] getDocumentsByFolderWIthAttributes(int id_folder, int attributeId, string attributeValue)
		{
			DTS_Document DA_Document = new DTS_Document();
			//DA_Document.Add_searchAttrId(attributeId);
			//DA_Document.Add_searchAttrValue(attributeValue);
			//DA_Document.where.Add("AND id_folder = " + id_folder);
			//DA_Document.Select(true);


            var c = new SearchCriteria()
            {
                FolderIds = new List<int>() { id_folder },
                  AttributeCriterias = new AttributeCriteriaCollection() { }
            };
            c.AttributeCriterias.Add(new DocumentAttribute() { id = attributeId, atbValue = attributeValue });
            DA_Document.Criteria.Add(c);
            DA_Document.Select();

            DataTable table = DA_Document.GetDataTable();

            return DA_Document.GetDocuments<Document>().ToArray();

   //         List<Document> documents = new List<Document>();
            

   //         foreach (DataRow row in table.Rows)
			//{
			//	Document userDocuments = new Document();
   //             //userDocuments.LoadDataRow(row);
                

   //             documents.Add(userDocuments);
			//}
		   
			////List<Document> documents = new List<Document>();
			////SqlConnection sqlConn = new SqlConnection(strConn);

			////try
			////{

			////	sql = "       SELECT view_document.id,id_version,title,LTRIM(replace(version,'V','')),id_status,extension,date";
			////	sql = sql + " FROM        view_document";
			////	sql = sql + " INNER JOIN document_attribute ON document_attribute.id_doc = view_document.id";
			////	sql = sql + " AND document_attribute.id_atb = " + attributeId + " AND document_attribute.atb_value = '" + attributeValue + "' ";
			////	sql = sql + " WHERE       (id_folder = " + id_folder + ")";
			////	sql = sql + " ORDER BY title";

			////	SqlCommand sqlCommand = new SqlCommand(sql, sqlConn);
			////	sqlConn.Open();


			////	SqlDataReader dr = sqlCommand.ExecuteReader();



			////	while(dr.Read())
			////	{
			////		Document userDocuments = new Document();
			////		userDocuments.id = dr.GetInt32(0);
			////		userDocuments.id_version = dr.GetInt32(1);
			////		userDocuments.title = dr.GetString(2);
			////		userDocuments.version = Int32.Parse(dr.GetString(3));
			////		userDocuments.id_status = dr.GetInt32(4);
			////		userDocuments.extension = dr.GetString(5);
			////		userDocuments.date = dr.GetDateTime(6);
			////		documents.Add(userDocuments);
			////	}


			////}
			////catch(Exception e)
			////{
			////	Console.WriteLine(e.Message);
			////}
			////finally
			////{

			////	sqlConn.Close();

			////}


			//return documents.ToArray();


		}

//---------------------------------------------------------------------------------
		public static Document[] getDocumentsSearch(String keyword, int filterSelection, int folderId)
		{

			List<Document> documents = new List<Document>();
			SqlConnection sqlConn = new SqlConnection(strConn);


			String whereCondition = "";


			int num;
			bool isNum = Int32.TryParse(keyword, out num);


			switch(filterSelection)
			{

				case 1:
					whereCondition = " CONTAINS (t2.filedata, '\"*" + keyword + "*\"') " +
									 " OR t1.title LIKE '%" + keyword + "%' " +
									 " OR t1.type LIKE '%" + keyword + "%' " +
									 " OR t1.document_folder LIKE '%" + keyword + "%' " +
									 " OR t1.date LIKE '%" + keyword + "%' " +
									 " OR t1.name LIKE '%" + keyword + "%' ";
					break;
				case 2:
					if(isNum)
					{
						whereCondition = "t1.id = " + keyword;
					}
					break;

				case 3:
					whereCondition = "title LIKE '%" + keyword + "%' ";
					break;
				default:
					whereCondition = " CONTAINS (t2.filedata, '\"*" + keyword + "*\"') " +
									 " OR t1.title LIKE '%" + keyword + "%' " +
									 " OR t1.type LIKE '%" + keyword + "%' " +
									 " OR t1.document_folder LIKE '%" + keyword + "%' " +
									 " OR t1.date LIKE '%" + keyword + "%' " +
									 " OR t1.name LIKE '%" + keyword + "%' ";
									if(isNum)
									{
										whereCondition += " OR t1.id = " + keyword;
									}
					break;

			}


			try
			{

				sql = "       SELECT t1.id,t1.id_version,t1.title,LTRIM(replace(t1.version,'V','')),t1.document_folder";
				sql = sql + " FROM        view_document t1 ";
				sql = sql + " INNER JOIN document_version t2 ON t2.id = t1.id_version";
				sql = sql + " WHERE (" + whereCondition + ")" + ((folderId!=0) ? " AND id_folder = " + folderId : "") ;
				sql = sql + " ORDER BY t1.title";

				SqlCommand sqlCommand = new SqlCommand(sql, sqlConn);
				sqlConn.Open();


				SqlDataReader dr = sqlCommand.ExecuteReader();



				while(dr.Read())
				{
					Document userDocuments = new Document();
					userDocuments.id = dr.GetInt32(0);
					userDocuments.id_version = dr.GetInt32(1);
					userDocuments.title = dr.GetString(2);
					userDocuments.version = Int32.Parse(dr.GetString(3));
					//userDocuments.name_folder = dr.GetString(4);
					documents.Add(userDocuments);
				}


			}
			catch(Exception e)
			{
			   // Console.WriteLine(e.Message);
			}
			finally
			{

				sqlConn.Close();

			}


			return documents.ToArray();


		}

//---------------------------------------------------------------------------------
		public static DocumentType[] getDocumentType()
		{
			List<DocumentType> documentTypes = new List<DocumentType>();
			SqlConnection sqlConn = new SqlConnection(strConn);

			try
			{

				sql = " SELECT      id, type";
				sql = sql + " FROM        document_type";
				sql = sql + " ORDER BY type";

				SqlCommand sqlCommand = new SqlCommand(sql, sqlConn);
				sqlConn.Open();


				SqlDataReader dr = sqlCommand.ExecuteReader();



				while(dr.Read())
				{
					DocumentType docType = new DocumentType();
					docType.id = dr.GetInt32(0);
					docType.type = dr.GetString(1);
					documentTypes.Add(docType);
				}


			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{

				sqlConn.Close();

			}


			return documentTypes.ToArray();


		}

//---------------------------------------------------------------------------------
		public static Users getUserDetails(String login, String password)
		{

			Crypt crypt = new Crypt();
			

			Users user = new Users();

			sql = " select id,name from [user] where login= '" + login + "'and password= '" + crypt.Encrypt(password) + "'";
			SqlConnection sqlConn = new SqlConnection(strConn);

			sqlConn.Open();

			SqlCommand sqlCommand = new SqlCommand(sql, sqlConn);
			SqlDataReader dr = sqlCommand.ExecuteReader();

			while(dr.Read())
			{
				user.id = dr.GetInt32(0);
				user.name = dr.GetString(1);
			}


			sqlConn.Close();
			return user;
		}

//---------------------------------------------------------------------------------
		public static void writeEventLog(Exception ex)
		{
			string eventLog = "Application";
			string eventSource = "MySpiderDocs";

			StringBuilder sbErrorMsg = new StringBuilder("");
			sbErrorMsg.Append("Message\r\n" + ex.Message.ToString() + "\r\n\r\n");
			sbErrorMsg.Append("Source\r\n" + ex.Source + "\r\n\r\n");
			sbErrorMsg.Append("Target site\r\n" + ex.TargetSite.ToString() + "\r\n\r\n");
			sbErrorMsg.Append("Stack trace\r\n" + ex.StackTrace + "\r\n\r\n");
			sbErrorMsg.Append("ToString()\r\n\r\n" + ex.ToString());

			while(ex.InnerException != null)
			{
				sbErrorMsg.Append("Message\r\n" + ex.Message.ToString() + "\r\n\r\n");
				sbErrorMsg.Append("Source\r\n" + ex.Source + "\r\n\r\n");
				sbErrorMsg.Append("Target site\r\n" + ex.TargetSite.ToString() + "\r\n\r\n");
				sbErrorMsg.Append("Stack trace\r\n" + ex.StackTrace + "\r\n\r\n");
				sbErrorMsg.Append("ToString()\r\n\r\n" + ex.ToString());

				// Assign the next InnerException
				// to catch the details of that exception as well
				ex = ex.InnerException;
			}

			if(!EventLog.SourceExists(eventSource))
				EventLog.CreateEventSource(eventSource, eventLog);

			// Create an EventLog instance and assign its source.
			EventLog myLog = new EventLog(eventLog);
			myLog.Source = eventSource;

			// Write the error entry to the event log.    
			myLog.WriteEntry("An error occurred in the MySpiderDocs application " + eventSource + "\r\n\r\n" + sbErrorMsg.ToString(), EventLogEntryType.Error);
		}

//---------------------------------------------------------------------------------
		public static DocumentType[] getDocumentTypes()
		{
            return SpiderDocsModule.DocumentTypeController.DocumentType().ToArray();

			//List<DocumentType> documentTypes = new List<DocumentType>();
			//DataTable table = SpiderDocs.Sources.getDocumentType(false);

			//foreach(DataRow row in table.Rows)
			//{
			//	DocumentType docType = new DocumentType();
			//	docType.id = Convert.ToInt32(row["id"]);
			//	docType.type = row["type"].ToString();
			//	documentTypes.Add(docType);
			//}

			//return documentTypes.ToArray();

			////SqlConnection sqlConn = new SqlConnection(strConn);

			////try
			////{

			////	sql = " SELECT      id, type";
			////	sql = sql + " FROM        document_type";
			////	sql = sql + " ORDER BY type";

			////	SqlCommand sqlCommand = new SqlCommand(sql, sqlConn);
			////	sqlConn.Open();


			////	SqlDataReader dr = sqlCommand.ExecuteReader();



			////	while(dr.Read())
			////	{
			////		DocumentType docType = new DocumentType();
			////		docType.id = dr.GetInt32(0);
			////		docType.type = dr.GetString(1);
			////		documentTypes.Add(docType);
			////	}


			////}
			////catch(Exception e)
			////{
			////	Console.WriteLine(e.Message);
			////}
			////finally
			////{

			////	sqlConn.Close();

			////}


			////return documentTypes.ToArray();
		}

//---------------------------------------------------------------------------------
		public static DocumentAttribute[] getAllAttributesbyTypeDtArray(int id_doc_type)
		{
            
            return SpiderDocsModule.DocumentAttributeController.GetAttributes().Where( x => (int)x.id_type == id_doc_type).ToArray();

   //         //List<Attributes> attributes = new List<Attributes>();

   //         DataTable table = SpiderDocs.Sources.getAllAttributesbyTypeDt(id_doc_type);
			//return PopurateAttr(table).ToArray();

			////SqlConnection sqlConn = new SqlConnection(strConn);
			////sqlConn.Open();

			////try
			////{
			//	//foreach(DataRow row in table.Rows)
			//	//{
			//	//	Attributes attr = new Attributes();
			//	//	attr.name = row["name"].ToString();
			//	//	attr.id = Convert.ToInt32(row["id"]);
			//	//	attr.id_type = (Attributes.en_AttrType)row["id_type"];
			//	//	attr.position = Convert.ToInt32(row["position"]);
			//	//	attr.required = Convert.ToBoolean(row["required"]);

			//	//	if(attr.id_type == Attributes.en_AttrType.Combo)
			//	//	{
			//	//		DataTable table2 = SpiderDocs.Sources.getAttribute_combo_item(attr.id, false);
			//	//		List<ComboItem> arrayComboItems = new List<ComboItem>();

			//	//		foreach(DataRow row2 in table2.Rows)
			//	//		{
			//	//			ComboItem comboItems = new ComboItem();
			//	//			comboItems.comboItemId = Convert.ToInt32(row2["id"]);
			//	//			comboItems.comboItemValue = row2["Value"].ToString();
			//	//			comboItems.comboItemAttrId = attr.id;
			//	//			arrayComboItems.Add(comboItems);
			//	//			attr.comboItems = arrayComboItems.ToArray();
			//	//		}


			//			//SqlConnection sqlConn2 = new SqlConnection(strConn);
			//			//sqlConn2.Open();
			//			//SqlCommand sqlCommand2 = new SqlCommand("SELECT id, value FROM attribute_combo_item WHERE id_atb = " + attr.id + "order by value", sqlConn2);
			//			//SqlDataReader dr2 = sqlCommand2.ExecuteReader();

			//			//List<ComboItem> arrayComboItems = new List<ComboItem>();


			//			//while(dr2.Read())
			//			//{
			//			//	ComboItem comboItems = new ComboItem();
			//			//	comboItems.comboItemId = dr2.GetInt32(0);
			//			//	comboItems.comboItemValue = dr2.GetString(1);
			//			//	comboItems.comboItemAttrId = attr.id;
			//			//	arrayComboItems.Add(comboItems);
			//			//	attr.comboItems = arrayComboItems.ToArray();
			//			//}
			//			//sqlConn2.Close();


			//	//	}


			//	//	attributes.Add(attr);					
			//	//}



			////	sql = "       SELECT atb.name, atb.id, atb.id_type, adt.position,atb.required ";
			////	sql = sql + " from attributes atb";
			////	sql = sql + " inner join attribute_doc_type adt on  atb.id = adt.id_attribute";
			////	sql = sql + " inner join attribute_field_type aft on atb.id_type = aft.id";
			////	sql = sql + " where adt.id_doc_type = " + id_doc_type;
			////	sql = sql + " order by adt.position ";
   
			////	SqlCommand sqlCommand = new SqlCommand(sql, sqlConn);

			////	SqlDataReader dr = sqlCommand.ExecuteReader();
				
			////	while(dr.Read()){

			////		Attributes attr = new Attributes();

			////		attr.name = dr.GetString(0);
			////		attr.id = dr.GetInt32(1);
			////		attr.id_type = dr.GetInt32(2);
			////		attr.position = dr.GetInt32(3);
			////		attr.required = dr.GetBoolean(4);
			////		attributes.Add(attr);


			////		if(attr.id_type == 4)
			////		{
			////			SqlConnection sqlConn2 = new SqlConnection(strConn);
			////			sqlConn2.Open();
			////			SqlCommand sqlCommand2 = new SqlCommand("SELECT id, value FROM attribute_combo_item WHERE id_atb = " + attr.id + "order by value", sqlConn2);
			////			SqlDataReader dr2 = sqlCommand2.ExecuteReader();

			////			List<ComboItem> arrayComboItems = new List<ComboItem>();


			////			while(dr2.Read())
			////			{
			////				ComboItem comboItems = new ComboItem();
			////				comboItems.comboItemId = dr2.GetInt32(0);
			////				comboItems.comboItemValue = dr2.GetString(1);
			////				comboItems.comboItemAttrId = attr.id;
			////				arrayComboItems.Add(comboItems);
			////				attr.comboItems = arrayComboItems.ToArray();
			////			}
			////			sqlConn2.Close();


			////		}


			////	}


			////}

			////finally
			////{
			////	sqlConn.Close();
			////}


			////return attributes.ToArray();


		}




        public static DocumentAttribute[] getAllAttributesArray()
        {

            return SpiderDocsModule.DocumentAttributeController.GetAttributes().ToArray();


   //         DataTable table = SpiderDocs.Sources.getAllAttributes();
			//return PopurateAttr(table).ToArray();

			////SqlConnection sqlConn = new SqlConnection(strConn);
			////sqlConn.Open();

			////try
			////{



			//	//sql = "       SELECT atb.name, atb.id, atb.id_type,atb.required ";
			//	//sql = sql + " from attributes atb";
			//	//sql = sql + " inner join attribute_field_type aft on atb.id_type = aft.id";
			//	//sql = sql + " order by atb.name ";

			//	//SqlCommand sqlCommand = new SqlCommand(sql, sqlConn);

			//	//SqlDataReader dr = sqlCommand.ExecuteReader();

			//	//while(dr.Read())
			//	//{

			//	//	Attributes attr = new Attributes();

			//	//	attr.name = dr.GetString(0);
			//	//	attr.id = dr.GetInt32(1);
			//	//	attr.id_type = dr.GetInt32(2);
			//	//	attr.required = dr.GetBoolean(3);
			//	//	attributes.Add(attr);


			//		//if(attr.id_type == 4)
			//		//{
			//		//	SqlConnection sqlConn2 = new SqlConnection(strConn);
			//		//	sqlConn2.Open();
			//		//	SqlCommand sqlCommand2 = new SqlCommand("SELECT id, value FROM attribute_combo_item WHERE id_atb = " + attr.id + "order by value", sqlConn2);
			//		//	SqlDataReader dr2 = sqlCommand2.ExecuteReader();

			//		//	List<ComboItem> arrayComboItems = new List<ComboItem>();


			//		//	while(dr2.Read())
			//		//	{
			//		//		ComboItem comboItems = new ComboItem();
			//		//		comboItems.comboItemId = dr2.GetInt32(0);
			//		//		comboItems.comboItemValue = dr2.GetString(1);
			//		//		comboItems.comboItemAttrId = attr.id;
			//		//		arrayComboItems.Add(comboItems);
			//		//		attr.comboItems = arrayComboItems.ToArray();
			//		//	}
			//		//	sqlConn2.Close();


			////		}

			////	}


			////}

			////finally
			////{
			//	//sqlConn.Close();
			////}


			////return attributes.ToArray();


        }

//---------------------------------------------------------------------------------
		class ComboItem
		{
			public int comboItemId { get; set; }
			public String comboItemValue { get; set; }
			public int comboItemAttrId { get; set; }
		}

		//static List<DocumentAttribute> PopurateAttr(DataTable table)
		//{
		//	List<DocumentAttribute> attributes = new List<DocumentAttribute>();

		//	foreach(DataRow row in table.Rows)
		//	{
  //              DocumentAttribute attr = new DocumentAttribute();

		//		try { attr.name = row["name"].ToString(); } catch {}
		//		try { attr.id = Convert.ToInt32(row["id"]); } catch {}
		//		try { attr.id_type = (en_AttrType)row["id_type"]; } catch {}
		//		try { attr.position = Convert.ToInt32(row["position"]); } catch {}
		//		//try { attr.required = Convert.ToBoolean(row["required"]); } catch {}

		//		if(attr.id_type == en_AttrType.Combo)
		//		{
		//			DataTable table2 = SpiderDocs.Sources.getAttribute_combo_item(attr.id, false);
		//			List<ComboItem> arrayComboItems = new List<ComboItem>();

		//			foreach(DataRow row2 in table2.Rows)
		//			{
		//				ComboItem comboItems = new ComboItem();
		//				comboItems.comboItemId = Convert.ToInt32(row2["id"]);
		//				comboItems.comboItemValue = row2["Value"].ToString();
		//				comboItems.comboItemAttrId = attr.id;
		//				arrayComboItems.Add(comboItems);
		//				attr.comboItems = arrayComboItems.ToArray();
                        
		//			}
		//		}

		//		attributes.Add(attr);
		//	}

		//	return attributes;
		//}

//---------------------------------------------------------------------------------
		//public class Folder
		//{
		//	public int      folder_id   { get; set; }
		//	public string   folder_name { get; set; }
		//}

		public static Folder[] getFoldersContainingDocsAttribute(int id_user, int attributeId, string attributeValue)
		{
            return PermissionController.GetAssignedFolderToUser(id_user).Select(x => new Folder {  folder_id= x.id, folder_name = x.document_folder}).ToArray();

   //         List<Folder> folders = new List<Folder>();
   //         //SqlConnection sqlConn = new SqlConnection(strConn);

   //         //try
   //         //{
            
   //             DataTable table = DTS_Folder.Select_UserFolder(id_user);
			//	table.DefaultView.Sort = "document_folder";

			//	SqlOperation sql = new SqlOperation("document_attribute", Spider.Data.SqlOperationMode.Select);
			//	sql.Fields("id_folder");
			//	sql.InnerJoin("document", "document.id = document_attribute.id_doc");
			//	sql.Where("id_atb", attributeId);
			//	sql.Where("atb_value", attributeValue);
			//	sql.Where("id_status", (int)en_file_Status.checked_in);
			//	sql.Commit();
				
			//	List<int> ids = new List<int>();
			//	while(sql.Read())
			//		ids.Add(Convert.ToInt32(sql.Result("id_folder")));

			//	foreach(DataRow row in table.Rows)
			//	{
			//		int id = Convert.ToInt32(row["id"]);

			//		if(ids.Contains(id))
			//		{
			//			Folder userFolder = new Folder();
			//			userFolder.folder_id = id;
			//			userFolder.folder_name = row["document_folder"].ToString();
			//			folders.Add(userFolder);
			//		}
			//	}

				


			////sql = " SELECT DISTINCT id, document_folder " +
			////	  " FROM view_folder " +
			////	  " WHERE id_user =  " + id_user +
			////	  " AND id IN " +
			////	  " (" +
			////	  " SELECT id_folder " +
			////	  "   FROM document_attribute " +
			////	  "   INNER JOIN document ON document.id = document_attribute.id_doc " +
			////	  "   WHERE id_atb = " + attributeId + " AND atb_value = '" + attributeValue + "' AND id_status = 1" +
			////	  "   )" + 
			////	  "   ORDER BY document_folder  ";

			////	SqlCommand sqlCommand = new SqlCommand(sql, sqlConn);
			////	sqlConn.Open();


			////	SqlDataReader dr = sqlCommand.ExecuteReader();



			////	while(dr.Read())
			////	{
			////		Folder userFolder = new Folder();
			////		userFolder.folder_id = dr.GetInt32(0);
			////		userFolder.folder_name = dr.GetString(1);
			////		folders.Add(userFolder);
			////	}


			////}
			////catch(Exception e)
			////{
			////	Console.WriteLine(e.Message);
			////}
			////finally
			////{

			////	sqlConn.Close();

			////}


			//return folders.ToArray();
		}
        
//---------------------------------------------------------------------------------
		public static String getMimeTypeFromExtension(String extension)
		{
			switch(extension.ToLower())
			{
				case ".txt":
					return "text/plain";
				case ".docx":
				case ".doc":
					return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
				case ".dotx":
					return "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
				case ".xlsx":
				case ".xls":
					return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				case ".xltx":
					return "application/vnd.openxmlformats-officedocument.spreadsheetml.template";
				case ".potx":
					return "application/vnd.openxmlformats-officedocument.presentationml.template";
				case ".ppsx":
					return "application/vnd.openxmlformats-officedocument.presentationml.slideshow";
				case ".pptx":
					return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
				case ".sldx":
					return "application/vnd.openxmlformats-officedocument.presentationml.slide";
				case ".xlam":
					return "application/vnd.ms-excel.addin.macroEnabled.12";
				case ".xlsb":
					return "application/vnd.ms-excel.sheet.binary.macroEnabled.12";
				case ".gif":
					return "image/gif";
				case ".jpg":
				case "jpeg":
					return "image/jpeg";
				case ".bmp":
					return "image/bmp";
				case ".wav":
					return "audio/wav";
				case ".ppt":
					return "application/mspowerpoint";
				case ".dwg":
					return "image/vnd.dwg";
				case ".pdf":
					return "application/pdf";
				case ".htm":
				case ".html":
					return "text/html";
				default:
					return "none";

			}
		}

//---------------------------------------------------------------------------------
	}
}