using ErrorOr;
using MediatR;

namespace EurekaBot.Application.Abstractions.Cqrs;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, ErrorOr<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
