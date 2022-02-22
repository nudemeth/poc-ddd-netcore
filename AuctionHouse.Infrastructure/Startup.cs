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
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
        {

            return services;
        }
    }
}
