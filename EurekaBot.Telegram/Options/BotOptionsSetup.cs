using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EurekaBot.Telegram.Options
{
    internal sealed class BotOptionsSetup : IConfigureOptions<BotOptions>
    {
        private const string SectionName = "BotOptions";
        private readonly IConfiguration _configuration;

        public BotOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(BotOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
            options.Token = _configuration["Token"];
            options.SecretKey = _configuration["SecretKey"];
        }
    }
}
