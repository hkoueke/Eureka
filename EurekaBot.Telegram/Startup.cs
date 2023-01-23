using EurekaBot.Telegram.Controllers;
using EurekaBot.Telegram.Extensions;
using EurekaBot.Telegram.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace EurekaBot.Telegram;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.InstallServices(
            Configuration,
            typeof(Startup).Assembly,
            Application.AssemblyReference.Assembly,
            Infrastructure.AssemblyReference.Assembly);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        //app.UseHttpsRedirection();

        app.UseRouting();

        app.UseUserDataDeletion()
           .UseUserRegistration()
           .UseRequestCulture();

        app.UseEndpoints(builder =>
        {
            var botOptions =
                app.ApplicationServices.GetRequiredService<IOptions<BotOptions>>().Value;

            builder.MapBotWebhookRoute<BotController>(route: botOptions.Route);
            //builder.MapControllers();
        });
    }
}

