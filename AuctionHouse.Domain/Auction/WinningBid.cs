using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.Auction
{
    public record WinningBid : ValueObject<WinningBid>
    {
        private WinningBid()
        {
        }

        public WinningBid(Guid bidder, Money maximumBid, Money bid, DateTimeOffset timeOfBid)
        {
            if (bidder == Guid.Empty)
                throw new ArgumentNullException("Bidder cannot be null");

            if (maximumBid == null)
                throw new ArgumentNullException("MaximumBid cannot be null");

            if (timeOfBid == DateTimeOffset.MinValue)
                throw new ArgumentNullException("TimeOfBid must have a value");

            Bidder = bidder;
            MaximumBid = new Money(maximumBid);
            TimeOfBid = timeOfBid;
            CurrentAuctionPrice = new Price(new Money(bid));
        }

        public Guid Bidder { get; private set; }
        public Money MaximumBid { get; private set; }
        public DateTimeOffset TimeOfBid { get; private set; }
        public Price CurrentAuctionPrice { get; private set; }
        public long Version { get; set; }

        public WinningBid RaiseMaximumBidTo(Money newAmount)
        {
            if (newAmount.IsGreaterThan(MaximumBid))
                return new WinningBid(Bidder, newAmount, CurrentAuctionPrice.Amount, DateTimeOffset.UtcNow);
            else
                throw new ApplicationException("Maximum bid increase must be larger than current maximum bid.");
        }

        public bool WasMadeBy(Guid bidder)
        {
            return Bidder.Equals(bidder);
        }

        public bool CanBeExceededBy(Money offer)
        {
            return CurrentAuctionPrice.CanBeExceededBy(offer);
        }

        public bool HasNotReachedMaximumBid()
        {
            return MaximumBid.IsGreaterThan(CurrentAuctionPrice.Amount);
        }
    }
}
