using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Domain;
using Moq;

namespace CoverGo.Task.Application.UnitTests.Mocks
{
    public class MockShoppingCartRepository
    {
        public static Mock<IShoppingCartRepository> GetShoppingCartRepository()
        {
            var mockRepo = new Mock<IShoppingCartRepository>();

            // Setup the method to get the shopping cart
            mockRepo.Setup(repo => repo.GetCart())
                .ReturnsAsync(() =>
                {
                    var shoppingCart = new ShoppingCart
                    {
                        Products = new List<ProductAmount>
                        {
                            new ProductAmount { ProductId = 1, Product = new Product{Id = 1, Name = "bike", Price = 150 }, Amount = 2 },
                            new ProductAmount { ProductId = 2, Product = new Product{Id = 1, Name = "tennis racket", Price = 15 }, Amount = 3 }
                        }
                    };

                    // Calculate total based on the products in the cart
                    shoppingCart.Total = CalculateTotal(shoppingCart.Products);

                    return shoppingCart;
                });

            return mockRepo;
        }

        private static decimal CalculateTotal(List<ProductAmount> products)
        {
            return products.Sum(item => item.Product!.Price * item.Amount);
        }
    }
}
