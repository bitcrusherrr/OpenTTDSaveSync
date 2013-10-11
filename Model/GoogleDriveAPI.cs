using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTTDSaveSync.Model
{
    //Note:TODO
    class GoogleDriveAPI : ISaveStorageProvider
    {
        public bool Authenticate()
        {
            throw new NotImplementedException("Nope");
        }

        public void UploadNewSaveFile(string gameExeName, string filePath)
        {
            throw new NotImplementedException("Nope");
        }

        public void UploadNewSaveFiles(string gameExeName, string[] filePath)
        {
            throw new NotImplementedException("Nope");
        }

        public string GetLatestSaveFile(string gameExeName, string savePath)
        {
            throw new NotImplementedException("Nope");
        }

        public string[] GetAllSaveFiles(string gameExeName)
        {
            throw new NotImplementedException("Nope");
        }
    }
}
