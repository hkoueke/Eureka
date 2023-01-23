using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EurekaBot.Domain.Entities.Shared.Primitives;

namespace EurekaBot.Infrastructure.Persistence.Specifications.Shared;

public abstract class Specification<T> where T : Entity
{
    protected internal Specification(Expression<Func<T, bool>>? criteria)
        => Criteria = criteria;

    public Expression<Func<T, bool>>? Criteria { get; }

    public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();

    public Expression<Func<T, object>>? OrderByExpression { get; private set; }

    public Expression<Func<T, object>>? OrderByDescendingExpression { get; private set; }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        IncludeExpressions.Add(includeExpression);
    }

    protected void OrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderByExpression = orderByExpression;
    }

    protected void OrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescendingExpression = orderByDescendingExpression;
    }
}
