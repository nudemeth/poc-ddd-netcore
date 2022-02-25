using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.Auction
{
    public interface IAuctionRepository
    {
        Task AddAsync(Auction auction);
        Task<Auction> FindByAsync(Guid Id);
    }
}
