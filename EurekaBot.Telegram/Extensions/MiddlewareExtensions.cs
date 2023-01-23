using EurekaBot.Telegram.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace EurekaBot.Telegram.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestCultureMiddleware>();
    }

    public static IApplicationBuilder UseUserDataDeletion(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UserDataDeletionMiddleware>();
    }

    public static IApplicationBuilder UseUserRegistration(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UserRegistrationMiddleware>();
    }
}
