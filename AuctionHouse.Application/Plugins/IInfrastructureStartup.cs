using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionHouse.Application.Plugins
{
    public interface IInfrastructureStartup
    {
        IServiceCollection ConfigureInfrastructure(IServiceCollection services, IConfiguration configuration);
        IServiceProvider UseInfrastructure(IServiceProvider provider, IConfiguration configuration);
    }
}
