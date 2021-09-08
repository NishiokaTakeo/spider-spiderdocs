// Note: You need to add dummy function if Actual function has arguments.
//
// Example:
// If you want to add function like below...
// [HttpPost]
// public ActionResult GetDocuments(SearchCriteria Criteria)
//
// You also need to add the following dummy function which does not have "[HttpPost]" and arguments.
// public ActionResult GetDocuments()
// {
//		return null;
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using SpiderDocsModule;
using System.Net;
using NLog;
using Newtonsoft.Json;
using WebSpiderDocs.ViewModels;
using System.Net.Http;
using System.Web.Http.Results;

//---------------------------------------------------------------------------------
namespace WebSpiderDocs.Controllers
{
    public class ExternalController : BaseController
    {
        public Crypt crypt = new Crypt();
        static Logger logger = LogManager.GetCurrentClassLogger();
        Api.ExternalController apiCtr = new Api.ExternalController();

        //---------------------------------------------------------------------------------
        public ExternalController() : base()
        {
        }

        //---------------------------------------------------------------------------------
        public ActionResult GetDocuments() // dummy function
        {
            return null;
        }

        [HttpPost]
        public JsonResult GetDocuments(SearchCriteria Criteria)
        {
            
            JsonResult json = Json(new { Documents = apiCtr.GetDocuments(Criteria) });

            json.MaxJsonLength = int.MaxValue;

            return json;
        }
		[HttpPost]
		public JsonResult GetRecentDocuments()
		{
			JsonResult json = Json(new { Documents = apiCtr.GetRecentDocuments() });

			json.MaxJsonLength = int.MaxValue;

			return json;
		}

		[HttpPost]
        public JsonResult Export(int[] VersionIds)
        {           
            return Json(new { Urls = apiCtr.Export(VersionIds) });
        }

        /// <summary>
        /// Get binary content by version id.
        /// Use this method when you want to show content in web page
        /// </summary>
        /// <param name="VersionId"></param>
        /// <returns>byte[] with propre contenttype</returns>
        [AcceptVerbs("GET"), RequireParameter("VersionId")]
        public FileContentResult GetContent(int VersionId)
        {
            byte[] binary = apiCtr._GetContent(VersionId, out string url);

            if(Fn.ForceDLExtension(url))
                return File(binary, Fn.TypeByExtnt(url),System.IO.Path.GetFileName(url));
            else
                return File(binary, Fn.TypeByExtnt(url)/*,System.IO.Path.GetFileName(url)*/);            
        }

        [AcceptVerbs("GET"), RequireParameter("DocumentId")]
        public FileContentResult GetContent(int DocumentId, string Version = "")
        {
            byte[] binary = apiCtr._GetContent(DocumentId, out string url, Version);

            //return File(binary, Fn.TypeByExtnt(url), System.IO.Path.GetFileName(url));
            if (Fn.ForceDLExtension(url))
                return File(binary, Fn.TypeByExtnt(url), System.IO.Path.GetFileName(url));
            else
                return File(binary, Fn.TypeByExtnt(url)/*,System.IO.Path.GetFileName(url)*/);
        }

        //---------------------------------------------------------------------------------
        [HttpPost]
        public JsonResult UploadFile()
        {
            return Json(apiCtr.UploadFile());
        }

        //---------------------------------------------------------------------------------
        public ActionResult UploadRemoteFile() // dummy function
        {
            return null;
        }

        [HttpPost]
        public JsonResult UploadRemoteFile(string strURL)
        {
        
            return Json(apiCtr.UploadRemoteFile(strURL));
        }

        //---------------------------------------------------------------------------------
        public ActionResult Import() // dummy function
        {
            return null;
        }

        [HttpPost]
        public JsonResult Import(string TempId, Document Document)
        {
            logger.Trace("Begin");

            return Json(apiCtr.Import(TempId,Document));
        }

        [HttpPost]
        public JsonResult SaveDoc(string TempId, Document Document/*, bool Property = false*/)
        {
            return Json(apiCtr.SaveDoc(TempId,Document/*, Property*/));            
        }

        /// <summary>
        /// Import by Remote URL at onece
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="Document"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RemoteImport(string Url, Document Document, List<DocumentAttributeCombo> TextCombo = null)
        {

            return Json(apiCtr.RemoteImport(Url,Document,TextCombo));
        }

