using CoverGo.Task.Application.DTO.Discounts;
using CoverGo.Task.Application.DTO.Product;
using CoverGo.Task.Application.Features.Discounts.Queries.Commands;
using CoverGo.Task.Application.Features.Products.Requests.Commands;
using CoverGo.Task.Application.Features.Products.Requests.Queries;
using CoverGo.Task.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoverGo.Task.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
                _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> Get()
        {
            var command = new GetProductsListRequest { };
            var repsonse = await _mediator.Send(command);
            return Ok(repsonse);
        }

        [HttpPost]
        //[Authorize]   <- In real world setting, this API will be authorized only for Admin 
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateProductDto product)
        {
            var command = new CreateProductCommand { ProductDto = product };
            var repsonse = await _mediator.Send(command);
            return Ok(repsonse);
        }

        [HttpPost("discount")]
        //[Authorize]   <- In real world setting, this API will be authorized only for Admin 
        public async Task<ActionResult<BaseCommandResponse>> PostDiscount([FromBody] CreateDiscountDto discount)
        {
            var command = new CreateDiscountCommand { DiscountDto = discount };
            var repsonse = await _mediator.Send(command);
            return Ok(repsonse);
        }
    }
}