using EurekaBot.Domain.Entities;
using EurekaBot.Domain.Entities.Shared.Primitives;
using EurekaBot.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaBot.Infrastructure.Persistence.Configurations;

internal sealed class IdCardConfiguration : ConfigurationBase<IdCard>
{
    public IdCardConfiguration() : base(TableNames.IdCards)
    {
    }

    public override void Configure(EntityTypeBuilder<IdCard> builder)
    {
        base.Configure(builder);

        builder.HasBaseType<Document>();

        builder
            .Property(x => x.CardNumber)
            .HasMaxLength(IdCard.CardNumberMaxLength)
            .IsRequired();
    }
}
