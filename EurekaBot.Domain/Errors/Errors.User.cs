using ErrorOr;

namespace EurekaBot.Domain.Errors;

public partial class Errors
{
    public static class User
    {
        public static readonly Error AlreadyExists = Error.Conflict(
            "User.AlreadyExists",
            "User already exists.");

        public static readonly Error NotFound = Error.NotFound(
            "User.NotFound",
            "User was not found.");

        public static readonly Error PhoneNumberAlreadyAssigned = Error.Conflict(
            "User.PhoneNumberAlreadyAssigned",
            "A Phone number is already assigned to this user.");

        public static readonly Error CountryAlreadySet = Error.Conflict(
            "User.CountryAlreadySet",
            "Country is already assigned to user.");

        public static readonly Error CountryNotSet = Error.Conflict(
            "User.CountryNotSet",
            "User has no country assigned to.");
    }
}
