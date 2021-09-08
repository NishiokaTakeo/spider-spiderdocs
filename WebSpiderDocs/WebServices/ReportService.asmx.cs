using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Script.Serialization;
using ReportBuilder.Models;
//using CET.Utils;
using ReportBuilder.Controllers;
using NLog;
//using CET.Helpers;
using Newtonsoft.Json.Linq;
using System.Web.Script.Services;
//using CET.Models.Report;

namespace ReportBuilder.WebServices
{
    /// <summary>
    /// Summary description for utilService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    [System.Web.Script.Services.ScriptService]
    public class ReportService : System.Web.Services.WebService
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public void TestSSRS2(string name, string filename, Dictionary<string, string> Params = null)
        //{ }


        //[WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string GetReport(string name, string filename, Dictionary<string, string> Params = null)
        //{
        //    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(filename))
        //    {
        //        logger.Error("Inadequate parameters: {0}, {1}, {2}", name, filename, Newtonsoft.Json.JsonConvert.SerializeObject(Params));
        //        return "/content/404.pdf";
        //    }

        //    Params = Params ?? new Dictionary<string, string>();

        //    ReportRequest rptClient;
        //    string url = string.Empty;

        //    // temporary code
        //    //return "/temp/spiderdocs-temporary/test-do-not-remove-this.pdf";

        //    //return file;

        //    try
        //    {
        //        rptClient = new ReportRequest($"/CETReports/{name}", filename, new ConfigurationFactory.ReportConf());

        //        foreach (var pair in Params)
        //            rptClient.addParamter(pair.Key, pair.Value);
        //        //
        //        url = rptClient.request().AsDownloadURL();

        //        rptClient.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Faild to create PDF ");

        //        url = "/content/comingsoon.pdf";

        //        //throw new Exception("Faild to create PDF ");
        //    }

        //    return url;
        //}
        //[WebMethod]
        //public string GetReportList()
        //{
        //    var lstReport = new List<tblReportList>();
        //    var TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            lstReport = rc.GetReportList();
        //            if (lstReport != null)
        //            {
        //                // instantiate a serializer
        //                JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
        //                TheSerializer.MaxJsonLength = int.MaxValue;
        //                TheJson = TheSerializer.Serialize(lstReport);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);
        //    }
        //    return TheJson;
        //}
        //[WebMethod]
        //public string GetParameterList(int ReportId)
        //{
        //    var lstParams = new List<tblReportParameters>();
        //    var TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            lstParams = rc.GetReportParameterList(ReportId);
        //            if (lstParams != null)
        //            {
        //                // instantiate a serializer
        //                JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
        //                TheSerializer.MaxJsonLength = int.MaxValue;
        //                TheJson = TheSerializer.Serialize(lstParams);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);
        //    }
        //    return TheJson;
        //}
        //[WebMethod]
        //public string GenerateReport(int ReportId, List<string> paramList, List<string> paramNameList, string LoggedBy)
        //{
        //    string retURL = "";
        //    using (StudentController sc = new StudentController())
        //    using (ReportController rc = new ReportController())
        //    {
        //        string strPath = "";
        //        tblReportList rep = rc.GetReportListById(ReportId);
        //        if (rep.ReportType == "csv")
        //        {
        //            strPath = rc.GenerateReportCSV(rep, paramList, paramNameList, LoggedBy);
        //            if (!string.IsNullOrEmpty(strPath))
        //            {
        //                retURL = strPath;
        //            }
        //        }
        //        else
        //        {
        //            strPath = rc.GenerateReport(ReportId, paramList, paramNameList);
        //            if (!string.IsNullOrEmpty(strPath))
        //            {
        //                logger.Debug("GenerateTimeTableReport 1 : {0}", strPath);
        //                retURL = strPath;
        //            }
        //        }
        //    }
        //    return retURL;

