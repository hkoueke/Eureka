using System;
using System.Collections.Generic;
using System.Linq;

namespace EurekaBot.Domain.Entities.Shared.Primitives;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        return obj?.GetType() == GetType() &&
            obj is ValueObject other &&
            GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public bool Equals(ValueObject? other)
    {
        return Equals(other as object);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(v => v?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
        => Equals(left, right);

    public static bool operator !=(ValueObject? left, ValueObject? right)
        => !(left == right);
}
