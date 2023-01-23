using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EurekaBot.Domain.Entities.Users;

namespace EurekaBot.Application.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(
        Expression<Func<User, bool>> predicate,
        bool trackChanges = default,
        CancellationToken cancellationToken = default);

    void AddUser(User userToAdd);

    void UpdateUser(User userToUpdate);

    void RemoveUser(User userToRemove);
}
