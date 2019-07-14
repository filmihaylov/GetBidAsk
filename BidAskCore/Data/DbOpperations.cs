using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidAskCore.Data
{
    public class DbOpperations
    {
        private CurrencyContext db;
        public DbOpperations()
        {
            this.db = new CurrencyContext();
        }

        public void InsertCurrency(Currency entity)
        {
            db.Currency.Add(entity);
            db.SaveChanges();
        }

        public Currency GetLastCurrency()
        {
            return this.db.Currency.OrderByDescending(x => x.TimeStamp).FirstOrDefault();
        }

        public void DeleteDataOderThanDays(int days)
        {
            db.Currency.RemoveRange(db.Currency.Where(x => (DateTime.Now - x.TimeStamp).Days > days));
            db.SaveChanges();
        }

        public void DeleteAllRows()
        {
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Currency]");
        }
    }
}
