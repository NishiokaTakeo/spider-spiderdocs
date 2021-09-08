using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Globalization;
using ReportBuilder.Models;
//using CET.Utils;
using NLog;
using Newtonsoft.Json.Linq;
//using CsvHelper;
using ReportBuilder.Models.Report;
using ReportBuilder.Utils;
using CsvHelper;

namespace ReportBuilder.Controllers
{
    public class ReportController : DaoBase
    {
        //static String conn = Settings.CETDatabase;
        static string conn = SpiderDocsModule.SqlOperation.DBConnectionString;
        static string reportGETURL = ConfigurationManager.AppSettings["ReportHttpGetURL_Common"];

        static Logger logger = LogManager.GetCurrentClassLogger();

        public ReportController() : base(conn)
        {
//            conn = SpiderDocsModule.SqlOperation.DBConnectionString;
        }

        //public string GenerateReportCSV(tblReportList rep, List<string> paramValue, List<string> paramNameList, string LoggedBy)
        //{
        //    string retURL = "";
        //    try
        //    {
        //        using (SqlConnection sqlConn = new SqlConnection(conn))
        //        {
        //            sqlConn.Open();
        //            using (SqlCommand command = new SqlCommand("reporting." + rep.ReportName, sqlConn))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Clear();
        //                for (int i = 0; i < paramNameList.Count; i++)
        //                {
        //                    command.Parameters.AddWithValue("@" + paramNameList[i], paramValue[i]);
        //                }
        //                using (SqlDataReader dr = command.ExecuteReader())
        //                {
        //                    switch (rep.ReportName)
        //                    {
        //                        case "csvUnitBreakdownByMonthMain":
        //                            List<csvUnitBreakdownByMonthMain> lstFull = GetUnitBreakdownByMonth(dr);
        //                            retURL = CreateBreakdownFullCSV(rep.ReportName, lstFull, LoggedBy);
        //                            break;
        //                        case "csvUnitBreakdownByMonthConcMain":
        //                            List<csvUnitBreakdownByMonthConcMain> lstConc = GetUnitBreakdownByMonthConc(dr);
        //                            retURL = CreateBreakdownConcCSV(rep.ReportName, lstConc, LoggedBy);
        //                            break;
        //                        case "csvAttendanceAttended":
        //                            List<csvAttendanceAttended> lstAttend = GetAttendanceAttended(dr);
        //                            retURL = CreateAttendanceAttended(rep.ReportName, lstAttend, LoggedBy);
        //                            break;
        //                        case "csvScheduleEnrolStudent":
        //                            List<csvScheduleEnrolStudent> lstScheduleStudents = GetScheduleEnrolStudent(dr);
        //                            retURL = CreateScheduleEnrolStudent(rep.ReportName, lstScheduleStudents, LoggedBy);
        //                            break;
        //                        case "csvNYCReport":
        //                            List<csvNYCReport> lstNYC = GetNYCReport(dr);
        //                            retURL = CreateNYCReport(rep.ReportName, lstNYC, LoggedBy);
        //                            break;

        //                        default:
        //                            break;
        //                    }
        //                }
        //            }
        //            sqlConn.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return retURL;
        //}
        //public List<csvUnitBreakdownByMonthMain> GetUnitBreakdownByMonth(SqlDataReader dr)
        //{
        //    List<csvUnitBreakdownByMonthMain> lstFull = new List<csvUnitBreakdownByMonthMain>();
        //    while (dr.Read())
        //    {
        //        csvUnitBreakdownByMonthMain full = new csvUnitBreakdownByMonthMain();
        //        full.ComponentCode = (dr["ComponentCode"] == DBNull.Value) ? "" : dr["ComponentCode"].ToString();
        //        full.IntakeTrainingYearName = (dr["IntakeTrainingYearName"] == DBNull.Value) ? "" : dr["IntakeTrainingYearName"].ToString();
        //        full.StartDate = (dr["StartDateTimeUTC"] == DBNull.Value) ? "" : dr["StartDateTimeUTC"].ToString();
        //        full.FinishDate = (dr["EndDateTimeUTC"] == DBNull.Value) ? "" : dr["EndDateTimeUTC"].ToString();
        //        decimal dFullFee = (dr["FullCost"] == DBNull.Value) ? 0 : decimal.Parse(dr["FullCost"].ToString());
        //        full.FullFee = dFullFee.ToString("C2", CultureInfo.CurrentCulture);
        //        decimal dResourceFee = (dr["ResourceFee"] == DBNull.Value) ? 0 : decimal.Parse(dr["ResourceFee"].ToString());
        //        full.ResourceFee = dResourceFee.ToString("C2", CultureInfo.CurrentCulture);
        //        int dNoStudents = (dr["NoStudents"] == DBNull.Value) ? 0 : int.Parse(dr["NoStudents"].ToString());
        //        full.TotalStudents = dNoStudents.ToString();
        //        decimal dExtendedFee = (dFullFee + dResourceFee) * dNoStudents;
        //        full.ExtendedFee = dExtendedFee.ToString("C2", CultureInfo.CurrentCulture);
        //        lstFull.Add(full);
        //    }
        //    return lstFull;
        //}
        //public List<csvUnitBreakdownByMonthConcMain> GetUnitBreakdownByMonthConc(SqlDataReader dr)
        //{
        //    List<csvUnitBreakdownByMonthConcMain> lstConc = new List<csvUnitBreakdownByMonthConcMain>();
        //    while (dr.Read())
        //    {
        //        csvUnitBreakdownByMonthConcMain conc = new csvUnitBreakdownByMonthConcMain();
        //        conc.ComponentCode = (dr["ComponentCode"] == DBNull.Value) ? "" : dr["ComponentCode"].ToString();
        //        conc.IntakeTrainingYearName = (dr["IntakeTrainingYearName"] == DBNull.Value) ? "" : dr["IntakeTrainingYearName"].ToString();
        //        conc.StartDate = (dr["StartDateTimeUTC"] == DBNull.Value) ? "" : dr["StartDateTimeUTC"].ToString();
        //        conc.FinishDate = (dr["EndDateTimeUTC"] == DBNull.Value) ? "" : dr["EndDateTimeUTC"].ToString();
        //        decimal dConcFee = (dr["ConcCost"] == DBNull.Value) ? 0 : decimal.Parse(dr["ConcCost"].ToString());
        //        conc.ConcFee = dConcFee.ToString("C2", CultureInfo.CurrentCulture);
        //        decimal dResourceFee = (dr["ResourceFee"] == DBNull.Value) ? 0 : decimal.Parse(dr["ResourceFee"].ToString());
        //        conc.ResourceFee = dResourceFee.ToString("C2", CultureInfo.CurrentCulture);
        //        int dNoStudents = (dr["NoStudents"] == DBNull.Value) ? 0 : int.Parse(dr["NoStudents"].ToString());
        //        conc.TotalStudents = dNoStudents.ToString();
        //        decimal dExtendedFee = (dConcFee + dResourceFee) * dNoStudents;
        //        conc.ExtendedFee = dExtendedFee.ToString("C2", CultureInfo.CurrentCulture);
        //        lstConc.Add(conc);
        //    }
        //    return lstConc;
        //}
        //public List<csvAttendanceAttended> GetAttendanceAttended(SqlDataReader dr)
        //{
        //    List<csvAttendanceAttended> lstAttend = new List<csvAttendanceAttended>();
        //    while (dr.Read())
        //    {
        //        csvAttendanceAttended att = new csvAttendanceAttended();
        //        att.Campus = (dr["Campus"] == DBNull.Value) ? "" : dr["Campus"].ToString();
        //        att.ClassName = (dr["ClassName"] == DBNull.Value) ? "" : dr["ClassName"].ToString();
        //        int StudentId = (dr["StudentId"] == DBNull.Value) ? 0 : int.Parse(dr["StudentId"].ToString());
        //        att.StudentId = StudentId.ToString();
        //        att.StudentName = (dr["StudentName"] == DBNull.Value) ? "" : dr["StudentName"].ToString();
        //        att.GTImported = (dr["GTImported"] == DBNull.Value) ? "" : dr["GTImported"].ToString();
        //        lstAttend.Add(att);
        //    }
        //    return lstAttend;
        //}
        //public List<csvScheduleEnrolStudent> GetScheduleEnrolStudent(SqlDataReader dr)
        //{
        //    List<csvScheduleEnrolStudent> lstScheduledStudent = new List<csvScheduleEnrolStudent>();
        //    while (dr.Read())
        //    {
        //        csvScheduleEnrolStudent st = new csvScheduleEnrolStudent();
        //        st.Campus = (dr["Campus"] == DBNull.Value) ? "" : dr["Campus"].ToString();
        //        st.ClassName = (dr["ClassName"] == DBNull.Value) ? "" : dr["ClassName"].ToString();
        //        st.StartDate = (dr["StartDate"] == DBNull.Value) ? "" : dr["StartDate"].ToString();
        //        int StudentId = (dr["StudentId"] == DBNull.Value) ? 0 : int.Parse(dr["StudentId"].ToString());
        //        st.StudentId = StudentId.ToString();
        //        st.StudentName = (dr["StudentName"] == DBNull.Value) ? "" : dr["StudentName"].ToString();
        //        st.StudentMobile = (dr["StudentMobile"] == DBNull.Value) ? "" : Utilities.FormatMobileNumber(dr["StudentMobile"].ToString());
        //        lstScheduledStudent.Add(st);
        //    }
        //    return lstScheduledStudent;
        //}
        //public List<csvNYCReport> GetNYCReport(SqlDataReader dr)
        //{
        //    List<csvNYCReport> lstNYCReport = new List<csvNYCReport>();
        //    while (dr.Read())
        //    {
        //        csvNYCReport nyc = new csvNYCReport();
        //        nyc.Campus = (dr["Campus"] == DBNull.Value) ? "" : dr["Campus"].ToString();
        //        int StudentID = (dr["Student ID"] == DBNull.Value) ? 0 : int.Parse(dr["Student ID"].ToString());
        //        nyc.StudentID = StudentID.ToString();
        //        nyc.StudentName = (dr["Student Name"] == DBNull.Value) ? "" : dr["Student Name"].ToString();
        //        nyc.ClassName = (dr["Class Name"] == DBNull.Value) ? "" : dr["Class Name"].ToString();
        //        nyc.Qualification = (dr["Qualification"] == DBNull.Value) ? "" : dr["Qualification"].ToString();
        //        nyc.Unit = (dr["Unit"] == DBNull.Value) ? "" : dr["Unit"].ToString();
        //        nyc.Status = (dr["Status"] == DBNull.Value) ? "" : dr["Status"].ToString();
        //        lstNYCReport.Add(nyc);
        //    }
        //    return lstNYCReport;
        //}

