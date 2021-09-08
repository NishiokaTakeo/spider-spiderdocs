//using System;
//using System.IO;
//using System.Collections.Generic;
//using System.ComponentModel;
//using Spider.PDFUtilities;
//using System.Linq;
//using lib = SpiderDocsModule.Library;
//using NLog;

////---------------------------------------------------------------------------------
//namespace SpiderDocsModule
//{
//    public enum ConvertedPDFSaveMode
//    {
//        InsertDocument,
//        UpdateDocument,
//        DoNotSaveToDB
//    }

//    public class ImgPDFSaver
//    {
//        //---------------------------------------------------------------------------------
//        static Logger logger = LogManager.GetCurrentClassLogger();

//        public delegate void CallBackFunc();
//        public delegate void CallBackFuncBool(bool result);
//        public delegate void CallBackFuncStr(string arg);
//        public delegate void BeforeSave(List<Document> saving, ImgPDFSaverArg args);

//        public event CallBackFunc onProgressUpdated;
//        public event CallBackFuncBool onCompleted;

//        public event CallBackFunc onStartExtractImage;
//        public event CallBackFuncStr onRequestExtractedImageAdd;
//        public event CallBackFunc onCompletedExtractImage;
//        public event BeforeSave onBeforeSave;
//        public event CallBackFunc onCompletedConvertToPDF;
//        public event CallBackFunc onCompletedSaveFile;

//        public event CallBackFunc onCancelled;

//        int AllPages = 0;
//        int CurrentPage = 0;
//        protected en_status status = en_status.None;
//        protected bool errorThread;
//        bool allow_duplicatedName;

//        string _StatusMessage;
//        public string StatusMessage { get { return _StatusMessage; } }

//        int _ProgBarVal;
//        public int ProgBarVal { get { return _ProgBarVal; } }

//        bool _ShowProgBar;
//        public bool ShowProgBar { get { return _ShowProgBar; } }

//        bool _ShowAbort;
//        public bool ShowAbort { get { return _ShowAbort; } }

//        List<string> _ExtractedImagePaths;
//        public List<string> ExtractedImagePaths { get { return _ExtractedImagePaths; } }
//        public List<string> PDF_Files { get; set; } = new List<string>();
//        List<OCRManager> _ocrs = new List<OCRManager>();

//        // bool _CancelReq = false;
//		// bool CancelReq
//		// {
//		// 	get
//		// 	{
//		// 		return _CancelReq;
//		// 	}

//		// 	set
//		// 	{
//		// 		_CancelReq = value;

//		// 		if(mOcr)
//		// 			OCRManager.CancelReq = _CancelReq;
//		// 	}
//		// }

////---------------------------------------------------------------------------------
//		public enum en_status
//		{
//			None = 0,

//			ExtractImg,
//			LoadImg,
//			MakePDF,

//			SaveFile,
//			Finish,

//			Error,
//			Aborting,
//			Aborted,

//			Max
//		}

//		public class ImgPDFSaverArg : EventArgs
//		{
//			public en_status status;
//			public bool errorThread;
//		}

//		//---------------------------------------------------------------------------------
//		readonly string[] tb_message = new string[(int)en_status.Max]
//		{
//			"",								//en_status.None
//			"Extracting pages from PDF",	//en_status.ExtractImg
//			"Loading images",				//en_status.LoadImg
//			"Converting to PDF",			//en_status.LoadImg
//			"Saving a file",				//en_status.ExtractImg
//			"Completed!",					//en_status.Finish
//			"Operation not performed.",		//en_status.Error
//			"Aborting",						//en_status.Aborting
//			"Cancelled"						//en_status.Aborted
//		};


////---------------------------------------------------------------------------------
//		public ImgPDFSaver(bool allow_duplicatedName)
//		{
//			this.allow_duplicatedName = allow_duplicatedName;
//		}

////---------------------------------------------------------------------------------
//		public void Abort()
//		{
//			status = en_status.Aborting;

//			updateProgress();

//			_ocrs.ForEach(x => x.Cancel());
			
//			if(onProgressUpdated != null)
//				onProgressUpdated();
//		}

//		public void Run()
//		{
			
//		}

//		//---------------------------------------------------------------------------------
//		// Extract Images -----------------------------------------------------------------
//		//---------------------------------------------------------------------------------
//		public void ExtractImage(string[] paths)
//		{
//            foreach (var path in paths)
//                _ocrs.Add(new OCRManager(path));
//		}

//		//public void ExtractImage(string[] paths)
//		//{
//		//	OnStartExtractImage();
			
