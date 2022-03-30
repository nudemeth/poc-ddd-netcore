using AuctionHouse.Application;
using AuctionHouse.Domain.BidHistory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure.Repositories
{
    public class BidHistoryRepository : IBidHistoryRepository
    {
        private readonly UnitOfWork unitOfWork;

        public BidHistoryRepository(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Bid bid)
        {
            await unitOfWork.DataContext.BidHistory.AddAsync(bid);
        }

        public async Task<int> NoOfBidsForAsync(Guid autionId)
        {
            return await unitOfWork.DataContext.BidHistory.CountAsync(bh => bh.AuctionId == autionId);
        }
    }
}
