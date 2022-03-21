using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.Auction
{
    public record BidPlacedEvent : DomainEvent
    {
        public BidPlacedEvent(Guid auctionId, Guid bidderId, Money amountBid, DateTimeOffset timeOfBid)
        {
            if (auctionId == Guid.Empty)
                throw new ArgumentNullException(nameof(auctionId));

            if (bidderId == Guid.Empty)
                throw new ArgumentNullException(nameof(bidderId));

            if (amountBid == null)
                throw new ArgumentNullException(nameof(amountBid));

            if (timeOfBid == DateTimeOffset.MinValue)
                throw new ArgumentNullException(nameof(timeOfBid));

            AuctionId = auctionId;
            Bidder = bidderId;
            AmountBid = amountBid;
            TimeOfBid = timeOfBid;
        }

        public Guid AuctionId { get; private set; }
        public Guid Bidder { get; private set; }
        public Money AmountBid { get; private set; }
        public DateTimeOffset TimeOfBid { get; private set; }
    }
}
