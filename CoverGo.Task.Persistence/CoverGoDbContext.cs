using CoverGo.Task.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverGo.Task.Persistence
{
    public class CoverGoDbContext : DbContext
    {
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public CoverGoDbContext()
        {

        }
        public CoverGoDbContext(DbContextOptions<CoverGoDbContext> options) : base(options)
        {

        }
    }
}
