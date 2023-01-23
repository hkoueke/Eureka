using System.Threading.Tasks;
using EurekaBot.Application.Extensions;
using EurekaBot.Application.Features.Users.Commands.Users.RemoveUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EurekaBot.Telegram.Middlewares;

public sealed class UserDataDeletionMiddleware : MiddlewareBase
{
    public UserDataDeletionMiddleware(ISender sender, ILogger<UserDataDeletionMiddleware> logger)
        : base(sender, logger)
    {
    }

    public override async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await base.InvokeAsync(context, next);

        if (Update.MyChatMember is not { } member || !member.IsKicked())
        {
            await next(context);
            return;
        }

        Logger.LogInformation("Attempting deletion of user data...");

        var result = await Sender.Send(new RemoveUserByIdCommand(member.From.Id));

        result.SwitchFirst(
            _ => Logger.LogInformation("User data deleted successfully."),
            error => Logger.LogWarning("WARNING: {warning}", error.Description));
    }
}