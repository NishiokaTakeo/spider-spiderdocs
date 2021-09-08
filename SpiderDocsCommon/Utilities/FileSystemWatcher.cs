using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Spider.IO
{
	/// <summary>
	/// <para>This class checks file system changes and fires a specified event.</para>
	/// </summary>
	public class FullFileSystemWatcher
	{
		FileSystemWatcher _watcher;
        Action<object, FileSystemEventArgs> _handler;
        string _path;
        /// <param name="path">Path to check file system changes.</param>
        /// <param name="event_handler">Event to be run when file system changes occur.</param>
        public FullFileSystemWatcher(string path, Action<object,FileSystemEventArgs> event_handler)
		{
            _path = path;
            _handler = event_handler;

            _watcher = GetNewInstance(path, event_handler);

		}

		FileSystemWatcher GetNewInstance(string path, Action<object,FileSystemEventArgs> event_handler)
		{
            FileSystemWatcher watcher = new FileSystemWatcher();

			watcher.Path = path;
			watcher.Changed += new FileSystemEventHandler(event_handler);
			watcher.Created += new FileSystemEventHandler(event_handler);
			watcher.Deleted += new FileSystemEventHandler(event_handler);
			watcher.Renamed += new RenamedEventHandler(event_handler);			
            watcher.Error += Watcher_Error;
            watcher.IncludeSubdirectories = true;

            // Filter is all.
            watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.FileName | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
            watcher.InternalBufferSize = 65536; // Max size 64kb
            watcher.EnableRaisingEvents = true;

            return watcher;
		}

        private void Watcher_Error(object sender, ErrorEventArgs e)
        {
            _watcher.Dispose();
            
            // recreate
            _watcher = GetNewInstance(_path, _handler);
        }
    }
}
