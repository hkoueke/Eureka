using System;

namespace EurekaBot.Domain.Entities.Shared.Primitives;

public abstract class Entity : IEquatable<Entity>
{
    protected Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public override bool Equals(object? obj)
    {
        return obj is Entity entity && entity.Id == Id;
    }

    public bool Equals(Entity? other)
    {
        return Equals(other as object);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }

    public static bool operator ==(Entity? first, Entity? second)
        => Equals(first, second);

    public static bool operator !=(Entity? first, Entity? second)
        => !Equals(first, second);

    public override string ToString()
    {
        return $"Type = {GetType().FullName} | ID = {Id}";
    }
}
