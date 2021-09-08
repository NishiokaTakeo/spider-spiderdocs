using System;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

//---------------------------------------------------------------------------------
namespace UpdateWaitDialog
{
	public partial class frmMain : Form
	{
		string mNewVersion = "";
		string mAppPath = "";

//---------------------------------------------------------------------------------
		public frmMain(string[] args)
		{
			InitializeComponent();

			mNewVersion = args[1];
			mAppPath = args[2];
		}

//---------------------------------------------------------------------------------
		private void frmMain_Shown(object sender, EventArgs e)
		{
			BackgroundWorker thread = new BackgroundWorker();
			thread.DoWork += new DoWorkEventHandler(thread_DoWork);
			thread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(thread_WorkDone);
			thread.RunWorkerAsync();
		}

//---------------------------------------------------------------------------------
		void thread_DoWork(object sender, DoWorkEventArgs e)
		{
            
			bool ans = true;
            int cnt = 0;
            string CurrentVersion = "";
	
			do
			{
                if (300 <= cnt) // 1000ms sleep * 180 = 3min
                {
                    ans = false;
                    break;
                }

                cnt++;

                System.Threading.Thread.Sleep(1000);

				if(File.Exists(mAppPath))
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(mAppPath);
					CurrentVersion = versionInfo.ProductVersion.Substring(0, (versionInfo.ProductVersion.Length - 2)); // 1.5.7.0 -> 1.5.7 cutting last 2 latters
				}

			} while(CurrentVersion != mNewVersion);

			e.Result = ans;
		}

//---------------------------------------------------------------------------------
		void thread_WorkDone(object sender, RunWorkerCompletedEventArgs e)
		{
			if((bool)e.Result)
			{
				Process process = new Process();
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.FileName = mAppPath;
				process.StartInfo = startInfo;
				process.Start();
			}

			Close();
		}

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        //---------------------------------------------------------------------------------
    }
}
