using AuctionHouse.Domain.Auction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.BidHistory
{
    public record Bid : ValueObject<Bid>
    {
        private Bid()
        {
        }

        public Bid(Guid auctionId, Guid bidder, Money amountBid, DateTimeOffset timeOfBid)
        {
            if (auctionId == Guid.Empty)
                throw new ArgumentNullException(nameof(auctionId));

            if (bidder == Guid.Empty)
                throw new ArgumentNullException(nameof(bidder));

            if (amountBid == null)
                throw new ArgumentNullException(nameof(amountBid));

            if (timeOfBid == DateTimeOffset.MinValue)
                throw new ArgumentNullException(nameof(timeOfBid));

            AuctionId = auctionId;
            Bidder = bidder;
            AmountBid = amountBid;
            TimeOfBid = timeOfBid;
        }

        public Guid AuctionId { get; private set; }
        public Guid Bidder { get; private set; }
        public Money AmountBid { get; private set; }
        public DateTimeOffset TimeOfBid { get; private set; }
        public Guid Id { get; set; }
    }
}
