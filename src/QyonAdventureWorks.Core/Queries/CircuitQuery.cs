using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Interfaces.Queries;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Queries
{
    public class CircuitQuery : ICircuitQuery
    {
        private readonly ICircuitRepository circuitRepository;

        public CircuitQuery(ICircuitRepository circuitRepository)
        {
            this.circuitRepository = circuitRepository;
        }

        public async Task<Circuit> Get(int id) => await circuitRepository.Get(id);
        public async Task<IEnumerable<Circuit>> Query(bool? used) => await circuitRepository.Query(used);
    }
}
