using System;
using EurekaBot.Application.Abstractions.Cqrs;

namespace EurekaBot.Application.Features.Countries.Commands.CreateCountry;

public sealed record CreateCountryCommand(string IsoCode) : ICommand<Guid>;