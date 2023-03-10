using System;
using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using EurekaBot.Application.Abstractions.Cqrs;
using EurekaBot.Application.Repositories;
using EurekaBot.Domain.Entities;
using EurekaBot.Domain.Entities.Users;
using EurekaBot.Domain.Errors;

namespace EurekaBot.Application.Features.Users.Commands.Posts.CreditCards;

internal sealed class AddCreditCardPostCommandHandler : ICommandHandler<AddCreditCardPostCommand, Guid>
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCreditCardPostCommandHandler(IUserRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Guid>> Handle(AddCreditCardPostCommand request, CancellationToken cancellationToken)
    {
        ErrorOr<CreditCard> docResult =
            CreditCard.CreateNew(request.CardNumber, request.Owner, request.Issuer);

        if (docResult.IsError)
        {
            return docResult.Errors;
        }

        User? postOwner = await
            _repository.GetAsync(x => x.TelegramId == request.TelegramId, true, cancellationToken);

        if (postOwner is null)
        {
            return Errors.User.NotFound;
        }

        ErrorOr<Guid> addPostResult = postOwner.AddPost(docResult.Value);

        if (addPostResult.IsError)
        {
            return addPostResult.Errors;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return addPostResult.Value;
    }
}
