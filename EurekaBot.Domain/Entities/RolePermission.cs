using System;

namespace EurekaBot.Domain.Entities;

public sealed class RolePermission
{
    public Guid RoleId { get; set; }
    public int PermissionId { get; set; }
}
