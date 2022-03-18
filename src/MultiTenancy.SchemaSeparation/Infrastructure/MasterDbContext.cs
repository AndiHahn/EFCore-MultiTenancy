using Microsoft.EntityFrameworkCore;
using MultiTenancy.SchemaSeparation.Core;

namespace MultiTenancy.SchemaSeparation.Infrastructure
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tenant> Tenant { get; private set; }

        public DbSet<TenantUser> TenantUser { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TenantUser>()
                .HasKey(tu => new { tu.UserId, tu.TenantId });
            modelBuilder.Entity<TenantUser>()
                .HasOne(tu => tu.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(tu => tu.TenantId);
        }
    }
}
