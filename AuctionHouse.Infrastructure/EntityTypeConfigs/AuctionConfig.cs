using AuctionHouse.Domain.Auction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

            builder.Property("WinningBid")
                .HasColumnName("winning_bid")
                .HasConversion<WinningBidConverter>();

            builder.Property("Version").HasColumnName("version")
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate()
                .UseIdentityAlwaysColumn();
        }

        public class WinningBidConverter : ValueConverter<WinningBid, string>
        {
            public WinningBidConverter()
                : base(
                    v => JsonSerializer.Serialize(v, default(JsonSerializerOptions)),
                    v => JsonSerializer.Deserialize<WinningBid>(v, default(JsonSerializerOptions)))
            {
            }
        }
    }
}
