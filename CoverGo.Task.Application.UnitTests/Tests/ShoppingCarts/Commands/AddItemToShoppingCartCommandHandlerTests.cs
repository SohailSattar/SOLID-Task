using AutoMapper;
using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Application.Features.ShoppingCarts.Handlers.Commands;
using CoverGo.Task.Application.Features.ShoppingCarts.Requests.Commands;
using CoverGo.Task.Application.Profiles;
using CoverGo.Task.Application.Responses;
using CoverGo.Task.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace CoverGo.Task.Application.UnitTests.Tests.ShoppingCarts.Commands
{
    public class AddItemToShoppingCartCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IShoppingCartRepository> _mockRepo;
        private readonly AddItemToShoppingCartCommandHandler _handler;

        public AddItemToShoppingCartCommandHandlerTests()
        {
            _mockRepo = MockShoppingCartRepository.GetShoppingCartRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new AddItemToShoppingCartCommandHandler(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async System.Threading.Tasks.Task Can_Add_Item_To_ShoppingCart()
        {
            _mockRepo.Setup(repo => repo.CheckIfProductExists(It.IsAny<int>())).ReturnsAsync(true);
            // Arrange
            var command = new AddItemToShoppingCartCommand
            {
                ProductDto = new DTO.ShoppingCart.AddShoppingCartItemDto {
                    ProductId = 1,
                    Amount = 2
                }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();
        }

        [Fact]
        public async System.Threading.Tasks.Task Cannot_Add_NonExisting_Product()
        {
            // Arrange
            var command = new AddItemToShoppingCartCommand
            {
                ProductDto = new DTO.ShoppingCart.AddShoppingCartItemDto {
                    ProductId = 100, // Non-existing product
                    Amount = 2
                }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
            result.Errors!.ShouldContain("Product doesnt exist.");
        }

        [Theory]
        [InlineData(0)] // Empty Product Id
        public async System.Threading.Tasks.Task Product_Id_Cannot_Be_Empty(int productId)
        {
            // Arrange
            var command = new AddItemToShoppingCartCommand
            {
                ProductDto = new DTO.ShoppingCart.AddShoppingCartItemDto
                {
                    ProductId = productId,
                    Amount = 2
                }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
            result.Errors!.ShouldContain("Product Id cannot be empty.");
        }

        [Theory]
        [InlineData(0)] // Empty Amount
        public async System.Threading.Tasks.Task Amount_Cannot_Be_Empty(int amount)
        {
            // Arrange
            var command = new AddItemToShoppingCartCommand
            {
               ProductDto = new DTO.ShoppingCart.AddShoppingCartItemDto
               {
                   ProductId = 1,
                   Amount = amount
               }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
            result.Errors!.ShouldContain("Amount cannot be empty.");
        }

        [Theory]
        [InlineData(-1)] // Negative Amount
        public async System.Threading.Tasks.Task Amount_Cannot_Be_Negative(int amount)
        {
            // Arrange
            var command = new AddItemToShoppingCartCommand
            {
                ProductDto = new DTO.ShoppingCart.AddShoppingCartItemDto
                {
                    ProductId = 1,
                    Amount = amount
                }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();
            result.Errors!.ShouldContain("Amount cannot be negative.");
        }
    }
}
