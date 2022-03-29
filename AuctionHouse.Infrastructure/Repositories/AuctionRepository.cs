using AuctionHouse.Application;
using AuctionHouse.Domain.Auction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly DataContext dataContext;

        public AuctionRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task AddAsync(Auction auction)
        {
            await dataContext.Auctions.AddAsync(auction);
        }

        public async Task<Auction> FindByAsync(Guid Id)
        {
            return await dataContext.Auctions.SingleAsync(a => a.Id == Id);
        }
    }
}
