using Bogus;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Infra.Seed.Fakers
{
    internal static class RaceHistoryFaker
    {
        internal static async Task Seed(this IRaceHistoryRepository raceHistoryRepository,
            Task<IEnumerable<Driver>> drivers,
            Task<IEnumerable<Circuit>> circuits)
        {
            var data = GetFaker(await drivers, await circuits).Generate(50);

            var cancellationToken = new CancellationToken();
            await raceHistoryRepository.Insert(data, cancellationToken);
        }

        internal static async Task<IEnumerable<RaceHistory>> Insert(this IRaceHistoryRepository raceHistoryRepository, IEnumerable<RaceHistory> data, CancellationToken cancellationToken)
        {
            var raceHistories = new List<RaceHistory>();

            foreach (var item in data)
            {
                raceHistories.Add(await raceHistoryRepository.Add(item, cancellationToken));
            }

            return raceHistories;
        }

        private static Faker<RaceHistory> GetFaker(IEnumerable<Driver> drivers, IEnumerable<Circuit> circuits)
        {
            return new Faker<RaceHistory>()
                .CustomInstantiator(f =>
                    new RaceHistory(f.PickRandom(drivers.Select(x => x.Id)),
                        f.PickRandom(circuits.Take(6).Select(x => x.Id)),
                        f.Date.Past(),
                        f.Random.Decimal()));
        }
    }
}
