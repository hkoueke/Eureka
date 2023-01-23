using EurekaBot.Domain.Entities.Shared.Primitives;
using EurekaBot.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaBot.Infrastructure.Persistence.Configurations;

internal sealed class DocumentConfiguration : ConfigurationBase<Document>
{
    public DocumentConfiguration() : base(TableNames.Documents)
    {
    }

    public override void Configure(EntityTypeBuilder<Document> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder
            .Property(p => p.Owner)
            .HasMaxLength(Document.OwnerNameMaxLength)
            .IsRequired();

        builder
            .HasQueryFilter(d => !d.Post.IsArchived);
    }
}