        private T rspnsData<T>(JsonResult r)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(r.Data));
        }

        //---------------------------------------------------------------------------------
        //@@Mori: webapi
        public ActionResult UpdateProperty() // dummy function
        {
            return null;
        }

        [HttpPost]
        public ActionResult UpdateProperty(Document Document)
        {
            
            return Json(apiCtr.UpdateProperty(Document));
        }

        [HttpPost]
        public ActionResult SaveLinkedAttribute(int keyID, string keyValue, DocumentAttribute linkedAttr)
        {
            return Json(apiCtr.SaveLinkedAttribute(keyID,keyValue, linkedAttr));
        }
        
        [HttpPost]
        public ActionResult Archive(int[] DocIds)
        {
            return Json(apiCtr.Archive(DocIds));
        }

        [HttpPost]
        public ActionResult UnArchive(int[] DocIds)
        {
            return Json(apiCtr.UnArchive(DocIds));
        }

        //---------------------------------------------------------------------------------
        public ActionResult Delete() // dummy function
        {
            return null;
        }

        [HttpPost]
        public ActionResult Delete(int[] DocumentIds, string Reason)
        {
            return Json(apiCtr.Delete(DocumentIds,Reason));
        }

        //---------------------------------------------------------------------------------
        [HttpPost]
        public ActionResult GetFolders(int[] FolderIDs = null)
        {
			List<Folder> folders = new List<Folder>();

			if ( FolderIDs == null)
				folders = apiCtr.GetFolders();
			else
				folders = apiCtr.GetFolders(FolderIDs);

			var json = Json(new { Folders = folders.ToArray() });
            json.MaxJsonLength = int.MaxValue;
            return json;
		}

		[HttpPost]
		public ActionResult GetFoldersL1(int idParent)
		{
			List<Folder> folders = apiCtr.GetFoldersL1(idParent);

			var json = Json(new { Folders = folders.ToArray() });
			json.MaxJsonLength = int.MaxValue;

			return json;
		}

		[HttpPost]
        public ActionResult GetFoldersL2(int idParent)
        {
            List<Folder> folders = apiCtr.GetFoldersL2(idParent);

            var json = Json(new { Folders = folders.ToArray() });
            json.MaxJsonLength = int.MaxValue;

            return json;
        }

        [HttpPost]
        public ActionResult SaveFolder(Folder folder)
        {
            //return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, "Unauthorized");

            folder = apiCtr.SaveFolder(folder);

            return Json(new { Folder = folder });

            //logger.Debug("id:{0},document_folder:{1},id_parent:{2}", folder.id, folder.document_folder, folder.id_parent);

            //var result = apiCtr.SaveFolder(folder);
            
            //var contentResult = result as OkNegotiatedContentResult<Folder>;
            
            //if ( !typeof(OkResult).IsInstanceOfType(result))
            //    return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, "InternalServerError");
            //else
            //    return Json(new { Folder = contentResult.Content });
        }

        //---------------------------------------------------------------------------------
        [HttpPost]
        public ActionResult GetDocumentTypes()
        {
            
            List<DocumentType> types = apiCtr.GetDocumentTypes();
            
            return Json(new { DocumentTypes = types.ToArray() });
        }

        //---------------------------------------------------------------------------------
        [HttpPost]
        public ActionResult GetAttributes()
        {
            List<DocumentAttribute> attrs = apiCtr.GetAttributes();
            
            return Json(new { Attributes = attrs.ToArray() });
        }
        
        [AcceptVerbs("GET")]
        public ActionResult GetAttributeDocType(int[] DocTypeIds)
        {
            List<AttributeDocType> attrs = apiCtr.GetAttributeDocType(DocTypeIds);

            return Json(attrs.ToArray(), JsonRequestBehavior.AllowGet);
        }

        //---------------------------------------------------------------------------------
        public ActionResult EditAttributeComboboxItem()  // dummy function  
        {
            return null;
        }

        [HttpPost]
        public JsonResult EditAttributeComboboxItem(int AttributeId, DocumentAttributeCombo Item)
        {

            int ans = apiCtr.EditAttributeComboboxItem(AttributeId,Item);
            
            return Json(ans);
        }

        //---------------------------------------------------------------------------------
        public ActionResult GetAttributeComboboxItems()  // dummy function
        {
            return null;
        }

        [HttpPost, RequireParameter("AttributeId", "ItemId")]
        public ActionResult GetAttributeComboboxItems(int AttributeId = 0, int ItemId = 0)
        {
            List<DocumentAttributeCombo> ComboboxItems = apiCtr.GetAttributeComboboxItems(AttributeId,ItemId);

            return Json(new { ComboboxItems = ComboboxItems.ToArray() });
        }

        [HttpPost, RequireParameter("Text","AttributeId")]
        public ActionResult GetAttributeComboboxItems(int AttributeId = 0, string Text = "")
        {
            List<DocumentAttributeCombo> ComboboxItems = apiCtr.GetAttributeComboboxItems(AttributeId,Text);

            return Json(new { ComboboxItems = ComboboxItems.ToArray()});

        }


        [HttpPost]
        public ActionResult GetUser(string UserName, string Password)
        {
            User user = apiCtr.GetUser(UserName,Password);

            return Json(new { User = user });
        }

        [HttpPost]
        public ActionResult UpdateUser(User user, string UserName, string Password)
        {
            bool success = apiCtr.UpdateUser(user,UserName,Password);

            return Json(new { User = success });
        }

        
        [HttpPost]
        public ActionResult GetHistories(SearchCriteria Criteria)
        {
            List<History> histories = apiCtr.GetHistories(Criteria);

            return Json(new { Histories = histories.ToArray() });
        }

        [HttpPost]
        public JsonResult ToPdf(int[] VersionIds, Document Document)
        {
            Document doc = apiCtr.ToPdf(VersionIds,Document);
            
            return Json(new { Document = doc});
        }

        /// <summary>
        /// Check out documents. 
        /// The ordered FolderIds and DocIds must be paired. 
        /// </summary>
        /// <param name="FolderIds">id of folders.</param>
        /// <param name="DocIds">id of documents</param>
        /// <returns>Successed:empty, other will be error</returns>
        [HttpPost]
        public ActionResult CheckOut(int[] DocIds)
        {
            return Json(apiCtr.CheckOut(DocIds).Result);
        }

		///// <summary>
		///// Check out documents. 
		///// The ordered FolderIds and DocIds must be paired. 
		///// </summary>
		///// <param name="FolderIds">id of folders.</param>
		///// <param name="DocIds">id of documents</param>
		///// <returns>Successed:empty, other will be error</returns>
		//[HttpPost]
		//public ActionResult CheckOut(int[] DocIds, bool Footer)
		//{
		//	return Json(apiCtr.CheckOut(DocIds, Footer).Result);
		//}


		/// <summary>
		/// Check out documents. 
		/// The ordered FolderIds and DocIds must be paired. 
		/// </summary>
		/// <param name="FolderIds">id of folders.</param>
		/// <param name="DocIds">id of documents</param>
		/// <returns>Successed:empty, other will be error</returns>
		[HttpPost]
        public ActionResult CancelCheckOut(int[] DocIds)
        {
            return Json(apiCtr.CancelCheckOut(DocIds));
        }
		[HttpPost]