//		//	BackgroundWorker bw = new BackgroundWorker();
//		//	bw.DoWork += new DoWorkEventHandler(thread_ExtractImage);
//		//	bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(thread_WorkDone);
//		//	bw.RunWorkerAsync(paths);
//		//}

//		//protected void OnStartExtractImage()
//		//{
//		//	status = en_status.ExtractImg;

//		//	updateProgress();
//		//	if(onStartExtractImage != null)
//		//		onStartExtractImage();
//		//}

//// Start thread --------------------------------------------------------------------
//		// void thread_ExtractImage(object sender, DoWorkEventArgs e)
//		// {
//        //     List<string> ls_paths = ExtractImage_Step1((string[])e.Argument);
            
//        //     if (status != en_status.Aborting)
//        //        ExtractImage_Step2(ls_paths);
//        // }

//// STEP1: Extract images ----------------------------------------------------------
//		//List<string> ExtractImage_Step1(string[] paths)
//		//{
            
//		//	//OCRManager.OnStartExtractImage += StartExtractImage;
//		//	//OCRManager.OnUpdatedExtractImage += UpdatedExtractImage;	
			
//		//	//return OCRManager.ExtractPDFToImage(paths);
//		//}

////---------------------------------------------------------------------------------
//		//protected void StartExtractImage(object arg)
//		//{
//		//	AllPages += (int)arg;
//		//	UpdatedExtractImage(null);
//		//}

//  //      //---------------------------------------------------------------------------------
//  //      public void UpdatedExtractImage(object arg)
//		//{
//		//	CurrentPage++;

//		//	if(status == en_status.Aborting)
//		//		CancelReq = true;
//		//	else
//		//		status = en_status.ExtractImg;

//		//	updateProgress();
//		//	if(onProgressUpdated != null)
//		//		onProgressUpdated();
//		//}

//// STEP2: Notify all paths of extracted images to UI thread  ----------------------
//		//void ExtractImage_Step2(List<string> ls_paths)
//		//{
//		//	AllPages = ls_paths.Count;
//		//	CurrentPage = 1;
//		//	status = en_status.LoadImg;

//		//	foreach(string file in ls_paths)
//		//	{
//		//		if(status == en_status.Aborting)
//		//			break;

//		//		RequestExtractedImageAdd(file);
//		//	}

//		//	_ExtractedImagePaths = ls_paths;

//		//	updateProgress();
//		//	CompletedExtractImage();
//		//}

//		//protected void CompletedExtractImage()
//		//{
//		//	if(onCompletedExtractImage != null)
//		//		onCompletedExtractImage();
//		//}

//  //      //---------------------------------------------------------------------------------
//  //      // This is also called by Twain operation
//  //      public void RequestExtractedImageAdd(object file)
//		//{
//		//	updateProgress();
//		//	if(onRequestExtractedImageAdd != null)
//		//		onRequestExtractedImageAdd((string)file);

//		//	CurrentPage++;
//		//}

////---------------------------------------------------------------------------------
//// Convert PDF and Save File ------------------------------------------------------
////---------------------------------------------------------------------------------
//		int m_userId;
//		bool m_localDb;
		
//		Document CurrentDoc;
//		List<string> mImagePaths;
//		bool mSeparate;
//		bool mOcr;
//		ConvertedPDFSaveMode mMode;
//		string mSavePath;

////---------------------------------------------------------------------------------
//		// doc.id_user must be set before calling this function
//		public void SaveFile(int userId, bool localDb, Document doc, List<string> ImagePaths, ConvertedPDFSaveMode Mode, string SavePath, bool Separate, bool Ocr)
//		{
//			errorThread = false;

//			m_userId = userId;
//			m_localDb = localDb;

//			CurrentDoc = doc;
//			mImagePaths = ImagePaths;
//			mSeparate = Separate;
//			mOcr = Ocr;
//			mSavePath = SavePath;
//			mMode = Mode;
			
//			BackgroundWorker bw = new BackgroundWorker();
//			bw.DoWork += new DoWorkEventHandler(thread_SaveFile);
//			bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(thread_WorkDone);
//			bw.RunWorkerAsync();
//		}

//// Start thread ---------------------------------------------------------------
//		void thread_SaveFile(object sender, DoWorkEventArgs e)
//		{
//			// Update status
//			status = en_status.MakePDF;
//			AllPages = mImagePaths.Count;
//			CurrentPage = 1;

//			updateProgress();
//			if(onProgressUpdated != null)
//				onProgressUpdated();

//            // STEP1: Convert images to PDF
//            //List<string> PDF_Files = convertImgToPdf();
//            PDF_Files = convertImgToPdf();

