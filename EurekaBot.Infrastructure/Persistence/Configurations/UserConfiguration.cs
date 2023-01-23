using EurekaBot.Domain.Entities.Users;
using EurekaBot.Infrastructure.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EurekaBot.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration : ConfigurationBase<User>
{
    public UserConfiguration() : base(TableNames.Users)
    {
    }

    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.HasKey(u => u.Id);

        builder
            .Property(p => p.TelegramId)
            .IsRequired();

        builder
            .Property(p => p.FirstName)
            .HasMaxLength(64)
            .IsRequired();

        builder
            .Property(p => p.Username)
            .HasMaxLength(32)
            .IsRequired(false);

        builder
            .Property(p => p.PhoneNumber)
            .HasMaxLength(30)
            .IsRequired(false);

        builder
            .Property(p => p.LanguageCode)
            .HasMaxLength(2)
            .IsRequired(false);

        builder
            .Property(p => p.CreatedOnUtc)
            .IsRequired();

        builder
            .Property(p => p.EditedOnUtc)
            .IsRequired(false);

        builder
            .HasOne(u => u.Session)
            .WithOne(s => s.User)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasOne(u => u.ReplyPath)
            .WithOne(r => r.User)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasOne(u => u.Country)
            .WithMany();

    }
}