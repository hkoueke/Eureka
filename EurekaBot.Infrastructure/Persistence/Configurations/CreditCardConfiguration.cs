using EurekaBot.Domain.Entities;
using EurekaBot.Domain.Entities.Shared.Primitives;
using EurekaBot.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaBot.Infrastructure.Persistence.Configurations;

internal sealed class CreditCardConfiguration : ConfigurationBase<CreditCard>
{
    public CreditCardConfiguration() : base(TableNames.CreditCards)
    {
    }

    public override void Configure(EntityTypeBuilder<CreditCard> builder)
    {
        base.Configure(builder);

        builder.HasBaseType<Document>();

        builder
            .Property(x => x.CardNumber)
            .HasMaxLength(CreditCard.CardNumberMaxLength)
            .IsRequired();

        builder
            .Property(x => x.Issuer)
            .HasMaxLength(CreditCard.IssuerNameMaxLength)
            .IsRequired();
    }
}
