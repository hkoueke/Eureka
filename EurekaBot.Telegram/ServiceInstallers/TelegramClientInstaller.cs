using EurekaBot.Application.Abstractions;
using EurekaBot.Telegram.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace EurekaBot.Telegram.ServiceInstallers;

internal sealed class TelegramClientInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpClient("eurekabot_http_client")
            .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
            {
                var options = sp.GetRequiredService<IOptions<BotOptions>>().Value;
                return new TelegramBotClient(options.Token, httpClient);
            });
    }
}
