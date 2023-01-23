using System;
using EurekaBot.Domain.Entities.Users;

namespace EurekaBot.Domain.Entities.Shared.Primitives;

public abstract class Document : Entity
{
    public const int OwnerNameMaxLength = 50;

    protected Document(Guid id, string owner) : base(id)
    {
        Owner = owner;
    }

    public Guid PostId { get; private set; }

    public string Owner { get; }

    public Post Post { get; private set; } = default!;
}