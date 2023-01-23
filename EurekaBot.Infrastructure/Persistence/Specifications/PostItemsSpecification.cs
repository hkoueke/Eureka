using System;
using EurekaBot.Infrastructure.Persistence.Specifications.Shared;
using System.Linq.Expressions;
using EurekaBot.Domain.Entities.Users;

namespace EurekaBot.Infrastructure.Persistence.Specifications;

internal sealed class PostItemsSpecification : Specification<Post>
{
    internal PostItemsSpecification(Expression<Func<Post, bool>>? criteria = null) : base(criteria)
    {
        AddInclude(x => x.User);
        AddInclude(x => x.Document);
        AddInclude(x => x.PostCountry);
        OrderByDescending(x => x.CreatedOnUtc);
    }
}
