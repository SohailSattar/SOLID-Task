using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverGo.Task.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly CoverGoDbContext _dbContext;
        public ProductRepository(CoverGoDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Exists(int Id)
        {
            var entity = await _dbContext.Products.FindAsync(Id);
            return entity != null;
        }
    }
}
