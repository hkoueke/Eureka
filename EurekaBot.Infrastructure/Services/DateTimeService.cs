using System;
using EurekaBot.Application.Abstractions;
using Microsoft.Extensions.Logging;
using NodaTime;
using NodaTime.TimeZones;
using System.Globalization;
using System.Linq;

namespace EurekaBot.Infrastructure.Services;

internal sealed class DateTimeService : IDateTimeService
{
    private readonly ILogger<DateTimeService> _logger;
    private readonly IDateTimeZoneProvider _zoneProvider;
    private readonly IDateTimeZoneSource _zoneSource;

    public DateTimeService(
        ILogger<DateTimeService> logger,
        IDateTimeZoneProvider zoneProvider,
        IDateTimeZoneSource zoneSource)
    {
        _logger = logger;
        _zoneProvider = zoneProvider;
        _zoneSource = zoneSource;
    }

    public DateTime UtcNow => DateTime.UtcNow;

    public string GetTimeZoneDateTime(DateTime dateTimeUtc, string countryCode)
    {
        var zoneLocation = GetTimeZoneByCountryCode(countryCode);

        if (zoneLocation is null)
        {
            _logger.LogWarning("Zone location with Country code <{countryCode}> not found", countryCode);
            return dateTimeUtc.ToString("ddd d MMM yyyy", CultureInfo.CurrentUICulture);
        }

        if (_zoneProvider.GetZoneOrNull(zoneLocation.ZoneId) is not DateTimeZone zone)
        {
            _logger.LogWarning("DateTime Zone for Zone ID <{ZoneId}> not found", zoneLocation.ZoneId);
            return dateTimeUtc.ToString("ddd d MMM yyyy", CultureInfo.CurrentUICulture);
        }

        ZonedDateTime zonedDate = Instant.FromDateTimeUtc(dateTimeUtc).InZone(zone);

        return zonedDate.ToString("ddd d MMM yyyy", CultureInfo.CurrentUICulture);
    }

    private TzdbZoneLocation? GetTimeZoneByCountryCode(string countryCode)
    {
        if (string.IsNullOrWhiteSpace(countryCode) || countryCode.Length != 2)
        {
            return default;
        }

        return (_zoneSource as TzdbDateTimeZoneSource)
            ?.ZoneLocations
            ?.FirstOrDefault(x => x.CountryCode == countryCode.ToUpper());
    }
}
