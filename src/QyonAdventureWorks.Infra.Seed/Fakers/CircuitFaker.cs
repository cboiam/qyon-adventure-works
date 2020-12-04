using Bogus;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Infra.Seed.Fakers
{
    internal static class CircuitFaker
    {
        internal static async Task<IEnumerable<Circuit>> Seed(this ICircuitRepository circuitRepository)
        {
            var data = GetFaker().Generate(10);

            var cancellationToken = new CancellationToken();
            var circuit = await circuitRepository.Insert(data, cancellationToken);

            return circuit;
        }

        internal static async Task<IEnumerable<Circuit>> Insert(this ICircuitRepository circuitRepository, IEnumerable<Circuit> data, CancellationToken cancellationToken)
        {
            var circuits = new List<Circuit>();

            foreach (var item in data)
            {
                circuits.Add(await circuitRepository.Add(item, cancellationToken));
            }

            return circuits;
        }

        private static Faker<Circuit> GetFaker()
        {
            return new Faker<Circuit>()
                .CustomInstantiator(f => 
                    new Circuit(default, f.Address.City()));
        }
    }
}
