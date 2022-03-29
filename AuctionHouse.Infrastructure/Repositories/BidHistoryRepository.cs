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
        private readonly DataContext dataContext;

        public BidHistoryRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task AddAsync(Bid bid)
        {
            await dataContext.BidHistory.AddAsync(bid);
        }

        public async Task<int> NoOfBidsForAsync(Guid autionId)
        {
            return await dataContext.BidHistory.CountAsync(bh => bh.AuctionId == autionId);
        }
    }
}
