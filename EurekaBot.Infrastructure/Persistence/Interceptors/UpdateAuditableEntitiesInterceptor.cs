using System.Threading;
using System.Threading.Tasks;
using EurekaBot.Application.Abstractions;
using EurekaBot.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EurekaBot.Infrastructure.Persistence.Interceptors;

internal sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    private readonly IDateTimeService _dateTimeService;

    public UpdateAuditableEntitiesInterceptor(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var entityEntries =
            dbContext.ChangeTracker.Entries<IAuditable>();

        foreach (var entityEntry in entityEntries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(p => p.CreatedOnUtc).CurrentValue = _dateTimeService.UtcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(p => p.EditedOnUtc).CurrentValue = _dateTimeService.UtcNow;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
