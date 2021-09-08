using NDesk.Options;
using Saraff.Twain;
using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using System.Drawing.Imaging;

// Using Saraff Scnning 
// https://github.com/saraff-9EB1047A4BEB4cef8506B29BA325BD5A/Saraff.Twain.NET
namespace Scanner
{
    class Program
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        static bool ShowHelp {get;set;} = false;
        static int Index {get;set;} = 0;
        static string ScanType {get;set;} = string.Empty;
        static string Resolution {get;set;} = string.Empty;
        static bool IsPreview { get; set; } = false;
        static string OutputFile {get;set;} = string.Empty;
        static OptionSet CommandParameters = new OptionSet() {

            { "o|output=", "The based path that output actual file. ",
                v => OutputFile = v },

            { "s|source=", "The Source Index that Twain Driver access",
                (int v) => Index = v },

            { "t|scantype=", "Scan Type:8bit, 16bit, 32bit. NOT SUPPORTED ",
                v => ScanType = v },

            { "r|resolution=", "Resolution Index.",
                v => Resolution = v },

            { "p|preview=", "Preview Mode(y/N). Default No",
                v => IsPreview = !string.IsNullOrEmpty(v) ? v.ToLower() == "y" : false },

            { "h|help",  "show this message and exit",
                v => ShowHelp = v != null }
        };

        static void Main(string[] args)
        {
            try
            {
                List<string> extra = CommandParameters.Parse(args);

                Acquire(OutputFile,Index, ScanType,Resolution);

            }
            catch (Exception e)
            {
                logger.Error(e);
                
                Console.WriteLine(3); // Return As Error 
            }
        }

        static void Acquire(string output, int sourceIndex, string type, string resolution)
        {            
            Twain32 twain32 = new Twain32();
            
            int exitCode = 0; // en_Status.None
            
            int fileNumber = 0 ;             

            List<string> paths = new List<string>();
            
            string[] devided = output.Split('.');            
            string basefile = string.Join(".", devided.Take( devided.Count() - 1) );    // C:\Dev\test.jpg -> C:\Dev\test
            string extension = devided.LastOrDefault();     // C:\Dev\test.jpg -> .jpg

            try
            {
                twain32.Language = TwLanguage.ENGLISH_AUSTRALIAN;
                twain32.ShowUI = false;
                twain32.IsTwain2Enable = true;
                twain32.OpenDSM();
                twain32.SourceIndex = sourceIndex;
                twain32.OpenDataSource();

                // Colour depth is not supported.

                //Resolution
                var _resolutions = twain32.Capabilities.XResolution.Get();
                var _val1 = (float)_resolutions[Convert.ToInt32(resolution)];
                twain32.Capabilities.XResolution.Set(_val1);
                twain32.Capabilities.YResolution.Set(_val1);

                // Scan Type
                var _pixels = twain32.Capabilities.PixelType.Get();
                var _val = (TwPixelType)_pixels[Convert.ToInt32(type)];
                twain32.Capabilities.PixelType.Set(_val);

                twain32.EndXfer += (object sender, Twain32.EndXferEventArgs e) =>
                {
                    try
                    {
                        string path = string.Format("{0}_{1}.{2}",basefile,fileNumber++,extension); // C:\Dev\test_1.jpg 
                        e.Image.Save(path, ImageFormat.Bmp);
                        e.Image.Dispose();

                        paths.Add(path);

                        exitCode = 1;  // en_Status.Success

                        if (IsPreview) e.Cancel = true; // Preview only show one file
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                };

                twain32.AcquireCompleted += (sender, e) =>
                {

                    //try
                    //{
                    //    Console.WriteLine();
                    //    Console.WriteLine("Acquire Completed.");
                    //}
                    //catch (Exception ex)
                    //{
                    //    Program.WriteException(ex);
                    //}
                };

                twain32.AcquireError += (object sender, Twain32.AcquireErrorEventArgs e) =>
                {
                    if (e.Exception != null)
                        logger.Error(e);

                    if (e.Exception.ReturnCode == TwRC.Cancel)
                        exitCode = 2; // en_Status.Cancel;
                    else
                        exitCode = 3; // en_Status.Error
                };

                twain32.Acquire();
                //await Task.Run(() => _twain32.Acquire());
                
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
            finally
            {
                twain32.CloseDataSource();
                twain32.CloseDSM();

                // Response information to the parent process
                Console.WriteLine(exitCode);

                for ( int i = 0; i < paths.Count; i++)
                {
                    if (i < paths.Count - 1)
                        Console.WriteLine(paths[i]);
                    else
                        Console.Write(paths[i]);    // last
                }
            }
        }
    
    }
}
