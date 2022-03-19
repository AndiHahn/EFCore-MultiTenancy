using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MultiTenancy.Discriminator.Core;
using MultiTenancy.Discriminator.Infrastructure.MultiTenancy;

namespace MultiTenancy.Discriminator.Infrastructure
{
    public class TenantDbContext : MasterDbContext
    {
        private readonly ITenantAccessor tenantAccessor;

        public TenantDbContext(
            DbContextOptions<TenantDbContext> options,
            ITenantAccessor tenantAccessor)
            : base(options)
        {
            this.tenantAccessor = tenantAccessor ?? throw new ArgumentNullException(nameof(tenantAccessor));
        }

        public Guid TenantId => this.tenantAccessor.TenantId;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ReplaceService<IModelCacheKeyFactory, ModelCacheKeyFactory>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyGlobalQueryFilter<ITenantDependentEntity>(
                entity => entity.TenantId == this.tenantAccessor.TenantId);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetTenantIds();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetTenantIds();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SetTenantIds()
        {
            this.ChangeTracker.DetectChanges();
            this.ChangeTracker.Entries<ITenantDependentEntity>()
                .Where(e => e.State != EntityState.Unchanged)
                .ToList()
                .ForEach(e =>
                {
                    if (e.Entity.TenantId == Guid.Empty)
                    {
                        e.Entity.TenantId = this.tenantAccessor.TenantId;
                    }
                });
        }
    }
}
