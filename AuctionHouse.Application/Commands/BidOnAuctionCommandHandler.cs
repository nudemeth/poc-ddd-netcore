using AuctionHouse.Application.Exception;
using AuctionHouse.Application.Services;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly IClock clock;

        public BidOnAuctionCommandHandler(IAuctionRepository auctionRepository, IUnitOfWork unitOfWork, IClock clock)
        {
            this.auctionRepository = auctionRepository;
            this.unitOfWork = unitOfWork;
            this.clock = clock;
        }

        public async Task Consume(ConsumeContext<BidOnAuctionCommandRequest> context)
        {
            await Handle(context.Message);
            await context.RespondAsync(new BidOnAuctionCommandResponse());
        }

        private async Task Handle(BidOnAuctionCommandRequest request)
        {
            try
            {
                var auction = await auctionRepository.FindByAsync(request.AuctionId);
                var bidAmount = new Money(request.Amount);
                var now = clock.Time();

                auction.PlaceBidFor(new Offer(request.MemberId, bidAmount, now), now);

                await unitOfWork.SaveAsync();
            }
            catch (OutDatedDataException)
            {
                await unitOfWork.ClearAsync();
                await Handle(request);
            }
        }
    }
}
