using System;
using System.Threading;
using System.Threading.Tasks;
using EurekaBot.Telegram.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace EurekaBot.Telegram.Services;


public class ConfigureWebhook : IHostedService
{
    private readonly ILogger<ConfigureWebhook> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly BotOptions _botOptions;

    public ConfigureWebhook(
        ILogger<ConfigureWebhook> logger,
        IServiceProvider serviceProvider,
        IOptions<BotOptions> botOptions)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _botOptions = botOptions.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        // Configure custom endpoint per Telegram API recommendations:
        // https://core.telegram.org/bots/api#setwebhook
        // If you'd like to make sure that the webhook was set by you, you can specify secret data
        // in the parameter secret_token. If specified, the request will contain a header
        // "X-Telegram-Bot-Api-Secret-Token" with the secret token as content.
        var webhookAddress = $"{_botOptions.HostAddress}{_botOptions.Route}";

        _logger.LogInformation("Setting webhook: {WebhookAddress}", webhookAddress);

        var allowedUpdates = new[]
        {
            UpdateType.Message, UpdateType.InlineQuery, UpdateType.MyChatMember,
            UpdateType.ChosenInlineResult, UpdateType.CallbackQuery
        };

        await botClient.SetWebhookAsync(
            url: webhookAddress,
            allowedUpdates: allowedUpdates,
            dropPendingUpdates: true,
            //secretToken: _botOptions.SecretKey,
            cancellationToken: cancellationToken);

    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        // Remove webhook on app shutdown
        _logger.LogInformation("Removing webhook");
        await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
    }
}
