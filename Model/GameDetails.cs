using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTTDSaveSync.Model
{
    class GameDetails
    {
        private List<FileSystemWatcher> _saveLocationCollection;
        private readonly string TTDEXENAME = "openttd.exe";
        private string _gamePath;
        private ISaveStorageProvider _saveStorageProvider;

        public GameDetails(string gamePath, ISaveStorageProvider saveStorageProvider)
        {
            _saveLocationCollection = new List<FileSystemWatcher>();
            _saveStorageProvider = saveStorageProvider;
            _gamePath = gamePath;

            //Try to locate default OpenTTD save file location
            AddDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "OpenTTD", "save"));
        }

        void directory_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                _saveStorageProvider.UploadNewSaveFile(TTDEXENAME, e.FullPath);
            }
        }

        public bool AddDirectory(string path)
        {
            bool result = false;

            if (Directory.Exists(path))
            {
                FileSystemWatcher directory = new FileSystemWatcher();
                directory.Path = path;
                _saveLocationCollection.Add(directory);
                directory.Changed += directory_Changed;

                result = true;
            }

            return result;
        }

        /// <summary>
        /// This tool is going to be run in the background as it is to get, or upload save changes, might as well serve as a launcher.
        /// </summary>
        public void LaunchGame()
        {
            Process.Start(Path.Combine(_gamePath, TTDEXENAME));
        }
    }
}
