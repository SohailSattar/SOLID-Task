using CoverGo.Task.Application.Contracts.Persistence;
using FluentValidation;

namespace CoverGo.Task.Application.DTO.Discounts.Validators
{
    public class CreateDiscountDtoValidator : AbstractValidator<CreateDiscountDto>
    {
        private readonly IDiscountRepository _discountRepository;
        public CreateDiscountDtoValidator(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;

            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product Id cannot be empty.");
            RuleFor(x => x.RequiredAmount).NotEmpty().WithMessage("Required Amount cannot be empty.").GreaterThanOrEqualTo(0).WithMessage("Required Amount cannot be negative.");
            RuleFor(x => x.DiscountPercentage).NotEmpty().WithMessage("Percentage cannot be empty.").GreaterThanOrEqualTo(0).WithMessage("Percentage cannot be negative.").LessThan(100).WithMessage("Percentage cannot be greater than 100%.");
        }
    }
}
