using MultiTenancy.Discriminator.Core;

namespace MultiTenancy.Discriminator.Infrastructure.MultiTenancy
{
    internal class HttpTenantAccessor : ITenantAccessor
    {
        private readonly IHttpContextAccessor contextAccessor;

        public HttpTenantAccessor(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public Guid TenantId
        {
            get
            {
                var httpContext = this.contextAccessor.HttpContext;
                if (httpContext?.Items != null &&
                    httpContext.Items.ContainsKey(nameof(ITenantDependentEntity.TenantId)))
                {
                    return new Guid(httpContext.Items[nameof(ITenantDependentEntity.TenantId)]?.ToString());
                }

                throw new InvalidOperationException("Tenant id is not available in http context.");
            }
        }
    }
}
