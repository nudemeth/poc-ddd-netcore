using AuctionHouse.Application;
using AuctionHouse.Infrastructure.DtoConfigs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> ExecuteRawQueryAsync<T>(string rawQuery)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            try
            {
                await this.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("The data has already been updated by other.", ex);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuctionDtoConfig());
            modelBuilder.ApplyConfiguration(new BidHistoryDtoConfig());
        }
    }
}
