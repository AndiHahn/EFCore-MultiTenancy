namespace MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy.TenantAccessor
{
    internal class HttpTenantAccessor : ITenantAccessor
    {
        private readonly IHttpContextAccessor contextAccessor;

        public HttpTenantAccessor(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public TenantInfo GetTenant()
        {
            var httpContext = this.contextAccessor.HttpContext;
            if (httpContext?.Items != null &&
                httpContext.Items.ContainsKey(TenantInfo.Identifier))
            {
                return httpContext.Items[TenantInfo.Identifier] as TenantInfo;
            }

            throw new InvalidOperationException("Tenant is not available in http context.");
        }
    }
}
