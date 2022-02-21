using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Commands
{
    public record BidOnAuctionCommandRequest : IRequest
    {
        public Guid AuctionId { get; init; }
        public Guid MemberId { get; init; }
        public decimal Amount { get; init; }
    }
}
