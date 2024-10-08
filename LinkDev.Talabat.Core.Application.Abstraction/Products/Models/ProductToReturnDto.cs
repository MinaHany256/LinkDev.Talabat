﻿namespace LinkDev.Talabat.Core.Application.Abstraction.Products.Models
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int? BrandId { get; set; }    // Forgien Key For The ProductBrand
        public string? Brand { get; set; }

        public int? CategoryId { get; set; }  // Forgien Key For The ProductCategory
        public string? Category { get; set; }
    }
}
    