using System;
using System.Collections.Generic;
using BankFileLibrary;
using System.Net.Mail;
using System.Configuration;

namespace PositivePayMonitorService
{
    class FileProcessor
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Queue<string> _filesToProcess;
        private bool _processing;

        public log4net.ILog Logger
        {
            get { return log; }
        }

        public FileProcessor()
        {
            _filesToProcess = new Queue<string>();
            _processing = false;
        }

        public void AddFile(string fileName)
        {
            log.Info("Adding " + fileName);
            _filesToProcess.Enqueue(fileName);
            if(!_processing)
                ProcessFiles();
        }

        private void ProcessFiles()
        {
            _processing = true;
            string fileName;
            IFacility facility;

            log.Info("ProcessFiles called with " + _filesToProcess.Count + " in queue.");

            while (_filesToProcess.Count > 0)
            {
                fileName = _filesToProcess.Dequeue();
                log.Info("File: " + fileName + " is being processed.");
                FacilityFactory.Log = log;
                try
                {
                    facility = FacilityFactory.ProcessFile(fileName);
                    log.Info(fileName + " had " + facility.Checks + " checks.");

                    if (ConfigurationManager.AppSettings["AppMode"].ToUpper().Equals("PRODUCTION"))
                    {
                        NotifyPositivePay(facility);
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                }

            }

            _processing = false;
        }

        private void NotifyPositivePay(IFacility facility)
        {
            var email = new MailMessage("paypositiveapp@centershealthcare.org", "pospayuploads@centersbusiness.org");
            var Smtp = new SmtpClient("192.168.254.220");

            email.Bcc.Add("ysheinfil@centershealthcare.org");
            email.Subject = "New Pay Positive File";
            email.Body = "There is a new Positive Pay file \n" + facility.OutputFileName + "\nwith " + facility.Checks + " checks, that can be uploaded.";
            Smtp.Send(email);
        }

    }
}
