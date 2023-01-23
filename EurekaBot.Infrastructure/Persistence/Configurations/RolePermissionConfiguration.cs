using EurekaBot.Domain.Entities;
using EurekaBot.Domain.Entities.Shared;
using EurekaBot.Domain.Entities.Shared.Primitives;
using EurekaBot.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Permission = EurekaBot.Domain.Enums.Permission;

namespace EurekaBot.Infrastructure.Persistence.Configurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable(TableNames.RolePermissions);

        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(
                Create(Role.Registered, Permission.ReadUser),
                Create(Role.Registered, Permission.ReadPost),
                Create(Role.Registered, Permission.CreatePost),
                Create(Role.Registered, Permission.EditUser),
                Create(Role.Registered, Permission.DeletePost),
                Create(Role.Registered, Permission.ClaimPost),
                Create(Role.Administrator, Permission.EditRole),
                Create(Role.SuperAdministrator, Permission.DeleteUser));
    }

    private static RolePermission Create(Entity role, Permission permission)
    {
        return new RolePermission
        {
            RoleId = role.Id,
            PermissionId = (int)permission
        };
    }
}