        //public string CreateBreakdownFullCSV(string ReportName, List<csvUnitBreakdownByMonthMain> lstFull, string LoggedBy = "")
        //{
        //    string strPath = GenerateCSVFileName(ReportName, LoggedBy);
        //    try
        //    {
        //        using (var writer = new StreamWriter(strPath))
        //        using (var csv = new CsvWriter(writer))
        //        {
        //            csv.WriteRecords(lstFull);
        //            writer.Flush();
        //            strPath = Fn.ToWebPath(Fn.ToWebURL(strPath));
        //            // Add Log
        //            LogController.Add_Log("CSV Report", "0", "Created Class With Full Fee Students (CSV)", LoggedBy);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strPath = "";
        //    }
        //    return strPath;
        //}
        //public string CreateBreakdownConcCSV(string ReportName, List<csvUnitBreakdownByMonthConcMain> lstConc, string LoggedBy = "")
        //{
        //    string strPath = GenerateCSVFileName(ReportName, LoggedBy);
        //    try
        //    {
        //        using (var writer = new StreamWriter(strPath))
        //        using (var csv = new CsvWriter(writer))
        //        {
        //            csv.WriteRecords(lstConc);
        //            writer.Flush();
        //            strPath = Fn.ToWebPath(Fn.ToWebURL(strPath));
        //            // Add Log
        //            LogController.Add_Log("CSV Report", "0", "Created Class With Conc Fee Students (CSV)", LoggedBy);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strPath = "";
        //    }
        //    return strPath;
        //}
        //public string CreateAttendanceAttended(string ReportName, List<csvAttendanceAttended> lstAtt, string LoggedBy = "")
        //{
        //    string strPath = GenerateCSVFileName(ReportName, LoggedBy);
        //    try
        //    {
        //        using (var writer = new StreamWriter(strPath))
        //        using (var csv = new CsvWriter(writer))
        //        {
        //            csv.WriteRecords(lstAtt);
        //            writer.Flush();
        //            strPath = Fn.ToWebPath(Fn.ToWebURL(strPath));
        //            // Add Log
        //            LogController.Add_Log("CSV Report", "0", "Attendance Report (CSV)", LoggedBy);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strPath = "";
        //    }
        //    return strPath;
        //}
        //public string CreateScheduleEnrolStudent(string ReportName, List<csvScheduleEnrolStudent> lstScheduleStudents, string LoggedBy = "")
        //{
        //    string strPath = GenerateCSVFileName(ReportName, LoggedBy);
        //    try
        //    {
        //        using (var writer = new StreamWriter(strPath))
        //        using (var csv = new CsvWriter(writer))
        //        {
        //            csv.WriteRecords(lstScheduleStudents);
        //            writer.Flush();
        //            strPath = Fn.ToWebPath(Fn.ToWebURL(strPath));
        //            // Add Log
        //            LogController.Add_Log("CSV Report", "0", "Apprentice Units Start Students Report (CSV)", LoggedBy);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strPath = "";
        //    }
        //    return strPath;
        //}
        //public string CreateNYCReport(string ReportName, List<csvNYCReport> lstNYCReport, string LoggedBy = "")
        //{
        //    string strPath = GenerateCSVFileName(ReportName, LoggedBy);
        //    try
        //    {
        //        using (var writer = new StreamWriter(strPath))
        //        using (var csv = new CsvWriter(writer))
        //        {
        //            csv.WriteRecords(lstNYCReport);
        //            writer.Flush();
        //            strPath = Fn.ToWebPath(Fn.ToWebURL(strPath));
        //            // Add Log
        //            LogController.Add_Log("CSV Report", "0", "NYC Report (CSV)", LoggedBy);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strPath = "";
        //    }
        //    return strPath;
        //}

        //public string GenerateReport(int ReportId, List<string> paramValue, List<string> paramNameList)
        //{
        //    string retURL = "";
        //    string html = string.Empty;
        //    string url = reportGETURL;
        //    string queryString = "";
        //    //// Report Name
        //    //using (ReportController rc = new ReportController())
        //    //{
        //    //    tblReportList rep = rc.GetReportListById(ReportId);
        //    //    queryString += "ReportName=" + rep.ReportName;
        //    //}
        //    // Report ID
        //    queryString += "ReportId=" + ReportId;

        //    // Report Parameter
        //    for (int i = 0; i < paramNameList.Count; i++)
        //    {
        //        if (!string.IsNullOrEmpty(queryString))
        //        {
        //            queryString += "&";
        //        }
        //        queryString += paramNameList[i] + "=" + paramValue[i];
        //    }
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + queryString);
        //    request.Method = "GET";
        //    request.ContentType = "application/x-www-form-urlencoded";

        //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //    {
        //        using (Stream stream = response.GetResponseStream())
        //        {
        //            using (StreamReader reader = new StreamReader(stream))
        //            {
        //                html = reader.ReadToEnd();
        //                string tagName = "url";
        //                int startIndex = html.IndexOf(tagName) + tagName.Length + 2;
        //                int endIndex = html.IndexOf("</div>", startIndex);
        //                retURL = html.Substring(startIndex, endIndex - startIndex);
        //            }
        //        }
        //    }

        //    return retURL;
        //}

