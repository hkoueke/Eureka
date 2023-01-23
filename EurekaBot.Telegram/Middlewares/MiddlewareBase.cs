using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace EurekaBot.Telegram.Middlewares;

public abstract class MiddlewareBase : IMiddleware
{
    protected Update Update { get; private set; } = default!;
    protected readonly ISender Sender;
    protected readonly ILogger<MiddlewareBase> Logger;

    protected MiddlewareBase(ISender sender, ILogger<MiddlewareBase> logger)
    {
        Sender = sender;
        Logger = logger;
    }

    public virtual async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string bodyResult;

        context.Request.EnableBuffering();

        using (var sr = new StreamReader(context.Request.Body, leaveOpen: true))
        {
            bodyResult = await sr.ReadToEndAsync();
            context.Request.Body.Position = 0;
        }

        var result = JsonConvert.DeserializeObject<Update>(bodyResult);

        if (result is null)
        {
            Logger.LogError("Unable to deserialize Update from HttpContext request body.");
            throw new InvalidOperationException("Unable to deserialize Update from HttpContext request body.");
        }

        Update = result;
    }
}
