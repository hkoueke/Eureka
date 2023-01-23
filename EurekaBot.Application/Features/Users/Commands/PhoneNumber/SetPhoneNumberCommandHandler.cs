using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using EurekaBot.Application.Abstractions.Cqrs;
using EurekaBot.Application.Repositories;
using EurekaBot.Domain.Entities.Users;
using EurekaBot.Domain.Errors;

namespace EurekaBot.Application.Features.Users.Commands.PhoneNumber;

internal sealed class SetPhoneNumberCommandHandler : ICommandHandler<SetPhoneNumberCommand, Created>
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public SetPhoneNumberCommandHandler(IUserRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Created>> Handle(SetPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        User? user = await
            _repository.GetAsync(x => x.TelegramId == request.TelegramId, true, cancellationToken);

        if (user is null)
        {
            return Errors.User.NotFound;
        }

        var setPhoneNumberResult = user.SetPhoneNumber(request.PhoneNumber);

        if (setPhoneNumberResult.IsError)
        {
            return setPhoneNumberResult.Errors;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Created;
    }
}
