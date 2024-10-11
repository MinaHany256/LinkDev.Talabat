using LinkDev.Talabat.Core.Domain.Entites.Products;

namespace LinkDev.Talabat.Core.Domain.Specifications.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {

        public ProductWithBrandAndCategorySpecifications(string? sort) : base()
        {
            AddIncludes();

            AddOrderBy(P => P.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "nameDesc":
                        AddOrderByDesc(P => P.Name);
                        break;

                    case "priceAsc":
                        AddOrderBy(E => E.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;

                    default:
                        AddOrderBy(E => E.Name);
                        break;
                }
            }
        }

        public ProductWithBrandAndCategorySpecifications(int id) : base(id)
        {
            AddIncludes();
        }

        private protected override void AddIncludes()
        {
            base.AddIncludes();

            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }

    }
}
