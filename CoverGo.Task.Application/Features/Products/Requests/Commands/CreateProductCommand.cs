using CoverGo.Task.Application.DTO.Product;
using CoverGo.Task.Application.Responses;
using MediatR;

namespace CoverGo.Task.Application.Features.Products.Requests.Commands
{
    public class CreateProductCommand : IRequest<BaseCommandResponse>
    {
        public CreateProductDto? ProductDto { get; set; }
    }
}
