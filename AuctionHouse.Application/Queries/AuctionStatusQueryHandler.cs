using AuctionHouse.Domain.BidHistory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Queries
{
    public class AuctionStatusQueryHandler : IRequestHandler<AuctionStatusQueryRequest, AuctionStatusQueryResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBidHistoryRepository bidHistoryRepository;
        private readonly IClock clock;

        public AuctionStatusQueryHandler(IBidHistoryRepository bidHistory, IUnitOfWork unitOfWork, IClock clock)
        {
            this.bidHistoryRepository = bidHistory;
            this.unitOfWork = unitOfWork;
            this.clock = clock;
        }

        public async Task<AuctionStatusQueryResponse> Handle(AuctionStatusQueryRequest request, CancellationToken cancellationToken)
        {
            var numberOfBids = bidHistoryRepository.NoOfBidsFor(request.AuctionId);
            var auctionDto = await unitOfWork.ExecuteRawQueryAsync<AuctionDto>("");
            var status = new AuctionStatusQueryResponse
            {
                Id = auctionDto.Id,
                CurrentPrice = auctionDto.CurrentPrice,
                AuctionEnds = auctionDto.AuctionEnds,
                WinningBidderId = auctionDto.WinningBidderId,
                TimeRemaining = TimeRemaining(auctionDto.AuctionEnds),
                NumberOfBids = numberOfBids,
            };

            return status;
        }

        public record AuctionDto
        {
            public Guid Id { get; init; }
            public decimal CurrentPrice { get; init; }
            public DateTime AuctionEnds { get; init; }
            public Guid WinningBidderId { get; init; }
        }

        private TimeSpan TimeRemaining(DateTime AuctionEnds)
        {
            if (clock.Time() < AuctionEnds)
                return AuctionEnds.Subtract(clock.Time());
            
            return new TimeSpan();
        }
    }
}
