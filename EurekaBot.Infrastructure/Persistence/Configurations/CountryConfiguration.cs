using System;
using System.Collections.Generic;
using System.Linq;
using EurekaBot.Domain.Entities.Shared;
using EurekaBot.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaBot.Infrastructure.Persistence.Configurations;

internal sealed class CountryConfiguration : ConfigurationBase<Country>
{
    public CountryConfiguration() : base(TableNames.Countries)
    {
    }

    public override void Configure(EntityTypeBuilder<Country> builder)
    {
        base.Configure(builder);

        builder.HasKey(c => c.Id);

        builder
            .Property(x => x.Iso2Code)
            .HasMaxLength(2)
            .IsRequired();

        var countries = GetCountries();

        builder.HasData(countries);
    }

    private static IEnumerable<Country> GetCountries()
    {
        return Array
            .Empty<string>()
            .Append("CM")
            .Select(x => Country.CreateNew(x).Value);
    }
}