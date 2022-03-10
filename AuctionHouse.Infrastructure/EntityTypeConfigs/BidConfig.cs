using AuctionHouse.Domain.Auction;
using AuctionHouse.Domain.BidHistory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure.EntityTypeConfigs
{
    public class BidConfig : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.ToTable("bid_history");
            builder.HasKey("Id");

            builder.Property("Id").HasColumnName("id");
            builder.Property("AuctionId").HasColumnName("auction_id");
            builder.Property("Bidder").HasColumnName("bidder_id");
            builder.OwnsOne(typeof(Money), "AmountBid", aa => aa.Property("Value").HasColumnName("bid"));
            builder.Property("TimeOfBid").HasColumnName("time_of_bid");
        }
    }
}
