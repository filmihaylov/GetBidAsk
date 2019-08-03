﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BidAskService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            #if DEBUG
            //While debugging this section is used.
            BidAskService myService = new BidAskService();
            myService.onDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            #endif

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BidAskService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
