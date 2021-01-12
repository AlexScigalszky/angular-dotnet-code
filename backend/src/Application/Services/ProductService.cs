using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Repositories;
using Application.Models.Product;
using Application.Mapper;
using Application.Interfaces;
using ApplicationException = Application.Exceptions.ApplicationException;
using Domain.Repositories;
using Domain.Entities;

namespace Application.Services
{
    // TODO  For Example
    // TODO : add validation , authorization, logging, exception handling etc. -- cross cutting activities in here.
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IAppLogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, IAppLogger<ProductService> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<ProductModel>> GetProductList()
        {
            var productList = await _productRepository.GetProductListAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductModel>>(productList);
            return mapped;
        }

        public async Task<ProductModel> GetProductById(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            var mapped = ObjectMapper.Mapper.Map<ProductModel>(product);
            return mapped;
        }

        public async Task<IEnumerable<ProductModel>> GetProductsByName(string productName)
        {
            var productList = await _productRepository.GetProductByNameAsync(productName);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductModel>>(productList);
            return mapped;
        }

        public async Task<IEnumerable<ProductModel>> GetProductByCategory(int categoryId)
        {
            var productList = new List<Product>(); // TODO  For example: await _productRepository.GetProductByCategoryAsync(categoryId);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ProductModel>>(productList);
            return mapped;
        }

        public async Task<ProductModel> Create(ProductDTO productDTO)
        {
            await ValidateProductIfExist(productDTO);

            var mappedEntity = ObjectMapper.Mapper.Map<Product>(productDTO);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _productRepository.AddAsync(mappedEntity);
            _logger.LogInformation($"Entity successfully added - AppService");

            var newMappedEntity = ObjectMapper.Mapper.Map<ProductModel>(newEntity);
            return newMappedEntity;
        }

        public async Task Update(ProductDTO productDTO)
        {
            ValidateProductIfNotExist(productDTO);
            
            var editProduct = await _productRepository.GetByIdAsync(productDTO.Id);
            if (editProduct == null)
                throw new ApplicationException($"Entity could not be loaded.");

            ObjectMapper.Mapper.Map<ProductDTO, Product>(productDTO, editProduct);

            await _productRepository.UpdateAsync(editProduct);
            _logger.LogInformation($"Entity successfully updated - AppService");
        }

        public async Task Delete(ProductDTO productDTO)
        {
            ValidateProductIfNotExist(productDTO);
            var deletedProduct = await _productRepository.GetByIdAsync(productDTO.Id);
            if (deletedProduct == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await _productRepository.DeleteAsync(deletedProduct);
            _logger.LogInformation($"Entity successfully deleted - AppService");
        }

        private async Task ValidateProductIfExist(ProductDTO productDTO)
        {
            var existingEntity = await _productRepository.GetByIdAsync(productDTO.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{productDTO} with this id already exists");
        }

        private void ValidateProductIfNotExist(ProductDTO productDTO)
        {
            var existingEntity = _productRepository.GetByIdAsync(productDTO.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{productDTO} with this id is not exists");
        }
    }
}