//            // Check if cancel has been requested
//            if (status != en_status.Aborting)
//			{
//				// Check if an user wants to save the PDF to database
//				if(mMode != ConvertedPDFSaveMode.DoNotSaveToDB)
//				{
//					// Update status
//					status = en_status.SaveFile;

//					updateProgress();
//					if(onProgressUpdated != null)
//						onProgressUpdated();

//					// STEP2: Save the PDF to database
//					SaveFileToDB(PDF_Files);

//					if (errorThread)
//						e.Cancel = true;
//				}

//				// Update status
//				updateProgress();
//				if(onCompletedSaveFile != null)
//					onCompletedSaveFile();
//			}

//		}

//// STEP1: Convert images to PDF ---------------------------------------------------
//        /// <summary>
//        /// Merge:
//        ///     to PDf
//        ///     to PDF with OCR
//        /// Seprate:
//        ///     to PDF
//        ///     to PDF with OCR
//        /// </summary>
//        /// <returns></returns>
//		List<string> convertImgToPdf()
//		{
//			string BasePath = mSavePath + Path.GetFileNameWithoutExtension(CurrentDoc.title);
//			List<string> PDF_Files = new List<string>();
//			List<string> PDF_Titles = new List<string>();

//			// Convert to PDF files one by one
//			if(mSeparate)
//			{
//                int i = 0;
//                foreach (var ocr in _ocrs)
//                {
//                    string PostFix = "-" + i;

//                    string path = BasePath + PostFix + ".pdf";

//                    ocr.GetPDF(path);

//                    PDF_Files.Add(path);
//                }

//			// Convert to one combined PDF
//			}else
//			{
//                //string path = BasePath + ".pdf";

//                int i = 0;
//                foreach (var ocr in _ocrs)
//                {
//                    string PostFix = "-" + CurrentPage;

//                    string path = BasePath + i + ".pdf";
//                    string tmp = Path.GetTempFileName();

//                    if (mOcr)
//                    {
//                        ocr.GetPDF(path);
//                        PDF_Files.Add(path);
//                    }
//                    else
//                    {
//                        PdfUtilities.ConvertToPdf(path, tmp);
//                        PDF_Files.Add(tmp);
//                    }

                    
//                }

//                string dst = FileFolder.GetAvailableFileName(BasePath + ".pdf");

//                PdfMerger.MergeFiles(PDF_Files, dst);
//                FileFolder.DeleteFiles(PDF_Files);
//            }

//			if(onCompletedConvertToPDF != null)
//				onCompletedConvertToPDF();

//			return PDF_Files;
//		}

//////---------------------------------------------------------------------------------
////		void convertImgToPdf(string dst, string src)
////		{
////			List<string> paths = new List<string>();

////			paths.Add(src);
////			convertImgToPdf(dst, paths);
////		}

////		string convertImgToPdf(string dst, List<OCRManager> src)
////		{
////			dst = FileFolder.GetAvailableFileName(dst);	//check valid name			

////			if(mOcr)
////			{
////                //OCRManager.OnUpdatedCreatePDF += OCRManager_OnUpdatedCreatePDF;
////                //OCRManager.GetPDF(src, dst);
////                src.First().GetPDF(dst);


////            }
////            else
////			{
////				List<string> pdf_files = new List<string>();

////				foreach(var path in src)
////				{
////					if(_CancelReq)
////						break;

////					string tmp = Path.GetTempFileName();
////					PdfUtilities.ConvertToPdf(path, tmp);
////                    //path.GetPDF(tmp);
////					pdf_files.Add(tmp);

////					OCRManager_OnUpdatedCreatePDF(null);
////				}

////				PdfMerger.MergeFiles(pdf_files, dst);
////				FileFolder.DeleteFiles(pdf_files);
////			}

////			return dst;
////		}

////        string convertImgToPdf2(string dst, List<OCRManager> src)
////        {
////            dst = FileFolder.GetAvailableFileName(dst); //check valid name			

////            if (mOcr)
////            {
////                //OCRManager.OnUpdatedCreatePDF += OCRManager_OnUpdatedCreatePDF;
////                //OCRManager.GetPDF(src, dst);
////                src.First().GetPDF(dst);


////            }
////            else
////            {
////                List<string> pdf_files = new List<string>();

////                foreach (var path in src)
////                {
////                    if (_CancelReq)
////                        break;

////                    string tmp = Path.GetTempFileName();
////                    PdfUtilities.ConvertToPdf(path, tmp);
////                    //path.GetPDF(tmp);
////                    pdf_files.Add(tmp);

////                    OCRManager_OnUpdatedCreatePDF(null);
////                }

////                PdfMerger.MergeFiles(pdf_files, dst);
////                FileFolder.DeleteFiles(pdf_files);
////            }

