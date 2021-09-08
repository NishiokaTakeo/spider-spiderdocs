using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ReportBuilder.Utils
{
    public class Log2File
    {
        //String strPath = Utilities.GetApplicationPath() + ConfigurationManager.AppSettings["logDir"];
        String activated = ConfigurationManager.AppSettings["logActivated"];
        //string defRoot = HttpRuntime.AppDomainAppPath + "logs\\";
        string defRoot { get
                            {
                                try
                                {
                                    return HttpRuntime.AppDomainAppPath + "logs\\";
                                }
                                catch {}

                                return @"C:\Windows\temp\logs\";
                            }
                        } 

        public void WriteLog(String strComments, int userId = 0)
        {
            return;

            //if (activated == "true")
            //{
            //    if (String.IsNullOrEmpty(strPath))
            //    {
            //        return;
            //    }
            //    else
            //    {                    
            //        if (!System.IO.File.Exists(strPath))
            //        {
            //            System.IO.Directory.CreateDirectory(defRoot);
            //            strPath = defRoot + "Error_" + (userId > 0 ? userId.ToString() + "_" : "") + DateTime.Now.ToString("ddMMyyyy") + ".log";
            //        }

            //        FileStream fileStream;
            //        if (System.IO.File.Exists(strPath))
            //            fileStream = new FileStream(strPath, FileMode.Append, FileAccess.Write);
            //        else
            //            fileStream = new FileStream(strPath, FileMode.Create, FileAccess.Write);
                    
            //        using (fileStream)
            //        {
            //            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            //            {
            //                streamWriter.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " " + strComments);
            //            }
            //        }
            //        return;
            //    }
            //}
        }
    }
}