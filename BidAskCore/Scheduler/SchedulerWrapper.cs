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

        public SchedulerWrapper()
        {
            JobStorage.Current = GlobalConfiguration.Configuration.UseMemoryStorage();
            var server = new BackgroundJobServer();
            server.Start();
        }
        public void StartJobs()
        {

            this.StartDataCollectionJob();
            this.ClearOldDataJob();
        }

        public void StartDataCollectionJob()
        {
            RecurringJob.AddOrUpdate(
                       () => this.StartDataCollection(),
                       Cron.MinuteInterval(1));
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
                dto = rtScrapper.ExtractEURUSDData();
                source = "RT";
            }
            catch
            {
                try
                {
                    dto = f1Scrapper.ExtractEURUSDData();
                    source = "F1";
                }
                catch
                {
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
                        () => db.DeleteDataOderThanDays(4),
                        Cron.MinuteInterval(2));
        }

    }
}
