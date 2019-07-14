using BidAskCore;
using BidAskCore.Data;
using BidAskCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidAskTest
{
    class Program
    {
        static void Main(string[] args)
        {

            DbOpperations db = new DbOpperations();

            CurrencyDto dto = new CurrencyDto()
            {
                EUR = 2,
                USD = 3.4m
            };

            Currency test = new Currency();

            test.CurrencyData = dto;

            test.Source = "fdfd";

            test.TimeStamp = DateTime.Now;


            db.InsertCurrency(test);


            db.DeleteDataOderThanDays(5);

            Currency gg = db.GetLastCurrency();


            Console.WriteLine(gg.CurrencyData.EUR);
            Console.WriteLine(gg.CurrencyData.USD);


            db.DeleteAllRows();

            Console.ReadKey();



        }
    }
}
