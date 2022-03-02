using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Queries
{
    public class BidHistoryQueryResponse : ReadOnlyCollection<BidHistoryQueryResponse.BidHistoryQueryResponseItem>
    {
        public BidHistoryQueryResponse(IList<BidHistoryQueryResponseItem> items)
            : base(items)
        {
        }

        public record BidHistoryQueryResponseItem
        {
            public Guid Bidder { get; init; }
            public decimal AmountBid { get; init; }
            public DateTime TimeOfBid { get; init; }
        }
    }
}
