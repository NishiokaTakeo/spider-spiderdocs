using System;
using System.Collections.Generic;
using System.IO;
using NLog;
using System.Linq;


//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public partial class Document : DocumentProperty
	{
        static Logger logger = LogManager.GetCurrentClassLogger();


        //---------------------------------------------------------------------------------
        readonly static int[, ,] tb_DocumentCondition = new int[(int)en_file_Sp_Status.Max - 1, (int)en_file_Status.Max - 1, (int)en_Actions.Max - 1]
		{
			// 0:restrict, 1: accept
			// normal
			{					    /*CheckIn_Out OpenRead Export SendByEmail Properties Rollback Delete Archive CancelCheckOut Review UnArchive*/
				/* checked_in  */	{ 1,          1,       1,     1,          1,         1,       1,     1,      0,             1	    ,   0	},
				/* checked_out */	{ 1,          1,       1,     1,          0,         0,       0,     0,      1,             1	    ,   0	},
				/* readOnly    */	{ 1,          1,       1,     1,          1,         1,       1,     1,      0,             1       ,   0   },
				/* archived    */	{ 0,          1,       1,     1,          0,         0,       0,     0,      0,             1       ,   1   },
				/* deleted     */	{ 1,          1,       1,     1,          1,         1,       1,     1,      0,             1       ,   0   },
				/* etc         */	{ 1,          1,       1,     1,          1,         1,       1,     1,      0,             1       ,   0   },
            },
			// review
			{						/*CheckIn_Out OpenRead Export SendByEmail Properties Rollback Delete Archive CancelCheckOut Review UnArchive*/
				/* checked_in  */	{ 1,          1,       1,     1,          0,         1,       0,     0,      0,             1       ,   0   },
				/* checked_out */	{ 1,          1,       1,     1,          0,         0,       0,     0,      1,             1       ,   0   },
				/* readOnly    */	{ 1,          1,       1,     1,          0,         1,       0,     0,      0,             1       ,   0   },
				/* archived    */	{ 0,          1,       1,     1,          0,         0,       0,     0,      0,             1       ,   0   },
				/* deleted     */	{ 1,          1,       1,     1,          0,         1,       0,     0,      0,             1       ,   0   },
				/* etc         */	{ 1,          1,       1,     1,          0,         1,       0,     0,      0,             1       ,   0   },
            },
			// review_overdue
			{						/*CheckIn_Out OpenRead Export SendByEmail Properties Rollback Delete Archive CancelCheckOut Review UnArchive*/
				/* checked_in  */	{ 1,          1,       1,     1,          0,         1,       0,     0,      0,             1       ,   0   },
				/* checked_out */	{ 1,          1,       1,     1,          0,         0,       0,     0,      1,             1       ,   0   },
				/* readOnly    */	{ 1,          1,       1,     1,          0,         1,       0,     0,      0,             1       ,   0   },
				/* archived    */	{ 0,          1,       1,     1,          0,         0,       0,     0,      0,             1       ,   0   },
				/* deleted     */	{ 1,          1,       1,     1,          0,         1,       0,     0,      0,             1       ,   0   },
				/* etc         */	{ 1,          1,       1,     1,          0,         1,       0,     0,      0,             1       ,   0   },
            }
        };

		public String name_docType
		{
			get
			{
                try
                {
                    if (String.IsNullOrEmpty(_name_docType) && (0 < this.id_docType))
				    {

					    DocumentType wrk = DocumentTypeController.DocumentType(this.id_docType);
						_name_docType = wrk?.type;
				    }
                }
                catch { }

                return _name_docType;
			}
			set
			{
				_name_docType = value;
			}
		}

//---------------------------------------------------------------------------------
		public String name_folder
		{
			get
			{
                try
                {

                    if (String.IsNullOrEmpty(_name_folder) && (0 < this.id_folder))
                    {
                        Folder wrk = FolderController.GetFolder(this.id_folder);
                        if (wrk != null)
                            _name_folder = wrk.document_folder;
                    }
                    else
                    {
                        _name_folder = string.Empty;
                    }

                }
                catch { }

                return _name_folder;
			}
			set
			{
				_name_folder = value;
			}
		}

//---------------------------------------------------------------------------------
		public string PathWithVersionIdFolder
		{
			get
			{
				if(!String.IsNullOrEmpty(this.title))
					return this.id_version.ToString() + "\\" + this.title.ToString();
				else
					return "";
			}
		}

//---------------------------------------------------------------------------------
		public string title
		{
			get
			{
				if(_title != null)
					return _title;
				else if(!String.IsNullOrEmpty(path))
					return Path.GetFileName(path);
				else
					return "";

			}
			set
			{
				_title = ToValidTitle(value);
			}
		}

		public string title_without_ext
		{
			get { return Path.GetFileNameWithoutExtension(title); }
		}

//---------------------------------------------------------------------------------
		public void SetCreationDate()
		{
			base.SetCreationDate(path);
		}

//---------------------------------------------------------------------------------
		public static string ToValidTitle(string title)
		{

            if (title == null) return string.Empty;

            title = title
                        .Replace("\\", "")
                        .Replace("/", "")
                        .Replace(":", "")
                        .Replace("*", "")
                        .Replace("<", "")
                        .Replace(">", "")
                        .Replace("|", "")
                        .Replace("?", "")
                        //.Replace("''", "")
                        //.Replace("~", "")
                        .Replace("\"", "-")
                        //.Replace("'", "")
                        //.Replace("`", "")
                        .Replace("\n", "")
                        .Replace("\r", "");

			return title;
		}


        //---------------------------------------------------------------------------------
        bool IsActionAllowed(en_file_Sp_Status sp_status, en_file_Status status, en_Actions action)
        {
            if (sp_status == en_file_Sp_Status.invalid || status == en_file_Status.invalid) return false;

            if (tb_DocumentCondition[(int)sp_status - 1, (int)status - 1, (int)action - 1] == 1)
                return true;
            else
                return false;
        }

        //---------------------------------------------------------------------------------
        virtual public bool IsActionAllowed(en_Actions action, int user_id, Dictionary<en_Actions, en_FolderPermission> permissions = null)
        {
            permissions = permissions ?? new Dictionary<en_Actions, en_FolderPermission>();
            int permissionId;

            // check user's permmision
            if (en_Actions.Max <= action)
                permissionId = (int)action / 100;
            else
                permissionId = (int)action;

            if (logger.IsDebugEnabled) logger.Debug("Permission ID:" + permissionId);

            // true if you have right permision ( check whether your user_id or your gorup id has permission or not.
            bool hasPer = permissions.Count() > 0 ? permissions[(en_Actions)permissionId] == en_FolderPermission.Allow : PermissionController.CheckPermission(this.id_folder, (en_Actions)permissionId, user_id);
            if (!hasPer) return false;

            if (!IsActionAllowed(this.id_sp_status, this.id_status, (en_Actions)permissionId))
            {
                if (logger.IsDebugEnabled) logger.Debug("Action is not allowed: IsActionAllowed({0}, {1}, {2})", this.id_sp_status, this.id_status, (en_Actions)permissionId);

                return false;
            }

            switch (this.id_sp_status)
            {
                case en_file_Sp_Status.normal:
                        // Nothing to do.
                    break;

                case en_file_Sp_Status.review:
                case en_file_Sp_Status.review_overdue:
                    switch (action)
                    {
                        case en_Actions.CheckIn_Out:
                        case en_Actions.ImportNewVer:
                        case en_Actions.CheckIn_Out_Foot:
                        case en_Actions.Rollback:
                        case en_Actions.CancelCheckOut:
                            Review review = ReviewController.GetReview(id);

                            if (!review.allow_checkout || (review.review_users.Count(a => a.id_user == user_id) <= 0))
                                return false;
                            break;
                    }
                    break;
            }

            switch (action)
            {
                case en_Actions.ImportNewVer:
                    if (this.id_status == en_file_Status.checked_out)
                        return false;
                    break;
            }

            return true;
        }
        public bool isArchived()
        {
            return (this.id_status == en_file_Status.archived || FolderController.isArhived(this.id_folder));
        }

        public virtual bool isNotDuplicated(bool careOfcnv = false)
        {
			var tmp = new Document(); //use this class. title is converted when is is set.
			tmp.title = this.title;

			string atbs = string.Join(",",this.Attrs.Select(x => x.id));

			string atb_vals = string.Join(",", this.Attrs.Select(x => DocumentAttribute.IsComboTypes(x.id_type) ? x.atbValue_str.Replace("'",""): x.atbValue));

			if(careOfcnv)
				tmp.title = ViewController.ConvertTitle(tmp.title);

            int dup_count = StoredProcedureController.hasDuplicate(tmp.title, this.id_docType, atbs, atb_vals,this.id);

            return dup_count <= 1;
        }

		//public virtual bool WarnForDupliate(bool careOfcnv = false)
		//{
		//	/*if (this.id_docType > 0) return false;*/

		//	var tmp = new Document(); //use this class. title is converted when is is set.
		//	tmp.title = this.title;

		//	if (careOfcnv)
		//		tmp.title = ViewController.ConvertTitle(tmp.title);

  //          int dup_count = StoredProcedureController.hasWarnDuplicate(tmp.title, id_folder, this.id);

  //          return dup_count > 0;
		//}

        //---------------------------------------------------------------------------------
        // methods for operations ---------------------------------------------------------
        //---------------------------------------------------------------------------------
        // New document
        public string AddDocument(int userId, bool localDb, DateTime? newdate = null)
		{
			string ans = "";
			SqlOperation sql = new SqlOperation();

			// Initialize document detais
			this.id_status = en_file_Status.checked_in;
			this.id_sp_status = en_file_Sp_Status.normal;
			this.id_user = userId;
			this.Load();
			this.version = 1;
			this.date = (this.date == DateTime.MinValue || this.date == null ? DateTime.Now : this.date);

            if (this.id_event <= 0)
				this.id_event = EventIdController.GetEventId(en_Events.Created);

			try
			{
				sql.BeginTran();

                this.title = ViewController.ConvertTitle(this.title);

				DocumentController<Document>.AddDocument(sql, localDb, this);
				int id_historic = DocumentController<Document>.AddVersion(sql, userId, localDb, this);
                //DocumentNotificationGroupController.SaveOne(sql, this.id, this.id_notification_group);

                //this.UpdateLinkedAttribute();

                if (newdate != null)
                {
                    HistoryController.UpdateHistricTime(sql, id_historic, (DateTime)newdate);
                }

                sql.CommitTran();

			}
			catch(Exception error)
			{
                logger.Error(error);
				sql.RollBack();
				ans = error.Message;
			}

			return ans;
		}

        public string UpdateDocumentVersion(int userId, bool localDb)
        {
            string ans = "";
            SqlOperation sql = new SqlOperation();

            if (this.id_event <= 0)
                this.id_event = EventIdController.GetEventId(en_Events.UpVer);

            try
            {
                sql.BeginTran();

                this.Load();

                ans = DocumentController<Document>.UpdateVersion(sql, userId, localDb, this).ToString();

                sql.CommitTran();
            }
            catch (Exception error)
            {
                logger.Error(error);
                sql.RollBack();
                ans = error.Message;
            }

            return ans;
        }

        //---------------------------------------------------------------------------------
        // Update a document version
        virtual public string AddVersion(int userId, bool localDb, DateTime? newdate = null)
		{
			string ans = "";
			SqlOperation sql = new SqlOperation();

			this.Load();
			this.version++;

			if(this.id_event <= 0)
				this.id_event = EventIdController.GetEventId(en_Events.SaveNewVer);

			this.id_user = userId;

			try
			{
				sql.BeginTran();
				int id_histric = DocumentController<Document>.AddVersion(sql, userId, localDb, this); //return id_histric

                if(newdate != null)
                {
                    HistoryController.UpdateHistricTime(sql, id_histric, (DateTime)newdate);
                }

                sql.CommitTran();

                DocumentController<Document>.ScheduleNotification(userId, localDb, this);

            }
            catch (Exception error)
			{
                logger.Error(error);
				sql.RollBack();
				ans = error.Message;
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		// Update a document property
		public string UpdateProperty(int userId, bool add_history_log = true)
		{
			string ans = "";
			Document original_doc = DocumentController<Document>.GetDocument(this.id);

			//update documents fields
			if((original_doc.title != this.title) || !PropertyCompare(this, original_doc))
			{
				SqlOperation sql = new SqlOperation();

				if(this.id_event <= 0)
					this.id_event = EventIdController.GetEventId(en_Events.Property);

				this.id_user = userId;

				try
				{
					// if Document type has been changed
					if(original_doc.id_docType != this.id_docType)
						DocumentTypeController.RemoveAttributeFromDocType(this.id);	// Delete all attributes belonging the document id

					sql.BeginTran();
					DocumentController<Document>.InsertOrUpdateDocument(sql, this, false);

                    // Update Document Notification Group
                    //DocumentNotificationGroupController.SaveOne(sql, this.id,this.id_notification_group);

					sql.CommitTran();

				}
				catch(Exception error)
				{
                    logger.Error(error);
					sql.RollBack();
					ans = error.Message;
				}

				if(add_history_log && String.IsNullOrEmpty(ans))
				{
                    System.Threading.Thread thread1 = new System.Threading.Thread(() => HistoryController.SaveDocumentHistoric(sql, userId, this));
                    thread1.Priority = System.Threading.ThreadPriority.Lowest;
                    thread1.Start();

                    System.Threading.Thread thread2 = new System.Threading.Thread(() => SaveHistory(userId, original_doc));
                    thread2.Priority = System.Threading.ThreadPriority.Lowest;
                    thread2.Start();
                }
			}

			return ans;
		}

        /// <summary>
        /// Update linked attributes
        /// </summary>
        /// <returns></returns>
        public string UpdateLinkedAttribute()
        {
			string ans = "";

            try
            {
                foreach(var attr in  this.Attrs)
                {
                    // Delete/Insert linked attributes if linked value is filled
                    if( attr.LinkedAttr?.id > 0

                        &&

                        false == string.IsNullOrWhiteSpace(attr.LinkedAttr?.atbValue_str))
                    {
                        // Insert new linked value if not exists.
                        var linkedAttr = DocumentAttributeController.GetLinkedAttribute(attr.id, attr.atbValue_str ) ;
                        if( linkedAttr == null)
                        {
                            DocumentAttributeController.InsertLinkedAttribute(attr.id,attr.atbValue_str, attr.LinkedAttr.id, attr.LinkedAttr.atbValue_str);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }

            return ans;
        }


        void SaveHistory(int userId, Document original_doc)
        {
            //insert recent document
            UserController.SaveDocumentRecent(null, userId, this.id);

            SqlOperation sql = new SqlOperation();
            sql.BeginTran();

            // Add historic log of changing title
            if (original_doc.title != this.title)
            {
                this.id_event = EventIdController.GetEventId(en_Events.ChgName);
                this.comments = "(" + original_doc.title + " -> " + this.title + ")";
                this.comments = this.comments.Replace("'", "''");

                //insert historic
                HistoryController.SaveDocumentHistoric(sql, userId, this);
            }

            // Add historic log of changing folder
            if (original_doc.id_folder != this.id_folder)
            {
                this.id_event = EventIdController.GetEventId(en_Events.ChgFolder);

                string Origin_FolderName = FolderController.GetFolder(original_doc.id_folder).document_folder;
                string New_FolderName = FolderController.GetFolder(this.id_folder).document_folder;

                this.comments = "(" + Origin_FolderName + " -> " + New_FolderName + ")";
                this.comments = this.comments.Replace("'", "''");

                //insert historic
                HistoryController.SaveDocumentHistoric(sql, userId, this);
            }

            //update attributes
            // if Document type has been changed
            if (original_doc.id_docType != this.id_docType)
            {
                string Origin_DocTypeName = DocumentTypeController.DocumentType(original_doc.id_docType)?.type;
                string New_DocTypeName = DocumentTypeController.DocumentType(this.id_docType)?.type;

                this.id_event = EventIdController.GetEventId(en_Events.ChgDT);
                this.comments = "(" + Origin_DocTypeName + " -> " + New_DocTypeName + ")";
                this.comments = this.comments.Replace("'", "''");

                //insert historic
                HistoryController.SaveDocumentHistoric(sql, userId, this);

				if(hasAccepted)
					HistoryController.SaveDocumentHistoric(null, new Document() { id_event = (int)en_Events.DupOK, id_version = id_version, comments = Library.cmt_duplication_ok });

			}
            else if (!DocumentAttribute.CompareAttributes(this.Attrs, original_doc.Attrs))
            {
                this.id_event = EventIdController.GetEventId(en_Events.ChgAttr);
                this.comments = "";

                //insert historic
                HistoryController.SaveDocumentHistoric(sql, userId, this);
            }

            sql.CommitTran();
        }

        public bool Archive(int userId)
        {

            if (this.IsActionAllowed(en_Actions.Archive, userId))
            {
                SqlOperation sql = new SqlOperation();

                try
                {
                    if ((this.id_sp_status == en_file_Sp_Status.review)
                    || (this.id_sp_status == en_file_Sp_Status.review_overdue))
                    {
                        Review review = new Review(this.id);
                        review.FinalizeReview(userId);
                    }

                    this.id_status = en_file_Status.archived;
                    this.id_event = EventIdController.GetEventId(en_Events.Archive);

                    sql.BeginTran();
                    DocumentController.InsertOrUpdateDocument(sql, this, false);
                    HistoryController.SaveDocumentHistoric(sql, this);
                    sql.CommitTran();

                    return true;

                }
                catch (Exception error)
                {
                    sql.RollBack();
                    logger.Error(error);

                    return false;
                }
            }

            return false;
        }

        public bool UnArchive(int userId)
        {

            if (this.IsActionAllowed(en_Actions.UnArchive, userId))
            {
                SqlOperation sql = new SqlOperation();

                try
                {


                    this.id_status = en_file_Status.checked_in;
                    this.id_event = EventIdController.GetEventId(en_Events.CanceledArchive);

                    sql.BeginTran();
                    DocumentController.InsertOrUpdateDocument(sql, this, false);
                    HistoryController.SaveDocumentHistoric(sql, this);
                    sql.CommitTran();

                    return true;

                }
                catch (Exception error)
                {
                    sql.RollBack();
                    logger.Error(error);

                    return false;
                }
            }

            return false;
        }

        // Change status
        public void ChangeStatus(en_file_Status status = en_file_Status.invalid, en_file_Sp_Status sp_status = en_file_Sp_Status.invalid)
		{
			if(status != en_file_Status.invalid)
				this.id_status = status;

			if(sp_status != en_file_Sp_Status.invalid)
				this.id_sp_status = sp_status;

			DocumentController<Document>.InsertOrUpdateDocument(null, this, false);
		}

//---------------------------------------------------------------------------------
		public bool CheckOut(int userId, bool footer, string path_newName = "")
		{
			bool ans = false;
			SqlOperation sql = new SqlOperation();

			if((footer && IsActionAllowed(en_Actions.CheckIn_Out_Foot, userId)) // If an user selected Checkout with footer, check the file extension
			|| (IsActionAllowed(en_Actions.CheckIn_Out, userId))) // If an user selected Checkout WITHOUT footer or the file which extension is not supported the footer, check out the file without footer anyway
			{
				try
				{
					if(!String.IsNullOrEmpty(path_newName))
					{
						DocumentController.LoadBinaryData(this);
						this.Save(path_newName);
					}

					sql.BeginTran();

					this.id_status = en_file_Status.checked_out;
					this.id_checkout_user = userId;
                    this.id_latest_version = DocumentController.GetDocument(this.id).id_latest_version;

					DocumentController<Document>.InsertOrUpdateDocument(sql, this, false);

					//insert historic
					this.id_event = EventIdController.GetEventId(en_Events.Chkout);
					HistoryController.SaveDocumentHistoric(sql, userId, this);

					//add to recent files
					UserController.SaveDocumentRecent(sql, userId, this.id);

					sql.CommitTran();
					ans = true;
				}
				catch(Exception error)
				{
                    logger.Error(error);
					sql.RollBack();

					throw error;
				}
			}

			return ans;
		}
        public bool CancelCheckOut()
        {
            logger.Trace("Begin");

            //open connection
            SqlOperation sql = new SqlOperation();
            sql.BeginTran();

            try
            {
                this.id_event = EventIdController.GetEventId(en_Events.Cancel);
                this.id_status = en_file_Status.checked_in;
                this.id_checkout_user = -1;

                DocumentController.InsertOrUpdateDocument(sql, this, false);
                HistoryController.SaveDocumentHistoric(sql, this);

                //commit transaction
                sql.CommitTran();

                return true;
            }
            catch (Exception error)
            {
                sql.RollBack();
                logger.Error(error);
                throw error;
            }
        }

//---------------------------------------------------------------------------------
		public string Open(int userId)
		{
			string ans = "";

			//if(!IsActionAllowed(en_Actions.OpenRead, userId))
			//	return ans;

			try
			{
				//get file from the databse and save in the temp folder
				string tempPath = FileFolder.GetTempFolder() + this.PathWithVersionIdFolder;

				//check if the file is already open, if so, get another name to open again
				string path = FileFolder.GetPath(tempPath);
				Directory.CreateDirectory(path);
				FileFolder.DeleteFiles(path);
				ans = tempPath;

				//save from databse to hard drive
				DocumentController<Document>.LoadBinaryData(this);
				this.Save(ans);

				//insert historic
				this.id_event = EventIdController.GetEventId(en_Events.Read);
				HistoryController.SaveDocumentHistoric(null, userId, this);

			}
			catch(Exception error)
			{
                logger.Error(error);
			}

			return ans;
		}

//---------------------------------------------------------------------------------
		public void Export(string path, int userId)
		{
			this.id_event = EventIdController.GetEventId(en_Events.Exp);
			HistoryController.SaveDocumentHistoric(null, userId, this);

			if(this.filedata == null)
				DocumentController.LoadBinaryData(this);

			this.Save(path);
		}

//---------------------------------------------------------------------------------
		public void Load(string file_path = "")
		{
			if(!String.IsNullOrEmpty(file_path))
				this.path = file_path;

            try
            {
                using (var fileHandle = ZetaLongPaths.ZlpIOHelper.CreateFileHandle(
                                            this.path,
                                            ZetaLongPaths.Native.CreationDisposition.OpenExisting,
                                            ZetaLongPaths.Native.FileAccess.GenericRead,
                                            ZetaLongPaths.Native.FileShare.Read | ZetaLongPaths.Native.FileShare.Write))
                using (FileStream fs = new System.IO.FileStream(fileHandle, System.IO.FileAccess.Read))
                {
                    BinaryReader br = new BinaryReader(fs);

                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    br.Close();
                    fs.Close();

                    this.filedata = bytes;

                    fileHandle.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            //FileStream fs = new FileStream(this.path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //BinaryReader br = new BinaryReader(fs);

            //Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            //br.Close();
            //fs.Close();

            //this.filedata = bytes;
        }

//---------------------------------------------------------------------------------
		public bool Save(string path = "")
		{
			bool ans = false;

			if(!String.IsNullOrEmpty(path))
				this.path = path;

			FileInfo fileinfo = new FileInfo(this.path);
			if(fileinfo.Exists)
			{
				try
				{
					File.SetAttributes(this.path, FileAttributes.Normal);
					File.Delete(this.path);

				} catch {}

			}else if(!Directory.Exists(FileFolder.GetPath(this.path)))
			{
				Directory.CreateDirectory(FileFolder.GetPath(this.path));
			}

			try
			{
				FileStream fs = new FileStream(this.path, FileMode.OpenOrCreate, FileAccess.Write);
				fs.Write(this.filedata, 0, this.filedata.Length);
				fs.Flush();
				fs.Close();

				ans = true;

			} catch {}

			return ans;
		}

        public bool Remove(string reason, int userId)
        {

            bool ans = true;

            ans = DocumentController.DeleteDocument(userId, this, reason);

            return ans;
        }

//---------------------------------------------------------------------------------

	}
}
