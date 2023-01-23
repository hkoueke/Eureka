using System;
using EurekaBot.Application.Abstractions.Cqrs;

namespace EurekaBot.Application.Features.Users.Commands.Posts.IDCards;

public sealed record AddIdCardPostCommand(long TelegramId, string Owner, string Identifier) : ICommand<Guid>;
