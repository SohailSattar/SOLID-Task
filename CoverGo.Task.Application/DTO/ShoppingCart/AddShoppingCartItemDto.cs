using CoverGo.Task.Application.DTO.ProductAmount;

namespace CoverGo.Task.Application.DTO.ShoppingCart
{
    public class AddShoppingCartItemDto
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        //public int Total { get; set; }
    }
}
