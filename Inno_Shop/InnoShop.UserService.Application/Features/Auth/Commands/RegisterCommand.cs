using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InnoShop.UserService.Application.Features.Auth.Commands
{
    public class RegisterCommand : IRequest<string>
    {
        public string Name { get; set; }=string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
