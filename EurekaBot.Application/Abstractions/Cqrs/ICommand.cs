using ErrorOr;
using MediatR;

namespace EurekaBot.Application.Abstractions.Cqrs;

public interface ICommand : IRequest
{
}

public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>
{
}
