using QyonAdventureWorks.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Interfaces.Repositories
{
    public interface ICircuitRepository
    {
        Task<Circuit> Add(Circuit circuit, CancellationToken cancellationToken);
        Task<Circuit> Get(int id);
        Task Update(Circuit circuit, CancellationToken cancellationToken);
        Task<Circuit> Delete(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Circuit>> Query(bool? used);
    }
}
