using QyonAdventureWorks.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Interfaces.Queries
{
    public interface IDriverQuery
    {
        Task<IEnumerable<Driver>> Query(bool? veteran);
        Task<Driver> Get(int id);
    }
}
