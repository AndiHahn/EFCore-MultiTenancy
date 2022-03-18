namespace MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy.TenantAccessor
{
    internal class MigrationTenantAccessor : ITenantAccessor
    {
        private readonly string connectionString;

        public MigrationTenantAccessor(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("TenantDbConnection");
        }

        public TenantInfo GetTenant() => new("$schema-placeholder$", this.connectionString);
    }
}
