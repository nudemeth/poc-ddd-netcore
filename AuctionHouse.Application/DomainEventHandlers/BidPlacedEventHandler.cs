using AuctionHouse.Domain;
using AuctionHouse.Domain.Auction;
using AuctionHouse.Domain.BidHistory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.DomainEventHandlers
{
    public class BidPlacedEventHandler : INotificationHandler<BidPlacedEvent>
    {
        private readonly IBidHistoryRepository bidHistoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public BidPlacedEventHandler(IBidHistoryRepository bidHistoryRepository, IUnitOfWork unitOfWork)
        {
            this.bidHistoryRepository = bidHistoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(BidPlacedEvent notification, CancellationToken cancellationToken)
        {
            var bidEvent = new Bid(notification.AuctionId, notification.Bidder, notification.AmountBid, notification.TimeOfBid);
            await bidHistoryRepository.AddAsync(bidEvent);
            await unitOfWork.SaveAsync();
        }
    }
}
