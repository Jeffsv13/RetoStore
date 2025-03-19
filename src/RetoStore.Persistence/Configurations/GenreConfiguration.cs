using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetoStore.Entities;

namespace RetoStore.Persistence.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.ToTable("Genre", "Apptelink");
        builder.HasQueryFilter(x => x.Status);
    }
}
