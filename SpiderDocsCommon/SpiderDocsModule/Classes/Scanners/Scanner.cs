using Saraff.Twain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;
using NLog;

namespace SpiderDocsModule.Classes.Scanners
{
    public class Scanner
    {
        static protected Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Cache purpose
        /// </summary>
        static List<IScanner> uniqScanners { get; set; } = new List<IScanner>();

        public const string SUPPORT_BMP = "bmp";  

        
        public enum en_SupportExt
        {
            BMP
        }

        public enum en_Status
        {
            None,
            Success,
            Cancel,
            Error
        }

        protected string UniqStr()
        {
            string
                    prefix = DateTime.Now.ToString("ddHHmmssffff"),

                    support = new Random().Next(1, 10000).ToString();

            return prefix + support;

        }

        static public bool scanEnable(bool useCache = true)
        {
           return GetScanners(useCache).Count() > 0;
        }



        static public List<IScanner> GetScanners(bool useCache = true)
        {
            if (useCache && uniqScanners.Count() > 0) return uniqScanners;

            uniqScanners = new List<IScanner>();
            List<IScanner> WIASource =  GetWIAScanner();
            List<IScanner> TWainSource = GetTWainScanner();

            uniqScanners.AddRange(WIASource);
            
            logger.Debug("Scanner found :{0}",uniqScanners.Count);

            foreach ( var _device in TWainSource)
            {
                for (int i = 0; i < uniqScanners.Count; i++)
                    if (!uniqScanners.Exists(x => x.ToString() == _device.ToString())) uniqScanners.Add(_device);                
            }
            

            return uniqScanners;
        }

        static List<IScanner> GetWIAScanner()
        {
            List<IScanner> Scanners = new List<IScanner>();
            try
            {
                // Create a DeviceManager instance
                var deviceManager = new DeviceManager();

                // Loop through the list of devices and add the name to the listbox
                for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
                {
                    // Add the device only if it's a scanner
                    if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                    {
                        continue;
                    }

                    // Add the Scanner device to the listbox (the entire DeviceInfos object)
                    // Important: we store an object of type scanner (which ToString method returns the name of the scanner)

                    IScanner intrface = new WIAScanner(deviceManager.DeviceInfos[i]);

                    Scanners.Add(intrface);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }

            return Scanners;
        }

        static List<IScanner> GetTWainScanner()
        {
            List<IScanner> Scanners = new List<IScanner>();

            using (Twain32 _twain32 = new Twain32()) { 
                _twain32.IsTwain2Enable = true;
                _twain32.OpenDSM();

                for (var i = 0; i < _twain32.SourcesCount; i++)
                {
                    logger.Debug("_twain32.SourcesCount at {0}",i);
                    
                    IScanner intrface = new TWainScanner(i);
                    Scanners.Add(intrface);
                }
            }
           
            return Scanners;
        }

        public bool Equals(IScanner other)
        {
            return (this.ToString() == other.ToString());
        }


        #region  WIA Property Classes

        /// <summary>
        /// https://www.c-sharpcorner.com/blogs/create-wpf-browser-application-to-scan-a-document-from-scanneradf-and-flatbed-using-wia
        /// </summary>
        public class PropColurDepth
        {
            //https://msdn.microsoft.com/en-us/library/windows/desktop/ms630202(v=vs.85).aspx
            public const string WIA_PROPERTY = "4104";  //bit depth 

            public string Name { set; get; }

            public int Value { set; get; }

            public PropColurDepth(string name, int value)
            {
                Name = name;
                Value = value;
            }


        }

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms630190(v=vs.85).aspx
        /// </summary>
        /// 
        public class PropScanType
        {
            //https://msdn.microsoft.com/en-us/library/windows/desktop/ms630202(v=vs.85).aspx
            public const string WIA_PROPERTY = "6146";  //CurrentIntent 

            public string Name { set; get; }

            public int Value { set; get; }

            public enum ImageIntent
            {
                UnspecifiedIntent = 0,
                ColorIntent = 1,
                GrayscaleIntent = 2,
                TextIntent = 4

            }
            public PropScanType(string name, int value)
            {
                Name = ToConvinientName(name);
                Value = value;
            }
            string ToConvinientName(string name)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("RGB", "Color");
                dic.Add("Gray", "Gray Scale");
                dic.Add("BW", "Black and White");

                if ( dic.ContainsKey(name))
                {
                    name = dic[name];
                }

                return name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class PropResolution
        {
            //
            public const string WIA_PROPERTY_H = "6147";  //HorizontalResolution  
            public const string WIA_PROPERTY_V = "6148";  //HorizontalResolution  

            public string Name { set; get; }

            public int Value { set; get; }
            

            public PropResolution(string name, int value)
            {
                Name = name;
                Value = value;
            }
        }
        #endregion 



    }

}
