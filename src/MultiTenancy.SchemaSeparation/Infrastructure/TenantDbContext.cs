using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using MultiTenancy.SchemaSeparation.Core;
using MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy;
using MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy.TenantAccessor;

namespace MultiTenancy.SchemaSeparation.Infrastructure
{
    public class TenantDbContext : DbContext, ITenantDbContext
    {
        public TenantDbContext(
            DbContextOptions<TenantDbContext> options,
            ITenantAccessor tenantAccessor)
            : base(options)
        {
            this.TenantInfo = tenantAccessor.GetTenant();
        }

        public TenantInfo TenantInfo { get; }

        public DbSet<Bill> Bill { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(this.TenantInfo.Name);

            modelBuilder.Entity<Bill>()
                .HasKey(b => b.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(this.TenantInfo.ConnectionString, o =>
                    {
                        o.MigrationsHistoryTable("__MigrationsHistory", this.TenantInfo.Name);
                    })
                    .ReplaceService<IModelCacheKeyFactory, MultiTenantModelCacheKeyFactory>()
                    .ReplaceService<IMigrationsAssembly, MigrationByTenantAssembly>();
            }
        }
    }
}
