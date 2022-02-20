using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.Model.Auction
{
    internal record WinningBid : ValueObject<WinningBid>
    {
        internal WinningBid(Guid bidder, Money maximumBid, Money bid, DateTime timeOfBid)
        {
            if (bidder == Guid.Empty)
                throw new ArgumentNullException("Bidder cannot be null");

            if (maximumBid == null)
                throw new ArgumentNullException("MaximumBid cannot be null");

            if (timeOfBid == DateTime.MinValue)
                throw new ArgumentNullException("TimeOfBid must have a value");

            Bidder = bidder;
            MaximumBid = maximumBid;
            TimeOfBid = timeOfBid;
            CurrentAuctionPrice = new Price(bid);
        }

        internal Guid Bidder { get; private set; }
        internal Money MaximumBid { get; private set; }
        internal DateTime TimeOfBid { get; private set; }
        internal Price CurrentAuctionPrice { get; private set; }

        internal WinningBid RaiseMaximumBidTo(Money newAmount)
        {
            if (newAmount.IsGreaterThan(MaximumBid))
                return new WinningBid(Bidder, newAmount, CurrentAuctionPrice.Amount, DateTime.Now);
            else
                throw new ApplicationException("Maximum bid increase must be larger than current maximum bid.");
        }

        internal bool WasMadeBy(Guid bidder)
        {
            return Bidder.Equals(bidder);
        }

        internal bool CanBeExceededBy(Money offer)
        {
            return CurrentAuctionPrice.CanBeExceededBy(offer);
        }

        internal bool HasNotReachedMaximumBid()
        {
            return MaximumBid.IsGreaterThan(CurrentAuctionPrice.Amount);
        }
    }
}
