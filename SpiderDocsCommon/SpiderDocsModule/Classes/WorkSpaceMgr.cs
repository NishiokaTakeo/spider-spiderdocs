using System;
using System.Collections.Generic;
using NLog;
using System.Linq;
using System.IO;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
    public class WorkSpaceMgr
    {
        Logger logger = LogManager.GetLogger("Sync");

        public string UserFolder = string.Empty;

        Dictionary<string, int> _processOfEditing = new Dictionary<string, int>();


        //public object isDeleting = false;
        static private readonly object lckDel = new object();
        static private readonly object lckSync = new object();
        static private readonly object lckInit = new object();
        private static int _lockFlag = 0; // 0 - free

        public int RunAfterSeconds { get; set; } = 10;
        DateTime lastRanDate = DateTime.MinValue;

        public WorkSpaceMgr(string filePath)
        {
            UserFolder = filePath;
        }

        public WorkSpaceMgr FileOpened(WorkSpaceFile wk, int processId)
        {
            //_processOfEditing.Add(string.Format(@"{0}\{1}", wk.DocVersion, wk.FileName));
            if (false == _processOfEditing.ContainsKey(wk.FullName))
            {
                _processOfEditing.Add(wk.FullName, processId);
            }

            wk.UpdateLastAccess();

            return this;
        }

        //public WorkSpaceMgr FileClosed(WorkSpaceFile wk)
        //{
        //    var exists = _processOfEditing.ContainsKey(wk.FullName);

        //    // never stored the key means not opening.
        //    if (false == exists) return this;

        //    _processOfEditing.Remove(wk.FullName);

        //    return this;
        //}

        private bool isOpening(WorkSpaceFile wk)
        {
            if (false == System.IO.File.Exists(wk.FullName)) return false;

            try
            {
                using (System.IO.FileStream stream = System.IO.File.OpenRead(wk.FullName)) { }

                return false;
            }
            catch (Exception ex)
            {
                return true;
            }



            //var exists = _processOfEditing.ContainsKey(wk.FullName);

            //// never stored the key means not opening.
            //if (false == exists) return false;

            //int processId = _processOfEditing[wk.FullName];

            //try
            //{
            //    var ps = System.Diagnostics.Process.GetProcessById(processId);

            //    return true;
            //}
            //catch(Exception ex)
            //{
            //    // If the file is closed then should remove the key
            //    _processOfEditing.Remove(wk.FullName);

            //    return false;
            //}

        }
        bool _stop = false;
        public void Start()
        {
            _stop = false;
        }
        public void Stop()
        {
            _stop = true;
        }
        /// <summary>
        /// Sync local to remote.
        /// </summary>
        public bool InitSync(WorkSpaceFile wFile)
        {
            if (logger.IsTraceEnabled) logger.Trace("[Begin] ", Newtonsoft.Json.JsonConvert.SerializeObject(wFile));

            bool lockTaken = false;
            try
            {
                System.Threading.Monitor.Enter(lckInit, ref lockTaken);
                //if( System.Threading.Monitor.)
                //lock(lckInit)
                //{
                //if (lockTaken)
                //{
                //logger.Trace("Sync is LOCKED");

                wFile.InitSync();

                //logger.Trace("Sync is RELEASED");
                //}
                //}
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);

            }
            finally
            {
                if (lockTaken) System.Threading.Monitor.Exit(lckInit);
            }

            if (logger.IsTraceEnabled) logger.Trace("[End] wFile", Newtonsoft.Json.JsonConvert.SerializeObject(wFile));
            return false;
            //isSyncing = false;
        }


		/// <summary>
		/// Remove a file locally and remotelly.
		/// </summary>
		/// <param name="wrkFile"></param>
		/// <returns></returns>
		public System.Threading.Tasks.Task<bool> Delete(WorkSpaceFile wrkFile)
		{
			if (logger.IsTraceEnabled) logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(wrkFile));

			System.Threading.Tasks.Task<bool> success = System.Threading.Tasks.Task.Run(() => false);

			try
			{
				Stop();

				if (isOpening(wrkFile))
				{
					logger.Info("File is opening. Cannot delete a file. {0}", wrkFile.FullName);

					return success;
				}

				success = wrkFile.DeleteLocalAndRemote();

			}
			catch (Exception ex)
			{
				logger.Error(ex);

			}
			finally
			{
				Start();
			}

			return success;
		}

		/// <summary>
		/// Rename a file locally and remotelly.
		/// </summary>
		/// <param name="wrkFile"></param>
		/// <returns></returns>
		public bool ReName(WorkSpaceFile wrkFile)
        {
            if (logger.IsTraceEnabled) logger.Trace(Newtonsoft.Json.JsonConvert.SerializeObject(wrkFile));

            bool success = false;
            //System.Threading.Tasks.Task<bool> success = System.Threading.Tasks.Task.Run(() => false);

            bool lockTaken = false;

            try
            {
                System.Threading.Monitor.Enter(lckInit, ref lockTaken);

                if (lockTaken)
                {
                    success = wrkFile.RenameTo();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);

            }
            finally
            {
                if (lockTaken) System.Threading.Monitor.Exit(lckInit);
            }

            return success;
        }

        /// <summary>
        /// Check if syncronization is running.
        /// </summary>
        /// <returns></returns>
        public bool CanSync()
        {
            //logger.Trace("[Begin]");

            if (lastRanDate.AddSeconds(RunAfterSeconds) < DateTime.Now)
            {
                //if (false == System.Threading.Monitor.IsEntered(lckSync) )
                if (System.Threading.Interlocked.CompareExchange(ref _lockFlag, 0, 0) == 0)
                {
                    logger.Trace("Sync is not LOCKED");
                    return true;
                }
            }

            return false;
        }

		public void Lock()
		{
			System.Threading.Interlocked.CompareExchange(ref _lockFlag, 1, 0);
		}

		public void UnLock()
		{
			System.Threading.Interlocked.Decrement(ref _lockFlag);
		}

		public bool IsLocked()
        {
            return System.Threading.Interlocked.CompareExchange(ref _lockFlag, 0, 0) == 0;
        }

        /// <summary>
        /// Sync local work space files intervally. It follows by RunAfterMinites property.
        /// If the local file is updated then remote db updates. if the remote db updates then local file is updated.
        /// </summary>
        /// <returns></returns>

        //[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        async public System.Threading.Tasks.Task<WorkSpaceMgr> Sync()
        {
            logger.Trace("[Begin]");

            // if(lastRanDate.AddSeconds(RunAfterSeconds) > DateTime.Now) return this;

            //if (isSyncing) return this;
            //isSyncing = true;
            //if (this.IsSyncing()) return this;
            // Skips this Sync is another sync is runnning.
            if ( false == CanSync() )
            {
                logger.Debug("[Sync] Already Running or Not enought gaps");

                return this;
            }

            bool lockTaken = false;
            try
            {
                //System.Threading.Monitor.Enter(lckSync, ref lockTaken);
                var lckLogger = LogManager.GetLogger("lck");


                lckLogger.Debug("Before Lock");

                if (System.Threading.Interlocked.CompareExchange(ref _lockFlag, 1, 0) == 0)
                {
                    //lock (lckSync)
                    //{
                    //if (lockTaken)
                    //{

                        lckLogger.Debug("Lock is entered");
                    //if (IsLocked())
                    //{
                    //    lckLogger.Debug("Is Locked is true");
                    //}

                        await RawSync();

                        lastRanDate = DateTime.Now;
                    //}


                    lckLogger.Debug("Is Locked is relesed");

                    //return x;
                }

                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex);


            }
            finally
            {
                logger.Trace("Sync is RELEASED");
                if (lockTaken) System.Threading.Monitor.Exit(lckSync);

				System.Threading.Interlocked.Decrement(ref _lockFlag);
			}

            //isSyncing = false;
            logger.Trace("[End]");

            return this;
        }

        /// <summary>
        /// Sync
        /// </summary>
        /// <returns></returns>
        public System.Threading.Tasks.Task<WorkSpaceMgr> SyncAsTask()
        {
            System.Threading.Tasks.Task<WorkSpaceMgr> task = System.Threading.Tasks.Task.Run(() => this);

            bool lockTaken = false;
            try
            {
                //System.Threading.Monitor.Enter(lckSync, ref lockTaken);
                logger.Trace("Sync is not LOCKED");

                lock (lckSync)
                //{
                //if (lockTaken)
                {
                    task = System.Threading.Tasks.Task.Run(() => RawSync());

                    lastRanDate = DateTime.Now;
                }
                //}
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            finally
            {
                logger.Trace("Sync is RELEASED");
                if(lockTaken) System.Threading.Monitor.Exit(lckSync);
            }

            return task;
        }

        /// <summary>
        /// Just sync
        /// </summary>
        /// <returns></returns>
        async private System.Threading.Tasks.Task<WorkSpaceMgr> RawSync()
        {
            if (_stop) return this;

            var localFiles = this.LoadFile4Local();


            var excludes = localFiles.Select(x => x.LinkId).ToArray();
            var remoteTasks = this.LoadFile4Remote(excludes);
            var remoteTask = System.Threading.Tasks.Task.WhenAll(remoteTasks);

            if (System.Threading.Monitor.IsEntered(lckInit)) return this;

            // Local change is priority.
            await _Sync(localFiles);

            await remoteTask;

            // Then get all changes by remote.
            await _Sync(remoteTask.Result.ToList(), true);


            return this;

        }

        /// <summary>
        /// Sync files by passed files.
        /// </summary>
        /// <param name="files"></param>
        async private System.Threading.Tasks.Task<bool> _Sync(List<WorkSpaceFile> files, bool remote = false)
        {
            var lckLogger = LogManager.GetLogger("lck");

            foreach (var wfile in files)
            {
                // list the files
                try
                {
                    //var wfile = new WorkSpaceFile(f.OriginalPath);

                    var remoteFile = await WorkSpaceSyncController.GetByAsync(wfile.GetLinkId());

                    //if (isDeleting) return;
                    if (System.Threading.Monitor.IsEntered(lckDel)) return true;
                    if (System.Threading.Monitor.IsEntered(lckInit)) return true;
                    //if (false == System.Threading.Monitor.IsEntered(isDeleting)) return;
                    if (isOpening(wfile)) continue;
                    if (remote && remoteFile.id != wfile.LinkId) continue; // Timing issue, if wfile(remote) has been removed until the code gets here it means removed. shoudl be continue.
                    if (SpiderDocsApplication.CurrentUserId == 0)
                    {
                        logger.Error("[Sync] UserID is zero.");
                        return false;
                    }

                    if (_stop) return true;

                    switch ( await wfile.GetStatus(remoteFile.filehash, remoteFile.lastmodified_date, remoteFile.filename))
                    {
                        case WorkSpaceFile.en_Status.IsUpdatedByRemote:
                            // When file is updated on other computer.

                            logger.Info("[Sync-Detected] Updated by the remote. {0}: {1} ", wfile.LinkId, wfile.FullName.Replace(UserFolder,""));

                            await wfile.SyncDown();

                            logger.Info("[Sync-Done] {0}: {1} ", wfile.LinkId, wfile.FullName.Replace(UserFolder, ""));
                            break;

                        case WorkSpaceFile.en_Status.IsUpdatedByLocal:

                            // When file is updated on this computer.

                            logger.Info("[Sync-Detected] Updated by the local. {0}: {1} ", wfile.LinkId, wfile.FullName.Replace(UserFolder, ""));

                            await wfile.SyncUp();

                            logger.Info("[Sync-Done] {0}: {1} ", wfile.LinkId, wfile.FullName.Replace(UserFolder, ""));

                            break;

                        case WorkSpaceFile.en_Status.IsUpdatedOnlyNameByRemote:
                            // When file is updated on other computer.

                            logger.Info("[Sync-Detected] Renamed by the remote. {0}: {1} ", wfile.LinkId, wfile.FullName.Replace(UserFolder, ""));

                            wfile.RenameFromRemote(remoteFile);

                            logger.Info("[Sync-Done] {0}: {1} ", wfile.LinkId, wfile.FullName.Replace(UserFolder, ""));

                            break;

                        case WorkSpaceFile.en_Status.IsUpdatedOnlyNameByLocal:
                            // When file is updated on this computer.

                            logger.Info("[Sync-Detected] Renamed by the local. {0}: {1} ", wfile.LinkId, wfile.FullName.Replace(UserFolder, ""));

                            wfile.RenameTo();

                            logger.Info("[Sync-Done] {0}: {1} ", wfile.LinkId, wfile.FullName.Replace(UserFolder, ""));

                            break;


                        case WorkSpaceFile.en_Status.CheckOutFinished:
                            // When checkin, deleted, discard checkout did.

                            logger.Info("[Sync-Detected] A File has been removed. {0}: {1} ", wfile.LinkId, wfile.FullName.Replace(UserFolder, ""));

                            await wfile.DeleteLocalAndRemote();//.EndLife();

                            logger.Info("[Sync-Done] {0}: {1} ", wfile.LinkId, wfile.FullName.Replace(UserFolder, ""));

                            break;

                        case WorkSpaceFile.en_Status.IsInitial:
                            /*
                                When spider docs is updated, all user_workspace is empty.
                                This needs to be cared otherwise all local workspace's files WILL be removed.
                            */

                            logger.Info("[Sync-Detected] New file is found. {0}: {1} ", wfile.LinkId, wfile.FullName.Replace(UserFolder, ""));

                            wfile.InitSync();

                            logger.Info("[Sync-Done] {0}: {1} ", wfile.LinkId, wfile.FullName.Replace(UserFolder, ""));

                            break;

						case WorkSpaceFile.en_Status.NoDiff:
                        case WorkSpaceFile.en_Status.None:
                            // The file has not been changed so do NOTHING
                            continue;

                    }

                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }

            return true;
        }


        /// <summary>
        /// Load workspace files from local folder and remote db
        /// </summary>
        /// <returns></returns>
        private List<WorkSpaceFile> LoadFile4Local()
        {
            var prospectFiles = new List<WorkSpaceFile>();

            var basePath = UserFolder;
            var dir = new ZetaLongPaths.ZlpDirectoryInfo(basePath);

            try
            {
                // Import
                // for local files
                foreach (var f in dir.GetFiles("*", SearchOption.AllDirectories))
                {
                    // Sync if the document is not currently editing.
                    if (
                            false == f.Attributes.HasFlag(ZetaLongPaths.Native.FileAttributes.Hidden) // exclude hidden file.

                            &&

                            //false == _processOfEditing.Exists(wsf => f.OriginalPath.Replace(basePath, "").IndexOf(wsf) > -1)

                            //&&

                            false == f.Name.Contains(".user_workspace.sd")
                        )
                    {
                        prospectFiles.Add(new WorkSpaceFile(f.OriginalPath, UserFolder));
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }

            return prospectFiles;
        }


        /// <summary>
        /// Load workspace files from local folder and remote db
        /// </summary>
        /// <returns></returns>
        private System.Threading.Tasks.Task<WorkSpaceFile>[] LoadFile4Remote(int[] excludes = null)
        {
            //System.Threading.Tasks.Task<List<WorkSpaceFile>> tasks;
            var tasks = new List<System.Threading.Tasks.Task<WorkSpaceFile>>();
            Dictionary<string, int> chkDuplication = new Dictionary<string, int>();
            var rmtFiles = new List<UserWorkSpace>();
            try
            {

                // For remote files.
                // This might contains duplication. So get only latest one records on each id_version:filename.
                var tmpRemoteFiles = WorkSpaceSyncController.SearchBy(SpiderDocsApplication.CurrentUserId);


                // Filter only unique file
                //foreach (var uniqueOrNo in tmpRemoteFiles)
                //{
                //    if (chkDuplication.ContainsKey(uniqueOrNo.filename)) continue;

                //    rmtFiles.Add(uniqueOrNo); // unique

                //    chkDuplication.Add(uniqueOrNo.filename, uniqueOrNo.id_version);
                //}

                foreach (var rfile in tmpRemoteFiles)
                {
                    if (excludes.Contains(rfile.id)) continue;

                    var task = System.Threading.Tasks.Task.Run(() =>
                    {
                        //var prospectFiles = new List<WorkSpaceFile>();

                        /* There are two cases.
                         *  1. a file exists only remote
                         *  2. a file exists both remote and local.
                         */
                        WorkSpaceFile wrkSpFile;

                        string wrkFilePath = WorkSpaceFile.FindPathBy(rfile.id, UserFolder);

                        if (System.IO.File.Exists(wrkFilePath)) // a file exists both local and remote.
                            wrkSpFile = new WorkSpaceFile(wrkFilePath, UserFolder);
                        else
                            wrkSpFile = new WorkSpaceFile(rfile, UserFolder);

                        if (wrkSpFile.FileName.Contains("user_workspace.sd"))
                        {

                        }
                        return wrkSpFile;
                    });

                    //task.Wait();

                    tasks.Add(task);

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return  tasks.ToArray();

            //return prospectFiles;
        }
    }
}
