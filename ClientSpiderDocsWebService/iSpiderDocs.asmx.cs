using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Data.SqlClient;
using System.Collections;
using SpiderDocs;
using SpiderDocsModule;

namespace ClientSpiderDocsWebService
{
    /// <summary>
    /// This Webservice provides access to SpiderDocs Documents from iOS devices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class iSpiderDocs : System.Web.Services.WebService
    {

    [WebMethod]
    public SoapResponse[] sendScannedDocument(Document[] arrayDocument, String title, int folderId, int docType, int IdUser, DocumentAttribute[] attributes)
    {

        try
        {

            int id_doc = 0;
            int id_version = 0;
            ArrayList OcrText = new ArrayList();
            PdfDocument doc = new PdfDocument();


            for(int i = 0; i < arrayDocument.Count(); i++)
            {
                String documentPage = arrayDocument[i].base64data;

                if(documentPage.Length > 1)
                {
                    // Convert Base64 String to byte[]
                    byte[] imageBytes = Convert.FromBase64String(documentPage);
                    MemoryStream ms = new MemoryStream(imageBytes, 0,
                      imageBytes.Length);

                    // Convert byte[] to Image
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    Image image = Image.FromStream(ms, true);



                    PdfPage page = new PdfPage();

                    doc.Pages.Add(page);
                    XGraphics xgr = XGraphics.FromPdfPage(page);
                    XImage img = XImage.FromGdiPlusImage(Utilities.fixImageOrientation(image));

                    doc.Pages[i].Width = XUnit.FromPoint(img.Size.Width);
                    doc.Pages[i].Height = XUnit.FromPoint(img.Size.Height);
                    xgr.DrawImage(img, 0, 0, img.Size.Width, img.Size.Height);

                    //do OCR
                    OcrText.Add(Utilities.OCR(image));
                }
            }

            //insert the text inside the pdf file
            byte[] searcheablePdf = Utilities.makeSearcheablePdf(doc, OcrText);


			//SqlConnection sqlConn = Utilities.getSqlConnBeginTran();

            SoapResponse response = new SoapResponse();
            List<SoapResponse> responseArray = new List<SoapResponse>();

            try
            {

                //Memory stream
                MemoryStream stream = new MemoryStream();
                doc.Save(stream, false);
                byte[] bytes = stream.ToArray();
                doc.Close();

                //check name
                string fileName = title + ".pdf";
                    
                if(SpiderDocsModule.DocumentController<Document>.IsDocumentExists(0, fileName))
                {
                    fileName = title + " " + DateTime.Now.Ticks + ".pdf";
                }

                
                //save document
                Document objDoc = new Document();
                objDoc.title = fileName;
                objDoc.extension = ".pdf";
                objDoc.id_status = SpiderDocsModule.en_file_Status.checked_in;
                objDoc.id_user = IdUser;
                objDoc.id_folder = folderId;
                objDoc.id_docType = docType;
                objDoc.filedata = searcheablePdf;
                objDoc.version = 1;
                objDoc.id_event = 3;
                objDoc.date = DateTime.Now;

				Settings.userId = IdUser;
                objDoc.AddDocument(SpiderDocsModule.SpiderDocsApplication.CurrentUserId, false);

                //SpiderDocsModule.DocumentController<Document>.SaveDocument(objDoc);

				//DocumentController.SaveDocument(sqlConn, objDoc);
				//Document newVersion = DTS_Document.SaveDocumentVersion(sqlConn, objDoc);
				//int id_hist = DocumentController.SaveDocumentHistoric(sqlConn, objDoc);
				//DocumentController.SaveDocumentRecent(sqlConn, IdUser, objDoc.id, id_hist, newVersion.id_version);



				//if(attributes != null)
				//{
				//	if(attributes.Count() > 0)
				//	{
				//		Sources.Save(sqlConn, objDoc, attributes);
				//	}
				//}




                id_doc = objDoc.id;
                id_version = objDoc.id_version;


                response.documentNumber = id_doc.ToString();


                //commit tran
				//Utilities.sqlConnCommit(sqlConn);

                //getting OCR

                // Sources.SalveContentText(sqlConn, OCR(image), id_version);

                //TextWriter tw = new StreamWriter(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "test.txt");

                //tw.WriteLine(id_doc + "\n" + OCR(image));

                //tw.Close();

                response.result = "success";

            }
            catch(Exception error)
            {

                Sources.writeEventLog(error);

                //response.result = "fail";
                response.result = error.Message + " " + error.StackTrace;
                response.message = error.Message;
                //Utilities.sqlConnRollBack(sqlConn);

            }

            responseArray.Add(response);

            return responseArray.ToArray();
        }
        catch(Exception ex)
        {
            Sources.writeEventLog(ex);
        }


        return null;           
    }

    [WebMethod]
    public SoapResponse[] sendImportDocument(String document, String title, String extention, int folderId, int docType, int IdUser, Attributes[] attributes)
    {
         int id_doc = 0;
         int id_version = 0;

         // Convert Base64 String to byte[]
         byte[] fileBytes = Convert.FromBase64String(document);


		//SqlConnection sqlConn = Utilities.getSqlConnBeginTran();

        SoapResponse response = new SoapResponse();
        List<SoapResponse> responseArray = new List<SoapResponse>();

        try
        {

            extention = "." + extention;

            //check name
            string fileName = title + extention;

            if(SpiderDocsModule.DocumentController<Document>.IsDocumentExists(0, fileName))
            {
                fileName = title + " " + DateTime.Now.Ticks + extention;
            }


            //save document
            Document objDoc = new Document();
            objDoc.title = fileName;
            objDoc.extension = extention;
            objDoc.id_status = SpiderDocsModule.en_file_Status.checked_in;
            objDoc.id_user = IdUser;
            objDoc.id_folder = folderId;
            objDoc.id_docType = docType;
            objDoc.filedata = fileBytes;
            objDoc.version = 1;
            objDoc.id_event = SpiderDocsModule.EventIdController.GetEventId(SpiderDocsModule.en_Events.Read);
            objDoc.date = DateTime.Now;
                
			Settings.userId = IdUser;
            objDoc.AddDocument(SpiderDocsModule.SpiderDocsApplication.CurrentUserId, false);

            //DocumentController.SaveDocument(objDoc);

			//DocumentController.SaveDocument(sqlConn, objDoc);
			//Document newVersion = DTS_Document.SaveDocumentVersion(sqlConn, objDoc);
			//int id_historic = DocumentController.SaveDocumentHistoric(sqlConn, objDoc);
			//DocumentController.SaveDocumentRecent(sqlConn, IdUser, objDoc.id, id_historic,newVersion.id_version);



			//if(attributes != null)
			//{
			//	if(attributes.Count() > 0)
			//	{
			//		Sources.Save(sqlConn, objDoc, attributes);
			//	}
			//}




            id_doc = objDoc.id;
            id_version = objDoc.id_version;


            response.documentNumber = id_doc.ToString();


			////commit tran
			//Utilities.sqlConnCommit(sqlConn);



            response.result = "success";
        }
        catch(Exception error)
        {
            Sources.writeEventLog(error);
            //response.result = "fail";
            response.result = error.Message + " " + error.StackTrace;
            response.message = error.Message;
			//Utilities.sqlConnRollBack(sqlConn);

        }
		//finally
		//{
		//	sqlConn.Close();
		//}


        responseArray.Add(response);

        return responseArray.ToArray();

    }

    [WebMethod]
    public Users getUserDetails(String userLogin, String userPassword)
    {
        Users remoteUser = Sources.getUserDetails(userLogin, userPassword);
        return remoteUser;
    }

    [WebMethod]
    public Folder[] getSystemFoldersByUser(int userId, bool toSave)
    {
        return SpiderDocsModule.PermissionController.GetAssignedFolderToUser(userId).Select( x => new Folder { folder_id = x.id, folder_name = x.document_folder }).ToArray();
        //return SpiderDocsModule.PermissionController.GetAssignedFolderToUser()(userId);
    }

    [WebMethod]
    public DocumentType[] getSystemDocumentTypes()
    {

        return Sources.getDocumentTypes();

    }

    [WebMethod]
    public Document[] getDocumentsByFolder(int folderId, int userId)
    {
        return Sources.getDocumentsByFolder(folderId, userId);
    }

    [WebMethod]
    public Document[] searchDocuments(String keyword, int filterSelection, int folderId)
    {
        return Sources.getDocumentsSearch(keyword, filterSelection, folderId);
    }

    [WebMethod]
    public SoapResponse archiveDocuments(int documentId, int IdUser)
    {
        SoapResponse response = new SoapResponse();
        SqlConnection sqlConn = Utilities.getSqlConnBeginTran();
        try
        {
            //change status to "archive"
            //DocumentController.UpdateStatus(documentId, SpiderDocsModule.en_file_Status.archived);

                
            //insert historic
            var doc = SpiderDocsModule.DocumentController<Document>.GetDocument(id_doc: new int[] { documentId }).FirstOrDefault() ?? new Document();

            doc.ChangeStatus(SpiderDocsModule.en_file_Status.archived);

            Utilities.saveHistoric(sqlConn, doc.id_latest_version, 17, IdUser);

            response.result = "success";
            Utilities.sqlConnCommit(sqlConn);
        }
        catch(Exception e)
        {
            response.result = "fail";
            Utilities.sqlConnRollBack(sqlConn);
        }
        finally
        {
            sqlConn.Close();
        }


        return response;
    }

    [WebMethod]
    public void getDocumentById(int documentId)
    {
        HttpResponse response = HttpContext.Current.Response;
        HttpRequest request = HttpContext.Current.Request;

        Document document = SpiderDocsModule.DocumentController<Document>.GetDocument(id_doc: new int[] { }).FirstOrDefault() ?? new Document();
        //Document document = (Document)Document.getDocument(documentId, Document.en_DocumentIdType.Doc);

        if(document.id_status == SpiderDocsModule.en_file_Status.deleted)
        {
            response.Write("<error>This file has been deleted</error>");
        }
        else
        {
            //String mimeType = Sources.getMimeTypeFromExtension(document.extension);
            //if(mimeType == "none")
            //{
            //    response.Write("<html><h1>This file cannot be opened on this device!</h1></html>");
            //}
            //else
            //{

                byte[] buffer = document.filedata;


               // response.ContentType = mimeType;
                response.AddHeader("Content-Disposition", "attachment; filename=download" + document.extension);
                response.ContentEncoding = System.Text.Encoding.UTF8;

                response.BinaryWrite(buffer);
                response.Flush();



            //}
        }
    }

    [WebMethod]
    public SoapResponse deleteDocumentById(int documentId, String reason, int IdUser)
    {
        SoapResponse response = new SoapResponse();

        ///bool ans = Sources.Delete(documentId, reason, IdUser);
        bool ans = SpiderDocsModule.DocumentController<Document>.DeleteDocument(SpiderDocsModule.SpiderDocsApplication.CurrentUserId, documentId, reason);

        if (ans)
			response.result = "success";
		else
			response.result = "fail";

        return response;
    }

    [WebMethod]
    public DocumentType[] getDocumentTypes()
    {
        return Sources.getDocumentType();
    }

    [WebMethod]
    public DocumentAttribute[] getDocumentAttributes(int documentTypeId)
    {
        return Sources.getAllAttributesbyTypeDtArray(documentTypeId);
    }


    [WebMethod]
    public DocumentAttribute[] getAttributes()
    {
        return Sources.getAllAttributesArray();
    }


    }
    }
