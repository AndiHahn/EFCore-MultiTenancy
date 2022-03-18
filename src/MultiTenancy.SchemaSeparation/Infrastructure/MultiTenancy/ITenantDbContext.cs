namespace MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy
{
    internal interface ITenantDbContext
    {
        public TenantInfo TenantInfo { get; }
    }
}
