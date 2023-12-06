using CoverGo.Task.Application.DTO.ShoppingCart;
using MediatR;

namespace CoverGo.Task.Application.Features.ShoppingCarts.Requests.Queries
{
    public class GetShoppingCartDetailsListRequest : IRequest<ShoppingCartDetailsDto>
    {
    }
}
