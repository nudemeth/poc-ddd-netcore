﻿using AuctionHouse.Domain.Auction;
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
    public class AuctionConfig : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.ToTable("auction");
            builder.HasKey("Id");

            builder.Property("Id").HasColumnName("id");
            builder.OwnsOne(typeof(Money), "StartingPrice", aa => aa.Property("Value").HasColumnName("starting_price"));
            builder.Property("EndsAt").HasColumnName("auction_ends").IsRequired();

            builder.OwnsOne(typeof(WinningBid), "WinningBid", aa =>
            {
                aa.Property("Bidder").HasColumnName("bidder_member_id");
                aa.OwnsOne(typeof(Price), "CurrentAuctionPrice", bb => bb.OwnsOne(typeof(Money), "Amount", cc => cc.Property("Value").HasColumnName("current_price").IsRequired()));
                aa.OwnsOne(typeof(Money), "MaximumBid", bb => bb.Property("Value").HasColumnName("maximum_bid"));
                aa.Property("TimeOfBid").HasColumnName("time_of_bid");
            });

            builder.Property("Version").HasColumnName("version")
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();
        }
    }
}