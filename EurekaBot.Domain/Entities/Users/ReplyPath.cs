using System;
using EurekaBot.Domain.Entities.Shared.Primitives;

namespace EurekaBot.Domain.Entities.Users;

public sealed class ReplyPath : Entity
{
    public ReplyPath(long chatId) : base(Guid.NewGuid())
    {
        ChatId = chatId;
    }

    public Guid UserId { get; private set; }

    public long ChatId { get; private set; }

    public User User { get; private set; } = default!;
}
