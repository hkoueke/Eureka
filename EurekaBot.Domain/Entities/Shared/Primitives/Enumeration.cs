using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EurekaBot.Domain.Entities.Shared.Primitives;

public abstract class Enumeration<TEnum> : Entity, IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<Guid, TEnum> Enumerations = CreateEnumerations();

    protected Enumeration(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; protected init; }

    public static TEnum? FromValue(Guid key)
    {
        return Enumerations.TryGetValue(key, out TEnum? enumeration) ? enumeration : default;
    }

    public static TEnum? FromName(string name)
    {
        return Enumerations.Values.SingleOrDefault(x => x.Name == name);
    }

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType()
            && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration<TEnum> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }

    public override string ToString()
    {
        return Name;
    }

    public static IEnumerable<TEnum> GetValues()
    {
        return Enumerations.Select(x => x.Value);
    }

    private static Dictionary<Guid, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fieldsForType = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Id);
    }
}
