using EurekaBot.Application.Abstractions;
using EurekaBot.Telegram.Middlewares;
using EurekaBot.Telegram.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EurekaBot.Telegram.ServiceInstallers;

internal sealed class ServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<UserDataDeletionMiddleware>()
            .AddScoped<UserRegistrationMiddleware>()
            .AddScoped<RequestCultureMiddleware>();

        services
                .AddControllers()
                .AddNewtonsoftJson();

        services.AddHostedService<ConfigureWebhook>();
    }
}
