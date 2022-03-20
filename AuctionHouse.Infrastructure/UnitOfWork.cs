using AuctionHouse.Application;
using AuctionHouse.Application.Exception;
using AuctionHouse.Domain.Auction;
using AuctionHouse.Domain.BidHistory;
using AuctionHouse.Infrastructure.EntityTypeConfigs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure
{
    public class UnitOfWork : DbContext, IUnitOfWork
    {
        public UnitOfWork(DbContextOptions<UnitOfWork> options)
            : base(options)
        {
        }

        public DbSet<Auction> Auctions { get; set; } = default!;
        public DbSet<Bid> BidHistory { get; set; } = default!;

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            try
            {
                var affected = await this.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new OutDatedDataException("The data has already been updated by other.", ex);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuctionConfig());
            modelBuilder.ApplyConfiguration(new BidConfig());
        }
    }
}
