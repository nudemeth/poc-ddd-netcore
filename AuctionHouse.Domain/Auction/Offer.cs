using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.Auction
{
    public record Offer : ValueObject<Offer>
    {
        public Offer(Guid bidderId, Money maximumBid, DateTime timeOfOffer)
        {
            if (bidderId == Guid.Empty)
                throw new ArgumentNullException("BidderId cannot be null");

            if (maximumBid == null)
                throw new ArgumentNullException("MaximumBid cannot be null");

            if (timeOfOffer == DateTime.MinValue)
                throw new ArgumentNullException("Time of Offer must have a value");

            Bidder = bidderId;
            MaximumBid = maximumBid;
            TimeOfOffer = timeOfOffer;
        }

        internal Guid Bidder { get; private set; }
        internal Money MaximumBid { get; private set; }
        internal DateTime TimeOfOffer { get; private set; }
    }
}
