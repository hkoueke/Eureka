using System;
using EurekaBot.Application.Abstractions.Cqrs;

namespace EurekaBot.Application.Features.Users.Commands.Posts.CreditCards;

public sealed record AddCreditCardPostCommand(
    long TelegramId,
    string Owner,
    string CardNumber,
    string Issuer) : ICommand<Guid>;