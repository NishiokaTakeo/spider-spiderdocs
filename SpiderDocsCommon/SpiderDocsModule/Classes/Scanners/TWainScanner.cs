using Saraff.Twain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
namespace SpiderDocsModule.Classes.Scanners
{
    class TWainScanner : Scanner, IScanner,IDisposable
    {

        Twain32 _twain32 = null ;

        string Name { get; set; } = string.Empty;
        public bool ShowProgressBar { get; set; } = true;
        //public bool IsTwain2Enable { get; set; } = true;

        public en_Status Status { get; set; } = en_Status.None;

        public string OutputPath { get; set; }
        public int SourceIndex { get; private set; } = 0;

        public TWainScanner(int index)
        {
            _twain32 = new Twain32();
            _twain32.Language = TwLanguage.ENGLISH_AUSTRALIAN;
            _twain32.ShowUI = false;
            _twain32.IsTwain2Enable = true;

            SourceIndex = index;

            OpenTwain(false);

            CloseTwain(false);
        }

        bool OpenTwain(bool withDataSource = true)
        {
            logger.Trace("Begin");

            bool online = true;
            try
            {
                _twain32.OpenDSM();
                _twain32.SourceIndex = SourceIndex;

                Name = _twain32.GetSourceProductName(SourceIndex);
                
                logger.Debug("Name by {0}",Name);

                if(withDataSource) _twain32.OpenDataSource();
            }/*
            catch(Saraff.Twain.TwainException ex)
            {
                 
                switch(ex.ConditionCode)
                {
                    case TwCC.CheckDeviceOnline:
                        if (ex.ReturnCode == TwRC.Failure)
                        {

                        }

                        break;
                }

            }*/
            catch(Exception ex)
            {
                logger.Error(ex);
                online = false;
            }

            return online;
        }

        public bool CheckOnline()
        {
            bool online = true;

            try
            {
                _twain32.OpenDSM();
                _twain32.SourceIndex = SourceIndex;
                _twain32.OpenDataSource();
                _twain32.CloseDataSource();
                _twain32.CloseDSM();

            }
            catch
            {
                online = false;
            }

            return online;
        }

        void CloseTwain(bool withDataSource = true)
        {
            if (withDataSource) _twain32.CloseDataSource();

            _twain32.CloseDSM();
        }
        
        public bool IsSupportFormat(en_SupportExt extension)
        {
            return GetFormat(extension) != null;
        }

        ImageFormat GetFormat(en_SupportExt extension)
        {
            Dictionary<en_SupportExt, ImageFormat> dic = new Dictionary<en_SupportExt, ImageFormat>();

            ImageFormat[] supports = new ImageFormat[] { ImageFormat.Bmp/*, WIA.FormatID.wiaFormatGIF, WIA.FormatID.wiaFormatJPEG, WIA.FormatID.wiaFormatPNG, WIA.FormatID.wiaFormatTIFF*/ };
            foreach (ImageFormat support in supports)
                dic.Add(en_SupportExt.BMP, support);

            return dic[extension];
        }

