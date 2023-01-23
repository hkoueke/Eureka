using EurekaBot.Domain.Abstractions;

namespace EurekaBot.Domain.Events;

public sealed record UserBanStatusChangedDomainEvent(
    long ManagedUserTelegramId,
    bool IsBanned,
    long ChatId,
    int MessageId) : IDomainEvent;
