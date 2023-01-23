using ErrorOr;
using EurekaBot.Application.Abstractions.Cqrs;

namespace EurekaBot.Application.Features.Users.Commands.PhoneNumber;

public sealed record SetPhoneNumberCommand(long TelegramId, string PhoneNumber) : ICommand<Created>;
