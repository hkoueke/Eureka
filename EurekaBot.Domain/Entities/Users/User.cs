using System;
using System.Collections.Generic;
using ErrorOr;
using System.Linq;
using DomainErrors = EurekaBot.Domain.Errors.Errors;
using EurekaBot.Domain.Entities.Shared;
using EurekaBot.Domain.Entities.Shared.Primitives;
using EurekaBot.Domain.Shared;

namespace EurekaBot.Domain.Entities.Users;

public sealed class User : AggregateRoot
{
    private readonly List<Post> _posts = new();

    public User(long telegramId, string firstName) : base(Guid.NewGuid())
    {
        TelegramId = telegramId;
        FirstName = firstName;
    }

    public Guid? CountryId { get; private set; }

    public long TelegramId { get; private set; }

    public string FirstName { get; private set; }

    public string? Username { get; set; }

    public string? LanguageCode { get; set; }

    public string? PhoneNumber { get; private set; }

    public Session Session { get; private set; } = default!;

    public Country? Country { get; private set; }

    public ReplyPath ReplyPath { get; private set; } = default!;

    public IEnumerable<Post> Posts => _posts;

    public IEnumerable<Role> Roles { get; private set; } = new List<Role>();

    public void SetReplyPath(long chatId)
    {
        ReplyPath = new ReplyPath(chatId);
    }

    public void SetSession(Session session)
    {
        Session = session;
    }

    public ErrorOr<Success> SetPhoneNumber(string phoneNumber)
    {
        if (PhoneNumber == phoneNumber)
        {
            return DomainErrors.User.PhoneNumberAlreadyAssigned;
        }

        PhoneNumber = phoneNumber;

        return Result.Success;
    }

    public ErrorOr<Success> SetCountry(Guid countryId)
    {
        if (countryId == default)
        {
            return DomainErrors.Country.NotSupported;
        }

        if (CountryId == countryId)
        {
            return DomainErrors.User.CountryAlreadySet;
        }

        CountryId = countryId;

        return Result.Success;
    }

    public ErrorOr<Guid> AddPost(Document document)
    {
        Post postToAdd = new();

        ErrorOr<Success> addOwnerResult = postToAdd.AddOwner(this);

        if (addOwnerResult.IsError)
        {
            return addOwnerResult.Errors;
        }

        postToAdd.AddDocument(document);

        _posts.Add(postToAdd);

        return postToAdd.Id;
    }

    public ErrorOr<Success> RemovePost(Post postToRemove)
    {
        if (_posts.All(p => p.Id != postToRemove.Id))
        {
            return DomainErrors.Post.NotFound;
        }

        _posts.Remove(postToRemove);

        return Result.Success;
    }
}