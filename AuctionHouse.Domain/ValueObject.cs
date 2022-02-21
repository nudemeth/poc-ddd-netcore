using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain
{
    internal abstract record ValueObject<T>
        where T : ValueObject<T>
    {
    }
}
