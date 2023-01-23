using System;
using EurekaBot.Infrastructure.Persistence.Specifications.Shared;
using System.Linq.Expressions;
using EurekaBot.Domain.Entities.Users;

namespace EurekaBot.Infrastructure.Persistence.Specifications;

internal sealed class UserItemsSpecification : Specification<User>
{
    internal UserItemsSpecification(Expression<Func<User, bool>>? criteria = null)
        : base(criteria)
    {
        AddInclude(x => x.Session);
        AddInclude(x => x.ReplyPath);
        AddInclude(x => x.Posts);
        OrderBy(x => x.FirstName);
    }
}
