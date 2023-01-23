using EurekaBot.Domain.Entities;
using EurekaBot.Domain.Entities.Shared.Primitives;
using EurekaBot.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaBot.Infrastructure.Persistence.Configurations;

internal sealed class PassportConfiguration : ConfigurationBase<Passport>
{
    public PassportConfiguration() : base(TableNames.Passports)
    {
    }

    public override void Configure(EntityTypeBuilder<Passport> builder)
    {
        base.Configure(builder);

        builder.HasBaseType<Document>();

        builder
            .Property(p => p.PassportNumber)
            .HasMaxLength(Passport.PassportNumberMaxLength)
            .IsRequired();
    }
}