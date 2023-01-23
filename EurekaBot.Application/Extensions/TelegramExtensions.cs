using System;
using System.Linq;
using EurekaBot.Domain.Entities.Users;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace EurekaBot.Application.Extensions;

public static class TelegramExtensions
{
    public static Domain.Entities.Users.User ToUserEntity(this User user)
    {
        return new Domain.Entities.User(user.Id, user.FirstName)
        {
            Username = user.Username,
            LanguageCode = user.LanguageCode,
        };
    }

    public static bool HasCommand(this Update update)
    {
        return
            update.Type == UpdateType.Message
            && update.Message!.Entities is not null
            && update.Message!.Entities.Any(x => x.Type == MessageEntityType.BotCommand);
    }

    public static bool IsKicked(this ChatMemberUpdated member)
    {
        return
            member.OldChatMember.Status == ChatMemberStatus.Member &&
            member.NewChatMember.Status == ChatMemberStatus.Kicked;
    }

    public static User GetTelegramUser(this Update update)
    {
        // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
        return update.Type switch
        {
            UpdateType.Message => update.Message!.From!,
            UpdateType.InlineQuery => update.InlineQuery!.From,
            UpdateType.ChosenInlineResult => update.ChosenInlineResult!.From,
            UpdateType.CallbackQuery => update.CallbackQuery!.From,
            UpdateType.MyChatMember => update.MyChatMember!.From,
            _ => throw new NotImplementedException()
        };
    }
}
