using EurekaBot.Application.Abstractions;
using EurekaBot.Application.Pipelines;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EurekaBot.Application.ServiceInstallers;

internal sealed class PipelineLoggingInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddScoped(
            typeof(IPipelineBehavior<,>), 
            typeof(PipelineLoggingBehavior<,>));
    }
}
