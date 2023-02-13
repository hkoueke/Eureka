using EurekaBot.Application.Abstractions;
using EurekaBot.Application.Repositories;
using EurekaBot.Infrastructure.Persistence;
using EurekaBot.Infrastructure.Persistence.Interceptors;
using EurekaBot.Infrastructure.Persistence.Repositories;
using EurekaBot.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NodaTime;
using NodaTime.TimeZones;

namespace EurekaBot.Infrastructure.ServiceInstallers;

internal sealed class ServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddSingleton<IDateTimeZoneSource>(TzdbDateTimeZoneSource.Default);
        services.TryAddSingleton<IDateTimeZoneProvider, DateTimeZoneCache>();
        services.TryAddSingleton<IDateTimeService, DateTimeService>();
        services.TryAddScoped<UpdateAuditableEntitiesInterceptor>();
        services.TryAddScoped<IRegionCodeEvaluator, RegionCodeEvaluator>();

        services.TryAddScoped<IUnitOfWork, UnitOfWork>();
        services.TryAddScoped<ICountryRepository, CountryRepository>();
        services.TryAddScoped<IPostRepository, PostRepository>();
        services.TryAddScoped<IRoleRepository, RoleRepository>();
        services.TryAddScoped<IUserRepository, UserRepository>();
    }
}
