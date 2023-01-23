using System;
using System.Collections.Generic;
using EurekaBot.Application.Repositories;
using EurekaBot.Infrastructure.Persistence.Specifications;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EurekaBot.Domain.Entities.Users;

namespace EurekaBot.Infrastructure.Persistence.Repositories;

public sealed class PostRepository : RepositoryBase<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Post?> GetByIdAsync(
        Guid id,
        bool trackChanges = default,
        CancellationToken cancellationToken = default)
    {
        return
            await ApplySpecification(new PostItemsSpecification(x => x.Id == id), trackChanges)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Post>> GetAllAsync(
        Expression<Func<Post, bool>>? criteria = null,
        bool trackChanges = default,
        CancellationToken cancellationToken = default)
    {
        return
            await ApplySpecification(new PostItemsSpecification(criteria), trackChanges)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}