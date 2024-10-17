namespace LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers
{
    public interface IDbInitializer
    {
        Task InitializeAync();
        Task SeddAsync();
    }
}
