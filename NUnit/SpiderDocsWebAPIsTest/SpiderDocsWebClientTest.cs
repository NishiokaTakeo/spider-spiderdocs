using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Text.RegularExpressions;
using SpiderDocsModule;
using RestSharp;
using System.Net;

namespace SpiderDocsWebAPIs.Tests
{
    [TestFixture]
    public class SpiderDocsWebClientTest : SpiderDoscWebClient.IConfiguration
    {
        Logger logger;

        SpiderDocsWebAPIs.SpiderDoscWebClient client;
        

        [SetUp]
        public void Setup()
        {
            try
            { 
                client = new SpiderDoscWebClient(this);

                if (!UnitTestController.IsTestDatabase())
                {
                        throw new Exception("Stop tests");
                }

                Connect2SpiderDocs();
            }
            catch(Exception ex)
            {
                client = null;
                throw new InvalidOperationException("This should be unit test database");
            }
        }

        void Connect2SpiderDocs()
        {
            SqlOperation.MethodToGetDbManager = this.GetDbManager();
           
        }

        private void Server_onConnectionErr()
        {
            logger.Warn("Connect error to Spider Docs.");

            System.Threading.Thread.Sleep(3000);

            Connect2SpiderDocs();
        }


        private Document[] ShouldGetDocument(int id_doc)
        {

            var res = client.GetDocument(new SearchCriteria() { DocIds = new int[] { id_doc }.ToList() });

            return res;
        }

        [Test]
        public void ShouldLoginWithoutAuth()
        {
            client = new SpiderDoscWebClient(new NoAuthConfig());
            Assert.AreEqual(2, client.LoginedUser.id);

            var doc = ShouldGetDocument(1);

            Assert.AreEqual(1, doc.FirstOrDefault()?.id);
        }
        [Test]
        public void ShouldLoginWithAuth()
        {
            client = new SpiderDoscWebClient(this);
            Assert.AreEqual(1, client.LoginedUser.id);
        }

        [Test]
        public void ShouldGetHistories()
        {

            var  res = client.GetHistories(new SearchCriteria() { DocIds = new int[] { 1 }.ToList() });

            //Assert.IsTrue(res);
        }

        [Test]
        public void ShouldArchive()
        {

            bool res = client.Archive(new int[] { 1000 });

            Assert.IsTrue(res);
        }

        [Test]
        public void ShouldUnArchive()
        {
            int id_test = 1004;
            bool res = client.UnArchive(new int[] { id_test });

            var doc = ShouldGetDocument(id_test).FirstOrDefault() ?? new Document();
 
            Assert.IsTrue(doc.id > 0 && doc.id_status == en_file_Status.checked_in);
        }



        [Test]
        public void ShouldGetFolderL1()
        {

            List<Folder> folders = client.GetFoldersL1(0);

            Assert.AreEqual(client.LastResponse.ResponseStatus, ResponseStatus.Completed);

            //Assert.IsTrue(ok);

            //FolderController.Save(new Folder() { document_folder ="Test", id_parent = 0 } );

            //List<Folder> folders = client.GetFoldersL1(0);

            //Assert.NotZero(folders.Count);            
        }

        [Test]
        public void ShouldSaveFolder()
        {
            Folder folder = client.SaveFolder(new Folder());

            Assert.AreEqual(HttpStatusCode.OK, client.LastResponse.StatusCode);

            //List<Folder> folders2 = FolderController.GetFolders().Where(f => f.id != 1).ToList();
            //folders2.ForEach(f => FolderController.Delete(f.id));


            //List<Folder> folders = client.GetFoldersL1(0);

            //Folder folder = new Folder() { document_folder = "Test", id_parent = 0 };

            //Folder saved = client.SaveFolder(folder);

            //Assert.NotZero(saved.id);
        }

        //[Test]
        //public void ShouldCheckOutWithRightPermission()
        //{
        //    Dictionary<en_Actions, en_FolderPermission> pers = new Dictionary<en_Actions, en_FolderPermission>() { };
        //    pers.Add(en_Actions.CheckIn_Out, en_FolderPermission.Allow);
        //    pers.Add(en_Actions.OpenRead, en_FolderPermission.Allow);

        //    PermissionController.AddPermission(1, client.LoginedUser.id, en_FolderPermissionMode.Group, pers);


        //    Folder saved = client.SaveFolder(new Folder() {document_folder = "ShouldCheckOutWithRightPermission", id_parent = 0 });

        //    var doc = client.GetDocument(new SearchCriteria() { DocIds = new List<int> { 1 } }).First();
        //    //doc.id_checkout_user = 0;
        //    //doc.id_folder = saved.id;

