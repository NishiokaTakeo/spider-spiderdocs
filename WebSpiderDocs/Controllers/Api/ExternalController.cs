using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using NLog;
using Spider.Security;
using SpiderDocsModule;
using WebSpiderDocs.ViewModels;
using lib = SpiderDocsModule.Library;

namespace WebSpiderDocs.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    //[RoutePrefix("api/external/{action}/{id}/")]
    public class ExternalController : ApiController
    {
        // GET: Api/User
        public SpiderDocsModule.Crypt crypt = new SpiderDocsModule.Crypt();
        static Logger logger = LogManager.GetCurrentClassLogger();
        string tempFolder = "temp";

        //---------------------------------------------------------------------------------
        public ExternalController() : base()
        {
            var temp = System.Web.Configuration.WebConfigurationManager.AppSettings["Folder.Temp"]; ;

            if (!string.IsNullOrEmpty(temp))
                tempFolder = temp;
        }

        IHttpActionResult  ReturnReadableFile(byte[] bytes, string path)
        {
            var stream = new MemoryStream(bytes);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };

            result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = System.IO.Path.GetFileName(path)
            };

            result.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue(Fn.TypeByExtnt(path));

            return ResponseMessage(result);
        }

        [HttpPost]
        public List<SpiderDocsModule.Document> GetDocuments(SearchCriteria Criteria)
        {
            List<SpiderDocsModule.Document> docs = new List<SpiderDocsModule.Document>();

            try
            {
                int limit = 150;
                DTS_Document table = new DTS_Document(WebSettings.CurrentUserId, limit, SpiderDocsApplication.CurrentServerSettings.localDb);
                table.Criteria.Add(Criteria);
                table.Select();

                if (0 < table.GetDataTable().Rows.Count)
                    docs = table.GetDocuments<SpiderDocsModule.Document>();

            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(Criteria));
            }

            return docs.OrderBy(x => x.title).ToList();
        }

		[HttpPost]
		public List<SpiderDocsModule.Document> GetRecentDocuments()
		{
			List<SpiderDocsModule.Document> docs = new List<SpiderDocsModule.Document>();

			try
			{
				int limit = 100;

				DTS_Document table = new DTS_Document(WebSettings.CurrentUserId, limit, SpiderDocsApplication.CurrentServerSettings.localDb);
				SearchCriteria criteria = new SearchCriteria();
				criteria = excludeArchive(criteria);
				table.SetRecentDocumentsCriteria(criteria);

				table.Select();

				if (0 < table.GetDataTable().Rows.Count)
					docs = table.GetDocuments<SpiderDocsModule.Document>();

			}
			catch (Exception ex)
			{
				logger.Error(ex.ToString(), "userId:{0}", WebSettings.CurrentUserId);
			}

			return docs.OrderByDescending(x => x.id).ToList();
		}

        [HttpPost]
        public List<ViewUserWorkSpace> GetUserWorkspaceDocuments()
        {
            List<ViewUserWorkSpace> docs = new List<ViewUserWorkSpace>();

            try
            {
				docs  = WorkSpaceSyncController.SearchBy(WebSettings.CurrentUserId);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return docs.OrderByDescending(x => x.filename).ToList();
        }

        [HttpPost]
        public List<string> ExportUserWorkspace(int[] Ids)
        {
            List<string> ans = new List<string>();

			try
            {

				foreach(var id in Ids)
				{
					var doc  = WorkSpaceSyncController.GetBy(id, true);

					using (var _utilists = new WebUtilities(tempFolder))
					{

						string filepath = _utilists.CreateTempFolder(postfix: "uw"+id.ToString());
						string uri = WebUtilities.GetCurrentUri(GetRootUriMode.Root, filepath);

						var filename = System.IO.Path.GetFileNameWithoutExtension(doc.filename);
						var ext = System.IO.Path.GetExtension(doc.filename).ToLower();

						doc.Save(filepath + filename + ext);
						ans.Add(uri + filename + ext);
					}

				}

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return ans;
        }

		[HttpPost]
        public List<string> Export(int[] VersionIds)
        {
            logger.Debug("Ids:" + string.Join(",", VersionIds));
            List<string> ans = new List<string>();
            try
            {
                Dictionary<int, int> doc_ids = DocumentController.GetDocumentId(VersionIds);
                using (var _utilists = new WebUtilities(tempFolder))
                {
                    if (0 < doc_ids.Count)
                    {
                        foreach (KeyValuePair<int, int> doc_id in doc_ids)
                        {
                            Document doc = DocumentController<Document>.GetDocument(doc_id.Value, doc_id.Key, data: true);

                            logger.Debug("userId:{0},folder:{1}", WebSettings.CurrentUserId, doc.id_folder);

                            var permissions = PermissionController.GetFolderPermissions(doc.id_folder, WebSettings.CurrentUserId);
                            var canRead = (permissions.ContainsKey(en_Actions.OpenRead) && permissions[en_Actions.OpenRead] == en_FolderPermission.Allow);
                            logger.Debug("canRead:" + canRead.ToString());
                            if (canRead
                            && PermissionController.CheckPermission(doc.id_folder, en_Actions.Export, WebSettings.CurrentUserId))
                            {
                                logger.Debug("canExport:true");
                                string filepath = _utilists.CreateTempFolder(postfix: doc_id.Key.ToString());
                                string uri = WebUtilities.GetCurrentUri(GetRootUriMode.Root, filepath);

								var filename = System.IO.Path.GetFileNameWithoutExtension(doc.title);
								var ext = System.IO.Path.GetExtension(doc.title).ToLower();

								doc.Save(filepath + filename + ext);
                                ans.Add(uri + filename + ext);
                            }
                            else
                            {
                                logger.Warn("Export {0} fails. Most of cases are probably permission issue.", doc.id_latest_version);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(VersionIds));
            }
            return ans;
        }

        /// <summary>
        /// Get binary content by version id.
        /// Use this method when you want to show content in web page
        /// </summary>
        /// <param name="VersionId"></param>
        /// <returns>byte[] with propre contenttype</returns>
        internal byte[] _GetContent(int VersionId, out string path)
        {
            byte[] binary = new byte[] { };

            logger.Debug("parameters: versionId={0}", VersionId);

            List<string> urls = this.Export(new int[] { VersionId });

            string url = urls.FirstOrDefault();
            path = Fn.ToFileSystemPath(url);

            try
            {
                binary = System.IO.File.ReadAllBytes(path);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} {1}", VersionId, url);
            }
            return binary;
        }

        /// <summary>
        /// Get binary content by version id.
        /// Use this method when you want to show content in web page
        /// </summary>
        /// <param name="VersionId"></param>
        /// <returns>byte[] with propre contenttype</returns>
        [AcceptVerbs("GET")/*, RequireParameter("VersionId")*/]
        public IHttpActionResult GetContent(int VersionId)
        {
            byte[] binary = _GetContent(VersionId, out string path);

            return ReturnReadableFile(binary, path);
        }

        internal byte[] _GetContent(int DocumentId, out string path, string Version = "")
        {

            logger.Debug("parameters: DocumentId={0}", DocumentId);
            Version = Version.Trim();

            byte[] binary = new byte[] { };

            path = "notfound.txt";

            List<int> versionIds = DocumentController.GetVersionIds(DocumentId);

            if (versionIds.Count() == 0) return binary;

            int _version = 0;
            if (!int.TryParse(Version, out _version))
            {
                for (_version = 1; _version <= versionIds.Count(); _version++)
                    if (Fn.ConvertToLetter(_version).ToUpper() == Version.ToUpper())
                        break;
            }

            int versionId = versionIds.Count() < _version ? versionIds.Last() : versionIds.ElementAt(_version - 1);

            logger.Debug("parameters: versionId={0}", versionId);

            List<string> urls = this.Export(new int[] { versionId });

            string url = urls.FirstOrDefault();

            path = Fn.ToFileSystemPath(url);

            try
            {
                binary = System.IO.File.ReadAllBytes(path);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} {1} {2}", DocumentId, Version, versionId);
            }

            return binary;
        }


        [AcceptVerbs("GET")/*, RequireParameter("VersionId")*/]
        public IHttpActionResult GetContent(int DocumentId, string Version = "")
        {
            Version = Version.Trim();

            byte[] binary = new byte[] { };


            List<int> versionIds = DocumentController.GetVersionIds(DocumentId);

            if (versionIds.Count() == 0) return ReturnReadableFile(binary,"notfound.txt");

            int _version = 0;
            if (!int.TryParse(Version, out _version))
            {
                for (_version = 1; _version <= versionIds.Count(); _version++)
                    if (Fn.ConvertToLetter(_version).ToUpper() == Version.ToUpper())
                        break;
            }

            int versionId = versionIds.Count() < _version ? versionIds.Last() : versionIds.ElementAt(_version - 1);

            logger.Debug("parameters: versionId={0}", versionId);

            List<string> urls = this.Export(new int[] { versionId });

            string url = urls.FirstOrDefault(),path = Fn.ToFileSystemPath(url);

            try
            {
                binary = System.IO.File.ReadAllBytes(path);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} {1} {2}", DocumentId, Version, versionId);
            }

            return ReturnReadableFile(binary,path);
        }

        //---------------------------------------------------------------------------------
        [HttpPost]
        public string UploadFile()
        {
            logger.Trace("Begin");
            string temp;

            using (var _utilists = new WebUtilities(tempFolder)) {
                temp = _utilists.CreateTempFolder();
            }

            string filepath = temp;
            string fullpath = "";
            try
            {
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    fullpath = filepath + System.Web.HttpContext.Current.Request.Files[0].FileName;

                    System.Web.HttpContext.Current.Request.Files[0].SaveAs(fullpath);

                    logger.Debug("Key:{0},SaveAs:{1},{2} byte", System.Web.HttpContext.Current.Request.Files.AllKeys.Length, fullpath, new System.IO.FileInfo(fullpath).Length);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(Request));
            }

            return new DirectoryInfo(filepath).Name;
        }

		string SaveFileToTemp(int userId, string tempId, string name ,byte[] file )
		{
			logger.Trace("Begin");

			//var userId = WebSettings.CurrentUserId;
			//var tempId = System.Web.HttpContext.Current.Request.Form["TempId"].ToString();
			//var files = System.Web.HttpContext.Current.Request.Files;

			string ansTempId = string.Empty;

			string saveAt = "";
			// first call will be here.
			if (string.IsNullOrEmpty(tempId))
			{
				saveAt = FileFolder.GetAvailableDirPath(FileFolder.GetTempFolder(), userId.ToString());

				FileFolder.CreateFolderAt(saveAt, true);
			}
			else
			{
				saveAt = System.IO.Path.Combine(FileFolder.GetTempFolder(), tempId);
			}

			ansTempId = FileFolder.GetLastDirName(saveAt);

			//if (files.Count != 1)
			//{
			//	logger.Warn("Upload file must be one");
			//	return string.Empty;
			//}

			//var file = files[0];

			logger.Debug("Paramters: {0}, {1}", saveAt, name);

			try
			{
				//var filepath = FileFolder.CreateTempFolderAt(tempId);
				var savePath = System.IO.Path.Combine(saveAt, name);

				//file.SaveAs(savePath);
				System.IO.File.WriteAllBytes(savePath, file);

				logger.Debug("SaveAs:{1},{2} byte", savePath, new System.IO.FileInfo(savePath).Length);

			}
			catch (Exception ex)
			{
				logger.Error(ex);

			}

			return ansTempId;
		}


		//[HttpPost]
		//public async System.Threading.Tasks.Task<string> UploadFileToTemp()
		//{
		//	logger.Trace("Begin");

		//	var userId= WebSettings.CurrentUserId;
		//	var tempId = System.Web.HttpContext.Current.Request.Form["TempId"].ToString();
		//	var files = System.Web.HttpContext.Current.Request.Files;

		//	var file = files[0];

		//	return await System.Threading.Tasks.Task.Run(() => SaveFileToTemp(userId, tempId, file));
		//}

		[HttpPost]
		public async System.Threading.Tasks.Task<string> SaveFile2TempAsync(WebParams args)
		{
			logger.Trace("Begin");

			var userId = WebSettings.CurrentUserId;
			//var tempId = System.Web.HttpContext.Current.Request.Form["TempId"].ToString();
			//var files = System.Web.HttpContext.Current.Request.Files;

			//var file = files[0];

			return await System.Threading.Tasks.Task.Run(() =>
			{

				Document doc = DocumentController<Document>.GetDocument(args.DocumentId, args.VersionId, data: true);
				var saveAt = FileFolder.GetAvailableDirPath();

				var path = System.IO.Path.Combine(saveAt, doc.title);

				doc.Save(path);

				var tempId = FileFolder.GetLastDirName(saveAt);

				//SaveFileToTemp(userId, tempId, doc.title, file);

				return tempId;
			});
		}



		[HttpPost]
		public async System.Threading.Tasks.Task<string> SaveWorkSpaceFile2TempAsync(WebParams args)
		{
			logger.Trace("Begin");

			var userId = WebSettings.CurrentUserId;

			return await System.Threading.Tasks.Task.Run(() =>
			{
				var saveAt = FileFolder.GetAvailableDirPath();

				var uworkRecord =  WorkSpaceSyncController.GetBy(args.Id, true);
				var wfile = new WorkSpaceFile(uworkRecord, saveAt);
				wfile.SyncDown();

				var tempId = FileFolder.GetLastDirName(saveAt);

				return tempId;
			});
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<string> SaveDMS2TempAsync(WebParams args)
		{
			logger.Trace("Begin");

			var userId = WebSettings.CurrentUserId;

			return await System.Threading.Tasks.Task.Run(() =>
			{
				var saveAt = FileFolder.GetAvailableDirPath();

				Document doc = DocumentController<Document>.GetDocument(args.DocumentId);

				SpiderDocsModule.DmsFile<Document>.SaveDmsFile(doc, saveAt);

				var tempId = FileFolder.GetLastDirName(saveAt);

				return tempId;
			});
		}


		[HttpPost]
        public string UploadRemoteFile(string strURL)
        {
            logger.Trace("Begin");
            string filepath;
            using (var _utilists = new WebUtilities(tempFolder))
            {
                filepath = _utilists.CreateTempFolder();

                try
                {
                    using (var client = new WebClient())
                    {
                        Uri uri = new Uri(strURL);
                        string filename = Path.GetFileName(uri.LocalPath);

                        client.DownloadFile(new System.Uri(strURL), filepath + filename);

                        logger.Debug("Download File:{0},{1} byte", filepath + filename, new System.IO.FileInfo(filepath + filename).Length);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(strURL));
                }
            }
            return new DirectoryInfo(filepath).Name;
        }

        [HttpPost]
        public string Import(string TempId, Document Document)
        {
            logger.Trace("[Begin] Parameters:{0}", TempId);

            string ans = "";
            try
            {
                var temproot = "";
                using (var temp = new WebUtilities(tempFolder))
                {
                    temproot = temp.GetTempFolder();
                }

                if (PermissionController.CheckPermission(Document.id_folder, en_Actions.CheckIn_Out, WebSettings.CurrentUserId))
                {

                    //string path = Directory.GetFiles(Fn.MapPath("temp\\" + TempId + "\\"))[0];
                    string path = Directory.GetFiles(temproot + "\\" + TempId + "\\")[0];

                    logger.Trace("[Before Import] TempId:{0}, Path:{1}", TempId, path);
                    if (0 < Document.id)
                    {
                        Document = DocumentController.GetDocument(Document.id);
                        Document.path = path;

                        if (Document.id_status != en_file_Status.checked_out)
                        {
                            Document.CheckOut(WebSettings.CurrentUserId, false);
                            Document.id_event = 0;
                        }

                        ans = Document.AddVersion(WebSettings.CurrentUserId, SpiderDocsApplication.CurrentServerSettings.localDb);
                        logger.Trace("[After AddVersion] TempId:{0}", TempId);
                    }
                    else
                    {
                        if (!Document.isNotDuplicated(true))
                        {
                            ans = lib.msg_existing_doc_type;
                            throw new Exception(ans);
                        }

                        //Duplication check
                        Document.path = path;
                        ans = Document.AddDocument(WebSettings.CurrentUserId, SpiderDocsApplication.CurrentServerSettings.localDb);
                        logger.Trace("[After AddDocument] TempId:{0}", TempId);
                    }


                }
                else
                {
                    logger.Warn("[No Permission] TempId:{0}, Folder:{1}, UserID:{2}", TempId, Document.id_folder, WebSettings.CurrentUserId);

                    ans = Library.msg_permissio_checkIn;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} , {1}", TempId, Newtonsoft.Json.JsonConvert.SerializeObject(Document));
            }

            logger.Trace("[End] Parameters:{0}, Ans:{1}", TempId, ans);

            return ans;
        }

        [HttpPost]
        public Document SaveDoc(string TempId, Document Document/*, bool Property = false*/)
        {
            Document ans = new Document();

            try
            {
                string message = "";
                var temproot = "";
                using (var temp = new WebUtilities(tempFolder))
                {
                    temproot = temp.GetTempFolder();
                }
                    if (PermissionController.CheckPermission(Document.id_folder, en_Actions.CheckIn_Out, WebSettings.CurrentUserId))
                    {
                        //string path = Directory.GetFiles(Fn.MapPath("temp\\" + TempId + "\\"))[0];
                        string path = Directory.GetFiles(temproot + "\\" + TempId + "\\")[0];
                        Document.path = path;

                        Document.id_checkout_user = WebSettings.CurrentUserId;
                        Document.id_status = en_file_Status.checked_out;
                        Document.id_sp_status = en_file_Sp_Status.normal;

                        if (0 < Document.id)
                        {
                            message = Document.AddVersion(WebSettings.CurrentUserId, SpiderDocsApplication.CurrentServerSettings.localDb);
                        }
                        else
                        {
                            if (!Document.isNotDuplicated(true))
                            {
                                throw new Exception(lib.msg_existing_doc_type);
                            }


                            message = Document.AddDocument(WebSettings.CurrentUserId, SpiderDocsApplication.CurrentServerSettings.localDb);
                        }

                        //if(Property)
                        //{
                        //    UpdateProperty(Document);
                        //}

                        if (string.IsNullOrWhiteSpace(message))
                        {
                            ans = DocumentController.GetDocument(Document.id);
                        }
                    }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} , {1}", TempId, Newtonsoft.Json.JsonConvert.SerializeObject(Document));
            }

            return ans;
        }

		[HttpPost]
		public Document SaveDoc(WebParams arg)
		{
			return SaveDoc(arg.TempId, arg.Document);
		}
		/// <summary>
		/// Import by Remote URL at onece
		/// </summary>
		/// <param name="Url"></param>
		/// <param name="Document"></param>
		/// <returns></returns>
		[HttpPost]
        public Document RemoteImport(string Url, Document Document, List<DocumentAttributeCombo> TextCombo = null)
        {
            TextCombo = TextCombo == null ? new List<DocumentAttributeCombo>() : TextCombo;

            string tempid = UploadRemoteFile(Url);

            // Update Combo Item Children
            foreach (DocumentAttributeCombo combo in TextCombo)
            {
                int comboid = EditAttributeComboboxItem(combo.id_atb, combo);

                if (comboid == 0) continue;

                DocumentAttribute a = Document.Attrs.Find(x => x.id == combo.id_atb);

                if (a == null || !a.IsCombo()) continue;

                List<int> list = ((List<int>)a.atbValue);

                if (list.FindIndex(xx => xx == comboid) == -1)
                    list.Add(comboid);

            }

            Document doc = SaveDoc(tempid, Document);

            // return . it should be id_doc
            return doc;
        }


		/// <summary>
		/// Save file in Database as new ver.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public Document SaveAsNewVer()
		{
			logger.Trace("Begin");

			//string error = string.Empty;
			Document ans = new Document();

			var userId= WebSettings.CurrentUserId;
			var docId = Convert.ToInt32(System.Web.HttpContext.Current.Request.Form["DocumentId"]);
			var reason = System.Web.HttpContext.Current.Request.Form["Reason"].ToString();
			var files = System.Web.HttpContext.Current.Request.Files;

			var savedPath = string.Empty;



			if( files.Count != 1 )
			{
				logger.Warn("Upload file must be one");
				return ans;
			}

			var file = files[0];

			logger.Debug("Paramters: {0}, {1}, {2}",docId,file.FileName,reason);

			try
			{
				string filepath = FileFolder.GetTempFolder(), savePath = System.IO.Path.Combine(filepath,file.FileName);
				file.SaveAs( savePath );

				logger.Debug("SaveAs:{1},{2} byte",  savePath, new System.IO.FileInfo(savePath).Length);

				savedPath = savePath;
			}
			catch (Exception ex)
			{
				logger.Error(ex);

				savedPath = "";
			}

			if( false == string.IsNullOrWhiteSpace(savedPath))
			{
				var doc = DocumentController.GetDocument(id_doc:docId);
				var old_status = doc.id_status;

				if (doc.CheckOut(userId, false))
				{
					doc.id_event = 0;
					doc.reason = reason;
					doc.path = savedPath;

					var error = doc.AddVersion(WebSettings.CurrentUserId, SpiderDocsApplication.CurrentServerSettings.localDb);
					logger.Error(error);

					ans = DocumentController.GetDocument(id_doc:docId);
				}
				else
				{
					logger.Warn("This file has checkout. Cannot proceed.");
					return ans;
				}
			}
			else
			{
				logger.Warn("Faield. Folder might not exist or no permission");
				return ans;
			}

			return ans;
		}



		[HttpPost]
		public UserWorkSpace ImportUserWorkSpaceFile()
		{
			logger.Trace("Begin");
			var ans = new UserWorkSpace();

			var userId= WebSettings.CurrentUserId;
			var linkId = Convert.ToInt32(System.Web.HttpContext.Current.Request.Form["linkid"]);
			//var reason = System.Web.HttpContext.Current.Request.Form["Reason"].ToString();
			var files = System.Web.HttpContext.Current.Request.Files;

			var savedPath = string.Empty;



			if( files.Count != 1 )
			{
				logger.Warn("Upload file must be one");
				return ans;
			}

			var file = files[0];

			logger.Debug("Paramters: {0}, {1}",linkId,file.FileName);

			var wfile = WorkSpaceSyncController.GetBy(linkId, false);


			try
			{

				// Place the file
				string filepath = FileFolder.GetTempFolder(), savePath = System.IO.Path.Combine(filepath,wfile.filename);
				file.SaveAs( savePath );

				logger.Debug("SaveAs:{1},{2} byte",  savePath, new System.IO.FileInfo(savePath).Length);

				savedPath = savePath;



				// Replace user work space file
				WorkSpaceFile newVer = new WorkSpaceFile(wfile, FileFolder.GetTempFolder());

				if (System.IO.File.Exists(newVer.FullName)) System.IO.File.Delete(newVer.FullName);

				System.IO.File.Move(savePath, newVer.FullName);

				newVer.SyncUp();

				ans = WorkSpaceSyncController.GetBy(linkId, false);
			}
			catch (Exception ex)
			{
				logger.Error(ex);

				savedPath = "";
			}



			return ans;
		}

        [HttpPost]
        public string UpdateProperty(Document Document)
        {
            string ans = "";

            try
            {

                if (!Document.isNotDuplicated())
                {
                    ans = lib.msg_existing_doc_type;
                    throw new Exception(lib.msg_existing_doc_type);
                }


				if (Document.IsActionAllowed(en_Actions.Properties, WebSettings.CurrentUserId))
				{
					ans = Document.UpdateProperty(WebSettings.CurrentUserId);

					DocumentNotificationGroupController.SaveOne(null, Document.id, Document.id_notification_group);

					CustomViewCtr.SaveCustomViewDocument(Document.id, Document.customviews.ToList());
				}
				else
					ans = Library.msg_permissio_denied;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(Document));
				ans = lib.msg_error_default;

			}
            return ans;
        }

        /// <summary>
        /// Save linked attribute
        /// </summary>
        /// <param name="keyID"></param>
        /// <param name="keyValue"></param>
        /// <param name="linkedID"></param>
        /// <param name="linkedValue"></param>
        [HttpPost]
        /// <returns></returns>
        public string SaveLinkedAttribute(int keyID, string keyValue, DocumentAttribute linkedAttr)
        {
            string ans = "";

            if ( keyID <= 0 || string.IsNullOrWhiteSpace(keyValue) || linkedAttr.id <= 0 )
            {
                return string.Empty;
            }

            try
            {
                // Delete if linked value is empty
                if( string.IsNullOrWhiteSpace(linkedAttr.atbValue_str))
                {
                    DocumentAttributeController.DeleteLinkedAttribute(keyID, keyValue);
                    return string.Empty;
                }

                // Otherwise insert as new
                var linked = DocumentAttributeController.GetLinkedAttribute(keyID, keyValue);
                if(linked == null || linked.id != linkedAttr.id || linked.atbValue_str != linkedAttr.atbValue_str)
                {
                    var sql = new SqlOperation();
                    if( DocumentAttributeController.DeleteLinkedAttribute(keyID, keyValue,sql))
                    {
                        DocumentAttributeController.InsertLinkedAttribute(keyID, keyValue, linkedAttr.id, linkedAttr.atbValue_str, sql);
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex, "keyID:{0}, keyValue:{1}, linkedID:{2}, linkedValue:{3}", keyID, keyValue, linkedAttr.id, linkedAttr.atbValue_str);
            }
            return ans;
        }

        [HttpPost]
        /// <returns></returns>
        public DocumentAttribute SaveAttribute(WebParams args)
        {
			DocumentAttribute ans = new DocumentAttribute();

            try
            {
				ans = DocumentAttributeController.UpdateOrInsertAttribute(args.Attribute);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(args));
            }
			finally
			{
				Cache.Remove(Cache.en_GKeys.DB_GetAttributes);
			}

            return ans;
        }


		[HttpPost]
		public async System.Threading.Tasks.Task<DocumentAttribute> SaveAttributeAsync(WebParams args)
		{
			var ans = await System.Threading.Tasks.Task.Run(() => SaveAttribute(args));

			return ans;
		}


		/// <summary>
		/// Delete document logically.
		/// </summary>
		/// <param name="DocumentIds">ids of documents you want to delete</param>
		/// <param name="Reason">Reason for delete documents.</param>
		/// <returns>Error message</returns>
		[HttpPost]
        public string Delete(int[] DocumentIds, string Reason)
        {
            string ans = "";

            try
            {
                if (APISettings.AllowDeleteDocuments)
                {
                    List<int> error_ids = new List<int>();

                    List<Document> docs = DocumentController.GetDocument(id_doc: DocumentIds);

                    foreach (Document doc in docs)
                    {
                        if (PermissionController.CheckPermission(doc.id_folder, en_Actions.Delete, WebSettings.CurrentUserId))
                            DocumentController.DeleteDocument(WebSettings.CurrentUserId, doc.id, Reason);
                        else
                            error_ids.Add(doc.id);
                    }

                    if (0 < error_ids.Count)
                        ans = Library.msg_permissio_denied + "," + String.Join(",", error_ids);
                }
                else
                {
                    ans = Library.msg_permissio_denied;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} , {1}", Newtonsoft.Json.JsonConvert.SerializeObject(DocumentIds), Reason);
            }

            return ans;
        }

		/// <summary>
		/// Delete document logically.
		/// </summary>
		/// <param name="DocumentIds">ids of documents you want to delete</param>
		/// <param name="Reason">Reason for delete documents.</param>
		/// <returns>Error message</returns>
		[HttpPost]
		public async System.Threading.Tasks.Task<string> DeleteAsync(WebParams args)
		{
			var ans = await System.Threading.Tasks.Task.Run( () => this.Delete(args.DocumentIds, args.Reason)) ;

			return ans;
		}



		/// <summary>
		/// Delete document logically.
		/// </summary>
		/// <param name="DocumentIds">ids of documents you want to delete</param>
		/// <param name="Reason">Reason for delete documents.</param>
		/// <returns>Error message</returns>
		[HttpPost]
		async public System.Threading.Tasks.Task<string> DeleteUserWorkSpaceFile(WebParams args)
		{
			string error = string.Empty;

			string temp = string.Empty;
			List<bool> ans = new List<bool>();


			var uwspaces = await WorkSpaceSyncController.GetByAsync(ids: args.UserWorkSpaceIds);

			var docIds = uwspaces.Where(x => x.id_doc > 0).Select(x => x.id_doc).ToArray(); // checkout docs

			// Delete checkedout documents
			if(docIds.Length > 0)
				error = await CancelCheckOutAsync(docIds);


			// Delete new
			var ids = uwspaces.Where(x => x.id_doc <= 0).Select(x => x.id).ToArray();  // new docs
			if(ids.Length > 0)
			{

				foreach (var linkid in ids)
				{
					var file = await WorkSpaceSyncController.GetByAsync(linkid, false);

					WorkSpaceFile newFile = new WorkSpaceFile(file, FileFolder.GetTempFolder());

					await newFile.DeleteRemoteAsync();
				}
			}

			return error;

            // using (var _utilists = new WebUtilities(tempFolder))
			// {
            //     temp = _utilists.CreateTempFolder();

			// 	temp = FileFolder.GetAvailableFileName(temp);
            // }

			// foreach(var linkid in args.DocumentIds)
			// {
			// 	var file = await WorkSpaceSyncController.GetByAsync(linkid,false);

			// 	WorkSpaceFile newFile = new WorkSpaceFile(file,temp);

			// 	var ok = await newFile.DeleteRemote();

			// 	// cancel checkout
			// 	if( newFile.DocVersion > 0)
			// 	{
			// 		Dictionary<int,int> dic = DocumentController.GetDocumentId(newFile.DocVersion);

			// 		if (dic.ContainsKey(newFile.DocVersion))
			// 			await CancelCheckOutAsync(new int[] { dic[newFile.DocVersion] });
			// 	}

			// 	ans.Add(ok);
			// }

			// return ans.ToArray();
		}

		#region Folder
		[HttpPost, Route("api/external/GetFolders/{id:int}"), Route("api/external/GetFolders")]
        public List<Folder> GetFolders()
        {
            LogManager.GetLogger("depreciate").Warn("This method is still used. Use GetFoldersL2() methods instead");

            List<Folder> folders = new List<Folder>();
            try
            {
                //folders = new Cache(WebSettings.CurrentUserId).GetAssignedFolderToUser();
                folders = SpiderDocsModule.PermissionController.GetAssignedFolderToUser(WebSettings.CurrentUserId);//.Select(x => new Folder() { folder_id = x.id, folder_name = x.document_folder }).ToList();

                folders = folders.Where(f => f.id < 200 || f.id == 59998 || f.id == 59999).ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0}", WebSettings.CurrentUserId);
            }

            return folders.OrderBy(x => x.document_folder).ToList();
        }

		[HttpPost, Route("api/external/GetFolders/{id:int}/{FolderIDs}")]
		public List<Folder> GetFolders(int[] FolderIDs)
		{
            LogManager.GetLogger("depreciate").Warn("This method is still used. Use GetFoldersL2() methods instead");

            List<int> folderIds = FolderIDs.ToList();

			List<Folder> folders = new List<Folder>();
			try
			{
				//folders = new Cache(WebSettings.CurrentUserId).GetAssignedFolderToUser();
                folders = SpiderDocsModule.PermissionController.GetAssignedFolderToUser(WebSettings.CurrentUserId);

                folders = folders.Where(f => folderIds.Contains(f.id)).ToList();
			}
			catch (Exception ex)
			{
				logger.Error(ex, "{0}", WebSettings.CurrentUserId);
			}

			return folders.OrderBy(x => x.document_folder).ToList();
		}

		[HttpPost]
        public List<Folder> GetFoldersL2(int idParent)
        {
			LogManager.GetLogger("depreciate").Warn("This method is still used. Use GetFoldersL1() methods instead");

			List<Folder> folders = new List<Folder>();
            try
            {
                folders = PermissionController.GetAssignedFolderLevel2(idParent, WebSettings.CurrentUserId);

            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0}", WebSettings.CurrentUserId);
            }

            return folders.OrderBy(x => x.document_folder).ToList();
        }

        [HttpPost]
        public List<Folder> GetFoldersL1(int idParent, en_Actions permission = en_Actions.None)
        {
            //LogManager.GetLogger("depreciate").Warn("This method is still used. Use GetFoldersL2() methods instead");

            List<Folder> folders = new List<Folder>();
            try
            {
                folders = PermissionController.GetAssignedFolderLevel1(idParent, WebSettings.CurrentUserId, permission);

            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0}", WebSettings.CurrentUserId);
            }

            return folders.OrderBy(x => x.document_folder).ToList();
        }

		[HttpPost]
		public async System.Threading.Tasks.Task<List<Folder>> GetFoldersL1Async(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetFoldersL1(args.idParent, args.Permission));
		}

		[HttpPost]

		public List<Folder> SearchFolders(int[] FolderIDs)
		{
			List<Folder> folders = new List<Folder>();
			try
			{
				folders = FolderController.GetFolders(FolderIDs);
			}
			catch (Exception ex)
			{
				logger.Error(ex, "{0}", WebSettings.CurrentUserId);
			}

			return folders;
		}

		[HttpPost]

		public async System.Threading.Tasks.Task<List<Folder>> SearchFoldersAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => SearchFolders(args.FolderIds));
		}


		[HttpPost]
        public List<Folder> DrillUpFoldersWithParentsFrom(int idParent, en_Actions permission = en_Actions.None)
        {
            List<Folder> ans = new List<Folder>();
            try
            {
                var folders = PermissionController.drillUpfoldersby(idParent, WebSettings.CurrentUserId, permission);

				// TODO: This is very slow logic. Must be change later.
				foreach (var folder in folders)
				{
					var parents = FolderController.FindBy(folder.id_parent);
					foreach(var parant in parents)
					{
						if( PermissionController.CheckPermission(parant.id, permission, WebSettings.CurrentUserId ) )
						{
							ans.Add(parant);
						}
					}
				}

				var rootFolders = PermissionController.GetAssignedFolderLevel1(0, WebSettings.CurrentUserId, permission);

				ans.AddRange(rootFolders.Where(x => x.id_parent != 0));

			}
			catch (Exception ex)
            {
                logger.Error(ex, "{0}", WebSettings.CurrentUserId);
            }

            return ans.OrderBy(x => x.document_folder).ToList();
        }

		[HttpPost]
		public async System.Threading.Tasks.Task<List<Folder>> DrillUpFoldersWithParentsFromAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => DrillUpFoldersWithParentsFrom(args.idParent, args.Permission));
		}

		[HttpPost]
        public List<KeyValuePair<en_Actions, en_FolderPermission>> GetFolderPermissions(WebParams args)
        {
			List<KeyValuePair<en_Actions ,en_FolderPermission>> ans = new List<KeyValuePair<en_Actions, en_FolderPermission>>();

            try
            {
				 ans = PermissionController.GetFolderPermissions(args.idFolder, args.UserId).ToList();
			}
			catch (Exception ex)
            {
                logger.Error(ex, "{0}", args.UserId);
            }

            return ans.ToList();
        }

		[HttpPost]
		public async System.Threading.Tasks.Task<List<KeyValuePair<en_Actions, en_FolderPermission>>> GetFolderPermissionsAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetFolderPermissions(args));
		}


		[HttpPost]
        public Folder SaveFolder(Folder folder)
        {
            try
            {
                if (!FolderController.IsUniqueFolderName(folder.document_folder, folder.id_parent))
                {
                    return FolderController.FindBy(folder.document_folder, folder.id_parent);
                }

                int id = FolderController.Save(folder);

                if (folder.id_parent == 0)
                    PermissionController.GrantFullPermission(id);

                folder.id = id;

            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0}", WebSettings.CurrentUserId);
            }
			finally
			{
				Cache.Remove(Cache.en_GKeys.DB_GetFolder);
				new Cache(WebSettings.CurrentUserId).Remove(Cache.en_UKeys.DB_GetAssignedFolderToUser);
			}
			
            return folder;
        }


		[HttpPost]
        public bool RemoveFolder(WebParams args)
        {
			logger.Trace("Begin");

			bool removed = true;
			// Do nothing if you don't have permission to execute
			bool hasPermission = PermissionController.CheckPermission(args.idFolder, en_Actions.Delete);
			if (!hasPermission) return false;


			// Delete Files under the folder
			List<Document> docs = DocumentController.GetBy(id_folder: args.idFolder);
			for (int i = docs.Count - 1; i >= 0; i--)
			{
				Document doc = docs[i];

				bool ok = doc.CancelCheckOut();

				if (ok)
				{
					logger.Info("Document is removed: {0}", doc.id);
					if (logger.IsDebugEnabled) logger.Debug("{0}", Newtonsoft.Json.JsonConvert.SerializeObject(doc));

					doc.Remove("Deleted by Folder Exploere", SpiderDocsApplication.CurrentUserId);
				}
				else
				{
					removed = false;
				}
			}

			// Remove the folder
			FolderController.Delete(args.idFolder);

			return removed;
		}
	
		[HttpPost]
        public async System.Threading.Tasks.Task<bool> RemoveFolderAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run( () => RemoveFolder(args));
		}





		[HttpPost]
		public string AddGroupOrUserToFolder(WebParams args)
		{
			string ans = string.Empty;
			try
			{

				if(0 < args.GroupId)
					PermissionController.AssignFolder(en_FolderPermissionMode.Group, Convert.ToInt32(args.idFolder), args.GroupId);
				else
					PermissionController.AssignFolder(en_FolderPermissionMode.User, Convert.ToInt32(args.idFolder), args.UserId);

			}
			catch(Exception error)
			{
				logger.Error(error);
				ans = error.Message;
			}

			return ans;
		}

		[HttpPost]
		public string AddPermission(WebParams args)
		{
			string ans = string.Empty;
			try
			{
				if (0 < args.GroupId)
					PermissionController.AddPermission(args.idFolder, args.GroupId, en_FolderPermissionMode.Group, args.Permissions);
				else
					PermissionController.AddPermission(args.idFolder, args.UserId, en_FolderPermissionMode.User, args.Permissions);

			}
			catch(Exception error)
			{
				logger.Error(error);
				ans = error.Message;
			}

			return ans;
		}

		[HttpPost]
        public string GetInheritedFolderName(WebParams args)
        {
			int curFolderId = args.idFolder;

            var curFolder = FolderController.GetFolder(curFolderId);
            if (curFolder.id == 0 || curFolder.id_parent == 0)
            {
                return string.Empty;
            }

            // btnToggleInheritance.Enabled = true;

            var inheritedFolder = PermissionController.GetInheritanceFolder(curFolderId);
            if (inheritedFolder.id == curFolderId)
            {
				return string.Empty;

            }
            else
            {


				return inheritedFolder.document_folder;
            }
        }


		[HttpPost]
		public Folder GetInheritanceFolder(WebParams args)
		{
			int curFolderId = args.idFolder;

			var curFolder = FolderController.GetFolder(curFolderId);
			if (curFolder.id == 0 || curFolder.id_parent == 0)
			{
				return new Folder();
			}

			var inheritedFolder = PermissionController.GetInheritanceFolder(curFolderId);

			return inheritedFolder;
		}


		[HttpPost]
		public string ToggleInheritance(WebParams args)
        {
			string ans = string.Empty;
            int curFolderId = args.idFolder;

            var curFolder = FolderController.GetFolder(curFolderId);

            if (curFolderId == 0 || curFolder.id_parent == 0)
            {
                return ans;
            };
			try {
				var inheritedFolder = PermissionController.GetInheritanceFolder(curFolderId);
				if (inheritedFolder.id == curFolder.id)
				{

						// Delete current folder's permission
						PermissionController.DeleteAllPermission(curFolder.id);


				}
				else
				{
					// (Disable inheritance)

					PermissionController.CopyPermissions(inheritedFolder.id, curFolder.id);

				}

					//cboFolder_SelectionChangeCommitted();

			}catch(Exception ex)
			{
				logger.Error(ex);
				ans = "Something went wrong";
			}
			return ans;
		}




		public Dictionary<en_Actions, string> GetFolderPermissionTitles(WebParams args)
		{
			Dictionary<en_Actions, string> titles = PermissionController.GetFolderPermissionTitles();

			return titles;
		}


		[HttpPost]
		public async System.Threading.Tasks.Task<Dictionary<en_Actions, string>> GetFolderPermissionTitlesAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetFolderPermissionTitles(args));
		}


		public string DeletePermission(WebParams args)
		{
			string ans = string.Empty;
			try
			{
				// if (lvGroupAndUser.SelectedItems.Count == 0)
				// 	return;

				int id_folder = args.idFolder;

				// The root folder must have at least one permission.
				var curFolder = FolderController.GetFolder(id_folder);
				var grups = PermissionController.GetAssignedUserOrGroupToFolder(en_FolderPermissionMode.Group,id_folder);
				var users = PermissionController.GetAssignedUserOrGroupToFolder(en_FolderPermissionMode.User, id_folder);

				if (curFolder.id_parent == 0 && (grups.Count() + users.Count()) <= 1)
				{
					ans = "The root folder must have one group/user setup at least.";
					return ans;
				}

				//var result = (MessageBox.Show("Are you sure you want to delete this group/user?", "Spider Docs", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question));

				//if (result == DialogResult.Yes)
				//{
					//foreach (ListViewItem eachItem in this.lvGroupAndUser.SelectedItems)
					//{
						//delete from base
						if (args.GroupId > 0)
							PermissionController.DeleteAssignedFolder(en_FolderPermissionMode.Group, id_folder, args.GroupId);
						else
							PermissionController.DeleteAssignedFolder(en_FolderPermissionMode.User, id_folder, args.UserId);

						////delete from grid
						//execRoutine = false;
						//lvGroupAndUser.Items.RemoveAt(eachItem.Index);
						//execRoutine = true;

						//clearDataGrid();
					//}

					//cboFolder_SelectionChangeCommitted();

				//}
			}
			catch (Exception error)
			{
				logger.Error(error);
			}
			finally
			{
				new Cache(SpiderDocsApplication.CurrentUserId).Remove(Cache.en_UKeys.DB_GetAssignedFolderToUser);
			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<string> DeletePermissionAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => DeletePermission(args));
		}

		[HttpPost]
		public Dictionary<en_Actions, en_FolderPermission> GetPermissionsByGroupAndUser(WebParams args)
		{
			//if (execRoutine)
			//{
			//execRoutine = false;

			//lvGroupAndUser.AllowReorder = false;
				Dictionary<en_Actions, en_FolderPermission> ans = new Dictionary<en_Actions, en_FolderPermission>();
			 int foler_id = args.idFolder;

				Dictionary<en_Actions, en_FolderPermission> permissions = new Dictionary<en_Actions, en_FolderPermission>();
				if (args.GroupId > 0)
					permissions = PermissionController.GetFolderPermission(foler_id, args.GroupId, en_FolderPermissionMode.Group, true);
				else
					permissions = PermissionController.GetFolderPermission(foler_id, args.UserId, en_FolderPermissionMode.User, true);

				//dtgPermission.Rows.Clear();

				Dictionary<en_Actions, string> titles = PermissionController.GetFolderPermissionTitles();
				foreach (KeyValuePair<en_Actions, string> wrk in titles)
				{
				//if (permissions.ContainsKey(wrk.Key))
				//{
				//	switch (permissions[wrk.Key])
				//	{
				//		case en_FolderPermission.Both:
				//		permission =
				//			break;

				//		case en_FolderPermission.Allow:
				//			Allow = true;
				//			break;

				//		case en_FolderPermission.Deny:
				//			Deny = true;
				//			break;
				//	}
				//}
					if ( permissions.ContainsKey(wrk.Key) )
						ans.Add(wrk.Key, permissions[wrk.Key]);

				}

				//cbHeaderAllow.ChkAllTick();
				//cbHeaderDeny.ChkAllTick();

				//execRoutine = true;
				//}

			return ans;
		}


		//[HttpPost]
		//public IHttpActionResult SaveFolder(Folder folder)
		//{
		//    try
		//    {
		//        //throw new Exception();
		//        if (!FolderController.IsUniqueFolderName(folder.document_folder, folder.id_parent))
		//        {
		//            //return  new Folder() ;
		//            return Ok(FolderController.FindBy(folder.document_folder, folder.id_parent));
		//        }

		//        int id = FolderController.Save(folder);

		//        PermissionController.GrantFullPermission(id);

		//        folder.id = id;
		//    }
		//    catch (Exception ex)
		//    {
		//        logger.Error(ex, "{0}", WebSettings.CurrentUserId);
		//        //throw new HttpResponseException(HttpStatusCode.InternalServerError);
		//        //Ok();
		//        //NotFound();
		//        return InternalServerError(ex);

		//    }
		//    return Ok(folder);
		//}

		#endregion

		//---------------------------------------------------------------------------------
		[HttpPost]
        public List<DocumentType> GetDocumentTypes()
        {
            List<DocumentType> types = new List<DocumentType>();
            try
            {
                types = DocumentTypeController.DocumentType();

				types = types.OrderBy(x => x.type).ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return types;
        }

		[HttpPost]
		public DocumentType SaveDocumentType(DocumentType documentType)
		{
			DocumentType ans = new DocumentType();
			var id = documentType.id;

			try
			{
				if (documentType.id > 0)
					DocumentTypeController.UpdateDocumentType(documentType.id, documentType.type);
				else
					id = DocumentTypeController.InsertDocumentType(documentType.type);

				ans = DocumentTypeController.DocumentType(id);
			}
			catch (Exception ex)
			{
				logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(documentType));
			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<DocumentType> SaveDocumentTypeAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => SaveDocumentType(args.DocumentType));
		}


		[HttpPost]
		public bool RemoveDocumentType(int id)
		{
			bool ans = false;

			try
			{
				bool hasFiles = DocumentTypeController.IsDocTypeUsed(id);
				if (hasFiles)
				{
					ans = false;
					return ans;
				}
				else
				{
					DocumentTypeController.RemoveDocumentType(id);
					ans = true;
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(id));
			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<bool> RemoveDocumentTypeAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => RemoveDocumentType(args.Id));
		}


		[HttpPost]
		public List<AttributeFieldType> GetAttributeFieldTypes()
		{
			List<AttributeFieldType> types = new List<AttributeFieldType>();
			try
			{
				types = AttributeFieldTypeController.GetAll();
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}
			return types;
		}


		[HttpPost]
		public async System.Threading.Tasks.Task<List<AttributeFieldType>> GetAttributeFieldTypesAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetAttributeFieldTypes());
		}

		//---------------------------------------------------------------------------------
		[HttpPost]
        public List<DocumentAttribute> GetAttributes()
        {
            List<DocumentAttribute> attrs = new List<DocumentAttribute>();
            try
            {
                attrs = DocumentAttributeController.GetAttributesCache();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return attrs;
        }

        [HttpGet]
        public List<AttributeDocType> GetAttributeDocType([FromUri] int[] DocTypeIds)
        {
            List<AttributeDocType> attrs = new List<AttributeDocType>();
            try
            {
                attrs = DocumentAttributeController.GetIdListByDocType(doc_type_id: DocTypeIds);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return attrs;
        }

        /// <summary>
        /// Check out documents.
        /// The ordered FolderIds and DocIds must be paired.
        /// </summary>
        /// <param name="FolderIds">id of folders.</param>
        /// <param name="DocIds">id of documents</param>
        /// <returns>Successed:empty, other will be error</returns>
        [HttpPost]
        public async System.Threading.Tasks.Task<string> CheckOut(int[] DocIds)
        {
            string ans = string.Empty;

			try
			{
				List<Document> docs = DocumentController.GetDocument(id_doc: DocIds);

				// check first.
				foreach (var doc in docs)
					if( !doc.IsActionAllowed(en_Actions.CheckIn_Out,WebSettings.CurrentUserId) ) return Library.msg_api_err_prmt;

				// checkout & move files to user workspace.
				foreach (var doc in docs)
				{
					await CheckoutAsync(doc);
				}
			}
			catch(Exception ex)
			{
				logger.Error(ex);

				ans = "Reject Checkout. Check permission.";
			}

            return ans;
        }

		/// <summary>
		/// Check out documents.
		/// The ordered FolderIds and DocIds must be paired.
		/// </summary>
		/// <param name="FolderIds">id of folders.</param>
		/// <param name="DocIds">id of documents</param>
		/// <returns>Successed:empty, other will be error</returns>
		[HttpPost]
		public async System.Threading.Tasks.Task<string> CheckOutWithFooterAsync(WebParams args)
		{
			string ans = string.Empty;

			try
			{
				List<Document> docs = DocumentController.GetDocument(id_doc: args.DocIds);

				// check first.
				foreach (var doc in docs)
					if (!doc.IsActionAllowed(en_Actions.CheckIn_Out, WebSettings.CurrentUserId)) return Library.msg_api_err_prmt;

				// checkout & move files to user workspace.
				foreach (var doc in docs)
				{
					await CheckoutAsync(doc, args.Footer);
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex);

				ans = "Reject Checkout. Check permission.";
			}

			return ans;
		}
				
		public async System.Threading.Tasks.Task<WorkSpaceFile> CheckoutAsync(Document doc, bool footer = false)
		{


			var ans = new UserWorkSpace();

			string temp;
			int userId = WebSettings.CurrentUserId;

			using (var _utilists = new WebUtilities(tempFolder))
			{
				temp = System.IO.Path.GetDirectoryName(_utilists.CreateTempFolder() + doc.PathWithVersionIdFolder);

				temp = FileFolder.GetAvailableFileName(temp);
			}

			var path = System.IO.Path.Combine(temp, doc.title);

			await System.Threading.Tasks.Task.Yield();

			doc.CheckOut(userId, footer, path);
			var wfile = new WorkSpaceFile(path, temp);
			wfile.InitSync();
			return wfile;
		}


		/// <summary>
		/// Check in a file as new ver for checked out document.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="reason"></param>
		/// <returns></returns>
        [HttpPost]
		public ViewUserWorkSpace CheckInAsNewVer(int id, string reason)
        {
            ViewUserWorkSpace ans = new ViewUserWorkSpace();

			try
            {
				var workspaceRecord  = WorkSpaceSyncController.GetBy(ids:new int[]{ id}, filedata: true).First();

				var localFile = new WorkSpaceFile( workspaceRecord, FileFolder.GetTempFolder());
				localFile.SyncDown();

				Document doc = DocumentController.GetDocument(workspaceRecord.id_doc);
				if(doc.id > 0 )
				{

					doc.reason = reason;
					doc.path = localFile.FullName;


					var error = doc.AddVersion(WebSettings.CurrentUserId, SpiderDocsApplication.CurrentServerSettings.localDb);
					logger.Error(error);


					if( string.IsNullOrWhiteSpace(error))
					{
						ans = workspaceRecord;

						localFile.DeleteRemote();
					}
				}
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return ans;
        }


        [HttpPost]
        public async System.Threading.Tasks.Task<ViewUserWorkSpace> CheckInAsNewVerAsync(WebParams args)
        {
			return await System.Threading.Tasks.Task.Run(() => CheckInAsNewVer(args.Id,args.Reason));
        }



        [HttpPost]
        //public async System.Threading.Tasks.Task<ViewUserWorkSpace> CheckInAsNewVerAsync(int[] Ids)
		public Document SaveWorkSpaceFileToDbAsNew(WebParams args)
        {
			Document ans = new Document();

			try
            {
				var workspaceRecord  = WorkSpaceSyncController.GetBy(args.Id, filedata: true);

				var localFile = new WorkSpaceFile( workspaceRecord, FileFolder.GetTempFolder());
				localFile.SyncDown();

				//Document doc = DocumentController.GetDocument(workspaceRecord.id_doc);
				if (!args.Document.isNotDuplicated(true))
				{
					return ans;
				}

				// Convert PDF if option is true
				if( args.Options.SaveAsPDF)
				{
					var ocrMgr = new OCRManager(localFile.FullName, true) { UseIronOCR = true };

					var ocredPath = ocrMgr.GetPDF();

					args.Document.SetPath(ocredPath,System.IO.Path.GetFileNameWithoutExtension(args.Document.title));
				}
				else
				{
					args.Document.path = localFile.FullName;
				}

				string error = args.Document.AddDocument(WebSettings.CurrentUserId, SpiderDocsApplication.CurrentServerSettings.localDb);
				DocumentNotificationGroupController.SaveOne(null, args.Document.id, args.Document.id_notification_group);

				if( !string.IsNullOrEmpty(error))
					logger.Error(error);

				if( string.IsNullOrWhiteSpace(error))
				{
					localFile.DeleteRemote();
				}

				ans = DocumentController.GetDocument(id_doc: args.Document.id);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return ans;
        }

		[HttpPost]
		//public async System.Threading.Tasks.Task<ViewUserWorkSpace> CheckInAsNewVerAsync(int[] Ids)
		public async System.Threading.Tasks.Task<Document> SaveWorkSpaceFileToDbAsNewAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => SaveWorkSpaceFileToDbAsNew(args));
		}






        [HttpPost]
		public Document SaveWorkSpaceFileToDbAsVer(WebParams args)
        {
			Document ans = new Document();

			try
            {
				var workspaceRecord  = WorkSpaceSyncController.GetBy(args.Id, filedata: true);

				var localFile = new WorkSpaceFile( workspaceRecord, FileFolder.GetTempFolder());
				localFile.SyncDown();


				// Convert PDF if option is true
				if( args.Options.SaveAsPDF)
				{
					var ocrMgr = new OCRManager(localFile.FullName, true) { UseIronOCR = true };

					var ocredPath = ocrMgr.GetPDF();

					args.Document.SetPath(ocredPath,System.IO.Path.GetFileNameWithoutExtension(args.Document.title));
				}
				else
				{
					args.Document.path = localFile.FullName;
				}

				args.Document.id_event = 0;
				args.Document.reason = args.Reason;

				string error = args.Document.AddVersion(WebSettings.CurrentUserId, SpiderDocsApplication.CurrentServerSettings.localDb);

				if( !string.IsNullOrEmpty(error))
					logger.Error(error);

				if( string.IsNullOrWhiteSpace(error))
				{
					localFile.DeleteRemote();
				}

				ans = DocumentController.GetDocument(id_doc: args.Document.id);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return ans;
        }

		[HttpPost]
		//public async System.Threading.Tasks.Task<ViewUserWorkSpace> CheckInAsNewVerAsync(int[] Ids)
		public async System.Threading.Tasks.Task<Document> SaveWorkSpaceFileToDbAsVerAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => SaveWorkSpaceFileToDbAsVer(args));
		}


		[HttpPost]
		//public async System.Threading.Tasks.Task<ViewUserWorkSpace> CheckInAsNewVerAsync(int[] Ids)
		public Document ImportDbAsNew(WebParams args)
		{
			Document ans = new Document();

			try
			{
				var temproot = "";
				using (var temp = new WebUtilities(tempFolder))
				{
					temproot = temp.GetTempFolder();
				}
				string path = Directory.GetFiles(temproot + "\\" + args.TempId + "\\")[0];

				
				if (!args.Document.isNotDuplicated(true))
				{
					return ans;
				}

				// Convert PDF if option is true
				if (args.Options.SaveAsPDF)
				{
					var ocrMgr = new OCRManager(path, true) { UseIronOCR = true };

					var ocredPath = ocrMgr.GetPDF();

					args.Document.SetPath(ocredPath, System.IO.Path.GetFileNameWithoutExtension(args.Document.title));
				}
				else
				{
					args.Document.path = path;
				}

				string error = args.Document.AddDocument(WebSettings.CurrentUserId, SpiderDocsApplication.CurrentServerSettings.localDb);
				DocumentNotificationGroupController.SaveOne(null, args.Document.id, args.Document.id_notification_group);

				if (!string.IsNullOrEmpty(error))
					logger.Error(error);

				//if (string.IsNullOrWhiteSpace(error))
				//{
				//	localFile.DeleteRemote();
				//}

				ans = DocumentController.GetDocument(id_doc: args.Document.id);

			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}

			return ans;
		}

		[HttpPost]
		//public async System.Threading.Tasks.Task<ViewUserWorkSpace> CheckInAsNewVerAsync(int[] Ids)
		public async System.Threading.Tasks.Task<Document> ImportDbAsNewAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => ImportDbAsNew(args));
		}









		[HttpPost]
		public UserWorkSpace RenameUserWorkSpaceFile(WebParams args)
		{
			UserWorkSpace ans = new UserWorkSpace();

			try
			{
				var workspaceRecord = WorkSpaceSyncController.GetBy(args.Id, filedata: true);

				var localFile = new WorkSpaceFile(workspaceRecord, FileFolder.GetTempFolder());
				localFile.SyncDown();

				if( localFile.RenameTo(args.NewName) )
				{
					ans = WorkSpaceSyncController.GetBy(args.Id, filedata: false);
				}

				if (workspaceRecord.id_version > 0)
				{
					var ids = DocumentController.GetDocumentId(workspaceRecord.id_version);
					if (ids.Count > 0)
					{
						args.DocumentId = ids.First().Value;

						var doc = this.RenameDbFile(args);
						if (doc.id == 0)
							ans = new UserWorkSpace();
					}
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}

			return ans;
		}
		[HttpPost]
		public async System.Threading.Tasks.Task<UserWorkSpace> RenameUserWorkSpaceFileAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => RenameUserWorkSpaceFile(args));
		}

		[HttpPost]
		public Document RenameDbFile(WebParams args)
		{
			Document ans = new Document();

			try
			{
				Document doc = DocumentController.GetDocument(args.DocumentId);
				doc.title = args.NewName;

				// Check if no duplication
				if (!doc.isNotDuplicated())
				{
					return new Document();
				}

				if(  doc.UpdateProperty(WebSettings.CurrentUserId).Trim() == string.Empty)
				{
					return doc;
				}
				else
				{
					return new Document();
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<Document> RenameDbFileAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => RenameDbFile(args));
		}

		/// <summary>
		/// Archive
		/// </summary>
		/// <param name="DocIds">ids of document that will be archived</param>
		/// <returns>empty if all of documents have archived. others mean fails to do</returns>
		[HttpPost]
        public string Archive(int[] DocIds)
        {
            logger.Trace("Begin");

            string ans = string.Empty;
            List<Document> docs = new List<Document>();
            try
            {
                foreach (int id_doc in DocIds)
                {
                    Document doc = DocumentController.GetDocument(id_doc);
                    if(doc == null)
                        return lib.msg_api_err_archive;

                    if (doc.IsActionAllowed(en_Actions.Archive, WebSettings.CurrentUserId))
                        docs.Add(doc);
                }

                // You must have all folders permissions.
                if(docs.Count != DocIds.Length)
                    return lib.msg_api_err_prmt;

                foreach (Document doc in docs)
                    doc.Archive(WebSettings.CurrentUserId);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return ex.Message;
            }

            return string.Empty;
        }

        /// <summary>
        /// Canceled Archive
        /// </summary>
        /// <param name="DocIds">ids of document that will be canceled archives</param>
        /// <returns>empty if all of documents have been canceled successfuly. others mean fails to do</returns>
        [HttpPost]
        public string UnArchive(int[] DocIds)
        {
            logger.Trace("Begin");

            string ans = string.Empty;
            List<Document> docs = new List<Document>();
            try
            {
                foreach (int id_doc in DocIds)
                {
                    Document doc = DocumentController.GetDocument(id_doc);
                    if (doc == null)
                        return lib.msg_api_err_unarchive;

                    if (doc.IsActionAllowed(en_Actions.UnArchive, WebSettings.CurrentUserId))
                        docs.Add(doc);
                }

                // You must have all folders permissions.
                if (docs.Count != DocIds.Length)
                    return lib.msg_api_err_unarchive;

                foreach (Document doc in docs)
                    doc.UnArchive(WebSettings.CurrentUserId);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return ex.Message;
            }

            return string.Empty;
        }


        /// <summary>
        /// CancelCheckout
        /// Nothing to do if one of documents is an error.
        /// </summary>
        /// <param name="DocIds">ids of document </param>
        /// <returns>empty if all of documents have cancelled check out. others mean fails to do</returns>
        [HttpPost]
        public string CancelCheckOut(int[] DocIds)
        {
            string ans = string.Empty;

            List<Document> docs = DocumentController.GetDocument(DocIds);

            if(logger.IsDebugEnabled) logger.Debug("Docs Count is : " + docs.Count);

            foreach( var doc in docs)
            {
                if (!doc.IsActionAllowed(en_Actions.CancelCheckOut,WebSettings.CurrentUserId)) return Library.msg_api_err_prmt_invaid;
            }

			string temp = FileFolder.GetTempFolder();

			// remove work space file.
			var uwspaces = WorkSpaceSyncController.GetBy(docIds: DocIds);

			foreach(var doc in docs)
			{
				if( doc.CancelCheckOut() )
				{
					foreach(var workspace in uwspaces.Where(us => us.id_doc == doc.id))
					{
						var file = WorkSpaceSyncController.GetBy(workspace.id, false);

						WorkSpaceFile newFile = new WorkSpaceFile(file, temp);

						newFile.DeleteRemote();
					}
				}
			}

            return ans;
        }

		[HttpPost]
		public async System.Threading.Tasks.Task<string> CancelCheckOutAsync(int[] DocIds)
		{
			return await System.Threading.Tasks.Task.Run(() => CancelCheckOut(DocIds));
		}

		[HttpPost]
        public int EditAttributeComboboxItem(int AttributeId, DocumentAttributeCombo Item)
        {
            int ans = 0;
            try
            {
                List<DocumentAttributeCombo> list = DocumentAttributeController.GetComboItems(sort: false, attr_ids: new int[] { AttributeId }, value_filter: Item.text);

                DocumentAttributeCombo work = list.FirstOrDefault(a => a.text == Item.text);

                if (work == null)
                    ans = DocumentAttributeController.InsertOrUpdateComboItem(Item, AttributeId, true);
                else
                    ans = work.id;

                // Update children even if combo item found.
                if (work != null)
                {
                    DocumentAttributeController.InsertOrUpdateComboItemChildren(work.id, Item.children);
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} , {1}", AttributeId, Newtonsoft.Json.JsonConvert.SerializeObject(Item));
            }

            return ans;
        }

        [HttpPost,Route("api/external/GetAttributeComboboxItems/{id:int}/{AttributeId:int}/{ItemId:int}")]
        public List<DocumentAttributeCombo> GetAttributeComboboxItems(int AttributeId = 0,int ItemId = 0)
        {
            List<DocumentAttributeCombo> ComboboxItems = new List<DocumentAttributeCombo>();
            try
            {
                if (0 < AttributeId)
                    ComboboxItems = DocumentAttributeController.GetComboItems(AttributeId);
                else if (0 < ItemId)
                    ComboboxItems = DocumentAttributeController.GetComboItems(ids: new int[] { ItemId });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} , {1}", AttributeId, ItemId);
            }

            return ComboboxItems;
        }

        [HttpPost, Route("api/external/GetAttributeComboboxItems/{id:int}/{AttributeId:int}/{Text}")]
        public List<DocumentAttributeCombo> GetAttributeComboboxItems(int AttributeId = 0, string Text = "")
        {
            List<DocumentAttributeCombo> ComboboxItems = new List<DocumentAttributeCombo>();
            try
            {
                ComboboxItems = DocumentAttributeController.GetComboItems(sort: false, attr_ids: new int[] { AttributeId }, value_filter: Text);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0} , {1}", AttributeId, Text);
            }

            return ComboboxItems;
        }


        [HttpPost]
        public User GetUser(string UserName, string Password)
        {
            User user = UserController.GetUser(true, UserName);
            user = (user == null ? new User() : user);

            if (crypt.Encrypt(Password).ToLower() != user.password.ToLower())
                user = new User();

            user.password = "";

            return user;
        }

        [HttpPost]
        public bool UpdateUser(User user, string UserName, string Password)
        {
            User origin = UserController.GetUser(true, UserName);

            if (origin == null) return false;

            if (crypt.Encrypt(Password).ToLower() != origin.password.ToLower())
                return false;

            // Don't allow to change login authentication
            user.password = origin.password;
            user.login = origin.login;

            UserController.UpdatetUser(user);

            return false;
        }

		[HttpPost]
		public User SaveUser(WebParams args)
		{
			User ans = new User();
			string plainPassword = string.Empty;
            try
            {
				plainPassword = args.User.password;
				args.User.password = crypt.Encrypt(args.User.password);

                if (0 < args.User.id)
                    UserController.UpdatetUser(args.User);
                else
                    UserController.InsertUser(args.User);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(args.User));
			}
            finally
            {
                Cache.Remove(Cache.en_GKeys.DB_User);
            }

			ans = this.GetUser(args.User.login, plainPassword);

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<User> SaveUserAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => SaveUser(args));
		}



		[HttpPost]
		public Group SaveGroup(WebParams args)
		{
			var groupId = 0;

			try
			{
				groupId = GroupController.UpdateOrInsertGroup(args.Group);
			}
			catch (Exception ex)
			{
				logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(args.User));
			}
			finally
			{
				Cache.Remove(Cache.en_GKeys.DB_User);
			}

			var ans = GroupController.GetGroups(groupId).FirstOrDefault() ?? new Group();

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<Group> SaveGroupAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => SaveGroup(args));
		}


		[HttpPost]
        public User GetUserByLoginPassword(WebParams args)
        {
            User user = UserController.GetUser(true, args.LoginName);
            user = (user == null ? new User() : user);

            if (crypt.Encrypt(args.Password).ToLower() != user.password.ToLower())
                user = new User();

            user.password = "";

            return user;
        }

		[HttpPost]
		public async System.Threading.Tasks.Task<User> GetUserByLoginPasswordAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetUserByLoginPassword(args));
		}

        [HttpPost]
        public List<History> GetHistories(SearchCriteria Criteria)
        {
            List<History> histories = null;

            try
            {
                histories = HistoryController.GetHistories(Criteria);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(Criteria));
            }

            return histories;
        }


        [HttpPost]
        public DocumentDetails GetDetails(WebParams args)
        {
            DocumentDetails ans = new DocumentDetails();

			try
			{
				//    histories = HistoryController.GetHistories(Criteria);

				//return histories;

				var doc = DocumentController.GetDocument(id_doc: args.DocumentId);

				if (doc != null)
				{
					//pictureBoxBd.Visible = true;
					//pictureBoxBd.Image = icon.GetLargeIcon(doc.extension);

					ans.id_doc = doc.id;
					ans.title = doc.title;
					ans.version = doc.version;

					History history = HistoryController.GetLatestHistory(doc.id_version,
																		 en_Events.Chkin, en_Events.Created, en_Events.SaveNewVer,
																		 en_Events.Rollback, en_Events.NewVer, en_Events.Archive,
																		 en_Events.ChgAttr, en_Events.ChgDT, en_Events.ChgFolder,
																		 en_Events.ChgName, en_Events.Import, en_Events.Property,
																		 en_Events.Scan);
					if (history != null)
						ans.date = history.date;
				//else
				//	lblBdUpdated.Text = Library.msg_no_history;

					ans.extension = doc.extension;

					ans.size = doc.size;

					if ((doc.id_sp_status == en_file_Sp_Status.review)
					|| (doc.id_sp_status == en_file_Sp_Status.review_overdue))
					{
						Review review = ReviewController.GetReview(doc.id);
						if (review != null)
						{
							if (review.status == en_ReviewStaus.UnReviewed)
							{

								ans.reviewDeadLine = review.deadline;

								if (doc.id_sp_status == en_file_Sp_Status.review_overdue)
									ans.isReviewDeadLineOverDue = true;
								else
									ans.isReviewDeadLineOverDue = false;

								List<User> users = UserController.GetUser(false, false, review.review_users.Select(a => a.id_user).ToArray());
								ans.reviewers = String.Join(", ", users.Select(a => a.name).ToArray());
							}
						}
					}
				}

			}
			catch (Exception ex)
			{
				logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(args));
			}

			return ans;
        }

    	[HttpPost]
        public Review GetReview(WebParams args)
        {
			try
			{
				Review ans = ReviewController.GetReview(args.DocumentId);

				if( ans == null || ans.status == en_ReviewStaus.Reviewed)
				{
					ans = new Review(args.DocumentId);

					return ans;
				}
				else
				{
					return ans ?? new Review(0);
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(args));
			}

			return new Review(0);
        }


		[HttpPost]
        public async System.Threading.Tasks.Task<Review> GetReviewAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run( () => GetReview(args));
		}


		[HttpPost]
        public List<User> GetReviewUsers(WebParams args)
        {
			List<User> ans = new List<User>();
			try
			{
				var doc = DocumentController.GetDocument(id_doc: args.DocumentId);

   				var possibleUsers = UserController.GetUser(true, true);

				foreach (var user in possibleUsers)
				{
					if(
						doc.IsActionAllowed(en_Actions.Review, user.id) &&
						doc.IsActionAllowed(en_Actions.OpenRead, user.id)
					)
					{
						ans.Add(user);
					}
				}

			}
			catch (Exception ex)
			{
				logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(args));
			}

			return ans;
        }

		[HttpPost]
        public async System.Threading.Tasks.Task<List<User>> GetReviewUsersAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run( () => GetReviewUsers(args));
		}


		[HttpPost]
		public List<User> GetUsersBy(WebParams args)
		{
			List<User> ans = new List<User>();
			try
			{
				if( args.idFolder > 0 )
				{
					List<int> AssignedUserId = PermissionController.GetAssignedUserOrGroupToFolder(en_FolderPermissionMode.User, args.idFolder);

					if (0 < AssignedUserId.Count)
						ans = UserController.GetUser(false, false, AssignedUserId.ToArray());
				}
				else
				{
					ans = UserController.GetUser(true, false);
				}
				for ( int i =0; i < ans.Count; i ++)
				{
					ans[i].password = "";
				}


			}
			catch (Exception ex)
			{
				logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(args));
			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<List<User>> GetUsersByAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetUsersBy(args));
		}


		[HttpPost]
		public List<User> GetUsers(WebParams args)
		{
			List<User> ans = new List<User>();
			try
			{

				var possibleUsers = UserController.GetUser(true, false);

				foreach (var user in possibleUsers)
				{
					// user.email = "";
					user.password = "";
					ans.Add(user);
				}

			}
			catch (Exception ex)
			{
				logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(args));
			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<List<User>> GetUsersAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetUsers(args));
		}

		[HttpPost]
		public List<DTS_System_permission_level> GetPermissionLevels(WebParams args)
		{
			List<DTS_System_permission_level> ans = new List<DTS_System_permission_level>();
			try
			{
				var DA_System_permission_level = new DTS_System_permission_level();
				var dt = DA_System_permission_level.GetDataTable();
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					DTS_System_permission_level ansstudent = new DTS_System_permission_level();
					ans.Add(new DTS_System_permission_level()
					{
						id = Convert .ToInt32 (dt.Rows[i]["id"]),
						permission = dt.Rows[i]["permission"].ToString(),
						obs = dt.Rows[i]["obs"].ToString()
					});
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(args));
			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<List<DTS_System_permission_level>> GetPermissionLevelsAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetPermissionLevels(args));
		}


		[HttpPost]
		public List<DTS_Group> GetGroups()
		{
			List<DTS_Group> ans = new List<DTS_Group>();
			try
			{
				var DA_Group = new DTS_Group();
				var dt = DA_Group.GetDataTable();
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					DTS_Group ansstudent = new DTS_Group();
					ans.Add(new DTS_Group()
					{
						id = Convert .ToInt32 (dt.Rows[i]["id"]),
						group_name = dt.Rows[i]["group_name"].ToString(),
						obs = dt.Rows[i]["obs"].ToString()
					});
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}

			return ans;
		}




		[HttpPost]
		public List<Group> GetGroupsBy(WebParams args)
		{

			List<Group> ans = new List<Group>();

			try
			{
                List<int> AssignedGroupsId = PermissionController.GetAssignedUserOrGroupToFolder(en_FolderPermissionMode.Group, args.idFolder);

				if(0 < AssignedGroupsId.Count)
					ans = GroupController.GetGroups(AssignedGroupsId.ToArray());

			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<List<Group>> GetGroupsByAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetGroupsBy(args));
		}


		[HttpPost]
		public async System.Threading.Tasks.Task<List<DTS_Group>> GetGroupsAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetGroups());
		}

		[HttpPost]
		public List<int> GetUserIdInGroup(WebParams args)
		{
			List<int> ans = new List<int>();

			try
			{
				ans = GroupController.GetUserIdInGroup(false, new int[] { args.GroupId });
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<List<int>> GetUserIdInGroupAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetUserIdInGroup(args));
		}


		[HttpPost]
		public bool DeleteUserGroup(int idGroup, int idUser)
		{
			bool ans = false;

			try
			{

				GroupController.DeleteUserGroup(idGroup, idUser);

				var ids = GroupController.GetUserIdInGroup(false, new int[] { idGroup });

				ans = ! ids.Exists(id => id == idUser);
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<bool> DeleteUserGroupAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => DeleteUserGroup(args.GroupId, args.UserId));
		}


		[HttpPost]
		public bool AssignGroup(int idGroup, int idUser)
		{
			bool ans = false;

			try
			{

				GroupController.AssignGroup(idGroup, idUser);

				var ids = GroupController.GetUserIdInGroup(false, new int[] { idGroup });

				ans = ids.Exists(id => id == idUser);
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<bool> AssignGroupAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => AssignGroup(args.GroupId, args.UserId));
		}


		[HttpPost]
        public Review StartReview(WebParams args)
        {
			Review ans = new Review(0);
			try
			{
				var review = new Review(args.DocumentId);
				review.id_version = args.VersionId;
				review.owner_comment = args.Comment;
				review.allow_checkout = args.allowCheckout;
				review.StartReview(WebSettings.CurrentUserId, args.UserIds, args.DeadLine);

				ans = ReviewController.GetReview(args.DocumentId);
			}
			catch (Exception ex)
			{
				logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(args));
			}

			return ans;
        }

		[HttpPost]
        public async System.Threading.Tasks.Task<Review> StartReviewAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run( () => StartReview(args));
		}


		[HttpPost]
        public Review FinishReview(WebParams args)
        {
			Review ans = new Review(0);

			try
			{
				Review currentReview = ReviewController.GetReview(args.DocumentId);

				ReviewUser review_user = currentReview.review_users.Find(a => a.id_user == WebSettings.CurrentUserId);
				review_user.FinalizeReview(args.VersionId, args.Comment);


				if (currentReview.IsAllUsersFinalized())
					currentReview.FinalizeReview(WebSettings.CurrentUserId);

				ans = ReviewController.GetReview(args.DocumentId);
			}
			catch (Exception ex)
			{
				logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(args));
			}

			return ans;
        }

		[HttpPost]
		public async System.Threading.Tasks.Task<Review> FinishReviewAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => FinishReview(args));
		}

		[HttpPost]
        public bool IsReviewOwner(WebParams args)
        {
			try
			{
				Review r = ReviewController.GetReview(args.DocumentId);

				if (r == null) return false;

				return r.owner_id == WebSettings.CurrentUserId;

			}
			catch (Exception ex)
			{
				logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(args));
			}

			return false;
        }

		[HttpPost]
		public async System.Threading.Tasks.Task<bool> IsReviewOwnerAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => IsReviewOwner(args));
		}


		[HttpPost]
        public List<ReviewHistory> GetReviewHistory(WebParams args)
		{
			List<ReviewHistory> ans = new List<ReviewHistory>();

			List<Review> ReviewHistory = ReviewController.GetReview(false, false, args.DocumentId);

			List<int> user_ids = new List<int>();
			foreach(Review review in ReviewHistory)
			{
				user_ids.Add(review.owner_id);
				user_ids.AddRange(review.review_users.Select(a => a.id_user).ToList());
			}

			List<User> Users = UserController.GetUser(false, false, user_ids.Distinct().ToArray());

			int serial = 0;
			foreach (Review review in ReviewHistory)
			{
				serial++;
				ans.Add(new ReviewHistory() { name = Users.Find(a => a.id == review.owner_id).name, note = "Start Review", comment = review.owner_comment });

				foreach (ReviewUser wrk in review.review_users)
				{
					serial++;

					string note = "";
					switch (wrk.action)
					{
						case en_ReviewAction.UnReviewed:
							note = "-";
							break;

						case en_ReviewAction.Start:
							note = "Start Review";
							break;

						case en_ReviewAction.PassOn:
							note = "Pass On";
							break;

						case en_ReviewAction.Finalize:
							note = "Finalize Review";
							break;
					}

					ans.Add(new ReviewHistory() { name = Users.Find(a => a.id == wrk.id_user).name, note = note, comment = wrk.comment });
				}

			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<List<ReviewHistory>> GetReviewHistoryAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetReviewHistory(args));
		}


		[HttpPost]
		public bool Test()
		{
			return true;
		}

		[HttpPost]
		public UserWorkSpace SaveFileToUserWorkSpace()
		{
            logger.Trace("Begin");
            string temp;
			var ans = new UserWorkSpace();

            using (var _utilists = new WebUtilities(tempFolder))
			{
                temp = _utilists.CreateTempFolder();

				temp = FileFolder.GetAvailableFileName(temp);
            }

            string filepath = temp;
            string fullpath = "";
            try
            {
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    fullpath = filepath + System.Web.HttpContext.Current.Request.Files[0].FileName;

                    System.Web.HttpContext.Current.Request.Files[0].SaveAs(fullpath);


					WorkSpaceFile newFile = new WorkSpaceFile(fullpath,temp);

					newFile.InitSync();

                    logger.Debug("Key:{0},SaveAs:{1}", System.Web.HttpContext.Current.Request.Files.AllKeys.Length, fullpath);

					ans =  WorkSpaceSyncController.GetBy(newFile.LinkId);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "{0}", Newtonsoft.Json.JsonConvert.SerializeObject(Request));
            }

			return ans;

		}

		[HttpPost]
		public string[] CreateDMSLink(int[] DocumentIds)
		{
            logger.Trace("Begin");
			var domainPath = System.Web.HttpRuntime.AppDomainAppPath;
			var tempPath = CreateWebTempFolder(System.IO.Path.Combine(domainPath, "temp", "dms"));

			var docs = DocumentController.GetDocument(id_doc: DocumentIds);

			if (docs.Count == 0) return new string[] { };

			var dmsLinks = SpiderDocsModule.DmsFile<Document>.SaveDmsFile(docs, tempPath).Select(x =>
			{
				return WebUtilities.ToWebURL(x, domainPath, System.Web.HttpContext.Current.Request.ApplicationPath, System.Web.HttpContext.Current.Request.Url);
			});


			return dmsLinks.ToArray();
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<string[]> CreateDMSLinkAsync(int[] DocumentIds)
		{
			logger.Trace("Begin");

			return await System.Threading.Tasks.Task.Run(() => CreateDMSLink(DocumentIds));
		}

		[HttpPost]
		public Document SearchByDMS()
		{
			logger.Trace("Begin");

			var ans = new Document();

			var files = System.Web.HttpContext.Current.Request.Files;
			int id;
			var savedPath = string.Empty;


			if( files.Count != 1 )
			{
				logger.Warn("Upload file must be one");
				return ans;
			}


			var file = files[0];

			logger.Debug("Paramters: {0}",file.FileName);

			try
			{
				// Place the file
				string filepath = FileFolder.GetTempFolder(), savePath = System.IO.Path.Combine(filepath, file.FileName);
				file.SaveAs( savePath );

				logger.Debug("SaveAs:{1},{2} byte",  savePath, new System.IO.FileInfo(savePath).Length);

				var dms = new DmsFile<Document>();
				dms.ReadDmsFile(savePath);

				if (int.TryParse(dms.GetVal(en_DmsFile.Id), out id))
				{
					ans = DocumentController.GetDocument(id);
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}

			return ans;
		}


		[HttpGet]
        public WebSpiderDocs.Models.SettingsViewModel GetSettings()
        {
			var _ = new WebSpiderDocs.Models.SettingsViewModel();

			_.Public = Cache.PublicSetting_Load();
			_.UserGlobal = new Cache(WebSettings.CurrentUserId).UserGlobalSetting_Load();
			_.WorkSpaceGridSize  = new Cache(WebSettings.CurrentUserId).cl_WorkspaceGridsize_Load();
			_.WorkspaceCustomize  = new Cache(WebSettings.CurrentUserId).cl_WorkspaceCustomize_Load();

            return _;
        }

		[HttpGet]
        public SpiderDocsModule.User GetMyProfile(int userId)
        {
			var user = UserController.GetUser(true, userId);

			return user;
        }

		[HttpGet]
		public async System.Threading.Tasks.Task<SpiderDocsModule.User> GetMyProfileAsync()
		{
			var userId = WebSettings.CurrentUserId;
			return await System.Threading.Tasks.Task.Run(() => GetMyProfile(userId));

		}


		[HttpPost]
		public string SendEmail(WebParams args)
		{
			var form  = args.EmailForm;

			var mailSetting = SpiderDocsModule.Cache.MailSettingss_Load();
			Spider.Net.Email email = new Spider.Net.Email(mailSetting.server);
			email.to.AddRange(form.To);
			email.subject = form.Subject;
			email.body = form.Body;

			if (form.CC.Count() > 0)
				email.cc.AddRange(form.CC);

			if (form.BCC.Count() > 0)
				email.bcc.AddRange(form.BCC);

			try
			{
				var filePaths = System.IO.Path.Combine(FileFolder.GetTempFolder(), args.TempId);

				DirectoryInfo d = new DirectoryInfo(filePaths);
				foreach (var file in d.GetFiles("*", SearchOption.AllDirectories))
				{
					if( file.Extension != ".sd")
					{
						email.attachments.Add(file.FullName);
					}
				}

				logger.Info("An email has been sent from {0} to {1}: {2}", mailSetting.server.User, string.Join(",", form.To), form.Subject);

				email.MultiThread = false;

				email.Send();
			}
			catch(Exception ex)
			{
				logger.Error(ex);
			}

			return string.Empty;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<string> SendEmailAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => SendEmail(args));
		}


		[HttpPost]
        public List<NotificationGroup> GetNotificationGroups(WebParams args)
        {
            List<NotificationGroup> groups = NotificationGroupController.GetGroups();

			return groups;
        }

		[HttpPost]
		public async System.Threading.Tasks.Task<List<NotificationGroup>> GetNotificationGroupsAsync(WebParams args)
		{
			if (args?.DocumentId > 0)
			{
				var docNotificationsGrp = await DocumentNotificationGroupController.SelectAsync(id_doc: args.DocumentId);

				if (docNotificationsGrp.Count > 0)
					return await NotificationGroupController.GetGroupsAsync(docNotificationsGrp.Select(x => x.id_notification_group).ToArray());
				else
					return new List<NotificationGroup>();
			}
			else
				return await System.Threading.Tasks.Task.Run(() => GetNotificationGroups(args));
		}






		[HttpPost]
        public bool UpdateDocumentTypeAttribute(int onIdDocType, int onIdAtb, bool onChkAtb, bool onChkNoDup, int[] AtbIds4Dup)
        {
			bool ans = false;
			int idType = onIdDocType;
			int id_atb = onIdAtb;
			bool chkAtb = onChkAtb;
			bool dupchk = onChkNoDup;

			int[] id_atbs = AtbIds4Dup;

			//const int atbIdx = 0, dupchkIdx = 3 ;

			// Handle checkbox state change here
			try
			{
				//var selectedRow = dragAndDropListView.Rows[e.RowIndex];
				//int id_atb = Convert.ToInt32(dragAndDropListView.Rows[e.RowIndex].Tag);
				//bool chkAtb = bool.Parse(selectedRow.Cells[atbIdx].Value.ToString());
				//bool dupchk = bool.Parse(selectedRow.Cells[dupchkIdx].Value.ToString());    //No Duplication


				//List<int> id_atbs = new List<int>();

				//for (int i = 0; i < dragAndDropListView.Rows.Count; i++)
				//{
				//	var row = dragAndDropListView.Rows[i];

				//	if ( bool.Parse(row.Cells[atbIdx].Value.ToString()) && bool.Parse(row.Cells[dupchkIdx].Value.ToString()))
				//		id_atbs.Add((int)dragAndDropListView.Rows[i].Tag);
				//}



				//List<int> typesOfAtb = DocumentAttributeController.GetIdListByDocType(doc_type_id: idType).Select(x => x.id_attribute).ToList();
				string atbs = string.Join(",", id_atbs/*typesOfAtb.Where(_id_atb => _id_atb != id_atb)*/);

				// can check off
				int dup_count = StoredProcedureController.canUnCheckDuplicate("", idType, atbs);
				bool canogo = dup_count == 0 || dup_count == 1;

				if(!canogo)
				{
					//MessageBox.Show(lib.msg_found_duplicate_docs, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Error);

					//populateAttributeExternal();
					return ans; //lib.msg_found_duplicate_docs
				}



				//delete
				DocumentTypeController.RemoveAttributeFromDocType(id_atb, idType);
				if(chkAtb == true )
				{
					//save
					DocumentTypeController.AssignAttributeToDocType(id_atb, idType, dupchk);
				}
				ans = true;
				return ans;
			}
			catch (Exception error)
			{
				logger.Error(error);
			}

			return ans;
        }



		[HttpPost]
		public async System.Threading.Tasks.Task<bool> UpdateDocumentTypeAttributeAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => UpdateDocumentTypeAttribute(args.DocumentTypeId,args.AttributeId,args.AttributeCheck,args.DuplicationCheck,args.AttributeIds));
		}




		[HttpPost]
        public List<CustomViewSource> GetCustomViewSources()
        {
			List<CustomViewSource> ans = new List<CustomViewSource>();
			try
			{
				// can check off
				ans = CustomViewSourceCtr.GetAll();
				ans = ans.Where(x => x.inactive == false).ToList();

			}
			catch (Exception error)
			{
				logger.Error(error);
			}

			return ans;
        }

		[HttpPost]
		public async System.Threading.Tasks.Task<List<CustomViewSource>> GetCustomViewSourcesAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetCustomViewSources());
		}



		[HttpPost]
        public List<CustomViewDocument> GetCustomViewDocuments(WebParams args)
        {
			List<CustomViewDocument> ans = new List<CustomViewDocument>();
			try
			{
				// can check off
				ans = CustomViewCtr.GetAllCustomViewDocuments(args.DocumentId);

			}
			catch (Exception error)
			{
				logger.Error(error);
			}

			return ans;
        }

		[HttpPost]
		public async System.Threading.Tasks.Task<List<CustomViewDocument>> GetCustomViewDocumentsAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetCustomViewDocuments(args));
		}

		[HttpPost]
        public List<CustomViewDocument> DeleteAllCustomViewDocument(WebParams args)
        {
			List<CustomViewDocument> ans = new List<CustomViewDocument>();

			try
			{
				CustomViewCtr.DeleteCustomViewDocument(args.DocumentId, SpiderDocsApplication.CurrentUserId);

				//foreach ( var view in CustomViewController.GetAllCustomViewDocuments(args.DocumentId) )
				//{
				//	CustomViewController.DeleteCustomViewDocument(view.id_doc, SpiderDocsApplication.CurrentUserId);
				//}

				ans = CustomViewCtr.GetAllCustomViewDocuments(args.DocumentId);
			}
			catch (Exception error)
			{
				logger.Error(error);
			}

			return ans;
        }

		[HttpPost]
		public async System.Threading.Tasks.Task<List<CustomViewDocument>> DeleteAllCustomViewDocumentAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => DeleteAllCustomViewDocument(args));
		}


		[HttpPost]
        public List<CustomView> GetCustomViews()
        {
			List<CustomView> ans = new List<CustomView>();
			try
			{
				// can check off
				ans = CustomViewCtr.GetAll();
			}
			catch (Exception error)
			{
				logger.Error(error);
			}

			return ans;
        }

		[HttpPost]
		public async System.Threading.Tasks.Task<List<CustomView>> GetCustomViewsAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetCustomViews());
		}

		[HttpPost]
        public CustomView DeleteCustomView(WebParams args)
        {
			CustomView ans = new CustomView();
			int id = 0;
			try
			{
				// can check off
				if ( args.CustomView.id > 0)
				{
					CustomViewCtr.Inactive(args.CustomView.id);
				}

				ans = CustomViewCtr.GetAll(true).Where( x => x.id == args.CustomView.id).FirstOrDefault() ?? new CustomView();


			}
			catch (Exception error)
			{
				logger.Error(error);
			}

			return ans;
        }

		[HttpPost]
		public async System.Threading.Tasks.Task<CustomView> DeleteCustomViewAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => DeleteCustomView(args));
		}

		[HttpPost]
        public CustomView SaveCustomView(WebParams args)
        {
			CustomView ans = new CustomView();
			int id = 0;
			try
			{

				// can check off
				if ( args.CustomView.id == 0)
				{
					args.CustomView.created_by = WebSettings.CurrentUserId;

					id = CustomViewCtr.InsertCustomView(args.CustomView);
				}
				else
				{
					id = CustomViewCtr.UpdateCustomView(args.CustomView);
				}
				if( id <= 0 )
				{
					logger.Error("Insertion failed.", JsonConvert.SerializeObject(args));
				}
				var vies = CustomViewCtr.GetAll();
				ans = vies.Where( x => x.id == id).FirstOrDefault() ?? new CustomView();

				if (ans.id == 0)
					ans = vies.Last();
			}
			catch (Exception error)
			{
				logger.Error(error);
			}

			return ans;
        }

		[HttpPost]
		public async System.Threading.Tasks.Task<CustomView> SaveCustomViewAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => SaveCustomView(args));
		}




		[HttpPost]
		public List<CustomViewSource> SaveCustomViewSource(WebParams args)
		{
			List<CustomViewSource> ans = new List<CustomViewSource>();
			int id = 0;
			try
			{
				// create external document_version
				//TODO: test
				if( !string.IsNullOrEmpty(args.CustomViewSource.data_source))
				{
					CustomViewSourceCtr.InitExternalSource(args.CustomViewSource.data_source, args.CustomViewSource.sql_mode);
				}

				// can check off
				if (args.CustomViewSource.id == 0)
				{
					args.CustomViewSource.created_by = WebSettings.CurrentUserId;

					id = CustomViewSourceCtr.InsertCustomViewSource(args.CustomViewSource);
				}
				else
				{
					id = CustomViewSourceCtr.UpdateCustomViewSource(args.CustomViewSource);
				}
				if (id <= 0)
				{
					logger.Error("Insertion failed.", JsonConvert.SerializeObject(args));
				}

				ans = CustomViewSourceCtr.GetAll();
			}
			catch (Exception error)
			{
				logger.Error(error);
			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<List<CustomViewSource>> SaveCustomViewSourceAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => SaveCustomViewSource(args));
		}


		[HttpPost]
		public CustomViewSource DeleteCustomViewSource(WebParams args)
		{
			CustomViewSource ans = new CustomViewSource();
			int id = 0;
			try
			{
				// can check off
				if (args.CustomViewSource.id > 0)
				{
					CustomViewSourceCtr.Inactive(args.CustomViewSource.id, SpiderDocsApplication.CurrentUserId);
				}

				ans = CustomViewSourceCtr.GetAll().Where(x => x.id == args.CustomViewSource.id && x.inactive == false).FirstOrDefault() ?? new CustomViewSource();


			}
			catch (Exception error)
			{
				logger.Error(error);
			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<CustomViewSource> DeleteCustomViewSourceAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => DeleteCustomViewSource(args));
		}



		/// <summary>
		/// Get user preferences
		/// Some of settings are cached.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		[HttpPost]
		public SpiderDocsModule.Preference GetPreference(WebParams args)
		{

			SpiderDocsModule.Preference ans = new SpiderDocsModule.Preference();

			try
			{
				// Remove cache first
				var cache = new Cache(WebSettings.CurrentUserId);				
				Cache.Remove(Cache.en_GKeys.DB_PublicSettings_Load);
				cache.Remove(Cache.en_UKeys.DB_UserGlobalSetting_Load);

				// Load Settings
				var currentPublicSettings = new PublicSettings();
				currentPublicSettings.Load();
				var currentUserGlobalSettings = new UserGlobalSettings(WebSettings.CurrentUserId);
				currentUserGlobalSettings.Load();

				ans.LoadFrom(new ApplicationSettings(), currentPublicSettings, currentUserGlobalSettings, SpiderDocsModule.Factory.Instance4UserSettins());

			}
			catch (Exception error)
			{
				logger.Error(error);
			}

			return ans;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<SpiderDocsModule.Preference> GetPreferenceAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => GetPreference(args));
		}


		/// <summary>
		/// Get user preferences
		/// Some of settings are cached.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		[HttpPost]
		public bool SavePreference(WebParams args)
		{

			try
			{

				// Load Settings
				// Load Settings
				var currentPublicSettings = new PublicSettings();
				currentPublicSettings.Load();
				var currentUserGlobalSettings = new UserGlobalSettings(WebSettings.CurrentUserId);
				currentUserGlobalSettings.Load();
				//var userSettings = SpiderDocsModule.Factory.Instance4UserSettins();

				//userSettings.autoLogin = args.Preference.AutoLogin;
				
				//if (!args.Preference.AutoLogin)
				//	userSettings.pass = "";
				
				currentUserGlobalSettings.ocr = args.Preference.OCR;
				
				/* TODO: Save it to database 
				 * userSettings.AutoStartup = args.Preference.AutoStartup;
				 * */

				currentUserGlobalSettings.show_import_dialog_new_mail = args.Preference.ShowImportDialogNewMail;
				currentUserGlobalSettings.exclude_archive = args.Preference.ExcludeArchive;
				currentUserGlobalSettings.default_ocr_import = args.Preference.DefaultOCRImport;
				currentUserGlobalSettings.default_pdf_merge = args.Preference.DefaultPDFMerge;
				currentUserGlobalSettings.enable_folder_creation_by_user = args.Preference.EnableFolderCreationByUser;

				currentUserGlobalSettings.double_click = (en_DoubleClickBehavior)args.Preference.DblClickBehavior;

				//userSettings.Save();
				currentUserGlobalSettings.Save();

				//int port2=0;
				//ApplicationSettings setting = new ApplicationSettings();
				//setting.UpdateServer2 = this.txtServer.Text;
				//if( int.TryParse(this.txtPort.Text, out port2)) setting.UpdateServerPort2 = port2;
				//setting.SaveAsJson();

				// Remove cache first
				var cache = new Cache(WebSettings.CurrentUserId);				
				Cache.Remove(Cache.en_GKeys.DB_PublicSettings_Load);
				cache.Remove(Cache.en_UKeys.DB_UserGlobalSetting_Load);

				return true;

			}
			catch (Exception error)
			{
				logger.Error(error);
			}

			return false;
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<bool> SavePreferenceAsync(WebParams args)
		{
			return await System.Threading.Tasks.Task.Run(() => SavePreference(args));
		}


		[HttpPost]
        public List<string> ExportAsPDF(int[] VersionIds)
        {
			List<string> exprted = new List<string>();

			logger.Debug("Ids:" + string.Join(",", VersionIds));
			try {
				var paths = Export(VersionIds);

				foreach (var path in paths)
				{
					// replace url to physical path.
					var systempath = Fn.ToFixSystemPath(Fn.ToFileSystemPath(path));
					string yeild = FileFolder.YeildNewFileName(systempath);

					var output = SpiderDocsForms.PDFConverter.pdfconversion(systempath, Path.Combine(Path.GetDirectoryName(yeild),Path.GetFileNameWithoutExtension(yeild) + ".pdf"));

					string uri = WebUtilities.GetCurrentUri(GetRootUriMode.Root, output);

					exprted.Add(uri);

				}
			}
			catch(Exception ex)
			{
				logger.Error(ex);
			}
			return exprted;
		}










		[HttpPost]
        public Document ToPdf(int[] VersionIds, Document Document)
        {
            string temp;
            using (var _utilists = new WebUtilities(tempFolder))
            {
                temp = _utilists.CreateTempFolder(postfix: Fn.Guid().ToString());
            }
                string
                        empty_pdf_path = Fn.MapPath("~/template/empty.pdf"),

                    filepath = temp,

                    uri = WebUtilities.GetCurrentUri(GetRootUriMode.Root, filepath),

                    dest = filepath + "merged.pdf",

                    destURL = uri + "merged.pdf"; ;

                List<string> exportedURLs = Export(VersionIds);

                using (FileStream stream = new FileStream(dest, FileMode.Create))
                using (iTextSharp.text.Document doc2 = new iTextSharp.text.Document())
                using (iTextSharp.text.pdf.PdfCopy pdf = new iTextSharp.text.pdf.PdfCopy(doc2, stream))
                {
                    doc2.Open();

                    iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(empty_pdf_path);
                    iTextSharp.text.pdf.PdfImportedPage page = null;

                    exportedURLs.ForEach(url =>
                    {

                        byte[] binary;

                        try
                        {
                            binary = new WebClient().DownloadData(url);

                            switch (Fn.Extension(url))
                            {
                                case "jpg":
                                case "jpeg":
                                case "png":
                                case "gif":

                                    string output = Fn.Img2PdfPlacedTmp(binary);

                                    reader = new iTextSharp.text.pdf.PdfReader(System.IO.File.ReadAllBytes(output));

                                    for (int i = 0; i < reader.NumberOfPages; i++)
                                    {
                                        page = pdf.GetImportedPage(reader, i + 1);

                                        pdf.AddPage(page);
                                    }

                                    break;

                                case "pdf":

                                    reader = new iTextSharp.text.pdf.PdfReader(binary);
                                    for (int i = 0; i < reader.NumberOfPages; i++)
                                    {
                                        page = pdf.GetImportedPage(reader, i + 1);
                                        pdf.AddPage(page);
                                    }

                                    break;

                                case "txt":
                                    string output2 = Fn.Txt2PdfPlacedTmp(System.Text.Encoding.Default.GetString(binary));

                                    reader = new iTextSharp.text.pdf.PdfReader(System.IO.File.ReadAllBytes(output2));

                                    for (int i = 0; i < reader.NumberOfPages; i++)
                                    {
                                        page = pdf.GetImportedPage(reader, i + 1);

                                        pdf.AddPage(page);
                                    }
                                    break;
                            }
                        }
                        catch { }
                    });

                    pdf.FreeReader(reader);

                    reader.Close();
                }

            Document saved = RemoteImport(destURL, Document);

            return saved;
        }


		SearchCriteria excludeArchive(SearchCriteria criteria)
		{
			logger.Trace("Begin");

			criteria.ExcludeStatuses.Add(en_file_Status.archived);
			criteria.ExcludeStatuses.Add(en_file_Status.deleted);

			return criteria;
		}

		// public class DeleteUserWorkSpaceAPIModel
		// {
		// 	public int[] DocumentIds;
		// 	public string Reason;
		// }


		public class WebParams
		{
			public int Id;
			public int[] DocumentIds;
			public int[] UserWorkSpaceIds;
			public string Reason;
			public int idParent;
			public System.Web.HttpPostedFile file1;
			public int DocumentId;
			public int[] DocIds;
			public int VersionId;
			public Document Document;
			public string NewName;
			public en_Actions Permission;
			public string TempId;
			public EmailForm EmailForm;
			public Options Options;
			public int idFolder;
			public int UserId;
			public List<int> UserIds;
			public string Comment;
			public DateTime DeadLine;
			//public Review Review;
			public bool allowCheckout;
			public User User;
			public string LoginName;
			public string Password;
			public DocumentAttribute Attribute;
			public int[] FolderIds;
			public Group Group;
			public int GroupId;
			public bool Footer;

			public int DocumentTypeId;
			public int AttributeId;
			public bool AttributeCheck;
			public bool DuplicationCheck;
			public int[] AttributeIds;
			public DocumentType DocumentType;

			public Dictionary<en_Actions, en_FolderPermission> Permissions;

			public CustomView CustomView;
			public CustomViewSource CustomViewSource;

			public Preference Preference;



		}

		public class EmailForm
		{
			public string Subject ="" ;
			public string[] To = new string[] { };
			public string[] CC = new string[] { };
			public string[] BCC = new string[] { };
			public string Body = "";
			public string[] Attachments = new string[] { };
		}
		public class Options
		{
			public bool SaveAsPDF = false;
		}

		// private string ConvertToLetter(int version)
		// {

		//     int alpha, remainder;
		//     string str = "";

		//     if (version > 0)
		//     {
		//         alpha = (int)(version / 27);

		//         remainder = version - (alpha * 26);

		//         if (alpha > 0)
		//         {
		//             str = ((char)(alpha + 64)).ToString();
		//         }

		//         if (remainder > 0)
		//         {
		//             str = (str + (char)(remainder + 64));
		//         }
		//     }

		//     return str;

		// }

		//---------------------------------------------------------------------------------
		string CreateWebTempFolder(string tempPath = "")
		{
			string result = string.Empty;

			using (var _utilists = new WebUtilities(tempPath))
			{
				result = _utilists.CreateTempFolder();

				result = FileFolder.GetAvailableFileName(result);
			}

			return result;
		}
	}
}
