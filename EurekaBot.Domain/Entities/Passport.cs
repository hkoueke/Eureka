using System;
using EurekaBot.Domain.Entities.Shared.Primitives;

namespace EurekaBot.Domain.Entities;

public sealed class Passport : Document
{
    public const int PassportNumberMaxLength = 15;

    public Passport(string passportNumber, string owner) : base(Guid.NewGuid(), owner)
    {
        PassportNumber = passportNumber;
    }

    public string PassportNumber { get; }
}
