﻿using System;
using System.Linq;
using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EurekaBot.Application.Pipelines;

public sealed class PipelineLoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ILogger<PipelineLoggingBehavior<TRequest, TResponse>> _logger;

    public PipelineLoggingBehavior(ILogger<PipelineLoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Starting request {@RequestName}, {@DateTimeUtc}.",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        var result = await next();

        if (result.IsError)
        {
            _logger.LogError("Request failure {@RequestName}, {@Error}, {@DateTimeUtc}.",
                typeof(TRequest).Name,
                string.Join(',', result.Errors!.Select(e => e.Code)),
                DateTime.UtcNow);
        }

        _logger.LogInformation(
            "Completed request {@RequestName}, {@DateTimeUtc}.",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        return result;
    }
}
