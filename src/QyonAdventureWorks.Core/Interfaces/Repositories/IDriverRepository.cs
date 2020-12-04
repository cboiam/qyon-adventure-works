using QyonAdventureWorks.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Interfaces.Repositories
{
    public interface IDriverRepository
    {
        Task<Driver> Add(Driver driver, CancellationToken cancellationToken);
        Task<IEnumerable<Driver>> Query(bool? veteran);
        Task<Driver> Get(int id);
        Task Update(Driver driver, CancellationToken cancellationToken);
        Task<Driver> Delete(int id, CancellationToken cancellationToken);
    }
}
