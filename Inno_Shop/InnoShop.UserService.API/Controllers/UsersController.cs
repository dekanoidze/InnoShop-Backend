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
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user=await _mediator.Send(new GetAllUsersQuery());
            return Ok(user);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery{Id = id});
            return Ok(user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(Guid id, UpdateUserCommand updateUser)
        {
            updateUser.Id = id;
            var update=await _mediator.Send(updateUser);
            return Ok(update);
        }
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateById(Guid id,DeactivateUserCommand deactivateUser)
        {
            deactivateUser.Id = id;
            var deactivate = await _mediator.Send(deactivateUser);
            return Ok(deactivate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id, DeleteUserCommand deleteUser)
        {
            deleteUser.Id = id;
            var delete=await _mediator.Send(deleteUser);
            return Ok(delete);
        }
    }
}