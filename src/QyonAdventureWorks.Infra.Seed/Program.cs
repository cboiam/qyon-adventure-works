using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QyonAdventureWorks.Core;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using QyonAdventureWorks.Infra.MySql;
using QyonAdventureWorks.Infra.Seed.Fakers;
using System;

namespace QyonAdventureWorks.Infra.Seed
{
    class Program
    {
        static void Main()
        {
            var configurations = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            
            IServiceProvider services = new ServiceCollection()
                .AddLogging()
                .RegisterCoreDependencies()
                .RegisterMySqlDependencies(configurations)
                .BuildServiceProvider();

            var driverRepository = services.GetService<IDriverRepository>();
            var drivers = driverRepository.Seed();
            drivers.Wait();

            var circuitRepository = services.GetService<ICircuitRepository>();
            var circuits = circuitRepository.Seed();
            circuits.Wait();

            var raceHistoryRepository = services.GetService<IRaceHistoryRepository>();
            raceHistoryRepository.Seed(drivers, circuits)
                .Wait();
        }
    }
}