        //    //if (string.IsNullOrWhiteSpace(doc.UpdateProperty(client.LoginedUser.id, false)))
        //    //{
        //    //    Assert.Fail();
        //    //    return;
        //    //}

        //    doc.id_folder = saved.id;

        //    //Add permission
        //    PermissionController.AddPermission(saved.id, client.LoginedUser.id, en_FolderPermissionMode.Group, pers);

        //    doc.id_checkout_user = 1;
        //    bool ok = client.CheckOut(new int[] { doc.id }/*,new int[] { folder.id }*/);

        //    if (!ok) { Assert.Fail(); return; }

        //    var assertedDoc = client.GetDocument(new SearchCriteria() { DocIds = new List<int> { 1 } }).First();
            
        //    Assert.NotZero(assertedDoc.id_checkout_user);
        //}

        //[Test]
        //public void ShouldCheckOutWithNoPermission()
        //{
        //    Folder saved = client.SaveFolder(new Folder() { document_folder = "ShouldCheckOutWithNoPermission", id_parent = 0 });


        //    var doc = client.GetDocument(new SearchCriteria() { DocIds = new List<int> { 1 } }).First();

        //    doc.id_folder = saved.id;

        //    Dictionary<en_Actions, en_FolderPermission> pers = new Dictionary<en_Actions, en_FolderPermission>() { };
        //    pers.Add(en_Actions.CheckIn_Out, en_FolderPermission.Allow);

        //    //Add permission
        //    PermissionController.AddPermission(saved.id, client.LoginedUser.id, en_FolderPermissionMode.Group, pers);

        //    doc.id_checkout_user = 1;
        //    bool ok = client.CheckOut(new int[] { doc.id }/*,new int[] { folder.id }*/);

        //    if (ok) { Assert.Fail(); return; }

        //    var assertedDoc = client.GetDocument(new SearchCriteria() { DocIds = new List<int> { 1 } }).First();

        //    Assert.Zero(assertedDoc.id_checkout_user);
        //}

        //[Test]
        //public void ShouldNotCancelCheckOut()
        //{
        //    Dictionary<en_Actions, en_FolderPermission> pers = new Dictionary<en_Actions, en_FolderPermission>() { };
        //    pers.Add(en_Actions.CheckIn_Out, en_FolderPermission.Allow);
        //    pers.Add(en_Actions.OpenRead, en_FolderPermission.Allow);


        //    Folder saved = client.SaveFolder(new Folder() { document_folder = "ShouldNotCancelCheckOut", id_parent = 0 });

        //    var doc = client.GetDocument(new SearchCriteria() { DocIds = new List<int> { 1 } }).First();

        //    doc.id_folder = saved.id;

        //    //Add permission
        //    PermissionController.AddPermission(saved.id, client.LoginedUser.id, en_FolderPermissionMode.Group, pers);

        //    doc.id_checkout_user = 1;

        //    doc.UpdateProperty(client.LoginedUser.id, false);


        //    bool ok = client.CancelCheckOut(new int[] { doc.id }/*,new int[] { folder.id }*/);

        //    Assert.IsFalse(ok);
        //}

        [Test]
        public void ShouldCancelCheckOut()
        {

            bool ok = client.CancelCheckOut(new int[] { 1 });
            
            Assert.AreEqual(client.LastResponse.ResponseStatus, ResponseStatus.Completed);
            Assert.IsTrue(ok);


            //Dictionary<en_Actions, en_FolderPermission> pers = new Dictionary<en_Actions, en_FolderPermission>() { };
            //pers.Add(en_Actions.CheckIn_Out, en_FolderPermission.Allow);
            //pers.Add(en_Actions.CancelCheckOut, en_FolderPermission.Allow);
            //pers.Add(en_Actions.OpenRead, en_FolderPermission.Allow);

            //PermissionController.AddPermission(1, 1, en_FolderPermissionMode.Group, pers);

            //Folder saved = client.SaveFolder(new Folder() { document_folder = "CancelCheckOut", id_parent = 0 });

            //var doc = client.GetDocument(new SearchCriteria() { DocIds = new List<int> { 1 } }).First();

            //doc.id_folder = saved.id;

            ////Add permission
            //PermissionController.AddPermission(saved.id, client.LoginedUser.id, en_FolderPermissionMode.Group, pers);

            //doc.id_checkout_user = 1;
            //doc.id_status = en_file_Status.checked_out;

            //doc.UpdateProperty(client.LoginedUser.id,false);


            //bool ok = client.CancelCheckOut(new int[] { doc.id }/*,new int[] { folder.id }*/);

            //if (!ok) { Assert.Fail(); return; }

            //var assertedDoc = client.GetDocument(new SearchCriteria() { DocIds = new List<int> { 1 } }).First();

            //Assert.LessOrEqual(assertedDoc.id_checkout_user,0);
        }

