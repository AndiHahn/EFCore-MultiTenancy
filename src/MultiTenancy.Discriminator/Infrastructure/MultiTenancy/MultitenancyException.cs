namespace MultiTenancy.Discriminator.Infrastructure.MultiTenancy
{
    public class MultitenancyException : Exception
    {
        public MultitenancyException()
        {
        }

        public MultitenancyException(string message)
            : base(message)
        {
        }

        public MultitenancyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
