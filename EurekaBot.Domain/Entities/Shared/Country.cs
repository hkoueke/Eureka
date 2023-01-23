using System;
using ErrorOr;
using EurekaBot.Domain.Entities.Shared.Primitives;
using EurekaBot.Domain.Services;

namespace EurekaBot.Domain.Entities.Shared;

public sealed class Country : Entity
{
    private Country(string iso2Code) : base(Guid.NewGuid())
    {
        Iso2Code = iso2Code;
    }

    public string Iso2Code { get; }

    public static ErrorOr<Country> CreateNew(string iso2Code)
    {
        var isoCodeResult = Parser.Parse(iso2Code, Predicates.IsoCodePredicate);

        return isoCodeResult.IsError
            ? isoCodeResult.Errors
            : new Country(isoCodeResult.Value);
    }
}
