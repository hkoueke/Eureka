using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using EurekaBot.Application.Abstractions.Cqrs;
using EurekaBot.Application.Repositories;
using EurekaBot.Domain.Errors;

namespace EurekaBot.Application.Features.Users.Commands.Roles;

internal sealed class ManageRoleCommandHandler : ICommandHandler<ManageRoleCommand, Updated>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ManageRoleCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Updated>> Handle(ManageRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await
            _userRepository.GetAsync(
                x => x.TelegramId == request.TelegramId, 
                false, 
                cancellationToken);

        if (user is null)
        {
            return Errors.User.NotFound;
        }

        var roleToManage = await
            _roleRepository.GetAsync(x => x.Id == request.RoleId, true, cancellationToken);

        if (roleToManage is null)
        {
            return Errors.Role.NotFound;
        }

        switch (request.Remove)
        {
            case true when roleToManage.Users.All(u => u.TelegramId != request.TelegramId):
                return Errors.Role.NotAssigned;
            case false when roleToManage.Users.Any(u => u.TelegramId == request.TelegramId):
                return Errors.Role.AlreadyAssigned;
            case true:
                roleToManage.Users.Remove(user);
                break;
            default:
                roleToManage.Users.Add(user);
                break;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}