        [Test]
        public void ShouldDelete()
        {
            bool ok = client.Delete(new int[] { 1 }, "ShouldDelete");
            Assert.AreEqual(client.LastResponse.ResponseStatus, ResponseStatus.Completed);
            Assert.IsTrue(ok);
            
            //var doc = client.GetDocument(new SearchCriteria() { DocIds = new List<int> { 1 } }).First();

            //string TmpPath = FileFolder.YeildNewFileName(FileFolder.TempPath + doc.title);

            //doc.Export(TmpPath,client.LoginedUser.id);
            
            //Document newdoc = new Document();
            //newdoc.title = doc.title;
            //newdoc.path = TmpPath;
            //newdoc.AddDocument(client.LoginedUser.id,false);
            
            //bool ok = client.Delete(new int[] {newdoc.id}, "ShouldDelete");

            //if (!ok) { Assert.Fail(); return; }

            //doc = client.GetDocument(new SearchCriteria() { DocIds = new List<int> { 1 } }).First();

            //Assert.Equals(doc.id_status, (int)en_file_Status.deleted);
        }

        
        public void TearDown()
        {
            List<Folder> folders = FolderController.GetFolders().Where(f => f.id != 1).ToList();
            folders.ForEach(f => FolderController.Delete(f.id));

            //Reset default document
            //Dictionary<en_Actions, en_FolderPermission> pers = new Dictionary<en_Actions, en_FolderPermission>() { };
            //pers.Add(en_Actions.CheckIn_Out, en_FolderPermission.Allow);
            //pers.Add(en_Actions.CancelCheckOut, en_FolderPermission.Allow);
            //pers.Add(en_Actions.OpenRead, en_FolderPermission.Allow);

            //PermissionController.AddPermission(1, 1, en_FolderPermissionMode.User, pers);

            //var doc = DocumentController<Document>.GetDocument(id_doc: new int[] { 1 }).First();

            ////var doc = client.GetDocument(new SearchCriteria() { DocIds = new List<int> { 1 } }).First();
            //doc.id_checkout_user = -1;
            //doc.id_folder = 1;
            //doc.id_docType = -1;
            //doc.id_status = en_file_Status.checked_in;

            //doc.UpdateProperty(client.LoginedUser.id, false);

            //PermissionController.DeleteAllPermission(1);

        }

        #region Spider Docs Client Config

        public string LoginID()
        {
            return "administrator";
        }

        public string LoginPassword()
        {
            return "Slayer6zed";
        }

        public string GetServerURL()
        {
            return "http://localhost:5321/"; // Must be unit test server.
        }

        public global::NLog.Logger GetLogger()
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
            return logger;
        }

        public Func<global::SpiderDocsModule.DbManager> GetDbManager()
        {
            SpiderDocsApplication.CurrentServerSettings = new ServerSettings();
            SpiderDocsApplication.CurrentServerSettings.server = "localhost";
            SpiderDocsApplication.CurrentServerSettings.port = 5322;

            SpiderDocsServer server = new SpiderDocsServer(SpiderDocsApplication.CurrentServerSettings);

            server.Connect();

            return SpiderDocsModule.SqlOperation.MethodToGetDbManager = new Func<DbManager>(() =>
            {
                return new DbManager(SpiderDocsApplication.CurrentServerSettings.conn, SpiderDocsApplication.CurrentServerSettings.svmode);
            });
        }

        public int NoAuth()
        {
            return 0;
        }

        #endregion
    }

    public class NoAuthConfig : SpiderDoscWebClient.IConfiguration
    {
        NLog.Logger logger;

        public string LoginID()
        {
            return "";
        }

        public string LoginPassword()
        {
            return "";
        }

        public string GetServerURL()
        {
            return "http://localhost:5321/"; // Must be unit test server.
        }

        public global::NLog.Logger GetLogger()
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
            return logger;
        }

        public Func<global::SpiderDocsModule.DbManager> GetDbManager()
        {
            SpiderDocsApplication.CurrentServerSettings = new ServerSettings();
            SpiderDocsApplication.CurrentServerSettings.server = "localhost";
            SpiderDocsApplication.CurrentServerSettings.port = 5322;

            SpiderDocsServer server = new SpiderDocsServer(SpiderDocsApplication.CurrentServerSettings);

            server.Connect();

            return SpiderDocsModule.SqlOperation.MethodToGetDbManager = new Func<DbManager>(() =>
            {
                return new DbManager(SpiderDocsApplication.CurrentServerSettings.conn, SpiderDocsApplication.CurrentServerSettings.svmode);
            });
        }

        public int NoAuth()
        {
            return 2;
        }

    }
}
