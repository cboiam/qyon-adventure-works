using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Interfaces.Queries;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Queries
{
    public partial class DriverQuery : IDriverQuery
    {
        private readonly IDriverRepository driverRepository;

        public DriverQuery(IDriverRepository driverRepository)
        {
            this.driverRepository = driverRepository;
        }

        public async Task<Driver> Get(int id) => await driverRepository.Get(id);
        public async Task<IEnumerable<Driver>> Query(bool? veteran) => await driverRepository.Query(veteran);
    }
}
