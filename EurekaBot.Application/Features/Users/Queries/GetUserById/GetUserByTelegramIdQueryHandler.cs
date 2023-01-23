using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using EurekaBot.Application.Abstractions.Cqrs;
using EurekaBot.Application.Repositories;
using EurekaBot.Domain.Entities.Users;
using EurekaBot.Domain.Errors;

namespace EurekaBot.Application.Features.Users.Queries.GetUserById;

internal sealed class GetUserByTelegramIdQueryHandler : IQueryHandler<GetUserByTelegramIdQuery, User>
{
    private readonly IUserRepository _repository;

    public GetUserByTelegramIdQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<User>> Handle(GetUserByTelegramIdQuery request, CancellationToken cancellationToken)
    {
        User? user = await
            _repository.GetAsync(x => x.TelegramId == request.TelegramId, false, cancellationToken);

        if (user is null)
        {
            return Errors.User.NotFound;
        }

        return user;
    }
}
