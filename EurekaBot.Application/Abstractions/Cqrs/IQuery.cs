using ErrorOr;
using MediatR;

namespace EurekaBot.Application.Abstractions.Cqrs;

public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{
}
