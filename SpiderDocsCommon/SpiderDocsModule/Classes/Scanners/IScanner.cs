using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpiderDocsModule.Classes.Scanners
{
    public interface IScanner : IEquatable<IScanner>
    {
        bool ShowProgressBar { get; set; }

        Scanner.en_Status Status { get; set; }
        
        //string OutputPath { get; set; }
        bool CheckOnline();
        
        bool IsSupportFormat(Scanner.en_SupportExt extension);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>file path</returns>
        string[] Scan(Scanner.en_SupportExt format, bool isPreview = false, Scanner.PropScanType ScanType = null, Scanner.PropResolution Resolution = null, Scanner.PropColurDepth BitsPerPix = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Human Device Name</returns>
        string ToString();


        List<Scanner.PropColurDepth> PropColurDepthAllSource();

        List<Scanner.PropScanType> PropScanTypeAllSource();

        List<Scanner.PropResolution> PropResolutionAllSource();


        /*
                
        private static void SetWIAProperty(IProperties properties, object propName, object propValue)

        
        */
    }
}
