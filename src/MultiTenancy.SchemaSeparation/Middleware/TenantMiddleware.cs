using Microsoft.EntityFrameworkCore;
using MultiTenancy.SchemaSeparation.Infrastructure;
using MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy;

namespace MultiTenancy.SchemaSeparation.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate next;

        public TenantMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext, MasterDbContext masterContext)
        {
            /*
             * Possibly read userId from claims
             */

            /*
            var userIdClaim = httpContext.User.FindFirst("userId");
            if (userIdClaim is null)
            {
                throw new UnauthorizedAccessException("UserId claim is not available.");
            }

            if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                throw new UnauthorizedAccessException("TenantId has no valid format.");
            }
            */

            /*
             * Possibly caching of tenant for user
             */

            Guid userId = MasterContextSeed.TenantAUserId;

            var tenant = await masterContext.Tenant
                .FirstOrDefaultAsync(t => t.Users.Any(tu => tu.UserId == userId));
            if (tenant is null)
            {
                throw new UnauthorizedAccessException("User is not assigned to any tenant.");
            }

            TenantInfo currentTenant = new TenantInfo(tenant.Name, tenant.ConnectionString);

            httpContext.Items.Add(TenantInfo.Identifier, currentTenant);

            await this.next(httpContext);
        }
    }
}
