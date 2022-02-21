using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Commands
{
    public record CreateAuctionCommandResponse
    {
        public Guid AuctionId { get; init; }
    }
}
