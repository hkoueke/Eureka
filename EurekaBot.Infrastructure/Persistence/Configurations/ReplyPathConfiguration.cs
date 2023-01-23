using EurekaBot.Domain.Entities.Users;
using EurekaBot.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaBot.Infrastructure.Persistence.Configurations;


internal sealed class ReplyPathConfiguration : ConfigurationBase<ReplyPath>
{
    public ReplyPathConfiguration() : base(TableNames.ReplyPaths)
    {
    }

    public override void Configure(EntityTypeBuilder<ReplyPath> builder)
    {
        base.Configure(builder);

        builder.
            Property(p => p.ChatId)
            .IsRequired();
    }
}
