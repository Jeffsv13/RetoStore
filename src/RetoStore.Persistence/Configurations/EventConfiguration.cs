using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetoStore.Entities;

namespace RetoStore.Persistence.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(200);
        builder.Property(x => x.ExtendedDescription).HasMaxLength(1000);
        builder.Property(x => x.Place).HasMaxLength(100);
        builder.Property(x => x.DateEvent).HasColumnType("datetime")
            .HasDefaultValueSql("GETDATE()");
        builder.Property(x => x.ImageUrl).HasMaxLength(450)
            .IsUnicode(false);
        builder.HasIndex(x => x.Title);
        builder.ToTable("Event", "Apptelink");
    }
}
