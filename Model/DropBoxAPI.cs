using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DropNet;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace OpenTTDSaveSync.Model
{
    class DropBoxAPI : ISaveStorageProvider
    {
        private DropNetClient _client;

        public bool Authenticate()
        {
            _client = new DropNetClient("4m6epjjse7qj0zt", "kp40315nnzmfa8a");
            _client.GetToken();
            _client.UseSandbox = true;
            var url = _client.BuildAuthorizeUrl();

            Process.Start(url);
            MessageBox.Show("PLease login into the Dropbox and accept the application access request before proceeding.", "Please login!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                var accessToken = _client.GetAccessToken();

                _client = new DropNetClient("4m6epjjse7qj0zt", "kp40315nnzmfa8a", accessToken.Token, accessToken.Secret);
                _client.UseSandbox = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void UploadNewSaveFile(string gameExeName, string filePath)
        {
            try
            {
                _client.CreateFolder("/" + gameExeName.Replace(".exe", ""));
            }
            catch (DropNet.Exceptions.DropboxException e)
            {
                e.Message.ToString();
            }

            try
            {
                _client.UploadFile("/" + gameExeName.Replace(".exe", ""), Path.GetFileName(filePath), File.ReadAllBytes(filePath));
            }
            catch (DropNet.Exceptions.DropboxException e)
            {
                e.Message.ToString();
            }
        }

        public string GetLatestSaveFile(string gameExeName, string savePath)
        {
            string resultPath = string.Empty;

            try
            {
                var result = _client.GetMetaData("/" + gameExeName.Replace(".exe", ""));

                var latestItem = result.Contents.FirstOrDefault();

                if (latestItem != null)
                {
                    foreach (var item in result.Contents)
                    {
                        if (latestItem != item)
                        {
                            if (item.ModifiedDate.CompareTo(latestItem.ModifiedDate) > 0)
                                latestItem = item;
                        }
                    }

                    //Check if we already have this save
                    if (!File.Exists(Path.Combine(savePath, latestItem.Name)))
                    {
                        byte[] file = _client.GetFile(latestItem.Path);

                        resultPath = Path.Combine(savePath, latestItem.Name);

                        File.WriteAllBytes(resultPath, file);
                    }
                }
            }
            catch (DropNet.Exceptions.DropboxException e)
            {
                e.Message.ToString();
            }

            return resultPath;
        }

        public string[] GetAllSaveFiles(string gameExeName)
        {
            throw new NotImplementedException("Nope");
        }
    }
}
