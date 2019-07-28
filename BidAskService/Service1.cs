using BidAskCore.Scheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BidAskService
{
    public partial class Service1 : ServiceBase
    {
        SchedulerWrapper sheduler;
        HttpServer server;
        public Service1()
        {
            InitializeComponent();
            this.server = new HttpServer();
        }

        protected override void OnStart(string[] args)
        {
            this.sheduler = new SchedulerWrapper();
            //this.sheduler.StartJobs();
            this.server.start();
        }

        protected override void OnStop()
        {
            //this.sheduler.StopJobs();
            this.server.stop();
        }
        public void onDebug()
        {
            OnStart(null);
        }
    }
}
