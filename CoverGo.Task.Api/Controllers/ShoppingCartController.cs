using CoverGo.Task.Application.DTO.ProductAmount;
using CoverGo.Task.Application.DTO.ShoppingCart;
using CoverGo.Task.Application.Features.ShoppingCarts.Requests.Commands;
using CoverGo.Task.Application.Features.ShoppingCarts.Requests.Queries;
using CoverGo.Task.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoverGo.Task.Api.Controllers
{
    [Route("api/shopping-cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShoppingCartController(IMediator mediator)
        {
                _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCartDetailsDto>> Get()
        {
            var command = new GetShoppingCartDetailsListRequest { };
            var repsonse = await _mediator.Send(command);
            return Ok(repsonse);
        }


        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] AddShoppingCartItemDto productDto)
        {
            var command = new AddItemToShoppingCartCommand { ProductDto = productDto };
            var repsonse = await _mediator.Send(command);
            return Ok(repsonse);
        }
    }
}
