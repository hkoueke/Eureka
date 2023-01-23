using System.Collections.Generic;
using EurekaBot.Application.Abstractions.Cqrs;
using EurekaBot.Domain.Entities.Shared;

namespace EurekaBot.Application.Features.Countries.Queries.GetCountries;

public sealed record GetCountriesQuery() : IQuery<IEnumerable<Country>>;
