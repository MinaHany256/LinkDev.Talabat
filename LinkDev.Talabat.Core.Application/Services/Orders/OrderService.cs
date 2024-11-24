using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entites.Orders;
using LinkDev.Talabat.Core.Domain.Entites.Products;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LinkDev.Talabat.Core.Application.Services.Orders
{
    internal class OrderService(IBasketService basketService, IUnitOfWork unitOfWork, IMapper mapper) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, OrderToCreateDto order)
        {
            // 1. Get Basket From Basket Repo
            var basket = await basketService.GetCustomerBasketAsync(order.BasketId);

            // 2. Get Selected Items at basket from product repo 

            var orderItems = new List<OrderItem>();

            if (basket.Items.Count() > 0)
            {
                var productRepo = unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);

                    if (product is not null)
                    {
                        var productItemOrdered = new ProductItemOrdered()
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            PictureUrl = product.PictureUrl ?? "",
                        };

                        var orderItem = new OrderItem()
                        {
                            Product = productItemOrdered,
                            Price = product.Price,
                            Quantity = item.Quantity,
                        };

                        orderItems.Add(orderItem);

                    }

                }
            }

            // 3. calculate subtotal
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 4. Map Address
            var address = mapper.Map<Address>(order.ShippingAddress);


            // 5. Create Order
            var orderToCreate = new Order()
            {
                BuyerEmail = buyerEmail,
                ShippingAddress = address,
                DeliveryMethodId = order.DeliveryMethodId,
                Items = orderItems,
                Subtotal = subtotal,
            };

            await unitOfWork.GetRepository<Order, int>().AddAsync(orderToCreate);

            // 6. Save To Database
            var created = await unitOfWork.CompleteAsync() > 0;

            if (!created) throw new BadRequestException("an error has occured during creating the order");

            return mapper.Map<OrderToReturnDto>(orderToCreate);

        }

        public Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
