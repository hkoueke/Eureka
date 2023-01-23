using System;
using System.Collections.Generic;
using EurekaBot.Application.Repositories;
using EurekaBot.Infrastructure.Persistence.Specifications;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Role = EurekaBot.Domain.Entities.Shared.Role;

namespace EurekaBot.Infrastructure.Persistence.Repositories;

public sealed class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public void AddRole(Role roleToAdd)
    {
        Add(roleToAdd);
    }

    public void DeleteRole(Role roleToDelete)
    {
        Remove(roleToDelete);
    }

    public void UpdateRole(Role roleToUpdate)
    {
        Update(roleToUpdate);
    }

    public async Task<Role?> GetAsync(Expression<Func<Role, bool>> predicate,
        bool trackChanges = false,
        CancellationToken cancellationToken = default)
    {
        return await
            ApplySpecification(new RoleItemsSpecification(predicate), trackChanges)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Role>> GetAllAsync(
        Expression<Func<Role, bool>>? criteria,
        bool trackChanges = default,
        CancellationToken cancellationToken = default)
    {
        return await
            ApplySpecification(new RoleItemsSpecification(criteria), trackChanges)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
