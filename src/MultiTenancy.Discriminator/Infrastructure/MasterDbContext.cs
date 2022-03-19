using Microsoft.EntityFrameworkCore;
using MultiTenancy.Discriminator.Core;
using MultiTenancy.Discriminator.Infrastructure.MultiTenancy;

namespace MultiTenancy.Discriminator.Infrastructure
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options)
            : base(options)
        {
        }

        protected MasterDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Bill> Bill { get; set; }

        public DbSet<User> TenantUser { get; set; }

        public DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bill>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<User>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Role>()
                .HasKey(t => t.Id);

            EnsureTenantInterfaces(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            EnsureTenantIdIsSet();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            EnsureTenantIdIsSet();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void EnsureTenantIdIsSet()
        {
            this.ChangeTracker.DetectChanges();
            ChangeTracker.Entries<ITenantDependentEntity>()
                .Where(e => e.State != EntityState.Unchanged)
                .ToList()
                .ForEach(e =>
                {
                    if (e.Entity.TenantId == Guid.Empty)
                    {
                        throw new MultitenancyException($"TenantId is missing on entity: {e.Entity.GetType()}");
                    }
                });
        }

        private void EnsureTenantInterfaces(ModelBuilder modelBuilder)
        {
            var tenantInterfaces = new[] { typeof(ITenantDependentEntity), typeof(ITenantIndependentEntity) };

            modelBuilder.Model.GetEntityTypes().ToList()
                .ForEach(e =>
                {
                    var interfaces = e.ClrType.GetInterfaces();
                    int nrOfMarkerInterfaces = interfaces.Intersect(tenantInterfaces).Count();

                    if (nrOfMarkerInterfaces != 1)
                    {
                        throw new MultitenancyException($"Entity {e.Name} must implement either interface {nameof(ITenantDependentEntity)} or {nameof(ITenantIndependentEntity)}.");
                    }
                });
        }
    }
}
