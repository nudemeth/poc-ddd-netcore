using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.Auction
{
    public record OutBidEvent : DomainEvent
    {
        internal OutBidEvent(Guid auctionId, Guid bidderId)
        {
            if (auctionId == Guid.Empty)
                throw new ArgumentNullException(nameof(auctionId));

            if (bidderId == Guid.Empty)
                throw new ArgumentNullException(nameof(bidderId));

            AuctionId = auctionId;
            Bidder = bidderId;
        }

        internal Guid AuctionId { get; private set; }
        internal Guid Bidder { get; private set; }
    }
}
