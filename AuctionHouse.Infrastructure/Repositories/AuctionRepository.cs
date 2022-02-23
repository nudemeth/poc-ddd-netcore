using AuctionHouse.Domain.Auction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        public Task AddAsync(Auction auction)
        {
            throw new NotImplementedException();
        }

        public Auction FindByAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
