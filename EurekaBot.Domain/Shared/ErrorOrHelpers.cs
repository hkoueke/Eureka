using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EurekaBot.Domain.Shared;

internal static class ErrorOrHelpers
{
    public static ErrorOr<T> Ensure<T>(
        T value,
        params (Func<T, bool> predicate, Error error)[] functions)
    {
        var results = new List<ErrorOr<T>>();

        foreach (var (predicate, error) in functions)
        {
            results.Add(Ensure(value, predicate, error));
        }

        return Combine(results.ToArray());
    }

    public static TResult Map<TSource, TResult>(this ErrorOr<TSource> result, Func<TSource, TResult> mapper)
    {
        return mapper(result.Value);
    }


    private static ErrorOr<T> Ensure<T>(T value, Func<T, bool> predicate, Error error)
    {
        return predicate(value) ? value : error;
    }

    private static ErrorOr<T> Combine<T>(params ErrorOr<T>[] results)
    {
        if (results.Any(x => x.IsError))
        {
            var distinctErrors =
                results
                    .SelectMany(r => r.Errors)
                    .Distinct()
                    .ToList();

            return ErrorOr<T>.From(distinctErrors);
        }

        return results[0].Value;
    }
}