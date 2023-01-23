using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EurekaBot.Application.Abstractions;

public interface IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration);
}
