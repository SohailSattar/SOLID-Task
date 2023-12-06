using AutoMapper;
using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Application.DTO.Discounts;
using CoverGo.Task.Application.Features.Discounts.Handlers.Commands;
using CoverGo.Task.Application.Features.Discounts.Queries.Commands;
using CoverGo.Task.Application.Profiles;
using CoverGo.Task.Application.Responses;
using CoverGo.Task.Application.UnitTests.Mocks;
using CoverGo.Task.Domain;
using Moq;
using Shouldly;
using Xunit;

namespace CoverGo.Task.Application.UnitTests.Tests.Discounts.Commands
{
    public class CreateDiscountCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDiscountRepository> _mockRepo;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly CreateDiscountDto _validDiscountDto;
        private readonly CreateDiscountDto _invalidProductIdDto;
        private readonly CreateDiscountDto _invalidEmptyRequiredAmountDto;
        private readonly CreateDiscountDto _invalidNegativeRequiredAmountDto;
        private readonly CreateDiscountDto _invalidEmptyPercentageDto;
        private readonly CreateDiscountDto _invalidNegativePercentageDto;
        private readonly CreateDiscountDto _invalidPercentageGreaterThan100Dto;
        private readonly CreateDiscountCommandHandler _handler;

        public CreateDiscountCommandHandlerTests()
        {
            _mockProductRepo = MockProductRepository.GetProductRepository();
            _mockRepo = MockDiscountRepository.GetDiscountRepository();



            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateDiscountCommandHandler( _mockRepo.Object, _mapper);

            _validDiscountDto = new CreateDiscountDto
            {
                ProductId = 2,
                RequiredAmount = 2,
                DiscountPercentage = 50
            };

            _invalidProductIdDto = new CreateDiscountDto
            {
                ProductId = 0, // Invalid: product id cannot be zero
                RequiredAmount = 3,
                DiscountPercentage = 20
            };

            _invalidEmptyRequiredAmountDto = new CreateDiscountDto
            {
                ProductId = 2,
                RequiredAmount = 0, // Invalid: required amount cannot be zero
                DiscountPercentage = 15
            };

            _invalidNegativeRequiredAmountDto = new CreateDiscountDto
            {
                ProductId = 3,
                RequiredAmount = -5, // Invalid: required amount cannot be negative
                DiscountPercentage = 25
            };

            _invalidEmptyPercentageDto = new CreateDiscountDto
            {
                ProductId = 4,
                RequiredAmount = 4,
                DiscountPercentage = 0 // Invalid: percentage cannot be zero
            };

            _invalidNegativePercentageDto = new CreateDiscountDto
            {
                ProductId = 5,
                RequiredAmount = 5,
                DiscountPercentage = -30 // Invalid: percentage cannot be negative
            };

            _invalidPercentageGreaterThan100Dto = new CreateDiscountDto
            {
                ProductId = 6,
                RequiredAmount = 6,
                DiscountPercentage = 120 // Invalid: percentage cannot be greater than 100
            };
        }

        [Fact]
        public async System.Threading.Tasks.Task Valid_Discount_Added()
        {
            _mockRepo.Setup(repo => repo.CheckIfProductExists(It.IsAny<int>())).ReturnsAsync(true);
            // Act
            var result = await _handler.Handle(new CreateDiscountCommand { DiscountDto = _validDiscountDto }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeTrue();

            var discounts = await _mockRepo.Object.GetAll();

            // Currently, there are 3 existing discounts in the mock repository. 1 is just added.
            discounts.Count.ShouldBe(4);

            _mockRepo.Verify(repo => repo.Add(It.IsAny<Discount>()), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task Invalid_ProductId_NotAdded()
        {
            // Act
            var result = await _handler.Handle(new CreateDiscountCommand { DiscountDto = _invalidProductIdDto }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();

            var discounts = await _mockRepo.Object.GetAll();

            // No discount should be added - Original Count is 3
            discounts.Count.ShouldBe(3);

            _mockRepo.Verify(repo => repo.Add(It.IsAny<Discount>()), Times.Never);
        }

        [Fact]
        public async System.Threading.Tasks.Task Invalid_EmptyRequiredAmount_NotAdded()
        {
            // Act
            var result = await _handler.Handle(new CreateDiscountCommand { DiscountDto = _invalidEmptyRequiredAmountDto }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();

            var discounts = await _mockRepo.Object.GetAll();

            // No discount should be added - Original Count is 3
            discounts.Count.ShouldBe(3);

            _mockRepo.Verify(repo => repo.Add(It.IsAny<Discount>()), Times.Never);
        }

        [Fact]
        public async System.Threading.Tasks.Task Invalid_NegativeRequiredAmount_NotAdded()
        {
            // Act
            var result = await _handler.Handle(new CreateDiscountCommand { DiscountDto = _invalidNegativeRequiredAmountDto }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();

            var discounts = await _mockRepo.Object.GetAll();

            // No discount should be added - Original Count is 3
            discounts.Count.ShouldBe(3);

            _mockRepo.Verify(repo => repo.Add(It.IsAny<Discount>()), Times.Never);
        }

        [Fact]
        public async System.Threading.Tasks.Task Invalid_EmptyPercentage_NotAdded()
        {
            // Act
            var result = await _handler.Handle(new CreateDiscountCommand { DiscountDto = _invalidEmptyPercentageDto }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();

            var discounts = await _mockRepo.Object.GetAll();

            // No discount should be added - Original Count is 3
            discounts.Count.ShouldBe(3);

            _mockRepo.Verify(repo => repo.Add(It.IsAny<Discount>()), Times.Never);
        }

        [Fact]
        public async System.Threading.Tasks.Task Invalid_NegativePercentage_NotAdded()
        {
            // Act
            var result = await _handler.Handle(new CreateDiscountCommand { DiscountDto = _invalidNegativePercentageDto }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();

            var discounts = await _mockRepo.Object.GetAll();

            // No discount should be added - Original Count is 3
            discounts.Count.ShouldBe(3);

            _mockRepo.Verify(repo => repo.Add(It.IsAny<Discount>()), Times.Never);
        }

        [Fact]
        public async System.Threading.Tasks.Task Invalid_PercentageGreaterThan100_NotAdded()
        {
            // Act
            var result = await _handler.Handle(new CreateDiscountCommand { DiscountDto = _invalidPercentageGreaterThan100Dto }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            result.Success.ShouldBeFalse();

            var discounts = await _mockRepo.Object.GetAll();

            // No discount should be added - Original Count is 3
            discounts.Count.ShouldBe(3);

            _mockRepo.Verify(repo => repo.Add(It.IsAny<Discount>()), Times.Never);
        }
    }
}