////            return dst;
////        }
////        //---------------------------------------------------------------------------------
////        // Call back from GetPDF to update progress information
////        void OCRManager_OnUpdatedCreatePDF(object arg)
////		{
////			if(status == en_status.Aborting)
////			{
////				CancelReq = true;

////			}else
////			{
////				CurrentPage++;

////				updateProgress();
////				if(onProgressUpdated != null)
////					onProgressUpdated();
////			}
////		}

//// STEP2: Save the PDF to database -------------------------------------------------		
//		void SaveFileToDB(List<string> paths)
//		{
//			List<Document> docs = new List<Document>();

//			int i = 1;
//			foreach (string path in paths)
//			{

//				CurrentDoc.path = path;

//				if (String.IsNullOrEmpty(CurrentDoc.title))
//					CurrentDoc.title = Path.GetFileName(path);

//				if (mMode == ConvertedPDFSaveMode.UpdateDocument)
//				{
//					docs.Add(CurrentDoc);

//				}
//				else
//				{
//					CurrentDoc.id_event = EventIdController.GetEventId(en_Events.Scan);

//					string bkup = CurrentDoc.title;
//					if (mSeparate)
//						CurrentDoc.title = CurrentDoc.title_without_ext + "-" + i + CurrentDoc.extension;
						
//					docs.Add(CurrentDoc);

//					CurrentDoc.title = bkup;
//				}

//					//check name
//				if (!allow_duplicatedName && DocumentController.IsDocumentExists(CurrentDoc.id_folder, CurrentDoc.filenameWithExt))
//					CurrentDoc.title = DocumentController.GetNameAvailable(CurrentDoc.id_folder, CurrentDoc.title.Trim(), ".pdf");

//				i++;
//			}

//			var args = new ImgPDFSaverArg() { status = status, errorThread = errorThread };
//			if (onBeforeSave != null)
//				onBeforeSave(docs, args);

//			errorThread = args.errorThread;
//			status = args.status;

//			if (errorThread) return;

//			foreach (var doc in docs)
//			{
//				string error = string.Empty;

//				if (mMode == ConvertedPDFSaveMode.UpdateDocument)
//				{
//					error = doc.AddVersion(m_userId, m_localDb);

//				}
//				else
//				{
//					error = doc.AddDocument(m_userId, m_localDb);
//				}

//				if (error != "")
//				{ 
//					logger.Error(error);
//					errorThread = true;
//				}
//			}
//		}

////---------------------------------------------------------------------------------
//// General methods ----------------------------------------------------------------
////---------------------------------------------------------------------------------
//		protected void Cancelled(object arg)
//		{
//			if(onCancelled != null)
//				onCancelled();
//		}

////---------------------------------------------------------------------------------
//		void thread_WorkDone(object sender, RunWorkerCompletedEventArgs e)
//		{
//			errorThread = (e.Error != null);
//			WorkDone();
//		}

////---------------------------------------------------------------------------------
//		protected void WorkDone()
//		{
//			if(errorThread == false)
//			{
//				if(status == en_status.Aborting)
//					status = en_status.Aborted;
//				else
//					status = en_status.Finish;
//			}
//			else
//			{
//				status = en_status.Error;
//			}

//			updateProgress();
//			if(onCompleted != null)
//				onCompleted(!errorThread);
//		}

//        public void StateFinish()
//        {
//            WorkDone();
//        }
////---------------------------------------------------------------------------------
//		protected void updateProgress()
//		{
//			_StatusMessage = tb_message[(int)status];

//			switch(status)
//			{
//			case en_status.None:
//			case en_status.Error:
//			case en_status.SaveFile:
//			case en_status.Aborted:
//				AllPages = 0;
//				CurrentPage = 0;

//				_ProgBarVal = 0;
//				_ShowProgBar = false;
//				_ShowAbort = false;
//				break;

//			case en_status.Finish:
//				AllPages = 0;
//				CurrentPage = 0;

//				_ProgBarVal = 100;
//				_ShowProgBar = true;
//				_ShowAbort = false;
//				break;

//			default:
//				int percentage = 0;

//				if((0 < AllPages) && (0 < CurrentPage))
//				{
//					if(AllPages < CurrentPage)
//						CurrentPage = AllPages;

//					_StatusMessage += " (pag " + CurrentPage.ToString() + " of " + AllPages.ToString() + ")";

//					percentage = (int)(((float)(CurrentPage - 1) / (float)AllPages) * 100f);
//					if(100 < percentage)
//						percentage = 100;
//				}

//				_ProgBarVal = percentage;
//				_ShowProgBar = true;
//				_ShowAbort = true;
//				break;
//			}
//		}

////---------------------------------------------------------------------------------
//	}
//}
