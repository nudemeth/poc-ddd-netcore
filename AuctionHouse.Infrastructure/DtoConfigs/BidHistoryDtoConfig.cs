using AuctionHouse.Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure.DtoConfigs
{
    public class BidHistoryDtoConfig : IEntityTypeConfiguration<BidHistoryDto>
    {
        public void Configure(EntityTypeBuilder<BidHistoryDto> builder)
        {
            builder.ToTable("bid_history");
            builder.HasKey(t => t.AuctionId);

            builder.Property(t => t.Id).HasColumnName("id");
            builder.Property(t => t.AuctionId).HasColumnName("auction_id");
            builder.Property(t => t.BidderId).HasColumnName("bidder_id");
            builder.Property(t => t.Bid).HasColumnName("bid");
            builder.Property(t => t.TimeOfBid).HasColumnName("time_of_bid");
        }
    }
}
