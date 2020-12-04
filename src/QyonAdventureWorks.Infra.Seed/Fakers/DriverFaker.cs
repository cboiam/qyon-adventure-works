using Bogus;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Enums;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Infra.Seed.Fakers
{
    internal static class DriverFaker
    {
        internal static async Task<IEnumerable<Driver>> Seed(this IDriverRepository driverRepository)
        {
            var data = GetFaker().Generate(20);

            var cancellationToken = new CancellationToken();
            var drivers = await driverRepository.Insert(data, cancellationToken);
            
            return drivers;
        }

        internal static async Task<IEnumerable<Driver>> Insert(this IDriverRepository driverRepository, IEnumerable<Driver> data, CancellationToken cancellationToken)
        {
            var drivers = new List<Driver>();

            foreach (var item in data)
            {
                drivers.Add(await driverRepository.Add(item, cancellationToken));
            }

            return drivers;
        }

        private static Faker<Driver> GetFaker()
        {
            return new Faker<Driver>()
                .CustomInstantiator(f =>
                    new Driver(f.Name.FirstName(),
                        f.PickRandom<Gender>(),
                        f.Random.Decimal(),
                        f.Random.Decimal(),
                        f.Random.Decimal()));
        }
    }
}
