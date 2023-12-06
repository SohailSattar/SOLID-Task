using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Domain;
using Microsoft.EntityFrameworkCore;

namespace CoverGo.Task.Persistence.Repositories
{
    public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
        private readonly CoverGoDbContext _dbContext;
        private readonly IProductRepository _productRepository;
        public DiscountRepository(CoverGoDbContext dbContext, IProductRepository productRepository) : base(dbContext)
        {
            _dbContext = dbContext;
            _productRepository = productRepository;
        }

        public async Task<bool> CheckIfProductExists(int productId)
        {
            return await _dbContext.Products.AnyAsync(p => p.Id == productId);
        }
    }
}