using System;
using System.Collections.Generic;
using NLog;
using System.Linq;
using System.IO;
using System.Threading;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class WorkSpaceFile : ICloneable
	{
        static Logger logger = LogManager.GetLogger("Sync");

        public string _userFolder = "";

        private int _linkid = 0;
        private string _fileHash = string.Empty;
        private string _filePath = "";

		/// <summary>
		/// True if status in spider docs is neither Deleted and Archived. otherwiese False.
		/// Null means not checked yet.
		/// </summary>
		private bool? _isActiveInSpiderDocs;

        string ShortName {get {return this.FullName.Replace(this._userFolder,"");}}
        public string FileName { get; set; } = "";
        public int DocVersion { get; set; } = 0;
        public string FullName { get { return _filePath; } }

        public int LinkId
        {
            get
            {
                if (_linkid > 0) return _linkid;

                int id = GetLinkIDFromLocal();

                return id;
            }

        }

        System.Threading.Tasks.Task<string> _taskGethash = null;
		System.Threading.Tasks.Task<List<Tuple<int, int>>> _taskCheckActiveDoc = null;

		public string RootPath
        {
            get
            {
                return _filePath == "" ?  "" : System.IO.Directory.GetParent(_filePath).FullName; // Path does not include \\
            }
        }

//---------------------------------------------------------------------------------
		//public WorkSpaceFile()
		//{

		//}

        public enum en_Status
        {
            None,
            IsInitial,
            NoDiff,
            IsUpdatedByRemote,
            IsUpdatedOnlyNameByRemote,
            IsUpdatedByLocal,
            IsUpdatedOnlyNameByLocal,
            CheckOutFinished
        }

        public WorkSpaceFile(string filePath, string userFolder)
        {
            if (!System.IO.File.Exists(filePath)) return;

            //logger.Trace("[Begin] {0}, {1}", filePath, userFolder);

            _userFolder = userFolder;
            _filePath = filePath;

            this._taskGethash = System.Threading.Tasks.Task.Run(() => this._GetFileHash());

            Extract();

			this._taskCheckActiveDoc = DocumentController.ExistAsync(new int[] { DocVersion });
		}

        public WorkSpaceFile(UserWorkSpace rfile, string userFolder)
        {
            this.SetLinkID(rfile.id);
            this.DocVersion = rfile.id_version;

            // id_version >== 0 means new file.
            var verstr = rfile.id_version > 0 ? rfile.id_version.ToString() : this.GetVerZeroFolderName();

            _filePath = userFolder + "\\" + verstr + "\\" + rfile.filename;

            if (System.IO.File.Exists(_filePath))
                this._taskGethash = System.Threading.Tasks.Task.Run(() => this._GetFileHash());

			this._taskCheckActiveDoc = DocumentController.ExistAsync(new int[] { DocVersion });

			_userFolder = userFolder;
        }

        /// <summary>
        /// Find a work space file that links linkId of user_workspace table.
        /// This needs reverse search from remote to local.
        /// </summary>
        /// <param name="root"></param>
        public static string FindPathBy(int linkId, string userFolderPath )
        {
            var ptah = userFolderPath;
            var dir = new ZetaLongPaths.ZlpDirectoryInfo(ptah);

            // Import
            var files = dir.GetFiles(string.Format("{0}.user_workspace.sd", linkId), System.IO.SearchOption.AllDirectories);
            if( files.Count() > 1)
            {
                logger.Warn("[LinkID] Found multiple. Should be one. {0}", linkId);
            }

            foreach (var f in files)
            {
                var baseFolder = new ZetaLongPaths.ZlpDirectoryInfo(System.IO.Directory.GetParent(f.FullName));

                var file = baseFolder.GetFiles("*", System.IO.SearchOption.TopDirectoryOnly).First(x => !x.Name.Contains(".user_workspace.sd") && x.Name != f.Name);

                return file.FullName;
            }

            return string.Empty;
        }

        /// <summary>
        /// Find link id that is same filename and version folder.
        /// </summary>
        /// <param name="userFolderPath"></param>
        /// <returns></returns>
        public int[] FindDuplicate(string userFolderPath)
        {
            var ptah = userFolderPath;
            var dir = new ZetaLongPaths.ZlpDirectoryInfo(ptah);
            List<int> dup = new List<int>();

            // Import
            foreach (var fle in dir.GetFiles(this.FileName, System.IO.SearchOption.AllDirectories))
            {
                var baseFolder = new ZetaLongPaths.ZlpDirectoryInfo(System.IO.Directory.GetParent(fle.FullName));

                if (baseFolder.Name == this.DocVersion.ToString() )
                {
                    foreach (var file in baseFolder.GetFiles("*.user_workspace.sd", System.IO.SearchOption.TopDirectoryOnly))
                    {
                        var id = int.Parse(file.Name.Split('.').First());
                        if (id != this.LinkId)
                        {
                            dup.Add(id);
                        }
                    }
                }
            }

            return dup.ToArray();
        }

        public string Link2Table(int id)
        {
            if( !System.IO.File.Exists(_filePath))
            {
                logger.Error("File is not exists:{0}",_filePath);

                return "";
            }

            var linkPath = RootPath + string.Format("\\{0}.user_workspace.sd", id);

            if (!System.IO.File.Exists(linkPath))
            {
                using (System.IO.File.Create(linkPath)) ;
            }

            System.IO.File.SetCreationTime(linkPath, DateTime.UtcNow);

            return linkPath;
        }


        /// <summary>
        /// Find a id of user workspace
        /// </summary>
        /// <returns>  0 > :id of the table. 0 = : sync failure or has no synced</returns>
        public void SetLinkID(int linkId = 0)
        {
            _linkid = linkId;
        }

        public void SetLinkId(int id)
        {
            _linkid = id;
        }

        public int GetLinkId()
        {
            if (_linkid > 0) return _linkid;

            int id = GetLinkIDFromLocal();

            return id;
        }

        /// <summary>
        /// Find a id of user workspace only from local file
        /// </summary>
        /// <returns>  0 > :id of the table. 0 = : sync failure or has no synced</returns>
        public int GetLinkIDFromLocal()
        {
            try
            {
                // Root path should contain version folder. If this path is same user folder then the file is initial. Shuld return zero.
                if( RootPath.TrimEnd(new char[] { '\\' }) == _userFolder.TrimEnd(new char[] { '\\' }))
                {
                    return 0;
                }

                int id;
                var ptah = RootPath;
                var dir = new ZetaLongPaths.ZlpDirectoryInfo(ptah);

                // Import
                foreach (var f in dir.GetFiles("*.user_workspace.sd", System.IO.SearchOption.AllDirectories))
                {
                    string idOrEmpty = f.Name.Split('.').FirstOrDefault() ?? "";

                    if (int.TryParse(idOrEmpty, out id))
                        return id;
                    else
                        return 0;
                }
            }
            catch( Exception ex)
            {
                logger.Error(ex);
            }

            return 0;
        }

        /// <summary>
        /// Rename file and sync
        /// </summary>
        /// <param name="reName"></param>
        public bool RenameFromRemote(UserWorkSpace remote)
        {
            try
            {
                System.IO.File.Move(this.FullName, RootPath + "\\" + remote.filename);

                System.IO.File.SetLastWriteTimeUtc(RootPath + "\\" + remote.filename, remote.lastmodified_date);

                return true;
            }
            catch(Exception ex)
            {
                logger.Error(ex,"{0} to {1}",this.FullName, RootPath + "\\" + remote.filename);
            }

            return false;
        }

        /// <summary>
        /// Rename file and sync
        /// </summary>
        /// <param name="reName"></param>
        public bool RenameTo(string reName = "")
        {
            try
            {
                this.UpdateLastModified();

                if ( string.IsNullOrWhiteSpace(reName)) reName = this.FileName;
                if( FileName != reName) System.IO.File.Move(_filePath, RootPath + "\\" + reName);

                WorkSpaceSyncController.Rename(this.GetLinkId(), reName, WorkSpaceFile.NowUTC());

                return true;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }

            return false;
        }

		/// <summary>
		/// Updates last accessed date of this file. (remote only)
		/// </summary>
		public void UpdateLastAccess()
		{
			WorkSpaceSyncController.UpdateLastAccess(this.LinkId, DateTime.UtcNow);
		}

		/// <summary>
		/// Updates last modified date of this file.
		/// Rename does not update modified date. WHen update last modified date then will sync this infomation with caring which location(remote/local) did renaming/
		/// </summary>
		public void UpdateLastModified()
        {
            System.IO.File.SetLastWriteTimeUtc(this.FullName, WorkSpaceFile.NowUTC());
        }

        /// <summary>
        /// Get local file's created date
        /// </summary>
        /// <returns></returns>
        public DateTime CreatedDate()
        {
            DateTime date =  System.IO.File.GetCreationTimeUtc(_filePath);

            var sateStr = date.ToString("yyyy-MM-dd hh:mm:ss");
            // database field does not store after second. Should be removed.
            date = Convert.ToDateTime(sateStr);

            if ( date.ToString("yyyy") == "1601")
            {
                date = Convert.ToDateTime(System.IO.File.GetCreationTimeUtc(_filePath));
            }
            return date;
        }

        /// <summary>
        /// Get local file's last access date
        /// </summary>
        /// <returns></returns>
        public DateTime LastAccessDate()
        {
            if(false == System.IO.File.Exists(_filePath))
                return DateTime.MinValue;

            DateTime date =  System.IO.File.GetLastAccessTimeUtc(_filePath);

            // database field does not store after second. Should be removed.
            date = Convert.ToDateTime(date.ToString("yyyy-MM-dd hh:mm:ss"));

            return date;
        }

        /// <summary>
        /// Get local file's last modified date
        /// </summary>
        /// <returns></returns>
        public DateTime LastModifiedDate()
        {
            if(false == System.IO.File.Exists(_filePath))
                return DateTime.MinValue;

            DateTime date =  System.IO.File.GetLastWriteTimeUtc(_filePath);

            // database field does not store after second. Should be removed.
            date = Convert.ToDateTime(date.ToString("yyyy-MM-dd hh:mm:ss"));

            return date;
        }
        public static DateTime NowUTC()
        {
            return  Convert.ToDateTime(DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"));
        }

        /// <summary>
        /// Start sync program. This needs create new records in the database.
        /// </summary>
        public void InitSync()
        {
            int id = 0;

            if (this.DocVersion == 0 )
            {
                if (false == this.IsNewStructure())
                    MigrateFolderStucture();

                id = WorkSpaceSyncController.CreateUserWorkSpace(this.GetInitialRecord());
            }
            else
            {
				// id < 0 means a document has been removed or system error.
                id = WorkSpaceSyncController.TransferToUserWorkSpace(this.DocVersion, this.GetFileHash(), SpiderDocsApplication.CurrentUserId);

                WorkSpaceSyncController.UpdateLastAccess(id,this.LastAccessDate());
                WorkSpaceSyncController.UpdateLastModified(id, this.LastModifiedDate());
            }

            this.Link2Table(id);

            // remove read only flag.
            FileInfo fileInfo = new FileInfo(_filePath);
            fileInfo.IsReadOnly = false;

        }

        /// <summary>
        /// Sync local to remote.
        /// </summary>
        async public System.Threading.Tasks.Task<bool> SyncUp()
        {
            if(logger.IsTraceEnabled) logger.Trace("Begin");

            //System.Threading.Tasks.Task bkTask = System.Threading.Tasks.Task.Run(() => { });

            //var remoteFile = WorkSpaceSyncController.GetBy(this.LinkId, true);

            //// backup first
            //if (remoteFile.id > 0) // exclude initial file to backup
            //{
            //    bkTask = System.Threading.Tasks.Task.Run(() => WorkSpaceSyncController.BackUp(remoteFile));
            //}

            //else logger.Debug("No remote file found");


            //await System.Threading.Tasks.Task.Run(() => {

            //    SqlOperation sql = new SqlOperation();
            //    sql.BeginTran();

            //    WorkSpaceSyncController.TransferToUserWorkSpaceBack(this.LinkId, sql);

            //    WorkSpaceSyncController.SyncUp(this.LinkId, this.GetFileData(), this.GetFileHash(), sql);

            //    sql.CommitTran();
            //});

            var bkTask = System.Threading.Tasks.Task.Run(() => WorkSpaceSyncController.TransferToUserWorkSpaceBack(this.LinkId));

            // Update remote file
            await System.Threading.Tasks.Task.Run(() => WorkSpaceSyncController.SyncUp(this.LinkId, this.GetFileData(), this.GetFileHash()));

            await bkTask;

            if (logger.IsTraceEnabled) logger.Trace("End");

            return true;
        }

        /// <summary>
        /// Sync remote to local.
        /// </summary>
        async public System.Threading.Tasks.Task<bool> SyncDown()
        {
            System.Threading.Tasks.Task removeTask = System.Threading.Tasks.Task.Run(() => { });

            //var remoteFile = WorkSpaceSyncController.GetBy(this.LinkId, true);

            if (true == System.IO.File.Exists(this._filePath))
            {
                removeTask = System.Threading.Tasks.Task.Run(() =>
                {
                    // backup first
                    WorkSpaceSyncController.BackUp(this.GetBackup());

                    // delete local file
                    FileFolder.DeleteFile(this._filePath);
                });
            }

            var remoteChangeTask = WorkSpaceSyncController.GetByAsync(this.LinkId, true);

            await removeTask;

            var remoteFile = await remoteChangeTask;

            // update loal file
            var savePath = RootPath + "\\" + remoteFile.filename;
            FileFolder.SaveFile(remoteFile.filedata, savePath);

            // The last access and modified date are set as DOWNLOADED time. not datebase time.
            System.IO.File.SetLastAccessTimeUtc(savePath, remoteFile.lastaccess_date);
            System.IO.File.SetLastWriteTimeUtc(savePath, remoteFile.lastmodified_date);

            // If a file exists only database then should create linkid
            if (GetLinkIDFromLocal() == 0)
                Link2Table(this.LinkId);

            return true;
            //var remoteFile = WorkSpaceSyncController.GetBy(this.LinkId, true);

            //if ( true == System.IO.File.Exists(this._filePath))
            //{
            //    // backup first
            //    WorkSpaceSyncController.BackUp(this.GetBackup(remoteFile));

            //    // delete local file
            //    FileFolder.DeleteFile(this._filePath);
            //}

            //// update loal file
            //var savePath = RootPath +"\\" + remoteFile.filename;
            //FileFolder.SaveFile(remoteFile.filedata, savePath);

            //// The last access and modified date are set as DOWNLOADED time. not datebase time.
            //System.IO.File.SetLastAccessTimeUtc(savePath, remoteFile.lastaccess_date);
            //System.IO.File.SetLastWriteTimeUtc(savePath, remoteFile.lastmodified_date);

            //// If a file exists only database then should create linkid
            //if ( GetLinkIDFromLocal() == 0)
            //    Link2Table(this.LinkId);

        }

		/// <summary>
		/// Delete local file.
		/// </summary>
		/// <returns></returns>
		async public System.Threading.Tasks.Task<bool> DeleteOnlyLocal()
		{
			//throw new NotImplementedException("DisActive");
			bool success = true;

			System.Threading.Tasks.Task removeTask = System.Threading.Tasks.Task.Run(() => { return true; });

            if (true == System.IO.File.Exists(this._filePath))
            {
				removeTask =  WorkSpaceSyncController.BackUpAsync(this.GetBackup());
            }

			await removeTask;

			success = DeleteThisFileAndFolder();

			return success;
		}

		/// <summary>
		/// Check if a checkout-ed file is checkin, deleted, discard checkout.
		/// </summary>
		//public void EndLife()
		//{
		//    // backup first
		//    var remoteFile = WorkSpaceSyncController.GetBy(this.LinkId, true);
		//    if(remoteFile.id > 0)
		//        WorkSpaceSyncController.BackUp(this.GetBackup(remoteFile));

		//    // delete local file
		//    FileFolder.DeleteFile(this._filePath);


		//}

        /// <summary>
        /// Remove a file locally and remotelly.
        /// </summary>
        /// <param name="wrkFile"></param>
        /// <returns></returns>
        async public System.Threading.Tasks.Task<bool> DeleteLocalAndRemote()
        {
            logger.Debug("Deleting: {0}", this.GetLinkId());

            bool success = false;

            // backup first
            var remoteFile = await WorkSpaceSyncController.GetByAsync(this.LinkId, false);

            // backup. load a file first then you can remove.
            var backup = this.GetBackup(remoteFile);

			await WorkSpaceSyncController.BackUpAsync(backup);

            success = WorkSpaceSyncController.Delete(this.GetLinkId());

            if (!success)
            {

                logger.Info("Deletion(DB) has been failed. {0}:{1}",this.GetLinkId(), this.ShortName);

                return false;
            }

			success = await DeleteOnlyLocal();

            return success;
        }

        /// <summary>
        /// File status that can consider sync
        /// </summary>
        /// <param name="dbHash"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<en_Status> GetStatus(string dbHash, DateTime lastModified, string fileName)
        {
			await System.Threading.Tasks.Task.Yield();

			if (true == await this.CheckOutHasFinished(dbHash))
            {
                // When checkin, deleted, discard checkout did.
                return en_Status.CheckOutFinished;
            }

            else if (true == this.IsUpdatedByRemote(dbHash, lastModified, fileName))
            {
                // When file is updated on other computer.
                return en_Status.IsUpdatedByRemote;
            }

            else if (true == this.IsUpdatedByLocal(dbHash, lastModified, fileName))
            {
                // When file is updated on this.
                return en_Status.IsUpdatedByLocal;
            }

            else if (true == this.IsUpdatedOnlyNameByRemote(dbHash, lastModified, fileName))
            {
                // remote filename is updated
                return en_Status.IsUpdatedOnlyNameByRemote;
            }

            else if (true == this.IsUpdatedOnlyNamebyLocal(dbHash, lastModified, fileName))
            {
                // local file is updated
                return en_Status.IsUpdatedOnlyNameByLocal;
            }

            else if (true == this.IsInitial(dbHash))
            {

                /*
                 TODO:
                 When spider docs is updated, all user_workspace is empty.
                 This needs to be cared otherwise all local workspace's files WILL be removed.
                */
                return en_Status.IsInitial;
            }
            else
            {
                // The file has not been changed so do NOTHING
                return en_Status.NoDiff;

            }
        }


        /// <summary>
        /// Check if local file is updated since synced.
        /// </summary>
        /// <param name="dbHash">The hash on remote</param>
        public bool IsUpdatedByLocal(string dbHash, DateTime dbLastModified, string fileName)
        {
            // This would be initial sync needs.
            if (this.IsInitial(dbHash)) return false;

            var localUpdated = dbHash.Trim() != this.GetFileHash() && dbLastModified < this.LastModifiedDate();

            if (
                    // Local file is latest
                    dbHash.Trim() != this.GetFileHash()

                    &&

                    dbLastModified < this.LastModifiedDate()
                )
                return true;
            else
                return false;
        }

        /// <summary>
        /// Check if local file is updated since synced (ONLY RENAME).
        /// </summary>
        /// <param name="dbHash"></param>
        /// <param name="dbLastModified"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool IsUpdatedOnlyNamebyLocal(string dbHash, DateTime dbLastModified, string fileName)
        {
            // This would be initial sync needs.
            if (this.IsInitial(dbHash)) return false;

            if (
                    // only file name is updated
                    (dbHash.Trim() == this.GetFileHash()

                    &&

                    dbLastModified < this.LastModifiedDate()

                    &&

                    this.FileName != fileName)
                )
                return true;
            else
                return false;
        }


        /// <summary>
        /// Check if remote file is updated since synced.
        /// </summary>
        /// <param name="dbHash">The hash on remote</param>
        /// <returns></returns>
        public bool IsUpdatedByRemote(string dbHash, DateTime dbLastModified, string fileName)
        {
            // This would be initial sync needs.
            if ( this.IsInitial(dbHash)) return false;

            // If a local file does not exists and remote does. then shoud download a file.
            if (false == System.IO.File.Exists(_filePath)) return true;

            if (
                    // Remove file is latest
                    dbHash.Trim() != this.GetFileHash()

                    &&

                    dbLastModified > this.LastModifiedDate()
                )
                return true;
            else
                return false;
        }


        /// <summary>
        /// Check if remote file is updated since synced.
        /// </summary>
        /// <param name="dbHash">The hash on remote</param>
        /// <returns></returns>
        public bool IsUpdatedOnlyNameByRemote(string dbHash, DateTime dbLastModified, string fileName)
        {
            // This would be initial sync needs.
            if ( this.IsInitial(dbHash)) return false;

            // If a local file does not exists and remote does. then shoud download a file.
            if (false == System.IO.File.Exists(_filePath)) return false;

            if (
                    // only file name is updated
                    (dbHash.Trim() == this.GetFileHash()

                    &&

                    dbLastModified > this.LastModifiedDate()

                    &&

                    this.FileName != fileName)
                )
                return true;
            else
                return false;
        }

        /// <summary>
        /// Check if a remote files are deleted, checkin, or discard checkout
        /// </summary>
        /// <param name="dbHash"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<bool> CheckOutHasFinished(string dbHash)
        {
			await System.Threading.Tasks.Task.Yield();

			// GetLinkIDFromLocal > 0 means that the syncronization for this file has done previusly, and then deleted this local file.
            var yes = (this.GetLinkIDFromLocal() > 0 && string.IsNullOrWhiteSpace(dbHash));

			// No ( link file not found. Never synced to remote? or changed status of spider docs )
			if ( false == yes )
			{
				// Check if document has removed on sd side.
				//if the document has removed ( status change to Deleted or Archived of document table), treats as check out has finished.
				yes = await this.IsChangedToDeleteStatusOnSDDocument();
				if( yes )
				{
					logger.Warn("A file probably be removed after checked out:{0}", this.FullName);
				}
			}

			return yes;
        }

        /// <summary>
        /// Check if a local file is created before sync logic is released.
        /// </summary>
        /// <param name="dbHash"></param>
        /// <returns>True: a file is created before sync logic is released</returns>
        public bool IsInitial(string dbHash)
        {
            if (this.GetLinkIDFromLocal() == 0 && string.IsNullOrWhiteSpace(dbHash)) return true;

            return false;
        }

		/// <summary>
		/// SD document's status has changed to 'Deleted' or 'Archived'
		/// This happens when :
		/// 1. Checked out on computer A
		/// 2. Checked out on computer B
		/// 3. Discard check out a file on computer A
		/// 4. Deleted a file on computer A(status change)
		/// 5. Sync feature enabled
		/// </summary>
		/// <returns></returns>
		public async System.Threading.Tasks.Task<bool> IsChangedToDeleteStatusOnSDDocument()
		{
			// Excludes new file
			if (DocVersion == 0) return false;

			// Version has mean a document is in db
			var active = await IsActiveInSpiderDocs();

			return !active;
		}


		/// <summary>
		/// Get unique data name
		/// </summary>
		/// <returns></returns>
		public string GetFileHash()
        {
            if (false == string.IsNullOrWhiteSpace(this._fileHash)) return this._fileHash;

            // when this class is instantiated based on remote update, get file hash should get here.
            if (this._taskGethash == null && System.IO.File.Exists(_filePath))
                this._taskGethash = System.Threading.Tasks.Task.Run(() => this._GetFileHash());
            else if (this._taskGethash == null && false == System.IO.File.Exists(_filePath)) return this._fileHash;

            this._taskGethash.Wait();

            string hash = this._taskGethash.Result;

            this._fileHash = hash;

            //string hash  = Crypt.GetHashSha256(_filePath);

            return hash;
        }
        private string _GetFileHash()
        {
            if(System.IO.File.Exists(_filePath))
            {
                string hash  = Crypt.GetHashMD5(_filePath);

                logger.Debug("Hash is {0} for {1}", hash, this._filePath.Replace(_userFolder,""));

                return hash;
            }
            else
            {
                logger.Warn("File does not exists: {0}", this._filePath);
                return string.Empty;
            }
        }

		/// <summary>
		/// Check if linked document is still active.
		/// </summary>
		/// <returns></returns>
		async private System.Threading.Tasks.Task<bool> IsActiveInSpiderDocs()
		{
			logger.Trace("[Begin]");

			bool exist = false;

			// the task _taskCheckActiveDoc must execute when class is instantiated.
			if (this._taskCheckActiveDoc == null)
			{
				logger.Warn("Method is not supported");

				throw new NotImplementedException("Not supported yet");
			}

			// Null means never set value before.
			if (this._isActiveInSpiderDocs == null)
			{
				var result = await this._taskCheckActiveDoc;

				exist = result.Count() > 0;

				this._isActiveInSpiderDocs = exist;
			}
			else
			{
				exist = (bool)this._isActiveInSpiderDocs;
			}

			logger.Trace("[End]");

			if(DocVersion > 0)
			{
				return exist;
			}
			else
			{
				return false;
			}
		}

        /// <summary>
        /// Get folder name for version zero
        /// </summary>
        /// <returns></returns>
        public string GetVerZeroFolderName()
        {
            logger.Trace("[Begin]");

            int num; int rand = 2;
            string hash = string.Empty;

            string origin = Crypt.GetHashMD5(_filePath);

            if ( true == string.IsNullOrWhiteSpace(_filePath) )
            {
                origin = Guid.NewGuid().ToString().Replace("-","");
            }

            logger.Trace("[Original Hash] {0}",origin);

            hash = origin.Substring(0, 7);

            logger.Trace("[Computed Hash] {0}",hash);

            // the hash must not exists in user folder. if so set 0 value so that it can keep loop.
            if (System.IO.Directory.Exists(this._userFolder + "\\" + hash))
                hash = "0";

            while (true == int.TryParse(hash, out num))
            {
                logger.Trace("[random] {0}",rand);

                hash = Guid.NewGuid().ToString().Replace("-","");

                logger.Trace("[Computed Full Hash] {0}",hash);

                hash = hash.Substring(0,7);

                logger.Trace("[Computed Hash] {0}",hash);

                // the hash must not exists in user folder. if so set 0 value so that it can keep loop.
                if( System.IO.Directory.Exists(this._userFolder + "\\" + hash ) )
                {
                    hash = "0";
                    ++rand ;
                }
            }

            logger.Trace("[End]");

            return hash;
        }

        public byte[] GetFileData()
        {
            //var fInfo = new FileInfo(_filePath);
            //var hidden = fInfo.Attributes.HasFlag(FileAttributes.Hidden);
            //if ( hidden )
            //{
            //    fInfo.Attributes &= ~FileAttributes.Hidden;
            //}

            byte[] filedata = FileFolder.ByteArrayFromPath(_filePath);

            //if (hidden)
            //{
            //    fInfo.Attributes |= FileAttributes.Hidden;
            //}

            return filedata;
        }

        /// <summary>
        /// Check if new file ( not exists in db) is new folder structure.
        /// New structure has version folder. ( non number folder )
        /// </summary>
        /// <returns></returns>
        public bool IsNewStructure()
        {
            if ( this.DocVersion == 0)
            {
                var hasVerFolder = _filePath.Replace( _userFolder ,"").Contains("\\");

                if (false == hasVerFolder) return false;
            }

            return true;
        }

        /// <summary>
        /// migrate old folder structure to new structure.
        /// Old structure does not have parent folder at root folder. So create it and move a file into it.
        /// </summary>
        public void MigrateFolderStucture()
        {
            if ( true == IsNewStructure() ) return;

            string shortHash = this.GetVerZeroFolderName();

			//check file name
			string newPath = FileFolder.GetAvailableFileName(this.RootPath + "\\" + shortHash + "\\"+ this.FileName);

			File.Move(_filePath, newPath);

            // Reset everything
            _filePath = newPath;
            Extract();
        }



        /// <summary>
        /// Create backup file records data
        /// </summary>
        /// <param name="baseData"></param>
        /// <returns></returns>
        public UserWorkSpace GetBackup(UserWorkSpace baseData = null)
        {
            var backup = new UserWorkSpace();

            backup.id = (baseData?.id == null || baseData?.id <= 0) ? this.LinkId : baseData.id;
            backup.id_user = baseData?.id_user ?? SpiderDocsApplication.CurrentUserId;
            backup.id_version = baseData?.id_version ?? DocVersion;
            backup.filename = this.FileName;
            backup.filedata = this.GetFileData();
            backup.filehash = this.GetFileHash();
            backup.created_date = this.CreatedDate();
            backup.lastaccess_date = this.LastAccessDate();
            backup.lastmodified_date = this.LastModifiedDate();

            return backup;
        }

        /// <summary>
        /// Extract filename and version from workspace file path.
        /// </summary>
        void Extract()
        {
                FileName = System.IO.Path.GetFileName(_filePath);

                int idVersion = 0;
                int.TryParse(System.IO.Directory.GetParent(_filePath).Name, out idVersion);

                DocVersion = idVersion;

        }

        /// <summary>
        /// Create initial user work space record data
        /// </summary>
        /// <param name="baseData"></param>
        /// <returns></returns>
        UserWorkSpace GetInitialRecord()
        {
            var ini = new UserWorkSpace();
            ini.id_user = SpiderDocsApplication.CurrentUserId;
            ini.id_version = this.DocVersion;
            ini.filename = this.FileName;
            ini.filedata = this.GetFileData();
            ini.filehash = this.GetFileHash();
            ini.created_date = this.CreatedDate();

            ini.lastaccess_date = this.LastAccessDate();
            ini.lastmodified_date = this.LastModifiedDate();

            return ini;
        }

		/// <summary>
		/// Remove this file and container folder.
		/// Hide if one of then gets failuer.
		/// </summary>
		/// <returns></returns>
		bool DeleteThisFileAndFolder()
		{
			bool success = true;

			if (System.IO.Directory.Exists(this.RootPath))
			{
				success = FileFolder.DeleteFilesAndThisDir(this.RootPath);

				logger.Debug("Deletion has been {0}, {1}", success, this.RootPath);
			}

			if (System.IO.File.Exists(this.FullName))
			{
				logger.Debug("Deletion(Local) has been failed. {0}:{1}", this.GetLinkId(), this.ShortName);

				// If deletion fails then make a file as hidden so that later sync does not detect.
				FileFolder.HideFolder(System.IO.Path.GetDirectoryName(this.FullName));

				success = false;
			}

			return success;
		}

		public object Clone()
        {
            return this.MemberwiseClone();
        }

        //---------------------------------------------------------------------------------
    }
}
