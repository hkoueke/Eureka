using System;
using EurekaBot.Infrastructure.Persistence.Specifications.Shared;
using System.Linq.Expressions;
using EurekaBot.Domain.Entities.Shared;

namespace EurekaBot.Infrastructure.Persistence.Specifications;

internal sealed class CountryItemsSpecification : Specification<Country>
{
    internal CountryItemsSpecification(Expression<Func<Country, bool>>? criteria = null)
        : base(criteria)
    {
        OrderBy(c => c.Iso2Code);
    }
}
