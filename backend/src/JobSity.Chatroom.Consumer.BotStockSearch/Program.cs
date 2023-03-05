using JobSity.Chatroom.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Consumer.BotStockSearch
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

            return Host.CreateDefaultBuilder(args)
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddSingleton(configuration);
                        services.AddApplication(configuration);
                        services.AddHostedService<BotStockSearchConsumer>();
                    });
        }

    }
}