namespace MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy.TenantAccessor
{
    public class ManualTenantAccessor : ITenantAccessor
    {
        private readonly TenantInfo tenant;

        public ManualTenantAccessor(TenantInfo tenant)
        {
            this.tenant = tenant;
        }

        public TenantInfo GetTenant() => this.tenant;
    }
}
