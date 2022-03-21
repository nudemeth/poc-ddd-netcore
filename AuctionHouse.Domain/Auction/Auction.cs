using AuctionHouse.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.Auction
{
    public class Auction : Entity
    {
        public Auction(Guid id, Money startingPrice, DateTimeOffset endsAt)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Auction Id cannot be null");

            if (startingPrice == null)
                throw new ArgumentNullException("Starting Price cannot be null");

            if (endsAt == DateTimeOffset.MinValue)
                throw new ArgumentNullException("EndsAt must have a value");

            Id = id;
            StartingPrice = startingPrice;
            EndsAt = endsAt;
        }

        private Auction()
        {
        }

        private Money StartingPrice { get; set; }
        private WinningBid? WinningBid { get; set; }
        private DateTimeOffset EndsAt { get; set; }

        private bool StillInProgress(DateTimeOffset currentTime)
        {
            return (EndsAt > currentTime);
        }

        public void PlaceBidFor(Offer offer, DateTimeOffset currentTime)
        {
            if (StillInProgress(currentTime))
            {
                if (FirstOffer())
                    PlaceABidForTheFirst(offer);
                else if (BidderIsIncreasingMaximumBidToNew(offer))
                    WinningBid = WinningBid.RaiseMaximumBidTo(offer.MaximumBid);
                else if (WinningBid.CanBeExceededBy(offer.MaximumBid))
                {
                    var newBids = new AutomaticBidder().GenerateNextSequenceOfBidsAfter(offer, WinningBid);

                    foreach (var bid in newBids)
                        Place(bid);
                }
            }
        }

        private bool BidderIsIncreasingMaximumBidToNew(Offer offer)
        {
            return WinningBid.WasMadeBy(offer.Bidder) && offer.MaximumBid.IsGreaterThan(WinningBid.MaximumBid);
        }

        private bool FirstOffer()
        {
            return WinningBid == null;
        }

        private void PlaceABidForTheFirst(Offer offer)
        {
            if (offer.MaximumBid.IsGreaterThanOrEqualTo(StartingPrice))
                Place(new WinningBid(offer.Bidder, offer.MaximumBid, StartingPrice, offer.TimeOfOffer));
        }

        private void Place(WinningBid newBid)
        {
            if (!FirstOffer() && WinningBid.WasMadeBy(newBid.Bidder))
            {
                AddDomainEvent(new OutBidEvent(Id, WinningBid.Bidder));
            }

            WinningBid = newBid;
            AddDomainEvent(new BidPlacedEvent(Id, newBid.Bidder, newBid.CurrentAuctionPrice.Amount, newBid.TimeOfBid));
        }
    }
}
