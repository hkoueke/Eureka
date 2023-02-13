using EurekaBot.Application.Abstractions;
using EurekaBot.Telegram.Middlewares;
using EurekaBot.Telegram.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EurekaBot.Telegram.ServiceInstallers;

internal sealed class ServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddScoped<UserDataDeletionMiddleware>();
        services.TryAddScoped<UserRegistrationMiddleware>();
        services.TryAddScoped<RequestCultureMiddleware>();

        services
                .AddControllers()
                .AddNewtonsoftJson();

        services.AddHostedService<ConfigureWebhook>();
    }
}
