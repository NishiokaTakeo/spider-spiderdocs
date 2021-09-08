using System;
using System.IO;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public enum en_DocumentIdType
	{
		Doc = 0,
		Version,

		Max
	}
	
	public enum en_file_Status
	{
		invalid = -1,
		checked_in = 1,
		checked_out,
		readOnly,
		archived,
		deleted,

		etc,

		Max
	}

	public enum en_file_Sp_Status
	{
		invalid = -1,
		normal = 1,
		review,
		review_overdue,

        Max
	}

	public enum en_Actions
	{
        None = 0,
		CheckIn_Out = 1,
		OpenRead,
		Export,
		SendByEmail,
		Properties,
		Rollback,
		Delete,
		Archive,
		CancelCheckOut,
		Review,
        UnArchive,

        Max,

		ImportNewVer = CheckIn_Out * 100,		// Permission ID is same as 'CheckIn_Out'
		CheckIn_Out_Foot,						// Permission ID is same as 'CheckIn_Out'

		Export_PDF = Export * 100,				// Permission ID is same as 'Export'

		SendByEmail_PDF = SendByEmail * 100,	// Permission ID is same as 'SendByEmail'

		Max2
	}

    //---------------------------------------------------------------------------------
    public class DocumentBase : DocumentPropertyBase
    {
        //document
        public int id { get; set; }
        public int id_user { get; set; }
		public bool hasAccepted { get; set; } = false;
        new public int id_docType
        {
            set
            {
                if(base.id_docType != value)
                {
                    base.id_docType = value;
                    _name_docType = "";
                }
            }

            get { return base.id_docType; }
        }

        protected string _name_docType = string.Empty;

        new public int id_folder
        {
            set
            {
                if(base.id_folder != value)
                {
                    base.id_folder = value;
                    _name_folder = "";
                }
            }

            get { return base.id_folder; }
        }

        protected string _name_folder = string.Empty;
        public String author { get; set; }
        public en_file_Status id_status { get; set; }
        public en_file_Sp_Status id_sp_status { get; set; }
        public int id_review { get; set; }
        public int id_checkout_user { get; set; }
        public int id_latest_version { get; set; }
        public int[] id_notification_group { get; set; } = new int[] { };
		//document version
		protected Byte[] _filedata;
		public Byte[] filedata
		{
			get { return _filedata; }
			set
			{
				_filedata = value;
				size = 0;

				if(_filedata != null)
					size = _filedata.Length;
			}
		}

		public int version { get; set; }
		public long size { get; set; }
		public string reason { get; set; }

		//document historic
		public int id_version { get; set; }
		public int id_event { get; set; }
		public DateTime date { get; set; }
		public string comments { get; set; }

		//phisical path
		public string path { get; set; }

		protected string _title;

//---------------------------------------------------------------------------------
		public string filenameWithExt
		{
			get
			{
				if(!String.IsNullOrEmpty(path))
					return Path.GetFileName(path);
				else
					return "";
			}
		}

//---------------------------------------------------------------------------------
		public string filename
		{
			get
			{
				if(!String.IsNullOrEmpty(path))
					return Path.GetFileNameWithoutExtension(path);
				else
					return "";
			}
		}

//---------------------------------------------------------------------------------
		protected string _extension;
		public string extension
		{
			get
			{
				if(_extension != null)
					return _extension;
				else if(!String.IsNullOrEmpty(path))
					return Path.GetExtension(path).ToLower();
				else
					return "";
			}
			set
			{
				_extension = value;
			}
		}

//---------------------------------------------------------------------------------
		public DocumentBase()
		{
			id = -1;
			id_user = -1;
			id_status = en_file_Status.invalid;
			id_sp_status = en_file_Sp_Status.invalid;
			id_review = -1;	
			id_checkout_user = -1;
			id_latest_version = -1;
			version = -1;
			size = -1;
			id_version = -1;
			id_event = -1;
		}

//---------------------------------------------------------------------------------
	}
}
