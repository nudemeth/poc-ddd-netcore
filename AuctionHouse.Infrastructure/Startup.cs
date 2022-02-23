using AuctionHouse.Application;
using AuctionHouse.Domain.Auction;
using AuctionHouse.Domain.BidHistory;
using AuctionHouse.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<UnitOfWork>(builder =>
                {
                    builder.UseNpgsql(configuration.GetConnectionString("default"));
                })
                .AddScoped<IAuctionRepository, AuctionRepository>()
                .AddScoped<IBidHistoryRepository, BidHistoryRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IClock, SystemClock>();

            return services;
        }

        public static async Task<IServiceProvider> UseInfrastructure(this IServiceProvider provider, IConfiguration configuration)
        {
            using var scope = provider.CreateAsyncScope();
            using var context = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
            await context.Database.MigrateAsync();

            return provider;
        }
    }
}
