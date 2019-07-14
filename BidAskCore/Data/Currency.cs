using BidAskCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidAskCore.Data
{
    public class Currency
    {
        public int ID { get; set; }
        public CurrencyDto CurrencyData { get; set; }
        public string Source { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
