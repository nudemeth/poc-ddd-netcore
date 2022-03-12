using AuctionHouse.Domain.Auction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure.EntityTypeConfigs
{
    public class AuctionConfig : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.ToTable("auction");
            builder.HasKey("Id");

            builder.Property("Id").HasColumnName("id");
            builder.OwnsOne(typeof(Money), "StartingPrice", aa => aa.Property("Value").HasColumnName("starting_price"));
            builder.Property("EndsAt").HasColumnName("auction_ends");

            builder.OwnsOne(typeof(WinningBid), "WinningBid", aa =>
            {
                aa.Property("Bidder").HasColumnName("bidder_member_id").IsRequired(false);
                aa.OwnsOne(typeof(Price), "CurrentAuctionPrice", bb => bb.OwnsOne(typeof(Money), "Amount", cc => cc.Property("Value").HasColumnName("current_price").IsRequired(false)));
                aa.OwnsOne(typeof(Money), "MaximumBid", bb => bb.Property("Value").HasColumnName("maximum_bid").IsRequired(false));
                aa.Property("TimeOfBid").HasColumnName("time_of_bid").IsRequired(false);
            });

            builder.Property("Version").HasColumnName("version")
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();
        }
    }
}
