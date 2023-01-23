using EurekaBot.Application.Abstractions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EurekaBot.Application.ServiceInstallers;

internal sealed class MediatrInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(AssemblyReference.Assembly);
    }
}
