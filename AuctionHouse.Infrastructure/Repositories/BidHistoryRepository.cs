using AuctionHouse.Domain.BidHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure.Repositories
{
    public class BidHistoryRepository : IBidHistoryRepository
    {
        public void Add(Bid bid)
        {
            throw new NotImplementedException();
        }

        public int NoOfBidsFor(Guid autionId)
        {
            throw new NotImplementedException();
        }
    }
}
