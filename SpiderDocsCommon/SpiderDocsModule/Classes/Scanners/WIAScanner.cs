using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WIA;

namespace SpiderDocsModule.Classes.Scanners
{
    public class WIAScanner : Scanner, IScanner
    {
        DeviceInfo _device = null;

        public en_Status Status { get;set; } = en_Status.None;

        public bool ShowProgressBar { get; set; } = true;

        //string OutputPath { get; set; }



        public WIAScanner(DeviceInfo device)
        {
            _device = device;
        }

        public bool CheckOnline()
        {
            bool online = true;
            try
            {
                this._device.Connect();
            }
            catch
            {
                online = false;
            }

            return online;
        }

        public bool IsSupportFormat(en_SupportExt extension)
        {
            return GetFormat(extension) != string.Empty;
        }

        string GetFormat(en_SupportExt extension)
        {
            Dictionary<en_SupportExt, string> dic = new Dictionary<en_SupportExt, string>();

            string[] supports = new string[] { WIA.FormatID.wiaFormatBMP/*, WIA.FormatID.wiaFormatGIF, WIA.FormatID.wiaFormatJPEG, WIA.FormatID.wiaFormatPNG, WIA.FormatID.wiaFormatTIFF*/ };
            foreach (string support in supports)
                dic.Add(en_SupportExt.BMP, support);

            return dic[extension];
        }

        public string[] Scan(en_SupportExt extension, bool isPreview=false,PropScanType ScanType = null, PropResolution Resolution = null, PropColurDepth BitsPerPix = null)
        {
            string output = System.IO.Path.Combine(FileFolder.TempPath, string.Format("Scan_{0}", String.Format("{0:D4}", UniqStr())) + ".bmp");

            Status = en_Status.None;
            
            /*
            string[] supports = new string[] { WIA.FormatID.wiaFormatBMP, WIA.FormatID.wiaFormatGIF, WIA.FormatID.wiaFormatJPEG, WIA.FormatID.wiaFormatPNG, WIA.FormatID.wiaFormatTIFF };

            if (!supports.ToList().Contains(format)) throw new Exception("Not Support Format");
            */

            if( !IsSupportFormat(extension)) throw new Exception("Not Support Format");

            string format = GetFormat(extension);

            if (ScanType == null)
                ScanType = PropScanTypeAllSource()[0];

            if (Resolution == null)
                Resolution = PropResolutionAllSource()[0];

            if (BitsPerPix == null)
                BitsPerPix = PropColurDepthAllSource()[0];

            // Connect to the device and instruct it to scan
            // Connect to the device
            var device = this._device.Connect();

            // Select the scanner
            CommonDialogClass dlg = new CommonDialogClass();

            var item = device.Items[1];


            try
            {

                //AdjustScannerSettings(scannnerItem, (int)nudRes.Value, 0, 0, (int)nudWidth.Value, (int)nudHeight.Value, 0, 0, cmbCMIndex);

                SetWIAProperty(item.Properties, PropColurDepth.WIA_PROPERTY, BitsPerPix.Value);
                SetWIAProperty(item.Properties, PropScanType.WIA_PROPERTY, ScanType.Value);
                SetWIAProperty(item.Properties, PropResolution.WIA_PROPERTY_H, Resolution.Value);
                SetWIAProperty(item.Properties, PropResolution.WIA_PROPERTY_V, Resolution.Value);


                object scanResult;

                if (ShowProgressBar)
                    scanResult = dlg.ShowTransfer(item, format, true);
                else
                    scanResult = item.Transfer(format);

                //object scanResult = dlg.ShowTransfer(item, format, true);

                if (scanResult != null)
                {
                    Status = en_Status.Success;

                    var imageFile = (ImageFile)scanResult;


                    if (System.IO.File.Exists(output))
                    {
                        System.IO.File.Delete(output);
                    }

                    //imageFile.SaveFile(output);

                    imageFile.SaveFile(output);

                    // Return the imageFile
                    //return imageFile;
                }
            }
            catch (COMException e)
            {
                logger.Error(e);

                Status = en_Status.Error;

                // Display the exception in the console.
                //Console.WriteLine(e.ToString());

                uint errorCode = (uint)e.ErrorCode;

                // Catch 2 of the most common exceptions
                if (errorCode == 0x80210006)
                {
                    //MessageBox.Show("The scanner is busy or isn't ready");
                }
                else if (errorCode == 0x80210064)
                {
                    Status = en_Status.Cancel;
                    //MessageBox.Show("The scanning process has been cancelled.");
                }
                else
                {
                    //MessageBox.Show("A non catched error occurred, check the console", "Error", MessageBoxButtons.OK);
                }

            }

            return new[]{output};
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        private static void SetWIAProperty(IProperties properties, object propName, object propValue)
        {
            Property prop = properties.get_Item(ref propName);
            prop.set_Value(ref propValue);
        }

        /// <summary>
        /// Declare the ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (string)this._device.Properties["Name"].get_Value();
        }

        public List<PropColurDepth> PropColurDepthAllSource()
        {
            List<PropColurDepth> sources = new List<PropColurDepth>();

            sources.Add(new PropColurDepth("8 bit", 8));
            sources.Add(new PropColurDepth("24 bit", 24));

            return sources;
        }

        public List<PropScanType> PropScanTypeAllSource()
        {
            List<PropScanType> sources = new List<PropScanType>();

            sources.Add(new PropScanType("Color", (int)PropScanType.ImageIntent.ColorIntent));
            sources.Add(new PropScanType("Gray Scale", (int)PropScanType.ImageIntent.GrayscaleIntent));
            sources.Add(new PropScanType("Black and White", (int)PropScanType.ImageIntent.TextIntent));

            return sources;
        }

        public List<PropResolution> PropResolutionAllSource()
        {
            List<PropResolution> sources = new List<PropResolution>();

            var device = _device.Connect();
            int[] dpis = new int[] { 100, 150, 300, 600, 1200, 2400 };
            int _dpi = 0;
            for (int i = 0; i < dpis.Length; i++)
            {
                int dpi = dpis[i];

                try
                {
                    device.Items[1].Properties.get_Item(PropResolution.WIA_PROPERTY_H).set_Value(dpi);
                    device.Items[1].Properties.get_Item(PropResolution.WIA_PROPERTY_V).set_Value(dpi);
                    sources.Add(new PropResolution(string.Format("{0} x {0} dpi", dpi), dpi));
                    if (_dpi == 0) _dpi = dpi;
                }
                catch
                {
                    //dpi = dpis[i - 1];

                    //device.Items[1].Properties.get_Item(WIA_PROPERTY_H).set_Value(dpi);
                    //device.Items[1].Properties.get_Item(WIA_PROPERTY_V).set_Value(dpi);
                }
            }

            device.Items[1].Properties.get_Item(PropResolution.WIA_PROPERTY_H).set_Value(_dpi);
            device.Items[1].Properties.get_Item(PropResolution.WIA_PROPERTY_V).set_Value(_dpi);


            return sources;
        }


    }
}
