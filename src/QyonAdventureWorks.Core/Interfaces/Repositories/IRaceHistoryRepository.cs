using QyonAdventureWorks.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Interfaces.Repositories
{
    public interface IRaceHistoryRepository
    {
        Task<RaceHistory> Add(RaceHistory raceHistory, CancellationToken cancellationToken);
        Task Update(RaceHistory raceHistory, CancellationToken cancellationToken);
    }
}