        public string[] Scan(en_SupportExt extension, bool isPreview = false,PropScanType ScanType = null, PropResolution Resolution = null, PropColurDepth BitsPerPix = null)
        {
            //OpenTwain();
            string[] files = new string[]{};
            string output = System.IO.Path.Combine(FileFolder.TempPath, string.Format("Scan_{0}", String.Format("{0:D4}", UniqStr())) + ".bmp");

            Status = en_Status.None;

            
            if (!IsSupportFormat(extension)) throw new Exception("Not Support Format");

            ImageFormat format = GetFormat(extension);


            if (ScanType == null)
                ScanType = PropScanTypeAllSource()[0];

            if (Resolution == null)
                Resolution = PropResolutionAllSource()[0];

            if (BitsPerPix == null)
                BitsPerPix = PropColurDepthAllSource()[0];


            try
            {
                string[] returns = _StartScan(output, isPreview,ScanType, Resolution);

                Status  = (en_Status)int.Parse(returns[0]); // return code

                files = returns.Skip(1).ToArray(); // Skip Status code
                    
            }
            catch (Exception e)
            {
                Status = en_Status.Error;

                logger.Error(e);

            }
            finally
            {
                //CloseTwain();
            }

            return files;


            //try
            //{
            //    // Colour depth is not supported.

            //    //Resolution
            //    var _resolutions = _twain32.Capabilities.XResolution.Get();
            //    var _val1 = (float)_resolutions[Convert.ToInt32(Resolution.Value)];
            //    _twain32.Capabilities.XResolution.Set(_val1);
            //    _twain32.Capabilities.YResolution.Set(_val1);

            //    // Scan Type
            //    var _pixels = _twain32.Capabilities.PixelType.Get();
            //    var _val = (TwPixelType)_pixels[Convert.ToInt32(BitsPerPix.Value)];
            //    _twain32.Capabilities.PixelType.Set(_val);



            //    _twain32.EndXfer += (object sender, Twain32.EndXferEventArgs e) =>
            //    {
            //        try
            //        {
            //            e.Image.Save(output, format);
            //            e.Image.Dispose();
            //        }
            //        catch (Exception ex)
            //        {
            //            logger.Error(ex);

            //            Status = en_Status.Error;
            //        }
            //    };

            //    _twain32.AcquireCompleted += (sender, e) =>
            //    {
            //        //try
            //        //{
            //        //    Console.WriteLine();
            //        //    Console.WriteLine("Acquire Completed.");
            //        //}
            //        //catch (Exception ex)
            //        //{
            //        //    Program.WriteException(ex);
            //        //}
            //    };

            //    _twain32.AcquireError += (object sender, Twain32.AcquireErrorEventArgs e) =>
            //    {

            //        if (e.Exception != null)
            //            logger.Error(e);

            //        Status = en_Status.Error;

            //        if (e.Exception.ReturnCode == TwRC.Cancel)
            //            Status = en_Status.Cancel;
            //    };
            //    _twain32.Acquire();
            //    //await Task.Run(() => _twain32.Acquire());

            //}
            //catch (Exception e)
            //{
            //    Status = en_Status.Error;

            //    logger.Error(e);

            //}
            //finally
            //{
            //    CloseTwain();
            //}

            //return output;
        }
        string[] _StartScan(string output, bool isPreview = false,PropScanType ScanType = null,PropResolution Resolution = null)
        {
            string consoleResult = string.Empty;
            string args = string.Format("-o \"{0}\" -s {1} -t {2} -r {3} -p {4}", output, SourceIndex, ScanType.Value, Resolution.Value, isPreview ? "Y":"N");

            try{

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + "Scanner.exe";//@"C:\Dev\SpiderDocs\Scanner\bin\Debug\Scanner.exe";
                startInfo.Arguments = args;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;

                Process processTemp = new Process();
                processTemp.StartInfo = startInfo;
                processTemp.EnableRaisingEvents = true;

                processTemp.Start();

                
                using(StreamReader reader = processTemp.StandardOutput)
                    consoleResult = reader.ReadToEnd();
                
                processTemp.WaitForExit();

            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }

            return consoleResult.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }
        /// <summary>
        /// Declare the ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        public List<PropColurDepth> PropColurDepthAllSource()
        {
            OpenTwain();

            List<PropColurDepth> sources = new List<PropColurDepth>();

            sources.Add(new PropColurDepth("Device Default", 0));

            CloseTwain();

            return sources;
        }

        public List<PropResolution> PropResolutionAllSource()
        {
            List<PropResolution> sources = new List<PropResolution>();
            OpenTwain();

            var _resolutions = _twain32.Capabilities.XResolution.Get();
            int defIndex = _resolutions.CurrentIndex;

            //if (_resolutions[defIndex] < dpi)
            int[] dpis = new int[] { 100, 150, 300, 600, 1200, 2400 };
            foreach (int dpi in dpis)
            {

                try
                {
                    for (var i = 0; i < _resolutions.Count; i++)
                    {
                        int avlblDpi = int.Parse(_resolutions[i].ToString());

                        if (dpi == avlblDpi)
                        {
                            sources.Add(new PropResolution(string.Format("{0} x {0} dpi", dpi), i));
                        }

                    }

                }
                catch { }
            }


            if(sources.Count() == 0)
                sources.Add(new PropResolution(string.Format("{0} x {0} dpi", _resolutions[defIndex]), defIndex));

            CloseTwain();

            return sources;
        }
        
        public List<PropScanType> PropScanTypeAllSource()
        {
            List<PropScanType> sources = new List<PropScanType>();

            OpenTwain();

            var _pixels = _twain32.Capabilities.PixelType.Get();

            for (var i = _pixels.Count -1 ; i >= 0; i--)
            {
                sources.Add(new PropScanType(_pixels[i].ToString(), i));
            }

            CloseTwain();

            return sources;

        }

        public void Dispose()
        {
            CloseTwain();
        }


    }
}
