using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QyonAdventureWorks.Core.Entities;

namespace QyonAdventureWorks.Infra.MySql.Configurations
{
    public class CircuitConfiguration : IEntityTypeConfiguration<Circuit>
    {
        public void Configure(EntityTypeBuilder<Circuit> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Description)
                .HasMaxLength(50)
                .IsRequired(true);
        }
    }
}
