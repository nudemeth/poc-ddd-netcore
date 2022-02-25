using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Queries
{
    public record BidHistoryQueryResponse
    {
        public Guid Bidder { get; init; }
        public decimal AmountBid { get; init; }
        public DateTime TimeOfBid { get; init; }
    }
}
