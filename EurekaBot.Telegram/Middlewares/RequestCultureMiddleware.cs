using EurekaBot.Application.Extensions;
using MediatR;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EurekaBot.Telegram.Middlewares;

public sealed class RequestCultureMiddleware : MiddlewareBase
{
    public RequestCultureMiddleware(ISender sender, ILogger<RequestCultureMiddleware> logger)
        : base(sender, logger)
    {
    }

    public override async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await base.InvokeAsync(context, next);

        var userId = $"{Update.GetTelegramUser().Id}";

        var culture = Update.GetTelegramUser().LanguageCode ?? "en";

        CultureInfo.CurrentCulture = new CultureInfo(culture);

        CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;

        Logger.LogInformation(
            "Culture set to '{culture}' for user '{id}'",
            culture, userId.Anonymize(visibleChars: 3));

        //var user = await _sender.Send(new GetUserByTelegramIdQuery(Update.GetTelegramUser().Id));

        await next(context);
    }
}
