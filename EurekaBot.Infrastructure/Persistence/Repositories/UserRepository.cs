using System;
using EurekaBot.Application.Repositories;
using EurekaBot.Infrastructure.Persistence.Specifications;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EurekaBot.Domain.Entities.Users;

namespace EurekaBot.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public void AddUser(User userToAdd)
    {
        Add(userToAdd);
    }

    public void UpdateUser(User userToUpdate)
    {
        Update(userToUpdate);
    }

    public void RemoveUser(User userToRemove)
    {
        Remove(userToRemove);
    }

    public async Task<User?> GetAsync(
        Expression<Func<User, bool>> predicate,
        bool trackChanges = false,
        CancellationToken cancellationToken = default)
    {
        return await
            ApplySpecification(new UserItemsSpecification(predicate), trackChanges)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
