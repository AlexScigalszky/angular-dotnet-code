using FluentValidation;
using Application.Models.Product;

namespace AspnetRun.Api.Application.Validations
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        // TODO For Example
        public ProductValidator()
        {
            RuleFor(request => request.ProductName).NotNull();
        }
    }
}
