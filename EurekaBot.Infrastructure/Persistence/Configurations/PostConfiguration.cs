using EurekaBot.Domain.Entities.Shared.Primitives;
using EurekaBot.Domain.Entities.Users;
using EurekaBot.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaBot.Infrastructure.Persistence.Configurations;

internal sealed class PostConfiguration : ConfigurationBase<Post>
{
    public PostConfiguration() : base(TableNames.Posts)
    {
    }

    public override void Configure(EntityTypeBuilder<Post> builder)
    {
        base.Configure(builder);

        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.IsArchived)
            .HasDefaultValue(false);

        builder
            .HasOne(p => p.Document)
            .WithOne(d => d.Post)
            .HasForeignKey<Document>(d => d.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasOne(p => p.PostCountry)
            .WithMany();

        builder
            .HasQueryFilter(p => !p.IsArchived);
    }
}
