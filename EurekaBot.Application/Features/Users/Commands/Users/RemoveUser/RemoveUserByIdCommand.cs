using ErrorOr;
using EurekaBot.Application.Abstractions.Cqrs;

namespace EurekaBot.Application.Features.Users.Commands.Users.RemoveUser;

public sealed record RemoveUserByIdCommand(long TelegramId) : ICommand<Success>;
