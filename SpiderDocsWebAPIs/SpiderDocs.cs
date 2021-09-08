using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NLog;
using System.Web.Configuration;
using System.Text.RegularExpressions;
using SpiderDocsModule;
using System.IO;


/// <summary>
/// Wrapper for SpiderDoc Web Client.
/// ver 1.0.0
/// </summary>
namespace SpiderDocsWebAPIs
{
    public class SpiderDocument{

            public SpiderDocument(){
            }
            public SpiderDocument(Document doc){
                Document = doc;

                FileName = doc.title;
            }

            Logger logger = NLog.LogManager.GetCurrentClassLogger();
            string _filename = string.Empty;

            public string FileName
            {
                get { return _filename; }
                set
                {

                    // Log
                    if (!string.IsNullOrWhiteSpace(System.IO.Path.GetFileName(_filename))) logger.Info("Filename is changed {0} to {1}", _filename, value);

                    _filename = System.IO.Path.GetFileName(value);
                }
            }

        public Document Document { get; set; } = new Document();
            public string DownloadURL{get;set;} = string.Empty;
            public string DownloadedPath { get; set; } = string.Empty;
            //public byte[] Blob { get; set; }= new byte[]{};
        }


        public class SpiderDocHelper :IDisposable
        {
            public interface IConfiguration
            {
                string GetWebTempPath();
                string GetWebTempURL();
            }

            static Logger logger = LogManager.GetCurrentClassLogger();

            //static string id = WebConfigurationManager.AppSettings["SpiderDocs.LoginID"];
            //static string pass = WebConfigurationManager.AppSettings["SpiderDocs.Password"];

            SpiderDoscWebClient client ;
            public List<SpiderDocument> Docs{get;set;} = new List<SpiderDocument>();

            IConfiguration iConf;

            private string _home = Fn.Guid();

            private string HomeName{
                get{return _home;}
            }

            public SpiderDocHelper(SpiderDoscWebClient c, IConfiguration conf)
            {
                client = c;
                iConf = conf;

                Fn.MkDir( GetHome());
            }

            public string GetHome()
            {
                return iConf.GetWebTempPath() + "\\"+HomeName;
            }

            public string GetWebHome()
            {
                return iConf.GetWebTempURL() + "/"+HomeName;
            }

            public SpiderDocHelper GetById(int[] ids)
            {
                SpiderDocsModule.SearchCriteria criteria = new SpiderDocsModule.SearchCriteria();
                ids.ToList().ForEach(x => criteria.DocIds.Add(x));
                criteria.ExcludeStatuses.Add(en_file_Status.deleted);
                criteria.ExcludeStatuses.Add(en_file_Status.archived);

                client.GetDocument(criteria).ToList().ForEach( x => Docs.Add(new SpiderDocument(x)));

                return this;
            }

            public SpiderDocHelper GetByCriteria(SearchCriteria criteria)
            {

                client.GetDocument(criteria).ToList().ForEach(x => Docs.Add(new SpiderDocument(x)));

                return this;
            }

            public SpiderDocHelper GetDownloadURL()
            {
                Docs.ForEach(x=>{
                    string url = client.GetDownloadUrls( new int[]{ x.Document.id_version}).FirstOrDefault();
                    x.DownloadURL = url;
                });

                return this;
            }

            public SpiderDocHelper DownloadAll()
            {
                WebClient Client = new WebClient();
                Docs.ForEach(x =>
                {
                    /*
                    string savedDirectry = GetHome() + "\\" + Utils.Utilities.Path2Filename(x.DownloadURL);
                    string savedPath = GetWebHome() + "/" + Utils.Utilities.Path2Filename(x.DownloadURL);
                    */

                    string filename =string.Format("{0}-{1}.{2}", Fn.Filename2Name(x.Document.title), Fn.Guid(), Fn.Extention(x.DownloadURL)) ;
                    string savedDirectry = GetHome() + "\\" + filename;
                    string savedPath = GetWebHome() + "/" + filename;

                    Client.DownloadFile(x.DownloadURL, savedDirectry);

                    x.DownloadedPath = savedDirectry;
                    x.DownloadURL = savedPath;
                });

                return this;
            }


            public void PlaceFileBy(int versionId, out string filepath, out string downloadURL)
            {
                WebClient Client = new WebClient();

                string url = client.GetContentURL(versionId);

                string filename = string.Format("{0}-{1}.{2}", Fn.Filename2Name(url), Fn.Guid(), Fn.Extention(url));
                filepath = GetHome() + "\\" + filename;
                downloadURL = GetWebHome() + "/" + filename;

                Client.DownloadFile(url, filepath);
            }

            public void RemoveDownloadedFiles()
            {

            }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    Fn.RmDir(iConf.GetWebTempPath());



                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SpiderDocHelper() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }

}