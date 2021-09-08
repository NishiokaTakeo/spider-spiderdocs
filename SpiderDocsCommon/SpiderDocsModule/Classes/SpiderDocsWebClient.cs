using Newtonsoft.Json;
using NLog;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SpiderDocsModule.Classes
{

    public class SpiderDoscWebClient
    {
        public interface IConfiguration
        {
            string GetServerURL();
            Logger GetLogger();
        }

        public RestClient Client;

        Logger _logger;
        string _serverURL { get; set; }

        public SpiderDoscWebClient(IConfiguration iConfig)
        {
            _serverURL = iConfig.GetServerURL();
            Client = new RestClient(_serverURL); //"http://localhost:50920"

            _logger = iConfig.GetLogger();

            if (LoginedUser == null)
                login();
        }

        public User LoginedUser
        {
            get;
            set;
        }

        string requestPath(string path) {
            _logger.Trace("Begining of Path");
            if (LoginedUser == null) {
                _logger.Warn("This user is not logined");
            }

            return string.Format("spiderdocs/External/{0}/{1}", path, LoginedUser.id);
            //return string.Format("spiderdocs/External/{0}/{1}", path, "16");
        }
        public void login()
        {


            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, "Welcome1");
                var credential = new
                {
                    UserName = "spider",
                    Password_Md5 = hash
                };

                var request = new RestRequest("spiderdocs/Users/Login", Method.POST);
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

        public SpiderDocsModule.Document[] GetDocument(SpiderDocsModule.SearchCriteria c)
        {

            var request = new RestRequest(requestPath("GetDocuments"), Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddParameter("Application/Json", JsonConvert.SerializeObject(c), ParameterType.RequestBody);
            IRestResponse response = Execute(request);

            string preformat = Regex.Replace(response.Content, "^{\"Documents\":(.*)}$", "$1");

            SpiderDocsModule.Document[] d = JsonConvert.DeserializeObject<SpiderDocsModule.Document[]>(preformat);

            return d;
        }
        public string[] GetDownloadUrls(string[] ids)
        {

            var request = new RestRequest(requestPath("Export"), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { VersionIds = ids });
            IRestResponse response = Execute(request);

            string preformat = Regex.Replace(response.Content, "^{\"Urls\":(.*)}$", "$1");

            string[] dls = JsonConvert.DeserializeObject<string[]>(preformat);

            return dls;
        }

        public IRestResponse Execute(RestRequest r)
        {
            _logger.Trace("Execute RestSharp");

            IRestResponse response = Client.Execute(r);

            if (response.StatusCode == HttpStatusCode.OK)
            {
            }
            else
            {
                _logger.Warn("Login Faild");
            }

            CookieContainer _cookieJar = Client.CookieContainer ?? new CookieContainer();
            foreach (var c in response.Cookies)
            {
                _cookieJar.Add(new Cookie(c.Name, c.Value, c.Path, c.Domain));
                _logger.Debug(" Cookie {0} {1} {2} {3}", c.Name, c.Value, c.Path, c.Domain);
            }

            if (_cookieJar.Count > 0)
                Client.CookieContainer = _cookieJar;

            return response;
        }

    }


}
