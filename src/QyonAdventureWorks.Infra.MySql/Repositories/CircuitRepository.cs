using Microsoft.EntityFrameworkCore;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using QyonAdventureWorks.Infra.MySql.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace QyonAdventureWorks.Infra.MySql.Repositories
{
    public class CircuitRepository : ICircuitRepository
    {
        private readonly IRepository<Circuit> repository;

        public CircuitRepository(IRepository<Circuit> repository)
        {
            this.repository = repository;
        }

        public async Task<Circuit> Add(Circuit circuit, CancellationToken cancellationToken)
        {
            return await repository.Add(circuit, cancellationToken);
        }

        public async Task<Circuit> Delete(int id, CancellationToken cancellationToken)
        {
            return await repository.Delete(id, cancellationToken);
        }

        public async Task<Circuit> Get(int id)
        {
            return await repository.DbSet.AsNoTracking()
                .Include(e => e.RaceHistories)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Circuit>> Query(bool? used)
        {
            IQueryable<Circuit> query = repository.DbSet.AsNoTracking()
                .Include(e => e.RaceHistories);

            if (used.HasValue)
            {
                query = query.Where(e => e.RaceHistories.Any() == used.Value);
            }

            return await query.ToListAsync();
        }

        public async Task Update(Circuit circuit, CancellationToken cancellationToken)
        {
            await repository.Update(circuit, cancellationToken);
        }
    }
}
