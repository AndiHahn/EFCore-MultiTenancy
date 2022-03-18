using Microsoft.EntityFrameworkCore;
using MultiTenancy.SchemaSeparation.Infrastructure;
using MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy.TenantAccessor;

namespace MultiTenancy.SchemaSeparation
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string masterDbConnection = configuration.GetConnectionString("MasterDbConnection");
            services.AddDbContext<MasterDbContext>(
                options => options.UseSqlServer(masterDbConnection));

            services.AddDbContext<TenantDbContext>();

            services.AddTransient<ITenantAccessor, HttpTenantAccessor>();
            //services.AddTransient<ITenantAccessor, MigrationTenantAccessor>();
        }
    }
}
