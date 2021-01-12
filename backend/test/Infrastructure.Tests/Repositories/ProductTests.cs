using Domain.Data;
using Domain.Repositories.Implementation;
using Infrastructure.Data;
using Infrastructure.Tests.Builders;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Infrastructure.Tests.Repositories
{
    public class ProductTests
    {
        private readonly AzureExampleContext _azureExampleContext;
        private readonly ProductRepository _productRepository;
        private readonly ITestOutputHelper _output;
        private ProductBuilder ProductBuilder { get; } = new ProductBuilder();

        public ProductTests(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<ExampleContext>()
                .UseInMemoryDatabase(databaseName: "Example")
                .Options;
            _azureExampleContext = new AzureExampleContext(dbOptions);
            _productRepository = new ProductRepository(_azureExampleContext);
        }

        [Fact]
        public async Task Get_Existing_Product()
        {
            var existingProduct = ProductBuilder.WithDefaultValues();


            // TODO For example
            //_eapExampleContext.Products.Add(existingProduct);


            _azureExampleContext.SaveChanges();

            var productId = existingProduct.Id;
            _output.WriteLine($"ProductId: {productId}");

            var productFromRepo = await _productRepository.GetByIdAsync(productId);
            Assert.Equal(ProductBuilder.TestProductId, productFromRepo.Id);
            Assert.Equal(ProductBuilder.TestCategoryId, productFromRepo.CategoryId);
        }

        [Fact]
        public async Task Get_Product_By_Name()
        {
            var existingProduct = ProductBuilder.WithDefaultValues();


            // TODO For example
            //_eapExampleContext.Products.Add(existingProduct);

            _azureExampleContext.SaveChanges();
            var productName = existingProduct.ProductName;
            _output.WriteLine($"ProductName: {productName}");

            var productListFromRepo = await _productRepository.GetProductByNameAsync(productName);
            Assert.Equal(ProductBuilder.TestProductName, productListFromRepo.ToList().First().ProductName);
        }
    }
}
