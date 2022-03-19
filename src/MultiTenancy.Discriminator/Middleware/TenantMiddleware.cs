using MultiTenancy.Discriminator.Core;
using MultiTenancy.Discriminator.Infrastructure;

namespace MultiTenancy.Discriminator.Middleware
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

            Guid userId = DbContextSeed.TenantAUserId;

            var user = await masterContext.TenantUser.FindAsync(userId);
            if (user is null)
            {
                throw new UnauthorizedAccessException("User is not assigned to any tenant.");
            }

            httpContext.Items.Add(nameof(ITenantDependentEntity.TenantId), user.TenantId);

            await this.next(httpContext);
        }
    }
}
