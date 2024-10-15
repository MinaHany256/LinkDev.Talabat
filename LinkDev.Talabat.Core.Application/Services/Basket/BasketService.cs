using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Domain.Entites.Basket;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Services.Basket
{
    internal class BasketService(IBasketRepository basketRepository, IMapper mapper, IConfiguration configuration) : IBasketService
    {
        public async Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId)
        {
            var basket = await basketRepository.GetAsync(basketId);

            if(basket is null) throw new NotFoundException(nameof(CustomerBasket) , basketId);

            return mapper.Map<CustomerBasketDto>(basket);

        }

        public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto basketDto)
        {
            var basket = mapper.Map<CustomerBasket>(basketDto);

            var timeToLive = TimeSpan.FromDays(double.Parse(configuration.GetSection("RedisSettings")["TimeToLiveInDays"]!));

            var updatedbasket = await basketRepository.UpdateAsync(basket, timeToLive);

            if (updatedbasket is null) throw new BadRequestException("Can't update , a problem with your basket");

            return basketDto;

        }

        public async Task DeleteCustomerBasket(string basketId)
        {
            var deleted = await basketRepository.DeleteAsync(basketId);

            if (!deleted) throw new BadRequestException("Unable to delete this basket.");
        }

    }
}
