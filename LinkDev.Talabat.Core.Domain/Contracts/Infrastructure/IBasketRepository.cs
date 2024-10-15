using LinkDev.Talabat.Core.Domain.Entites.Basket;

namespace LinkDev.Talabat.Core.Domain.Contracts.Infrastructure
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetAsync(string id);

        Task<CustomerBasket?> UpdateAsync(CustomerBasket basket , TimeSpan TimeToLive);

        Task<bool> DeleteAsync(string id);

    }
}
