using CoverGo.Task.Application.Contracts.Persistence;
using FluentValidation;

namespace CoverGo.Task.Application.DTO.Product.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductDtoValidator(IProductRepository productRepository)
        {
                _productRepository = productRepository;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name cannot be empty.");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");
        }
    }
}
