using AuctionHouse.Application.Commands;
using AuctionHouse.Application.Queries;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application
{
    public static class Startup
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediator(config =>
            {
                config.AddConsumers(typeof(Startup).Assembly);
                config.AddRequestClient<BidOnAuctionCommandRequest>();
                config.AddRequestClient<CreateAuctionCommandRequest>();
                config.AddRequestClient<AuctionStatusQueryRequest>();
                config.AddRequestClient<BidHistoryQueryRequest>();
            });

            return services;
        }
    }
}
