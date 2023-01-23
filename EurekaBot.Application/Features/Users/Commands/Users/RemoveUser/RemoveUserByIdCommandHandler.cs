using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using EurekaBot.Application.Abstractions.Cqrs;
using EurekaBot.Application.Repositories;
using EurekaBot.Domain.Entities.Users;
using EurekaBot.Domain.Errors;

namespace EurekaBot.Application.Features.Users.Commands.Users.RemoveUser;

internal sealed class RemoveUserByIdCommandHandler : ICommandHandler<RemoveUserByIdCommand, Success>
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveUserByIdCommandHandler(IUserRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(RemoveUserByIdCommand request, CancellationToken cancellationToken)
    {
        User? user = await
            _repository.GetAsync(x => x.TelegramId == request.TelegramId, false, cancellationToken);

        if (user is null)
        {
            return Errors.User.NotFound;
        }

        _repository.RemoveUser(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
