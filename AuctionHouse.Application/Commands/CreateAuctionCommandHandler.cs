using AuctionHouse.Domain.Auction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Commands
{
    public class CreateAuctionCommandHandler : IRequestHandler<CreateAuctionCommandRequest, CreateAuctionCommandResponse>
    {
        private IAuctionRepository auctionRepository;
        private IUnitOfWork unitOfWork;

        public CreateAuctionCommandHandler(IAuctionRepository auctionRepository, IUnitOfWork unitOfWork)
        {
            this.auctionRepository = auctionRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<CreateAuctionCommandResponse> Handle(CreateAuctionCommandRequest request, CancellationToken cancellationToken)
        {
            var auctionId = Guid.NewGuid();
            var startingPrice = new Money(request.StartingPrice);

            await auctionRepository.AddAsync(new Auction(auctionId, startingPrice, request.EndsAt));
            await unitOfWork.SaveAsync();

            return new CreateAuctionCommandResponse { AuctionId = auctionId };
        }
    }
}
