using EurekaBot.Telegram;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var host = CreateHostBuilder(args).Build();

host.Run();


static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
