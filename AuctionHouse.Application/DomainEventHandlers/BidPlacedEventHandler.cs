using AuctionHouse.Domain;
using AuctionHouse.Domain.Auction;
using AuctionHouse.Domain.BidHistory;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.DomainEventHandlers
{
    public class BidPlacedEventHandler : DomainEventHandler<BidPlacedEvent>
    {
        private IBidHistoryRepository bidHistoryRepository;

        public BidPlacedEventHandler(IBidHistoryRepository bidHistoryRepository)
        {
            this.bidHistoryRepository = bidHistoryRepository;
        }

        protected override async Task Handle(BidPlacedEvent @event)
        {
            var bidEvent = new Bid(@event.AuctionId, @event.Bidder, @event.AmountBid, @event.TimeOfBid);
            await bidHistoryRepository.AddAsync(bidEvent);
        }
    }
}
