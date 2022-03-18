namespace MultiTenancy.SchemaSeparation.Core
{
    public class TenantUser
    {
        private Tenant tenant;

        public TenantUser(Guid userId, Guid tenantId)
        {
            this.UserId = userId;
            this.TenantId = tenantId;
        }

        public Guid UserId { get; private set; }

        public Guid TenantId { get; private set; }

        public virtual Tenant Tenant => this.tenant;
    }
}
