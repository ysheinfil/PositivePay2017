﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

// For log4net:
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace FolderWatcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new FolderWatcherService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
