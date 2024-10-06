﻿using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entites.Products;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    internal class StoreContextInitializer(StoreContext _dbContext) : IStoreContextInitializer
    {
        
        public async Task InitializeAync()
        {
            var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

            if (PendingMigrations.Any())
                await _dbContext.Database.MigrateAsync(); // Update-Database
        }

        public async Task SeddAsync()
        {
            if (!_dbContext.Brands.Any())
            {
                var brandsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

               

                if (brands?.Count > 0)
                {
                    await _dbContext.Set<ProductBrand>().AddRangeAsync(brands);
                    await _dbContext.SaveChangesAsync();
                }

            }

            if (!_dbContext.Categories.Any())
            {
                var categoriesData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

                if (categories?.Count > 0)
                {
                    await _dbContext.Set<ProductCategory>().AddRangeAsync(categories);
                    await _dbContext.SaveChangesAsync();
                }

            }

            if (!_dbContext.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistence/Data/Seeds/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count > 0)
                {
                    await _dbContext.Set<Product>().AddRangeAsync(products);
                    await _dbContext.SaveChangesAsync();
                }

            }
        }
    }
}