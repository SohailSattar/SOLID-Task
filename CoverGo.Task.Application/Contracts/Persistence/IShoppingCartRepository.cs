using CoverGo.Task.Domain;

namespace CoverGo.Task.Application.Contracts.Persistence
{
    public interface IShoppingCartRepository : IGenericRepository<ShoppingCart>
    {
        Task<ShoppingCart> GetCart();
        Task<ShoppingCart> AddItem(ProductAmount product);
        Task<bool> CheckIfProductExists(int productId);
    }
}
