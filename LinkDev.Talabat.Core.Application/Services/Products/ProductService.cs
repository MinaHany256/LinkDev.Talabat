using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Products.Models;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entites.Products;
using LinkDev.Talabat.Core.Domain.Specifications;
using LinkDev.Talabat.Core.Domain.Specifications.Product_Specs;

namespace LinkDev.Talabat.Core.Application.Services.Products
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
            => _mapper.Map<IEnumerable<BrandDto>>(await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());


        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
            => _mapper.Map<IEnumerable<CategoryDto>>(await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync());

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var specs = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _unitOfWork.GetRepository<Product, int>().GetWithSpecAsync(specs);

            var mappedProduct = _mapper.Map<ProductToReturnDto>(product);

            return mappedProduct;
        }


        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
        {
            var specs = new ProductWithBrandAndCategorySpecifications();
           

            var products = _mapper.Map<IEnumerable<ProductToReturnDto>>(await _unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(specs));

            return products;    
        }
    }
}
