using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace SpiderDocsWeb
{

                
    public partial class showDoc : System.Web.UI.Page
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            string strConn = SpiderDocsWeb.Properties.Settings.Default.bdConnection;

            try
            {

                string hash = Request.QueryString["hash"];

                if(hash == null)
                    return;


                HttpResponse response = HttpContext.Current.Response;
                HttpRequest request = HttpContext.Current.Request;

                SqlConnection sqlConn = new SqlConnection(strConn);
                sqlConn.Open();

                string sql = " select top 1 t1.filedata,t1.extension,t2.id_status, t2.title";
                sql = sql + " FROM document_version t1";
                sql = sql + " INNER JOIN document t2 on t1.id_doc = t2.id";
                sql = sql + " WHERE t1.id_doc = (select id_doc from document_external where external_hash = '" + hash.Trim() + "')";
                sql = sql + " order by t1.version desc";


                SqlCommand sqlCommand = new SqlCommand(sql, sqlConn);
                SqlDataReader dr = sqlCommand.ExecuteReader();
                dr.Read();



                if((int)dr["id_status"] == 5)
                {
                    response.Write("<error>This file has been deleted</error>");
                }
                else
                {


                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/unknown";


                    byte[] buffer = ((byte[])dr["filedata"]);


                    //supported extension
                    string extensionRange = ".pdf,.pgn,.jpg,.bmp,.html,.htm,";
                    String mimeType = getMimeTypeFromExtension(((String)dr["extension"]).ToString().ToLower());

                    if(extensionRange.IndexOf((String)dr["extension"].ToString().ToLower(), 0) != -1)
                    {

                        
                        if(mimeType == "none")
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Sorry, something went wrong while displaying this webpage. Try again in few minutes.";
                        }

                        response.ContentType = mimeType;
                        response.AppendHeader("Content-Disposition", "filename=" + (String)dr["title"]);


                    }
                    else
                    {
                        response.AppendHeader("Content-Disposition", "attachment;filename=" + (String)dr["title"]);
                    }

                    response.ContentType = mimeType;
                    response.ContentEncoding = System.Text.Encoding.UTF8;
                    response.BinaryWrite(buffer);
                    response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();



                }

                sqlConn.Close();
            }
            catch(Exception)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Sorry, something went wrong while displaying this webpage. Try again in few minutes.";
            }


        }


        public static String getMimeTypeFromExtension(String extension)
        {
            switch(extension)
            {
                case ".txt":
                    return "text/plain";
                case ".docx":
                case ".doc":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".dotx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";                
                case ".xlsx":
                case ".xls":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".xltx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.template";
                case ".potx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.template";
                case ".ppsx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.slideshow";
                case ".pptx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case ".sldx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.slide";
                case ".xlam":
                    return "application/vnd.ms-excel.addin.macroEnabled.12";
                case ".xlsb":
                    return "application/vnd.ms-excel.sheet.binary.macroEnabled.12";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".ppt":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".pdf":
                    return "application/pdf";
                case ".htm":
                case ".html":
                    return "text/html";
                default:
                    return "none";

            }



        }



    }
}