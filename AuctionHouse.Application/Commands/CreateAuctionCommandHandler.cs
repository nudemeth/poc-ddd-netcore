using AuctionHouse.Domain.Auction;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Commands
{
    public class CreateAuctionCommandHandler : IConsumer<CreateAuctionCommandRequest>
    {
        private IAuctionRepository auctionRepository;
        private IUnitOfWork unitOfWork;

        public CreateAuctionCommandHandler(IAuctionRepository auctionRepository, IUnitOfWork unitOfWork)
        {
            this.auctionRepository = auctionRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<CreateAuctionCommandRequest> context)
        {
            var auctionId = Guid.NewGuid();
            var startingPrice = new Money(context.Message.StartingPrice);

            await auctionRepository.AddAsync(new Auction(auctionId, startingPrice, context.Message.EndsAt));
            await unitOfWork.SaveAsync();
            await context.RespondAsync(new CreateAuctionCommandResponse { AuctionId = auctionId });
        }
    }
}
