using Microsoft.EntityFrameworkCore;
using MultiTenancy.SchemaSeparation.Core;

namespace MultiTenancy.SchemaSeparation.Infrastructure
{
    public static class TenantContextSeed
    {
        public static async Task SeedAsync(this TenantDbContext context)
        {
            if (!await context.Bill.AnyAsync())
            {
                var bill1 = new Bill("Amazon", 150.12, DateTime.UtcNow, context.TenantInfo.Name);
                var bill2 = new Bill("Google", 25.4, DateTime.UtcNow.AddMonths(-2), context.TenantInfo.Name);
                var bill3 = new Bill("Microsoft", 55.87, DateTime.UtcNow.AddDays(-4), context.TenantInfo.Name);

                context.Bill.Add(bill1);
                context.Bill.Add(bill2);
                context.Bill.Add(bill3);

                await context.SaveChangesAsync();
            }
        }
    }
}
