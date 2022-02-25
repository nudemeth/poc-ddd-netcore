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
            var numberOfBids = await bidHistoryRepository.NoOfBidsForAsync(request.AuctionId);
            var response = await unitOfWork.ExecuteRawQueryAsync(
                "SELECT id, current_price, bidder_member_id as winning_bidder_id, auction_ends FROM auction WHERE id = {0}",
                data => data
                    .Select(d => new AuctionStatusQueryResponse(d.id, d.current_price, d.auction_ends, d.winning_bidder_id, numberOfBids, clock.Time()))
                    .FirstOrDefault(),
                request.AuctionId);

            if (response == null)
            {
                throw new NotFoundException($"The auction cannot be found: AuctionId = {request.AuctionId}");
            }

            return response;
        }
    }
}
