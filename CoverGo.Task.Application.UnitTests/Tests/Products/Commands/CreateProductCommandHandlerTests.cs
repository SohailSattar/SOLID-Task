using AutoMapper;
using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Application.DTO.Product;
using CoverGo.Task.Application.Features.Products.Handlers.Commands;
using CoverGo.Task.Application.Features.Products.Requests.Commands;
using CoverGo.Task.Application.Profiles;
using CoverGo.Task.Application.Responses;
using CoverGo.Task.Application.UnitTests.Mocks;
using CoverGo.Task.Domain;
using Moq;
using Shouldly;
using Xunit;

namespace CoverGo.Task.Application.UnitTests.Tests.Products.Commands
{
    public class CreateProductCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly CreateProductDto _validProductDto;
        private readonly CreateProductDto _invalidNameProductDto;
        private readonly CreateProductDto _invalidPriceProductDto;
        private readonly CreateProductCommandHandler _handler;
        public CreateProductCommandHandlerTests()
        {
            _mockRepo = MockProductRepository.GetProductRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateProductCommandHandler(_mockRepo.Object, _mapper);

            _validProductDto = new CreateProductDto
            {
                Price = 15,
                Name = "Bike"
            };

            _invalidNameProductDto = new CreateProductDto
            {
                Price = 20,
                Name = string.Empty // Invalid: empty product name
            };

            _invalidPriceProductDto = new CreateProductDto
            {
                Price = -15, // Invalid: negative price
                Name = "Car"
            };
        }

        [Fact]
        public async System.Threading.Tasks.Task Valid_Product_Added()
        {
            // Act
            var result = await _handler.Handle(new CreateProductCommand { ProductDto = _validProductDto }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();

            var products = await _mockRepo.Object.GetAll();

            // Currently there are 5 existing products in the mock repository. 1 is just added.
            products.Count.ShouldBe(6);

            _mockRepo.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Invalid_Name_Product_NotAdded()
        {
            // Act
            var result = await _handler.Handle(new CreateProductCommand { ProductDto = _invalidNameProductDto }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();

            var products = await _mockRepo.Object.GetAll();

            // No product should be added - Original Count is 5
            products.Count.ShouldBe(5); 

            _mockRepo.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async System.Threading.Tasks.Task Invalid_Price_Product_NotAdded()
        {
            // Act
            var result = await _handler.Handle(new CreateProductCommand { ProductDto = _invalidPriceProductDto }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();

            var products = await _mockRepo.Object.GetAll();

            // No product should be added - Original count is 5
            products.Count.ShouldBe(5); 

            _mockRepo.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Never);
        }
    }
}
