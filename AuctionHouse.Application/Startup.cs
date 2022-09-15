using AuctionHouse.Application.Behaviours;
using AuctionHouse.Application.Commands;
using AuctionHouse.Application.Queries;
using AuctionHouse.Application.Services;
using FluentValidation;
using MediatR;
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
            services
                .AddValidatorsFromAssembly(typeof(Startup).Assembly)
                .AddMediatR(typeof(Startup).Assembly)
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>)) //First is outermost
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
