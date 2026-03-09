using InnoShop.ProductService.Application.Features.Commands;
using InnoShop.ProductService.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace InnoShop.ProductService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IMediator mediator, IConfiguration configuration) : ControllerBase
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
            command.UserId=Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await mediator.Send(command);
            return Ok(new {result});
        }
        [AllowAnonymous]
        [HttpPost("hide-by-user/{userId}")]
        public async Task<IActionResult> HideUserById(Guid userId, [FromHeader(Name = "X-Internal-Key")] string? apiKey)
        {
            if (apiKey != configuration["InternalApiKey"])
                return Unauthorized();
            var result=await mediator.Send(new HideUserProductsCommand { UserId=userId} );
            return Ok(new { result });
        }
        [AllowAnonymous]
        [HttpPost("show-by-user/{userId}")]
        public async Task<IActionResult> ShowUserById(Guid userId, [FromHeader(Name = "X-Internal-Key")] string? apiKey)
        {
            if (apiKey != configuration["InternalApiKey"])
                return Unauthorized();
            var result = await mediator.Send(new ShowUserProductsCommand { UserId=userId});
            return Ok(new {result});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductCommand command)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var product = await mediator.Send(new GetProductByIdQuery { Id = id });
            if (product!.UserId != userId)
            {
                return StatusCode(403, new { error = "You can only edit your own product" });
            }
            command.Id = id;
            var result = await mediator.Send(command);
            return Ok(new { result });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var product = await mediator.Send(new GetProductByIdQuery { Id = id });
            if (product!.UserId != userId)
            {
                return StatusCode(403, new { error = "You can only edit your own product" });
            }

            var delete = await mediator.Send(new DeleteProductCommand { Id = id });
            return Ok(delete);
        }
    }
}
