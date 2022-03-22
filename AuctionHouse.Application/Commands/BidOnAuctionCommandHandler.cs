using AuctionHouse.Application.Exception;
using AuctionHouse.Domain;
using AuctionHouse.Domain.Auction;
using AuctionHouse.Domain.BidHistory;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Commands
{
    public class BidOnAuctionCommandHandler : IConsumer<BidOnAuctionCommandRequest>
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

        public async Task Consume(ConsumeContext<BidOnAuctionCommandRequest> context)
        {
            try
            {
                var auction = await auctionRepository.FindByAsync(context.Message.AuctionId);
                var bidAmount = new Money(context.Message.Amount);
                var now = clock.Time();
                
                auction.PlaceBidFor(new Offer(context.Message.MemberId, bidAmount, now), now);

                await unitOfWork.SaveAsync();
            }
            catch (OutDatedDataException)
            {
                await unitOfWork.ClearAsync();
                await Consume(context);
            }

            await context.RespondAsync(new BidOnAuctionCommandResponse());
        }
    }
}
