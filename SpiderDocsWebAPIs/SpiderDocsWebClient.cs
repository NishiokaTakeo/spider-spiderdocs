using Newtonsoft.Json;
using NLog;
using RestSharp;
using SpiderDocsModule;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;


namespace SpiderDocsWebAPIs
{
    /* Last Update : 2018/05/31
    ----------- Release Note ----------- 
    2.1.1 : Add Property Method 
    3.0.0 : LoginID and LoginPassword is in Interface method 
    3.0.1 : Add logger handling when excute method causes an Exception 
    3.1.0 : Add new method EditAttributeComboboxItem, GetAttributeComboboxItems
    3.2.0 : Add new method SaveDoc(string filepath, Document doc, out int id_doc)
    */
    public class SpiderDoscWebClient
    {
        public interface IConfiguration
        {
            string LoginID();
            string LoginPassword();
            string GetServerURL();
            Logger GetLogger();
            Func<DbManager> GetDbManager();

            int NoAuth(); //return id of user that you want to similate logged in user.
        }

        public RestClient Client;
        public IRestResponse LastResponse;

        Logger _logger;
        string _serverURL { get; set; }

        IConfiguration IConfig;

        public User LoginedUser
        {
            get;
            set;
        }

        public SpiderDoscWebClient(IConfiguration iConfig)
        {
            IConfig = iConfig;
            _serverURL = iConfig.GetServerURL();
            Client = new RestClient(_serverURL); //"http://localhost:50920"

            SqlOperation.MethodToGetDbManager = iConfig.GetDbManager();

            _logger = IConfig.GetLogger();

            if ( IConfig.NoAuth() == 0)
            { 
                if (LoginedUser == null)
                    login();
            }
            else
            {
                LoginedUser = new SpiderDocsModule.User() { id = IConfig.NoAuth() };
            }
        }
               
        public IConfiguration Configration()
        {
            return IConfig;
        }

