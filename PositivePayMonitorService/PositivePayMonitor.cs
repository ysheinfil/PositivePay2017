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

namespace PositivePayMonitorService
{
    public partial class PositivePayMonitor : ServiceBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private FileProcessor _fileProcessor;
        private Queue<string> _newFiles;
        private System.Timers.Timer _timer;
        private const double MILLISECOND_MINUTE = 1000 * 60;

        public PositivePayMonitor()
        {
            InitializeComponent();
            _fileProcessor = new FileProcessor();
            _newFiles = new Queue<string>();
            string minutesDelay = ConfigurationManager.AppSettings["MinutesDelay"];
            if (int.TryParse(minutesDelay, out int delay))
            {
                _timer = new System.Timers.Timer(delay * MILLISECOND_MINUTE);
            }
            else
            {
                _timer = new System.Timers.Timer(10 * MILLISECOND_MINUTE);
            }
            _timer.AutoReset = false;
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ProcessQueue();
        }

        protected override void OnStart(string[] args)
        {
            fileWatcher = new System.IO.FileSystemWatcher(ConfigurationManager.AppSettings["FolderToWatch"]);
            fileWatcher.NotifyFilter = NotifyFilters.CreationTime |
    NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.LastAccess;
            fileWatcher.Filter = string.Empty;
            fileWatcher.Created += FileWatcher_Created;

            fileWatcher.EnableRaisingEvents = true;


            log.Info("Starting to watch " + fileWatcher.Path);
        }

        private void FileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            log.Info("New file noticed.");
            
            AddFileForProcessing(e.FullPath);
            SyncTimer();
        }

        private void SyncTimer()
        {
            _timer.Start();
        }

        private void AddFileForProcessing(string fullFilePath)
        {
            if (fullFilePath.Split('_').Count() == 4)
            {
                _newFiles.Enqueue(fullFilePath);
                log.Info(fullFilePath + " is #" + _newFiles.Count);
            }
            else
            {
                log.Error(fullFilePath + "'s name is in the wrong format.");
            }
        }


        private void ProcessQueue()
        {
            string targetFile;

            if (_newFiles.Count > 0)
            {
                targetFile = CopyFile(_newFiles.Dequeue());
                log.Info("Processing " + targetFile);
                _fileProcessor.AddFile(targetFile);

                SyncTimer();
            }
        }
        private string CopyFile(string fullFilePath)
        {

            string destFile, revFile;
            int revNo = 0, stringPivotPoint;

            log.Debug("Copying file " + fullFilePath);
            stringPivotPoint = fullFilePath.LastIndexOf(@"\", StringComparison.CurrentCulture);
            destFile = fullFilePath.Substring(0, stringPivotPoint + 1) +
                "processed" + fullFilePath.Substring(stringPivotPoint);

            revFile = destFile;
            while (File.Exists(revFile))
            {
                revNo++;
                revFile = destFile + ".rev" + revNo.ToString();
            }

            try
            {
                File.Copy(fullFilePath, revFile);
            }
            catch (Exception ex)
            {
                log.Error(ex.GetType().ToString());
            }
            return revFile;

            
        }

        protected override void OnStop()
        {
            
            log.Info("Stopping Service.");
        }

    }
}
