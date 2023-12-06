using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Application.Contracts.Services;
using CoverGo.Task.Application.Services;
using CoverGo.Task.Domain;
using Microsoft.EntityFrameworkCore;

namespace CoverGo.Task.Persistence.Repositories
{
    public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly CoverGoDbContext _dbContext; 
        private ShoppingCart _shoppingCartItem;
        private IDiscountService _discount_service;
        public ShoppingCartRepository(CoverGoDbContext dbContext, IDiscountService discountService) : base(dbContext)
        {
            _dbContext = dbContext;
            _discount_service = discountService;

            // Load the shopping cart from the database or create a new one if it doesn't exist
            _shoppingCartItem = _dbContext.ShoppingCarts.Include(sc => sc.Products).FirstOrDefault() ?? new ShoppingCart
            {
                Products = new List<ProductAmount>(),
                Total = 0
            };
        }

        public async System.Threading.Tasks.Task<ShoppingCart> AddItem(ProductAmount product)
        {
            var productDetails = await _dbContext.Products.FindAsync(product.ProductId);
            if (productDetails == null)
            {
                return _shoppingCartItem;
            }

            var existingProduct = _shoppingCartItem?.Products?.FirstOrDefault(p => p.Product != null && p.Product.Id == product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.Amount += product.Amount;
            }
            else
            {
                _shoppingCartItem!.Products!.Add(new ProductAmount { Product = productDetails, Amount = product.Amount });
            }

            _shoppingCartItem!.Total += (product.Amount * productDetails.Price);

            // Update the shopping cart in the database
            _dbContext.Update(_shoppingCartItem);
            await _dbContext.SaveChangesAsync();

            return _shoppingCartItem;

        }


        public async Task<ShoppingCart> GetCart()
        {
            // Reload the shopping cart from the database to ensure it's up-to-date
            _shoppingCartItem = await _dbContext.ShoppingCarts.Include(sc => sc.Products!).ThenInclude(x => x.Product!).FirstOrDefaultAsync();

            if (_shoppingCartItem != null)
            {
                var cart = new ShoppingCart
                {
                    Products = _shoppingCartItem.Products,
                    Total = _shoppingCartItem.Total
                };


                //Check if discount is available
                var discounts = await _dbContext.Discounts.ToListAsync();

                // Apply discounts for each product in the cart
                foreach (var discount in discounts)
                {
                    _discount_service.ApplyDiscount(cart, discount.ProductId, discount.RequiredAmount);
                }


                return cart;
            }
            else
            {
                // Handle the case where the shopping cart is not found
                // You might want to return null or create a new ShoppingCart instance with default values
                return new ShoppingCart { };
            }
        }



        public async Task<bool> CheckIfProductExists(int productId)
        {
            return await _dbContext.Products.AnyAsync(p => p.Id == productId);
        }
    }
}