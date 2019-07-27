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
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.sheduler = new SchedulerWrapper();
            this.sheduler.StartJobs();
        }

        protected override void OnStop()
        {
            this.sheduler.StopJobs();
        }
        public void onDebug()
        {
            OnStart(null);
        }
    }
}
