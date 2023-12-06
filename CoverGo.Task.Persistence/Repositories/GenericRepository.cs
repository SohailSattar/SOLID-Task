

using CoverGo.Task.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CoverGo.Task.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CoverGoDbContext _dbContext;
        public GenericRepository(CoverGoDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async ValueTask<IReadOnlyList<T>> GetAll()
        {
            return await _dbContext.Set<T>()
                .ToListAsync();
        }

        public async ValueTask<T> Get(int Id)
        {
            var result = await _dbContext.Set<T>().FindAsync(Id);
            return result!;
        }

        public async ValueTask<T> Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

    }
}
