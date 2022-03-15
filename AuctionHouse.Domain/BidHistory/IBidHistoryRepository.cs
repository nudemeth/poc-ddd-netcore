using AuctionHouse.Domain.BidHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.BidHistory
{
    public interface IBidHistoryRepository
    {
        Task<int> NoOfBidsForAsync(Guid autionId);
        Task AddAsync(Bid bid);
    }
}
