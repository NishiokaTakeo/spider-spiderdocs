using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spider.IO;
using NLog;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;

namespace WebSpiderDocs
{
    public class Fn: SpiderDocsModule.Fn
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        

        public static string ToFileSystemPath(string url)
        {
            logger.Debug("url:{0}", url);
            
            try
            {
                logger.Debug("{0},{1}", WebUtilities.GetCurrentUri(GetRootUriMode.Root), HttpRuntime.AppDomainAppPath);

                return ToFixPath(url.Replace(WebUtilities.GetCurrentUri(GetRootUriMode.Root), HttpRuntime.AppDomainAppPath));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return string.Empty;
        }

        
        public static string Img2PdfPlacedTmp(byte[] byte_image)
        {
            return Img2Pdf(byte_image, new WebUtilities().CreateTempFolder(postfix: Guid().ToString()) + "exported.pdf");
        }

        public static string Txt2PdfPlacedTmp(string text)
        {
            return Txt2Pdf(text, new WebUtilities().CreateTempFolder(postfix: Guid().ToString()) + "exported.pdf");
        }


    }
}