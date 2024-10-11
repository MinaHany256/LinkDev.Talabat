using LinkDev.Talabat.Core.Domain.Entites.Products;

namespace LinkDev.Talabat.Core.Domain.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {

        public ProductWithBrandAndCategorySpecifications() : base()
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }

        public ProductWithBrandAndCategorySpecifications(int id) : base(id)
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }

    }
}
