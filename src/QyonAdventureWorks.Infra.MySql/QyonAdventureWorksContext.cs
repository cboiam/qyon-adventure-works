using Microsoft.EntityFrameworkCore;
using QyonAdventureWorks.Core.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace QyonAdventureWorks.Infra.MySql
{
    public class QyonAdventureWorksContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Circuit> Circuits { get; set; }
        public DbSet<RaceHistory> RaceHistories { get; set; }

        public QyonAdventureWorksContext([NotNullAttribute] DbContextOptions options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
