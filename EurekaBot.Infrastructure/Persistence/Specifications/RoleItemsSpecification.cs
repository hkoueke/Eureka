using System;
using EurekaBot.Infrastructure.Persistence.Specifications.Shared;
using System.Linq.Expressions;
using EurekaBot.Domain.Entities.Shared;

namespace EurekaBot.Infrastructure.Persistence.Specifications;

internal sealed class RoleItemsSpecification : Specification<Role>
{
    internal RoleItemsSpecification(Expression<Func<Role, bool>>? criteria = null) : base(criteria)
    {
        OrderBy(r => r.Id);
        AddInclude(r => r.Permissions);
    }
}
