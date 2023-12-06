using CoverGo.Task.Application.DTO.ProductAmount;

namespace CoverGo.Task.Application.DTO.ShoppingCart
{
    public class ShoppingCartDetailsDto
    {
        public List<ProductAmountDetailDto>? Products { get; set; }
        public decimal Total { get; set; }
    }
}
