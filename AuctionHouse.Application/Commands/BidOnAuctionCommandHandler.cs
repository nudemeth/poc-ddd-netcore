using AuctionHouse.Application.Exception;
using AuctionHouse.Domain;
using AuctionHouse.Domain.Auction;
using AuctionHouse.Domain.BidHistory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Commands
{
    public class BidOnAuctionCommandHandler : IRequestHandler<BidOnAuctionCommandRequest>
    {
        private readonly IAuctionRepository auctionRepository;
        private readonly IBidHistoryRepository bidHistoryRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IClock clock;

        public BidOnAuctionCommandHandler(IAuctionRepository auctionRepository, IBidHistoryRepository bidHistoryRepository, IUnitOfWork unitOfWork, IClock clock)
        {
            this.auctionRepository = auctionRepository;
            this.bidHistoryRepository = bidHistoryRepository;
            this.unitOfWork = unitOfWork;
            this.clock = clock;
        }

        public async Task<Unit> Handle(BidOnAuctionCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                using (DomainEvents.Register(OutBid()))
                using (DomainEvents.Register(BidPlaced()))
                {
                    var auction = await auctionRepository.FindByAsync(request.AuctionId);
                    var bidAmount = new Money(request.Amount);

                    auction.PlaceBidFor(new Offer(request.MemberId, bidAmount, clock.Time()), clock.Time());
                }

                await unitOfWork.SaveAsync();
            }
            catch (ConcurrencyException)
            {
                await unitOfWork.ClearAsync();
                await Handle(request, cancellationToken);
            }

            return Unit.Value;
        }

        private Action<BidPlacedEvent> BidPlaced()
        {
            return (BidPlacedEvent e) =>
            {
                var bidEvent = new Bid(e.AuctionId, e.Bidder, e.AmountBid, e.TimeOfBid);

                bidHistoryRepository.Add(bidEvent);
            };
        }

        private Action<OutBidEvent> OutBid()
        {
            return (OutBidEvent e) =>
            {
                // Email customer to say that he has been out bid                
            };
        }
    }
}
