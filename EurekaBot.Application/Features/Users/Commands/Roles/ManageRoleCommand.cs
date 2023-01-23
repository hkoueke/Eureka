using System;
using ErrorOr;
using EurekaBot.Application.Abstractions.Cqrs;

namespace EurekaBot.Application.Features.Users.Commands.Roles;

public sealed record ManageRoleCommand(long TelegramId, Guid RoleId, bool Remove) : ICommand<Updated>;
