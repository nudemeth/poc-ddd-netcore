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
        private readonly IDataQueryable queryable;
        private readonly IBidHistoryRepository bidHistoryRepository;
        private readonly IClock clock;

        public AuctionStatusQueryHandler(IBidHistoryRepository bidHistory, IDataQueryable queryable, IClock clock)
        {
            this.bidHistoryRepository = bidHistory;
            this.queryable = queryable;
            this.clock = clock;
        }

        public async Task<AuctionStatusQueryResponse> Handle(AuctionStatusQueryRequest request, CancellationToken cancellationToken)
        {
            var numberOfBids = await bidHistoryRepository.NoOfBidsForAsync(request.AuctionId);
            var response = await queryable.ExecuteQueryAsync(
                "SELECT bidder_id, bid, time_of_bid FROM bid_history WHERE auction_id = @AuctionId ORDER BY bid DESC, time_of_bid ASC",
                new Dictionary<string, object> { { "@AuctionId", request.AuctionId } },
                data => data
                    .Select(d => new AuctionStatusQueryResponse(d.id, d.current_price, d.auction_ends, d.winning_bidder_id, numberOfBids, clock.Time()))
                    .FirstOrDefault());

            if (response == null)
            {
                throw new NotFoundException($"The auction cannot be found: AuctionId = {request.AuctionId}");
            }

            return response;
        }
    }
}
