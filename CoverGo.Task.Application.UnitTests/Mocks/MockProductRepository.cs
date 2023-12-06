using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Domain;
using Moq;

namespace CoverGo.Task.Application.UnitTests.Mocks
{
    public class MockProductRepository
    {
        public static Mock<IProductRepository> GetProductRepository()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                   Name = "tennis ball",
                   Price = 5
                },
                new Product
                {
                    Id = 2,
                   Name = "tennis racket",
                   Price = 20
                },
                new Product
                {
                    Id = 3,
                   Name = "t-shirt",
                   Price = 10
                },
                new Product
                {
                    Id = 4,
                   Name = "nun-chucks",
                   Price = 25
                },
                new Product
                {
                    Id = 5,
                   Name = "helmet",
                   Price = 15
                }
            };

            var mockRepo = new Mock<IProductRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(products);

            mockRepo.Setup(r => r.Add(It.IsAny<Product>())).ReturnsAsync((Product product) =>
            {
                products.Add(product);
                return product;
            });

            return mockRepo;

        }
    }
}
