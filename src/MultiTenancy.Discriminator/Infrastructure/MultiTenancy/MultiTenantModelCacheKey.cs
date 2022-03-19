using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable enable

namespace MultiTenancy.Discriminator.Infrastructure.MultiTenancy
{
    internal class MultiTenantModelCacheKey : ModelCacheKey
    {
        private readonly Guid tenantId;

        public MultiTenantModelCacheKey(DbContext context)
            : base(context)
        {
            if (!(context is TenantDbContext tenantDbContext))
            {
                throw new MultitenancyException($"Multitenant model cache key can only be used for {nameof(TenantDbContext)}.");
            }

            this.tenantId = tenantDbContext.TenantId;
        }

        public override bool Equals(object? obj) => base.Equals(obj);

        public override int GetHashCode() => this.tenantId.GetHashCode();

        protected override bool Equals(ModelCacheKey other)
            => base.Equals(other) && (other as MultiTenantModelCacheKey)?.tenantId == this.tenantId;
    }
}
