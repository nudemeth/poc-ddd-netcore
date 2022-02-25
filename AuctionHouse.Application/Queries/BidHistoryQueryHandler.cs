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
        private readonly IUnitOfWork unitOfWork;

        public BidHistoryQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BidHistoryQueryResponse>> Handle(BidHistoryQueryRequest request, CancellationToken cancellationToken)
        {
            return await unitOfWork.ExecuteRawQueryAsync(
                "SELECT bidder_id, bid, time_of_bid FROM bid_history WHERE auction_id = {0} ORDER BY bid DESC, time_of_bid ASC",
                data => data.Select(d => new BidHistoryQueryResponse
                {
                    AmountBid = d.bid,
                    Bidder = d.bidder_id,
                    TimeOfBid = d.time_of_bid,
                }),
                request.AuctionId);
        }
    }
}
