using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable enable

namespace MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy
{
    internal class MultiTenantModelCacheKey : ModelCacheKey
    {
        private readonly string? schema;

        public MultiTenantModelCacheKey(DbContext context)
            : base(context)
        {
            this.schema = (context as ITenantDbContext)?.TenantInfo.Name;
        }

        public override bool Equals(object? obj) => base.Equals(obj);

        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode() * 397;
            if (this.schema != null)
            {
                hashCode ^= this.schema.GetHashCode();
            }

            return hashCode;
        }

        protected override bool Equals(ModelCacheKey other)
            => base.Equals(other) && (other as MultiTenantModelCacheKey)?.schema == this.schema;
    }
}
