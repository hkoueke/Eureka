using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using EurekaBot.Application.Abstractions.Cqrs;
using EurekaBot.Application.Repositories;
using EurekaBot.Domain.Entities.Shared;

namespace EurekaBot.Application.Features.Countries.Queries.GetCountries;

internal sealed class GetCountriesQueryHandler : IQueryHandler<GetCountriesQuery, IEnumerable<Country>>
{
    private readonly ICountryRepository _countryRepository;

    public GetCountriesQueryHandler(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<ErrorOr<IEnumerable<Country>>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = await _countryRepository.GetAllAsync(cancellationToken: cancellationToken);
        return countries.ToList();
    }
}