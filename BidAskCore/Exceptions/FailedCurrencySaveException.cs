using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidAskCore.Exceptions
{
    class FailedCurrencySaveException: Exception
    {
        public FailedCurrencySaveException()
            : base(String.Format("Failed Currency db Save"))
        {

        }
    }
}
