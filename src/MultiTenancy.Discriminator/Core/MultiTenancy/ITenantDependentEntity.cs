namespace MultiTenancy.Discriminator.Core
{
    public interface ITenantDependentEntity
    {
        public Guid TenantId { get; set; }
    }
}
