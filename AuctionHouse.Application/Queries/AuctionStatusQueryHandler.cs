using AuctionHouse.Application.Exception;
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
        private readonly IDataQueryable<AuctionStatusQueryResponse> queryable;
        private readonly IBidHistoryRepository bidHistoryRepository;
        private readonly IClock clock;

        public AuctionStatusQueryHandler(IBidHistoryRepository bidHistory, IDataQueryable<AuctionStatusQueryResponse> queryable, IClock clock)
        {
            this.bidHistoryRepository = bidHistory;
            this.queryable = queryable;
            this.clock = clock;
        }

        public async Task<AuctionStatusQueryResponse> Handle(AuctionStatusQueryRequest request, CancellationToken cancellationToken)
        {
            var numberOfBids = await bidHistoryRepository.NoOfBidsForAsync(request.AuctionId);
            var response = await queryable.ExecuteQueryAsync<AuctionData>(
                new Dictionary<string, object> { { "@AuctionId", request.AuctionId } },
                data => data
                    .Select(d => new AuctionStatusQueryResponse(d.Id, d.CurrentPrice, d.AuctionEnds, d.WinningBidderId, numberOfBids, clock.Time()))
                    .SingleOrDefault() ?? throw new NotFoundException($"The auction cannot be found: AuctionId = {request.AuctionId}"));

            return response;
        }

        public record AuctionData
        {
            public Guid Id { get; init; }
            public decimal? CurrentPrice { get; init; }
            public DateTimeOffset AuctionEnds { get; init; }
            public Guid? WinningBidderId { get; init; }
        }
    }
}
