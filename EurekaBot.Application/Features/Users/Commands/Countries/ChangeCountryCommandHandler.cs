using System.Threading;
using System.Threading.Tasks;
using ErrorOr;
using EurekaBot.Application.Abstractions.Cqrs;
using EurekaBot.Application.Repositories;
using EurekaBot.Domain.Entities.Shared;
using EurekaBot.Domain.Entities.Users;
using EurekaBot.Domain.Errors;

namespace EurekaBot.Application.Features.Users.Commands.Countries;

internal sealed class ChangeCountryCommandHandler : ICommandHandler<ChangeCountryCommand, Updated>
{
    private readonly IUserRepository _userRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeCountryCommandHandler(
        IUserRepository userRepository,
        ICountryRepository countryRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _countryRepository = countryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Updated>> Handle(ChangeCountryCommand request, CancellationToken cancellationToken)
    {
        User? user = await
            _userRepository.GetAsync(
                x => x.TelegramId == request.TelegramId, 
                true, 
                cancellationToken);

        if (user is null)
        {
            return Errors.User.NotFound;
        }

        Country? country = await
            _countryRepository.GetAsync(x => x.Id == request.CountryId, false, cancellationToken);

        if (country is null)
        {
            return Errors.Country.NotFound;
        }

        var setCountryResult = user.SetCountry(country.Id);

        if (setCountryResult.IsError)
        {
            return setCountryResult.Errors;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}
