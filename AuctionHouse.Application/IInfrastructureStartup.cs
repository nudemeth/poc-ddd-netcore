﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouse.Application
{
    public interface IInfrastructureStartup
    {
        IServiceCollection ConfigureInfrastructure(IServiceCollection services, IConfiguration configuration);
    }
}
