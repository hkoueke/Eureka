using System;
using System.Linq;
using ErrorOr;
using EurekaBot.Domain.Entities.Shared.Primitives;
using EurekaBot.Domain.Services;

namespace EurekaBot.Domain.Entities;

public sealed class CreditCard : Document
{
    public const int CardNumberMaxLength = 16;
    public const int IssuerNameMaxLength = 50;

    private CreditCard(string cardNumber, string owner, string issuer)
        : base(Guid.NewGuid(), owner)
    {
        CardNumber = cardNumber;
        Issuer = issuer;
    }

    public string CardNumber { get; }

    public string Issuer { get; }

    public static ErrorOr<CreditCard> CreateNew(string cardNumber, string owner, string issuer)
    {
        var creditCardResult = Parser.Parse(cardNumber, Predicates.CreditCardNumberPredicate);

        var ownerResult = Parser.Parse(owner, Predicates.FullNamePredicate);

        var issuerResult = Parser.Parse(issuer, Predicates.FullNamePredicate);

        Error[] errors = new[] { creditCardResult, ownerResult, issuerResult }
        .Where(e => e.IsError)
        .SelectMany(x => x.Errors)
        .ToArray();

        return errors.Any()
            ? errors
            : new CreditCard(creditCardResult.Value, ownerResult.Value, issuerResult.Value);
    }
}
