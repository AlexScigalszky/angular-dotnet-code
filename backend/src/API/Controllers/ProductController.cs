using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Application.Models.Product;
using AspnetRun.Api.Application.Validations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("[action]")]
        [HttpGet]
        //[Authorize(Roles = "User, Admin, Invited")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ProductModel))]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            var products = await _productService.GetProductList();

            return Ok(products);
        }

        [Route("[action]")]
        [HttpPost]
        //[Authorize(Roles = "User, Admin, Invited")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ProductModel))]
        public async Task<ActionResult<ProductModel>> GetProductById([FromQuery] int id)
        {
            var product = await _productService.GetProductById(id);

            return Ok(product);
        }

        [Route("[action]")]
        [HttpPost]
        //[Authorize(Roles = "User, Admin, Invited")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ProductModel))]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductsByName([FromBody] ProductDTO productDTO)
        {
            var products = await _productService.GetProductsByName(productDTO.ProductName);

            return Ok(products);
        }


        [Route("[action]")]
        [HttpPost]
        //[Authorize(Roles = "User, Admin, Invited")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ProductModel))]
        public async Task<ActionResult<ProductModel>> CreateProduct([FromBody] ProductDTO productDTO)
        {
            var validator = new ProductValidator();
            var result = await validator.ValidateAsync(productDTO);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var product = await _productService.Create(productDTO);

            return Ok(product);
        }

        [Route("[action]")]
        [HttpPost]
        //[Authorize(Roles = "User, Admin, Invited")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(ProductModel))]
        public async Task<ActionResult> UpdateProduct([FromBody] ProductDTO productDTO)
        {
            var validator = new ProductValidator();
            var result = await validator.ValidateAsync(productDTO);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            await _productService.Update(productDTO);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        //[Authorize(Roles = "User, Admin, Invited")]
        public async Task<ActionResult> DeleteProductById([FromBody] ProductDTO productDTO)
        {
            await _productService.Delete(productDTO);
            return Ok();
        }
    }
}
