using CoverGo.Task.Domain;

namespace CoverGo.Task.Application.Contracts.Services
{
    public interface IDiscountService
    {
        void ApplyDiscount(ShoppingCart cart, int productId, int requiredAmount);
    }
}
