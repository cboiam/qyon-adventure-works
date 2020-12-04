using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Infra.MySql.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        DbSet<T> DbSet { get; }
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity, CancellationToken cancellationToken);
        Task<T> Update(T entity, CancellationToken cancellationToken);
        Task<T> Delete(int id, CancellationToken cancellationToken);
    }
}
