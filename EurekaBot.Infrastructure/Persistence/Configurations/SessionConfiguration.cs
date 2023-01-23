using EurekaBot.Domain.Entities.Users;
using EurekaBot.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaBot.Infrastructure.Persistence.Configurations;

internal sealed class SessionConfiguration : ConfigurationBase<Session>
{
    public SessionConfiguration() : base(TableNames.Sessions)
    {
    }

    public override void Configure(EntityTypeBuilder<Session> builder)
    {
        base.Configure(builder);

        builder
            .Property(p => p.CurrentContext)
            .HasMaxLength(50)
            .IsRequired(false);

        builder
           .Property(p => p.CurrentState)
           .HasMaxLength(50)
           .IsRequired(false);

        builder
           .Property(p => p.ContextData)
           .HasMaxLength(50)
           .IsRequired(false);
    }
}
