using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy
{
    internal class MultiTenantModelCacheKeyFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context) => new MultiTenantModelCacheKey(context);
    }
}
