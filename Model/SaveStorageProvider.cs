using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTTDSaveSync.Model
{
    interface ISaveStorageProvider
    {

        bool Authenticate();

        void UploadNewSaveFile(string gameExeName, string filePath);

        string GetLatestSaveFile(string gameExeName, string savePath);

        string[] GetAllSaveFiles(string gameExeName);
    }
}
