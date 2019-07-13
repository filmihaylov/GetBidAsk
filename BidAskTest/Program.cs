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

           ScrapperRT test = new ScrapperRT();

           CurrencyDto dto = test.ExtractEURUSDData();


            Console.WriteLine("EUR"+dto.EUR);
            Console.WriteLine("ÜSD"+dto.USD);

            Console.ReadKey();




        }
    }
}
