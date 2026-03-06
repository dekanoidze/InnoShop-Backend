using InnoShop.UserService.Application.Features.Users.Commands;
using InnoShop.UserService.Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InnoShop.UserService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user=await mediator.Send(new GetAllUsersQuery());
            return Ok(user);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await mediator.Send(new GetUserByIdQuery{Id = id});
            return Ok(user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(Guid id, UpdateUserCommand updateUser)
        {
            updateUser.Id = id;
            var update=await mediator.Send(updateUser);
            return Ok(update);
        }
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateById(Guid id)
        {
            var command = new DeactivateUserCommand { Id = id};
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPut("{id}/reactive")]
        public async Task<IActionResult> ReactivateById(Guid id)
        {
            var command=new ReactivateUserCommand { Id = id};
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id, DeleteUserCommand deleteUser)
        {
            deleteUser.Id = id;
            var delete=await mediator.Send(deleteUser);
            return Ok(delete);
        }
    }
}