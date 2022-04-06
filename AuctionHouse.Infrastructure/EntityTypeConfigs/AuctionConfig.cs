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
            builder.Ignore(e => e.DomainEvents);

            builder.Property("Id").HasColumnName("id");
            builder.OwnsOne(typeof(Money), "StartingPrice", aa => aa.Property("Value").HasColumnName("starting_price"));
            builder.Property("EndsAt").HasColumnName("auction_ends");

            builder.OwnsOne(typeof(WinningBid), "WinningBid", aa =>
            {
                aa.ToTable("winning_bid");

                aa.Property("AuctionId").HasColumnName("auction_id");
                aa.Property("Bidder").HasColumnName("bidder_member_id");
                aa.OwnsOne(typeof(Price), "CurrentAuctionPrice", bb => bb.OwnsOne(typeof(Money), "Amount", cc => cc.Property("Value").HasColumnName("current_price")));
                aa.OwnsOne(typeof(Money), "MaximumBid", bb => bb.Property("Value").HasColumnName("maximum_bid"));
                aa.Property("TimeOfBid").HasColumnName("time_of_bid");

                aa.Property("Version").HasColumnName("version")
                    .IsConcurrencyToken()
                    .ValueGeneratedOnAddOrUpdate()
                    .UseIdentityAlwaysColumn();
            });
        }
    }
}
