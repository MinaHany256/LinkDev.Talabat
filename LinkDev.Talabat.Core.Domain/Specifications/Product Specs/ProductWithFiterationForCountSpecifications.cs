using LinkDev.Talabat.Core.Domain.Entites.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.Product_Specs
{
    public class ProductWithFiterationForCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithFiterationForCountSpecifications(int? brandId, int? categoryId, string? search)
            : base(

                  P =>
                         (string.IsNullOrEmpty(search) || P.NormalizedName.Contains(search))
                            &&
                        (!brandId.HasValue || P.BrandId == brandId.Value)
                            &&
                        (!categoryId.HasValue || P.CategoryId == categoryId.Value)


                  )
        {
            
        }
    }
}
