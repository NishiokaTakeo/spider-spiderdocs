using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpiderDocsModule.Classes.Scanners;
using NLog;

namespace SpiderDocsForms
{
    public partial class frmScannerController : Form
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        
        public delegate void EventFunc(object arg);
        public delegate void EventFuncB(IScanner scn);
        
        public event EventFunc OnStartLoadImage;
        public event EventFunc OnLoadingImage;
        public event EventFunc OnLoadedImage;
        public event EventFuncB OnWorkDone;

        public IScanner iScanner { get; set; } 

        private int ScanFileCnt { get; set; } = 0;

        public frmScannerController(IScanner scanner = null)
        {
            InitializeComponent();

            iScanner = scanner ?? Scanner.GetScanners().FirstOrDefault();
            lblScanner.Text = iScanner?.ToString();
        }

        public void SetScanner(IScanner scanner)
        {
            iScanner = scanner;
            lblScanner.Text = iScanner?.ToString();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            //ListScanners();

            // Set start output folder TMP
            //textBox1.Text = Path.GetTempPath();
            // Set JPEG as default
            cmbScanType.Items.Clear();
            iScanner.PropScanTypeAllSource().ForEach(x => {
                cmbScanType.Items.Add(x);
            });
            cmbScanType.DisplayMember = "Name";
            cmbScanType.ValueMember = "Value";
            //cmbResolution.SelectedIndex =0;

            cmbResolution.Items.Clear();
            iScanner.PropResolutionAllSource().ForEach(x => {
                cmbResolution.Items.Add(x);
            });
            cmbResolution.DisplayMember = "Name";
            cmbResolution.ValueMember = "Value";

        }

        private void frmScannerController_Shown(object sender, EventArgs e)
        {
            cmbScanType.SelectedIndex = 0;
            cmbResolution.SelectedIndex = 0;// cmbResolution.Items.Count - 1;
            rbColourDepth1.Checked = true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(StartScanning).ContinueWith(result => TriggerScan());
        }

        private void TriggerScan()
        {
            Console.WriteLine("Image succesfully scanned");
        }

        public void StartScanning()
        {            
            if (OnStartLoadImage != null)
                OnStartLoadImage(1); //Page1

            IScanner device = null;

            this.Invoke(new MethodInvoker(delegate ()
            {
                device = iScanner;
            }));

            if (device == null)
            {
                MessageBox.Show("You need to select first an scanner device from the list",
                                "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string[] output = new string[]{};
            //string imageExtension = ".bmp";

            this.Invoke(new MethodInvoker(delegate ()
            {
                try
                { 
                    Scanner.PropScanType scanTyp = cmbScanType.SelectedItem as Scanner.PropScanType;
                    Scanner.PropResolution resolution = cmbResolution.SelectedItem as Scanner.PropResolution;

                    Scanner.PropColurDepth colourDepth = iScanner.PropColurDepthAllSource().FirstOrDefault();
                    if (rbColourDepth3.Checked) colourDepth = iScanner.PropColurDepthAllSource().LastOrDefault();
                
                    /*rbColourDepth.Checked ? 
                    rbColourDepth2.Checked
                    rbColourDepth3.Checked
                    */

                    output = device.Scan(Scanner.en_SupportExt.BMP,false, scanTyp, resolution, colourDepth);

                    if (device.Status == Scanner.en_Status.Cancel) { OnWorkDone(device); return; }

                    if (OnLoadingImage != null)
                        OnLoadingImage(null);

                    //image.FrameCount
                    // Save the image
                    foreach( string location in output)
                    {
                    
                        string path = Path.Combine(FileFolder.TempPath, Path.GetFileName(location));

                        //if (File.Exists(path)) File.Delete(path);

                        //System.IO.File.Move(location, path);
                        //image.SaveFile(path);

                        if (OnLoadedImage != null)
                            OnLoadedImage(location);

                        using (var img = new Bitmap(location)) 
                            pictureBox1.Image = img;

                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
                finally { 

                    if (OnWorkDone != null)
                        OnWorkDone(device);
                }

            }));                    
        }

        public void StartPreviewScanning()
        {
            //if (OnStartLoadImage != null)
            //    OnStartLoadImage(1); //Page1

            string filename = string.Format("Scan_prev_{0}", String.Format("{0:D4}", ScanFileCnt++));

            IScanner device = null;

            this.Invoke(new MethodInvoker(delegate ()
            {
                device = iScanner;
            }));

            if (device == null)
            {
                MessageBox.Show("You need to select first an scanner device from the list",
                                "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (String.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Provide a filename",
                                "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string[] output = new string[]{};
            //string imageExtension = ".bmp";

             Invoke(new MethodInvoker(delegate ()
            {
                try
                {
                    Scanner.PropScanType scanTyp = cmbScanType.SelectedItem as Scanner.PropScanType;
                    Scanner.PropResolution resolution = cmbResolution.SelectedItem as Scanner.PropResolution;

                    Scanner.PropColurDepth colourDepth = iScanner.PropColurDepthAllSource()[0]; ;
                    if (rbColourDepth3.Checked) colourDepth = iScanner.PropColurDepthAllSource()[1];

                    output = device.Scan(Scanner.en_SupportExt.BMP, true, scanTyp, resolution, colourDepth);

                    if (device.Status == Scanner.en_Status.Cancel) { /*OnWorkDone(device);*/ return; }

                    pictureBox1.Image = new Bitmap(output[0]);
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }

            }));
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnPreview_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(StartPreviewScanning).ContinueWith(result => TriggerScan());
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            cmbScanType.SelectedIndex = 0;
            cmbResolution.SelectedIndex = 0;// cmbResolution.Items.Count - 1;
            rbColourDepth1.Checked = true;
        }

        private void frmScannerController_FormClosed(object sender, FormClosedEventArgs e)
        {
            pictureBox1.Image = null;
        }


        /*
private void button2_Click(object sender, EventArgs e)
{
FolderBrowserDialog folderDlg = new FolderBrowserDialog();
folderDlg.ShowNewFolderButton = true;
DialogResult result = folderDlg.ShowDialog();

if (result == DialogResult.OK)
{
textBox1.Text = folderDlg.SelectedPath;
}
}
*/

    }


}
