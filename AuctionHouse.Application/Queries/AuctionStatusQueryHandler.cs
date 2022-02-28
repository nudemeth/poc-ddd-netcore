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
            var response = await queryable.GetAuctionStatusAsync(
                data => data
                    .Select(d => new AuctionStatusQueryResponse(d.id, d.current_price, d.auction_ends, d.winning_bidder_id, numberOfBids, clock.Time()))
                    .FirstOrDefault(),
                new Dictionary<string, object> { { "@AuctionId", request.AuctionId } });

            if (response == null)
            {
                throw new NotFoundException($"The auction cannot be found: AuctionId = {request.AuctionId}");
            }

            return response;
        }
    }
}
