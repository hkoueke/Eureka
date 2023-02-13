using System;
using System.Collections.Generic;
using System.Linq;
using EurekaBot.Application.Abstractions;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EurekaBot.Telegram.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection InstallServices(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assemblies)
    {
        IEnumerable<IServiceInstaller> serviceInstallers = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(IsAssignableToType<IServiceInstaller>)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();

        foreach (var service in serviceInstallers)
        {
            service.Install(services, configuration);
        }

        return services;

        static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
            typeof(T).IsAssignableFrom(typeInfo) &&
            !typeInfo.IsAbstract &&
            !typeInfo.IsInterface;
    }
}
