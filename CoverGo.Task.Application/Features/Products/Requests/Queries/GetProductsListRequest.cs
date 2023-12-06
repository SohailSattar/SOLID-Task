using CoverGo.Task.Application.DTO.Product;
using MediatR;

namespace CoverGo.Task.Application.Features.Products.Requests.Queries
{
    public class GetProductsListRequest : IRequest<IReadOnlyList<ProductDto>>
    {
    }
}
