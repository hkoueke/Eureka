namespace EurekaBot.Telegram.Options;

public sealed class BotOptions
{
    public string Token { get; set; } = default!;
    public string SecretKey { get; set; } = default!;
    public string HostAddress { get; init; } = default!;
    public string Route { get; init; } = "/bot";
}
