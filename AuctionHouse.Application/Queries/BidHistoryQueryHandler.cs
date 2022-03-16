using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Queries
{
    public class BidHistoryQueryHandler : IRequestHandler<BidHistoryQueryRequest, BidHistoryQueryResponse>
    {
        private readonly IDataQueryable<BidHistoryQueryResponse> queryable;

        public BidHistoryQueryHandler(IDataQueryable<BidHistoryQueryResponse> queryable)
        {
            this.queryable = queryable;
        }

        public async Task<BidHistoryQueryResponse> Handle(BidHistoryQueryRequest request, CancellationToken cancellationToken)
        {
            return await queryable.ExecuteQueryAsync<BidHistoryData>(
                new Dictionary<string, object> { { "@AuctionId", request.AuctionId } },
                data => new BidHistoryQueryResponse(
                    data.Select(d => new BidHistoryQueryResponse.BidHistoryQueryResponseItem
                    {
                        AmountBid = d.Bid,
                        Bidder = d.BidderId,
                        TimeOfBid = d.TimeOfBid,
                    })
                    .ToList()));
        }

        public record BidHistoryData
        {
            public decimal Bid { get; init; }
            public Guid BidderId { get; init; }
            public DateTimeOffset TimeOfBid { get; init; }
        }
    }
}
