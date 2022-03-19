using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MultiTenancy.Discriminator.Infrastructure.MultiTenancy
{
    internal class MultiTenantModelCacheKeyFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context) => new MultiTenantModelCacheKey(context);
    }
}
