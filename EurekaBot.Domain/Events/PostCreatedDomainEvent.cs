using System;
using EurekaBot.Domain.Abstractions;

namespace EurekaBot.Domain.Events;

public sealed record PostCreatedDomainEvent(Guid PostId, long ChatId, int MessageId) : IDomainEvent;