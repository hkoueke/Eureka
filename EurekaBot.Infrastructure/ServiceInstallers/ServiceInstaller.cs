using EurekaBot.Application.Abstractions;
using EurekaBot.Application.Repositories;
using EurekaBot.Infrastructure.Persistence;
using EurekaBot.Infrastructure.Persistence.Interceptors;
using EurekaBot.Infrastructure.Persistence.Repositories;
using EurekaBot.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using NodaTime.TimeZones;

namespace EurekaBot.Infrastructure.ServiceInstallers;

internal sealed class ServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
           .AddSingleton<IDateTimeZoneSource>(TzdbDateTimeZoneSource.Default)
           .AddSingleton<IDateTimeZoneProvider, DateTimeZoneCache>()
           .AddSingleton<IDateTimeService, DateTimeService>()
           .AddScoped<UpdateAuditableEntitiesInterceptor>();

        services
            .AddScoped<IRegionCodeEvaluator, RegionCodeEvaluator>();

        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<ICountryRepository, CountryRepository>()
            .AddScoped<IPostRepository, PostRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IUserRepository, UserRepository>();
    }
}
