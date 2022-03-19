namespace MultiTenancy.Discriminator.Infrastructure.MultiTenancy
{
    public interface ITenantAccessor
    {
        public Guid TenantId { get; }
    }
}
