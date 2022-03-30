using AuctionHouse.Application;
using AuctionHouse.Application.Exception;
using AuctionHouse.Domain;
using AuctionHouse.Domain.Auction;
using AuctionHouse.Domain.BidHistory;
using AuctionHouse.Infrastructure.EntityTypeConfigs;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
    {
        private readonly IDbContextFactory<DataContext> dbContextFactory;
        private readonly IMediator mediator;
        
        public UnitOfWork(IDbContextFactory<DataContext> dbContextFactory, IMediator mediator)
        {
            this.dbContextFactory = dbContextFactory;
            this.mediator = mediator;
            this.DataContext = dbContextFactory.CreateDbContext();
        }

        public DataContext DataContext { get; private set; }

        public void Dispose()
        {
            DataContext.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await DataContext.DisposeAsync();
        }

        public async Task ClearAsync()
        {
            await DataContext.DisposeAsync();
            DataContext = dbContextFactory.CreateDbContext();
        }

        public async Task SaveAsync()
        {
            try
            {
                await DispatchDomainEvents();
                var affected = await DataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new OutDatedDataException("The data has already been updated by other.", ex);
            }
        }

        private Task DispatchDomainEvents()
        {
            DataContext.ChangeTracker.Entries<Entity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .ToList()
                .ForEach(e =>
                {
                    e.Entity.DomainEvents.ToList().ForEach(async de =>
                    {
                        await mediator.Publish(de);
                    });

                    e.Entity.ClearDomainEvents();
                });

            return Task.CompletedTask;
        }
    }
}
