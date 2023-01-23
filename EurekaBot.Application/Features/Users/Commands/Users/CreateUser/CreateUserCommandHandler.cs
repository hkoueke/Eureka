using System;
using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using EurekaBot.Application.Abstractions.Cqrs;
using EurekaBot.Application.Extensions;
using EurekaBot.Application.Repositories;
using EurekaBot.Domain.Entities.Shared;
using EurekaBot.Domain.Entities.Users;
using EurekaBot.Domain.Errors;

namespace EurekaBot.Application.Features.Users.Commands.Users.CreateUser;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetAsync(
            x => x.TelegramId == request.TelegramUser.Id,
            cancellationToken: cancellationToken) is not null)
        {
            return Errors.User.AlreadyExists;
        }

        Role? roleToAdd = await
            _roleRepository.GetAsync(x => x.Name == Role.Registered.Name, true, cancellationToken);

        if (roleToAdd is null)
        {
            return Errors.Role.NotFound;
        }

        User userToCreate = request.TelegramUser.ToUserEntity();

        userToCreate.SetSession(new Session());

        userToCreate.SetReplyPath(request.ChatId);

        _userRepository.AddUser(userToCreate);

        roleToAdd.Users.Add(userToCreate);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return userToCreate.Id;
    }
}
