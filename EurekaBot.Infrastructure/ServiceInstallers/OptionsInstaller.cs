using EurekaBot.Application.Abstractions;
using EurekaBot.Infrastructure.Persistence.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EurekaBot.Infrastructure.ServiceInstallers;

internal sealed class OptionsInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<MySqlOptionsSetup>();
    }
}