        //}
        //[WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string GetValueFromDataSource()
        //{
        //    var TheJson = "";
        //    JToken body = null;
        //    string reportformInput = string.Empty;
        //    List<string> lstResult = new List<string>();
        //    List<tblReportParameters> lstReportParams = new List<tblReportParameters>();
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            body = Fn.ParseJson(this.Context);
        //            reportformInput = body["ListDataSource"].ToString();
        //            lstReportParams = Newtonsoft.Json.JsonConvert.DeserializeObject<List<tblReportParameters>>(reportformInput);
        //            foreach (tblReportParameters param in lstReportParams)
        //            {
        //                if (!string.IsNullOrEmpty(param.ParameterDataSource))
        //                {
        //                    string valuelist = rc.GetValueFromSPC(param.ParameterDataSource);
        //                    if (!string.IsNullOrEmpty(valuelist))
        //                    {
        //                        lstResult.Add(param.ReportParamId + "@@" + valuelist);
        //                    }
        //                }
        //            }
        //            if (lstResult != null)
        //            {
        //                // instantiate a serializer
        //                JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
        //                TheSerializer.MaxJsonLength = int.MaxValue;
        //                TheJson = TheSerializer.Serialize(lstResult);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error(e, body?.ToString());
        //    }
        //    return TheJson;
        //}
        //[WebMethod]
        //public string ExportGreenTreeCSV(string type, string dateFrom, string dateTo, string LoggedBy)
        //{
        //    string path = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            path = rc.ExportGreenTreeCSV(type, dateFrom, dateTo, LoggedBy);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);
        //    }
        //    return path;
        //}

        //[WebMethod]
        //public void TestSSRS()
        //{
        //    try
        //    {
        //        CET.Helpers.ReportRequest RPTClient = new CET.Helpers.ReportRequest("/CETReports/TestReport", "Report.PDF", new ConfigurationFactory.ReportConf());

        //        /*
        //                        RPTClient
        //                        .addParamter("JobNumber", jobno)
        //                        .addParamter("VoNo", vono)
        //                        .addParamter("Type", pdf_type);
        //        */

