using ErrorOr;

namespace EurekaBot.Application.Abstractions;

public interface IRegionCodeEvaluator
{
    ErrorOr<string> GetFlagEmojiFromCountryCode(string countryCode);

    bool IsCountrySupported(string countryCode);

    ErrorOr<string> GetCountryDisplayName(string countryCode);
}
