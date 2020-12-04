using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using QyonAdventureWorks.Infra.MySql.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Infra.MySql.Repositories
{
    public class RaceHistoryRepository : IRaceHistoryRepository
    {
        private readonly IRepository<RaceHistory> repository;

        public RaceHistoryRepository(IRepository<RaceHistory> repository)
        {
            this.repository = repository;
        }

        public async Task<RaceHistory> Add(RaceHistory raceHistory, CancellationToken cancellationToken)
        {
            return await repository.Add(raceHistory, cancellationToken);
        }

        public async Task Update(RaceHistory raceHistory, CancellationToken cancellationToken)
        {
            await repository.Update(raceHistory, cancellationToken);
        }
    }
}
