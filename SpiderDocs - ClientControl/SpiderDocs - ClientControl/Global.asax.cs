using NLog;
using System;

namespace SpiderDocs_ClientControl
{
    public class Global : System.Web.HttpApplication
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        protected void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            logger.Error(exception);

            Response.Clear();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //logger.Info("Application is Started");

        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
