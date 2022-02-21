using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure.Dtos
{
    public class AuctionDto
    {
        public Guid AuctionId { get; set; }
        public decimal StartingPrice { get; set; }
        public Guid BidderMemberId { get; set; }
        public DateTime TimeOfBid { get; set; }
        public decimal MaximumBid { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime AuctionEnds { get; set; }
        public decimal NextBidIncrement { get; set; }
        public int Version { get; set; }
    }
}
