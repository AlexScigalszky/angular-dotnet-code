using Core.Specifications;
using Core.Tests.Builders;
using Domain.Specifications;
using System.Linq;
using Xunit;

namespace Core.Tests.Specifications
{
    public class ProductSpecificationTests
    {
        private ProductBuilder ProductBuilder { get; } = new ProductBuilder();

        [Fact]
        public void Matches_Product_With_Category_Spec()
        {
            var spec = new ProductWithCategorySpecification(ProductBuilder.ProductName1);

            var result = ProductBuilder.GetProductCollection()
                .AsQueryable()
                .FirstOrDefault(spec.Criteria);

            Assert.NotNull(result);
            Assert.Equal(ProductBuilder.ProductId1, result.Id);
        }
    }
}
