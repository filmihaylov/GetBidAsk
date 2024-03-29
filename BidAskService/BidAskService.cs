﻿using BidAskCore.Scheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BidAskService
{
    public partial class BidAskService : ServiceBase
    {
        SchedulerWrapper sheduler;
        HttpServer server;
        public BidAskService()
        {
            InitializeComponent();
            this.server = new HttpServer();
        }

        protected override void OnStart(string[] args)
        {
            this.sheduler = new SchedulerWrapper();
            this.sheduler.StartJobs();
            Task.Run(() =>
            {
                this.server.start();
            });
        }

        protected override void OnStop()
        {
            this.sheduler.StopJobs();
            this.server.stop();
        }
        public void onDebug()
        {
            OnStart(null);
        }
    }
}
