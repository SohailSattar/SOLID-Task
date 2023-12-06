using AutoMapper;
using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Application.DTO.Product.Validators;
using CoverGo.Task.Application.DTO.ShoppingCart.Validators;
using CoverGo.Task.Application.Features.ShoppingCarts.Requests.Commands;
using CoverGo.Task.Application.Responses;
using CoverGo.Task.Domain;
using MediatR;

namespace CoverGo.Task.Application.Features.ShoppingCarts.Handlers.Commands
{
    public class AddItemToShoppingCartCommandHandler : IRequestHandler<AddItemToShoppingCartCommand, BaseCommandResponse>
    {
        private readonly IShoppingCartRepository _shoppingCardRepository;
        private readonly IMapper _mapper;
        public AddItemToShoppingCartCommandHandler(IShoppingCartRepository shoppingCardRepository, IMapper mapper)
        {
            _shoppingCardRepository = shoppingCardRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(AddItemToShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new AddShoppingCartItemDtoValidator(_shoppingCardRepository);

            // Validate mandatory fields
            var validationResult = await validator.ValidateAsync(request.ProductDto!);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                return response;
            }


            // Check if Product Exists
            var isExists = await _shoppingCardRepository.CheckIfProductExists(request.ProductDto!.ProductId);

            if (!isExists)
            {
                response.Success = false;
                response.Message = "Creation failed.";
                response.Errors = new List<string> { "Product doesnt exist." };

                return response;
            }


            var productAmount = _mapper.Map<ProductAmount>(request.ProductDto);
             await _shoppingCardRepository.AddItem(productAmount);
           // await _shoppingCardRepository.Add(x);
            return new BaseCommandResponse { Success = true };
        }
    }
}