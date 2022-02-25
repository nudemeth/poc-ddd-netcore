using AuctionHouse.Application;
using AuctionHouse.Application.Exception;
using AuctionHouse.Infrastructure.DtoConfigs;
using AuctionHouse.Infrastructure.Dtos;
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

        public DbSet<AuctionDto> Auction { get; set; } = default!;
        public DbSet<BidHistoryDto> BidHistory { get; set; } = default!;
        public DbSet<dynamic> Dummy { get; set; } = default!;

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TResult> ExecuteRawQueryAsync<TResult>(string rawQuery, Func<IEnumerable<dynamic>, TResult> map, params object[] @params)
        {
            var result = this.Dummy.FromSqlRaw(rawQuery, @params);
            return Task.FromResult(map(result));
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
            modelBuilder.Entity<DummyDto>().HasNoKey();

            modelBuilder.ApplyConfiguration(new AuctionDtoConfig());
            modelBuilder.ApplyConfiguration(new BidHistoryDtoConfig());
        }
    }
}
