using CoverGo.Task.Application.DTO.Discounts;
using CoverGo.Task.Application.Responses;
using MediatR;

namespace CoverGo.Task.Application.Features.Discounts.Queries.Commands
{
    public class CreateDiscountCommand : IRequest<BaseCommandResponse>
    {
        public CreateDiscountDto? DiscountDto { get; set; }
    }
}
