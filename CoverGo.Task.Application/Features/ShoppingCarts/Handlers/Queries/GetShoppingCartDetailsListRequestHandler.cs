using AutoMapper;
using CoverGo.Task.Application.Contracts.Persistence;
using CoverGo.Task.Application.DTO.ShoppingCart;
using CoverGo.Task.Application.Features.ShoppingCarts.Requests.Queries;
using MediatR;

namespace CoverGo.Task.Application.Features.ShoppingCarts.Handlers.Queries
{
    public class GetShoppingCartDetailsListRequestHandler : IRequestHandler<GetShoppingCartDetailsListRequest, ShoppingCartDetailsDto>
    {
        private readonly IShoppingCartRepository _shoppingCardRepository;
        private readonly IMapper _mapper;
        public GetShoppingCartDetailsListRequestHandler(IShoppingCartRepository shoppingCardRepository, IMapper mapper)
        {
            _shoppingCardRepository = shoppingCardRepository;
            _mapper = mapper;
        }
        public async Task<ShoppingCartDetailsDto> Handle(GetShoppingCartDetailsListRequest request, CancellationToken cancellationToken)
        {
            var cart = await _shoppingCardRepository.GetCart();
            return _mapper.Map<ShoppingCartDetailsDto>(cart);
        }
    }
}