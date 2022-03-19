using CSharpFunctionalExtensions;

namespace MultiTenancy.Discriminator.Core
{
    public class Bill : Entity<Guid>, ITenantDependentEntity
    {
        private Bill() { }

        public Bill(string shopName, double price, DateTime date, string notes)
            : base(Guid.NewGuid())
        {
            this.ShopName = shopName;
            this.Price = price;
            this.Date = date;
            this.Notes = notes;
        }

        public string ShopName { get; private set; }

        public double Price { get; private set; }

        public DateTime Date { get; private set; }

        public string Notes { get; private set; }

        public Guid TenantId { get; set; }
    }
}
