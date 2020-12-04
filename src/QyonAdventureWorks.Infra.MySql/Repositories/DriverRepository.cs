using Microsoft.EntityFrameworkCore;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using QyonAdventureWorks.Infra.MySql.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Infra.MySql.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly IRepository<Driver> repository;

        public DriverRepository(IRepository<Driver> repository)
        {
            this.repository = repository;
        }

        public async Task<Driver> Add(Driver driver, CancellationToken cancellationToken)
        {
            return await repository.Add(driver, cancellationToken);
        }

        public async Task<Driver> Delete(int id, CancellationToken cancellationToken)
        {
            return await repository.Delete(id, cancellationToken);
        }

        public async Task<Driver> Get(int id)
        {
            return await repository.DbSet.AsNoTracking()
                .Include(e => e.RaceHistories)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Driver>> Query(bool? veteran)
        {
            IQueryable<Driver> query = repository.DbSet.AsNoTracking()
                .Include(e => e.RaceHistories);

            if(veteran.HasValue)
            {
                query = query.Where(e => e.RaceHistories.Any() == veteran.Value);
            }

            return await query.ToListAsync();
        }

        public async Task Update(Driver driver, CancellationToken cancellationToken)
        {
            await repository.Update(driver, cancellationToken);
        }
    }
}
