using System;
using ErrorOr;
using EurekaBot.Domain.Abstractions;
using EurekaBot.Domain.Entities.Shared;
using EurekaBot.Domain.Entities.Shared.Primitives;

namespace EurekaBot.Domain.Entities.Users;

public sealed class Post : Entity, IAuditable
{
    public Post() : base(Guid.NewGuid())
    {
    }

    public Guid UserId { get; private set; }

    public Guid PostCountryId { get; private set; }

    public bool IsArchived { get; set; }

    public Document Document { get; private set; } = default!;

    public User User { get; private set; } = default!;

    public Country PostCountry { get; private set; } = default!;

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? EditedOnUtc { get; set; }

    public void AddDocument(Document document)
    {
        Document = document;
    }

    public ErrorOr<Success> AddOwner(User user)
    {
        if (user.CountryId is null)
        {
            return Errors.Errors.User.CountryNotSet;
        }

        UserId = user.Id;
        PostCountryId = user.CountryId.Value;

        return Result.Success;
    }
}
