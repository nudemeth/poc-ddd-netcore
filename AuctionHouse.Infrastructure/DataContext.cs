using AuctionHouse.Domain.Auction;
using AuctionHouse.Domain.BidHistory;
using AuctionHouse.Infrastructure.EntityTypeConfigs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Auction> Auctions { get; set; } = default!;
        public DbSet<Bid> BidHistory { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuctionConfig());
            modelBuilder.ApplyConfiguration(new BidConfig());
        }
    }
}
