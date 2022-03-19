using Microsoft.EntityFrameworkCore;
using MultiTenancy.Discriminator.Core;

namespace MultiTenancy.Discriminator.Infrastructure
{
    public static class DbContextSeed
    {
        public static Guid TenantAUserId = new Guid("9f265032-dd4a-497e-9ab9-a54a07ea5544");
        public static Guid TenantBUserId = new Guid("94c38ca7-5a5e-4f24-8c54-25ee5410cc83");

        public static Guid TenantAId = new Guid("0f92ac35-6c90-4bee-a20e-bea5a3ca9513");
        public static Guid TenantBId = new Guid("d0602d5c-1ca0-4b15-848b-e53ea25fffad");

        public static async Task SeedAsync(this MasterDbContext context)
        {
            if (!await context.TenantUser.AnyAsync())
            {
                var tenantAUser = new User(TenantAUserId, TenantAId);
                var tenantBUser = new User(TenantBUserId, TenantBId);

                context.TenantUser.Add(tenantAUser);
                context.TenantUser.Add(tenantBUser);

                await context.SaveChangesAsync();
            }

            if (!await context.Bill.AnyAsync())
            {
                var bill1 = new Bill("Amazon", 150.12, DateTime.UtcNow, string.Empty);
                var bill2 = new Bill("Google", 25.4, DateTime.UtcNow.AddMonths(-2), string.Empty);
                var bill3 = new Bill("Microsoft", 55.87, DateTime.UtcNow.AddDays(-4), string.Empty);

                bill1.TenantId = TenantAId;
                bill2.TenantId = TenantAId;
                bill3.TenantId = TenantBId;

                context.Bill.Add(bill1);
                context.Bill.Add(bill2);
                context.Bill.Add(bill3);

                await context.SaveChangesAsync();
            }

            if (!await context.Role.AnyAsync())
            {
                var role1 = new Role("User");
                var role2 = new Role("Admin");

                context.Role.Add(role1);
                context.Role.Add(role2);

                await context.SaveChangesAsync();
            }
        }
    }
}
