using Application.Exceptions;
using Application.Services;
using Core.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Services
{
    public class ProductTests
    {
        // NOTE : This layer we are not loaded database objects, test functionality of application layer

        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IAppLogger<ProductService>> _mockAppLogger;

        public ProductTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockAppLogger = new Mock<IAppLogger<ProductService>>();
        }

        [Fact]
        public async Task Get_Product_List()
        {
            var product1 = Product.Create(It.IsAny<int>(), 1, It.IsAny<string>());
            var product2 = Product.Create(It.IsAny<int>(), 1, It.IsAny<string>());

            _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product1);
            _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product2);

            var productService = new ProductService(_mockProductRepository.Object, _mockAppLogger.Object);
            var productList = await productService.GetProductList();

            _mockProductRepository.Verify(x => x.GetProductListAsync(), Times.Once);
        }

        [Fact]
        public async Task Create_New_Product()
        {
            var product = Product.Create(It.IsAny<int>(), 1, It.IsAny<string>());
            Product nullProduct = null; // we gave null product in order to create new one, if you give real product it returns already existing error

            _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(nullProduct);
            _mockProductRepository.Setup(x => x.AddAsync(product)).ReturnsAsync(product);

            var productService = new ProductService(_mockProductRepository.Object, _mockAppLogger.Object);
            var createdProductDto = await productService.Create(new Models.Product.ProductDTO { Id = product.Id, CategoryId = product.CategoryId, ProductName = product.ProductName });

            _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            _mockProductRepository.Verify(x => x.AddAsync(product), Times.Once);
        }

        [Fact]
        public async Task Create_New_Product_Validate_If_Exist()
        {
            var product = Product.Create(It.IsAny<int>(), 1, It.IsAny<string>());

            _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            _mockProductRepository.Setup(x => x.AddAsync(product)).ReturnsAsync(product);

            var productService = new ProductService(_mockProductRepository.Object, _mockAppLogger.Object);

            await Assert.ThrowsAsync<ApplicationException>(async () =>
                await productService.Create(new Models.Product.ProductDTO { Id = product.Id, CategoryId = product.CategoryId, ProductName = product.ProductName }));
        }
    }
}