        //        string path = RPTClient.request().AsFile();
        //        //byte[] pdf_byte_array = RPTClient.request().AsByteArray();

        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, "Faild to create PDF ");
        //    }

        //}

        //public class ReportParam
        //{
        //    public string Name { get; set; }
        //    public string Value { get; set; }
        //}

        //#region Reporting

        //[WebMethod]
        //public string GetReports(string reportName)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            var list = rc.GetReports(reportName);

        //            if (list != null)
        //            {
        //                TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(list);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"GetReports error: {reportName}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        [WebMethod]
        public string AddEmptyReport(int userId)
        {
            string TheJson = "";
            try
            {
                using (ReportController rc = new ReportController())
                {
                    var reportId = rc.AddEmptyReport(userId);

                    TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(reportId);
                }
            }
            catch (Exception ex)
            {
                //string errMsg = Error.ErrorOutput(ex);

                //Log2File l = new Log2File();
                //l.WriteLog($"AddEmptyReport error: {userId.ToString()}");
                //l.WriteLog(ex.Message);
                //l.WriteLog(ex.StackTrace);
                logger.Error(ex);
            }

            return TheJson;
        }

        //[WebMethod]
        //public string UpdateReport(int id, string reportName, bool active)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            var ret = rc.UpdateReport(id, reportName, active);

        //            TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"UpdateReport error: {id.ToString()} | {reportName} | {active.ToString()}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string GetFieldsCategory()
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            var list = rc.GetFieldsCategory();

        //            if (list != null)
        //            {
        //                TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(list);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"GetFieldsCategory error:");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string GetFieldsByCategory(int categoryId)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            var list = rc.GetFieldsByCategory(categoryId);

        //            if (list != null)
        //            {
        //                TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(list);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"GetFieldsByCategory error: {categoryId.ToString()}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string GetReportFieldsByReportId(int reportId)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            var list = rc.GetReportFieldsByReportId(reportId);

        //            if (list != null)
        //            {
        //                TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(list);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"GetReportFieldsByReportId error: {reportId.ToString()}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string AddReportFields(int reportId, List<int> fieldList)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            var ret = rc.AddReportFields(reportId, fieldList);

        //            TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(ret);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"AddReportFields error: {reportId.ToString()}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string GetReportFiltersByReportId(int reportId)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            var list = rc.GetReportFiltersByReportId(reportId);

        //            if (list != null)
        //            {
        //                TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(list);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"GetReportFiltersByReportId error: {reportId.ToString()}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string AddEmptyReportFilter(int reportId)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(rc.AddEmptyReportFilter(reportId));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"AddEmptyReportFilter error: {reportId.ToString()}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        [WebMethod]
        public string GetComparators()
        {
           string TheJson = "";
           try
           {
               using (ReportController rc = new ReportController())
               {
                   var list = rc.GetComparators();

                   if (list != null)
                   {
                       TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                   }

               }
           }
           catch (Exception ex)
           {
               logger.Error(ex);

            //    string errMsg = Error.ErrorOutput(ex);

            //    Log2File l = new Log2File();
            //    l.WriteLog($"GetComparators error:");
            //    l.WriteLog(ex.Message);
            //    l.WriteLog(ex.StackTrace);
           }

           return TheJson;
        }

        //[WebMethod]
        //public string GetComparatorsByFieldId(int fieldId)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            var list = rc.GetComparators(fieldId);

        //            if (list != null)
        //            {
        //                TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(list);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"GetComparatorsByFieldTypeId error: {fieldId.ToString()}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string UpdateReportFilter(List<Reporting_Report_Filter> filterList, bool finalUpdate)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            foreach (var item in filterList)
        //            {
        //                if (finalUpdate)
        //                {
        //                    if (item.Field_Id > 0)
        //                    {
        //                        rc.UpdateReportFilter(item.Id, item.Field_Id, item.Comparator_Id, item.Value_1, item.Value_2, item.Filter_Order, item.Filter_Group, item.Conditional);
        //                    }
        //                    else
        //                    {
        //                        rc.DeleteReportFilter(item.Id);
        //                    }
        //                }
        //                else
        //                {
        //                    rc.UpdateReportFilter(item.Id, item.Field_Id, item.Comparator_Id, item.Value_1, item.Value_2, item.Filter_Order, item.Filter_Group, item.Conditional);
        //                }
        //            }

        //            TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"UpdateReportFilter error:");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string CreateDynamicReport(int reportId)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(rc.CreateDynamicReport(reportId));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"CreateDynamicReport error: {reportId.ToString()}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string DeleteReport(int reportId)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(rc.DeleteReport(reportId));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"DeleteReport error: {reportId.ToString()}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string DeleteReportFilter(int id)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(rc.DeleteReportFilter(id));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"DeleteReportFilter error: {id.ToString()}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string DeleteReportField(int id)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(rc.DeleteReportField(id));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"DeleteReportField error: {id.ToString()}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string MoveReportFieldSort(int id, int sortChange)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(rc.MoveReportFieldSort(id, sortChange));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"MoveReportFieldSort error: {id.ToString()} | {sortChange.ToString()}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string UpdateReportFieldTitle(int id, string title)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(rc.UpdateReportFieldTitle(id, title));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"UpdateReportFieldTitle error: {id.ToString()} | {title.ToString()}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string GetReportDropdownFields()
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            var list = rc.GetReportDropdownFields();

        //            if (list != null)
        //            {
        //                foreach (var item in list)
        //                {
        //                    item.List = rc.GetReportDropdownFieldsList(item);
        //                }

        //                TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(list);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"GetReportDropdownFields error:");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //[WebMethod]
        //public string GetFilterPreview(int reportId)
        //{
        //    string TheJson = "";
        //    try
        //    {
        //        using (ReportController rc = new ReportController())
        //        {
        //            TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(rc.GetFilterPreview(reportId));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);

        //        Log2File l = new Log2File();
        //        l.WriteLog($"GetFilterPreview error: {reportId.ToString()}");
        //        l.WriteLog(ex.Message);
        //        l.WriteLog(ex.StackTrace);
        //    }

        //    return TheJson;
        //}

        //#endregion
    }
}
