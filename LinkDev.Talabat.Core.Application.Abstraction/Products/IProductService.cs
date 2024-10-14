using LinkDev.Talabat.Core.Application.Abstraction.Common;
using LinkDev.Talabat.Core.Application.Abstraction.Products.Models;

namespace LinkDev.Talabat.Core.Application.Abstraction.Products
{
    public interface IProductService
    {

        Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specParams);

        Task<ProductToReturnDto> GetProductAsync(int Id);

        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();


    }
}
