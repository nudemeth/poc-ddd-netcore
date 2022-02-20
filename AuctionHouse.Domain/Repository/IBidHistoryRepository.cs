using AuctionHouse.Domain.Model.BidHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.Repository
{
    internal interface IBidHistoryRepository
    {
        int NoOfBidsFor(Guid autionId);
        void Add(Bid bid);
    }
}
