using AuctionHouse.Domain.Model.Auction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.Model.BidHistory
{
    internal record Bid : ValueObject<Bid>
    {
        internal Bid(Guid auctionId, Guid bidderId, Money amountBid, DateTime timeOfBid)
        {
            if (auctionId == Guid.Empty)
                throw new ArgumentNullException(nameof(auctionId));

            if (bidderId == Guid.Empty)
                throw new ArgumentNullException(nameof(bidderId));

            if (amountBid == null)
                throw new ArgumentNullException(nameof(amountBid));

            if (timeOfBid == DateTime.MinValue)
                throw new ArgumentNullException(nameof(timeOfBid));

            AuctionId = auctionId;
            Bidder = bidderId;
            AmountBid = amountBid;
            TimeOfBid = timeOfBid;
        }

        internal Guid AuctionId { get; private set; }
        internal Guid Bidder { get; private set; }
        internal Money AmountBid { get; private set; }
        internal DateTime TimeOfBid { get; private set; }
        internal Guid Id { get; set; }
    }
}
