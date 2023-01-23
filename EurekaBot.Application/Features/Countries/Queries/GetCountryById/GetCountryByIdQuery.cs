using System;
using EurekaBot.Application.Abstractions.Cqrs;
using EurekaBot.Domain.Entities.Shared;

namespace EurekaBot.Application.Features.Countries.Queries.GetCountryById;

public sealed record GetCountryByIdQuery(Guid Id) : IQuery<Country>;
