using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidAskCore.Exceptions
{
    class DataBaseClearAllException : Exception
    {
        public DataBaseClearAllException()
            : base(String.Format("Three times scrapped source failed Clearing All DB Records Fatal"))
        {

        }
    }
}
