using AuctionHouse.Application.Commands;
using AuctionHouse.Application.Queries;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration, Assembly infrastructureAssembly)
        {
            var nullableStartUpType = infrastructureAssembly
                .GetTypes()
                .FirstOrDefault(t => t.IsAssignableFrom(typeof(IInfrastructureStartup)));

            return nullableStartUpType switch
            {
                Type startUpType => ConfigureInfrastructure(startUpType, services, configuration),
                _ => services,
            };
        }

        public static IServiceProvider UseInfrastructureServices(this IServiceProvider provider, IConfiguration configuration, Assembly infrastructureAssembly)
        {
            var nullableStartUpType = infrastructureAssembly
                .GetTypes()
                .FirstOrDefault(t => t.IsAssignableFrom(typeof(IInfrastructureStartup)));

            return nullableStartUpType switch
            {
                Type startUpType => UseInfrastructure(startUpType, provider, configuration),
                _ => provider,
            };
        }

        private static IServiceCollection ConfigureInfrastructure(Type startUpType, IServiceCollection services, IConfiguration configuration)
        {
            var startUp = Activator.CreateInstance(startUpType);
            var method = startUpType.GetMethod("ConfigureInfrastructure");
            
            return (IServiceCollection)method!.Invoke(startUp, new object[] { services, configuration })!;
        }

        private static IServiceProvider UseInfrastructure(Type startUpType, IServiceProvider provider, IConfiguration configuration)
        {
            var startUp = Activator.CreateInstance(startUpType);
            var method = startUpType.GetMethod("UseInfrastructure");

            return (IServiceProvider)method!.Invoke(startUp, new object[] { provider, configuration })!;
        }
    }
}
