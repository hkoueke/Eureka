using EurekaBot.Domain.Entities;
using EurekaBot.Domain.Entities.Shared;
using EurekaBot.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaBot.Infrastructure.Persistence.Configurations;

internal sealed class RoleConfiguration : ConfigurationBase<Role>
{
    public RoleConfiguration() : base(TableNames.Roles)
    {
    }

    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder
            .Property(p => p.Name)
            .HasMaxLength(Role.NameMaxLength);

        builder
            .HasMany(r => r.Permissions)
            .WithMany(p => p.Roles)
            .UsingEntity<RolePermission>();

        builder
            .HasMany(r => r.Users)
            .WithMany(u => u.Roles)
            .UsingEntity<UserRole>(j =>
            {
                j.ToTable(TableNames.UserRoles);
                j.HasKey(x => new { x.UserId, x.RoleId });
            });

        builder.HasData(Role.GetValues());
    }
}
