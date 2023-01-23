using System;
using System.Collections.Generic;
using EurekaBot.Application.Repositories;
using EurekaBot.Infrastructure.Persistence.Specifications;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EurekaBot.Domain.Entities.Shared;

namespace EurekaBot.Infrastructure.Persistence.Repositories;

public sealed class CountryRepository : RepositoryBase<Country>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public void AddCountry(Country countryToAdd)
    {
        Add(countryToAdd);
    }

    public async Task<Country?> GetAsync(
        Expression<Func<Country, bool>> predicate,
        bool trackChanges = false,
        CancellationToken cancellationToken = default)
    {
        return await
            ApplySpecification(new CountryItemsSpecification(predicate), trackChanges)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Country>> GetAllAsync(
        Expression<Func<Country, bool>>? criteria = null,
        bool trackChanges = default,
        CancellationToken cancellationToken = default)
    {
        return await
            ApplySpecification(new CountryItemsSpecification(criteria), trackChanges)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
