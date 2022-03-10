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
        public Bid(Guid auctionId, Guid bidderId, Money amountBid, DateTime timeOfBid)
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

        public Guid AuctionId { get; private set; }
        public Guid Bidder { get; private set; }
        public Money AmountBid { get; private set; }
        public DateTime TimeOfBid { get; private set; }
        public Guid Id { get; set; }
    }
}
