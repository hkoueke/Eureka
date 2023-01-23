using System.Threading.Tasks;
using EurekaBot.Application.Extensions;
using EurekaBot.Application.Features.Users.Commands.Users.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace EurekaBot.Telegram.Middlewares;

public sealed class UserRegistrationMiddleware : MiddlewareBase
{
    public UserRegistrationMiddleware(ISender sender, ILogger<UserRegistrationMiddleware> logger)
        : base(sender, logger)
    {
    }

    public override async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await base.InvokeAsync(context, next);

        User user = Update.GetTelegramUser();

        var chatId = Update.MyChatMember?.Chat.Id
                     ?? Update.Message?.Chat.Id
                     ?? user.Id;

        var result = await Sender.Send(new CreateUserCommand(user, chatId));

        result.SwitchFirst(
            value =>
                Logger.LogInformation(
                    "User with id '{id}' registered.",
                    value.ToString().Anonymize(6)),
            error => Logger.LogWarning("WARNING : {warning}", error.Description));

        await next(context);
    }
}
