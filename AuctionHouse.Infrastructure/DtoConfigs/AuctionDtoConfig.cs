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
    public class AuctionDtoConfig : IEntityTypeConfiguration<AuctionDto>
    {
        public void Configure(EntityTypeBuilder<AuctionDto> builder)
        {
            builder.ToTable("auction");
            builder.HasKey(t => t.AuctionId);

            builder.Property(t => t.AuctionId).HasColumnName("id");
            builder.Property(t => t.AuctionEnds).HasColumnName("auction_ends");
            builder.Property(t => t.BidderMemberId).HasColumnName("bidder_memeber_id");
            builder.Property(t => t.CurrentPrice).HasColumnName("current_price");
            builder.Property(t => t.MaximumBid).HasColumnName("maximum_bid");
            builder.Property(t => t.NextBidIncrement).HasColumnName("next_bid_increment");
            builder.Property(t => t.StartingPrice).HasColumnName("starting_price");
            builder.Property(t => t.TimeOfBid).HasColumnName("time_of_bid");
            builder.Property(t => t.Version).HasColumnName("version").IsConcurrencyToken();
        }
    }
}
