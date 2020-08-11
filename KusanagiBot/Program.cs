using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace KusanagiBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder()
                .ConfigureServices((hostContent, services) =>
                {
                    services.Configure<Config>(hostContent.Configuration);
                }).RunConsoleAppFrameworkAsync<KusanagiBot>(args);
        }
    }
}
