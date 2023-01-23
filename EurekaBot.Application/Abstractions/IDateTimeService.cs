using System;

namespace EurekaBot.Application.Abstractions;

public interface IDateTimeService
{
    DateTime UtcNow { get; }

    string GetTimeZoneDateTime(DateTime dateTime, string countryCode);
}
