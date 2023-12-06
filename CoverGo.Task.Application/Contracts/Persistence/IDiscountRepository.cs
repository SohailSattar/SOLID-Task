using CoverGo.Task.Domain;

namespace CoverGo.Task.Application.Contracts.Persistence
{
    public interface IDiscountRepository : IGenericRepository<Discount>
    {
        Task<bool> CheckIfProductExists(int productId);
    }
}
