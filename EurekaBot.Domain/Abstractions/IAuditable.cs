using System;

namespace EurekaBot.Domain.Abstractions;

public interface IAuditable
{
    DateTime CreatedOnUtc { get; set; }
    DateTime? EditedOnUtc { get; set; }
}
