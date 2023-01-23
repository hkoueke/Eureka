using System;
using ErrorOr;
using EurekaBot.Application.Abstractions;
using EurekaBot.Domain.Errors;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace EurekaBot.Infrastructure.Services
{
    internal sealed class RegionCodeEvaluator : IRegionCodeEvaluator
    {
        private static readonly Regex Validator = new(
            pattern: RegexString,
            options: RegexOptions.Compiled
            | RegexOptions.IgnorePatternWhitespace
            | RegexOptions.CultureInvariant);

        private const string RegexString
            = @"(\u00a9|\u00ae|[\u2000-\u3300]|\ud83c[\ud000-\udfff]|\ud83d[\ud000-\udfff]|\ud83e[\ud000-\udfff])";

        public ErrorOr<string> GetFlagEmojiFromCountryCode(string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode) || countryCode.Length != 2)
            {
                return Errors.Country.InvalidIsoCode;
            }

            return string.Concat(
                countryCode
                .ToUpper()
                .Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));
        }

        public ErrorOr<string> GetCountryDisplayName(string countryCode)
        {
            RegionInfo? regionInfo = GetRegionInfoFromIsoCode(countryCode);

            return regionInfo?.DisplayName ?? (ErrorOr<string>)Errors.Country.NotFound;
        }

        public bool IsCountrySupported(string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode) || countryCode.Length != 2)
            {
                return false;
            }

            RegionInfo? regionInfo = GetRegionInfoFromIsoCode(countryCode);
            return regionInfo is not null;
        }

        public static bool IsValidFlagEmoji(string emojiString)
        {
            return !string.IsNullOrWhiteSpace(emojiString) && 
                   Validator.IsMatch(emojiString);
        }

        private static RegionInfo? GetRegionInfoFromIsoCode(string countryCode)
        {
            var regionInfos =
                CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));

            return regionInfos
                .FirstOrDefault(region => region.TwoLetterISORegionName
                                                .Equals(countryCode, StringComparison.InvariantCulture));
        }
    }
}
