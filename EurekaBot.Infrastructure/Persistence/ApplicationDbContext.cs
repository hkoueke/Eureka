using EurekaBot.Domain.Entities.Shared;
using EurekaBot.Domain.Entities.Shared.Primitives;
using EurekaBot.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace EurekaBot.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Country> Countries { get; set; } = default!;
    public DbSet<Session> Sessions { get; set; } = default!;
    public DbSet<ReplyPath> ReplyPaths { get; set; } = default!;
    public DbSet<Document> Documents { get; set; } = default!;
    public DbSet<Role> Roles { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_520_ci")
            .HasCharSet("utf8mb4")
            .ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
