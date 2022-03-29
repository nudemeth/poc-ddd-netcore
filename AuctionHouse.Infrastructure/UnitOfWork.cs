using AuctionHouse.Application;
using AuctionHouse.Application.Exception;
using AuctionHouse.Domain;
using AuctionHouse.Domain.Auction;
using AuctionHouse.Domain.BidHistory;
using AuctionHouse.Infrastructure.EntityTypeConfigs;
using MassTransit.Mediator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AuctionHouse.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContextFactory<DataContext> dbContextFactory;
        private readonly IMediator mediator;
        private readonly IServiceCollection serviceDescriptors;
        private readonly DataContext dataContext;

        public UnitOfWork(IDbContextFactory<DataContext> dbContextFactory, IMediator mediator, IServiceProvider serviceProvider, IServiceCollection serviceDescriptors)
        {
            this.dbContextFactory = dbContextFactory;
            this.mediator = mediator;
            this.serviceDescriptors = serviceDescriptors;
            this.dataContext = serviceProvider.GetRequiredService<DataContext>();
        }

        public async Task ClearAsync()
        {
            await dataContext.DisposeAsync();
            serviceDescriptors.Replace(ServiceDescriptor.Scoped(async s => await dbContextFactory.CreateDbContextAsync()));
        }

        public async Task SaveAsync()
        {
            try
            {
                await DispatchDomainEvents();
                var affected = await dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new OutDatedDataException("The data has already been updated by other.", ex);
            }
        }

        private Task DispatchDomainEvents()
        {
            dataContext.ChangeTracker.Entries<Entity>()
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
