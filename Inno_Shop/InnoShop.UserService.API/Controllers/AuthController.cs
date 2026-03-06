using InnoShop.UserService.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.UserService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var token = await mediator.Send(command);
            return Ok(new { token });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var token=await mediator.Send(command);
            return Ok(new { token });
        }
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command)
        {
            var token=await mediator.Send(command);
            return Ok(new { token });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            var token=await mediator.Send(command);
            return Ok(new { token });
        }
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfrimEmail(ConfirmEmailCommand command)
        {
            return Ok(new { command }); 
        }
    }
}
