using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QyonAdventureWorks.Core.Entities;

namespace QyonAdventureWorks.Infra.MySql.Configurations
{
    public class RaceHistoryConfiguration : IEntityTypeConfiguration<RaceHistory>
    {
        public void Configure(EntityTypeBuilder<RaceHistory> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
