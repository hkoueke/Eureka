using EurekaBot.Domain.Abstractions;

namespace EurekaBot.Domain.Events;

public sealed record PostDeletedDomainEvent(long ChatId, int MessageId) : IDomainEvent;
