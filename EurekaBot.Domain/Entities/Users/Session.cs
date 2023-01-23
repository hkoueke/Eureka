using System;
using EurekaBot.Domain.Entities.Shared.Primitives;

namespace EurekaBot.Domain.Entities.Users;

public sealed class Session : Entity
{
    public Session() : base(Guid.NewGuid())
    {
    }

    public Guid UserId { get; private set; }

    public string? CurrentContext { get; set; }

    public string? CurrentState { get; set; }

    public string? ContextData { get; set; }

    public User User { get; private set; } = default!;
}
