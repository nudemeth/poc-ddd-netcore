using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure.Dtos
{
    public class BidHistoryDto
    {
        public Guid Id { get; set; }
        public Guid AuctionId { get; set; }
        public Guid BidderId { get; set; }
        public int Bid { get; set; }
        public DateTime TimeOfBid { get; set; }
    }
}
