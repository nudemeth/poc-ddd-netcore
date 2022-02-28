using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Queries
{
    public class BidHistoryQueryHandler : IRequestHandler<BidHistoryQueryRequest, IEnumerable<BidHistoryQueryResponse>>
    {
        private readonly IDataQueryable queryable;

        public BidHistoryQueryHandler(IDataQueryable queryable)
        {
            this.queryable = queryable;
        }

        public async Task<IEnumerable<BidHistoryQueryResponse>> Handle(BidHistoryQueryRequest request, CancellationToken cancellationToken)
        {
            return await queryable.GetBidHistoryAsync(
                data => data.Select(d => new BidHistoryQueryResponse
                {
                    AmountBid = d.bid,
                    Bidder = d.bidder_id,
                    TimeOfBid = d.time_of_bid,
                }),
                new Dictionary<string, object> { { "@AuctionId", request.AuctionId } });
        }
    }
}
