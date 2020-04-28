using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PositivePayMonitor
{
    public partial class ControlPanel : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static FileSystemWatcher fileWatcher;
        private FileProcessor _fileProcessor;
        private Queue<string> _newFiles;
        private System.Timers.Timer _timer;
        private const double MILLISECOND_MINUTE = 1000 * 60; 

        public ControlPanel()
        {
            InitializeComponent();
            label5.Text = ConfigurationManager.AppSettings["AppMode"];
            _fileProcessor = new FileProcessor();
            _newFiles = new Queue<string>();
            string minutesDelay = ConfigurationManager.AppSettings["MinutesDelay"];
            if (double.TryParse(minutesDelay, out double delay))
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

        private void ControlPanel_Load(object sender, EventArgs e)
        {
            fileWatcher = new System.IO.FileSystemWatcher(ConfigurationManager.AppSettings["FolderToWatch"]);
            WatchedFolder.Text = fileSystemWatcher1.Path;

            fileWatcher.NotifyFilter = NotifyFilters.CreationTime |
    NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.LastAccess;
            fileWatcher.Filter = string.Empty;
            fileWatcher.Created += OnChanged;

            fileWatcher.EnableRaisingEvents = true;

            WatchedFolder.Text = fileWatcher.Path;
            log.Info("Starting to watch " + fileWatcher.Path);
            AddMessage("Watching " + fileWatcher.Path);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            log.Info("New file noticed.");
            AddMessage("New file noticed: " + e.Name + ".\n");            
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
                AddMessage(fullFilePath + " is #" + _newFiles.Count);
            }
            else
            {
                log.Error(fullFilePath + "'s name is in the wrong format.");
                AddMessage("File name must be <FacilityName>_<BankName>-<Acct#>_<Date>_<Time>.csv");
            }
        }

        private void ProcessQueue()
        {
            string targetFile;

            if (_newFiles.Count > 0)
            {
                targetFile = CopyFile(_newFiles.Dequeue());
                log.Info("Processing " + targetFile);
                AddMessage("Processing " + targetFile);
                _fileProcessor.AddFile(targetFile);

                SyncTimer();
            }
        }

        public void AddMessage(string msgText)
        {
            this.ConsoleMessages.Invoke((MethodInvoker)delegate
            {
                ConsoleMessages.Text += DateTime.Now.ToLocalTime() + msgText + "\r\n";
            });
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            log.Fatal("Closing Application");
            
            base.OnFormClosing(e);
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
            catch(Exception ex)
            {
                log.Error(ex.GetType().ToString());
            }
            return revFile;
        }

    }
}
