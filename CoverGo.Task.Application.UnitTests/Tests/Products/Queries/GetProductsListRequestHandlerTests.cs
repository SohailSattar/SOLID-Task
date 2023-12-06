using AutoMapper;
using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Application.DTO.Product;
using CoverGo.Task.Application.Features.Products.Handlers.Requests;
using CoverGo.Task.Application.Features.Products.Requests.Queries;
using CoverGo.Task.Application.Profiles;
using CoverGo.Task.Application.UnitTests.Mocks;
using CoverGo.Task.Domain;
using Moq;
using Shouldly;
using Xunit;

namespace CoverGo.Task.Application.UnitTests.Tests.Products.Queries
{
    public class GetProductsListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly GetProductsListRequestHandler _handler;

        public GetProductsListRequestHandlerTests()
        {
            _mockRepo = MockProductRepository.GetProductRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetProductsListRequestHandler(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async System.Threading.Tasks.Task Get_Products_List()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Bike", Price = 15 },
                new Product { Id = 2, Name = "Car", Price = 20 }
            };

            _mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(products);

            // Act
            var result = await _handler.Handle(new GetProductsListRequest(), CancellationToken.None);

            // Assert
            result.ShouldBeOfType<List<ProductDto>>();

            // Ensuring the correct number of products is returned
            result.Count.ShouldBe(2);
        }
    }
}
