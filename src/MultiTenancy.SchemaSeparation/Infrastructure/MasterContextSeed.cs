using Microsoft.EntityFrameworkCore;
using MultiTenancy.SchemaSeparation.Core;

namespace MultiTenancy.SchemaSeparation.Infrastructure
{
    public static class MasterContextSeed
    {
        public static Guid TenantAUserId = new Guid("9f265032-dd4a-497e-9ab9-a54a07ea5544");
        public static Guid TenantBUserId = new Guid("94c38ca7-5a5e-4f24-8c54-25ee5410cc83");

        public static async Task SeedAsync(this MasterDbContext context, IConfiguration configuration)
        {
            if (!await context.Tenant.AnyAsync())
            {
                var tenantA = new Tenant("TenantA", configuration.GetConnectionString("MasterDbConnection"));
                var tenantB = new Tenant("TenantB", configuration.GetConnectionString("MasterDbConnection"));

                context.Tenant.Add(tenantA);
                context.Tenant.Add(tenantB);

                var tenantAUser = new TenantUser(TenantAUserId, tenantA.Id);
                var tenantBUser = new TenantUser(TenantBUserId, tenantB.Id);

                context.TenantUser.Add(tenantAUser);
                context.TenantUser.Add(tenantBUser);

                await context.SaveChangesAsync();
            }
        }
    }
}
