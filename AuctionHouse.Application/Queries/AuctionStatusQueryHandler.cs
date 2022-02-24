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
            return await unitOfWork.ExecuteRawQueryAsync<AuctionStatusQueryResponse>("");
        }
    }
}
