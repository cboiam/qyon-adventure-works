using QyonAdventureWorks.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Interfaces.Queries
{
    public interface ICircuitQuery
    {
        Task<Circuit> Get(int id);
        Task<IEnumerable<Circuit>> Query(bool? used);
    }
}
