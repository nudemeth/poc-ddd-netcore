using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Queries
{
    public record BidHistoryQueryRequest
    {
        public Guid AuctionId { get; init; }
    }
}
