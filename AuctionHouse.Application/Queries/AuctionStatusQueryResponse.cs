using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application.Queries
{
    public record AuctionStatusQueryResponse
    {
        public Guid Id { get; init; }
        public decimal CurrentPrice { get; init; }
        public DateTime AuctionEnds { get; init; }
        public Guid WinningBidderId { get; init; }
        public int NumberOfBids { get; init; }
        public TimeSpan TimeRemaining { get; init; }
    }
}
