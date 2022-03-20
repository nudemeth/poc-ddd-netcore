using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Queries
{
    public class BidHistoryQueryHandler : IConsumer<BidHistoryQueryRequest>
    {
        private readonly IDataQueryable<BidHistoryQueryResponse> queryable;

        public BidHistoryQueryHandler(IDataQueryable<BidHistoryQueryResponse> queryable)
        {
            this.queryable = queryable;
        }

        public async Task Consume(ConsumeContext<BidHistoryQueryRequest> context)
        {
            var response = await queryable.ExecuteQueryAsync<BidHistoryData>(
                new Dictionary<string, object> { { "@AuctionId", context.Message.AuctionId } },
                data => new BidHistoryQueryResponse(
                    data.Select(d => new BidHistoryQueryResponse.BidHistoryQueryResponseItem
                    {
                        AmountBid = d.Bid,
                        Bidder = d.BidderId,
                        TimeOfBid = d.TimeOfBid,
                    })
                    .ToList()));

            await context.RespondAsync(response);
        }

        public record BidHistoryData
        {
            public decimal Bid { get; init; }
            public Guid BidderId { get; init; }
            public DateTimeOffset TimeOfBid { get; init; }
        }
    }
}
