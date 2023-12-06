using CoverGo.Task.Application.Contracts.Persistence;
using FluentValidation;

namespace CoverGo.Task.Application.DTO.ShoppingCart.Validators
{
    public class AddShoppingCartItemDtoValidator  :AbstractValidator<AddShoppingCartItemDto>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public AddShoppingCartItemDtoValidator(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;

            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product Id cannot be empty.");
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount cannot be empty.").GreaterThanOrEqualTo(0).WithMessage("Amount cannot be negative.");

        }
    }
}
