namespace EurekaBot.Infrastructure.Persistence.Options;

public sealed class MySqlOptions
{
    public string ConnectionString { get; set; } = default!;
    public int CommandTimeout { get; init; } = 30;
    public int MaxRetryCount { get; init; } = 3;
    public double MaxRetryDelay { get; init; } = 3;
    public bool EnableDetailedErrors { get; init; }
    public bool EnableSensitiveDataLogging { get; init; }
}
