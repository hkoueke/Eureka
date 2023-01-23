using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using EurekaBot.Application.Abstractions.Cqrs;
using EurekaBot.Application.Repositories;
using EurekaBot.Domain.Entities.Shared;
using EurekaBot.Domain.Errors;

namespace EurekaBot.Application.Features.Countries.Queries.GetCountryById;

internal sealed class GetCountryByIdQueryHandler : IQueryHandler<GetCountryByIdQuery, Country>
{
    private readonly ICountryRepository _repository;

    public GetCountryByIdQueryHandler(ICountryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Country>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var country =
            await _repository.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (country is null)
        {
            return Errors.Country.NotFound;
        }

        return country;
    }
}
