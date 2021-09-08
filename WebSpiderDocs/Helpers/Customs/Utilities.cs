using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using ReportBuilder.Models;
using ReportBuilder.Controllers;
using NLog;

//namespace CET.Utils {
	public class Utilities {
        static Logger logger = LogManager.GetCurrentClassLogger();

    //      public static string IntToAlpha(int x)
    //      {
    //          int lowChar;
    //          StringBuilder result = new StringBuilder();
    //          do
    //          {
    //              lowChar = (x - 1) % 26;
    //              x = (x - 1) / 26;
    //              result.Insert(0, (char)(lowChar + 65));
    //          } while (x > 0);
    //          return result.ToString();
    //      }

    //      /// <summary>
    //      /// "TaKeo    KInG  NiSHIoka" to "Takeo King Nishioka"
    //      /// </summary>
    //      /// <param name="first"></param>
    //      /// <param name="middle"></param>
    //      /// <param name="surname"></param>
    //      /// <returns>Well formated full name if you pass all otherwise return well formated name within you passed params</returns>
    //public static string ConbineName(string first = "", string middle = "", string surname = ""){
    //	//string fullName = "";

    //	var n = new string[]{first,middle,surname};

    //	// To Uppercase First letter
    //	n = n.ToList().Select(x =>{
    //		 return x == "" ? "" : (x.First().ToString().ToUpper() + String.Join("", x.ToString().ToLower().Skip(1)));
    //	}).ToArray();

    //	// replace multi spaces to one space
    //	return string.Join(" ", n).Replace("  "," ");
    //}

    //public static JToken ParseJson(HttpContext context)
    //{
    //          context.Request.InputStream.Position = 0;
    //          string rawJson;
    //          using (StreamReader reader = new StreamReader(context.Request.InputStream))
    //          {
    //              rawJson = reader.ReadToEnd();
    //          }

    //          return JToken.Parse(rawJson);
    //}

    //      /// <summary>
    //      /// c:dev/aaa/bbb/ccc.jpg to ccc.jpg
    //      /// </summary>
    //      /// <param name="fullpath"></param>
    //      /// <returns>filename</returns>
    //      public static string Path2Filename(string fullpath)
    //      {
    //          string filename =fullpath
    //                              .Replace("/+", "/")
    //                              .Replace("\\+", "\\")
    //                              .Split(new char[] { '/', '\\' }).LastOrDefault();

    //          return filename;
    //      }
    //      public static string Extention(string path)
    //      {
    //          return Path2Filename(path).Split('.').LastOrDefault();
    //      }
    //      // /// <summary>
    //      // /// aaa.jpg to aaa
    //      // /// </summary>
    //      // /// <param name="filename"></param>
    //      // /// <returns>filename without extension</returns>
    //      // public static string Filename2Name(string filename)
    //      // {
    //      //     var array = filename.Split(new char[] { '.' });

    //      //     return string.Join(".", array.Take(array.Count() - 1));
    //      // }
    //      // public static string ToWebPath(string path)
    //      // {
    //      //     return (new Regex(@"(/|\\)+")).Replace(path, "/");
    //      // }
    //      // public static string ToFileSystemPath(string path)
    //      // {
    //      //     return (new Regex(@"(/|\\)+")).Replace(path, "\\");
    //      // }

    //      public static bool MkDir(string FilePath)
    //      {
    //          if (!Directory.Exists(FilePath))
    //          {
    //              Directory.CreateDirectory(FilePath);
    //              return true;
    //          }
    //          return false;
    //      }

    //      /// <summary>
    //      /// Remove folder recurcy
    //      /// </summary>
    //      /// <param name="path">folder path</param>
    //      /// <param name="after">remove filder if folder is updated after this paramter day. will include this parameter days.the value should be absolute number</param>
    //      /// <returns></returns>
    //      public static void RmDir(string path,int after = 7)
    //      {
    //          string[] dirs = Directory.GetDirectories(path);
    //          foreach ( string dOrf in dirs) {
    //              FileAttributes attr = File.GetAttributes(dOrf);

    //              // When File
    //              if (!attr.HasFlag(FileAttributes.Directory)) continue;

    //              // When Folder
    //              DirectoryInfo d = new DirectoryInfo(dOrf);
    //              if (d.LastWriteTime < DateTime.Now.AddDays(after * -1))
    //              {
    //                  d.Delete(true);
    //                  continue;
    //              }
    //              // Find meeting requirement folder to under this folder.
    //              RmDir(dOrf,after);
    //          }
    //      }
    //      /*
    //      public static bool RmDir(string path)
    //      {
    //          if (Directory.Exists(path))
    //          {
    //              Directory.Delete(path,true);
    //              return true;
    //          }
    //          return false;
    //      }
    //      */

    //      public static string Guid()
    //      {
    //          return System.Guid.NewGuid().ToString().Substring(0, 7);
    //      }

    //      /// <summary>
    //      /// Join Address
    //      /// </summary>
    //      /// <param name="instance">instance</param>
    //      /// <param name="match">target columns</param>
    //      /// <returns>string</returns>
    //      public static string JoinAddress<T>(T instance, params string[] match  )
    //      {
    //          string address = string.Empty;
    //          List<string> addrs = new List<string>();

    //          if( match == null || match.Count() == 0) match = new string[]{
    //                                                      "AddressBuildingName"
    //                                                      ,"AddressFlatUnitDetails"
    //                                                      ,"AddressStreetNumber"
    //                                                      ,"AddressStreetName"
    //                                                      ,"AddressSuburb"
    //                                                      ,"AddressState"
    //                                                      ,"AddressPostcode"
    //                                                  };
    //          return string.Join(" ", match.ToList()
    //                                  .Select(x=>{
    //                                      return ((T)instance).GetType().GetProperty(x).GetValue(((T)instance), null).ToString();
    //                                  })
    //                                  .Where(x => {
    //                                      return !string.IsNullOrWhiteSpace(x);
    //                                  }));
    //      }

    /// <summary>
    /// return filesystem path for application root. end will includes '//'
    /// </summary>
    /// <returns></returns>
    public static string GetApplicationPath()
    {
        return AppDomain.CurrentDomain.BaseDirectory;
    }

    //      public static DateTime GetDateTimeFromString(string strDate)
    //      {
    //          DateTime dtDateTime = DateTime.MinValue;
    //          try
    //          {
    //              dtDateTime = DateTime.ParseExact(strDate,
    //                                    "yyyy-MM-dd",
    //                                     CultureInfo.InvariantCulture);

    //          }
    //	catch (Exception ex)
    //	{
    //		logger.Error(ex);

    //          }
    //          return dtDateTime;
    //      }
    //      public static DateTime? AddTimeToDateTime(string strTime, DateTime? dtOrigDate)
    //      {
    //          if (dtOrigDate != null && dtOrigDate != DateTime.MinValue)
    //          {
    //              string[] arrTime = strTime.Split(':');
    //              if (arrTime.Length == 2)
    //              {
    //                  int Hours = int.Parse(arrTime[0]);
    //                  if (Hours > 0)
    //                  {
    //                      dtOrigDate = ((DateTime)dtOrigDate).AddHours(Hours);
    //                  }
    //                  int Minutes = int.Parse(arrTime[1]);
    //                  if (Minutes > 0)
    //                  {
    //                      dtOrigDate = ((DateTime)dtOrigDate).AddMinutes(Minutes);
    //                  }
    //              }
    //          }
    //          return dtOrigDate;
    //      }
    //      public static SqlParameter NullableObject(string paramName, object value, SqlDbType sqlType)
    //      {
    //          if (value != null && string.IsNullOrEmpty(value.ToString()))
    //          {
    //              value = null;
    //          }
    //          SqlParameter sqlParam = new SqlParameter(paramName, value == null ? (object)DBNull.Value : value);
    //          sqlParam.IsNullable = true;
    //          sqlParam.Direction = ParameterDirection.Input;
    //          sqlParam.SqlDbType = sqlType;
    //          return sqlParam;
    //      }

    //  public static string FormatMobileNumber(string mobile)
    //  {
    //      string formatMobile = "";
    //      if (!string.IsNullOrEmpty(mobile))
    //      {
    //          // Format this way - 0400 100 200 to avoid CSV to recognize as number
    //          mobile = mobile.Replace(" ", "");
    //          if (mobile.Length == 10)
    //          {
    //              formatMobile += mobile.Substring(0, 4);
    //              formatMobile += " ";
    //              formatMobile += mobile.Substring(4, 3);
    //              formatMobile += " ";
    //              formatMobile += mobile.Substring(7, 3);
    //          }
    //          else
    //          {
    //              formatMobile = mobile;
    //          }
    //      }
    //      return formatMobile;
    //  }

    //  public static bool UpdateAllCensusDate()
    //  {
    //      using (ClassController cc = new ClassController())
    //      using (IntakeTrainingYearController ityc = new IntakeTrainingYearController())
    //      using (ComponentController compc = new ComponentController())
    //      {
    //          List<tblClass> clsList = cc.GetAll();
    //          foreach (tblClass cls in clsList)
    //          {
    //              if (cls.IntakeTrainingYearId > 0 && cls.ComponentId > 0)
    //              {
    //                  tblIntakeTrainingYear tblIty = ityc.GetIntakeTrainingYearInfo(cls.IntakeTrainingYearId);
    //                  tblComponent tblComp = ComponentController.GetComponent(cls.ComponentId);
    //                  string startDate = (cls.StartDateTimeUTC != null) ? ((DateTime)cls.StartDateTimeUTC).ToString("yyyy-MM-dd") : "";
    //                  string endDate = (cls.EndDateTimeUTC != null) ? ((DateTime)cls.EndDateTimeUTC).ToString("yyyy-MM-dd") : "";
    //                  string censusDate = GetCensusDate(tblIty.BlockRelease, Decimal.ToInt32(tblComp.NominalHours), startDate, endDate);
    //                  if (!string.IsNullOrEmpty(censusDate))
    //                  {
    //                      DateTime dtCensusDate = DateTime.Parse(censusDate);
    //                      cls.CensusDate = dtCensusDate;
    //                      cc.UpdateTblClassCensusDate(cls);
    //                  }
    //              }
    //          }

    //      }
    //      return true;
    //  }
    //  public static string GetCensusDate(bool isBlock, int intHours, string startDate, string endDate)
    //  {
    //      // IsBlock inicates whether this is a block release (-1) or day release (0)
    //      string censusDate = "";
    //      if (isBlock == true)
    //      {
    //          // Block Release
    //          censusDate = Census_Date_Block(intHours, startDate);
    //      }
    //      else
    //      {
    //          // Day Release
    //          censusDate = Census_Date_Day(startDate, endDate);
    //      }
    //      return censusDate;
    //  }
    //  private static string Census_Date_Block(int intHours, string startDate)
    //  {
    //      // BLOCK release
    //      // Calculate the census date based on hours - Block Release
    //      // inthours is the total number of delivery hours for the module
    //      // this is based on the number of deliverable hours (inthours)
    //      // at the very least it will be the next working day
    //      return end_date(startDate, hours_to_days(intHours * 0.2)); // uses 20% of the hours
    //  }

    //  private static string Census_Date_Day(string startDate, string endDate)
    //  {
    //      string censusDate = "";
    //      string sdt = startDate.Replace("-", "");
    //      string edt = endDate.Replace("-", "");
    //      // DAY RELEASE
    //      // Census date based on start and end date, calculate 20% of the number of CALENDAR days
    //      if (sdt == edt) // The start and the end date are the same
    //      {
    //          return startDate;
    //      }
    //      int Cal_days = Convert.ToInt32((Convert.ToDateTime(endDate) - Convert.ToDateTime(startDate)).TotalDays);
    //      Cal_days = Convert.ToInt32((Cal_days * 0.2)) - 1;
    //      if (Cal_days == 0)
    //      {
    //          Cal_days = 1;
    //      }
    //      // Now calculate the census date based on the working days or calendar days from now (comment out the non applicable one)
    //      censusDate = Convert.ToDateTime(startDate).AddDays(Cal_days).ToString("yyyy-MM-dd");

    //      return censusDate;
    //  }
    //  private static string end_date(string fdt, int NoDays)
    //  {
    //      string strEndDate = "";
    //      // MS 12/09/2016
    //      // This function calculates the end date given a number of days
    //      if (string.IsNullOrEmpty(fdt))
    //      {
    //          return "";
    //      }
    //      if (NoDays == 0)
    //      {
    //          return fdt;
    //      }

    //      using (DateController dc = new DateController())
    //      {
    //          List<tblWorkingDays> lst = dc.GetWorkingDays(fdt);
    //          int iIndex = 0;
    //          if (lst.Count == 0)
    //          {
    //              //TODO: Now calculate the census date based on the working days or calendar days from now (comment out the non applicable one)
    //              strEndDate = Convert.ToDateTime(fdt).AddDays(NoDays).ToString("yyyy-MM-dd");
    //          }
    //          else
    //          {
    //              foreach (tblWorkingDays wkd in lst)
    //              {
    //                  if (wkd.Workday == true)
    //                  {
    //                      iIndex++;
    //                      if (iIndex == NoDays)
    //                      {
    //                          if (wkd.Date != null)
    //                          {
    //                              strEndDate = ((DateTime)wkd.Date).ToString("yyyy-MM-dd");
    //                              break;
    //                          }
    //                      }
    //                  }
    //              }
    //          }
    //      }
    //      return strEndDate;
    //  }
    //  private static int hours_to_days(double dblHours)
    //  {
    //      // This function uses the hours calculated (20% of the total) and converts this to days
    //      // The calculated day(s) indicate the day of the census as follows :-
    //      // If calculated days = 1 then the census date is the start date + 1 (ie it can never be the start date)
    //      int hoursToDays = Convert.ToInt32((dblHours - 0.05) / 8) + 1;
    //      if (hoursToDays == 1)   // Can't be the start date
    //      {
    //          hoursToDays = 2;
    //      }
    //      return hoursToDays;
    //  }
    //  public static string ConvertToCurrencyFormat(double dAmount)
    //  {
    //      return ((decimal)dAmount).ToString("C2", CultureInfo.CreateSpecificCulture("en-AU"));
    //  }

    //  /// <summary>
    //  /// Remove combox attributes.
    //  /// </summary>
    //  /// <param name="criteria"></param>
    //  /// <returns></returns>
    //  static public SpiderDocsModule.SearchCriteria ExcludeCombobox(SpiderDocsModule.SearchCriteria criteria)
    //  {
    //      var ans = criteria.AttributeCriterias.Attributes.ToList();
    //      int idx = -1;
    //      // Remove combobox
    //      idx = ans.FindIndex(x => x.Values.id == (int)CET.Helpers.Factory.SDAttr.StudentID);
    //      if (idx >= 0)
    //          ans.RemoveAt(idx);

    //      idx = ans.FindIndex(x => x.Values.id == (int)CET.Helpers.Factory.SDAttr.Group);
    //      if (idx >= 0)
    //          ans.RemoveAt(idx);

    //      idx = ans.FindIndex(x => x.Values.id == (int)CET.Helpers.Factory.SDAttr.Unit);
    //      if (idx >= 0)
    //          ans.RemoveAt(idx);

    //      idx = ans.FindIndex(x => x.Values.id == (int)CET.Helpers.Factory.SDAttr.Course);
    //      if (idx >= 0)
    //          ans.RemoveAt(idx);

    //      idx = ans.FindIndex(x => x.Values.id == (int)CET.Helpers.Factory.SDAttr.CompanyName);
    //      if (idx >= 0)
    //          ans.RemoveAt(idx);

    //      criteria.AttributeCriterias.Attributes = ans;

    //      return criteria;
    //  }
}