        //public string GenerateTimeTable(int studentId, int Year, int CampusId, int CourseId, int SITYId, int index)
        //{
        //    string retURL = "";
        //    string html = string.Empty;
        //    string url = reportGETURL;
        //    string queryString = "";
        //    // Report Name
        //    queryString += "ReportName=rptTimetable";
        //    // Year
        //    if (studentId > 0)
        //    {
        //        if (!string.IsNullOrEmpty(queryString))
        //        {
        //            queryString += "&";
        //        }
        //        queryString += "StudentId=" + studentId.ToString();
        //    }
        //    // Year
        //    if (Year > 0)
        //    {
        //        if (!string.IsNullOrEmpty(queryString))
        //        {
        //            queryString += "&";
        //        }
        //        queryString += "Year=" + Year.ToString();
        //    }
        //    // Campus
        //    if (CampusId > 0)
        //    {
        //        if (!string.IsNullOrEmpty(queryString))
        //        {
        //            queryString += "&";
        //        }
        //        queryString += "Campus=" + CampusId;
        //    }
        //    // CourseId
        //    if (CourseId > 0)
        //    {
        //        if (!string.IsNullOrEmpty(queryString))
        //        {
        //            queryString += "&";
        //        }
        //        queryString += "CourseId=" + CourseId;
        //    }
        //    // SITYId
        //    if (SITYId > 0)
        //    {
        //        if (!string.IsNullOrEmpty(queryString))
        //        {
        //            queryString += "&";
        //        }
        //        queryString += "SITYId=" + SITYId;
        //    }
        //    // Index
        //    if (index > 0)
        //    {
        //        if (!string.IsNullOrEmpty(queryString))
        //        {
        //            queryString += "&";
        //        }
        //        queryString += "Index=" + index;
        //    }
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + queryString);
        //    request.Method = "GET";
        //    request.ContentType = "application/x-www-form-urlencoded";

        //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //    {
        //        using (Stream stream = response.GetResponseStream())
        //        {
        //            using (StreamReader reader = new StreamReader(stream))
        //            {
        //                html = reader.ReadToEnd();
        //                string tagName = "url";
        //                int startIndex = html.IndexOf(tagName) + tagName.Length + 2;
        //                int endIndex = html.IndexOf("</div>", startIndex);
        //                retURL = html.Substring(startIndex, endIndex - startIndex);
        //            }
        //        }
        //    }

        //    return retURL;
        //}

        //public string GenerateAttendanceSheet(int ClassId, string StartDate, string EndDate)
        //{
        //    string retURL = "";
        //    string html = string.Empty;
        //    string url = reportGETURL;
        //    string queryString = "";
        //    using (AttendanceController ac = new AttendanceController())
        //    {
        //        // Report Name
        //        queryString += "ReportName=rptattendancesheetMain";
        //        // ClassId
        //        if (ClassId > 0)
        //        {
        //            if (!string.IsNullOrEmpty(queryString))
        //            {
        //                queryString += "&";
        //            }
        //            queryString += "ClassId=" + ClassId.ToString();

        //            // Attendance Id
        //            int AttendanceId = ac.GetAttendanceIdByClassId(ClassId);
        //            if (AttendanceId > 0)
        //            {
        //                if (!string.IsNullOrEmpty(queryString))
        //                {
        //                    queryString += "&";
        //                }
        //                queryString += "AttendanceId=" + AttendanceId.ToString();
        //            }
        //            // StartDate
        //            if (!string.IsNullOrEmpty(StartDate))
        //            {
        //                if (!string.IsNullOrEmpty(queryString))
        //                {
        //                    queryString += "&";
        //                }
        //                queryString += "StartDate=" + StartDate;
        //            }
        //            // EndDate
        //            if (!string.IsNullOrEmpty(EndDate))
        //            {
        //                if (!string.IsNullOrEmpty(queryString))
        //                {
        //                    queryString += "&";
        //                }
        //                queryString += "EndDate=" + EndDate;
        //            }
        //            // Day 1 to Day 5
        //            List<AttendanceDay> lstDays = ac.GetAttendanceDaysListByAttendanceId(AttendanceId, StartDate, EndDate);
        //            for (int i = 0; i < 5; i++)
        //            {
        //                string strDay = "";
        //                if (i < lstDays.Count)
        //                {
        //                    strDay = lstDays[i].Date;
        //                }
        //                if (!string.IsNullOrEmpty(queryString))
        //                {
        //                    queryString += "&";
        //                }
        //                queryString += String.Format("Day{0}={1}", (i + 1), strDay);
        //            }
        //        }
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + queryString);
        //        request.Method = "GET";
        //        request.ContentType = "application/x-www-form-urlencoded";

        //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        {
        //            using (Stream stream = response.GetResponseStream())
        //            {
        //                using (StreamReader reader = new StreamReader(stream))
        //                {
        //                    html = reader.ReadToEnd();
        //                    string tagName = "url";
        //                    int startIndex = html.IndexOf(tagName) + tagName.Length + 2;
        //                    int endIndex = html.IndexOf("</div>", startIndex);
        //                    retURL = html.Substring(startIndex, endIndex - startIndex);
        //                }
        //            }
        //        }
        //    }
        //    return retURL;
        //}

        //public List<tblReportList> GetReportList()
        //{
        //    Dictionary<string, object> dic = new Dictionary<string, object>();

        //    String sql = "SELECT * FROM [dbo].[tblReportList] WHERE [VisibleFromReport] = 'true' ORDER BY [ReportDescription]";

        //    IEnumerable<tblReportList> tbls = this.ExecuteSQL<tblReportList>(sql, dic);

        //    return tbls.ToList();
        //}

        //public tblReportList GetReportListById(int ReportId)
        //{
        //    Dictionary<string, object> dic = new Dictionary<string, object>();

        //    String sql = "SELECT * FROM [dbo].[tblReportList] WHERE ReportId = @rid";
        //    dic.Add("@rid", ReportId);

        //    IEnumerable<tblReportList> tbls = this.ExecuteSQL<tblReportList>(sql, dic);
        //    return tbls.Count() == 0 ? new tblReportList() : tbls.FirstOrDefault();
        //}

        //public tblReportList GetReportListByName(string ReportName)
        //{
        //    Dictionary<string, object> dic = new Dictionary<string, object>();

        //    String sql = "SELECT * FROM [dbo].[tblReportList] WHERE ReportName = @rname";
        //    dic.Add("@rname", ReportName);

        //    IEnumerable<tblReportList> tbls = this.ExecuteSQL<tblReportList>(sql, dic);
        //    return tbls.Count() == 0 ? new tblReportList() : tbls.FirstOrDefault();
        //}

        //public List<tblReportParameters> GetReportParameterList(int ReportId)
        //{
        //    Dictionary<string, object> dic = new Dictionary<string, object>();

        //    String sql = "SELECT * FROM [dbo].[tblReportParameters] WHERE ReportId = @rid";
        //    dic.Add("@rid", ReportId);

        //    IEnumerable<tblReportParameters> tbls = this.ExecuteSQL<tblReportParameters>(sql, dic);
        //    return tbls.ToList();
        //}

        //public string GetValueFromSPC(string ParameterDataSource)
        //{
        //    string valuelist = "";
        //    try
        //    {
        //        using (SqlConnection sqlConn = new SqlConnection(conn))
        //        {
        //            sqlConn.Open();
        //            using (SqlCommand command = new SqlCommand(ParameterDataSource.Trim(), sqlConn))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Clear();
        //                using (SqlDataReader dr = command.ExecuteReader())
        //                {
        //                    while (dr.Read())
        //                    {
        //                        if (!string.IsNullOrEmpty(valuelist))
        //                        {
        //                            valuelist += "|";
        //                        }
        //                        string id = (dr["id"] == DBNull.Value) ? "" : dr["id"].ToString();
        //                        string value = (dr["value"] == DBNull.Value) ? "" : dr["value"].ToString();
        //                        valuelist += id + "##" + value;
        //                    }
        //                }
        //            }
        //            sqlConn.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = Error.ErrorOutput(ex);
        //    }
        //    return valuelist;
        //}

        //public string GenerateClassResultsReport(int ClassId)
        //{
        //    string retURL = "";
        //    string html = string.Empty;
        //    string url = reportGETURL;
        //    string queryString = "";
        //    // Check Cluster component
        //    using (ClassController cc = new ClassController())
        //    using (ComponentController compc = new ComponentController())
        //    {
        //        tblClass tblCc = cc.Get(ClassId);
        //        int ComponentId = tblCc.ComponentId;
        //        int ComponentVersion = tblCc.ComponentVersion;
        //        List<tblClusterComponent> lstClusterComponents = compc.GetClusterComponents(ComponentId, ComponentVersion);
        //        if (lstClusterComponents.Count > 0)
        //        {
        //            // Cluster Component
        //            // Report Name
        //            queryString += "ReportName=rptClassCCResultsMain";
        //            // ClassId
        //            if (ClassId > 0)
        //            {
        //                if (!string.IsNullOrEmpty(queryString))
        //                {
        //                    queryString += "&";
        //                }
        //                queryString += "ClassId=" + ClassId.ToString();
        //            }
        //        }
        //        else
        //        {
        //            // Report Name
        //            queryString += "ReportName=rptClassResultsMain";
        //            // ClassId
        //            if (ClassId > 0)
        //            {
        //                if (!string.IsNullOrEmpty(queryString))
        //                {
        //                    queryString += "&";
        //                }
        //                queryString += "ClassId=" + ClassId.ToString();
        //            }
        //        }
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + queryString);
        //        request.Method = "GET";
        //        request.ContentType = "application/x-www-form-urlencoded";

        //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        {
        //            using (Stream stream = response.GetResponseStream())
        //            {
        //                using (StreamReader reader = new StreamReader(stream))
        //                {
        //                    html = reader.ReadToEnd();
        //                    string tagName = "url";
        //                    int startIndex = html.IndexOf(tagName) + tagName.Length + 2;
        //                    int endIndex = html.IndexOf("</div>", startIndex);
        //                    retURL = html.Substring(startIndex, endIndex - startIndex);
        //                }
        //            }
        //        }
        //    }
        //    return retURL;
        //}

        //public string GenerateAttendanceSignSheetReport(int ClassId)
        //{
        //    string retURL = "";
        //    string html = string.Empty;
        //    string url = reportGETURL;
        //    string queryString = "";
        //    // Report Name
        //    queryString += "ReportName=rptAttendanceSignSheetMain";
        //    // ClassId
        //    if (ClassId > 0)
        //    {
        //        if (!string.IsNullOrEmpty(queryString))
        //        {
        //            queryString += "&";
        //        }
        //        queryString += "ClassId=" + ClassId.ToString();

        //        using (var attendanceController = new AttendanceController())
        //        {
        //            attendanceController.CreateAttendanceMain(ClassId);
        //        }
        //    }

        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + queryString);
        //    request.Method = "GET";
        //    request.ContentType = "application/x-www-form-urlencoded";

        //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //    {
        //        using (Stream stream = response.GetResponseStream())
        //        {
        //            using (StreamReader reader = new StreamReader(stream))
        //            {
        //                html = reader.ReadToEnd();
        //                string tagName = "url";
        //                int startIndex = html.IndexOf(tagName) + tagName.Length + 2;
        //                int endIndex = html.IndexOf("</div>", startIndex);
        //                retURL = html.Substring(startIndex, endIndex - startIndex);
        //            }
        //        }
        //    }

        //    return retURL;
        //}

        //public string ExportGreenTreeCSV(string type, string dateFrom, string dateTo, string LoggedBy)
        //{
        //    string strPath = "";
        //    switch (type)
        //    {
        //        case "ARINV":
        //            List<GTInvoiceExport> lstGTInvoice = GetInvoicesByDate(dateFrom, dateTo);
        //            strPath = CreateInvoicesCSV(type, lstGTInvoice, LoggedBy);
        //            break;
        //        case "CRN":
        //            List<GTCreditNoteExport> lstGTCreditNote = GetCreditNotesByDate(dateFrom, dateTo);
        //            strPath = CreateCreditNotesCSV(type, lstGTCreditNote, LoggedBy);
        //            break;
        //        case "REC":
        //            List<GTReceiptExport> lstGTReceipt = GetReceiptsByDate(dateFrom, dateTo);
        //            strPath = CreateReceiptsCSV(type, lstGTReceipt, LoggedBy);
        //            break;
        //        case "ARCUST":
        //            List<GTNewCustomersExport> lstGTNewCustomers = GetNewCustomersByDate(dateFrom, dateTo);
        //            strPath = CreateNewCustomersCSV(type, lstGTNewCustomers, LoggedBy);
        //            break;
        //    }
        //    return strPath;
        //}
        //public List<GTInvoiceExport> GetInvoicesByDate(string dateFrom, string dateTo)
        //{
        //    List<GTInvoiceExport> lstGTInvoice = new List<GTInvoiceExport>();
        //    try
        //    {
        //        using (SqlConnection sqlConn = new SqlConnection(conn))
        //        {
        //            sqlConn.Open();
        //            using (SqlCommand command = new SqlCommand("dbo.spcGetInvoicesByDateRange", sqlConn))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Clear();
        //                command.Parameters.AddWithValue("@InvoiceDateStart", dateFrom);
        //                command.Parameters.AddWithValue("@InvoiceDateEnd", dateTo);
        //                using (SqlDataReader dr = command.ExecuteReader())
        //                {
        //                    while (dr.Read())
        //                    {
        //                        GTInvoiceExport gie = new GTInvoiceExport();
        //                        gie.TransactionType = (dr["Transaction Type"] == DBNull.Value) ? "" : dr["Transaction Type"].ToString();
        //                        gie.ReferenceNumber = (dr["Reference Number"] == DBNull.Value) ? "" : dr["Reference Number"].ToString();
        //                        gie.TransactionDate = (dr["Transaction Date"] == DBNull.Value) ? "" : dr["Transaction Date"].ToString();
        //                        gie.DocumentDate = (dr["DocumentDate"] == DBNull.Value) ? "" : dr["DocumentDate"].ToString();
        //                        gie.CustomerCode = (dr["Customer Code"] == DBNull.Value) ? "" : dr["Customer Code"].ToString();
        //                        gie.StudentName = (dr["StudentName"] == DBNull.Value) ? "" : dr["StudentName"].ToString();
        //                        gie.EmployerCode = (dr["Employer Code"] == DBNull.Value) ? "" : dr["Employer Code"].ToString();
        //                        gie.OldCustomerCode = (dr["Old Customer Code"] == DBNull.Value) ? "" : dr["Old Customer Code"].ToString();
        //                        gie.GLAccount = (dr["GL Account"] == DBNull.Value) ? "" : dr["GL Account"].ToString();
        //                        gie.Amount = (dr["Amount"] == DBNull.Value) ? 0 : decimal.Parse(dr["Amount"].ToString());
        //                        gie.TaxAmount = (dr["Tax Amount"] == DBNull.Value) ? 0 : decimal.Parse(dr["Tax Amount"].ToString());
        //                        gie.Quantity = (dr["Quantity"] == DBNull.Value) ? 0 : int.Parse(dr["Quantity"].ToString());
        //                        gie.TaxCode = (dr["Tax Code"] == DBNull.Value) ? "" : dr["Tax Code"].ToString();
        //                        gie.LineType = (dr["Line Type"] == DBNull.Value) ? "" : dr["Line Type"].ToString();
        //                        gie.TaxType = (dr["Tax Type"] == DBNull.Value) ? "" : dr["Tax Type"].ToString();
        //                        gie.LineNarration = (dr["Line Narration"] == DBNull.Value) ? "" : dr["Line Narration"].ToString();
        //                        gie.AppliedTR = (dr["Applied Transaction Reference"] == DBNull.Value) ? "" : dr["Applied Transaction Reference"].ToString();
        //                        gie.PaymentSchedule = (dr["Payment Schedule"] == DBNull.Value) ? "" : dr["Payment Schedule"].ToString();
        //                        gie.PaymentScheduleNumber = (dr["Payment Schedule Number"] == DBNull.Value) ? "" : dr["Payment Schedule Number"].ToString();
        //                        gie.BranchCode = (dr["Branch Code"] == DBNull.Value) ? "" : dr["Branch Code"].ToString();
        //                        gie.OrderNumber = (dr["Order Number"] == DBNull.Value) ? "" : dr["Order Number"].ToString();
        //                        gie.PaymentDate = (dr["Payment Date"] == DBNull.Value) ? "" : dr["Payment Date"].ToString();
        //                        gie.PostingDate = (dr["Posting Date"] == DBNull.Value) ? "" : dr["Posting Date"].ToString();
        //                        lstGTInvoice.Add(gie);
        //                    }
        //                }
        //            }
        //            sqlConn.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return lstGTInvoice;
        //}
        //public List<GTCreditNoteExport> GetCreditNotesByDate(string dateFrom, string dateTo)
        //{
        //    List<GTCreditNoteExport> lstGTCreditNote = new List<GTCreditNoteExport>();
        //    try
        //    {
        //        using (SqlConnection sqlConn = new SqlConnection(conn))
        //        {
        //            sqlConn.Open();
        //            using (SqlCommand command = new SqlCommand("dbo.spcGetCreditNotesByDateRange", sqlConn))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Clear();
        //                command.Parameters.AddWithValue("@CreditNoteDateStart", dateFrom);
        //                command.Parameters.AddWithValue("@CreditNoteDateEnd", dateTo);
        //                using (SqlDataReader dr = command.ExecuteReader())
        //                {
        //                    while (dr.Read())
        //                    {
        //                        GTCreditNoteExport gie = new GTCreditNoteExport();
        //                        gie.TransactionType = (dr["Transaction Type"] == DBNull.Value) ? "" : dr["Transaction Type"].ToString();
        //                        gie.ReferenceNumber = (dr["Reference Number"] == DBNull.Value) ? "" : dr["Reference Number"].ToString();
        //                        gie.TransactionDate = (dr["Transaction Date"] == DBNull.Value) ? "" : dr["Transaction Date"].ToString();
        //                        gie.DocumentDate = (dr["DocumentDate"] == DBNull.Value) ? "" : dr["DocumentDate"].ToString();
        //                        gie.CustomerCode = (dr["Customer Code"] == DBNull.Value) ? "" : dr["Customer Code"].ToString();
        //                        gie.StudentName = (dr["StudentName"] == DBNull.Value) ? "" : dr["StudentName"].ToString();
        //                        gie.OldCustomerCode = (dr["Old Customer Code"] == DBNull.Value) ? "" : dr["Old Customer Code"].ToString();
        //                        gie.GLAccount = (dr["GL Account"] == DBNull.Value) ? "" : dr["GL Account"].ToString();
        //                        gie.Amount = (dr["Amount"] == DBNull.Value) ? 0 : decimal.Parse(dr["Amount"].ToString());
        //                        gie.TaxAmount = (dr["Tax Amount"] == DBNull.Value) ? 0 : decimal.Parse(dr["Tax Amount"].ToString());
        //                        gie.Quantity = (dr["Quantity"] == DBNull.Value) ? 0 : int.Parse(dr["Quantity"].ToString());
        //                        gie.TaxCode = (dr["Tax Code"] == DBNull.Value) ? "" : dr["Tax Code"].ToString();
        //                        gie.LineType = (dr["Line Type"] == DBNull.Value) ? "" : dr["Line Type"].ToString();
        //                        gie.TaxType = (dr["Tax Type"] == DBNull.Value) ? "" : dr["Tax Type"].ToString();
        //                        gie.LineNarration = (dr["Line Narration"] == DBNull.Value) ? "" : dr["Line Narration"].ToString();
        //                        gie.AppliedTR = (dr["Applied Transaction Reference"] == DBNull.Value) ? "" : dr["Applied Transaction Reference"].ToString();
        //                        gie.PaymentSchedule = (dr["Payment Schedule"] == DBNull.Value) ? "" : dr["Payment Schedule"].ToString();
        //                        gie.PaymentScheduleNumber = (dr["Payment Schedule Number"] == DBNull.Value) ? "" : dr["Payment Schedule Number"].ToString();
        //                        gie.BranchCode = (dr["Branch Code"] == DBNull.Value) ? "" : dr["Branch Code"].ToString();
        //                        gie.OrderNumber = (dr["Order Number"] == DBNull.Value) ? "" : dr["Order Number"].ToString();
        //                        gie.PaymentDate = (dr["Payment Date"] == DBNull.Value) ? "" : dr["Payment Date"].ToString();
        //                        gie.PostingDate = (dr["Posting Date"] == DBNull.Value) ? "" : dr["Posting Date"].ToString();
        //                        lstGTCreditNote.Add(gie);
        //                    }
        //                }
        //            }
        //            sqlConn.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return lstGTCreditNote;
        //}
        //public List<GTReceiptExport> GetReceiptsByDate(string dateFrom, string dateTo)
        //{
        //    List<GTReceiptExport> lstGTReceipts = new List<GTReceiptExport>();
        //    try
        //    {
        //        using (SqlConnection sqlConn = new SqlConnection(conn))
        //        {
        //            sqlConn.Open();
        //            using (SqlCommand command = new SqlCommand("dbo.spcGetReceiptsByDateRange", sqlConn))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Clear();
        //                command.Parameters.AddWithValue("@ReceiptDateStart", dateFrom);
        //                command.Parameters.AddWithValue("@ReceiptDateEnd", dateTo);
        //                using (SqlDataReader dr = command.ExecuteReader())
        //                {
        //                    while (dr.Read())
        //                    {
        //                        GTReceiptExport gie = new GTReceiptExport();
        //                        gie.TransactionType = (dr["Transaction Type"] == DBNull.Value) ? "" : dr["Transaction Type"].ToString();
        //                        gie.ReferenceNumber = (dr["Reference Number"] == DBNull.Value) ? "" : dr["Reference Number"].ToString();
        //                        gie.PaymentDate = (dr["Payment Date"] == DBNull.Value) ? "" : dr["Payment Date"].ToString();
        //                        gie.CustomerCode = (dr["Customer Code"] == DBNull.Value) ? "" : dr["Customer Code"].ToString();
        //                        gie.Amount = (dr["Amount"] == DBNull.Value) ? 0 : decimal.Parse(dr["Amount"].ToString());
        //                        gie.TaxAmount = (dr["Tax Amount"] == DBNull.Value) ? 0 : decimal.Parse(dr["Tax Amount"].ToString());
        //                        gie.Quantity = (dr["Quantity"] == DBNull.Value) ? 0 : int.Parse(dr["Quantity"].ToString());
        //                        gie.TaxCode = (dr["Tax Code"] == DBNull.Value) ? "" : dr["Tax Code"].ToString();
        //                        gie.LineType = (dr["Line Type"] == DBNull.Value) ? "" : dr["Line Type"].ToString();
        //                        gie.TaxType = (dr["Tax Type"] == DBNull.Value) ? "" : dr["Tax Type"].ToString();
        //                        gie.LineNarration = (dr["Line Narration"] == DBNull.Value) ? "" : dr["Line Narration"].ToString();
        //                        gie.AppliedTR = (dr["Applied Transaction Reference"] == DBNull.Value) ? "" : dr["Applied Transaction Reference"].ToString();
        //                        gie.PaymentSchedule = (dr["Payment Schedule"] == DBNull.Value) ? "" : dr["Payment Schedule"].ToString();
        //                        gie.PaymentScheduleNumber = (dr["Payment Schedule Number"] == DBNull.Value) ? "" : dr["Payment Schedule Number"].ToString();
        //                        gie.BranchCode = (dr["Branch Code"] == DBNull.Value) ? "" : dr["Branch Code"].ToString();
        //                        gie.OrderNumber = (dr["Order Number"] == DBNull.Value) ? "" : dr["Order Number"].ToString();
        //                        gie.TransactionDate = (dr["Transaction Date"] == DBNull.Value) ? "" : dr["Transaction Date"].ToString();
        //                        gie.PostingDate = (dr["Posting Date"] == DBNull.Value) ? "" : dr["Posting Date"].ToString();
        //                        gie.ReceiptType = (dr["Receipt Type"] == DBNull.Value) ? "" : dr["Receipt Type"].ToString();
        //                        gie.BankAccountCode = (dr["Bank Account Code"] == DBNull.Value) ? "" : dr["Bank Account Code"].ToString();
        //                        gie.CreditCard = (dr["Credit Card"] == DBNull.Value) ? "" : dr["Credit Card"].ToString();
        //                        gie.AppliedTransType = (dr["Applied Trans Type"] == DBNull.Value) ? "" : dr["Applied Trans Type"].ToString();
        //                        gie.CreditCardNumber = (dr["Credit Card Number"] == DBNull.Value) ? "" : dr["Credit Card Number"].ToString();
        //                        gie.CreditCardExpiry = (dr["Credit Card Expiry"] == DBNull.Value) ? "" : dr["Credit Card Expiry"].ToString();
        //                        gie.CreditCardName = (dr["Credit Card Name"] == DBNull.Value) ? "" : dr["Credit Card Name"].ToString();
        //                        lstGTReceipts.Add(gie);
        //                    }
        //                }
        //            }
        //            sqlConn.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return lstGTReceipts;
        //}
        //public List<GTNewCustomersExport> GetNewCustomersByDate(string dateFrom, string dateTo)
        //{
        //    List<GTNewCustomersExport> lstGTNewCustomers = new List<GTNewCustomersExport>();
        //    try
        //    {
        //        using (SqlConnection sqlConn = new SqlConnection(conn))
        //        {
        //            sqlConn.Open();
        //            using (SqlCommand command = new SqlCommand("dbo.spcGetNewCustomersByDateRange", sqlConn))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Clear();
        //                command.Parameters.AddWithValue("@RegistrationDateStart", dateFrom);
        //                command.Parameters.AddWithValue("@RegistrationDateEnd", dateTo);
        //                using (SqlDataReader dr = command.ExecuteReader())
        //                {
        //                    while (dr.Read())
        //                    {
        //                        GTNewCustomersExport gnc = new GTNewCustomersExport();
        //                        gnc.CustomerCode = (dr["Customer Code"] == DBNull.Value) ? "" : dr["Customer Code"].ToString();
        //                        gnc.CustomerAlpha = (dr["Customer Alpha"] == DBNull.Value) ? "" : dr["Customer Alpha"].ToString();
        //                        gnc.CustomerName = (dr["Customer Name"] == DBNull.Value) ? "" : dr["Customer Name"].ToString();
        //                        gnc.Branch = (dr["Branch"] == DBNull.Value) ? "" : dr["Branch"].ToString();
        //                        gnc.AddressLine1 = (dr["Address Line 1"] == DBNull.Value) ? "" : dr["Address Line 1"].ToString();
        //                        gnc.AddressLine2 = (dr["Address Line 2"] == DBNull.Value) ? "" : dr["Address Line 2"].ToString();
        //                        gnc.AddressLine3 = (dr["Address Line 3"] == DBNull.Value) ? "" : dr["Address Line 3"].ToString();
        //                        gnc.Postcode = (dr["Postcode"] == DBNull.Value) ? "" : dr["Postcode"].ToString();
        //                        gnc.Suburb = (dr["Suburb"] == DBNull.Value) ? "" : dr["Suburb"].ToString();
        //                        gnc.State = (dr["State"] == DBNull.Value) ? "" : dr["State"].ToString();
        //                        gnc.Phone = (dr["Phone"] == DBNull.Value) ? "" : dr["Phone"].ToString();
        //                        gnc.Mobile = (dr["Mobile"] == DBNull.Value) ? "" : dr["Mobile"].ToString();
        //                        gnc.Fax = (dr["Fax"] == DBNull.Value) ? "" : dr["Fax"].ToString();
        //                        gnc.Contact = (dr["Contact"] == DBNull.Value) ? "" : dr["Contact"].ToString();
        //                        gnc.EmailAddress = (dr["Email Address"] == DBNull.Value) ? "" : dr["Email Address"].ToString();
        //                        gnc.PaymentTerms = (dr["Payment Terms"] == DBNull.Value) ? "" : dr["Payment Terms"].ToString();
        //                        gnc.Calendar = (dr["Calendar"] == DBNull.Value) ? "" : dr["Calendar"].ToString();
        //                        gnc.SalesPerson = (dr["Sales Person"] == DBNull.Value) ? "" : dr["Sales Person"].ToString();
        //                        gnc.OrderNumberRequired = (dr["Order Number Required"] == DBNull.Value) ? "" : dr["Order Number Required"].ToString();
        //                        gnc.Currency = (dr["Currency"] == DBNull.Value) ? "" : dr["Currency"].ToString();
        //                        gnc.TaxCode = (dr["Tax Code"] == DBNull.Value) ? "" : dr["Tax Code"].ToString();
        //                        gnc.InvoiceDeliveryMethod = (dr["Invoice Delivery Method"] == DBNull.Value) ? "" : dr["Invoice Delivery Method"].ToString();
        //                        gnc.InvoiceRecipient = (dr["Invoice Recipient"] == DBNull.Value) ? "" : dr["Invoice Recipient"].ToString();
        //                        gnc.StatementDeliveryMethod = (dr["Statement Delivery Method"] == DBNull.Value) ? "" : dr["Statement Delivery Method"].ToString();
        //                        gnc.StatementRecipient = (dr["Statement Recipient"] == DBNull.Value) ? "" : dr["Statement Recipient"].ToString();
        //                        gnc.ReceiptType = (dr["Receipt Type"] == DBNull.Value) ? "" : dr["Receipt Type"].ToString();
        //                        gnc.Notes = (dr["Notes"] == DBNull.Value) ? "" : dr["Notes"].ToString();
        //                        gnc.LegalEntityName = (dr["Legal Entity Name"] == DBNull.Value) ? "" : dr["Legal Entity Name"].ToString();
        //                        gnc.NECAMemberNumber = (dr["NECA Member Number"] == DBNull.Value) ? "" : dr["NECA Member Number"].ToString();
        //                        gnc.ABN = (dr["ABN"] == DBNull.Value) ? "" : dr["ABN"].ToString();

        //                        lstGTNewCustomers.Add(gnc);
        //                    }
        //                }
        //            }
        //            sqlConn.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return lstGTNewCustomers;
        //}

        //public string CreateInvoicesCSV(string type, List<GTInvoiceExport> lstGTInvoice, string LoggedBy)
        //{
        //    string strPath = GenerateGTCSVFileName(type, LoggedBy);
        //    try
        //    {
        //        using (var writer = new StreamWriter(strPath))
        //        using (var csv = new CsvWriter(writer))
        //        {
        //            csv.WriteRecords(lstGTInvoice);
        //            writer.Flush();
        //            strPath = Fn.ToWebPath(Fn.ToWebURL(strPath));
        //            // Add Log
        //            LogController.Add_Log("GT INVOICE EXPORT", "0", "Created GT Invoice CSV from the admin page", LoggedBy);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strPath = "";
        //    }
        //    return strPath;
        //}
        //public string CreateCreditNotesCSV(string type, List<GTCreditNoteExport> lstGTCreditNote, string LoggedBy)
        //{
        //    string strPath = GenerateGTCSVFileName(type, LoggedBy);
        //    try
        //    {
        //        using (var writer = new StreamWriter(strPath))
        //        using (var csv = new CsvWriter(writer))
        //        {
        //            csv.WriteRecords(lstGTCreditNote);
        //            writer.Flush();
        //            strPath = Fn.ToWebPath(Fn.ToWebURL(strPath));
        //            // Add Log
        //            LogController.Add_Log("GT CREDIT NOTE EXPORT", "0", "Created GT Credit Note CSV from the admin page", LoggedBy);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strPath = "";
        //    }
        //    return strPath;
        //}
        //public string CreateReceiptsCSV(string type, List<GTReceiptExport> lstGTReceipt, string LoggedBy)
        //{
        //    string strPath = GenerateGTCSVFileName(type, LoggedBy);
        //    try
        //    {
        //        using (var writer = new StreamWriter(strPath))
        //        using (var csv = new CsvWriter(writer))
        //        {
        //            csv.WriteRecords(lstGTReceipt);
        //            writer.Flush();
        //            strPath = Fn.ToWebPath(Fn.ToWebURL(strPath));
        //            // Add Log
        //            LogController.Add_Log("GT RECEIPT EXPORT", "0", "Created GT Receipt CSV from the admin page", LoggedBy);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strPath = "";
        //    }
        //    return strPath;
        //}
        //public string CreateNewCustomersCSV(string type, List<GTNewCustomersExport> lstGTNewCustomers, string LoggedBy)
        //{
        //    string strPath = GenerateGTCSVFileName(type, LoggedBy);
        //    try
        //    {
        //        using (var writer = new StreamWriter(strPath))
        //        using (var csv = new CsvWriter(writer))
        //        {
        //            csv.WriteRecords(lstGTNewCustomers);
        //            writer.Flush();
        //            strPath = Fn.ToWebPath(Fn.ToWebURL(strPath));
        //            // Add Log
        //            LogController.Add_Log("GT NEW CUSTOMER EXPORT", "0", "Created GT New Customer CSV from the admin page", LoggedBy);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strPath = "";
        //    }
        //    return strPath;
        //}
        //public string GenerateGTCSVFileName(string type, string LoggedBy)
        //{
        //    string strPath = "";
        //    if (Directory.Exists(Utilities.GetApplicationPath() + "temp"))
        //    {
        //        // Step 0: Create Greentree folder in temp
        //        string root = Path.Combine(Utilities.GetApplicationPath() + "temp", "Greentree");
        //        if (Directory.Exists(root) == false)
        //        {
        //            Directory.CreateDirectory(root);
        //        }
        //        // Step 1: Prepare user folder in temp.
        //        string rootUser = Path.Combine(root, LoggedBy);
        //        if (Directory.Exists(rootUser) == false)
        //        {
        //            Directory.CreateDirectory(rootUser);
        //        }
        //        // Step 2: delete all folders and files under the user first to avoid building up files.
        //        string[] subdirectoryEntries = Directory.GetDirectories(rootUser);
        //        foreach (string subdirectory in subdirectoryEntries)
        //        {
        //            Directory.Delete(subdirectory, true);
        //        }
        //        // Step 3. Create datetime folder under the user folder.
        //        string saveFolder = Path.Combine(rootUser, DateTime.Now.ToString("yyyyMMddhhmmss"));
        //        if (Directory.Exists(saveFolder) == false)
        //        {
        //            Directory.CreateDirectory(saveFolder);
        //        }
        //        // Step 4. Use this folder to save CSV file.
        //        strPath = saveFolder + "\\" + type + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";
        //    }
        //    return strPath;
        //}
        //public string GenerateCSVFileName(string reportName, string LoggedBy)
        //{
        //    string strPath = "";
        //    if (Directory.Exists(Utilities.GetApplicationPath() + "temp"))
        //    {
        //        // Step 0: Create Greentree folder in temp
        //        string root = Path.Combine(Utilities.GetApplicationPath() + "temp", "Report");
        //        if (Directory.Exists(root) == false)
        //        {
        //            Directory.CreateDirectory(root);
        //        }
        //        // Step 1: Prepare user folder in temp.
        //        string rootUser = Path.Combine(root, LoggedBy);
        //        if (Directory.Exists(rootUser) == false)
        //        {
        //            Directory.CreateDirectory(rootUser);
        //        }
        //        // Step 2: delete all folders and files under the user first to avoid building up files.
        //        string[] subdirectoryEntries = Directory.GetDirectories(rootUser);
        //        foreach (string subdirectory in subdirectoryEntries)
        //        {
        //            Directory.Delete(subdirectory, true);
        //        }
        //        // Step 3. Create datetime folder under the user folder.
        //        string saveFolder = Path.Combine(rootUser, DateTime.Now.ToString("yyyyMMddhhmmss"));
        //        if (Directory.Exists(saveFolder) == false)
        //        {
        //            Directory.CreateDirectory(saveFolder);
        //        }
        //        // Step 4. Use this folder to save CSV file.
        //        strPath = saveFolder + "\\" + reportName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";
        //    }
        //    return strPath;
        //}

        //#region Reporting

        public List<Reporting_Report> GetReports(string reportName)
        {
           List<Reporting_Report> list = new List<Reporting_Report>();

           try
           {
               using (SqlConnection sqlConn = new SqlConnection(conn))
               {
                   sqlConn.Open();
                   string sql = "SELECT * FROM reporting.Reports WHERE Active = 1 ";
                   sql += (!string.IsNullOrEmpty(reportName) ? $"AND Report_Name LIKE '%{reportName}%'" : "");

                   using (SqlCommand command = new SqlCommand(sql, sqlConn))
                   {
                       using (SqlDataReader dr = command.ExecuteReader())
                       {
                           while (dr.Read())
                           {
                               Reporting_Report r = new Reporting_Report();
                               r.Id = (dr["Id"] == DBNull.Value) ? 0 : int.Parse(dr["Id"].ToString());
                               r.Report_Name = (dr["Report_Name"] == DBNull.Value) ? "" : (dr["Report_Name"].ToString());
                               r.User_Id = (dr["User_Id"] == DBNull.Value) ? 0 : int.Parse(dr["User_Id"].ToString());
                               r.Created_Date = (dr["Created_Date"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr["Created_Date"].ToString());
                               r.Active = (dr["Active"] == DBNull.Value) ? false : bool.Parse(dr["Active"].ToString());
                               list.Add(r);
                           }
                       }
                   }
                   sqlConn.Close();
               }
           }
           catch (Exception ex)
           {
               Log2File l = new Log2File();
               l.WriteLog($"GetReports error: {reportName}");
               l.WriteLog(ex.Message);
               l.WriteLog(ex.StackTrace);
           }

           return list;
        }

        public int AddEmptyReport(int userId)
        {
            int reportId = 0;

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(conn))
                {
                    sqlConn.Open();
                    string sql = "INSERT INTO reporting.Reports (Report_Name, User_Id, Created_Date, Active) ";
                    sql += " OUTPUT INSERTED.ID VALUES (NULL, @userId, GETDATE(), 0) ";

                    using (SqlCommand command = new SqlCommand(sql, sqlConn))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@userId", userId);

                        reportId = (int)command.ExecuteScalar();
                    }

                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Log2File l = new Log2File();
                //l.WriteLog($"AddEmptyReport error: {userId.ToString()}");
                //l.WriteLog(ex.Message);
                //l.WriteLog(ex.StackTrace);

                logger.Error(ex);
            }

            return reportId;
        }

        public bool UpdateReport(int id, string reportName, bool active)
        {
            bool ret = false;

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(conn))
                {
                    sqlConn.Open();
                    string sql = "UPDATE reporting.Reports SET Report_Name = @reportName, Active = @active WHERE Id = @id ";

                    using (SqlCommand command = new SqlCommand(sql, sqlConn))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@reportName", reportName);
                        command.Parameters.AddWithValue("@active", active);

                        command.ExecuteNonQuery();

                        ret = true;
                    }

                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                Log2File l = new Log2File();
                l.WriteLog($"UpdateReport error: {id.ToString()} | {reportName} | {active.ToString()}");
                l.WriteLog(ex.Message);
                l.WriteLog(ex.StackTrace);
            }

            return ret;
        }

        public List<Reporting_Category> GetFieldsCategory()
        {
           List<Reporting_Category> list = new List<Reporting_Category>();

           try
           {
               using (SqlConnection sqlConn = new SqlConnection(conn))
               {
                   sqlConn.Open();
                   string sql = "SELECT * FROM reporting.Categories WHERE Active = 1 ";

                   using (SqlCommand command = new SqlCommand(sql, sqlConn))
                   {
                       using (SqlDataReader dr = command.ExecuteReader())
                       {
                           while (dr.Read())
                           {
                               Reporting_Category r = new Reporting_Category();
                               r.Id = (dr["Id"] == DBNull.Value) ? 0 : int.Parse(dr["Id"].ToString());
                               r.Table_Name = (dr["Table_Name"] == DBNull.Value) ? "" : (dr["Table_Name"].ToString());
                               r.Display_Name = (dr["Display_Name"] == DBNull.Value) ? "" : (dr["Display_Name"].ToString());
                               r.Created_Date = (dr["Created_Date"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr["Created_Date"].ToString());
                               r.Active = (dr["Active"] == DBNull.Value) ? false : bool.Parse(dr["Active"].ToString());
                               list.Add(r);
                           }
                       }
                   }
                   sqlConn.Close();
               }
           }
           catch (Exception ex)
           {
               Log2File l = new Log2File();
               l.WriteLog($"GetFieldsCategory error: ");
               l.WriteLog(ex.Message);
               l.WriteLog(ex.StackTrace);
           }

           return list;
        }

        public List<Reporting_Fields> GetFieldsByCategory(int categoryId)
        {
           List<Reporting_Fields> list = new List<Reporting_Fields>();

           try
           {
               using (SqlConnection sqlConn = new SqlConnection(conn))
               {
                   sqlConn.Open();
                   string sql = "SELECT * FROM reporting.Fields WHERE Active = 1 and Category_Id = @categoryId";

                   using (SqlCommand command = new SqlCommand(sql, sqlConn))
                   {
                       command.Parameters.Clear();
                       command.Parameters.AddWithValue("@categoryId", categoryId);

                       using (SqlDataReader dr = command.ExecuteReader())
                       {
                           while (dr.Read())
                           {
                               Reporting_Fields r = new Reporting_Fields();
                               r.Id = (dr["Id"] == DBNull.Value) ? 0 : int.Parse(dr["Id"].ToString());
                               r.Category_Id = (dr["Category_Id"] == DBNull.Value) ? 0 : int.Parse(dr["Category_Id"].ToString());
                               r.SQL_Field_Name = (dr["SQL_Field_Name"] == DBNull.Value) ? "" : (dr["SQL_Field_Name"].ToString());
                               r.Display_Name = (dr["Display_Name"] == DBNull.Value) ? "" : (dr["Display_Name"].ToString());
                               r.Field_Type_Id = (dr["Field_Type_Id"] == DBNull.Value) ? 0 : int.Parse(dr["Field_Type_Id"].ToString());
                               r.Created_Date = (dr["Created_Date"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr["Created_Date"].ToString());
                               r.Active = (dr["Active"] == DBNull.Value) ? false : bool.Parse(dr["Active"].ToString());
                               list.Add(r);
                           }
                       }
                   }
                   sqlConn.Close();
               }
           }
           catch (Exception ex)
           {
               Log2File l = new Log2File();
               l.WriteLog($"GetFieldsByCategory error: {categoryId.ToString()}");
               l.WriteLog(ex.Message);
               l.WriteLog(ex.StackTrace);
           }

           return list;
        }

        public List<Reporting_Report_Fields> GetReportFieldsByReportId(int reportId)
        {
           List<Reporting_Report_Fields> list = new List<Reporting_Report_Fields>();

           try
           {
               using (SqlConnection sqlConn = new SqlConnection(conn))
               {
                   sqlConn.Open();
                   string sql = @"SELECT rf.*, f.Field_Type_Id, f.Display_Name AS 'Display_Name_Base'
                                   FROM reporting.Report_Fields rf
                                   JOIN reporting.Fields f on f.Id = rf.Field_Id
                                   WHERE Report_Id = @reportId
                                   ORDER BY Sort";

                   using (SqlCommand command = new SqlCommand(sql, sqlConn))
                   {
                       command.Parameters.Clear();
                       command.Parameters.AddWithValue("@reportId", reportId);

                       using (SqlDataReader dr = command.ExecuteReader())
                       {
                           while (dr.Read())
                           {
                               Reporting_Report_Fields r = new Reporting_Report_Fields();
                               r.Id = (dr["Id"] == DBNull.Value) ? 0 : int.Parse(dr["Id"].ToString());
                               r.Report_Id = (dr["Report_Id"] == DBNull.Value) ? 0 : int.Parse(dr["Report_Id"].ToString());
                               r.Field_Id = (dr["Field_Id"] == DBNull.Value) ? 0 : int.Parse(dr["Field_Id"].ToString());
                               r.Sort = (dr["Sort"] == DBNull.Value) ? 0 : int.Parse(dr["Sort"].ToString());
                               r.Display_Name = (dr["Display_Name"] == DBNull.Value) ? "" : (dr["Display_Name"].ToString());
                               r.Field = new Reporting_Fields();
                               r.Field.Id = (dr["Field_Id"] == DBNull.Value) ? 0 : int.Parse(dr["Field_Id"].ToString());
                               r.Field.Display_Name = (dr["Display_Name_Base"] == DBNull.Value) ? "" : (dr["Display_Name_Base"].ToString());
                               r.Field.Field_Type_Id = (dr["Field_Type_Id"] == DBNull.Value) ? 0 : int.Parse(dr["Field_Type_Id"].ToString());
                               list.Add(r);
                           }
                       }
                   }
                   sqlConn.Close();
               }
           }
           catch (Exception ex)
           {
               Log2File l = new Log2File();
               l.WriteLog($"GetReportFieldsByReportId error: {reportId.ToString()}");
               l.WriteLog(ex.Message);
               l.WriteLog(ex.StackTrace);
           }

           return list;
        }

        public bool AddReportFields(int reportId, List<int> fieldList)
        {
           bool bOk = false;
           try
           {
               //if (DeleteReportFields(reportId))
               //{
               int sort = 1;
               foreach (var field in fieldList)
               {
                   AddReportFields(reportId, field, sort++);
               }
               //}
           }
           catch (Exception ex)
           {
                logger.Error(ex);
           }

           return bOk;
        }

        public bool AddReportFields(int reportId, int fieldId, int sort)
        {
           bool bOk = false;

           try
           {
               using (SqlConnection sqlConn = new SqlConnection(conn))
               {
                   sqlConn.Open();
                   string sql = @"IF NOT EXISTS(SELECT 1 FROM reporting.Report_Fields WHERE Report_Id = @reportId AND Field_Id = @fieldId)
                                   BEGIN
                                       INSERT INTO reporting.Report_Fields (Report_Id, Field_Id, Sort, Display_Name)
                                       VALUES (@reportId,
                                               @fieldId,
                                               (SELECT ISNULL(COUNT(*), 0) + 1 FROM reporting.Report_Fields WHERE Report_Id = @reportId ),
                                               (SELECT Display_Name FROM reporting.Fields f WHERE f.Id = @fieldId))
                                   END";

                   using (SqlCommand command = new SqlCommand(sql, sqlConn))
                   {
                       command.Parameters.Clear();
                       command.Parameters.AddWithValue("@reportId", reportId);
                       command.Parameters.AddWithValue("@fieldId", fieldId);
                       command.Parameters.AddWithValue("@sort", sort);
                       command.ExecuteNonQuery();

                       bOk = true;
                   }

                   sqlConn.Close();
               }
           }
           catch (Exception ex)
           {
               Log2File l = new Log2File();
               l.WriteLog($"AddReportFields error: {reportId.ToString()} | {fieldId.ToString()} | {sort.ToString()}");
               l.WriteLog(ex.Message);
               l.WriteLog(ex.StackTrace);
           }

           return bOk;
        }

        public bool DeleteReportFields(int reportId)
        {
            bool bOk = false;

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(conn))
                {
                    sqlConn.Open();
                    string sql = "DELETE FROM reporting.Report_Fields WHERE Report_Id = @reportId ";

                    using (SqlCommand command = new SqlCommand(sql, sqlConn))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@reportId", reportId);
                        command.ExecuteNonQuery();

                        bOk = true;
                    }

                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return bOk;
        }

        public List<Reporting_Report_Filter> GetReportFiltersByReportId(int reportId)
        {
           List<Reporting_Report_Filter> list = new List<Reporting_Report_Filter>();

           try
           {
               using (SqlConnection sqlConn = new SqlConnection(conn))
               {
                   sqlConn.Open();
                   string sql = "SELECT * FROM reporting.Report_Filters rf LEFT JOIN reporting.Fields f on f.Id = rf.Field_Id  WHERE Report_Id = @reportId ORDER BY Filter_Group, Filter_Order, rf.id";

                   using (SqlCommand command = new SqlCommand(sql, sqlConn))
                   {
                       command.Parameters.Clear();
                       command.Parameters.AddWithValue("@reportId", reportId);

                       using (SqlDataReader dr = command.ExecuteReader())
                       {
                           while (dr.Read())
                           {
                               Reporting_Report_Filter r = new Reporting_Report_Filter();
                               r.Id = (dr["Id"] == DBNull.Value) ? 0 : int.Parse(dr["Id"].ToString());
                               r.Report_Id = (dr["Report_Id"] == DBNull.Value) ? 0 : int.Parse(dr["Report_Id"].ToString());
                               r.Field_Id = (dr["Field_Id"] == DBNull.Value) ? 0 : int.Parse(dr["Field_Id"].ToString());
                               r.Filter_Group = (dr["Filter_Group"] == DBNull.Value) ? 0 : int.Parse(dr["Filter_Group"].ToString());
                               r.Filter_Order = (dr["Filter_Order"] == DBNull.Value) ? 0 : int.Parse(dr["Filter_Order"].ToString());
                               r.Comparator_Id = (dr["Comparator_Id"] == DBNull.Value) ? 0 : int.Parse(dr["Comparator_Id"].ToString());
                               r.Value_1 = (dr["Value_1"] == DBNull.Value) ? "" : (dr["Value_1"].ToString());
                               r.Value_2 = (dr["Value_2"] == DBNull.Value) ? "" : (dr["Value_2"].ToString());
                               r.Conditional = (dr["Conditional"] == DBNull.Value) ? "" : (dr["Conditional"].ToString());

                               r.Field = new Reporting_Fields();
                               r.Field.Id = (dr["Field_Id"] == DBNull.Value) ? 0 : int.Parse(dr["Field_Id"].ToString());
                               r.Field.Display_Name = (dr["Display_Name"] == DBNull.Value) ? "" : (dr["Display_Name"].ToString());
                               r.Field.Field_Type_Id = (dr["Field_Type_Id"] == DBNull.Value) ? 0 : int.Parse(dr["Field_Type_Id"].ToString());
                               list.Add(r);
                           }
                       }
                   }
                   sqlConn.Close();
               }
           }
           catch (Exception ex)
           {
               Log2File l = new Log2File();
               l.WriteLog($"GetReportFiltersByReportId error: {reportId.ToString()}");
               l.WriteLog(ex.Message);
               l.WriteLog(ex.StackTrace);
           }

           return list;
        }

        public bool AddEmptyReportFilter(int reportId)
        {
           bool bOk = false;

           try
           {
               using (SqlConnection sqlConn = new SqlConnection(conn))
               {
                   sqlConn.Open();
                   string sql = @"INSERT INTO reporting.Report_Filters (Report_Id, Filter_Order, Filter_Group)
                               VALUES (
                                           @reportId,
                                           (
                                               SELECT ISNULL(MAX(Filter_Order), 0) + 1
                                               FROM reporting.Report_Filters
                                               WHERE Report_Id = @reportId
                                           ),
                                           (
                                               SELECT ISNULL(MAX(Filter_Group), 1)
                                               FROM reporting.Report_Filters
                                               WHERE Report_Id = @reportId
                                           )
                                       ) ";

                   using (SqlCommand command = new SqlCommand(sql, sqlConn))
                   {
                       command.Parameters.Clear();
                       command.Parameters.AddWithValue("@reportId", reportId);
                       command.ExecuteNonQuery();
                       bOk = true;
                   }

                   sqlConn.Close();
               }
           }
           catch (Exception ex)
           {
               Log2File l = new Log2File();
               l.WriteLog($"AddEmptyReportFilter error: {reportId.ToString()}");
               l.WriteLog(ex.Message);
               l.WriteLog(ex.StackTrace);
           }

           return bOk;
        }

        public List<Reporting_Comparator> GetComparators(int fieldId = 0)
        {
           List<Reporting_Comparator> list = new List<Reporting_Comparator>();

           try
           {
               using (SqlConnection sqlConn = new SqlConnection(conn))
               {
                   sqlConn.Open();
                   string sql = "SELECT c.* FROM reporting.Comparators c ";
                   if (fieldId > 0)
                   {
                       sql += $"JOIN reporting.Field_Type_Comparator ftc on ftc.Comparator_Id = c.Id ";
                       sql += $"JOIN reporting.Fields f on f.Field_Type_Id = ftc.Field_Type_Id and f.Id = {fieldId} ";
                   }

                   sql += "WHERE c.Active = 1 ";

                   using (SqlCommand command = new SqlCommand(sql, sqlConn))
                   {
                       using (SqlDataReader dr = command.ExecuteReader())
                       {
                           while (dr.Read())
                           {
                               Reporting_Comparator r = new Reporting_Comparator();
                               r.Id = (dr["Id"] == DBNull.Value) ? 0 : int.Parse(dr["Id"].ToString());
                               r.SQL_Value = (dr["SQL_Value"] == DBNull.Value) ? "" : (dr["SQL_Value"].ToString());
                               r.Display_Value = (dr["Display_Value"] == DBNull.Value) ? "" : (dr["Display_Value"].ToString());
                               r.Created_Date = (dr["Created_Date"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr["Created_Date"].ToString());
                               r.Active = (dr["Active"] == DBNull.Value) ? false : bool.Parse(dr["Active"].ToString());
                               r.Field_Types = GetComparatorsFieldTypes(r.Id);
                               list.Add(r);
                           }
                       }
                   }
                   sqlConn.Close();
               }
           }
           catch (Exception ex)
           {
               //Log2File l = new Log2File();
               //l.WriteLog($"GetComparators error: {fieldId.ToString()}");
               //l.WriteLog(ex.Message);
               //l.WriteLog(ex.StackTrace);
                logger.Error(ex);
           }

           return list;
        }

        public List<int> GetComparatorsFieldTypes(int comparatirId)
        {
            List<int> list = new List<int>();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(conn))
                {
                    sqlConn.Open();
                    string sql = "SELECT * FROM reporting.Field_Type_Comparator WHERE Comparator_Id = @comparatirId";

                    using (SqlCommand command = new SqlCommand(sql, sqlConn))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@comparatirId", comparatirId);

                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add((dr["Field_Type_Id"] == DBNull.Value) ? 0 : int.Parse(dr["Field_Type_Id"].ToString()));
                            }
                        }
                    }
                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Log2File l = new Log2File();
                //l.WriteLog($"GetComparatorsFieldTypes error: {comparatirId.ToString()}");
                //l.WriteLog(ex.Message);
                //l.WriteLog(ex.StackTrace);
                logger.Error(ex);
            }

            return list;
        }

        public bool UpdateReportFilter(int id, int fieldId, int comparatorId, string value1, string value2, int filterOrder, int filterGroup, string conditional)
        {
            bool ret = false;

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(conn))
                {
                    sqlConn.Open();
                    string sql = @"UPDATE reporting.Report_Filters
                                    SET Field_Id = @fieldId,
                                        Comparator_Id = @comparatorId,
                                        Value_1 = @value1,
                                        Value_2 = @value2,
                                        Filter_Order = @filterOrder,
                                        Filter_Group = @filterGroup,
                                        Conditional = @conditional

                                    WHERE Id = @id ";

                    using (SqlCommand command = new SqlCommand(sql, sqlConn))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@fieldId", fieldId);
                        command.Parameters.AddWithValue("@comparatorId", comparatorId);
                        command.Parameters.AddWithValue("@value1", value1);
                        command.Parameters.AddWithValue("@value2", value2);
                        command.Parameters.AddWithValue("@filterOrder", filterOrder);
                        command.Parameters.AddWithValue("@filterGroup", filterGroup);
                        command.Parameters.AddWithValue("@conditional", conditional);

                        command.ExecuteNonQuery();

                        ret = true;
                    }

                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                Log2File l = new Log2File();
                l.WriteLog($"UpdateReportFilter error: {id.ToString()} | {fieldId.ToString()} | {comparatorId.ToString()} | {value1} | {value2}");
                l.WriteLog(ex.Message);
                l.WriteLog(ex.StackTrace);
            }

            return ret;
        }

        public bool DeleteReportFilter(int id)
        {
            bool bOk = false;

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(conn))
                {
                    sqlConn.Open();
                    string sql = "DELETE FROM reporting.Report_Filters WHERE Id = @id ";

                    using (SqlCommand command = new SqlCommand(sql, sqlConn))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();

                        bOk = true;
                    }

                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                Log2File l = new Log2File();
                l.WriteLog($"DeleteReportFilter error: {id.ToString()}");
                l.WriteLog(ex.Message);
                l.WriteLog(ex.StackTrace);
            }

            return bOk;
        }

        public DataTable GetReportResults(int reportId)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(conn))
                {
                    sqlConn.Open();

                    using (SqlCommand command = new SqlCommand("[reporting].[SP_Generate_Dynamic_Report]", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@REPORT_ID", reportId);

                        using (SqlDataAdapter sda = new SqlDataAdapter(command))
                        {
                            sda.Fill(dt);
                        }
                    }
                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                Log2File l = new Log2File();
                l.WriteLog($"GetReportResults error: {reportId.ToString()}");
                l.WriteLog(ex.Message);
                l.WriteLog(ex.StackTrace);
            }

            return dt;
        }

        public string CreateDynamicReport(int reportId)
        {
            // TODO: change username
            string strPath = GetReportFileName(GetReportById(reportId).Report_Name, "Administrator");
            try
            {
                var result = GetReportResults(reportId);
                using (var writer = new StreamWriter(strPath))
                using (var csv = new CsvWriter(writer))
                {
                    int columnCount = 0;
                    foreach (DataColumn col in result.Columns)
                    {
                        csv.WriteField(col.ColumnName);
                        columnCount++;
                    }

                    csv.NextRecord();

                    foreach (DataRow row in result.Rows)
                    {
                        for (int i = 0; i < columnCount; i++)
                        {
                            csv.WriteField(row[i].ToString());
                        }

                        csv.NextRecord();
                    }

                    writer.Flush();
                    strPath = Fn.ToWebPath(Fn.ToWebURL(strPath));
                    //    // Add Log
                    //    LogController.Add_Log("GT NEW CUSTOMER EXPORT", "0", "Created GT New Customer CSV from the admin page", LoggedBy);
                }
            }
            catch (Exception ex)
            {
                strPath = "";
                Log2File l = new Log2File();
                l.WriteLog($"CreateDynamicReport error: {reportId.ToString()}");
                l.WriteLog(ex.Message);
                l.WriteLog(ex.StackTrace);
            }

            return strPath;
        }

        public string GetReportFileName(string reportName, string LoggedBy)
        {
            string strPath = "";
            if (Directory.Exists(Utilities.GetApplicationPath() + "temp"))
            {
                // Step 0: Create Greentree folder in temp
                string root = Path.Combine(Utilities.GetApplicationPath() + "temp", "Reports");
                if (Directory.Exists(root) == false)
                {
                    Directory.CreateDirectory(root);
                }
                // Step 1: Prepare user folder in temp.
                string rootUser = Path.Combine(root, LoggedBy);
                if (Directory.Exists(rootUser) == false)
                {
                    Directory.CreateDirectory(rootUser);
                }
                // Step 2: delete all folders and files under the user first to avoid building up files.
                string[] subdirectoryEntries = Directory.GetDirectories(rootUser);
                foreach (string subdirectory in subdirectoryEntries)
                {
                    Directory.Delete(subdirectory, true);
                }
                // Step 3. Create datetime folder under the user folder.
                string saveFolder = Path.Combine(rootUser, DateTime.Now.ToString("yyyyMMddhhmmss"));
                if (Directory.Exists(saveFolder) == false)
                {
                    Directory.CreateDirectory(saveFolder);
                }
                // Step 4. Use this folder to save CSV file.
                strPath = saveFolder + "\\" + reportName + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".csv";
            }
            return strPath;
        }

        public Reporting_Report GetReportById(int reportId)
        {
            Reporting_Report rr = new Reporting_Report();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(conn))
                {
                    sqlConn.Open();
                    string sql = "SELECT * FROM reporting.Reports WHERE Id = @reportId ";

                    using (SqlCommand command = new SqlCommand(sql, sqlConn))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@reportId", reportId);
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rr.Id = (dr["Id"] == DBNull.Value) ? 0 : int.Parse(dr["Id"].ToString());
                                rr.Report_Name = (dr["Report_Name"] == DBNull.Value) ? "" : (dr["Report_Name"].ToString().Trim());
                                rr.User_Id = (dr["User_Id"] == DBNull.Value) ? 0 : int.Parse(dr["User_Id"].ToString());
                                rr.Created_Date = (dr["Created_Date"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr["Created_Date"].ToString());
                                rr.Active = (dr["Active"] == DBNull.Value) ? false : bool.Parse(dr["Active"].ToString());
                            }
                        }
                    }

                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                Log2File l = new Log2File();
                l.WriteLog($"GetReportById error: {reportId.ToString()}");
                l.WriteLog(ex.Message);
                l.WriteLog(ex.StackTrace);
            }

            return rr;
        }

        public bool DeleteReport(int id)
        {
           bool bOk = false;

           try
           {
               using (SqlConnection sqlConn = new SqlConnection(conn))
               {
                   sqlConn.Open();
                   string sql = "UPDATE reporting.Reports SET Active = 0 WHERE Id = @id ";

                   using (SqlCommand command = new SqlCommand(sql, sqlConn))
                   {
                       command.Parameters.Clear();
                       command.Parameters.AddWithValue("@id", id);
                       command.ExecuteNonQuery();

                       bOk = true;
                   }

                   sqlConn.Close();
               }
           }
           catch (Exception ex)
           {
               Log2File l = new Log2File();
               l.WriteLog($"DeleteReport error: {id.ToString()}");
               l.WriteLog(ex.Message);
               l.WriteLog(ex.StackTrace);
           }

           return bOk;
        }

        public bool DeleteReportField(int id)
        {
            bool bOk = false;

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(conn))
                {
                    sqlConn.Open();
                    string sql = "DELETE FROM reporting.Report_Fields WHERE Id = @id ";

                    using (SqlCommand command = new SqlCommand(sql, sqlConn))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();

                        bOk = true;
                    }

                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return bOk;
        }

        public bool MoveReportFieldSort(int id, int sortChange)
        {
            bool ret = false;

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(conn))
                {
                    sqlConn.Open();

                    using (SqlCommand command = new SqlCommand("[reporting].[SP_Move_Report_Field_Sort]", sqlConn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@ID", id);
                        command.Parameters.AddWithValue("@SORT_CHANGE", sortChange);

                        command.ExecuteNonQuery();

                        ret = true;
                    }

                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                Log2File l = new Log2File();
                l.WriteLog($"MoveReportFieldSort error: {id.ToString()} | {sortChange.ToString()} ");
                l.WriteLog(ex.Message);
                l.WriteLog(ex.StackTrace);
            }

            return ret;
        }

        public bool UpdateReportFieldTitle(int id, string title)
        {
            bool ret = false;

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(conn))
                {
                    sqlConn.Open();
                    string sql = @"UPDATE reporting.Report_Fields
                                    SET Display_Name = @title
                                    WHERE Id = @id ";

                    using (SqlCommand command = new SqlCommand(sql, sqlConn))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@title", title);

                        command.ExecuteNonQuery();

                        ret = true;
                    }

                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return ret;
        }

        public List<Reporting_Dropdown_Fields> GetReportDropdownFields()
        {
           List<Reporting_Dropdown_Fields> list = new List<Reporting_Dropdown_Fields>();

           try
           {
               using (SqlConnection sqlConn = new SqlConnection(conn))
               {
                   sqlConn.Open();
                   string sql = "SELECT * FROM reporting.[Dropdown_Fields] WHERE Active = 1 ";

                   using (SqlCommand command = new SqlCommand(sql, sqlConn))
                   {
                       command.Parameters.Clear();
                       using (SqlDataReader dr = command.ExecuteReader())
                       {
                           while (dr.Read())
                           {
                               Reporting_Dropdown_Fields rdf = new Reporting_Dropdown_Fields();

                               rdf.Field_Id = (dr["Field_Id"] == DBNull.Value) ? 0 : int.Parse(dr["Field_Id"].ToString());
                               rdf.Source_Table = (dr["Source_Table"] == DBNull.Value) ? "" : (dr["Source_Table"].ToString().Trim());
                               rdf.Value_Field = (dr["Value_Field"] == DBNull.Value) ? "" : (dr["Value_Field"].ToString().Trim());
                               rdf.Text_Field = (dr["Text_Field"] == DBNull.Value) ? "" : (dr["Text_Field"].ToString().Trim());
                               rdf.Created_Date = (dr["Created_Date"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr["Created_Date"].ToString());
                               rdf.Active = (dr["Active"] == DBNull.Value) ? false : bool.Parse(dr["Active"].ToString());

                               list.Add(rdf);
                           }
                       }
                   }

                   sqlConn.Close();
               }
           }
           catch (Exception ex)
           {
               Log2File l = new Log2File();
               l.WriteLog($"GetReportDropdownFields error:");
               l.WriteLog(ex.Message);
               l.WriteLog(ex.StackTrace);
           }

           return list;
        }

        public List<Tuple<string, string>> GetReportDropdownFieldsList(Reporting_Dropdown_Fields rdf)
        {
            List<Tuple<string, string>> list = new List<Tuple<string, string>>();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(conn))
                {
                    sqlConn.Open();
                    string sql = $@"SELECT {rdf.Value_Field} as 'Id', {rdf.Text_Field} as 'Text' FROM {rdf.Source_Table}";

                    using (SqlCommand command = new SqlCommand(sql, sqlConn))
                    {
                        command.Parameters.Clear();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(new Tuple<string, string>(((dr["Id"] == DBNull.Value) ? "0" : dr["Id"].ToString()),
                                                                (dr["Text"] == DBNull.Value) ? "" : dr["Text"].ToString()));
                            }
                        }
                    }

                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                Log2File l = new Log2File();
                l.WriteLog($"GetReportDropdownFieldsList error:");
                l.WriteLog(ex.Message);
                l.WriteLog(ex.StackTrace);
            }

            return list;
        }

        public string GetFilterPreview(int reportId)
        {
           string ret = "";

           try
           {
               using (SqlConnection sqlConn = new SqlConnection(conn))
               {
                   sqlConn.Open();

                   using (SqlCommand command = new SqlCommand("reporting.[SP_Generate_Dynamic_Report_Filter]", sqlConn))
                   {
                       command.CommandType = CommandType.StoredProcedure;
                       command.Parameters.Clear();
                       command.Parameters.AddWithValue("@REPORT_ID", reportId);
                       command.Parameters.AddWithValue("@QUERY_TO_EXECUTE", 0);
                       using (SqlDataReader dr = command.ExecuteReader())
                       {
                           while (dr.Read())
                           {
                               ret = dr.GetString(0);
                           }
                       }
                   }

                   sqlConn.Close();
               }
           }
           catch (Exception ex)
           {
               Log2File l = new Log2File();
               l.WriteLog($"GetFilterPreview error: {reportId.ToString()}");
               l.WriteLog(ex.Message);
               l.WriteLog(ex.StackTrace);
               ret = "CLICK HERE TO REFRESH";
           }

           return ret;
        }

        //#endregion
    }
}