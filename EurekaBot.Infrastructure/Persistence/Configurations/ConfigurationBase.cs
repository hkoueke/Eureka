using EurekaBot.Domain.Entities.Shared.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaBot.Infrastructure.Persistence.Configurations;

internal abstract class ConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity
{
    private readonly string _tableName;

    protected internal ConfigurationBase(string tableName) => _tableName = tableName;

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(_tableName);

        builder
            .Property(p => p.Id)
            .ValueGeneratedNever();
    }
}