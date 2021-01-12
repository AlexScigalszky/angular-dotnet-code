using Core.Specifications.Base;
using Domain.Entities;

namespace Domain.Specifications
{
    public class ProductWithCategorySpecification : BaseSpecification<Product>
    {
        // TODO For Example
        public ProductWithCategorySpecification(string productName)
            : base(p => p.ProductName.ToLower().Contains(productName.ToLower()))
        {
            //AddInclude(p => p.Category);
        }

        public ProductWithCategorySpecification() : base(null)
        {
            //AddInclude(p => p.Category);
        }
    }
}
