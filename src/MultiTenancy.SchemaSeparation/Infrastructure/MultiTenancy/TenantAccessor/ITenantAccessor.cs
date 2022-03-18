namespace MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy.TenantAccessor
{
    public interface ITenantAccessor
    {
        TenantInfo GetTenant();
    }
}
