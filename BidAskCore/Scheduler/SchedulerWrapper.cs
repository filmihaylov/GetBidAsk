using BidAskCore.Data;
using BidAskCore.DTOs;
using Hangfire;
using Hangfire.MemoryStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidAskCore.Scheduler
{
    public class SchedulerWrapper
    {
        private BackgroundJobServer server;
        private Logger.Logger logger = new Logger.Logger();
        public SchedulerWrapper()
        {
            JobStorage.Current = GlobalConfiguration.Configuration.UseMemoryStorage();
            this.server = new BackgroundJobServer();
            this.server.Start();
        }
        public void StartJobs()
        {

            this.StartDataCollectionJob();
            this.ClearOldDataJob();
        }

        public void StopJobs()
        {
            this.server.Dispose();
        }

        public void StartDataCollectionJob()
        {
            RecurringJob.AddOrUpdate(
                       () => this.StartDataCollection(),
                       Cron.MinuteInterval(3));
        }

        public void StartDataCollection()
        {
            DbOpperations db = new DbOpperations();
            CurrencyDto dto = new CurrencyDto();
            ScrapperRT rtScrapper = new ScrapperRT();
            ScrapperF1 f1Scrapper = new ScrapperF1();
            string source = null;

            try
            {
                throw new Exception();
                dto = rtScrapper.ExtractEURUSDData();
                source = "RT";
            }
            catch(Exception ex)
            {
                this.logger.Log(ex);
                try
                {
                    dto = f1Scrapper.ExtractEURUSDData();
                    source = "F1";
                }
                catch(Exception exInner)
                {
                    this.logger.Log(exInner);
                    dto = null;
                }
            }

            Currency currency = new Currency()
            {
                CurrencyData = dto,
                Source = source,
                TimeStamp = DateTime.Now
            };

            db.InsertCurrency(currency);
        }

        public void ClearOldDataJob()
        {
            DbOpperations db = new DbOpperations();
            RecurringJob.AddOrUpdate(
                        () => db.DeleteDataOderThanDays(2),
                        Cron.DayInterval(3));
        }

    }
}
