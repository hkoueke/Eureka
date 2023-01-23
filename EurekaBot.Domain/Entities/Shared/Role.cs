using System;
using System.Collections.Generic;
using EurekaBot.Domain.Entities.Shared.Primitives;
using EurekaBot.Domain.Entities.Users;

namespace EurekaBot.Domain.Entities.Shared;

public sealed class Role : Enumeration<Role>
{
    public const int NameMaxLength = 30;

    public static readonly Role Registered = new(nameof(Registered));

    public static readonly Role Administrator = new(nameof(Administrator));

    public static readonly Role SuperAdministrator = new(nameof(SuperAdministrator));

    public Role(string name) : base(Guid.NewGuid(), name)
    {
    }

    public IEnumerable<Permission> Permissions { get; set; } = new List<Permission>();

    public IList<User> Users { get; set; } = new List<User>();
}
