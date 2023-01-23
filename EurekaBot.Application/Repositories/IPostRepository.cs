using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EurekaBot.Domain.Entities.Users;

namespace EurekaBot.Application.Repositories;

public interface IPostRepository
{
    Task<Post?> GetByIdAsync(
        Guid id,
        bool trackChanges = default,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Post>> GetAllAsync(
        Expression<Func<Post, bool>>? criteria,
        bool trackChanges = default,
        CancellationToken cancellationToken = default);
}
