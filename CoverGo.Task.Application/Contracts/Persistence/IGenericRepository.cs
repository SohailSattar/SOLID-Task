using System.Numerics;

namespace CoverGo.Task.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        ValueTask<IReadOnlyList<T>> GetAll();
        ValueTask<T> Get(int id);
        ValueTask<T> Add(T entity);
    }
}
