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
        private IUnitOfWork unitOfWork;

        public BidHistoryQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<BidHistoryQueryResponse> Handle(BidHistoryQueryRequest request, CancellationToken cancellationToken)
        {
            return await unitOfWork.ExecuteRawQueryAsync<BidHistoryQueryResponse>("");
        }
    }
}
