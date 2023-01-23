using EurekaBot.Application.Abstractions;
using EurekaBot.Telegram.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EurekaBot.Telegram.ServiceInstallers;

internal sealed class OptionsInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<BotOptionsSetup>();
    }
}
