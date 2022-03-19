using CSharpFunctionalExtensions;

namespace MultiTenancy.Discriminator.Core
{
    public class Role : Entity<Guid>, ITenantIndependentEntity
    {
        private Role() { }

        public Role(string name)
            : base(Guid.NewGuid())
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
