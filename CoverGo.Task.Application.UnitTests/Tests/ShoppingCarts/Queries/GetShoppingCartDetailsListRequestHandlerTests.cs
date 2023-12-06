using AutoMapper;
using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Application.DTO.ShoppingCart;
using CoverGo.Task.Application.Features.ShoppingCarts.Handlers.Queries;
using CoverGo.Task.Application.Features.ShoppingCarts.Requests.Queries;
using CoverGo.Task.Application.Profiles;
using CoverGo.Task.Application.UnitTests.Mocks;
using CoverGo.Task.Domain;
using Moq;
using Shouldly;
using Xunit;

namespace CoverGo.Task.Application.UnitTests.Tests.ShoppingCarts.Queries
{
    public class GetShoppingCartDetailsListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IShoppingCartRepository> _mockShoppingCartRepo;
        private readonly GetShoppingCartDetailsListRequestHandler _handler;

        public GetShoppingCartDetailsListRequestHandlerTests()
        {
            _mockShoppingCartRepo = MockShoppingCartRepository.GetShoppingCartRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetShoppingCartDetailsListRequestHandler (_mockShoppingCartRepo.Object, _mapper);
        }

        [Fact]
        public async System.Threading.Tasks.Task Get_ShoppingCartDetails_List()
        {
            // Arrange
            var shoppingCart = new ShoppingCart
            {
                Id = 1,
                Products = new List<ProductAmount> { },
                Total = 100
            };

            _mockShoppingCartRepo.Setup(repo => repo.GetCart()).ReturnsAsync(shoppingCart);

            // Act
            var result = await _handler.Handle(new GetShoppingCartDetailsListRequest { }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<ShoppingCartDetailsDto>();
        }
    }
}
