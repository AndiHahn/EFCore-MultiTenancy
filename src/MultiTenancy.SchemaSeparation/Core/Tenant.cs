using CSharpFunctionalExtensions;

namespace MultiTenancy.SchemaSeparation.Core
{
    public class Tenant : Entity<Guid>
    {
        private readonly List<TenantUser> users = new List<TenantUser>();

        private Tenant() { }

        public Tenant(string name, string connectionString)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.ConnectionString = connectionString;
        }

        public string Name { get; private set; }

        public string ConnectionString { get; private set; }

        public virtual IReadOnlyCollection<TenantUser> Users => this.users.AsReadOnly();
    }
}
