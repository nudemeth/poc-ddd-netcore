using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Commands
{
    public record CreateAuctionCommandRequest : IRequest<CreateAuctionCommandResponse>
    {
        public decimal StartingPrice { get; init; }
        public DateTime EndsAt { get; init; }
    }
}
