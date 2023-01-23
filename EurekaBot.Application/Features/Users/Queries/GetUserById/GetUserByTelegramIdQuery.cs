using EurekaBot.Application.Abstractions.Cqrs;
using EurekaBot.Domain.Entities.Users;

namespace EurekaBot.Application.Features.Users.Queries.GetUserById;

public sealed record GetUserByTelegramIdQuery(long TelegramId) : IQuery<User>;