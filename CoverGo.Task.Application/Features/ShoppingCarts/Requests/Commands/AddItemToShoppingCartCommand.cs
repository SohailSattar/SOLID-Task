using CoverGo.Task.Application.DTO.ProductAmount;
using CoverGo.Task.Application.DTO.ShoppingCart;
using CoverGo.Task.Application.Responses;
using MediatR;

namespace CoverGo.Task.Application.Features.ShoppingCarts.Requests.Commands
{
    public class AddItemToShoppingCartCommand : IRequest<BaseCommandResponse>
    {
        public AddShoppingCartItemDto? ProductDto { get; set; }
    }
}
