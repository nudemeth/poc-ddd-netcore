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
    public class BidPlacedEventHandler : IConsumer<BidPlacedEvent>
    {
        private IBidHistoryRepository bidHistoryRepository;

        public BidPlacedEventHandler(IBidHistoryRepository bidHistoryRepository)
        {
            this.bidHistoryRepository = bidHistoryRepository;
        }

        public async Task Consume(ConsumeContext<BidPlacedEvent> context)
        {
            var domainEvent = context.Message;
            var bidEvent = new Bid(domainEvent.AuctionId, domainEvent.Bidder, domainEvent.AmountBid, domainEvent.TimeOfBid);
            await bidHistoryRepository.AddAsync(bidEvent);

            await context.RespondAsync(NoReplyMessage.Instance);
        }
    }
}
