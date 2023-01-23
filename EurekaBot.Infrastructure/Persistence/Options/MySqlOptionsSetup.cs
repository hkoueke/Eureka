using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EurekaBot.Infrastructure.Persistence.Options;

internal sealed class MySqlOptionsSetup : IConfigureOptions<MySqlOptions>
{
    private readonly IConfiguration _configuration;
    private const string ConfigurationSectionName = "MySqlOptions";
    private const string ConnectionStringSectionName = "MySql";

    public MySqlOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(MySqlOptions options)
    {
        var connectionString = _configuration.GetConnectionString(ConnectionStringSectionName);
        options.ConnectionString = connectionString;
        _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
