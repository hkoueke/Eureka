using System;
using System.Linq;

namespace EurekaBot.Application.Extensions;

public static class StringExtensions
{
    public static string Anonymize(
        this string value,
        int visibleChars,
        char separator = '*',
        bool startOnly = false)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "String must not be null, empty, or consists only of white-space characters.", 
                nameof(value));
        }

        if (visibleChars <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(visibleChars),
                $"'{nameof(visibleChars)}' must be greater than zero.");
        }

        var trimmed = value.Trim();

        switch (startOnly)
        {
            case true when trimmed.Length < visibleChars:
                throw new InvalidOperationException($"String must be at least {visibleChars} characters long.");

            case false when trimmed.Length < 2 * visibleChars:
                throw new InvalidOperationException($"String must be at least {2 * visibleChars} characters long.");
        }

        if (trimmed.Length == visibleChars || trimmed.Length == 2 * visibleChars)
        {
            return trimmed;
        }

        var separatorCount = startOnly switch
        {
            true => trimmed.Length - visibleChars,
            _ => trimmed.Length - (2 * visibleChars)
        };

        string separators = new(Enumerable.Repeat(separator, separatorCount).ToArray());

        return startOnly switch
        {
            true => string.Concat(separators.ToUpper(), trimmed[..visibleChars]),
            _ => string.Join(separators.ToUpper(), trimmed[..visibleChars], trimmed[^visibleChars..])
        };
    }
}
