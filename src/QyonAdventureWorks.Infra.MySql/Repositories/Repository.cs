using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QyonAdventureWorks.Core.Interfaces.Notifications;
using QyonAdventureWorks.Core.Notifications;
using QyonAdventureWorks.Infra.MySql.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Infra.MySql.Repositories
{
    public sealed class Repository<T> : IRepository<T>
        where T : class
    {
        public DbSet<T> DbSet { get; }
        private readonly QyonAdventureWorksContext context;
        private readonly INotificationService notificationService;
        private readonly ILogger<Repository<T>> logger;

        public Repository(QyonAdventureWorksContext context, INotificationService notificationService, ILogger<Repository<T>> logger)
        {
            this.context = context;
            DbSet = context.Set<T>();

            this.notificationService = notificationService;
            this.logger = logger;
        }

        public async Task<T> Add(T entity, CancellationToken cancellationToken)
        {
            var result = await DbSet.AddAsync(entity);
            await Save(cancellationToken);

            return result.Entity;
        }

        public async Task<T> Delete(int id, CancellationToken cancellationToken)
        {
            var entity = await Get(id);
            DbSet.Remove(entity);
            await Save(cancellationToken);

            return entity;
        }

        public async Task<T> Get(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> Update(T entity, CancellationToken cancellationToken)
        {
            var result = DbSet.Update(entity);
            await Save(cancellationToken);

            return result.Entity;
        }

        private async Task<bool> Save(CancellationToken cancellationToken)
        {
            try
            {
                int linesChanged = await context.SaveChangesAsync(cancellationToken);
                return linesChanged != default;
            }
            catch (DbUpdateException ex)
            {
                await notificationService.Notify(new Notification(typeof(T).Name, "Could not save changes"));
                logger.LogError(ex, "Error on updating database");
                
                return false;
            }
        }
    }
}
