using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DropNet;
using System.Diagnostics;

namespace OpenTTDSaveSync.Model
{
    class DropBoxAPI : ISaveStorageProvider
    {
        private DropNetClient _client;

        public bool Authenticate()
        {
            _client = new DropNetClient("4m6epjjse7qj0zt", "kp40315nnzmfa8a");
            _client.GetToken();
            var url = _client.BuildAuthorizeUrl();

            Process.Start(url);

            var accessToken = _client.GetAccessToken();

            _client = new DropNetClient("4m6epjjse7qj0zt", "kp40315nnzmfa8a", accessToken.Token, accessToken.Secret);

            return true;
        }

        public void UploadNewSaveFile(string gameExeName, string filePath)
        {
            throw new NotImplementedException("Nope");
        }

        public void UploadNewSaveFiles(string gameExeName, string[] filePath)
        {
            throw new NotImplementedException("Nope");
        }

        public string GetLatestSaveFile(string gameExeName)
        {
            throw new NotImplementedException("Nope");
        }

        public string[] GetAllSaveFiles(string gameExeName)
        {
            throw new NotImplementedException("Nope");
        }
    }
}
