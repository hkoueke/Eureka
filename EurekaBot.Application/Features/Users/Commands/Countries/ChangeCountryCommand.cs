using System;
using ErrorOr;
using EurekaBot.Application.Abstractions.Cqrs;

namespace EurekaBot.Application.Features.Users.Commands.Countries;

public sealed record ChangeCountryCommand(long TelegramId, Guid CountryId) : ICommand<Updated>;
