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
        private readonly IBidHistoryRepository bidHistoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public BidPlacedEventHandler(IBidHistoryRepository bidHistoryRepository, IUnitOfWork unitOfWork)
        {
            this.bidHistoryRepository = bidHistoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<BidPlacedEvent> context)
        {
            var @event = context.Message;
            var bidEvent = new Bid(@event.AuctionId, @event.Bidder, @event.AmountBid, @event.TimeOfBid);
            await bidHistoryRepository.AddAsync(bidEvent);
            await unitOfWork.SaveAsync();
        }
    }
}
