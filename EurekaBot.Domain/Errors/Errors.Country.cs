using ErrorOr;

namespace EurekaBot.Domain.Errors;

public partial class Errors
{
    public static class Country
    {
        public static readonly Error NotFound = Error.NotFound(
           "Country.NotFound",
           "Country was not found.");

        public static readonly Error AlreadyExists = Error.Conflict(
           "Country.AlreadyExists",
           "Country already exists.");

        public static readonly Error InvalidIsoCode = Error.Validation(
           "Country.InvalidIsoCode",
           "Country ISO 3166-1 alpha-2 code is invalid.");

        public static readonly Error NotSupported = Error.Failure(
           "Country.NotSupported",
           "Country ISO 3166-1 alpha-2 code is not supported.");
    }
}


