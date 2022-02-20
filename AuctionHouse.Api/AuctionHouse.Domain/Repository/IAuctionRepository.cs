using AuctionHouse.Domain.Model.Auction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Domain.Repository
{
    internal interface IAuctionRepository
    {
        void Add(Auction auction);
        Auction FindBy(Guid Id);
    }
}
