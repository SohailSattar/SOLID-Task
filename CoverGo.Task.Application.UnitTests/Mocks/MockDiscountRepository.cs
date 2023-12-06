using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Domain;
using Moq;

namespace CoverGo.Task.Application.UnitTests.Mocks
{
    public class MockDiscountRepository
    {
        public static Mock<IDiscountRepository> GetDiscountRepository()
        {
            var discounts = new List<Discount>
            {
                new Discount
                {
                    ProductId = 2,
                    RequiredAmount = 3,
                    DiscountPercentage = 15
                },
                new Discount
                {
                    ProductId = 3,
                    RequiredAmount = 2,
                    DiscountPercentage = 50
                },
                new Discount
                {
                    ProductId = 5,
                    RequiredAmount = 5,
                    DiscountPercentage = 25
                }
            };

            var mockRepo = new Mock<IDiscountRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(discounts);


            mockRepo.Setup(r => r.Add(It.IsAny<Discount>())).ReturnsAsync((Discount discount) =>
            {
                discounts.Add(discount);
                return discount;
            });

            return mockRepo;

        }
    }
}
