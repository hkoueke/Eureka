using EurekaBot.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EurekaBot.Infrastructure.ServiceInstallers;

internal sealed class MemoryCacheInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
    }
}
