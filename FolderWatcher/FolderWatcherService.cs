using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FolderWatcher
{

    public partial class FolderWatcherService : ServiceBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static FileSystemWatcher fileWatcher;
        public FolderWatcherService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            log.Info("Starting Service");
            try
            {
                log.Info("Attempting to watch " + ConfigurationManager.AppSettings["FolderToWatch"]);
                fileWatcher = new FileSystemWatcher(ConfigurationManager.AppSettings["FolderToWatch"]);

                fileWatcher.NotifyFilter = NotifyFilters.CreationTime |
    NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.LastAccess;
                fileWatcher.Filter = string.Empty;
                fileWatcher.Created += OnChanged;
                fileWatcher.Changed += OnChanged;
                fileWatcher.Renamed += OnChanged;
                
                fileWatcher.EnableRaisingEvents = true;
           }
            catch (ArgumentException argEx)
            {
                log.Error(argEx.Message);
                Stop();
            }
            log.Info("Watching " + fileWatcher.Path);

        }

        protected override void OnStop()
        {
            fileWatcher.EnableRaisingEvents = false;

            log.Info("Service stopped.");
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            log.Info("New file arrived.");
            try
            {
                BankFileLibrary.FacilityFactory.ProcessFile(e.FullPath);
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        

    }
}
