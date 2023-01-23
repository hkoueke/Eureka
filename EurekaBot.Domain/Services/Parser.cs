using System;
using ErrorOr;

namespace EurekaBot.Domain.Services;

internal static class Parser
{
    internal static ErrorOr<TResult> Parse<T, TResult>(T param, Func<T, ErrorOr<TResult>> predicate)
        where TResult : notnull
        where T : notnull
    {
        return predicate(param);
    }
}
