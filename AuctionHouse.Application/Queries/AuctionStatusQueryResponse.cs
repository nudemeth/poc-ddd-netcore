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
        public AuctionStatusQueryResponse(Guid id, decimal currentPrice, DateTime auctionEnds, Guid winningBidderId, int numberOfBids, DateTime currentTime)
        {
            Id = id;
            CurrentPrice = currentPrice;
            AuctionEnds = auctionEnds;
            WinningBidderId = winningBidderId;
            NumberOfBids = numberOfBids;
            TimeRemaining = CalculateTimeRemaining(currentTime, auctionEnds);
        }

        public Guid Id { get; init; }
        public decimal CurrentPrice { get; init; }
        public DateTime? AuctionEnds { get; init; }
        public Guid? WinningBidderId { get; init; }
        public int NumberOfBids { get; init; }
        public TimeSpan TimeRemaining { get; init; }

        private static TimeSpan CalculateTimeRemaining(DateTime currentTime, DateTime AuctionEnds)
        {
            if (currentTime < AuctionEnds)
                return AuctionEnds.Subtract(currentTime);

            return new TimeSpan();
        }
    }
}
