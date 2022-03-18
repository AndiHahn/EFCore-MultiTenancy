using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using System.Reflection;

namespace MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy
{
    internal class MigrationByTenantAssembly : MigrationsAssembly
    {
        private readonly DbContext context;

        public MigrationByTenantAssembly(
            ICurrentDbContext currentContext,
            IDbContextOptions options,
            IMigrationsIdGenerator idGenerator,
            IDiagnosticsLogger<DbLoggerCategory.Migrations> logger)
            : base(currentContext, options, idGenerator, logger)
        {
            this.context = currentContext.Context;
        }

        public override Migration CreateMigration(
            TypeInfo migrationClass,
            string activeProvider)
        {
            if (activeProvider == null)
            {
                throw new ArgumentNullException(nameof(activeProvider), $"{nameof(activeProvider)} argument is null");
            }

            bool hasCtorWithSchema = migrationClass
                    .GetConstructor(new[] { typeof(string) }) != null;

            if (hasCtorWithSchema && this.context is ITenantDbContext tenantDbContext)
            {
                var instance = (Migration)Activator.CreateInstance(migrationClass.AsType(), tenantDbContext?.TenantInfo?.Name);
                instance.ActiveProvider = activeProvider;
                return instance;
            }

            return base.CreateMigration(migrationClass, activeProvider);
        }
    }
}