        string requestPath(string path)
        {
            _logger.Trace("Begining of Path");
            if (LoginedUser == null)
            {
                _logger.Warn("This user is not logined");
            }
            return string.Format("/External/{0}/{1}", path, LoginedUser.id);
            //return string.Format("spiderdocs/External/{0}/{1}", path, LoginedUser.id);
            //return string.Format("spiderdocs/External/{0}/{1}", path, "16");
        }
        public void login()
        {


            using (MD5 md5Hash = MD5.Create())
            {
                //string hash = GetMd5Hash(md5Hash, "Welcome1");
                string hash = GetMd5Hash(md5Hash, IConfig.LoginPassword());
                
                var credential = new
                {
                    UserName = IConfig.LoginID(),
                    Password_Md5 = hash
                };

                //var request = new RestRequest("spiderdocs/Users/Login", Method.POST);
                var request = new RestRequest("/Users/Login", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(credential);

                IRestResponse r = Execute(request);

                string preformat = Regex.Replace(r.Content, "^{\"User\":(.*)}$", "$1");


                LoginedUser = JsonConvert.DeserializeObject<User>(preformat);
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public Document[] GetDocument(SearchCriteria c)
        {

            var request = new RestRequest(requestPath("GetDocuments"), Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddParameter("Application/Json", JsonConvert.SerializeObject(c), ParameterType.RequestBody);
            LastResponse = Execute(request);

            string preformat = Regex.Replace(LastResponse.Content, "^{\"Documents\":(.*)}$", "$1");

            Document[] d = JsonConvert.DeserializeObject<Document[]>(preformat);

            return d;
        }

        public Document[] GetDocument(Document doc)
        {
            return this.GetDocument(convert_document2search_criteria(doc));
        }

        /// <summary>
        /// Create/Update Combobox Item. 
        /// This can also create child combobox.
        /// </summary>
        /// <param name="AttributeId"></param>
        /// <param name="Item"></param>
        /// <returns>attribute_combo_item.id</returns>
        public int EditAttributeComboboxItem(int AttributeId, DocumentAttributeCombo Item)
        {
            IConfig.GetLogger().Trace("[Begin] AttributeId:{0}, Item:{1}", AttributeId,Item.text);

            var request = new RestRequest(requestPath("EditAttributeComboboxItem"), Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddBody(new { AttributeId = AttributeId, Item  = Item });

            LastResponse = Execute(request);

            string preformat = Regex.Replace(LastResponse.Content, "^{?(.*)}?$", "$1");

            int d = JsonConvert.DeserializeObject<int>(preformat);

            IConfig.GetLogger().Debug("[Success] ComboboxID:{0}", d);

            return d;
        }

        public DocumentAttributeCombo[] GetAttributeComboboxItems(int AttributeId = 0, string Text = "")
        {

            var request = new RestRequest(requestPath("GetAttributeComboboxItems"), Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("AttributeId", AttributeId);
            request.AddParameter("Text", Text);

            //
            LastResponse = Execute(request);

            string preformat = Regex.Replace(LastResponse.Content, "^{\"ComboboxItems\":(.*)}$", "$1");

            DocumentAttributeCombo[] d = JsonConvert.DeserializeObject<DocumentAttributeCombo[]>(preformat);

            return d;
        }
        

        public string[] GetDownloadUrls(int[] versionIds)
        {

            var request = new RestRequest(requestPath("Export"), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { VersionIds = versionIds });
            LastResponse = Execute(request);

            string preformat = Regex.Replace(LastResponse.Content, "^{\"Urls\":(.*)}$", "$1");

            string[] dls = JsonConvert.DeserializeObject<string[]>(preformat);

            return dls;
        }


        public bool SaveDoc(string filepath, Document doc, out Document result)
        {
            result = new Document();

            //Upload File
            var request = new RestRequest(requestPath("UploadFile"), Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddFile("fileData", file_get_byte_contents(filepath), filepath.Split(new Char[] { '\\', '/' }).Last());
            IRestResponse response = Execute(request);

            string tempID = Regex.Replace(response.Content, "\"(.*)\"", "$1");


            //add attributes
            request = new RestRequest(requestPath("SaveDoc"), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("Application/Json", JsonConvert.SerializeObject(new { TempId = tempID, Document = doc, Property = true }), ParameterType.RequestBody);
            response = Execute(request);

            //string preformat = Regex.Replace(response.Content, "\"(.*)\"", "$1");

            result = JsonConvert.DeserializeObject<Document>(response.Content);

            if (result.id <= 0)
                IConfig.GetLogger().Error("Saving failed :{0}", JsonConvert.SerializeObject(new { TempId = tempID, Document = doc }));

            return result.id > 0;
        }

        //public bool SaveDoc(string filepath, Document doc, out int id_doc)
        //{
        //    id_doc = 0;

        //    bool isSuccess = SaveDoc(filepath, doc);


        //    if (!isSuccess) return isSuccess;


        //    SearchCriteria criteria = convert_document2search_criteria(doc);

        //    Document stored = GetDocument(criteria).OrderByDescending(d => d.id_latest_version).FirstOrDefault();

        //    if (stored == null) throw new Exception("Document not found.");

        //    id_doc = stored.id;

        //    return isSuccess;
        //}

        ///// <summary>
        ///// Save Document and return saved document with filling id
        ///// Also update property, title and etc...
        ///// </summary>
        ///// <param name="filepath"></param>
        ///// <param name="doc"></param>
        ///// <param name="result"></param>
        ///// <returns></returns>
        //public bool SaveDoc(string filepath, Document doc, out Document result)
        //{
        //    result = new Document();

        //    bool isSuccess = SaveDoc(filepath, doc);

        //    if (!isSuccess) return isSuccess;

        //    isSuccess = UpdateProperty(doc);

        //    if (!isSuccess) return isSuccess;

        //    SearchCriteria criteria = convert_document2search_criteria(doc);

        //    result = GetDocument(criteria).OrderByDescending(d => d.id_latest_version).FirstOrDefault();

        //    if (result == null) throw new Exception("Document not found.");

        //    return isSuccess;
        //}

        /// <summary>
        /// Check out documents
        /// </summary>
        /// <param name="DocIds"></param>
        /// <param name="FolderIds"></param>
        /// <returns></returns>
        public bool CheckOut(int[] DocIds/*, int[] FolderIds*/)
        {
            var request = new RestRequest(requestPath("CheckOut"), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { DocIds = DocIds/*, FolderIds = FolderIds */});
            LastResponse = Execute(request);

            string res_message = Regex.Replace(LastResponse.Content, "\"(.*)\"", "$1");

            return string.IsNullOrWhiteSpace(res_message);
        }

        /// <summary>
        /// Check out documents. 
        /// The ordered FolderIds and DocIds must be paired. 
        /// </summary>
        /// <param name="FolderIds">id of folders.</param>
        /// <param name="DocIds">id of documents</param>
        /// <returns>Successed:empty, other will be error</returns>
        public bool CancelCheckOut(int[] DocIds)
        {
            var request = new RestRequest(requestPath("CancelCheckOut"), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { DocIds = DocIds });
            LastResponse = Execute(request);

            string res_message = Regex.Replace(LastResponse.Content, "\"(.*)\"", "$1");

            return string.IsNullOrWhiteSpace(res_message);
        }

        public bool Archive(int[] DocIds)
        {
            var request = new RestRequest(requestPath("Archive"), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { DocIds = DocIds });
            LastResponse = Execute(request);

            string res_message = Regex.Replace(LastResponse.Content, "\"(.*)\"", "$1");

            return string.IsNullOrWhiteSpace(res_message);
        }

        public bool UnArchive(int[] DocIds)
        {
            var request = new RestRequest(requestPath("UnArchive"), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { DocIds = DocIds });
            LastResponse = Execute(request);

            string res_message = Regex.Replace(LastResponse.Content, "\"(.*)\"", "$1");

            return string.IsNullOrWhiteSpace(res_message);
        }

        public History[] GetHistories(SearchCriteria Criteria)
        {
            var request = new RestRequest(requestPath("GetHistories"), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { Criteria  = Criteria });
            LastResponse = Execute(request);

            string preformat = Regex.Replace(LastResponse.Content, "^{\"Histories\":(.*)}$", "$1");

            History[] histories = JsonConvert.DeserializeObject<History[]>(preformat);

            return histories;
        }
        


        public bool Delete(int[] DocumentIds, string Reason)
        {
            var request = new RestRequest(requestPath("Delete"), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { DocumentIds = DocumentIds, Reason= Reason });
            LastResponse = Execute(request);

            string res_message = Regex.Replace(LastResponse.Content, "\"(.*)\"", "$1");

            return string.IsNullOrWhiteSpace(res_message);
        }

        public bool UpdateProperty(Document doc)
        {

            var request = new RestRequest(requestPath("UpdateProperty"), Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddParameter("Application/Json", JsonConvert.SerializeObject(new { Document = doc }), ParameterType.RequestBody);
            LastResponse = Execute(request);

            string res_message = Regex.Replace(LastResponse.Content, "\"(.*)\"", "$1");

            return (string.IsNullOrWhiteSpace(res_message));

        }

        public bool SaveLinkedAttribute(int keyID, string keyValue, DocumentAttribute linkedAttr)
        {

            var request = new RestRequest(requestPath("SaveLinkedAttribute"), Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddParameter("Application/Json", JsonConvert.SerializeObject(new { keyID, keyValue, linkedAttr }), ParameterType.RequestBody);
            LastResponse = Execute(request);

            string res_message = Regex.Replace(LastResponse.Content, "\"(.*)\"", "$1");

            return (string.IsNullOrWhiteSpace(res_message));

        }

        public List<DocumentAttributeCombo> GetAttributeComboboxItems(int idAttr = 0, int idItem = 0)
        {
            var request = new RestRequest(requestPath("GetAttributeComboboxItems"), Method.POST);
            //request.RequestFormat = DataFormat.Json;
            request.AddParameter("AttributeId", idAttr);
            request.AddParameter("ItemId", idItem);
            //request.AddBody(new { AttributeId = idAttr, ItemId = idItem });
            LastResponse = Execute(request);

            string preformat = Regex.Replace(LastResponse.Content, "^{\"ComboboxItems\":(.*)}$", "$1");

            List<DocumentAttributeCombo> dls = JsonConvert.DeserializeObject<List<DocumentAttributeCombo>>(preformat);

            return dls;


            /*
            var request = new RestRequest(requestPath("GetAttributeComboboxItems"), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { AttributeId = idAttr, ItemId = idItem });
            LastResponse = Execute(request);

            string preformat = Regex.Replace(response.Content, "^{\"ComboboxItems\":(.*)}$", "$1");

            List<DocumentAttributeCombo> dls = JsonConvert.DeserializeObject<List<DocumentAttributeCombo>>(preformat);

            return dls;
            */
        }
        
        public Document ToPDFBy(int[] versionIds, Document doc)
        {

            var request = new RestRequest(requestPath("ToPdf"), Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddParameter("Application/Json", JsonConvert.SerializeObject(new { VersionIds = versionIds, Document = doc }), ParameterType.RequestBody);
            LastResponse = Execute(request);

            string preformat = Regex.Replace(LastResponse.Content, "\"(.*)\"", "$1");

            Document d = JsonConvert.DeserializeObject<Document>(preformat);

            return d;
        }

        public string GetContentURLA(int id_version)
        {
            if (id_version == 0) return string.Empty;

            string url = GetDownloadUrls(new int[] { id_version }).FirstOrDefault() ?? "";

            if (string.IsNullOrEmpty(url)) return id_version.ToString();

            string link = $"<a href=\"{IConfig.GetServerURL()}{requestPath("GetContent")}?VersionId={id_version}\">{Path.GetFileName(url)}</a>";

            return link;
        }

        public string GetContentURL(int id_version)
        {
            if (id_version == 0) return string.Empty;

            string url = GetDownloadUrls(new int[] { id_version }).FirstOrDefault() ?? "";

            if (string.IsNullOrEmpty(url)) return id_version.ToString();

            string link = $"{IConfig.GetServerURL()}{requestPath("GetContent")}?VersionId={id_version}";

            return link;
        }

        public Folder SaveFolder(Folder folder)
        {
            var request = new RestRequest(requestPath("SaveFolder"), Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddParameter("Application/Json", JsonConvert.SerializeObject(folder), ParameterType.RequestBody);
            LastResponse = Execute(request);

            if (LastResponse.StatusCode != HttpStatusCode.OK) return new Folder();

            string json = Regex.Replace(LastResponse.Content, "^{\"Folder\":(.*)}$", "$1");

            Folder dls = JsonConvert.DeserializeObject<Folder>(json);

            return dls;
        }

        public List<Folder> GetFoldersL1(int idParent)
        {
            var request = new RestRequest(requestPath("GetFoldersL1"), Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddParameter("Application/Json", JsonConvert.SerializeObject(new { idParent = idParent }), ParameterType.RequestBody);
            LastResponse = Execute(request);

            string json = Regex.Replace(LastResponse.Content, "^{\"Folders\":(.*)}$", "$1");

            List<Folder> dls = JsonConvert.DeserializeObject<List<Folder>>(json);

            return dls;
        }

        SearchCriteria convert_document2search_criteria(Document doc)
        {
            // Here attribute 
            AttributeCriteriaCollection attrs = new AttributeCriteriaCollection();
            doc.Attrs.ForEach(attr => { attrs.Add(attr); });

            SearchCriteria criteria = new SearchCriteria()
            {
                FolderIds = new List<int> { doc.id_folder },
                DocTypeIds = new List<int> { doc.id_docType },
                AttributeCriterias = attrs,
                ExcludeStatuses = new List<en_file_Status>() {
                        en_file_Status.deleted,
                        en_file_Status.archived
                }
            };

            return criteria;
        }

        byte[] file_get_byte_contents(string fileName)
        {
            byte[] sContents;
            if (fileName.ToLower().IndexOf("http:") > -1 || fileName.ToLower().IndexOf("https:") > -1)
            {
                // URL 
                System.Net.WebClient wc = new System.Net.WebClient();
                sContents = wc.DownloadData(fileName);
            }
            else
            {   

                if( !File.Exists(fileName))
                {
                    IConfig.GetLogger().Error("File doesn't exists : {0}",fileName);
                    return new byte[]{};
                }

                // Get file size
                FileInfo fi = new FileInfo(fileName);

                // Disk
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                sContents = br.ReadBytes((int)fi.Length);
                br.Close();
                fs.Close();
            }

            return sContents;
        }

        public IRestResponse Execute(RestRequest r)
        {
            _logger.Trace("Execute RestSharp");
            //r.AddHeader("User-Agent", "Mozilla/5.0+(Windows+NT+10.0;+WOW64)+AppleWebKit/537.36+(KHTML,+like+Gecko)+Chrome/59.0.3071.115+Safari/537.36");

            IRestResponse response = Client.Execute(r);

            try
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                }
                else
                {
                    response.Headers.ToList().ForEach(x =>
                    {                        
                        _logger.Error("HTTP HEADER {0} : {1}", x.Name, x.Value);
                    });

                    _logger.Error("Login Faild : {0}", JsonConvert.SerializeObject(response));
                    throw new UnauthorizedAccessException(response.Content);
                }

                CookieContainer _cookieJar = Client.CookieContainer ?? new CookieContainer();
                foreach (var c in response.Cookies)
                {
                    _cookieJar.Add(new System.Net.Cookie(c.Name, c.Value, c.Path, c.Domain));
                    _logger.Debug(" Cookie {0} {1} {2} {3}", c.Name, c.Value, c.Path, c.Domain);
                }

                if (_cookieJar.Count > 0)
                    Client.CookieContainer = _cookieJar;

            }
            catch(UnauthorizedAccessException ex)
            {
                _logger.Error(ex,"Response Status isn't OK. Check the network issue");
                //throw new Exception();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw ex;
            }

            return response;
        }

    }

}
