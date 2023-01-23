using System.Collections.Generic;
using EurekaBot.Domain.Entities.Shared;

namespace EurekaBot.Domain.Entities;

public sealed class Permission
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public IEnumerable<Role> Roles { get; set; } = new List<Role>();
}
