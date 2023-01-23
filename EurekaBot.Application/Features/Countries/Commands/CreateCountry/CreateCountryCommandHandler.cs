using System;
using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using EurekaBot.Application.Abstractions;
using EurekaBot.Application.Abstractions.Cqrs;
using EurekaBot.Application.Repositories;
using EurekaBot.Domain.Entities.Shared;
using DomainErrors = EurekaBot.Domain.Errors.Errors;

namespace EurekaBot.Application.Features.Countries.Commands.CreateCountry;


internal sealed class CreateCountryCommandHandler : ICommandHandler<CreateCountryCommand, Guid>
{
    private readonly ICountryRepository _repository;
    private readonly IRegionCodeEvaluator _regionEvaluator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCountryCommandHandler(
        ICountryRepository repository,
        IUnitOfWork unitOfWork,
        IRegionCodeEvaluator regionEvaluator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _regionEvaluator = regionEvaluator;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var countryResult = Country.CreateNew(request.IsoCode);

        if (countryResult.IsError)
        {
            return countryResult.Errors;
        }

        if (!_regionEvaluator.IsCountrySupported(request.IsoCode))
        {
            return DomainErrors.Country.NotSupported;
        }

        if (await _repository.GetAsync(
                x => x.Iso2Code == request.IsoCode, 
                cancellationToken: cancellationToken) is not null)
        {
            return DomainErrors.Country.AlreadyExists;
        }

        _repository.AddCountry(countryResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return countryResult.Value.Id;
    }
}
