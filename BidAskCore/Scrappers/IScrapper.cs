using BidAskCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidAskCore.Scrappers
{
    public interface IScrapper
    {
        CurrencyDto ExtractEURUSDData();
    }
}
