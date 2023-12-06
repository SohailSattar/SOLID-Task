using CoverGo.Task.Domain;

namespace CoverGo.Task.Application.Contracts.Persistence
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> Exists(int Id);
    }
}
