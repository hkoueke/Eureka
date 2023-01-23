using System;
using EurekaBot.Application.Abstractions.Cqrs;

namespace EurekaBot.Application.Features.Users.Commands.Users.CreateUser;

public sealed record CreateUserCommand(Telegram.Bot.Types.User TelegramUser, long ChatId)
    : ICommand<Guid>;
