namespace MultiTenancy.SchemaSeparation.Infrastructure.MultiTenancy
{
    public class TenantInfo
    {
        public static string Identifier = "tenant";

        public TenantInfo(string name, string connectionString)
        {
            this.Name = name;
            this.ConnectionString = connectionString;
        }

        public string Name { get; private set; }

        public string ConnectionString { get; private set; }
    }
}
