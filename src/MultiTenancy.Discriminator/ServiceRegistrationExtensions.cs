using Microsoft.EntityFrameworkCore;
using MultiTenancy.Discriminator.Infrastructure;
using MultiTenancy.Discriminator.Infrastructure.MultiTenancy;

namespace MultiTenancy.Discriminator
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string dbConnection = configuration.GetConnectionString("ApplicationDbConnection");

            services.AddDbContext<MasterDbContext>(
                options => options.UseSqlServer(dbConnection));

            services.AddDbContext<TenantDbContext>(
                options => options.UseSqlServer(dbConnection));

            services.AddTransient<ITenantAccessor, HttpTenantAccessor>();
        }
    }
}
