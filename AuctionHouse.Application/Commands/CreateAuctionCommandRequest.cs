using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Commands
{
    public record CreateAuctionCommandRequest
    {
        public decimal StartingPrice { get; init; }
        public DateTimeOffset EndsAt { get; init; }
    }
}
