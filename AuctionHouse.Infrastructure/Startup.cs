using AuctionHouse.Application;
using AuctionHouse.Application.Queries;
using AuctionHouse.Domain.Auction;
using AuctionHouse.Domain.BidHistory;
using AuctionHouse.Infrastructure.Queries;
using AuctionHouse.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
                .AddTransient<IDbConnection>(s => new NpgsqlConnection(configuration.GetConnectionString("default")))
                .AddScoped<IUnitOfWork>(s => s.GetRequiredService<UnitOfWork>())
                .AddScoped<IAuctionRepository, AuctionRepository>()
                .AddScoped<IBidHistoryRepository, BidHistoryRepository>()
                .AddScoped<IClock, SystemClock>()
                .AddScoped<IDataQueryable<AuctionStatusQueryResponse>, AuctionStatusQuery>()
                .AddScoped<IDataQueryable<BidHistoryQueryResponse>, BidHistoryQuery>();

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
