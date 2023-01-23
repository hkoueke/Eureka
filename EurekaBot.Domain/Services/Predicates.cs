using System;
using System.Linq;
using ErrorOr;

namespace EurekaBot.Domain.Services;

internal static class Predicates
{
    public static readonly Func<string, ErrorOr<string>> IsoCodePredicate = input =>
    {
        var value = input.Trim();

        return string.IsNullOrEmpty(value) || value.Length != 2
            ? Errors.Errors.Country.InvalidIsoCode
            : value.ToUpper();
    };

    public static readonly Func<string, ErrorOr<string>> IdentifierPredicate = input =>
    {
        var value = input.Trim();

        return string.IsNullOrEmpty(value)
            ? Errors.Errors.Document.EmptyIdentifier
            : value.Length switch
            {
                < 3 => Errors.Errors.Document.IdentifierTooShort,
                > 15 => Errors.Errors.Document.IdentifierTooLong,
                _ => value.ToUpper()
            };
    };

    public static readonly Func<string, ErrorOr<string>> FullNamePredicate = input =>
    {
        var value = input.Trim();

        return string.IsNullOrEmpty(value)
            ? Errors.Errors.Document.EmptyName
            : value.Length switch
            {
                < 3 => Errors.Errors.Document.NameTooShort,
                > 50 => Errors.Errors.Document.NameTooLong,
                _ => value.ToUpper()
            };
    };

    public static readonly Func<string, ErrorOr<string>> CreditCardNumberPredicate = input =>
    {
        var value = input.Trim();

        if (string.IsNullOrEmpty(value))
        {
            return Errors.Errors.Document.EmptyIdentifier;
        }

        if (!IsValidLuhn(value))
        {
            return Errors.Errors.CreditCard.CardNumberInvalid;
        }

        return value;
    };

    private static bool IsValidLuhn(string cardNumber)
    {
        var numbers = cardNumber
            .Replace(" ", "");

        return numbers.All(char.IsDigit) switch
        {
            true => numbers.Select(x => x - '0')
                           .Reverse()
                           .Select((num, idx) => (idx + 1) % 2 == 0 ? Check(num * 2) : num)
                           .Sum() % 10 == 0,
            _ => false
        };

        static int Check(int digit) => digit > 9 ? digit % 10 + 1 : digit;
    }
}
