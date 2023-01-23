using System.Linq;
using EurekaBot.Domain.Entities.Shared.Primitives;
using Microsoft.EntityFrameworkCore;

namespace EurekaBot.Infrastructure.Persistence.Specifications.Shared;

internal static class SpecificationEvaluator
{
    public static IQueryable<T> GetQuery<T>(
        IQueryable<T> inputQueryable,
        Specification<T> specification,
        bool trackChanges = default) where T : Entity
    {

        var queryable = trackChanges
            ? inputQueryable.AsTracking()
            : inputQueryable.AsNoTrackingWithIdentityResolution();


        if (specification.Criteria is not null)
        {
            queryable = queryable.Where(specification.Criteria);
        }

        queryable = specification
            .IncludeExpressions
            .Aggregate(queryable, (current, includeExpression) => current.Include(includeExpression));

        if (specification.OrderByExpression is not null)
        {
            queryable = queryable.OrderBy(specification.OrderByExpression);
        }
        else if (specification.OrderByDescendingExpression is not null)
        {
            queryable = queryable.OrderBy(specification.OrderByDescendingExpression);
        }

        return queryable;
    }
}
