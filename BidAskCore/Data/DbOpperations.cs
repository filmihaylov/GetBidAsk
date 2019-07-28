using BidAskCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BidAskCore.Data
{
    public class DbOpperations
    {
        private static int failedAttempts = 0;
        private Logger.Logger logger = new Logger.Logger();

        private CurrencyContext db;
        public DbOpperations()
        {
            this.db = new CurrencyContext();
        }

        public void InsertCurrency(Currency entity)
        {
            try
            {
                db.Currency.Add(entity);
                db.SaveChanges();
                failedAttempts = 0;
            }
            catch(Exception ex)
            {
                this.logger.Log(ex);
                this.logger.Log(new FailedCurrencySaveException());
                if (failedAttempts >= 3)
                {
                    this.DeleteAllRows();
                    this.logger.Log(new DataBaseClearAllException());
                }
                Interlocked.Add(ref failedAttempts, 1);
            }
        }

        public Currency GetLastCurrency()
        {
            return this.db.Currency.OrderByDescending(x => x.TimeStamp).FirstOrDefault();
        }

        public void DeleteDataOderThanDays(int days)
        {
            // arithmetic not supported for now
            //db.Currency.RemoveRange(db.Currency.Where(x => (DateTime.Now - x.TimeStamp).Days > days));
            //db.SaveChanges();
            db.Database.ExecuteSqlCommand($"delete from Currencies where TimeStamp < date('now','-{days} days');");
        }

        public void DeleteAllRows()
        {
            db.Database.ExecuteSqlCommand("delete from Currencies");
        }
    }
}
