using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EurekaBot.Domain.Entities.Shared;

namespace EurekaBot.Application.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetAsync(
        Expression<Func<Role, bool>> predicate,
        bool trackChanges = default,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Role>> GetAllAsync(
        Expression<Func<Role, bool>>? criteria,
        bool trackChanges = default,
        CancellationToken cancellationToken = default);

    void AddRole(Role roleToAdd);

    void UpdateRole(Role roleToUpdate);

    void DeleteRole(Role roleToDelete);
}
