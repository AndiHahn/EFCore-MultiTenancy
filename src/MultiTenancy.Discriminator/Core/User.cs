using CSharpFunctionalExtensions;

namespace MultiTenancy.Discriminator.Core
{
    public class User : Entity<Guid>, ITenantDependentEntity
    {
        public User(Guid id, Guid tenantId)
            : base(id)
        {
            this.TenantId = tenantId;
        }

        public Guid TenantId { get; set; }
    }
}
