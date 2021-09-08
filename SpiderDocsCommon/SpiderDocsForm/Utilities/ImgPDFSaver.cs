using SpiderDocsModule;
using System.Collections.Generic;
using System.Threading;

//---------------------------------------------------------------------------------
namespace SpiderDocsForms
{
	public class ImgPDFSaver
	{
        
        bool _searchable = true;
        string[] _paths = new string[] { };

		public enum en_status
		{
			None = 0,

			ExtractImg,
			LoadImg,
			MakePDF,

			SaveFile,
			Finish,

			Error,
			Aborting,
			Aborted,

			Max
		}


        public ImgPDFSaver(string[] paths, bool searchable = true) 
        {
            _searchable = searchable;
            _paths = paths;
        }

        public Thread StartDialog()
        {
            Thread dialogThread;
            
            dialogThread = new Thread(() => { try { new frmBusy().ShowDialog(); } catch { } });
            dialogThread.SetApartmentState(ApartmentState.STA);
            dialogThread.Start();

            return dialogThread;
        }
        public List<string> Run(bool withDialog = true)
        {
            Thread thread = null;
            List<string> ans = new List<string>();

            try
            {
                if ( withDialog)
                    thread = StartDialog();

                ans = Run();

                thread?.Abort();
                thread?.Join();
            }
            catch (ThreadAbortException)
            {

            }
            return ans;
        }

        private List<string> Run()
        {
            List<string> ans = new List<string>();
            List<OCRManager> ocrs = new List<OCRManager>();

            foreach (string path in _paths)
                ocrs.Add(new OCRManager(path, _searchable));

            ocrs.ForEach(ocr =>
            {
                ans.Add(ocr.GetPDF());
            });

            return ans;
        }

        public string Merge(bool withDialog = true)
        {
            string res = string.Empty;

            try 
            { 
                Thread thread = null;
                List<string> ans = new List<string>();

                if (withDialog)
                    thread = StartDialog();

                res = OCRManager.GetPDFWithMerge(_paths, true);

                thread?.Abort();
                thread?.Join();
            }
            catch(ThreadAbortException)
            {

            }
            
            return res;
        }
    }
}
