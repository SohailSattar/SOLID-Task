using CoverGo.Task.Application.Contracts.Services;
using CoverGo.Task.Domain;

namespace CoverGo.Task.Application.Services
{
    public class DiscountService : IDiscountService
    {
        public void ApplyDiscount(ShoppingCart cart, int productId, int requiredAmount)
        {
            var productAmount = cart.Products?.FirstOrDefault(p => p.ProductId == productId);

            if (productAmount != null && productAmount.Amount >= requiredAmount)
            {
                int freeItemsCount = productAmount.Amount / (requiredAmount + 1); // +1 to get one item for free
                decimal discountAmount = freeItemsCount * productAmount.Product!.Price!;

                // Subtract the discount amount from the total
                cart.Total -= discountAmount;

                //// Optionally, you can update the cart's product list to reflect the discounted amount
                //productAmount.Amount -= freeItemsCount;
            }
        }
    }
}
