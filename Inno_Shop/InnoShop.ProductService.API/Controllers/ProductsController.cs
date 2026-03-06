using InnoShop.ProductService.Application.Features.Commands;
using InnoShop.ProductService.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InnoShop.ProductService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await mediator.Send(new GetAllProductsQuery());
            return Ok(products) ;
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await mediator.Send(new GetProductByIdQuery { Id = id });
            return Ok(product) ;
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProductsByUserId(Guid userId)
        {
            var product=await mediator.Send(new GetProductsByUserIdQuery { UserId = userId });
            return Ok(product) ;
        }
        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync([FromQuery] SearchProductsQuery query)
        {
            var result=await mediator.Send(query);
            return Ok(result) ;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(new {result});
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductCommand command)
        {
            command.Id = id;
            var result = await mediator.Send(command);
            return Ok(new { result });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id, DeleteProductCommand command)
        {
            command.Id = id;
            var delete=await mediator.Send(command);
            return Ok(delete);
        }


    }
}
