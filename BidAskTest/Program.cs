using BidAskCore;
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

            ScrapperF1 test = new ScrapperF1();

            CurrencyDto dto = test.ExtractEURUSDData();


            Console.WriteLine("EUR" + dto.EUR);
            Console.WriteLine("USD" + dto.USD);

            Console.ReadKey();

        }
    }
}
