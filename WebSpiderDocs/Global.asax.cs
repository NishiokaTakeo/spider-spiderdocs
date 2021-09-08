using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using SpiderDocsModule;
using NLog;
using System.IO.Compression;
using System.Web.Optimization;
using System.Web.Security;

namespace WebSpiderDocs {
	// Note: For instructions on enabling IIS6 or IIS7 classic mode,
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication:System.Web.HttpApplication
	{
        Logger logger = LogManager.GetCurrentClassLogger();

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {

            //ApplyCompressionIfNecessary();

            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
                //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                //HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }

		protected void Application_Start()
		{
            //try
            //{
            //    List<int> a = new List<int>();
            //    var b = a.ToList().Count(x => x == 1);
            //}
            //catch (Exception ex)
            //{
            //}
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            logger.Info("Application is Started with '{0}'", userName);

            AreaRegistration.RegisterAllAreas();
			ModelBinders.Binders.Add(typeof(Document), new DocumentModelBinder());

			//WebApiConfig.Register(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);

            Connect2SpiderDocs();

        }
		protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
		{

			if (FormsAuthentication.CookiesSupported == true)
			{
				var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
				if (authCookie != null)
				{
					try
					{
						//let us take out the username now
						string 
								decryptedCookie = FormsAuthentication.Decrypt(authCookie.Value).Name ,
								
								id = decryptedCookie.Split('_')[0], 
								
								username = decryptedCookie.Substring(id.Length +1);

						int userId = Convert.ToInt32(id);

						string roles = string.Empty;

						logger.Trace(".ASPXAUTH {0}:{1}", userId, username);

						//using (userDbEntities entities = new userDbEntities())
						//{
						//	User user = entities.Users.SingleOrDefault(u => u.username == username);

						//	roles = user.Roles;
						//}

						roles = "user";
						//let us extract the roles from our own custom cookie

						// these use for web spider docs api
						WebSettings.CurrentUserId = userId;
						MMF.WriteData<int>(userId, MMF_Items.UserId);

						//Let us set the Pricipal with our user specific details
						HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
						  new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));

						//if(0 < WebSettings.CurrentUserId ) new SpiderDocsModule.Cache(WebSettings.CurrentUserId).Create();
					}
					catch (Exception ex)
					{
						logger.Error(ex);

						// reset for just in case
						WebSettings.CurrentUserId = 0;

					}
				}
			}
			else
			{
				// reset for just in case
				WebSettings.CurrentUserId = 0;

			}
		}
		void Connect2SpiderDocs()
        {

            // Spider Docs initial connection
            SpiderDocsWebApplication.CurrentServerSettings = new ServerSettings();
            SpiderDocsWebApplication.CurrentServerSettings.server = WebSettings.server;
            SpiderDocsWebApplication.CurrentServerSettings.port = WebSettings.port;

            SpiderDocsServer server = new SpiderDocsServer(SpiderDocsWebApplication.CurrentServerSettings);


            server.onConnected += new Action<ServerSettings, bool>((RetServerSettings, ConnectionChk) =>
            {
                if (ConnectionChk)
                {
                    SpiderDocsWebApplication.CurrentServerSettings = RetServerSettings;

                    SqlOperation.MethodToGetDbManager = new Func<DbManager>(() =>
                    {
                        return new DbManager(SpiderDocsWebApplication.CurrentServerSettings.conn, SpiderDocsWebApplication.CurrentServerSettings.svmode);
                    });

                    SpiderDocsWebApplication.CurrentPublicSettings = new PublicSettings();
                    SpiderDocsWebApplication.CurrentPublicSettings.Load();
					
					CustomViewSourceCtr.UpdateLocalSource(SqlOperation.MethodToGetDbManager().strConn);

				}
            });

            server.onConnectionErr += Server_onConnectionErr;

            server.Connect();
        }

        private void Server_onConnectionErr()
        {
            logger.Warn("Connect error to Spider Docs.");

            System.Threading.Thread.Sleep(3000);

            Connect2SpiderDocs();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            logger.Error(exception);

            Response.Clear();
        }

        /// <summary>
        /// Determines if GZip is supported
        /// </summary>
        /// <returns></returns>
        protected bool IsGZipSupported()
        {
            string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Headers["Postman-Token"])) return false;

            if (!string.IsNullOrEmpty(AcceptEncoding) && (AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate")))
                return true;

            return false;
        }


        protected void ApplyCompressionIfNecessary()
        {

            if (IsGZipSupported())
            {
                string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

                if (AcceptEncoding.Contains("gzip"))
                {
                    HttpContext context = HttpContext.Current;
                    context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
                    HttpContext.Current.Response.AppendHeader("Content-Encoding", "gzip");
                    HttpContext.Current.Response.Cache.VaryByHeaders["Accept-Encoding"] = true;
                }
                else
                {
                    Response.Filter = new System.IO.Compression.DeflateStream(Response.Filter,
                                                System.IO.Compression.CompressionMode.Compress);
                    Response.Headers.Remove("Content-Encoding");
                    Response.AppendHeader("Content-Encoding", "deflate");
                }
            }

        }

    }
}
