using System;
using EurekaBot.Application.Abstractions;
using EurekaBot.Infrastructure.Persistence;
using EurekaBot.Infrastructure.Persistence.Interceptors;
using EurekaBot.Infrastructure.Persistence.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace EurekaBot.Infrastructure.ServiceInstallers;

internal sealed class DatabaseInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((serviceProvider, dbContextOptionsBuilder) =>
        {
            var databaseOptions = serviceProvider.GetRequiredService<IOptions<MySqlOptions>>().Value;
            var auditableInterceptor = serviceProvider.GetRequiredService<UpdateAuditableEntitiesInterceptor>();

            MySqlConnectionStringBuilder builder = new(databaseOptions.ConnectionString)
            {
                UserID = configuration["UserID"],
                Password = configuration["Password"],
                Server = configuration["Server"],
                Database = configuration["Database"]
            };

            dbContextOptionsBuilder.UseMySql(
                connectionString: builder.ConnectionString,
                serverVersion: ServerVersion.AutoDetect(builder.ConnectionString),
                options =>
                {
                    options.CommandTimeout(databaseOptions.CommandTimeout);

                    options.EnableRetryOnFailure(
                            maxRetryCount: databaseOptions.MaxRetryCount,
                            maxRetryDelay: TimeSpan.FromSeconds(databaseOptions.MaxRetryDelay),
                            default);
                })
                .AddInterceptors(auditableInterceptor)
                .LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted })
                .EnableDetailedErrors(databaseOptions.EnableDetailedErrors)
                .EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
        });
    }
}
