using LinkDev.Talabat.Core.Domain.Entites.Products;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IGenericRepository<Product, int> ProductRepository { get;  }
        public IGenericRepository<ProductBrand, int> BrandsRepository { get;  }
        public IGenericRepository<ProductCategory, int> CategoriesRepository { get; }

        Task<int> CompleteAsync();

    }
}
