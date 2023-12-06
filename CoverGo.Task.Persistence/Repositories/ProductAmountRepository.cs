using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Domain;

namespace CoverGo.Task.Persistence.Repositories
{
    public class ProductAmountRepository : GenericRepository<ProductAmount>, IProductAmountRepository
    {
        private readonly CoverGoDbContext _dbContext;
        public ProductAmountRepository(CoverGoDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
