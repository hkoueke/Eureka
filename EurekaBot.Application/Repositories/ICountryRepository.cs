using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EurekaBot.Domain.Entities.Shared;

namespace EurekaBot.Application.Repositories;

public interface ICountryRepository
{
    Task<Country?> GetAsync(
        Expression<Func<Country, bool>> predicate,
        bool trackChanges = default,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Country>> GetAllAsync(
        Expression<Func<Country, bool>>? criteria = default,
        bool trackChanges = default,
        CancellationToken cancellationToken = default);

    void AddCountry(Country countryToAdd);
}
