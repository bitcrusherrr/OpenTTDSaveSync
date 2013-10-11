using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbz.UIComponents;
using OpenTTDSaveSync.Model;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;

namespace OpenTTDSaveSync
{
    public class MainScreenVM : BaseIObservable
    {
        private DelegateCommand _launchTTDCommand;
        private DelegateCommand _addSaveDirectoryCommand;
        private DelegateCommand _addGamePathCommand;
        private DelegateCommand _applyLoginDetailsCommand;

        public DelegateCommand LaunchTTDCommand { get { return _launchTTDCommand; } }
        public DelegateCommand AddSaveDirectoryCommand { get { return _addSaveDirectoryCommand; } }
        public DelegateCommand AddGamePathCommand { get { return _addGamePathCommand; } }
        public DelegateCommand ApplyLoginDetailsCommand { get { return _applyLoginDetailsCommand; } }

        private GameDetails openTTD;
        private ISaveStorageProvider storageProvider;

        public MainScreenVM()
        {
            _launchTTDCommand = new DelegateCommand
            {
                CanExecuteDelegate = x => openTTD != null,
                ExecuteDelegate = x => LaunchOTTD()
            };

            _addSaveDirectoryCommand = new DelegateCommand
            {
                CanExecuteDelegate = x => openTTD != null,
                ExecuteDelegate = x => AddSavePath()
            };

            _addGamePathCommand = new DelegateCommand
            {
                CanExecuteDelegate = x => storageProvider != null,
                ExecuteDelegate = x => AddGameLocation()
            };

            _applyLoginDetailsCommand = new DelegateCommand
            {
                CanExecuteDelegate = x => true,
                ExecuteDelegate = x => SetLoginDetails()
            };
        }

        private void AddGameLocation()
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            DialogResult result = browser.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (Directory.EnumerateFiles(browser.SelectedPath).FirstOrDefault(x => x.ToLower().Contains("openttd.exe")) != null)
                    openTTD = new GameDetails(browser.SelectedPath, storageProvider);
            }
        }

        private void SetLoginDetails()
        {
            storageProvider = new DropBoxAPI();

            storageProvider.Authenticate();
        }

        private void AddSavePath()
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            DialogResult result = browser.ShowDialog();

            if (result == DialogResult.OK)
            {
                openTTD.AddDirectory(browser.SelectedPath);
            }
        }

        private void LaunchOTTD()
        {
            openTTD.LaunchGame();
        }
    }
}