public class Fn
    {
    //public static JToken ParseJson(HttpContext context)
    //{
    //    context.Request.InputStream.Position = 0;
    //    string rawJson;
    //    using (StreamReader reader = new StreamReader(context.Request.InputStream))
    //    {
    //        rawJson = reader.ReadToEnd();
    //    }

    //    return JToken.Parse(rawJson);
    //}

    ///// <summary>
    ///// c:dev/aaa/bbb/ccc.jpg to ccc.jpg
    ///// </summary>
    ///// <param name="fullpath"></param>
    ///// <returns>filename</returns>
    //public static string Path2Filename(string fullpath)
    //{
    //    string filename = fullpath
    //                        .Replace("/+", "/")
    //                        .Replace("\\+", "\\")
    //                        .Split(new char[] { '/', '\\' }).LastOrDefault();

    //    return filename;
    //}
    //public static string Extention(string path)
    //{
    //    return Path2Filename(path).Split('.').LastOrDefault();
    //}
    ///// <summary>
    ///// aaa.jpg to aaa
    ///// </summary>
    ///// <param name="filename"></param>
    ///// <returns>filename without extension</returns>
    //public static string Filename2Name(string filename)
    //{
    //    var array = filename.Split(new char[] { '.' });

    //    return string.Join(".", array.Take(array.Count() - 1));
    //}
    public static string ToWebPath(string path)
    {
        if (path.IndexOf(@"://") > -1)
        {

            string[] arr = Regex.Split(path, "://");
            return arr[0] + "://" + (new Regex(@"(/|\\)+")).Replace(arr[1], "/");
        }
        else
        {
            return (new Regex(@"(/|\\)+")).Replace(path, "/");
        }
    }
    //public static string ToFileSystemPath(string path)
    //{
    //    return (new Regex(@"(/|\\)+")).Replace(path, "\\");
    //}
    public static string ToWebURL(string physicalPath)
    {
        physicalPath = physicalPath.Replace(AppDomain.CurrentDomain.BaseDirectory, Fn.GetWebRoot());
        return physicalPath;
    }

    //public static bool MkDir(string FilePath)
    //{
    //    if (!Directory.Exists(FilePath))
    //    {
    //        Directory.CreateDirectory(FilePath);
    //        return true;
    //    }
    //    return false;
    //}

    ///// <summary>
    ///// Remove folder recurcy
    ///// </summary>
    ///// <param name="path">folder path</param>
    ///// <param name="after">remove filder if folder is updated after this paramter day. will include this parameter days.the value should be absolute number</param>
    ///// <returns></returns>
    //public static void RmDir(string path, int after = 7)
    //{
    //    string[] dirs = Directory.GetDirectories(path);
    //    foreach (string dOrf in dirs)
    //    {
    //        FileAttributes attr = File.GetAttributes(dOrf);

    //        // When File
    //        if (!attr.HasFlag(FileAttributes.Directory)) continue;

    //        // When Folder
    //        DirectoryInfo d = new DirectoryInfo(dOrf);
    //        if (d.LastWriteTime < DateTime.Now.AddDays(after * -1))
    //        {
    //            d.Delete(true);
    //            continue;
    //        }
    //        // Find meeting requirement folder to under this folder.
    //        RmDir(dOrf, after);
    //    }
    //}
    ///*
    //public static bool RmDir(string path)
    //{
    //    if (Directory.Exists(path))
    //    {
    //        Directory.Delete(path,true);
    //        return true;
    //    }
    //    return false;
    //}
    //*/

    //public static string Guid()
    //{
    //    return System.Guid.NewGuid().ToString().Substring(0, 7);
    //}
    //public static string GetFullQuery(SqlCommand command)
    //{
    //    string fullQuery = "";
    //    fullQuery = command.CommandText.ToString();
    //    foreach (SqlParameter p in command.Parameters)
    //    {
    //        fullQuery = fullQuery.Replace(p.ParameterName.ToString(), "'" + p.Value.ToString() + "'");
    //    }
    //    return fullQuery;
    //}

    //public static List<string> FilesByPath(string path)
    //{
    //    List<string> list = new List<string>();

    //    if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path)) return list;

    //    return Directory.GetFiles(path).ToList();
    //}

    public static string GetWebRoot()
    {
        return string.Format("{0}://{1}:{2}{3}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port, VirtualPathUtility.ToAbsolute("~/"));
    }

    //public static string Beautify(string text)
    //{
    //    if( string.IsNullOrEmpty(text)) return text;

    //    string beauful = string.Empty;
    //    foreach (string pieace in text.Split(' '))
    //        if (!string.IsNullOrWhiteSpace(pieace) && pieace.Length > 1)
    //            beauful += pieace.First().ToString().ToUpper() + pieace.Substring(1).ToLower() + " ";

    //    return beauful.Trim();
    //}

    //public static string[] EmailFromAccessFormat(string EmailAddress)
    //{
    //    string[] pureAddress = EmailAddress.Split('#').Select(m => m.Replace("mailto:", "")).Where(m => !string.IsNullOrWhiteSpace(m)).ToArray();

    //    return pureAddress;
    //}

    //public static string AddStr2Path(string path, string add)
    //{
    //    List<string> arr = path.Split('.').ToList();
    //    arr.Insert(arr.Count - 1,"-"+add);

    //    return string.Join(".",arr);
    //}

    //public static SpiderDocsModule.SearchCriteria ToSpiderDocsSearchCriteria(SpiderDocsModule.Document doc)
    //{
    //    // Here attribute
    //    var attrs = new SpiderDocsModule.AttributeCriteriaCollection();
    //    doc.Attrs.ForEach(attr => { attrs.Add(attr); });

    //    var criteria = new SpiderDocsModule.SearchCriteria()
    //    {
    //        FolderIds = new List<int> { doc.id_folder },
    //        DocTypeIds = new List<int> { doc.id_docType },
    //        Titles = new List<string> { doc.title, System.IO.Path.GetFileNameWithoutExtension(doc.title) },
    //        AttributeCriterias = attrs,
    //        ExcludeStatuses = new List<SpiderDocsModule.en_file_Status>() {
    //                    SpiderDocsModule.en_file_Status.deleted,
    //                    SpiderDocsModule.en_file_Status.archived
    //            }
    //    };

    //    return criteria;
    //}
}
//}