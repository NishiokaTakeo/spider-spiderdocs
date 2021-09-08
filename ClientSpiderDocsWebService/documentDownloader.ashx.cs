using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpiderDocs.DTO;


namespace ClientSpiderDocsWebService
{
    /// <summary>
    /// Summary description for documentDownloader
    /// </summary>
    public class documentDownloader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            int id = (context.Request.QueryString["id"] != null && context.Request.QueryString["id"].Length >0 ? Convert.ToInt32(context.Request.QueryString["id"].ToString()) : 0);


            if(id != 0)
            {
                //Document document = (Document)Document.getDocument(id, Document.en_DocumentIdType.Doc);
                var document = SpiderDocsModule.DocumentController<Document>.GetDocument(id_doc: new int[] { id }, mode: SpiderDocsModule.en_GetDocumentInfoMode.DocumentAndData).FirstOrDefault() ?? new Document();

                if(document.id_status == SpiderDocsModule.en_file_Status.deleted)
                {
                    context.Response.Write("<error>This file has been deleted</error>");
                }
                else
                {
                    String mimeType = Sources.getMimeTypeFromExtension(document.extension);
                    if(mimeType == "none")
                    {
                        context.Response.Write("<html><h1>This file cannot be opened on this device!</h1></html>");
                    }
                    else
                    {

                        byte[] buffer = document.filedata;


                        context.Response.ContentType = mimeType;
                        context.Response.AddHeader("Content-Disposition", "attachment; filename=" + document.title.Replace(" ", "_").Replace(document.extension,"") + document.extension);
                        context.Response.ContentEncoding = System.Text.Encoding.UTF8;

                        context.Response.BinaryWrite(buffer);
                        context.Response.Flush();



                    }
                }

            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}