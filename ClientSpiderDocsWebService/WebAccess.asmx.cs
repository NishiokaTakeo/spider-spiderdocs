using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ClientSpiderDocsWebService
{
    /// <summary>
    /// This webservice provide external access to documents stored on SpiderDocs. It can be used for websites, etc
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebAccess : System.Web.Services.WebService
    {

        [WebMethod]
        public Folder[] getFoldersContainingDocsAttribute(int id_user, int attributeId, string attributeValue)
        {
            return Sources.getFoldersContainingDocsAttribute(id_user, attributeId, attributeValue);
        }



        [WebMethod]
        public Document[] getDocumentsByFolder(int id_folder, int attributeId, string attributeValue)
        {
            return Sources.getDocumentsByFolderWIthAttributes(id_folder,  attributeId, attributeValue);
        }


    }
}
