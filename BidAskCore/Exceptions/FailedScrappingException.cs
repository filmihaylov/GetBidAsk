using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidAskCore.Exceptions
{
    public class FailedScrappingException : Exception
    {
        public FailedScrappingException()
        {

        }

        public FailedScrappingException(string sourceScrappe)
            : base(String.Format("Scrapping failed from source: {0}", sourceScrappe))
        {

        }
    }
}