//		[Authorize]

		public ActionResult Test()
		{
			return Json(apiCtr.Test());
		}
		public class RequireParameterAttribute : ActionMethodSelectorAttribute
        {
            public RequireParameterAttribute(params string[] valueName)
            {
                ValueName = valueName;
            }
            public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
            {
                IEnumerable<string> exists;
                string contenttype = controllerContext.RequestContext.HttpContext.Request.ContentType;

                if (contenttype.Trim().Contains("application/json"))
                {                    
                    Stream req = controllerContext.RequestContext.HttpContext.Request.InputStream;
                    req.Seek(0, System.IO.SeekOrigin.Begin);
                    string json = new StreamReader(req).ReadToEnd();
                    Dictionary<string, string> root = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, string>>(json);                    

                    req.Seek(0, System.IO.SeekOrigin.Begin);

                    exists = ValueName.Where(x => root.ContainsKey(x));

                }
                else
                {
                    exists = ValueName.Where(x => !string.IsNullOrEmpty(controllerContext.RequestContext.HttpContext.Request[x]));                    
                }

                bool isMatched =  ValueName.Count() == exists.Count();

                return isMatched;
            }

            public string[] ValueName { get; private set; } = new string[] { };

        }

        //---------------------------------------------------------------------------------
    }
}
