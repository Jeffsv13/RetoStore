using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RetoStore.Entities.Info;
using System.Reflection;


namespace RetoStore.Persistence;

public class ApplicationDbContext : IdentityDbContext<RetoStoreUserIdentity>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    //Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Customizing the migration
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Ignore<Entities.Info.EventInfo>();
        modelBuilder.Ignore<ReportInfo>();

        modelBuilder.Entity<RetoStoreUserIdentity>(x => x.ToTable("User"));
        modelBuilder.Entity<IdentityRole>(x => x.ToTable("Role"));
        modelBuilder.Entity<IdentityUserRole<string>>(x => x.ToTable("UserRole"));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseLazyLoadingProxies();
    }

}
