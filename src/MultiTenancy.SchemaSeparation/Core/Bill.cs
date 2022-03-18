using CSharpFunctionalExtensions;

namespace MultiTenancy.SchemaSeparation.Core
{
    public class Bill : Entity<Guid>
    {
        private Bill() { }

        public Bill(string shopName, double price, DateTime date, string notes)
        {
            this.Id = Guid.NewGuid();
            this.ShopName = shopName;
            this.Price = price;
            this.Date = date;
            this.Notes = notes;
        }

        public string ShopName { get; private set; }

        public double Price { get; private set; }

        public DateTime Date { get; private set; }

        public string Notes { get; private set; }
    }
}
