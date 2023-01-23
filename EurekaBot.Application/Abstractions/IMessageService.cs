using System;
using System.Threading;
using System.Threading.Tasks;
using EurekaBot.Domain.Entities.Users;

namespace EurekaBot.Application.Abstractions;

public interface IMessageService
{
    Task SendPostCreatedMessageAsync(Guid userId, Post post, CancellationToken cancellationToken);

}
