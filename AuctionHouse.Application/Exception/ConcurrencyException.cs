using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Exception
{
    public class ConcurrencyException : System.Exception
    {
        public ConcurrencyException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
