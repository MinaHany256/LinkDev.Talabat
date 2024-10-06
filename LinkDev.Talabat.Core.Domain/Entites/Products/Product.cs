using LinkDev.Talabat.Core.Domain.Common;

namespace LinkDev.Talabat.Core.Domain.Entites.Products
{
    public class Product : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int? BrandId { get; set; }    // Forgien Key For The ProductBrand
        public virtual ProductBrand? Brand { get; set; }

        public int? CategoryId { get; set; }  // Forgien Key For The ProductCategory
        public virtual ProductCategory? Category { get; set; }
    }
}
