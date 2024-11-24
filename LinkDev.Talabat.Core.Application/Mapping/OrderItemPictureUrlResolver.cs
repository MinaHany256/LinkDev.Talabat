using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Domain.Entites.Orders;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Mapping
{
    internal class OrderItemPictureUrlResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
                return $"{configuration["Urls:ApiBaseUrl"]}/{source.Product.PictureUrl}";

            return string.Empty;
        }
    }
}
