using System;
using System.Collections.Generic;
using EurekaBot.Domain.Abstractions;

namespace EurekaBot.Domain.Entities.Shared.Primitives;

public abstract class AggregateRoot : Entity, IAuditable
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(Guid id) : base(id)
    {
    }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? EditedOnUtc { get; set; }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
