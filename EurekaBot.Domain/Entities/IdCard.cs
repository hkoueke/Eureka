using System;
using System.Linq;
using ErrorOr;
using EurekaBot.Domain.Entities.Shared.Primitives;
using EurekaBot.Domain.Services;

namespace EurekaBot.Domain.Entities;

public sealed class IdCard : Document
{
    public const int CardNumberMaxLength = 16;

    private IdCard(string cardNumber, string owner) : base(Guid.NewGuid(), owner)
    {
        CardNumber = cardNumber;
    }

    public string CardNumber { get; }

    public static ErrorOr<IdCard> CreateNew(string identifier, string owner)
    {
        ErrorOr<string> identifierResult = Parser.Parse(identifier, Predicates.IdentifierPredicate);

        ErrorOr<string> ownerResult = Parser.Parse(owner, Predicates.FullNamePredicate);

        Error[] errors = new[] { identifierResult, ownerResult }
        .Where(e => e.IsError)
        .SelectMany(x => x.Errors)
        .ToArray();

        return errors.Any()
            ? errors
            : new IdCard(identifierResult.Value, ownerResult.Value);
    }
}
