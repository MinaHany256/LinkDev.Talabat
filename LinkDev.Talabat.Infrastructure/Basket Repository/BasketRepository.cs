using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Domain.Entites.Basket;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LinkDev.Talabat.Infrastructure.Basket_Repository
{
    internal class BasketRepository(IConnectionMultiplexer redis) : IBasketRepository
    {
        private readonly IDatabase _database = redis.GetDatabase();

        public async Task<CustomerBasket?> GetAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);

            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }

        public async Task<CustomerBasket?> UpdateAsync(CustomerBasket basket, TimeSpan TimeToLive)
        {
            var value = JsonSerializer.Serialize(basket);

            var Updated = await _database.StringSetAsync(basket.Id, value , TimeToLive);

            if (Updated) return basket;

            return null;
        }

        public async Task<bool> DeleteAsync(string id) => await _database.KeyDeleteAsync(id); 
       

       
    }
}
