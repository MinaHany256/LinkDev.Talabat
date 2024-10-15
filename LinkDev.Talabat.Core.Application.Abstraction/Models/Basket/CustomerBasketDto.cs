using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Basket
{
    public class CustomerBasketDto
    {
        [Required]
        public required string Id { get; set; }

        public IEnumerable<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
