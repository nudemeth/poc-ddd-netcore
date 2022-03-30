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
        private readonly UnitOfWork unitOfWork;

        public AuctionRepository(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Auction auction)
        {
            await unitOfWork.DataContext.Auctions.AddAsync(auction);
        }

        public async Task<Auction> FindByAsync(Guid Id)
        {
            return await unitOfWork.DataContext.Auctions.SingleAsync(a => a.Id == Id);
        }
    }
}
